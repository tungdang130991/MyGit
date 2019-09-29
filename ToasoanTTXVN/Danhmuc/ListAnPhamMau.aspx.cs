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
    public partial class ListAnPhamMau : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_AnPhamMau _ap = null;
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
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            BindList_AnphamMau();
                        }
                        else
                        {
                            BindList_AnphamMau();
                        }
                    }
                }
            }
        }

        #region Method Gridview
        public void BindList_AnphamMau()
        {
            string where = " 1=1 ";

            AnPhamMauDAL _DAL = new AnPhamMauDAL();
            DataSet _ds;
            pages.PageSize = 10;
            _ds = _DAL.BindGridT_Anphammau(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Anphammau(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                GVAnphamMau.DataSource = _ds;
                GVAnphamMau.DataBind();
                GVAnphamMau.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVAnphamMau.DataSource = _ds;
                GVAnphamMau.DataBind();
                int columncount = GVAnphamMau.Rows[0].Cells.Count;
                GVAnphamMau.Rows[0].Cells.Clear();
                GVAnphamMau.Rows[0].Cells.Add(new TableCell());
                GVAnphamMau.Rows[0].Cells[0].ColumnSpan = columncount;
                GVAnphamMau.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVAnphamMau.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }
        protected string TenAnpham(string _maAnpham)
        {
            string strReturn = "";
            if (!String.IsNullOrEmpty(_maAnpham) && Convert.ToInt32(_maAnpham) > 0)
            {
                T_AnPham _anpham;
                AnPhamDAL _anphamDAL = new AnPhamDAL();
                _anpham = _anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham));
                strReturn = _anpham.Ten_AnPham;
            }
            else
                strReturn = "";
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

        #endregion

        #region Click

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindList_AnphamMau();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GVAnphamMau.ShowFooter = true;
            BindList_AnphamMau();
        }
        #endregion

        #region Gridview
        protected void GVAnphamMau_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVAnphamMau.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlAnPham = (DropDownList)e.Row.FindControl("ddl_AnPham");
                UltilFunc.BindCombox(ddlAnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham");
                Label lblAnpham = (Label)e.Row.FindControl("lblAnpham");
                if (Convert.ToInt32(lblAnpham.Text.Trim()) > 0)
                    ddlAnPham.Items.FindByValue(lblAnpham.Text).Selected = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlAnPham = (DropDownList)e.Row.FindControl("ddl_AnPham");
                UltilFunc.BindCombox(ddlAnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham");
            }
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
            GVAnphamMau.ShowFooter = true;
            BindList_AnphamMau();

        }
        protected void GVAnphamMau_OnRowCommand1(object sender, GridViewCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            if (e.CommandName.Equals("Mangxec"))
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                EID = (int)GVAnphamMau.DataKeys[row.RowIndex].Values[0];
                if (EID != 0)
                {
                    LoadDataApprovied();
                    PopupMangXec.Show();
                }
                
            }
            if (e.CommandName.Equals("AddNew"))
            {

                TextBox _mota = (TextBox)GVAnphamMau.FooterRow.FindControl("txt_mota");
                DropDownList _ddlAnpham = (DropDownList)GVAnphamMau.FooterRow.FindControl("ddl_AnPham");
                int _return;
                AnPhamMauDAL _apDAL = new AnPhamMauDAL();
                _ap = new T_AnPhamMau();
                _ap.MA_Mau = 0;
                _ap.Mota = _mota.Text.Trim();

                if (Convert.ToInt32(_ddlAnpham.SelectedValue.ToString()) > 0)
                    _ap.Ma_Anpham = Convert.ToInt32(_ddlAnpham.SelectedValue.ToString());
                else
                    _ap.Ma_Anpham = 0;

                _return = _apDAL.InsertT_Anphammau(_ap);
                action.Thaotac = "[Thêm mới ấn phẩm mẫu]-->[Mã ấn phẩm mẫu:" + _return.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindList_AnphamMau();
            }
        }

        protected void GVAnphamMau_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            int _id = Convert.ToInt32(GVAnphamMau.DataKeys[e.RowIndex].Value);
            AnPhamMauDAL _apDAL = new AnPhamMauDAL();
            _apDAL.DeleteOneFromT_Anphammau(_id);

            action.Thaotac = "[Xóa ấn phẩm mẫu]-->[Mã ấn phẩm mẫu:" + _id.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            BindList_AnphamMau();

        }
        protected void GVAnphamMau_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVAnphamMau.EditIndex = e.NewEditIndex;
            BindList_AnphamMau();
        }
        protected void GVAnphamMau_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
            int _id = Convert.ToInt32(GVAnphamMau.DataKeys[e.RowIndex].Value.ToString());

            TextBox _mota = (TextBox)GVAnphamMau.Rows[e.RowIndex].FindControl("txt_mota");
            DropDownList _ddlAnpham = (DropDownList)GVAnphamMau.Rows[e.RowIndex].FindControl("ddl_AnPham");

            AnPhamMauDAL _apDAL = new AnPhamMauDAL();
            _ap = new T_AnPhamMau();
            _ap.MA_Mau = _id;
            _ap.Mota = _mota.Text.Trim();

            if (Convert.ToInt32(_ddlAnpham.SelectedValue.ToString()) > 0)
                _ap.Ma_Anpham = Convert.ToInt32(_ddlAnpham.SelectedValue.ToString());
            else
                _ap.Ma_Anpham = 0;

            _return = _apDAL.InsertT_Anphammau(_ap);

            action.Thaotac = "[Sửa ấn phẩm mẫu]-->[Mã mẫu:" + _return.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            GVAnphamMau.EditIndex = -1;
            BindList_AnphamMau();
        }
        protected void GVAnphamMau_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVAnphamMau.EditIndex = -1;
            BindList_AnphamMau();

        }


        #endregion

        #region Layout Popup
        protected void Layout(object sender, EventArgs e)
        {
            using (GridViewRow _row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {

                EID = Convert.ToInt32(GVAnphamMau.DataKeys[_row.RowIndex].Value);
                lblMess.Text = "";
                Label _lblMota = (Label)GVAnphamMau.Rows[_row.RowIndex].FindControl("lblMota");
                if (_lblMota != null)
                    lbl_Mau.Text = _lblMota.Text;
                else
                    lbl_Mau.Text = "";
                Label _lblAnpham = (Label)GVAnphamMau.Rows[_row.RowIndex].FindControl("lblAnpham");
                if (!String.IsNullOrEmpty(_lblAnpham.Text))
                    lbl_TenAnPham.Text = _lblAnpham.Text;
                else
                    lbl_TenAnPham.Text = "";
                Bind_AnphamMau_Layout();
                lbl_Message.Text = "";
                popup.Show();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            EID = -1;
            BindList_AnphamMau();
        }

        public void Bind_AnphamMau_Layout()
        {
            string where = " Ma_Mau = " + EID.ToString();

            AnPhamMau_LayoutDAL _DAL = new AnPhamMau_LayoutDAL();
            DataSet _ds;
            Pager1.PageSize = 10;
            _ds = _DAL.BindGridT_AnPhamLayout(Pager1.PageIndex, Pager1.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_AnPhamLayout(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                GVAnPhamLayout.DataSource = _ds;
                GVAnPhamLayout.DataBind();
                GVAnPhamLayout.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVAnPhamLayout.DataSource = _ds;
                GVAnPhamLayout.DataBind();
                int columncount = GVAnphamMau.Rows[0].Cells.Count;
                GVAnPhamLayout.Rows[0].Cells.Clear();
                GVAnPhamLayout.Rows[0].Cells.Add(new TableCell());
                GVAnPhamLayout.Rows[0].Cells[0].ColumnSpan = columncount;
                GVAnPhamLayout.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVAnPhamLayout.ShowFooter = false;
            }
            Pager1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = Pager1.CalculateTotalPages();
            CurrentPage1.PageIndex = Pager1.PageIndex;
        }
        protected void pages1_IndexChanged(object sender, EventArgs e)
        {
            Bind_AnphamMau_Layout();
        }
        protected void GVAnPhamLayout_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVAnPhamLayout.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddl_Layout = (DropDownList)e.Row.FindControl("ddl_Layout");
                UltilFunc.BindCombox(ddl_Layout, "Ma_Layout", "Mota", "T_Layout");
                Label lblLayout = (Label)e.Row.FindControl("lblLayout");
                if (Convert.ToInt32(lblLayout.Text.Trim()) > 0)
                    ddl_Layout.Items.FindByValue(lblLayout.Text).Selected = true;
                //GridViewRow gvRow = e.Row;
                //TextBox _trang = (TextBox)e.Row.FindControl("txt_Trang");
                //RequiredFieldValidator objReq = new RequiredFieldValidator();

                //objReq.ControlToValidate = "txt_Trang";

                //objReq.ErrorMessage = "Enter Value";
                //gvRow.Cells[1].Controls.Add(objReq);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddl_Layout = (DropDownList)e.Row.FindControl("ddl_Layout");
                UltilFunc.BindCombox(ddl_Layout, "Ma_Layout", "Mota", "T_Layout");
            }
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
        protected void btnAddPopUp_Click(object sender, EventArgs e)
        {
            GVAnPhamLayout.ShowFooter = true;
            Bind_AnphamMau_Layout();
        }
        protected void GVAnPhamLayout_OnRowCommand(object sender, GridViewCommandEventArgs e)
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

                TextBox _trang = (TextBox)GVAnPhamLayout.FooterRow.FindControl("txt_Trang");
                DropDownList _ddl_Layout = (DropDownList)GVAnPhamLayout.FooterRow.FindControl("ddl_Layout");
                int _return;
                AnPhamMau_LayoutDAL _apDAL = new AnPhamMau_LayoutDAL();
                T_AnPhamMau_Layout _obj = new T_AnPhamMau_Layout();
                _obj.ID = 0;
                _obj.Ma_Mau = EID;
                if ((!String.IsNullOrEmpty(_trang.Text.Trim())) && (Convert.ToInt32(_ddl_Layout.SelectedValue.ToString()) > 0))
                {
                    _obj.Trang = Convert.ToInt32(_trang.Text.Trim());
                    _obj.Ma_layout = Convert.ToInt32(_ddl_Layout.SelectedValue.ToString());
                    _return = _apDAL.InsertT_Anphammau_Layout(_obj);
                    action.Thaotac = "[Thêm mới ấn phẩm mẫu - layout]-->[Mã ấn phẩm mẫu - layout:" + _return.ToString() + " ]";
                    actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                }
                Bind_AnphamMau_Layout();
            }

        }

        protected void GVAnPhamLayout_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVAnPhamLayout.EditIndex = e.NewEditIndex;
            Bind_AnphamMau_Layout();
        }
        protected void GVAnPhamLayout_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
            int _id = Convert.ToInt32(GVAnPhamLayout.DataKeys[e.RowIndex].Value.ToString());

            //TextBox _trang = (TextBox)GVAnPhamLayout.Rows[e.RowIndex].FindControl("txt_Trang");
            Label _trang = (Label)GVAnPhamLayout.Rows[e.RowIndex].FindControl("lblMota");
            DropDownList _ddlLayout = (DropDownList)GVAnPhamLayout.Rows[e.RowIndex].FindControl("ddl_Layout");

            AnPhamMau_LayoutDAL _apDAL = new AnPhamMau_LayoutDAL();
            T_AnPhamMau_Layout _ap = new T_AnPhamMau_Layout();
            _ap.ID = _id;
            _ap.Ma_Mau = EID;

            if (Convert.ToInt32(_ddlLayout.SelectedValue.ToString()) > 0)
                _ap.Ma_layout = Convert.ToInt32(_ddlLayout.SelectedValue.ToString());
            else
                _ap.Ma_layout = 0;
            if (!String.IsNullOrEmpty(_trang.Text.Trim()))
                _ap.Trang = Convert.ToInt32(_trang.Text.Trim());
            _return = _apDAL.InsertT_Anphammau_Layout(_ap);

            action.Thaotac = "[Sửa ấn phẩm mẫu - layout]-->[Mã ấn phẩm mẫu - layout:" + _return.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            GVAnPhamLayout.EditIndex = -1;
            Bind_AnphamMau_Layout();

        }
        protected void GVAnPhamLayout_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVAnPhamLayout.EditIndex = -1;
            Bind_AnphamMau_Layout();

        }
        #endregion

        #region Popup Mang xec

        public string GetURLMangXec(Object ID)
        {
            UltilFunc ulti = new UltilFunc();
            string str = "";

            if (ID != DBNull.Value)
            {
                string sql = "select Path_Logo from t_logo where Ma_logo=" + ID.ToString();
                DataTable dt = ulti.ExecSqlDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                    str = dt.Rows[0][0].ToString();
                else
                    str = "";
            }
            else
                str = "";
            return str;
        }
        public void LoadDataApprovied()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txt_tenanh.Text.Trim()))
                where += " AND " + string.Format(" Ten_Logo like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tenanh.Text.Trim()));
            where += " ORDER BY Ma_Logo DESC ";
            HPCBusinessLogic.DAL.T_LogoDAL _DAL = new HPCBusinessLogic.DAL.T_LogoDAL();
            DataSet _ds;

            pageappro.PageSize = 5;
            _ds = _DAL.BindGridT_Logo(pageappro.PageIndex, pageappro.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Logo(pageappro.PageIndex - 1, pageappro.PageSize, where);
            dgrListAppro.DataSource = _ds;
            dgrListAppro.DataBind();
            pageappro.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = pageappro.CalculateTotalPages();
            CurrentPage1.PageIndex = pageappro.PageIndex;
            Session["CurrentPage"] = pageappro.PageIndex;
        }
        protected void pageappro_IndexChanged(object sender, EventArgs e)
        {
            LoadDataApprovied();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pageappro.PageIndex = 0;
            LoadDataApprovied();
        }

        public void dgrListAppro_OnEditCommand(object source, DataGridCommandEventArgs e)
        {
            UltilFunc ulti = new UltilFunc();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int ID = Convert.ToInt32(this.dgrListAppro.DataKeys[e.Item.ItemIndex].ToString());
                string sqlupdate = "update T_AnPhamMau set MangXec_ID="+ID+" where MA_Mau=" + EID;
                ulti.ExecSql(sqlupdate);
                Response.Redirect("~/Danhmuc/ListAnPhamMau.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
                PopupMangXec.Hide();
            }
        }
        #endregion
    }
}
