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
    public partial class List_DuyetDeTaiTPPV : System.Web.UI.Page
    {
        #region Variable Member

        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    ActiverPermission();
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

        #region Methods
        private void LoadCombox()
        {
            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", " Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " and ID in (select Ma_Ngonngu from T_Nguoidung_NgonNgu where Ma_Nguoidung=" + _user.UserID + ")", "---Tất cả---");

            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
        }
        protected void ActiverPermission()
        {
            this.LinkButton9.Attributes.Add("onclick", "return CheckConfirmDelete1();");

            this.LinkButton4.Attributes.Add("onclick", "return CheckConfirmDeletereturn();");
            this.LinkButton3.Attributes.Add("onclick", "return CheckConfirmGuiDuyetreturn();");

            this.LinkButton5.Attributes.Add("onclick", "return CheckConfirmTraLaireturn();");
            this.LinkButton7.Attributes.Add("onclick", "return CheckConfirmGuiDuyets();");
            this.LinkButton8.Attributes.Add("onclick", "return CheckConfirmGuiTra();");
        }
        private void Load_DaPhanViec()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            Pager3.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex, Pager3.PageSize, BuildSQL(26, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(26, sOrder));
            dgr_tintuc3.DataSource = _ds;
            dgr_tintuc3.DataBind();
            Pager3.TotalRecords = CurrentPage3.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            TabPanel22.HeaderText = "Đề tài bị trả lại (" + TotalRecords + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaPhanCong(" + TotalRecords + ");", true);
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
            this.TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(22, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài chưa phân công công việc (" + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn2.Clear();
            _dsReturn3 = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder));
            this.TabPanel2.HeaderText = "Danh sách công việc (" + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn3.Clear();
            #endregion

            CurrentPage3.TotalPages = Pager3.CalculateTotalPages();
            CurrentPage3.PageIndex = Pager3.PageIndex;
        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(22, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(22, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            #region DETAI BI TRA LAI
            tabpnltinXuly.HeaderText = "Đề tài chưa phân công công việc (" + TotalRecords + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiChuaPhanCong(" + TotalRecords + ");", true);
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
            this.TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(26, sOrder));
            this.TabPanel22.HeaderText = "Đề tài bị trả lại (" + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn2.Clear();
            _dsReturn3 = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder));
            this.TabPanel2.HeaderText = "Danh sách công việc (" + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn3.Clear();
            #endregion
            CurrentPage1.TotalPages = pages.CalculateTotalPages();
            CurrentPage1.PageIndex = pages.PageIndex;
        }
        private void LoadData_Bitralai()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            pages2.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(23, sOrder));
            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind();
            pages2.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + TotalRecords + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaHoanThanh(" + TotalRecords + ");", true);
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(26, sOrder));
            this.TabPanel22.HeaderText = "Đề tài bị trả lại (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(22, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài chưa phân công công việc (" + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn2.Clear();
            _dsReturn3 = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder));
            this.TabPanel2.HeaderText = "Danh sách công việc (" + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn3.Clear();
            #endregion

            CurrentPage2.TotalPages = pages2.CalculateTotalPages();
            CurrentPage2.PageIndex = pages2.PageIndex;
        }
        private void LoadData_CongViec()
        {
            string sOrder = GetOrderString1() == "" ? "" : " ORDER BY " + GetOrderString1();

            Pager4.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex - 1, Pager4.PageSize, BuildSQLCongViec(sOrder));
            dgr_tintuc4.DataSource = _ds;
            dgr_tintuc4.DataBind();
            Pager4.TotalRecords = CurrentPage4.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            TabPanel2.HeaderText = "Danh sách công việc (" + TotalRecords + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerDanhSach(" + TotalRecords + ");", true);
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(26, sOrder));
            this.TabPanel22.HeaderText = "Đề tài bị trả lại (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(22, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài chưa phân công công việc (" + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn2.Clear();
            _dsReturn3 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(23, sOrder));
            this.TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn3.Clear();
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
                    return " Date_Edit DESC";
            }
        }
        private string GetOrderString1()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 4)

                    return " Date_Created DESC";
                else
                    return " Date_Duyet DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sql = "";

            string sClause = " 1=1 AND Status = " + status + " and CAT_ID in (select Ma_Chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") AND Lang_ID IN (SELECT Ma_NgonNgu FROM T_Nguoidung_NgonNgu WHERE Ma_Nguoidung = " + _user.UserID + ")";

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
        string BuildSQLCongViec(string sOrder)
        {
            string sql = "";

            string sClause = " 1=1 AND Status!=55 And Numbers =0 AND CAT_ID in (select Ma_Chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") AND Lang_ID IN (SELECT Ma_NgonNgu FROM T_Nguoidung_NgonNgu WHERE Ma_Nguoidung = " + _user.UserID + ")";

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

        protected bool IsEnable(string prmNews_Lock, string prmEditorID)
        {
            bool _isEnabled = true;
            int _prmEditorID = Convert.ToInt32(prmEditorID);
            if (prmNews_Lock == "True" && _prmEditorID != _user.UserID)
                _isEnabled = false;
            else
                _isEnabled = true;
            return _isEnabled;
        }
        protected string LockedUser(string prm_news_Lock, string prmNewsEditorID)
        {
            string _userLock = "";
            int _prmEditorID = Convert.ToInt32(prmNewsEditorID);

            if (prm_news_Lock == "True" && _prmEditorID != _user.UserID)
                _userLock = "<b> &nbsp;&nbsp;[ <font color='red'> Người locked: " + UltilFunc.GetUserFullName(prmNewsEditorID) + "</font> ]</b>";

            return _userLock;
        }
        protected string IsImageLock_SendBack(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/images/unlock.jpg";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/images/lock.jpg";
            return strReturn;
        }
        #endregion

        #region Event Click
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DeTai/Edit_DuyetDeTaiTPPV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        protected void Delete_Click1(object sender, EventArgs e)
        {

            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();

            string sOrder = GetOrderString1() == "" ? "" : " ORDER BY " + GetOrderString1();
            ArrayList ar = new ArrayList();
            foreach (DataGridItem m_Item in dgr_tintuc4.Items)
            {
                CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_select != null && chk_select.Checked)
                {
                    ar.Add(double.Parse(dgr_tintuc4.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            LoadData_CongViec();
            for (int i = 0; i < ar.Count; i++)
            {
                double _ID = double.Parse(ar[i].ToString());
                if (_DAL.GetOneFromT_AllotmentByID(int.Parse(_ID.ToString())).Status == 32)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài chưa hoàn thành không thể xóa.!');", true);
                    return;
                }
                _DAL.Update_Status_tintuc(_ID, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách CV TPPV:]-->[Xóa CV][T_Allotment_ID:" + _ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            LoadData_CongViec();
            DataSet _dsReturn;
            _dsReturn = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerDanhSach(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
        }
        protected void Send_Duyet(object sender, EventArgs e)
        {
            Gui_Duyet();
        }
        protected void Send_DuyetBT(object sender, EventArgs e)
        {
            Gui_DuyetBT();
        }
        protected void Send_TraLai(object sender, EventArgs e)
        {
            Gui_TraLai();
        }
        protected void XoaTralai_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        private void Gui_Duyet()
        {

            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();

            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
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

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                if (_T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).Diea_Lock == true && _T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                _T_IdieaDAL.IsLock(Diea_ID, 0, _user.UserID, DateTime.Now);
                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 62, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 2, 62, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TPPV:]-->[Gửi Duyêt(TPBT)][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(22, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiChuaPhanCong(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaHoanThanh(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
        }
        private void Gui_DuyetBT()
        {

            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
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
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_tintuc3.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc3.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
                Load_DaPhanViec();

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                if (_T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).Diea_Lock == true && _T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                _T_IdieaDAL.IsLock(Diea_ID, 0, _user.UserID, DateTime.Now);
                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 52, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 2, 52, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TPPV:]-->[Gửi Duyêt(TBT)][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(22, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiChuaPhanCong(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaHoanThanh(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                Load_DaPhanViec();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex, Pager3.PageSize, BuildSQL(26, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaPhanCong(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
        }
        private void Gui_TraLai()
        {

            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            ArrayList ar = new ArrayList();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
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
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_tintuc3.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc3.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
                Load_DaPhanViec();

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                if (_T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).Diea_Lock == true && _T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString())).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                _T_IdieaDAL.IsLock(Diea_ID, 0, _user.UserID, DateTime.Now);
                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 33, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 2, 33, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TPPV:]-->[Trả Lại(PV)][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(22, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiChuaPhanCong(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaHoanThanh(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                Load_DaPhanViec();
                DataSet _dsReturn;
                _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex, Pager3.PageSize, BuildSQL(26, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDaPhanCong(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
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
                Pager3.PageIndex = 0;
                this.Load_DaPhanViec();
            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                Pager4.PageIndex = 0;
                this.LoadData_CongViec();
            }

        }
        private void DelRecordsCheckedBox()
        {

            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            HPCBusinessLogic.DAL.T_AllotmentDAL _DAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            string sOrder1 = GetOrderString1() == "" ? "" : " ORDER BY " + GetOrderString1();
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
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_tintuc3.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc3.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in dgr_tintuc4.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc4.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
                Load_DaPhanViec();
            else if (TabContainer1.ActiveTabIndex == 3)
                LoadData_CongViec();
            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 55, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 2, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TPPV:]-->[Xóa Tin][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
                Load_DaPhanViec();
            else if (TabContainer1.ActiveTabIndex == 3)
                LoadData_CongViec();
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            DataSet _dsReturn4;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(22, sOrder));
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(23, sOrder));
            _dsReturn3 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(26, sOrder));
            _dsReturn4 = _DAL.BindGridT_AllotmentEditor(Pager4.PageIndex, Pager4.PageSize, BuildSQLCongViec(sOrder1));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerDuyetDetaiTPPV(" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn4.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn1.Clear();
            _dsReturn2.Clear();
            _dsReturn3.Clear();
            _dsReturn4.Clear();
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

                if (Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).Diea_Lock == true && Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                //Dal.Update_Status_tintuc(double.Parse(_ID), 22, _user.UserID, DateTime.Now);
                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock
                Response.Redirect("Edit_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            //LoadData_DangXuly();
        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();

                if (Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).Diea_Lock == true && Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                //Dal.Update_Status_tintuc(double.Parse(_ID), 23, _user.UserID, DateTime.Now);
                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock

                Response.Redirect("Edit_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgData_EditCommand3(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                T_Idiea obj = new T_Idiea();
                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc3.DataKeys[e.Item.ItemIndex].ToString();

                if (Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).Diea_Lock == true && Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }
                int CV_ID = Dal.GetOneFromT_IdieaByID(Convert.ToInt32(_ID.ToString())).CV_id;

                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock
                Response.Redirect("Edit_TraLaiPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&CV=" + CV_ID + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgData_EditCommand4(object source, DataGridCommandEventArgs e)
        {

        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }

        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }

        }
        protected void dgData_ItemDataBound3(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
        }
        protected void dgData_ItemDataBound4(object sender, DataGridItemEventArgs e)
        {

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
                this.Load_DaPhanViec();
            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                this.LoadData_CongViec();
            }
        }
        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }
        public void pages_IndexChanged_baidaxuly(object sender, EventArgs e)
        {
            //LoadData_Baidaxuly();
        }
        public void pages_IndexChanged_baitralai(object sender, EventArgs e)
        {
            LoadData_Bitralai();
        }
        public void pages_IndexChanged_baidangdaPCCV(object sender, EventArgs e)
        {
            Load_DaPhanViec();
        }
        public void pages_IndexChanged_Congviec(object sender, EventArgs e)
        {
            LoadData_CongViec();
        }
        
        #endregion



    }
}
