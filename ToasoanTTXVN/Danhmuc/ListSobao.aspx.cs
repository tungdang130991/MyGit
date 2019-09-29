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
    public partial class ListSobao : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        private int EID
        {
            get
            {
                if (ViewState["EID"] == null)
                    ViewState["EID"] = -1;
                return (int)ViewState["EID"];
            }
            set
            {
                ViewState["EID"] = value;
            }
        }
        UltilFunc ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    btnAdd.Visible = _Role.R_Read;
                    if (!IsPostBack)
                    {
                        if (_user == null)
                        {
                            Page.Response.Redirect("~/login.aspx", true);
                        }
                        else
                        {
                            cbo_loaibao.Items.Clear();
                            UltilFunc.BindCombox(this.cbo_loaibao, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", "---");
                            if (Session["CurrentPage"] != null)
                            {
                                pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                                Session["CurrentPage"] = null;
                                Danhsach_Sobao();
                            }
                            else
                                Danhsach_Sobao();
                        }
                    }

                }
            }
        }
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _Role.R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _Role.R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _Role.R_Read;
        }
        #region Methods
        public void Danhsach_Sobao()
        {
            string where = " 1=1 ";
            if (cbo_loaibao.SelectedIndex > 0)
                where += "  and Ma_AnPham=" + cbo_loaibao.SelectedValue.ToString();
            if (!String.IsNullOrEmpty(this.txtTen_Sobao.Text.Trim()))
                where += " AND " + string.Format(" Ten_Sobao like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTen_Sobao.Text.Trim()));
            pages.PageSize = Global.MembersPerPage;
            SobaoDAL _sobaoDAL = new SobaoDAL();
            DataSet _ds;
            _ds = _sobaoDAL.BindGridT_Sobao(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _sobaoDAL.BindGridT_Sobao(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds.Tables[0];
            grdList.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
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
        protected string TenAnpham(Object _maAnpham)
        {
            string strReturn = "";
            AnPhamDAL _anphamDAL = new AnPhamDAL();
            if (_maAnpham != DBNull.Value)
                if (_anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham)) != null)
                    strReturn = _anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham)).Ten_AnPham;

            return strReturn;
        }
        private bool checkdelete(int _masobao)
        {
            DataTable _dtsb = new DataTable();
            string _sql = "select Ma_Sobao from T_Sobao where Ma_Sobao in (select Ma_Sobao from T_TinBai where Ma_Sobao=" + _masobao + ")";
            _dtsb = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dtsb != null && _dtsb.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Danhmuc/EditSobao.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Danhsach_Sobao();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_Sobao();
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                Label lblSTT = (Label)e.Item.FindControl("lblSTT");
                if (lblSTT != null)
                {
                    lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
                }
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null)
                {
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else

                        btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
                }
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
            SobaoDAL objDAL = new SobaoDAL();
            int _id = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                Response.Redirect("~/Danhmuc/EditSobao.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _id);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                if (!checkdelete(_id))
                {
                    objDAL.DeleteOneFromT_Sobao(_id);
                    action.Thaotac = "[Xóa T_Sobao]-->[Thao tác Xóa Trong Bảng T_Sobao][ID:" + _id + " ]";
                    this.Danhsach_Sobao();
                }
                else
                {
                    FuncAlert.AlertJS(this, " Số báo đã được biên tập tin bài, nên không thể xóa!");
                    return;
                }
            }
        }
        #endregion

    }
}
