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
using System.Collections.Generic;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class Album_List : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
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
                    this.ActiverPerssion();
                    if (!IsPostBack)
                    {
                        string back = "";
                        string text_search = "";
                        try { back = Request["Back"].ToString(); }
                        catch { ;}
                        int pageindex = 0, tabindex = 0, langid = 0;
                        LoadComboBox();
                        if (!string.IsNullOrEmpty(back))
                        {
                            try { text_search = Session["QLPS_TenPS"].ToString(); }
                            catch { ;}
                            try { langid = int.Parse(Session["CurrentLangid_QLPS"].ToString()); }
                            catch { ;}
                            try { pageindex = int.Parse(Session["CurrentPage_QLPS"].ToString()); }
                            catch { ;}
                            try { tabindex = int.Parse(Session["CurrentTab_QLPS"].ToString()); }
                            catch { ;}
                            txtSearch_Cate.Text = text_search;
                            cboNgonNgu.SelectedValue = langid.ToString();
                            if (tabindex == 0)
                            {
                                pages.PageIndex = pageindex;
                                TabContainer1.ActiveTabIndex = tabindex;
                            }
                            else if (tabindex == 2)
                            {
                                Pager_tralai.PageIndex = pageindex;
                                TabContainer1.ActiveTabIndex = tabindex;

                            }
                            else
                            {
                                TabContainer1.ActiveTabIndex = 0; pages.PageIndex = 0;
                            }
                            Session["CurrentPage_QLPS"] = null;
                            Session["CurrentTab_QLPS"] = null;
                            Session["CurrentLangid_QLPS"] = null;
                        }
                        else
                        {
                            TabContainer1.ActiveTabIndex = 0; pages.PageIndex = 0;
                        }
                        int tab_id = 0;
                        int.TryParse(Request["Tab"] == null ? "0" : Request["Tab"], out tab_id);
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);
                        //LoadPSmoi();
                        //LoadPStralai();
                        //LoadPSdaduyet();
                        //LoadPSchoduyet();
                    }
                }
            }
        }

        #region Methods
        protected void ActiverPerssion()
        {
            //this.btnAddNewsOn.Visible = _Role.R_Add;
            //this.btnSendNewOn.Visible = _Role.R_Pub; 
            this.btnSendNewOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel_moi_grdListCate_ctl01_chkAll');");
            //this.btnDelelteNewOn.Visible = _Role.R_Del; 
            this.btnDelelteNewOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel_moi_grdListCate_ctl01_chkAll');");
            //this.btnAddNewsBottom.Visible = _Role.R_Add;
            //this.btnSendNewBottom.Visible = _Role.R_Pub;
            this.btnSendNewBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel_moi_grdListCate_ctl01_chkAll');");
            //this.btnDelelteNewBottom.Visible = _Role.R_Del; 
            this.btnDelelteNewBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel_moi_grdListCate_ctl01_chkAll');");

            //this.btnDeleteReturnOn.Visible = _Role.R_Del; 
            this.btnDeleteReturnOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel_tralai_DataGrid_tralai_ctl01_chkAll');");
            //this.btnDeleteReturnBottom.Visible = _Role.R_Del; 
            this.btnDeleteReturnBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel_tralai_DataGrid_tralai_ctl01_chkAll');");
            //this.btnSendReturnOn.Visible = _Role.R_Pub; 
            this.btnSendReturnOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel_tralai_DataGrid_tralai_ctl01_chkAll');");
            //this.btnSendReturnBottom.Visible = _Role.R_Pub; 
            this.btnSendReturnBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel_tralai_DataGrid_tralai_ctl01_chkAll');");
        }
        public string LoadImage(object Url)
        {
            string _Url = "";
            try { _Url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_Url))
                return CommonLib.tinpathBDT(_Url);
            else
                return "~/Dungchung/Images/no_images.jpg";
        }

        private void LoadComboBox()
        {

            cboNgonNgu.Items.Clear();
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
                cboNgonNgu.SelectedIndex = Global.DefaultLangID;
            else cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);

        }

        protected string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus.ToLower() == "false")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (prmImgStatus.ToLower() == "true")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            return strReturn;
        }

        public void LoadPSmoi()
        {
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(pages.PageIndex, pages.PageSize, WhereCondition(0));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(pages.PageIndex - 1, pages.PageSize, WhereCondition(0));
            grdListCate.DataSource = _ds;
            grdListCate.DataBind(); _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            GetTotal();
            foreach (DataGridItem item in grdListCate.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void LoadPSchoduyet()
        {
            Pager_choduyet.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_choduyet.PageIndex, Pager_choduyet.PageSize, WhereCondition(1));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_choduyet.PageIndex - 1, Pager_choduyet.PageSize, WhereCondition(1));
            DataGrid_choduyet.DataSource = _ds;
            DataGrid_choduyet.DataBind(); _ds.Clear();
            Pager_choduyet.TotalRecords = CurrentPage_choduyet.TotalRecords = TotalRecords;
            CurrentPage_choduyet.TotalPages = Pager_choduyet.CalculateTotalPages();
            CurrentPage_choduyet.PageIndex = Pager_choduyet.PageIndex;
            GetTotal();
            foreach (DataGridItem item in DataGrid_choduyet.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void LoadPStralai()
        {
            Pager_tralai.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_tralai.PageIndex, Pager_tralai.PageSize, WhereCondition(2));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_tralai.PageIndex - 1, Pager_tralai.PageSize, WhereCondition(2));
            DataGrid_tralai.DataSource = _ds;
            DataGrid_tralai.DataBind(); _ds.Clear();
            Pager_tralai.TotalRecords = CurrentPage_tralai.TotalRecords = TotalRecords;
            CurrentPage_tralai.TotalPages = Pager_tralai.CalculateTotalPages();
            CurrentPage_tralai.PageIndex = Pager_tralai.PageIndex;
            GetTotal();
            foreach (DataGridItem item in DataGrid_tralai.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void LoadPSdaduyet()
        {
            Pager_daduyet.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_daduyet.PageIndex, Pager_daduyet.PageSize, WhereCondition(3));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_daduyet.PageIndex - 1, Pager_daduyet.PageSize, WhereCondition(3));
            DataGrid_XB.DataSource = _ds;
            DataGrid_XB.DataBind(); _ds.Clear();
            Pager_daduyet.TotalRecords = CurrentPage_daduyet.TotalRecords = TotalRecords;
            CurrentPage_daduyet.TotalPages = Pager_daduyet.CalculateTotalPages();
            CurrentPage_daduyet.PageIndex = Pager_daduyet.PageIndex;
            GetTotal();
            foreach (DataGridItem item in DataGrid_XB.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        #region TONG SO
        public void GetTotal()
        {
            string _dsDangnew, _dsDangchoduyet, _dsDadaduyet, _dsDatralai;
            _dsDangnew = UltilFunc.GetTotalCountStatus(WhereCondition(0), "[CMS_CountListT_Album_Categories]").ToString();
            _dsDangchoduyet = UltilFunc.GetTotalCountStatus(WhereCondition(1), "[CMS_CountListT_Album_Categories]").ToString();
            _dsDatralai = UltilFunc.GetTotalCountStatus(WhereCondition(2), "[CMS_CountListT_Album_Categories]").ToString();
            _dsDadaduyet = UltilFunc.GetTotalCountStatus(WhereCondition(3), "[CMS_CountListT_Album_Categories]").ToString();
            
            _dsDangnew = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhmoi") + " (" + _dsDangnew + ")";
            _dsDangchoduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhchoduyet") + " (" + _dsDangchoduyet + ")";
            _dsDatralai = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhtralai") + " (" + _dsDatralai + ")";
            _dsDadaduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhXB") + " (" + _dsDadaduyet + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangnew + "','" + _dsDangchoduyet + "','" + _dsDatralai + "','" + _dsDadaduyet + "');", true);
        }
        #endregion
        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            switch (status)
            {
                case 0:
                    _whereNews += " and UserCreated = " + _user.UserID + " AND Cat_Album_Status_Approve = 1";// AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                case 1:
                    _whereNews += " and UserCreated = " + _user.UserID + " AND Cat_Album_Status_Approve = 2";// AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                case 2:
                    _whereNews += " and UserCreated = " + _user.UserID + " AND  Cat_Album_Status_Approve = 3";// AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                case 3:
                    _whereNews += " and UserCreated = " + _user.UserID + " AND  Cat_Album_Status_Approve = 4";// AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                default:
                    _whereNews += " and UserCreated = " + _user.UserID + " AND Cat_Album_Status_Approve = 1";// AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
            }
            if (cboNgonNgu.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Lang_ID = {0}", cboNgonNgu.SelectedValue);
            if (txtSearch_Cate.Text.Length > 0)
                _whereNews += " AND " + string.Format(" Cat_Album_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            _whereNews += " Order by Cat_Album_ID DESC";
            return _whereNews;
        }

        #endregion

        #region Event Click

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    pages.PageIndex = 0;
                    LoadPSmoi();
                    break;
                case 1:
                    Pager_choduyet.PageIndex = 0;
                    LoadPSchoduyet();
                    break;
                case 2:
                    Pager_tralai.PageIndex = 0;
                    LoadPStralai();
                    break;
                case 3:
                    Pager_daduyet.PageIndex = 0;
                    LoadPSdaduyet();
                    break;
                default:
                    pages.PageIndex = 0;
                    LoadPSmoi();
                    break;
            }
        }

        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }

        protected void btnLinkSendNgu_Click(object sender, EventArgs e)
        {

        }

        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL objAlbumDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                int AlbumID = 0;
                try
                {
                    foreach (DataGridItem m_Item in grdListCate.Items)
                    {
                        CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                        if (chk_Select != null && chk_Select.Checked)
                        {
                            Label lblTenPS = (Label)m_Item.FindControl("lblTenPS");
                            AlbumID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xóa", Request["Menu_ID"].ToString(), "[Xóa] [Phóng sự ảnh mới] [Thao tác xóa Phóng sự ảnh: " + objAlbumDAL.load_T_Album_Categories(AlbumID).Cat_Album_Name + "]", 0, ConstAction.GocAnh);
                            objAlbumDAL.DeleteFrom_T_Album_Categories(AlbumID);
                        }
                    }
                    LoadPSmoi();
                }
                catch (Exception ex) { }
                finally { objAlbumDAL = null; }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem item in DataGrid_tralai.Items)
                {
                    CheckBox chk_Select = (CheckBox)item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        int _ID = Convert.ToInt32(DataGrid_tralai.DataKeys[int.Parse(item.ItemIndex.ToString())].ToString());
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xóa", Request["Menu_ID"].ToString(), "[Xóa] [Phóng sự ảnh bị trả lại] [Thao tác xóa Phóng sự ảnh: " + objAlbumDAL.load_T_Album_Categories(_ID).Cat_Album_Name + "]", 0, ConstAction.GocAnh);
                        objAlbumDAL.DeleteFrom_T_Album_Categories(_ID);
                    }
                }
                LoadPStralai();
            }
        }

        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Session["CurrentPage_QLPS"] = pages.PageIndex;
                Session["CurrentTab_QLPS"] = 0;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["QLPS_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 1;
                Session["CurrentLangid_QLPS"] = cboNgonNgu.SelectedValue;
                Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&PageFromID=1");
            }
            else if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                HPCBusinessLogic.DAL.T_Album_CategoriesDAL obj_Cate = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                try
                {
                    if (obj_Cate.CheckDelete_T_Album_Categories(Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString())))
                    {
                        obj_Cate.DeleteFrom_T_Album_Categories(Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString()));
                        Label lblTenPS = (Label)e.Item.FindControl("lblTenPS");
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblTenPS.Text, Request["Menu_ID"].ToString(), "Xóa Phóng sự ảnh", 0, ConstAction.GocAnh);
                        this.LoadPSmoi();
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Không được xóa khi còn bài viết trong danh mục này');", true);
                    }

                }
                catch { }
                finally { obj_Cate = null; }
            }

            else if (e.CommandArgument.ToString().ToLower() == "inputphoto")
            {
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Session["CurrentPage_QLPS"] = pages.PageIndex;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["QLPS_TenPS"] = txtSearch_Cate.Text;
                Session["CurrentTab_QLPS"] = 0;
                Session["CurrentLangid_QLPS"] = cboNgonNgu.SelectedValue;
                Session["PageFromID"] = 1;
                Response.Redirect("~/PhongSuAnh/PhotoAlbum_EditMullti.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }

        public void DataGrid_tralai_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + CommonLib.ReadXML("Banmuonxoa") + "\");");
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void DataGrid_tralai_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.DataGrid_tralai.DataKeys[e.Item.ItemIndex].ToString());
                Session["CurrentPage_QLPS"] = Pager_tralai.PageIndex;
                Session["CurrentTab_QLPS"] = 2;
                Session["CurrentLangid_QLPS"] = cboNgonNgu.SelectedValue;
                Session["PageFromID"] = 1;
                Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&PageFromID=1");
            }
            else if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                HPCBusinessLogic.DAL.T_Album_CategoriesDAL obj_Cate = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                try
                {
                    if (obj_Cate.CheckDelete_T_Album_Categories(Convert.ToInt32(this.DataGrid_tralai.DataKeys[e.Item.ItemIndex].ToString())))
                    {
                        obj_Cate.DeleteFrom_T_Album_Categories(Convert.ToInt32(this.DataGrid_tralai.DataKeys[e.Item.ItemIndex].ToString()));
                        Label lblTenPS = (Label)e.Item.FindControl("lblTenPS");
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblTenPS.Text, Request["Menu_ID"].ToString(), "Xóa Phóng sự ảnh", 0, ConstAction.GocAnh);
                        this.LoadPSmoi();
                    }
                    else
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Không được xóa khi còn bài viết trong danh mục này');", true);
                }
                catch (Exception ex) { }
                finally { obj_Cate = null; }
            }
            else if (e.CommandArgument.ToString().ToLower() == "inputphoto")
            {
                int catID = Convert.ToInt32(this.DataGrid_tralai.DataKeys[e.Item.ItemIndex].ToString());
                Session["CurrentPage_QLPS"] = Pager_tralai.PageIndex;
                Session["CurrentTab_QLPS"] = 2;
                Session["CurrentLangid_QLPS"] = cboNgonNgu.SelectedValue;
                Session["PageFromID"] = 1;
                Response.Redirect("~/PhongSuAnh/PhotoAlbum_EditMullti.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    LoadPSmoi();
                    break;
                case 1:
                    LoadPSchoduyet();
                    break;
                case 2:
                    LoadPStralai();
                    break;
                case 3:
                    LoadPSdaduyet();
                    break;
                default:
                    LoadPSmoi();
                    break;
            }
        }

        protected void link_send_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem item in grdListCate.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    int _id = int.Parse(lblcatid.Text);
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                        T_Album.Update_Status(_id, 2, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Gửi duyệt", Request["Menu_ID"].ToString(), "[Gửi duyệt] [Phóng sự mới] [Thao tác Gửi duyệt Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
                }
                LoadPSmoi();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem item in DataGrid_tralai.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                        T_Album.Update_Status(int.Parse(lblcatid.Text), 2, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Gửi duyệt", Request["Menu_ID"].ToString(), "[Gửi duyệt] [Phóng sự trả lại] [Thao tác Gửi duyệt Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", 0, ConstAction.GocAnh);
                }
                LoadPStralai();
            }
        }

        #endregion

        #region page index change
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadPSmoi();
        }
        protected void Pager_choduyet_IndexChanged(object sender, EventArgs e)
        {
            LoadPSchoduyet();
        }
        protected void Pager_tralai_IndexChanged(object sender, EventArgs e)
        {
            LoadPStralai();
        }
        protected void Pager_daduyet_IndexChanged(object sender, EventArgs e)
        {
            LoadPSdaduyet();
        }
        #endregion

        //#region "Syn"
        //private void SynData_DeleteT_Album_CategoriesAny(int _ID, ArrayList _arr)
        //{
        //    if (_arr.Count > 0)
        //    {
        //        for (int i = 0; i < _arr.Count; i++)
        //        {
        //            SynData_DeleteT_Album_Categories(_ID, _arr[i].ToString());
        //        }
        //    }
        //}
        //private void SynData_DeleteT_Album_Categories(int _ID, string urlService)
        //{
        //    string _sql = "[Syn_DeleteOneFromT_Album_Categories]";
        //    ServicesPutDataBusines.UltilFunc _untilDAL = new ServicesPutDataBusines.UltilFunc(urlService);
        //    try
        //    {

        //        _untilDAL.ExecStore(_sql, new string[] { "@Cat_Album_ID" }, new object[] { _ID });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private void SynData_UpdataStatusT_Album_CategoriesAny(string strwhere, ArrayList _arr)
        //{
        //    if (_arr.Count > 0)
        //    {
        //        for (int i = 0; i < _arr.Count; i++)
        //        {
        //            SynData_UpdataStatusT_Album_Categories(strwhere, _arr[i].ToString());
        //        }
        //    }
        //}
        //private void SynData_UpdataStatusT_Album_Categories(string strwhere, string urlService)
        //{
        //    string _sql = "[Syn_UpdateStatusT_Album_Categories]";
        //    ServicesPutDataBusines.UltilFunc _untilDAL = new ServicesPutDataBusines.UltilFunc(urlService);
        //    try
        //    {
        //        _untilDAL.ExecStore(_sql, new string[] { "@WhereCondition" }, new object[] { strwhere });
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        //#endregion end
    }
}
