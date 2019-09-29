using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
namespace ToasoanTTXVN.Menu
{
    public partial class ListMenu : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            Danhsach_Chucnang();
                            Session["CurrentPage"] = null;
                        }
                        else
                            Danhsach_Chucnang();
                    }
                }
            }
        }
        #region Method
        private void Danhsach_Chucnang()
        {
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.ChucnangDAL _chucnangDAL = new HPCBusinessLogic.ChucnangDAL();
            DataSet _ds;
            _ds = _chucnangDAL.BindGridT_Chucnang(pages.PageIndex, pages.PageSize, "Ma_Chucnang_Cha=0");
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _chucnangDAL.BindGridT_Chucnang(pages.PageIndex - 1, pages.PageSize, "Ma_Chucnang_Cha=0");
            DataView _dv = _chucnangDAL.BindGridT_Chucnang_dataview(_ds.Tables[0]);
            gdListMenu.DataSource = _dv;
            gdListMenu.DataBind();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
        }
        #endregion

        #region Click
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Menu/EditMenu.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        public void gdListMenu_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDeleteAll = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDeleteAll != null)
            {
                btnDeleteAll.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void gdListMenu_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                Response.Redirect("~/Menu/EditMenu.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + this.gdListMenu.DataKeys[e.Item.ItemIndex].ToString());
            }            
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                ChucnangDAL _chucnangDAL = new ChucnangDAL();
                int _menuID = Convert.ToInt32(this.gdListMenu.DataKeys[e.Item.ItemIndex].ToString());
                _chucnangDAL.DeleteFromT_ChucnangByID(_menuID);
                action.Thaotac = "[Thao tác Xóa][Mã chức năng:" + _menuID.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Danhsach_Chucnang();
            }            
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_Chucnang();
        }
        protected string IpAddress()
        {
            string strIp;
            strIp = Page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = Page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
        #endregion
    }
}
