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
using ToasoanTTXVN;
namespace HPCApplication.Article
{
    public partial class ArticleList : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        #region Load Methors
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
            this.LinkDeleteOne.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkDeleteOne');");
            this.LinkPubOne.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkPubOne');");

            this.LinkPubTwo.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel1_LinkPubTwo');");
            this.LinkDeleteTwo.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel1_LinkDeleteTwo');");

            this.btnGuiDuyet.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonguiTBT") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_btnGuiDuyet');");
            this.BtnXoa.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_BtnXoa');");
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
            string sOrder = " Order by News_DateCreated DESC ";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, BuildSQL(ConstNews.AddNew, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(ConstNews.AddNew, sOrder));
            this.dgr_tintuc1.DataSource = _ds;
            this.dgr_tintuc1.DataBind(); _ds.Clear();
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
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages1.PageIndex, pages1.PageSize, BuildSQL(ConstNews.NewsReturn, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages1.PageIndex - 1, pages1.PageSize, BuildSQL(ConstNews.NewsReturn, sOrder));
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
            _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(Pager3.PageIndex, Pager3.PageSize, BuildSQL(ConstNews.NewsAppro, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);

            if (TotalRecord == 0)
                _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(Pager3.PageIndex - 1, Pager3.PageSize, BuildSQL(ConstNews.NewsAppro, sOrder));
            Dgr_Baidaxuly.DataSource = _ds;
            Dgr_Baidaxuly.DataBind(); _ds.Clear();
            Pager3.TotalRecords = CurrentPage3.TotalRecords = TotalRecords;
            CurrentPage3.TotalPages = Pager3.CalculateTotalPages();
            CurrentPage3.PageIndex = Pager3.PageIndex;
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
            string _dsDangCho, _dsDaXuLy, _dsDatralai, _dsBaiBiXoa;
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            _dsDangCho = UltilFunc.GetTotalCountStatus(BuildSQL(ConstNews.AddNew, sOrder), "CMS_CountListT_News").ToString();
            _dsDatralai = UltilFunc.GetTotalCountStatus(BuildSQL(ConstNews.NewsReturn, sOrder), "CMS_CountListT_News").ToString();
            _dsDaXuLy = UltilFunc.GetTotalCountStatus(BuildSQL(ConstNews.NewsAppro, sOrder), "CMS_CountListT_NewsVersion").ToString();
            _dsBaiBiXoa = UltilFunc.GetTotalCountStatus(BuildSQL(ConstNews.NewsDelete, sOrder), "CMS_CountListT_News").ToString();
            _dsDangCho = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinmoi") + " (" + _dsDangCho + ")";
            _dsDatralai = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTintralai") + " (" + _dsDatralai + ")";
            _dsDaXuLy = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindagui") + " (" + _dsDaXuLy + ")";
            _dsBaiBiXoa = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindaxoa") + " (" + _dsBaiBiXoa + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangCho + "','" + _dsDatralai + "','" + _dsDaXuLy + "','" + _dsBaiBiXoa + "');", true);
            
        }
        #endregion
        #endregion

        #region Methods
        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)
                    return " News_DateEdit DESC";
                else
                    return " ID DESC";
            }
        }
        protected string BuildSQL(int status, string sOrder)
        {
            string sql = string.Empty;
            string sClause = " 1=1 ";
            if (status == ConstNews.NewsDelete)
            {
                sClause += " AND News_EditorID =" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
            }
            else
            {
                sClause += " AND News_AuthorID =" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
            }
            sClause += " AND News_Status = " + status + " and CAT_ID in (select DISTINCT(Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + ")";
            string sWhere = string.Empty;
            if (txt_tieude.Text.Length > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += " News_Tittle LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
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
        /*Set image enablation*/
        protected bool IsEnable(string prmNews_Lock, string prmEditorID)
        {
            int _prmEditorID = Convert.ToInt32(prmEditorID);
            if (prmNews_Lock == "True" && _prmEditorID != _user.UserID)
                return false;
            else
                return true;
        }
        private void DelRecordsCheckedBox()
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                        Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài đang chờ xử lý] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
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
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text, Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài bị trả lại] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
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
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text, Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài bị xóa] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
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
        private void Send_TKTS()
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        //dung them vao de Unlock truoc khi send
                        //HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                        tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                        //ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        double News_ID = double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tk, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro, ConstNews.NewsApproving_tk, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                            Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài đang chờ xử lý] [Gửi Trình bày tin bài]", News_ID, ConstAction.BaoDT);
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
                        //dung them vao de Unlock truoc khi send
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                        double News_ID = double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tk, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro, ConstNews.NewsApproving_tk, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                            Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài bị trả lại] [Gửi Trình bày tin bài]", News_ID, ConstAction.BaoDT);
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
                        //dung them vao de Unlock truoc khi send
                        LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                        tt.IsLock(double.Parse(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                        double News_ID = double.Parse(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tk, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro, ConstNews.NewsApproving_tk, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                            Request["Menu_ID"].ToString(), "[Nhập tin bài] [Bài bị xóa] [Gửi Trình bày tin bài]", News_ID, ConstAction.BaoDT);
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
            Response.Redirect("~/BaoDienTu/ArticleEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Send_TKTS(object sender, EventArgs e)
        {
            Send_TKTS();
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
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion

        #region Dategrid Events

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
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 0)
                    tab = 0;
                HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
                string _ID = dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString();
                Dal.IsLock(double.Parse(_ID), 1);
                Response.Redirect("ArticleEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString());
                ToasoanTTXVN.BaoDienTu.FilesDoc _file = new ToasoanTTXVN.BaoDienTu.FilesDoc();
                _file.LoadFileDoc(_user.UserName, _ID);
            }
        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 1)
                    tab = 1;
                HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
                string _ID = dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();
                Dal.IsLock(double.Parse(_ID), 1);
                Response.Redirect("ArticleEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString());
                ToasoanTTXVN.BaoDienTu.FilesDoc _file = new ToasoanTTXVN.BaoDienTu.FilesDoc();
                _file.LoadFileDoc(_user.UserName, _ID);
            }
        }
        protected void dgr_BaiXoa_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 3)
                    tab = 3;
                double _ID = Convert.ToDouble(dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("ArticleEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            else if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                ToasoanTTXVN.BaoDienTu.FilesDoc _file = new ToasoanTTXVN.BaoDienTu.FilesDoc();
                _file.LoadFileDoc(_user.UserName, _ID);
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void dgr_BaiXoa_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        #endregion

    }
}
