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
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCApplication;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ArticleApproveList : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion

        #region Load Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        int tab_id = 0;
                        int.TryParse(Request["Tab"] == null ? "0" : Request["Tab"], out tab_id);
                        LoadCombox();
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);
                    }
                }
            }
        }
        protected void ActiverPermission()
        {
            this.linkDelete.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_linkDelete');");
            this.Linksend.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Linksend');");
            this.Linktralai.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Linktralai');");

            this.LinkPubTwo.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel1_LinkPubTwo');");
            this.LinkReturnTwo.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_TabPanel1_LinkReturnTwo');");
            this.LinkDeleteTwo.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel1_LinkDeleteTwo');");

            this.btnGuiDuyet.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_btnGuiDuyet');");
            this.btnTraLai.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_btnTraLai');");
            //this.BtnXoa.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_BtnXoa');");
        }
        public void LoadFileDoc(int _ID)
        {
            string strHTML = "";
            HPCBusinessLogic.DAL.T_NewsDAL dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News obj = dal.load_T_news(_ID);
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b>" + obj.News_Tittle + "<o:p></o:p></b></p>";
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b><br>" + obj.News_Summary + "<u1:p></u1:p></b></p>";
            strHTML += "<p style='text-align:justify'>" + obj.News_Body + "<o:p></o:p></p>";
            if (strHTML.Length > 0)
                SaveAsText(strHTML);

        }

        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
            {
                cboNgonNgu.SelectedIndex = Global.DefaultLangID;
            }
            else
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
            if (cboNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }

        private void LoadData_DangXuly()
        {
            string sOrder = " Order by News_DateSend DESC ";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, BuildSQL(ConstNews.NewsApproving_tk, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(72, sOrder));
            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - i, pages.PageSize, BuildSQL(ConstNews.NewsApproving_tk, sOrder));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind(); _ds.Clear();
            pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
            CurrentPage2.TotalPages = pages.CalculateTotalPages();
            CurrentPage2.PageIndex = pages.PageIndex;
            GetTotal();
        }
        private void LoadData_Bitralai()
        {
            string sOrder = " Order by News_DateSend DESC ";
            pages1.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages1.PageIndex, pages1.PageSize, BuildSQL(ConstNews.NewsReturn_tk, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _T_newsDAL.BindGridT_NewsEditor(pages1.PageIndex - 1, pages1.PageSize, BuildSQL(73, sOrder));

            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.BindGridT_NewsEditor(pages1.PageIndex - i, pages1.PageSize, BuildSQL(ConstNews.NewsReturn_tk, sOrder));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind(); _ds.Clear();
            pages1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = pages1.CalculateTotalPages();
            CurrentPage1.PageIndex = pages1.PageIndex;
            GetTotal();
        }

        private void LoadData_Baidaxuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager3.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(Pager3.PageIndex, Pager3.PageSize, BuildSQL(ConstNews.NewsAppro_tk, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(7, sOrder));

            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(Pager3.PageIndex - i, Pager3.PageSize, BuildSQL(ConstNews.NewsAppro_tk, sOrder));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            Dgr_Baidaxuly.DataSource = _ds;
            Dgr_Baidaxuly.DataBind(); _ds.Clear();
            Pager3.TotalRecords = CurrentPage3.TotalRecords = TotalRecords;
            CurrentPage3.TotalPages = Pager3.CalculateTotalPages();
            CurrentPage3.PageIndex = Pager3.PageIndex;
            string sOrderNews = GetOrderStringXuLy() == "" ? "" : " ORDER BY " + GetOrderStringXuLy();
            GetTotal();
        }
        private void LoadData_Baibixoa()
        {
            string sOrder = " Order by News_DateEdit DESC ";
            pageBaixoa.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex, pageBaixoa.PageSize, BuildSQL(ConstNews.NewsDelete, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex - i, pageBaixoa.PageSize, BuildSQL(ConstNews.NewsDelete, sOrder));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            dgr_BaiXoa.DataSource = _ds;
            dgr_BaiXoa.DataBind(); _ds.Clear();
            pageBaixoa.TotalRecords = CurrentPageBaixoa.TotalRecords = TotalRecords;
            CurrentPageBaixoa.TotalPages = pageBaixoa.CalculateTotalPages();
            CurrentPageBaixoa.PageIndex = pageBaixoa.PageIndex;
            GetTotal();
        }
        #region Get total record from T_News and T_NewsVersion
        public void GetTotal()
        {
            string _dsDangCho, _dsDaXuLy, _dsDatralaiXB, _dsBaiBiXoa;
            _dsDangCho = UltilFunc.GetTotalCountStatus(WhereCondition(0), "CMS_CountListT_News").ToString();
            _dsDatralaiXB = UltilFunc.GetTotalCountStatus(WhereCondition(1), "CMS_CountListT_News").ToString();
            _dsDaXuLy = UltilFunc.GetTotalCountStatus(WhereCondition(2), "CMS_CountListT_NewsVersion").ToString();
            _dsBaiBiXoa = UltilFunc.GetTotalCountStatus(WhereCondition(3), "CMS_CountListT_News").ToString();
            _dsDangCho = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinbientap") + " (" + _dsDangCho + ")";
            _dsDatralaiXB = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTintralai") + " (" + _dsDatralaiXB + ")";
            _dsDaXuLy = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindagui") + " (" + _dsDaXuLy + ")";
            _dsBaiBiXoa = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindaxoa") + " (" + _dsBaiBiXoa + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangCho + "','" + _dsDatralaiXB + "','" + _dsDaXuLy + "','" + _dsBaiBiXoa + "');", true);
        }
        #endregion
        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            if (status == 0)
                _whereNews += " AND News_Status = " + ConstNews.NewsApproving_tk + "  AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            else if (status == 1)
                _whereNews += " AND News_Status =  " + ConstNews.NewsReturn_tk + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            else if (status == 2)
                _whereNews += " AND News_Status=" + ConstNews.NewsAppro_tk + " AND CAT_ID in (select DISTINCT(Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ")";
            else if (status == 3)
            {
                _whereNews += " AND News_EditorID =" + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
                _whereNews += " AND News_Status =  " + ConstNews.NewsDelete + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            }
            if (cboNgonNgu.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Lang_ID = {0}", cboNgonNgu.SelectedValue);
            if (cbo_chuyenmuc.SelectedIndex > 0)
                _whereNews += "" + string.Format(" AND CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.cbo_chuyenmuc.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _whereNews += " AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));

            return _whereNews;
        }
        #endregion

        #region Methods
        private string BuildSQL(int status, string sOrder)
        {
            string sql = "";
            string sClause = "";
            if (TabContainer1.ActiveTabIndex != 2)
                if (status == ConstNews.NewsReturn_tk || status == ConstNews.NewsAppro_tk)
                    //sClause = " 1=1 AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") and News_Status=" + status + " and News_EditorID=" + _user.UserID + " and CAT_ID in (select Ma_chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";
                    sClause = " 1=1 and News_Status=" + status + " And CAT_ID in (select DISTINCT(Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";
                else
                    sClause = " 1=1 and News_Status=" + status + "  AND CAT_ID in (select DISTINCT(Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";
            else
                sClause = " 1=1 and News_Status=" + status + " AND CAT_ID in (select DISTINCT(Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";//" and News_EditorID=" + _user.UserID + 
            string sWhere = "";
            if (status == ConstNews.NewsDelete)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " News_EditorID =" + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
            }
            if (txt_tieude.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " News_Tittle LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "" + string.Format(" CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.cbo_chuyenmuc.SelectedValue);
            }
            if (cboNgonNgu.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            }

            sql += sClause;
            if (sWhere.Trim().Length > 0)
                sql += " AND" + sWhere;
            return sql + sOrder;
        }
        private void SaveAsText(string _arr_IN)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            DirectoryInfo r = new DirectoryInfo(HttpContext.Current.Server.MapPath(HPCComponents.Global.GetAppPath(Request)));
            FileInfo[] file;
            file = r.GetFiles("*.doc");
            foreach (FileInfo i in file)
            {
                File.Delete(r.FullName + "\\" + i.Name);
            }
            strFileName = _user.UserName + "_DXB_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            Page.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
                return ViewState["OrderString"].ToString();
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)
                    return " News_DateSend DESC";
                else
                    return " ID DESC";
            }
        }

        private string GetOrderStringXuLy()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
                return ViewState["OrderString"].ToString();
            else
                return " News_DateEdit DESC";
        }

       
        #endregion

        #region Method Events

        private void DelRecordsCheckedBox()
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                                tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                           Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Danh sách tin bài đang xử lý] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                       Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Danh sách tin bài đang xử lý] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                                tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                           Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Danh sách tin bài bị trả lại] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                       Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Danh sách tin bài bị trả lại] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_EditorID == _user.UserID)
                        {
                            double News_ID = double.Parse(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                       Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Danh sách tin bài bị trả lại] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                            return;
                        }
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 3)
                LoadData_Baibixoa();
        }

        private void Send_choduyet()
        {
            T_NewsDAL tt = new T_NewsDAL();
            T_News _obj_T_News = new T_News();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock == true)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                                ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            //Unlock before send
                            tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                            ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock == true)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                                ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!.');", true);
                                return;
                            }
                        }
                        else
                        {
                            //Unlock before send
                            tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                            ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        ar.Add(double.Parse(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                _obj_T_News = tt.load_T_news(int.Parse(News_ID.ToString()));
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    //if (_obj_T_News.Lang_ID == 1)
                    //{
                    //    tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tbt, _user.UserID, DateTime.Now);
                    //    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tbt, _user.UserID);
                    //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                    //        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Gửi Duyệt tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    //else
                    //{
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tb, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tb, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Gửi Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    LoadData_DangXuly();
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    //if (_obj_T_News.Lang_ID == 1)
                    //{
                    //    tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tbt, _user.UserID, _obj_T_News.News_DateEdit);
                    //    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tbt, _user.UserID);
                    //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                    //        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Gửi Duyệt tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    //else
                    //{
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tb, _user.UserID, _obj_T_News.News_DateEdit);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tb, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Gửi Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    
                    LoadData_Bitralai();
                }
                else if (TabContainer1.ActiveTabIndex == 3)
                {
                    //if (_obj_T_News.Lang_ID == 1)
                    //{
                    //    tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tbt, _user.UserID, DateTime.Now);
                    //    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tbt, _user.UserID);
                    //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                    //        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị xóa] [Gửi Duyệt tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    //else
                    //{
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tb, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsApproving_tb, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị xóa] [Gửi Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                    //}
                    
                    LoadData_Baibixoa();
                }


            }

        }

        private void Send_Back_HoaSi()
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                                double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                                tt.UpdateStatus_T_News_ex_New_HV(Convert.ToInt32(News_ID), ConstNews.NewsReturn, _user.UserID, DateTime.Now);
                                tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsReturn, _user.UserID);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                    Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Trả lại tin bài pv]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                            }
                        }
                        else
                        {
                            tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                            double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.UpdateStatus_T_News_ex_New_HV(Convert.ToInt32(News_ID), ConstNews.NewsReturn, _user.UserID, DateTime.Now);
                            tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsReturn, _user.UserID);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài đang chờ xử lý] [Trả lại tin bài pv]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                                double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                                tt.UpdateStatus_T_News_ex_New_HV(Convert.ToInt32(News_ID), ConstNews.NewsReturn, 0, DateTime.Now);
                                tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsReturn, _user.UserID);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Trả lại tin bài pv]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true); return;
                            }
                        }
                        else
                        {
                            tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                            double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.UpdateStatus_T_News_ex_New_HV(Convert.ToInt32(News_ID), ConstNews.NewsReturn, 0, DateTime.Now);
                            tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsReturn, _user.UserID);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị trả lại] [Trả lại tin bài pv]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        double News_ID = double.Parse(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.UpdateStatus_T_News_ex_New_HV(Convert.ToInt32(News_ID), ConstNews.NewsReturn, 0, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, ConstNews.NewsReturn, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Bài bị xóa] [Trả lại tin bài pv]", _obj_T_News.News_ID, ConstAction.BaoDT);

                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 3)
                LoadData_Baibixoa();
        }

        #endregion

        #region Event Click

        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                pages1.PageIndex = 0;
                this.LoadData_Bitralai();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                Pager3.PageIndex = 0;
                this.LoadData_Baidaxuly();
            }
            else
            {
                pageBaixoa.PageIndex = 0;
                this.LoadData_Baibixoa();
            }
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
                this.LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                this.LoadData_Bitralai();
            else if (TabContainer1.ActiveTabIndex == 2)
                this.LoadData_Baidaxuly();
            else
                this.LoadData_Baibixoa();
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BaoDienTu/ArticleApproveEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Send_ChoDuyet_Click(object sender, EventArgs e)
        {
            Send_choduyet();
        }

        protected void tralai_HoaSi_Click(object sender, EventArgs e)
        {
            Send_Back_HoaSi();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cboNgonNgu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.cboNgonNgu.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
        protected string IsStatusImages(string str)
        {
            string strReturn = "";
            int _count = UltilFunc.CountImgTag(str);
            if (_count != 0)
                strReturn = _count.ToString();
            return strReturn;
        }

        #endregion

        #region Datagrid Events

        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            T_NewsVersion _obj_T_NewsVecion = new T_NewsVersion();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 0)
                    tab = 0;
                double _ID = Convert.ToDouble(dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString());
                _obj_T_News = tt.load_T_news(Convert.ToInt32(_ID));
                if (_obj_T_News.News_Lock)
                {
                    if (_obj_T_News.News_EditorID == _user.UserID)
                    {
                        //Lock
                        tt.IsLock(_ID, 1, _user.UserID);
                        Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!.');", true);
                        return;
                    }
                }
                else
                {
                    //Lock
                    tt.IsLock(_ID, 1, _user.UserID);
                    Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
                }
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {
            T_NewsDAL tt = new T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 1)
                    tab = 1;
                double _ID = Convert.ToDouble(dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString());
                _obj_T_News = tt.load_T_news(Convert.ToInt32(_ID));
                if (_obj_T_News.News_Lock)
                {
                    if (_obj_T_News.News_EditorID == _user.UserID)
                    {
                        //Lock
                        tt.IsLock(_ID, 1, _user.UserID);
                        Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                        return;
                    }
                }
                else
                {
                    //Lock
                    tt.IsLock(_ID, 1, _user.UserID);
                    Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
                }
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
        }
        protected void dgr_BaiXoa_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            T_NewsVersion _obj_T_NewsVecion = new T_NewsVersion();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 3)
                    tab = 3;
                double _ID = Convert.ToDouble(dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('" + CommonLib.ReadXML("lblBanmuonxoa") + "');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('" + CommonLib.ReadXML("lblBanmuonxoa") + "');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        protected void dgr_BaiXoa_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('" + CommonLib.ReadXML("lblBanmuonxoa") + "?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }

        public void pages_IndexChanged_baitralai(object sender, EventArgs e)
        {
            LoadData_Bitralai();
        }

        public void pages_IndexChanged_baidaxuly(object sender, EventArgs e)
        {
            LoadData_Baidaxuly();
        }

        public void pages_IndexChanged_Baixoa(object sender, EventArgs e)
        {
            LoadData_Baibixoa();
        }
        #endregion

        #region dich ngu
        protected void link_copy_Click(object sender, EventArgs e)
        {
            LoadCM();
            ModalPopupExtender1.Show();
        }
        public void LoadCM()
        {
            string where = string.Format(" hoatdong=1 AND ID!=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID));
            NgonNgu_DAL _DAL = new NgonNgu_DAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_NgonNgu(0, 5000, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            DataTable _dv = _ds.Tables[0];
            this.dgCopyNgonNgu.DataSource = _dv;
            this.dgCopyNgonNgu.DataBind();
        }
        protected void but_Trans_Click(object sender, EventArgs e)
        {

            ArrayList arNgu = new ArrayList();
            foreach (DataGridItem m_Item in this.dgCopyNgonNgu.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    arNgu.Add(double.Parse(this.dgCopyNgonNgu.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            ArrayList arrTin = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            T_NewsDAL tt = new T_NewsDAL();
            NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
            T_NgonNgu _obj = new T_NgonNgu();

            LoadData_DangXuly();

            if (arrTin.Count > 0)
            {
                for (int j = 0; j < arrTin.Count; j++)
                {
                    double News_ID = double.Parse(arrTin[j].ToString());
                    if (tt.load_T_news(int.Parse(News_ID.ToString())).Lang_ID == 1)
                    {
                        for (int i = 0; i < arNgu.Count; i++)
                        {
                            //Thực hiện dịch ngữ
                            int Lang_ID = int.Parse(arNgu[i].ToString());
                            if (!tt.CheckLangquageStartnotVietNam(int.Parse(News_ID.ToString()), Lang_ID))
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                            }
                            else
                            {
                                if (!HPCShareDLL.HPCDataProvider.Instance().LanquageExitsTranlate(int.Parse(News_ID.ToString()), Lang_ID))
                                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                                else
                                {
                                    if (Lang_ID == 1)
                                    {
                                        if (TabContainer1.ActiveTabIndex == 1)
                                            tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tk, _user.UserID, tt.load_T_news(int.Parse(News_ID.ToString())).News_DateEdit);
                                        else
                                            tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tk, _user.UserID, DateTime.Now);
                                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tk, 4, _user.UserID);
                                    }
                                    else
                                    {
                                        HPCShareDLL.HPCDataProvider.Instance().Insert_T_NewsChuyenDe_From_T_NewsChuyenDe(int.Parse(News_ID.ToString()), int.Parse(News_ID.ToString()), Lang_ID, ConstNews.NewsApproving_tk, _user.UserID, DateTime.Now);
                                        tt.Insert_Version_From_T_News_WithLanquage(News_ID, ConstNews.NewsApproving_tk, 4, _user.UserID, Lang_ID, DateTime.Now);
                                        string _ActionsCode = "[Báo Điện tử] [Dịch ngữ] [Ngữ: " + _obj.TenNgonNgu + "] [Bài: " + tt.load_T_news(int.Parse(News_ID.ToString())).News_Tittle.ToString() + "]";
                                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Dịch ngữ]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, 0, ConstAction.BaoDT);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT_EXITS") + "');", true);
                        return;
                    }
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhandich") + "');", true);
                return;
            }
            ModalPopupExtender1.Hide();
            LoadData_DangXuly();
            Response.Redirect("~/BaoDienTu/ArticleApproveList.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + TabContainer1.ActiveTabIndex);
        }
        #endregion
    }
}
