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
    public partial class List_PhongBan : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        T_Phongban _PB = new T_Phongban();
        HPCBusinessLogic.DAL.PhongBan_DAL _DAL = new HPCBusinessLogic.DAL.PhongBan_DAL();
        UltilFunc _ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    btnAdd.Visible = _Role.R_Read;
                    if (!IsPostBack)
                    {
                        lblMessError.Text = "";
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            BindList_Phongban();
                        }
                        else
                        {
                            BindList_Phongban();
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
        #region Method
        public void BindList_Phongban()
        {
            string where = " 1=1 ";
            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_PhongBan(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_PhongBan(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataView _dv = _ds.Tables[0].DefaultView;

                GridViewPhongBan.DataSource = _dv;
                GridViewPhongBan.DataBind();
                GridViewPhongBan.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GridViewPhongBan.DataSource = _ds;
                GridViewPhongBan.DataBind();
                int columncount = GridViewPhongBan.Rows[0].Cells.Count;
                GridViewPhongBan.Rows[0].Cells.Clear();
                GridViewPhongBan.Rows[0].Cells.Add(new TableCell());
                GridViewPhongBan.Rows[0].Cells[0].ColumnSpan = columncount;
                GridViewPhongBan.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GridViewPhongBan.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
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
        private bool isExist(string CustomerName, double ID)
        {
            string _sql = "select * from T_Phongban where Ten_Phongban=N'" + CustomerName + "' and Ma_Phongban<>" + ID.ToString();
            DataTable d = _ulti.ExecSqlDataSet(_sql).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExist(string CustomerName)
        {
            string _sql = "select * from T_Phongban where Ten_Phongban=N'" + CustomerName + "'";
            DataTable d = _ulti.ExecSqlDataSet(_sql).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Click

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindList_Phongban();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GridViewPhongBan.ShowFooter = true;
            BindList_Phongban();
        }
        #endregion

        #region Gridview
        protected void GridViewPhongBan_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                ImageButton btnAdd = (ImageButton)e.Row.FindControl("btnAdd");
                if (btnAdd != null)
                    if (!_Role.R_Write)
                        btnAdd.Enabled = _Role.R_Write;
                if (btnDelete != null)
                {
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Bạn có chắc chắn muốn xóa?');");
                   
                }
                Label lblSTT = (Label)e.Row.FindControl("lblSTT");
                if (lblSTT != null)
                {
                    lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Row.RowIndex + 1).ToString();
                }

                e.Row.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void AddNewRecord(object sender, EventArgs e)
        {
            GridViewPhongBan.ShowFooter = true;
            BindList_Phongban();

        }
        protected void GridViewPhongBan_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            if (e.CommandName.Equals("AddNew"))
            {
                TextBox _tenphongban = (TextBox)GridViewPhongBan.FooterRow.FindControl("txt_Tenphong");

                _PB.Ma_Phongban = 0;
                _PB.Ten_Phongban = _tenphongban.Text.Trim();
                if (!isExist(_tenphongban.Text.Trim()))
                    _DAL.InsertT_Phongban(_PB);
                else
                    FuncAlert.AlertJS(this, "Tên phòng ban đã tồn tại!");
                action.Thaotac = "[Thêm mới phòng ban]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindList_Phongban();

            }

        }
        protected void GridViewPhongBan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPhongBan.PageIndex = e.NewPageIndex;

        }
        protected void GridViewPhongBan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            int _id = Convert.ToInt32(GridViewPhongBan.DataKeys[e.RowIndex].Value.ToString());
            _DAL.DeleteOneFromT_Phongban(_id);
            BindList_Phongban();
        }
        protected void GridViewPhongBan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPhongBan.EditIndex = e.NewEditIndex;
            BindList_Phongban();

        }
        protected void GridViewPhongBan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            int _return;
            int _id = Convert.ToInt32(GridViewPhongBan.DataKeys[e.RowIndex].Value.ToString());

            TextBox _tenphongban = (TextBox)GridViewPhongBan.Rows[e.RowIndex].FindControl("txt_Tenphong");

            _PB.Ma_Phongban = _id;
            _PB.Ten_Phongban = _tenphongban.Text.Trim();
            if (!isExist(_tenphongban.Text.Trim(), _id))
                _return = _DAL.InsertT_Phongban(_PB);
            else
                FuncAlert.AlertJS(this, "Tên phòng ban đã tồn tại!");
            action.Thaotac = "[Sửa phòng ban]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
            GridViewPhongBan.EditIndex = -1;
            BindList_Phongban();

        }
        protected void GridViewPhongBan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPhongBan.EditIndex = -1;
            BindList_Phongban();
            lblMessError.Text = "";
        }
        #endregion


    }
}
