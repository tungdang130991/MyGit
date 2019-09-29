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
    public partial class List_NgonNgu : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_NgonNgu _NN = new T_NgonNgu();
        HPCBusinessLogic.DAL.NgonNgu_DAL _DAL = new HPCBusinessLogic.DAL.NgonNgu_DAL();
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
        #region Method
        public void BindList_Phongban()
        {
            string where = " 1=1 ";
            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_NgonNgu(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_NgonNgu(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataView _dv = _ds.Tables[0].DefaultView;

                GridViewNgonNgu.DataSource = _dv;
                GridViewNgonNgu.DataBind();
                GridViewNgonNgu.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GridViewNgonNgu.DataSource = _ds;
                GridViewNgonNgu.DataBind();
                int columncount = GridViewNgonNgu.Rows[0].Cells.Count;
                GridViewNgonNgu.Rows[0].Cells.Clear();
                GridViewNgonNgu.Rows[0].Cells.Add(new TableCell());
                GridViewNgonNgu.Rows[0].Cells[0].ColumnSpan = columncount;
                GridViewNgonNgu.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GridViewNgonNgu.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }
        protected string Hoatdong(Object Hoatdong)
        {
            string strReturn = "";
            if (Hoatdong != DBNull.Value)
            {
                if (bool.Parse(Hoatdong.ToString()))
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            return strReturn;
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
            string _sql = "select * from T_NgonNgu where TenNgonNgu=N'" + CustomerName + "' and ID<>" + ID.ToString();
            DataTable d = _ulti.ExecSqlDataSet(_sql).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExist(string CustomerName)
        {
            string _sql = "select * from T_NgonNgu where TenNgonNgu=N'" + CustomerName + "'";
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
            GridViewNgonNgu.ShowFooter = true;
            BindList_Phongban();
        }
        #endregion

        #region Gridview
        protected void GridViewNgonNgu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            Label lblSTT = (Label)e.Row.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Row.RowIndex + 1).ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void AddNewRecord(object sender, EventArgs e)
        {
            GridViewNgonNgu.ShowFooter = true;
            BindList_Phongban();

        }
        protected void GridViewNgonNgu_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("AddNew"))
            {
                TextBox _tenphongban = (TextBox)GridViewNgonNgu.FooterRow.FindControl("txt_Tenphong");
                TextBox _Thutu = (TextBox)GridViewNgonNgu.FooterRow.FindControl("txt_STT");
                CheckBox chk_Hoatdong = (CheckBox)GridViewNgonNgu.FooterRow.FindControl("chk_Hoatdong");
                _NN.ID = 0;
                _NN.TenNgonNgu = _tenphongban.Text.Trim();
                _NN.ThuTu = int.Parse(_Thutu.Text.Trim());
                _NN.HoatDong = chk_Hoatdong.Checked;
                if (!isExist(_tenphongban.Text.Trim()))
                    _DAL.InsertT_NgonNgu(_NN);
                else
                    FuncAlert.AlertJS(this, "Tên phòng ban đã tồn tại!");
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), "thêm ngôn ngữ");
                BindList_Phongban();

            }

        }
        protected void GridViewNgonNgu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewNgonNgu.PageIndex = e.NewPageIndex;

        }
        protected void GridViewNgonNgu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int _id = Convert.ToInt32(GridViewNgonNgu.DataKeys[e.RowIndex].Value.ToString());
            _DAL.DeleteOneFromT_NgonNgu(_id);
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), "Xóa ngôn ngữ");
            BindList_Phongban();
        }
        protected void GridViewNgonNgu_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewNgonNgu.EditIndex = e.NewEditIndex;
            BindList_Phongban();

        }
        protected void GridViewNgonNgu_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _return;
            int _id = Convert.ToInt32(GridViewNgonNgu.DataKeys[e.RowIndex].Value.ToString());

            TextBox _tenphongban = (TextBox)GridViewNgonNgu.Rows[e.RowIndex].FindControl("txt_Tenphong");
            TextBox _Thutu = (TextBox)GridViewNgonNgu.Rows[e.RowIndex].FindControl("txt_STT");
            CheckBox chk_Hoatdong = (CheckBox)GridViewNgonNgu.Rows[e.RowIndex].FindControl("chk_Hoatdong");
            _NN.ID = _id;
            _NN.TenNgonNgu = _tenphongban.Text.Trim();
            _NN.ThuTu = int.Parse(_Thutu.Text.Trim());
            _NN.HoatDong = chk_Hoatdong.Checked;
            if (!isExist(_tenphongban.Text.Trim(), _id))
                _return = _DAL.InsertT_NgonNgu(_NN);
            else
                FuncAlert.AlertJS(this, "Tên phòng ban đã tồn tại!");
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), "Sửa ngôn ngữ");
            GridViewNgonNgu.EditIndex = -1;
            BindList_Phongban();

        }
        protected void GridViewNgonNgu_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewNgonNgu.EditIndex = -1;
            BindList_Phongban();
            lblMessError.Text = "";
        }
        #endregion
    }
}
