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
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HPCApplication;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class PublishedList : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion

        #region Load Methods
        private double NewsID
        {
            get { if (ViewState["NewsID"] != null) return Convert.ToDouble(ViewState["NewsID"]); else return 0.0; }

            set { ViewState["NewsID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        LoadCombox();
                        int tab_id = 0;
                        int.TryParse(Request["Tab"] == null ? "0" : Request["Tab"], out tab_id);
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);
                    }
                }
            }
        }
        protected void ActiverPermission()
        {
            this.LinkButton1.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkButton1');");
            this.LinkHuyXB.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkHuyXB');");
            this.btnDangBaiTop.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonXB") + "','ctl00_MainContent_TabContainer1_TabPanel1_btnDangBaiTop');");
            this.btnDangBaiBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonXB") + "','ctl00_MainContent_TabContainer1_TabPanel1_btnDangBaiBottom');");
            this.btnReturnUnPubLisherTop.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_TabPanel1_btnReturnUnPubLisherTop');");
            this.btnReturnUnPubLisherBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_TabPanel1_btnReturnUnPubLisherBottom');");
            this.btnDeleteTop.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel1_btnDeleteTop');");
            this.linkDelete.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel1_linkDelete');");
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
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " and Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    pages.PageIndex = 0;
                    this.LoadData_DangXuly();
                    break;
                case 1:
                    PagerUnPublish.PageIndex = 0;
                    this.LoadData_UnPublisher();
                    break;
                default: pages.PageIndex = 0;
                    this.LoadData_DangXuly();
                    break;
            }

        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            string tieude = txt_tieude.Text.Trim();
            Session["searchvalueroot"] = null;
            if (tieude != "")
            {
                Session["searchvalueroot"] = UltilFunc.SplitString(tieude);
            }
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsDynamic(pages.PageIndex, pages.PageSize, BuildSQL(6, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.BindGridT_NewsDynamic(pages.PageIndex - i, pages.PageSize, BuildSQL(6, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
            CurrentPage2.TotalPages = pages.CalculateTotalPages();
            CurrentPage2.PageIndex = pages.PageIndex;

            GetTotal();
            _ds.Clear();

        }
        #region COPY NEWS CATEGORYS
        public void LoadCM()
        {
            string where = " Ma_Chuyenmuc_Cha = 0 AND HienThi_BDT = 1 and HienThi_BDT = 1 ";
            if (!String.IsNullOrEmpty(this.txtSearch_name.Text.Trim()))
                where += " AND Ten_ChuyenMuc like N'%" + UltilFunc.SqlFormatText(this.txtSearch_name.Text.Trim()) + "%'";
            if (ddlLang.SelectedIndex > 0)
                where += " AND Ma_AnPham =" + UltilFunc.SqlFormatText(ddlLang.SelectedValue);

            //where += " Order by T_ChuyenMuc.ThuTuHienThi ASC";
            ChuyenmucDAL _cateDAL = new ChuyenmucDAL();
            DataSet _ds;
            _ds = _cateDAL.BindGridT_Cagegorys(0, 5000, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            DataTable _dv = _cateDAL.BindGridCategory(_ds.Tables[0]);
            _ds.Clear();
            dgCategorysCopy.DataSource = _dv;
            dgCategorysCopy.DataBind();
        }
        #endregion END
        protected void GetTotal()
        {
            string sOrder = "";
            if (TabContainer1.ActiveTabIndex == 0)
                sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            else
                sOrder = " ORDER BY News_DateSend DESC ";
            string tieude = txt_tieude.Text.Trim();
            Session["searchvalueroot"] = null;
            if (tieude != "")
            {
                Session["searchvalueroot"] = UltilFunc.SplitString(tieude);
            }
            string _dangdang, _dsngungdang;
            _dangdang = UltilFunc.GetTotalCountT_NewsStatus(BuildSQL(ConstNews.NewsPublishing, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"), "[CMS_ListCountT_News_FullTextSearch]").ToString();
            _dsngungdang = UltilFunc.GetTotalCountT_NewsStatus(BuildSQL(ConstNews.NewsUnPublishing, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"), "[CMS_ListCountT_News_FullTextSearch]").ToString();
            _dangdang = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinxuatban") + " (" + _dangdang + ")";
            _dsngungdang = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinngungdang") + " (" + _dsngungdang + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dangdang + "','" + _dsngungdang + "');", true);
        }

        #endregion

        #region Menthods
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
            strFileName = _user.UserName + "_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
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
            else return " News_DatePublished DESC";
        }

        protected string BuildSQL(int status, string sOrder)
        {
            //dgr_tintuc1.Columns[8].Visible = false;
            LinkButton_updateTT.Visible = false;
            string sql = string.Empty;
            string sClause = " 1=1 And News_DatePublished is NOT null  AND News_Status=" + status + " and CAT_ID in (select DISTINCT(T_Nguoidung_Chuyenmuc.Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + ") ";
            string sWhere = string.Empty;
            if (chkNewsIsBaidinh.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsHomePages=1 ";
            }
            if (chkNewsIsFocus.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsHot=1 ";
            }

            if (chkNewTieudiem.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsFocus=1 ";
            }
            if (chkNewFocusParent.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsCategoryParrent=1 ";
            }
            if (chkNewFocusChild.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsCategorys=1 ";
            }

            if (chkImageIsFocus.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsImages=1 ";
            }
            if (chkVideoIsFocus.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsVideo=1 ";
            }
            if (chkHosoIsFocus.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_IsHistory=1 ";

            }
            if (chDisplayMobi.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_DisplayMobile=1 ";

            }
            if (cbMoreViews.Checked)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_Delete=1 ";

            }
            if (txtTieuDetin.Text.Length > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += " News_Tittle LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txtTieuDetin.Text.Trim()));
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "" + string.Format(" CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.cbo_chuyenmuc.SelectedValue);
            }
            if (cboNgonNgu.SelectedIndex > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            }

            if (txt_tungay.Text.Length > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_DatePublished >= convert(datetime,'" + txt_tungay.Text.Trim() + " 00:00:00',103) ";
            }
            if (txt_denngay.Text.Length > 0)
            {
                if (sWhere.Trim().Length > 0) sWhere += " AND ";
                sWhere += "  News_DatePublished <= convert(datetime,'" + txt_denngay.Text.Trim() + " 23:59:59',103) ";
            }
            sql += sClause;
            if (sWhere.Trim().Length > 0)
                sql += " AND" + sWhere;
            return sql + sOrder;
        }

        #endregion

        #region Event Click

        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            TabContainer1_ActiveTabChanged(sender, e);

        }

        protected void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }
        protected void HuyDXB_Click(object sender, EventArgs e)
        {
            NgungDang(this.dgr_tintuc1);
        }
        private void NgungDang(DataGrid dgr)
        {
            foreach (DataGridItem m_Item in dgr.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                if (chk_Select != null && chk_Select.Checked)
                {
                    LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                    double News_ID = double.Parse(dgr.DataKeys[m_Item.ItemIndex].ToString());
                    tt.IsLock(News_ID, 0);
                    tt.UpdateStatus_T_News_ex_New_HV(News_ID, ConstNews.NewsUnPublishing, _user.UserID, DateTime.Now);
                    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsPublishing, ConstNews.NewsUnPublishing, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                           Request["Menu_ID"].ToString(), "[Xuất bản tin bài] [Danh sách tin bài đang đăng] [Hủy đăng]", News_ID, ConstAction.BaoDT);
                }
            }
            //Tao cache
            //UltilFunc.GenCacheHTML();
            this.LoadData_DangXuly();

        }

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cboNgonNgu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }

        }

        protected void LinkButton_updateTT_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in dgr_tintuc1.Items)
            {
                TextBox txt_thutu = (TextBox)m_Item.FindControl("txt_thutu");
                Label lbl_thutu = (Label)m_Item.FindControl("lbl_thutu");
                double _ID = Convert.ToInt32(dgr_tintuc1.DataKeys[m_Item.ItemIndex].ToString());
                if (txt_thutu.Text != lbl_thutu.Text)
                {
                    int vitri = 0;
                    try
                    {
                        vitri = int.Parse(txt_thutu.Text);
                    }
                    catch { vitri = 0; }
                    Label lbl_News_ID = (Label)m_Item.FindControl("lbl_News_ID");
                    HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                    tt.Update_Thutu_Noibat(double.Parse(lbl_News_ID.Text), vitri);
                }
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Cập nhật thành công !');", true);
            LoadData_DangXuly();
        }
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            LoadCM();
        }
        protected void but_XB_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL _NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            HPCBusinessLogic.ChuyenmucDAL _catDAL = new ChuyenmucDAL();
            foreach (DataGridItem m_Item in dgCategorysCopy.Items)
            {
                CheckBox checkcopy = (CheckBox)m_Item.FindControl("optSelect");
                if (checkcopy != null && checkcopy.Checked && checkcopy.Enabled)
                {
                    int CatalogID = int.Parse(dgCategorysCopy.DataKeys[m_Item.ItemIndex].ToString());
                    double _IDNewsCopy = _NewsDAL.Copy_To_Categorys(NewsID, CatalogID, 6, DateTime.Now, _user.UserID, _user.UserID, _user.UserName);
                    if (_IDNewsCopy > 0)
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Copy]", Request["Menu_ID"], "[Copy] [Bài đã xuất bản]: [Tin bài đã xuất bản] [Thao tác copy bài vào chuyên mục: " + _catDAL.GetOneFromT_ChuyenmucByID(CatalogID).Ten_ChuyenMuc + "]", _IDNewsCopy, ConstAction.TSAnh);
                }
            }
            ModalPopupExtender1.Hide();
            this.LoadData_DangXuly();
            //Tao cache
            //UltilFunc.GenCacheHTML();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Xuất bản ra chuyên mục khác thành công !');", true);
        }
        protected string IsStatusImages(string str)
        {
            string strReturn = "";
            int _count = UltilFunc.CountImgTag(str);
            if (_count != 0)
                strReturn = "<b> &nbsp;&nbsp;[ <font color='red'> có " + _count.ToString() + " ảnh</font> ]</b>";
            return strReturn;
        }
        #endregion

        #region Dategrid Menthods

        protected void dgr_tintuc1_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            switch (e.CommandArgument.ToString().ToLower())
            {
                case "downloadalias":
                    LoadFileDoc(Convert.ToInt32(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString()));
                    break;
                case "edit":
                    {
                        string _ID = dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString();
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(_ID));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                //Lock
                                tt.IsLock(Convert.ToInt32(_ID), 1, _user.UserID);
                                Response.Redirect("PublishedEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=0");
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
                            tt.IsLock(Convert.ToInt32(_ID), 1, _user.UserID);
                            Response.Redirect("PublishedEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=0");
                        }

                    }
                    break;
                case "copycm":
                    {
                        ddlLang.Items.Clear();
                        UltilFunc.BindCombox(this.ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
                        if (this.ddlLang.Items.Count >= 3)
                            this.ddlLang.SelectedIndex = HPCComponents.Global.DefaultLangID;
                        else
                            this.ddlLang.SelectedIndex = UltilFunc.GetIndexControl(this.ddlLang, HPCComponents.Global.DefaultCombobox);
                        NewsID = Convert.ToDouble(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString());
                        LoadCM();
                        ModalPopupExtender1.Show();
                    }
                    break;
            }
        }

        protected void dgr_tintuc1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void dgCategorysCopy_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "editcopy")
            {
                HPCBusinessLogic.DAL.T_NewsDAL _NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                HPCBusinessLogic.ChuyenmucDAL _catDAL = new ChuyenmucDAL();
                DataGridItem m_Item = e.Item;
                int CatalogID = int.Parse(dgCategorysCopy.DataKeys[m_Item.ItemIndex].ToString());
                double _IDNewsCopy = _NewsDAL.Copy_To_Categorys(NewsID, CatalogID, 6, DateTime.Now, _user.UserID, _user.UserID, _user.UserName);
                if (_IDNewsCopy > 0)
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Copy]", Request["Menu_ID"], "[Copy] [Bài đã xuất bản]: [Tin bài đã xuất bản] [Thao tác copy bài vào chuyên mục: " + _catDAL.GetOneFromT_ChuyenmucByID(CatalogID).Ten_ChuyenMuc + "]", _IDNewsCopy, ConstAction.BaoDT);
                CheckBox checkcopy = (CheckBox)m_Item.FindControl("optSelect");
                ImageButton btnCopy = (ImageButton)m_Item.FindControl("btnCopy");
                btnCopy.Visible = false;
                checkcopy.Visible = false;
                checkcopy.Enabled = false;
                this.LoadData_DangXuly();
            }
        }

        #endregion

        #region BAI NGUNG DANG
        protected void dgListNewsUnPublish_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void dgListNewsUnPublish_EditCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandArgument.ToString().ToLower())
            {
                case "downloadalias":
                    LoadFileDoc(Convert.ToInt32(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString()));
                    break;
                case "edit":
                    {
                        HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
                        string _ID = this.dgListNewsUnPublish.DataKeys[e.Item.ItemIndex].ToString();
                        Dal.IsLock(double.Parse(_ID), 1);
                        Response.Redirect("PublishedEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=1");
                    }
                    break;
                case "copycm":
                    {
                        ddlLang.Items.Clear();
                        UltilFunc.BindCombox(this.ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
                        if (this.ddlLang.Items.Count == 2)
                            this.ddlLang.SelectedIndex = 1;
                        else
                            this.ddlLang.SelectedIndex = UltilFunc.GetIndexControl(this.ddlLang, HPCComponents.Global.DefaultCombobox);
                        ModalPopupExtender1.Show();
                    }
                    break;
            }
        }
        protected void LoadData_UnPublisher()
        {
            string sOrder = " Order by News_DateSend DESC ";
            PagerUnPublish.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            string tieude = txt_tieude.Text.Trim();
            Session["searchvalueroot"] = null;
            if (tieude != "")
            {
                Session["searchvalueroot"] = UltilFunc.SplitString(tieude);
            }
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsDynamic(PagerUnPublish.PageIndex, PagerUnPublish.PageSize, BuildSQL(ConstNews.NewsUnPublishing, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"));

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.BindGridT_NewsDynamic(PagerUnPublish.PageIndex - i, PagerUnPublish.PageSize, BuildSQL(ConstNews.NewsUnPublishing, sOrder), UltilFunc.ReplaceAll(UltilFunc.SplitString(tieude), "'", "’"));
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }
            this.dgListNewsUnPublish.DataSource = _ds;
            this.dgListNewsUnPublish.DataBind();
            this.PagerUnPublish.TotalRecords = this.CurrentPageUnPublish.TotalRecords = TotalRecords;
            this.CurrentPageUnPublish.TotalPages = this.PagerUnPublish.CalculateTotalPages();
            this.CurrentPageUnPublish.PageIndex = this.PagerUnPublish.PageIndex;

            GetTotal();
            _ds.Clear();
        }
        protected void PagerUnPublish_IndexChanged(object sender, EventArgs e)
        {
            this.LoadData_UnPublisher();
        }
        private void ReturnNewsPublisher(DataGrid dgr)
        {
            T_NewsDAL tt = new T_NewsDAL();
            T_News obj = new T_News();
            foreach (DataGridItem m_Item in dgr.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {

                    LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                    double News_ID = double.Parse(dgr.DataKeys[m_Item.ItemIndex].ToString());
                    obj = tt.GetOneFromT_NewsByID(News_ID);
                    tt.IsLock(News_ID, 0);
                    //if (obj.Lang_ID == 1)
                    //{
                    //    tt.UpdateStatus_T_News_ex_New_HV(News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                    //    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsUnPublishing, ConstNews.NewsReturn_tk, _user.UserID);
                    //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                    //           Request["Menu_ID"].ToString(), "[Xuất bản tin bài] [Danh sách tin bài hủy đăng] [Trả lại Trình bày bài viết]", News_ID, ConstAction.BaoDT);
                    //}
                    //else
                    //{
                        tt.UpdateStatus_T_News_ex_New_HV(News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsUnPublishing, ConstNews.NewsReturn_tb, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                               Request["Menu_ID"].ToString(), "[Xuất bản tin bài] [Danh sách tin bài hủy đăng] [Trả lại Biên tập tin bài]", News_ID, ConstAction.BaoDT);
                    //}
                }
            }
            this.LoadData_UnPublisher();
        }
        private void PublisherNews(DataGrid dgr)
        {
            foreach (DataGridItem m_Item in dgr.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                if (chk_Select != null && chk_Select.Checked)
                {
                    LinkButton linkname = (LinkButton)m_Item.FindControl("linkTittle");
                    double News_ID = double.Parse(dgr.DataKeys[m_Item.ItemIndex].ToString());
                    tt.IsLock(News_ID, 0);
                    tt.UpdateStatus_T_News_ex_New_HV(News_ID, ConstNews.NewsPublishing, _user.UserID, DateTime.Now);
                    tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsUnPublishing, ConstNews.NewsPublishing, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                           Request["Menu_ID"].ToString(), "[Xuất bản] [Danh sách tin bài hủy đăng] [Đăng bài]", News_ID, ConstAction.BaoDT);
                    #region Sync
                    HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                    HPCInfo.T_News _obj = new HPCInfo.T_News();
                    _obj = _untilDAL.GetOneFromT_NewsByID(News_ID);
                    // DONG BO FILE
                    SynFiles _syncfile = new SynFiles();
                    if (_obj.Images_Summary.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_obj.Images_Summary, Global.ImagesService);
                    }

                    //Cap nhat anh trong bai viet - vao may dong bo
                    if (_obj.News_Body.Length > 5)
                    {
                        _syncfile.SearchImgTag(_obj.News_Body);
                        _syncfile.SearchTagSwf(_obj.News_Body);
                        _syncfile.SearchTagFLV(_obj.News_Body);
                    }
                    //END
                    #endregion
                }
            }
            //Tao cache
            //UltilFunc.GenCacheHTML();
            this.LoadData_UnPublisher();
        }
        protected void btnDangBaiTop_Click(object sender, EventArgs e)
        {
            this.PublisherNews(this.dgListNewsUnPublish);
        }
        protected void btnReturnUnPubLisherTop_Click(object sender, EventArgs e)
        {
            this.ReturnNewsPublisher(this.dgListNewsUnPublish);
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        private void DelRecordsCheckedBox()
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgListNewsUnPublish.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgListNewsUnPublish.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                double News_ID = double.Parse(dgListNewsUnPublish.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                                tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                           Request["Menu_ID"].ToString(), "[Bài đang xuất bản] [Danh sách tin bài ngừng đăng] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            double News_ID = double.Parse(dgListNewsUnPublish.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            tt.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                                       Request["Menu_ID"].ToString(), "[Bài đang xuất bản] [Danh sách tin bài ngừng đăng] [Xóa bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        }
                    }
                }
            }
            this.LoadData_UnPublisher();
        }
        #endregion END
        #region Bôi màu
        public string paintColorSearch(Object strSearchword)
        {
            string txtFindSearchWord = Server.HtmlDecode(Convert.ToString(strSearchword).Trim());
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = txtFindSearchWord.Normalize(NormalizationForm.FormD);
            v_str_FormD = v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            string[] arrayWordSearch = arrSearchWord(Convert.ToString(Session["searchvalueroot"]));
            for (int x = 0; x < arrayWordSearch.Length; x++)
            {
                if (arrayWordSearch[x].ToString().Trim() != ""
                    && arrayWordSearch[x].ToString().Trim().ToLower() != "or"
                    && arrayWordSearch[x].ToString().Trim().ToLower() != "and"
                    && arrayWordSearch[x].ToString().Trim().ToLower() != " or"
                    && arrayWordSearch[x].ToString().Trim().ToLower() != " and")
                {
                    txtFindSearchWord = boimau(txtFindSearchWord, arrayWordSearch[x].ToString().Trim());
                }
            }
            return FunctionReplace(txtFindSearchWord);
        }

        public string boimau(string _root, string _strsearch)
        {
            string _strreturn = _root, _search = _strsearch;
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            _search = _search.Normalize(NormalizationForm.FormD);
            _search = v_reg_regex.Replace(_search, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            int check = 0, subindex = 0;
            if (_root.Length >= _strsearch.Length)
            {
                for (int i = 0; i < _root.Length; i++)
                {
                    string _strcheck = "";
                    if (_root.Length - i >= 1)
                    {
                        if (_root.Length - i >= 1)
                            _strcheck = _root.Substring(i, 1);
                        if (_strcheck == "<")
                            check = 1;
                        else
                        {
                            if (check == 1)
                            {
                                string _strcheck2 = "";
                                if (_root.Length - i >= 1)
                                    _strcheck2 = _root.Substring(i, 1);
                                if (_strcheck2 == ">")
                                    check = 0;
                            }
                        }
                    }
                    if ((_root.Length - i) >= _search.Length && check == 0)
                    {
                        string _strcomp = "";

                        if (i > 0)
                        {
                            _strcomp = _root.Substring(i, _search.Length);
                        }
                        else if (i == 0)
                        {
                            if ((_root.Length - (i + _search.Length)) >= 1)
                                _strcomp = _root.Substring(i, _search.Length + 1);
                            else if ((_root.Length - (i + _search.Length)) == 0)
                                _strcomp = _root.Substring(i, _search.Length);
                        }
                        _strcomp = _strcomp.Normalize(NormalizationForm.FormD);
                        _strcomp = v_reg_regex.Replace(_strcomp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                        if (_strcomp.ToLower().Trim() == _search.ToLower())
                        {
                            int tes = _strreturn.Length;
                            _strreturn = _strreturn.Substring(0, i + subindex)
                                + "<span style=\"background-color: #FFFF00\">" + _strreturn.Substring(i + subindex, _search.Length) + "</span>"
                                + _strreturn.Substring(i + subindex + _search.Length, _strreturn.Length - (i + subindex + _search.Length));//47
                            subindex = subindex + 47;
                        }
                    }
                }
            }
            return _strreturn;

        }

        public string FunctionReplace(string wordSearch)
        {
            string word_search = Regex.Replace(wordSearch, "<sp<span style=\"background-color: #FFFF00\">an</span>", "<span>", System.Text.RegularExpressions.RegexOptions.None);
            return Regex.Replace(word_search, "</sp<span style=\"background-color: #FFFF00\">an</span>", "<span>", System.Text.RegularExpressions.RegexOptions.None); ;
        }

        public string[] arrSearchWord(string strString)
        {
            string[] arInfo;
            char[] splitter = { '"' };
            arInfo = strString.Split(splitter);
            return arInfo;
        }
        #endregion
    }
}
