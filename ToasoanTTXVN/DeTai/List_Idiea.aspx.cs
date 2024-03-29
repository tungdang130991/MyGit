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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.DeTai
{
    public partial class List_Idiea : System.Web.UI.Page
    {
        #region Variable Member

        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Lbt_Delete.Attributes.Add("onclick", "return CheckConfirmDelete();");
            this.LinkButton4.Attributes.Add("onclick", "return CheckConfirmDeletereturn();");
            this.LinkButton3.Attributes.Add("onclick", "return CheckConfirmGuiDuyetreturn();");
            this.Lbt_Send_Duyet.Attributes.Add("onclick", "return CheckConfirmGuiDuyet();");
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        int tab_id = 0;
                        if (Request["Tab"] != null)
                        {
                            tab_id = Convert.ToInt32(Request["Tab"].ToString());

                        }
                        if (tab_id == -1)
                        {
                            this.TabContainer1.ActiveTabIndex = 0;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        else
                        {
                            this.TabContainer1.ActiveTabIndex = tab_id;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        LoadCombox();
                    }
                }
            }
        }
        private void LoadCombox()
        {
            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", " Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " and ID in (select Ma_Ngonngu from T_Nguoidung_NgonNgu where Ma_Nguoidung=" + _user.UserID + ")", "---Tất cả---");
            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
        }
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeTai/Edit_Idiea.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        protected void DeleteCongViec_Click(object sender, EventArgs e)
        {
            DelRecordsCongViec();
        }
        protected void Send_Duyet(object sender, EventArgs e)
        {
            Gui_Duyet();
        }

        private void Gui_Duyet()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            string ActionsCode = string.Empty;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
            { LoadData_Bitralai(); }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_CongViec();
            }
            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());
                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 62, _user.UserID, DateTime.Now, 0);

                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 1, 62, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý PV:]-->[Gửi Duyêt đề tài (TBT)][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);

            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();

            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_CongViec();
            }
            // DANG XU LY
            DataSet _dsDangXuLy;
            _dsDangXuLy = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            // TRA LAI
            DataSet _dsReturn;
            DataSet _dsDaxuly;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(13, sOrder));
            _dsDaxuly = _T_IdieaDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsDangXuLy.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsDangXuLy.Clear();
            _dsDaxuly.Clear();
            //END
        }
        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData_DangXuly();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                pages2.PageIndex = 0;
                this.LoadData_Bitralai();
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                Pager4.PageIndex = 0;
                this.LoadData_CongViec();
            }
        }
        private void DelRecordsCheckedBox()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();

            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_CongViec();
            }
            string ActionsCode = string.Empty;
            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());
                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 55, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 1, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý PV:]-->[Xóa Tin][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_CongViec();
            }
            // DANG XU LY
            DataSet _dsDangXuLy;
            _dsDangXuLy = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            // TRA LAI
            DataSet _dsReturn;
            DataSet _dsDaxuly;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(13, sOrder));
            _dsDaxuly = _T_IdieaDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsDangXuLy.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsDangXuLy.Clear();
            _dsDaxuly.Clear();
            //END
        }

        private void DelRecordsCongViec()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            string ActionsCode = string.Empty;
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_ListCongViec.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_ListCongViec.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());
                _T_IdieaDAL.Update_Status_Detai(Diea_ID, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đã xử lý PV:]-->[Xóa Tin][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_CongViec();
            }
            // DANG XU LY
            DataSet _dsDangXuLy;
            _dsDangXuLy = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            // TRA LAI
            DataSet _dsReturn;
            DataSet _dsDaxuly;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(13, sOrder));
            _dsDaxuly = _T_IdieaDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsDangXuLy.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsDangXuLy.Clear();
            _dsDaxuly.Clear();
            //END
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
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString();

                Response.Redirect("Edit_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }

        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();

                Response.Redirect("Edit_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgr_ListCongViec_EditCommand(object source, DataGridCommandEventArgs e)
        {

        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgr_ListCongViec_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData_DangXuly();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_Bitralai();

            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                this.LoadData_CongViec();

            }

        }
        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }
        public void pages_IndexChanged_baitralai(object sender, EventArgs e)
        {
            LoadData_Bitralai();
        }
        public void pages_IndexChanged_Congviec(object sender, EventArgs e)
        {
            LoadData_CongViec();
        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(12, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            DataSet _dsDaxuly;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(13, sOrder));
            this.TabPanel1.HeaderText = "Đề tài trả lại (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsDaxuly = _T_IdieaDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            this.TabPanel2.HeaderText = "Đề tài đã xử lý (" + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + TotalRecords + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsDaxuly.Clear();
            #endregion
            CurrentPage1.TotalPages = pages.CalculateTotalPages();
            CurrentPage1.PageIndex = pages.PageIndex;
        }
        private void LoadData_Bitralai()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages2.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(13, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(13, sOrder));
            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind();
            pages2.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            TabPanel1.HeaderText = "Đề tài trả lại (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            DataSet _dsDaxuly;
            _dsDaxuly = _T_IdieaDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            this.TabPanel2.HeaderText = "Đề tài đã xử lý (" + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + TotalRecords + "," + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsDaxuly.Clear();
            #endregion
            CurrentPage2.TotalPages = pages2.CalculateTotalPages();
            CurrentPage2.PageIndex = pages2.PageIndex;
        }
        private void LoadData_CongViec()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager4.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex, Pager4.PageSize, BuildSQL(1, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);

            if (TotalRecord == 0)
                _ds = _T_newsDAL.Bin_T_IdieaVersionDynamic(Pager4.PageIndex - 1, Pager4.PageSize, BuildSQL(1, sOrder));
            dgr_ListCongViec.DataSource = _ds;
            dgr_ListCongViec.DataBind();
            Pager4.TotalRecords = CurrentPage4.TotalRecords = TotalRecords;

            #region DETAI DA XU LY
            this.TabPanel2.HeaderText = "Đề tài đã xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_newsDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(12, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            DataSet _dsDaxuly;
            _dsDaxuly = _T_newsDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(13, sOrder));
            this.TabPanel1.HeaderText = "Đề tài trả lại (" + _dsDaxuly.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + TotalRecords + ");", true);
            _dsReturn.Clear();
            _dsDaxuly.Clear();
            #endregion
            CurrentPage4.TotalPages = Pager4.CalculateTotalPages();
            CurrentPage4.PageIndex = Pager4.PageIndex;
        }

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 3)

                    return " Date_Duyet DESC";
                else
                    return " Date_Created DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sql = "";
            string sClause = " 1=1 AND User_Created =" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND Status = " + status + " and CAT_ID in ( SELECT tc.Ma_Chuyenmuc FROM T_Chuyenmuc tc )AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE Ma_Nguoidung= " + _user.UserID + ")";
            string sWhere = "";

            if (txt_tieude.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Title LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  CAT_ID=" + cbo_chuyenmuc.SelectedValue.ToString();
            }

            sql += sClause;
            if (sWhere.Trim().Length > 0)
                sql += " AND" + sWhere;

            return sql + sOrder;
        }
        public string ActionID(Object _ID)
        {
            string _return = "";
            int _idIdiead = Convert.ToInt32(_ID);
            try
            {
                string sql = "select top 1 T_IdieaVersion.Action from  T_IdieaVersion where 1=1 AND Diea_ID =" + _idIdiead + " AND User_Created =" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND CAT_ID in ( SELECT tc.Categorys_ID FROM T_Categorys tc WHERE tc.IsCateOrSubject = 1 )AND Lang_ID IN (SELECT T_UserLanguages.Languages_ID FROM T_UserLanguages WHERE T_UserLanguages.[User_ID] = " + _user.UserID + ") Order by Date_Duyet DESC";
                DataSet ds = HPCShareDLL.HPCDataProvider.Instance().ExecSqlDataSet(sql);
                _return = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                _return = "0";
            }
            return _return;
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
    }
}
