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
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;


namespace ToasoanTTXVN.DeTai
{
    public partial class List_DuyetDeTaiTBT : System.Web.UI.Page
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
        HPCBusinessLogic.DAL.TinBaiDAL _DalNewBaoIn = new HPCBusinessLogic.DAL.TinBaiDAL();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Lbt_Delete.Attributes.Add("onclick", "return CheckConfirmDelete();");
            this.lbDelete.Attributes.Add("onclick", "return CheckConfirmDeleteDXL();");
            this.lbDuyet.Attributes.Add("onclick", "return CheckConfirmGuiTKTS();");
            this.Lbt_Send_Duyet.Attributes.Add("onclick", "return CheckConfirmGuiDuyet();");
            this.Lbt_TraLai.Attributes.Add("onclick", "return CheckConfirmTralai();");
            this.lbTralai.Attributes.Add("onclick", "return CheckConfirmTralaireturn();");
            this.lbDelet.Attributes.Add("onclick", "return CheckConfirmDeletereturn();");
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
                        LoadCombox();
                        if (Request["Tab"] != null)
                        {
                            tab_id = Convert.ToInt32(Request["Tab"].ToString());
                        }
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);
                    }
                }
            }
        }

        #region Methods
        private void LoadCombox()
        {
            UltilFunc.BindCombox(ddlLang, "id", "TenNgonNgu", "T_NgonNgu", string.Format(" Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " AND ID IN ({0}) Order by TenNgonNGu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");

            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Chọn chuyên mục---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        private T_News SetItem(double id)
        {
            T_News obj_news = new T_News();
            T_Idiea _objIdiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL _IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            _objIdiea = _IdieaDAL.GetOneFromT_IdieaByID(int.Parse(id.ToString()));

            obj_news.News_Tittle = _objIdiea.Title;
            obj_news.CAT_ID = _objIdiea.Cat_ID;
            obj_news.Lang_ID = _objIdiea.Lang_ID;
            obj_news.News_Body = _objIdiea.Diea_Articles;
            obj_news.News_PublishNumber = int.Parse(DateTime.Now.Month.ToString());
            obj_news.News_PublishYear = int.Parse(DateTime.Now.Year.ToString());
            obj_news.News_DateCreated = DateTime.Now;
            obj_news.News_DateEdit = DateTime.Now;
            obj_news.News_DatePublished = DateTime.Now;
            obj_news.News_DateApproved = DateTime.Now;
            obj_news.News_AuthorID = _objIdiea.User_Created;
            obj_news.News_AprovedID = _user.UserID;
            obj_news.News_EditorID = _user.UserID;
            obj_news.News_PublishedID = _user.UserID;
            obj_news.News_CopyFrom = 0;
            obj_news.News_Status = 22;

            return obj_news;
        }

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)

                    return " Date_Duyet DESC";
                else
                    return " Date_Edit DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sql = "";
            string sClause = " 1=1 AND Status = " + status + " and CAT_ID in ( SELECT Ma_chuyenmuc FROM T_Nguoidung_Chuyenmuc WHERE Ma_Nguoidung = " + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + ")AND Lang_ID IN (SELECT Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE Ma_Nguoidung = " + _user.UserID + ")";

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
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(62, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(62, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(64, sOrder));
            this.TabPanel2.HeaderText = "Đề tài đã xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            DataSet _dsReturn1;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            this.TabPanel1.HeaderText = "Bài chờ duyệt (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            CurrentPage1.TotalPages = pages.CalculateTotalPages();
            CurrentPage1.PageIndex = pages.PageIndex;
        }
        private void LoadData_Bitralai()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            Pager3.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex, Pager3.PageSize, BuildSQL(64, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(64, sOrder));
            dgDXL.DataSource = _ds;
            dgDXL.DataBind();
            Pager3.TotalRecords = CurrentPage3.TotalRecords = TotalRecords;

            TabPanel2.HeaderText = "Đề tài đã xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(62, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            DataSet _dsReturn1;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            this.TabPanel1.HeaderText = "Bài chờ duyệt (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            CurrentPage3.TotalPages = Pager3.CalculateTotalPages();
            CurrentPage3.PageIndex = Pager3.PageIndex;
        }
        private void LoadData_DetaiChoDuyet()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            pages2.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex, pages2.PageSize, BuildSQL(63, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind();
            pages2.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;

            TabPanel1.HeaderText = "Bài chờ duyệt (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(62, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            DataSet _dsReturn1;
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(64, sOrder));
            this.TabPanel2.HeaderText = "Đề tài đã xử lý (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn1.Clear();
            CurrentPage2.TotalPages = pages2.CalculateTotalPages();
            CurrentPage2.PageIndex = pages2.PageIndex;

        }
        private void Gui_Duyet()
        {


            ArrayList ar = new ArrayList();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
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
            if (TabContainer1.ActiveTabIndex == 1)
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
            if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgDXL.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgDXL.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());
                T_Idiea obj_Idiea = new T_Idiea();
                obj_Idiea = _T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString()));
                if (obj_Idiea.Diea_Lock == true && obj_Idiea.User_Edit != _user.UserID)
                {
                    FuncAlert.AlertJS(this, "Bài đang có người làm việc.!");
                    return;
                }

                _T_IdieaDAL.IsLock(Diea_ID, 0, _user.UserID, DateTime.Now);
                if (obj_Idiea.Diea_Stype == 2)
                {
                    if (obj_Idiea.Number != 1)
                    {
                        _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 22, _user.UserID, DateTime.Now, 0);
                        _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 6, 22, _user.UserID, DateTime.Now);

                        ActionsCode = "[Danh sách Đề tài đang chờ xử lý TBT:]-->[Gửi TPPV][Diea_ID:" + Diea_ID + "]";
                        UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                    }
                    else
                    {
                        T_Idiea _objIdiea = new T_Idiea();
                        HPCBusinessLogic.DAL.T_IdieaDAL _IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                        HPCBusinessLogic.DAL.T_NewsDAL _T_NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                        T_News _objT_News = SetItem(Diea_ID);
                        _T_NewsDAL.InsertT_news(_objT_News);

                        ActionsCode = "[TBT chuyển bài viết đề tài]-->[TKTS][Diea_ID =" + Diea_ID + "]";
                        UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                        _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 64, _user.UserID, DateTime.Now, 1);
                    }
                }
                else
                {
                    if (obj_Idiea.Number != 1)
                    {
                        _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 52, _user.UserID, DateTime.Now, 0);
                        _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 2, 52, _user.UserID, DateTime.Now);

                        ActionsCode = "[TBT duyệt đề tài:]-->[Gửi TPBT][Diea_ID:" + Diea_ID + "]";
                        UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                    }
                    else
                    {
                        T_Idiea _objIdiea = new T_Idiea();
                        HPCBusinessLogic.DAL.T_IdieaDAL _IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                        HPCBusinessLogic.DAL.T_NewsDAL _T_NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                        T_News _objT_News = SetItem(Diea_ID);
                        _T_NewsDAL.InsertT_news(_objT_News);

                        ActionsCode = "[TBT chuyển bài viết đề tài]-->[TKTS][Diea_ID =" + Diea_ID + "]";
                        UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                        _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 64, _user.UserID, DateTime.Now, 1);
                    }


                }

            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_DetaiChoDuyet();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                LoadData_Bitralai();
            }
            DataSet _dsReturn;
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(62, sOrder));
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(64, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsReturn1.Clear();
            _dsReturn2.Clear();
        }
        private void TraLai()
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
            if (TabContainer1.ActiveTabIndex == 1)
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
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_DetaiChoDuyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadData_Bitralai();

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());
                T_Idiea _objIdiea = new T_Idiea();
                _objIdiea = _T_IdieaDAL.GetOneFromT_IdieaByID(int.Parse(Diea_ID.ToString()));

                if (_objIdiea.Diea_Lock == true && _objIdiea.User_Edit != _user.UserID)
                {
                    FuncAlert.AlertJS(this, "Bài đang có người làm việc.!");
                    return;
                }

                _T_IdieaDAL.IsLock(Diea_ID, 0, _user.UserID, DateTime.Now);

                if (_T_IdieaDAL.GetOneFromT_IdieaVersionByID(int.Parse(Diea_ID.ToString()), 5, 63) == true)
                {
                    _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 53, _user.UserID, DateTime.Now, 0);
                    _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 6, 53, _user.UserID, DateTime.Now);

                    ActionsCode = "[Danh sách Đề tài đang chờ xử lý TBT:]-->[Trả lại Dề xuất ĐT][Diea_ID:" + Diea_ID + "]";
                    UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                }
                else
                {
                    _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 13, _user.UserID, DateTime.Now, 0);
                    _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 6, 13, _user.UserID, DateTime.Now);

                    ActionsCode = "[Danh sách Đề tài đang chờ xử lý TBT:]-->[Trả lại Đề xuât ĐT][Diea_ID:" + Diea_ID + "]";
                    UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_DetaiChoDuyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadData_Bitralai();
            DataSet _dsReturn;
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(62, sOrder));
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(64, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsReturn1.Clear();
            _dsReturn2.Clear();
        }
        private void DelRecordsCheckedBox()
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
                foreach (DataGridItem m_Item in dgDXL.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgDXL.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();

            }
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_DetaiChoDuyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadData_Bitralai();

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 55, _user.UserID, DateTime.Now, 0);
                _T_IdieaDAL.Insert_Version_From_T_idiea_WithUserModify(Diea_ID, 3, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TBT:]-->[Xóa Tin][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();

            }
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_DetaiChoDuyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadData_Bitralai();
            DataSet _dsReturn;
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(62, sOrder));
            _dsReturn1 = _T_IdieaDAL.BindGridT_IdieaEditor(pages2.PageIndex - 1, pages2.PageSize, BuildSQL(63, sOrder));
            _dsReturn2 = _T_IdieaDAL.BindGridT_IdieaEditor(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(64, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsReturn1.Clear();
            _dsReturn2.Clear();
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
                _userLock = "<b> &nbsp;&nbsp;[ <font color='red'> Người biên tập: " + UltilFunc.GetUserFullName(prmNewsEditorID) + "</font> ]</b>";

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
            Response.Redirect("~/QuanLyDeTai/Edit_DuyetDeTaiTBT.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        protected void Send_Duyet(object sender, EventArgs e)
        {
            Gui_Duyet();
        }
        protected void Send_Tralai(object sender, EventArgs e)
        {
            TraLai();
        }

        protected void lbDuyet_Duyet(object sender, EventArgs e)
        {
            Gui_Duyet();
        }
        protected void lbTralai_Tralai(object sender, EventArgs e)
        {
            TraLai();
        }
        protected void lbDelet_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
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
                this.LoadData_DetaiChoDuyet();
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                Pager3.PageIndex = 0;
                this.LoadData_Bitralai();
            }
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


                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock
                Response.Redirect("Edit_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }

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


                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock
                Response.Redirect("Edit_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            //e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            //e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            //e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            //e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgDXL_EditCommand1(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgDXL.DataKeys[e.Item.ItemIndex].ToString();

                if (Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).Diea_Lock == true && Dal.GetOneFromT_IdieaByID(int.Parse(_ID)).User_Edit != _user.UserID)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc.!');", true);
                    return;
                }


                Dal.IsLock(double.Parse(_ID), 1, _user.UserID, DateTime.Now);// trang thai bai lock
                Response.Redirect("Edit_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgDXL_ItemDataBound1(object sender, DataGridItemEventArgs e)
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
                this.LoadData_DetaiChoDuyet();
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                this.LoadData_Bitralai();
            }
        }
        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }
        public void Pager3_IndexChanged_Daxuly(object sender, EventArgs e)
        {
            LoadData_Bitralai();
        }
        public void pages_IndexChanged_baitralai(object sender, EventArgs e)
        {
            LoadData_DetaiChoDuyet();
        }
        
        #endregion

        
    }
}
