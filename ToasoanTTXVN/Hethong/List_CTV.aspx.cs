using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Hethong
{
    public partial class List_CTV : BasePage
    {
        private bool _refreshState;
        private bool _isRefresh;
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] AllStates = (object[])savedState;
                base.LoadViewState(AllStates[0]);
                _refreshState = bool.Parse(AllStates[1].ToString());
                _isRefresh = _refreshState ==
                bool.Parse(Session["__ISREFRESH"].ToString());
            }
            catch
            { }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;

        private int userID
        {
            get { if (ViewState["userID"] != null) return Convert.ToInt32(ViewState["userID"]); else return 0; }

            set { ViewState["userID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public void Search_OnClick(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hethong/Edit_CTV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
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
        public void LoadData()
        {
            string where = " Loai=1 and (Trangthai_Xoa=0 or Trangthai_Xoa is null) and NguoiTao=" + _user.UserID;
            if (!String.IsNullOrEmpty(txtSearch_UserName.Text.Trim()))
                where += "AND " + string.Format(" Ten_Dangnhap like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_UserName.Text.Trim()));
            if (!String.IsNullOrEmpty(txt_userfullname.Text.Trim()))
                where += "AND " + string.Format(" TenDaydu like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_userfullname.Text.Trim()));
            pages.PageSize = 1000;
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new HPCBusinessLogic.NguoidungDAL();
            DataSet _ds;
            _ds = _NguoidungDAL.BindGridT_Nguoidung(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _NguoidungDAL.BindGridT_Nguoidung(pages.PageIndex - 1, pages.PageSize, where);
            DataGridCTV.DataSource = _ds;
            DataGridCTV.DataBind();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }

        #region Tim KIEM
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        #endregion
        public void grdListUser_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListUser_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;

            #endregion
            if (e.CommandArgument.ToString().ToLower() == "editusers")
                Response.Redirect("~/Hethong/Edit_CTV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + this.DataGridCTV.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                userID = Convert.ToInt32(DataGridCTV.DataKeys[e.Item.ItemIndex]);
                if (_NguoidungDAL.CheckDelete_users(userID))
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn không được xóa CTV đã được chọn tác giả cho tin bài xuất bản.!');", true);
                    return;
                }
                else
                {
                    _NguoidungDAL.DeleteFromT_Nguoidung(userID);
                    action.Thaotac = "[Xóa UserID]-->[Thao tác Xóa Trong Bảng T_USERS][UserID:" + userID.ToString() + " ]";

                    this.LoadData();
                }
            }

            action.Ma_Chucnang = int.Parse(Page.Request["Menu_ID"].ToString());
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

        }
    }
}
