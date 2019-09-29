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
namespace ToasoanTTXVN.Danhmuc
{
    public partial class ListKhachHang : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    //_Role = _userDAL.getGetRole4UserMenu(_user.Ma_Nguoidung, Convert.ToInt32(Request["Menu_ID"]));
                    //this.btnAddMenu.Enabled = _Role.R_Add;
                    if (!IsPostBack)
                    {
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            Session["CurrentPage"] = null;
                            Danhsach_Khachhang();
                        }
                        else
                            Danhsach_Khachhang();
                    }

                }
            }
        }
        #region Methods
        public void Danhsach_Khachhang()
        {
            string where = "  Loai_KH = 1 ";
            if (!String.IsNullOrEmpty(this.txtTen_Khachhang.Text.Trim()))
                where += " " + string.Format(" Ten_KhachHang like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTen_Khachhang.Text.Trim()));            
            
            pages.PageSize = Global.MembersPerPage;
            KhachhangDAL _khachhangDAL = new KhachhangDAL();
            DataSet _ds;
            _ds = _khachhangDAL.BindGridT_Khachhang(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _khachhangDAL.BindGridT_Khachhang(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds.Tables[0];
            grdList.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
            grdList.Columns[6].Visible = IsRoleDelete();
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
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Read;
        }

        protected bool IsCustomerExsits(int _ID)
        {
            bool _exits = false;
            DataSet _ds;
            UltilFunc _ultil = new UltilFunc();
            _ds = _ultil.ExecSqlDataSet("select Ma_Khachhang from T_Yeucau where Ma_Khachhang = " + _ID.ToString());
            if (_ds.Tables[0].Rows.Count > 0)
                _exits = true;
            else
                _exits = false;
            return _exits;
        }

        protected string SortOrder
        {
            get
            {
                if (ViewState["_SortOrder"] == null)
                    return "asc";
                else
                    return ViewState["_SortOrder"].ToString();
            }
            set { ViewState["_SortOrder"] = "" + value; }
        }
        protected string SortExtension
        {
            get
            {
                if (ViewState["_SortExtension"] == null)
                    return "";
                else
                    return ViewState["_SortExtension"].ToString();
            }
            set { ViewState["_SortExtension"] = "" + value; }
        }

        //protected string Hoatdong(string str)
        //{
        //    string strReturn = "";
        //    if (str == "True")
        //        strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
        //    if (str == "False")
        //        strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
        //    return strReturn;
        //}
        #endregion

        #region Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Danhmuc/EditKhachHang.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Danhsach_Khachhang();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_Khachhang();
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            //if (btnDelete != null)
            //{
            //    btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            //}
            if (btnDelete != null)
            {
                bool _bool;
                _bool = IsCustomerExsits(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                if (_bool == true)
                    btnDelete.Attributes.Add("onclick", "return alert(\"Khách hàng này đang có yêu cầu phát hành\");");
                else
                    btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            KhachhangDAL objDAL = new KhachhangDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int cusID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Danhmuc/EditKhachHang.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + cusID);
            }           
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                objDAL.DeleteOneFromT_KhachHang(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                action.Thaotac = "[Xóa T_KhachHang]-->[Thao tác Xóa Trong Bảng T_KhachHang][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                this.Danhsach_Khachhang();
            }
        }
        #endregion
    }
}
