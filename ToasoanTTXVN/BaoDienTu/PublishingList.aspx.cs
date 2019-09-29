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
using System.Globalization;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class PublishingList : BasePage
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
                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        LoadComboBox();
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
            this.Link_GuiXuatBan.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonXB") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Link_GuiXuatBan');");
            this.LinkButton1.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonXB") + "','ctl00_MainContent_TabContainer1_TabPanel_HenXB_LinkButton1');");
            this.LinkButton2.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Link_TraLai');");
            this.Link_TraLai.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Link_TraLai');");
            this.Link_Delete.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_Link_Delete');");


            this.btnGuiDuyet.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonguiTBT") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_btnGuiDuyet');");
            this.btnTraLai.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_btnTraLai');");
            this.BtnXoa.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanelDelete_BtnXoa');");
        }
        #region Event Click


        protected void Link_GuiXuatBan_Click(object sender, EventArgs e)
        {
            LinkGuiDuyet();

        }

        protected void Link_TraLai_Click(object sender, EventArgs e)
        {
            TraLai();
        }

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void PagerTwo_IndexChanged(object sender, EventArgs e)
        {
            LoadDataBaidaxuly();
        }

        protected void Pager_henXB_IndexChanged(object sender, EventArgs e)
        {
            LoadDaHenXB();
        }
        public void pages_IndexChanged_Baixoa(object sender, EventArgs e)
        {
            LoadData_Baibixoa();
        }
        protected void Link_Delete_Click(object sender, EventArgs e)
        {
            LinkDelete();
        }

        protected void btnLinkDuyetAnh_Click(object sender, EventArgs e)
        {
            LinkGuiDuyet();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    pages.PageIndex = 0;
                    this.LoadData();
                    break;
                case 1:
                    Pager_henXB.PageIndex = 0;
                    this.LoadDaHenXB();
                    break;
                case 2:
                    PagerTwo.PageIndex = 0;
                    this.LoadDataBaidaxuly();
                    break;
                case 3:
                    pageBaixoa.PageIndex = 0;
                    this.LoadData_Baibixoa();
                    break;
                default: pages.PageIndex = 0;
                    this.LoadData();
                    break;
            }
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorysAll.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(ddlCategorysAll, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                ddlCategorysAll.UpdateAfterCallBack = true;
            }
            else
            {
                ddlCategorysAll.DataSource = null;
                ddlCategorysAll.DataBind();
                ddlCategorysAll.UpdateAfterCallBack = true;
            }
        }
        #endregion

        #region Datagrid Methods

        protected void grdList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void grdList_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            T_NewsDAL _DAL = new T_NewsDAL();
            int _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
            HtmlInputText txtTimexuatban = e.Item.FindControl("txtTimeXB") as HtmlInputText;
            if (txtTimexuatban != null)
            {
                string _where = string.Empty;
                if (!string.IsNullOrEmpty(txtTimexuatban.Value))
                {
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.Parse(txtTimexuatban.Value, new CultureInfo("fr-FR"));
                    }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày tháng phải nhập đúng định dạng dd/MM/yyyy HH:mm !');", true); return; }
                    _where = string.Format(" News_Status = " + ConstNews.NewsPublishingSchedule + ",News_PublishedID = " + _user.UserID + ", News_DatePublished = convert(datetime,'" + dt.ToString("dd/MM/yyyy HH:mm") + "',105) Where News_ID = " + _ID);
                }
                else
                {
                    _where = string.Format(" News_PublishedID = " + _user.UserID + ", News_DatePublished = convert(datetime,null,103) Where News_ID = " + _ID);
                }

                _DAL.UpdateFromT_NewsDynamic(_where);
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _DAL.GetOneFromT_NewsByID(_ID).News_Tittle,
                         Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Đặt lịch xuất bản]", _ID, ConstAction.BaoDT);

            }
            LoadData();
            grdList_CancelCommand(source, e);
            #region Sync
            // DONG BO FILE
            try
            {
                T_News _objSet = new T_News();
                _objSet = _DAL.GetOneFromT_NewsByID(_ID);
                SynFiles _syncfile = new SynFiles();
                if (_objSet.Images_Summary.Length > 0)
                {
                    _syncfile.SynData_UploadImgOne(_objSet.Images_Summary, HPCComponents.Global.ImagesService);
                }

                //Cap nhat anh trong bai viet - vao may dong bo
                if (_objSet.News_Body.Length > 5)
                {
                    _syncfile.SearchImgTag(_objSet.News_Body);
                    _syncfile.SearchTagSwf(_objSet.News_Body);
                    _syncfile.SearchTagFLV(_objSet.News_Body);
                }
            }
            catch (Exception)
            {
                throw;
            }

            //END
            #endregion
        }

        public void grdList_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            grdList.EditItemIndex = -1;
            LoadData();
        }

        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                _obj_T_News = _untilDAL.load_T_news(_ID);
                if (_obj_T_News.News_Lock)
                {
                    if (_obj_T_News.News_EditorID == _user.UserID)
                    {
                        _untilDAL.Update_Status_tintuc(_ID, ConstNews.NewsApproving_tbt, _user.UserID, _obj_T_News.News_DateEdit);
                        //Lock
                        _untilDAL.IsLock(_ID, 1, _user.UserID);
                        Response.Redirect("PublishingEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=0");
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                        return;
                    }
                }
                else
                {
                    _untilDAL.Update_Status_tintuc(_ID, ConstNews.NewsApproving_tbt, _user.UserID, _obj_T_News.News_DateEdit);
                    //Lock
                    _untilDAL.IsLock(_ID, 1, _user.UserID);
                    Response.Redirect("PublishingEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=0");
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
            else if (e.CommandArgument.ToString().ToLower() == "edittime")
            {
                grdList.EditItemIndex = e.Item.ItemIndex;
                LoadData();
            }
        }

        public void DataGrid_henXB_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.DataGrid_henXB.DataKeys[e.Item.ItemIndex].ToString());
                _obj_T_News = _untilDAL.load_T_news(_ID);
                if (_obj_T_News.News_Lock)
                {
                    if (_obj_T_News.News_EditorID == _user.UserID)
                    {
                        _untilDAL.Update_Status_tintuc(_ID, ConstNews.NewsPublishingSchedule, _user.UserID, _obj_T_News.News_DateEdit);
                        //Lock
                        _untilDAL.IsLock(_ID, 1, _user.UserID);
                        Response.Redirect("PublishingEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=1");
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                        return;
                    }
                }
                else
                {
                    _untilDAL.Update_Status_tintuc(_ID, ConstNews.NewsPublishingSchedule, _user.UserID, _obj_T_News.News_DateEdit);
                    //Lock
                    _untilDAL.IsLock(_ID, 1, _user.UserID);
                    Response.Redirect("PublishingEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=1");
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.DataGrid_henXB.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
            if (e.CommandArgument.ToString().ToLower() == "edittime")
            {
                DataGrid_henXB.EditItemIndex = e.Item.ItemIndex;
                LoadDaHenXB();
            }
        }

        protected void DataGrid_henXB_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void DataGrid_henXB_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            T_NewsDAL _DAL = new T_NewsDAL();
            int _ID = Convert.ToInt32(this.DataGrid_henXB.DataKeys[e.Item.ItemIndex].ToString());
            HtmlInputText txtTimexuatban = e.Item.FindControl("txtTimeXB") as HtmlInputText;
            if (txtTimexuatban != null)
            {
                string _where = string.Empty;
                if (!string.IsNullOrEmpty(txtTimexuatban.Value))
                {
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = Convert.ToDateTime(txtTimexuatban.Value, new CultureInfo("fr-FR"));
                    }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày tháng phải nhập đúng định dạng dd/MM/yyyy HH:mm !');", true); return; }
                    _where = string.Format(" News_PublishedID = " + _user.UserID + ", News_DatePublished = convert(datetime,'" + dt.ToString("dd/MM/yyyy HH:mm") + "',105) Where News_ID = " + _ID);
                    _DAL.UpdateFromT_NewsDynamic(_where);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Đặt lịch", Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Đặt lịch xuất bản]", Convert.ToDouble(_ID.ToString()), ConstAction.BaoDT);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn hãy chọn thời gian xuất bản !');", true); return;
                } 
            }

            LoadDaHenXB();
            DataGrid_henXB_CancelCommand(source, e);
            #region Sync
            // DONG BO FILE
            try
            {
                T_News _objSet = new T_News();
                _objSet = _DAL.GetOneFromT_NewsByID(_ID);
                SynFiles _syncfile = new SynFiles();
                if (_objSet.Images_Summary.Length > 0)
                {
                    _syncfile.SynData_UploadImgOne(_objSet.Images_Summary, HPCComponents.Global.ImagesService);
                }

                //Cap nhat anh trong bai viet - vao may dong bo
                if (_objSet.News_Body.Length > 5)
                {
                    _syncfile.SearchImgTag(_objSet.News_Body);
                    _syncfile.SearchTagSwf(_objSet.News_Body);
                    _syncfile.SearchTagFLV(_objSet.News_Body);
                }
            }
            catch (Exception)
            {

                throw;
            }

            //END
            #endregion
        }
        public void DataGrid_henXB_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            DataGrid_henXB.EditItemIndex = -1;
            LoadDaHenXB();
        }
        public void dgr_BaiXoa_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 3)
                    tab = 3;
                double _ID = Convert.ToDouble(dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("PublishingEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
            if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
            else if (e.CommandArgument.ToString().ToLower() == "edittime")
            {
                dgr_BaiXoa.EditItemIndex = e.Item.ItemIndex;
                LoadData_Baibixoa();
            }
        }
        public void dgr_BaiXoa_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            T_NewsDAL _DAL = new T_NewsDAL();
            int _ID = Convert.ToInt32(this.dgr_BaiXoa.DataKeys[e.Item.ItemIndex].ToString());
            //
            T_News _obj_T_News = new T_News();
            _obj_T_News = _DAL.load_T_news(_ID);
            _DAL.Update_Status_tintuc(_ID, ConstNews.NewsApproving_tbt, _user.UserID, _obj_T_News.News_DateEdit);
            //
            HtmlInputText txtTimexuatban = e.Item.FindControl("txtTimeXB") as HtmlInputText;
            if (txtTimexuatban != null)
            {
                string _where = string.Empty;
                if (!string.IsNullOrEmpty(txtTimexuatban.Value))
                {
                    DateTime dt = new DateTime();
                    try
                    {
                        dt = DateTime.Parse(txtTimexuatban.Value, new CultureInfo("fr-FR"));
                    }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày tháng phải nhập đúng định dạng dd/MM/yyyy HH:mm !');", true); return; }
                    _where = string.Format(" News_Status = " + ConstNews.NewsPublishingSchedule + ", News_PublishedID = " + _user.UserID + ", News_DatePublished = convert(datetime,'" + dt.ToString("dd/MM/yyyy HH:mm") + "',105) Where News_ID = " + _ID);
                }
                else
                {
                    _where = string.Format(" News_PublishedID = " + _user.UserID + ", News_DatePublished = convert(datetime,null,103) Where News_ID = " + _ID);
                }

                _DAL.UpdateFromT_NewsDynamic(_where);
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _DAL.GetOneFromT_NewsByID(_ID).News_Tittle,
                         Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài bị xóa] [Đặt lịch xuất bản]", _ID, ConstAction.BaoDT);

            }
            LoadData_Baibixoa();
            dgr_BaiXoa_CancelCommand(source, e);
            #region Sync
            // DONG BO FILE
            try
            {
                T_News _objSet = new T_News();
                _objSet = _DAL.GetOneFromT_NewsByID(_ID);
                SynFiles _syncfile = new SynFiles();
                if (_objSet.Images_Summary.Length > 0)
                {
                    _syncfile.SynData_UploadImgOne(_objSet.Images_Summary, HPCComponents.Global.ImagesService);
                }

                //Cap nhat anh trong bai viet - vao may dong bo
                if (_objSet.News_Body.Length > 5)
                {
                    _syncfile.SearchImgTag(_objSet.News_Body);
                    _syncfile.SearchTagSwf(_objSet.News_Body);
                    _syncfile.SearchTagFLV(_objSet.News_Body);
                }
            }
            catch (Exception)
            {
                throw;
            }

            //END
            #endregion
        }
        protected void dgr_BaiXoa_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void dgr_BaiXoa_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            dgr_BaiXoa.EditItemIndex = -1;
            LoadData_Baibixoa();
        }
        #endregion

        #region Methods
        private void LoadData()
        {
            string _where = " 1 = 1 AND News_Status = " + ConstNews.NewsApproving_tbt + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            if (ddlLang.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _where += "AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));
            _where += " Order by T_News.News_DateSend DESC ";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _untilDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, _where);

            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _untilDAL.BindGridT_NewsEditor(pages.PageIndex - i, pages.PageSize, _where);
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            grdList.DataSource = _ds.Tables[0].DefaultView;
            grdList.DataBind(); _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            GetTotal();
        }
        public void LoadDaHenXB()
        {
            string _where = " 1 = 1 AND News_Status = " + ConstNews.NewsPublishingSchedule + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            if (ddlLang.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _where += "AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));
            _where += " Order by T_News.News_DateEdit DESC ";
            Pager_henXB.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_NewsEditor(Pager_henXB.PageIndex, Pager_henXB.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _untilDAL.BindGridT_NewsEditor(Pager_henXB.PageIndex - 1, Pager_henXB.PageSize, _where);

            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _untilDAL.BindGridT_NewsEditor(Pager_henXB.PageIndex - i, Pager_henXB.PageSize, _where);
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            DataGrid_henXB.DataSource = _ds.Tables[0].DefaultView;
            DataGrid_henXB.DataBind(); _ds.Clear();
            Pager_henXB.TotalRecords = CurrentPage_henXB.TotalRecords = TotalRecords;
            CurrentPage_henXB.TotalPages = Pager_henXB.CalculateTotalPages();
            CurrentPage_henXB.PageIndex = Pager_henXB.PageIndex;
            GetTotal();
        }

        private void LoadDataBaidaxuly()
        {
            string _where = " 1=1 AND T_NewsVersion.News_Status = " + ConstNews.NewsAppro_tbt + " AND T_NewsVersion.News_EditorID = " + _user.UserID;
            if (ddlLang.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_NewsVersion.Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _where += string.Format(" AND T_NewsVersion.CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _where += " AND " + string.Format(" T_NewsVersion.News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));
            _where += " Order by T_NewsVersion.News_DateEdit DESC ";
            PagerTwo.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(PagerTwo.PageIndex, PagerTwo.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(PagerTwo.PageIndex - 1, PagerTwo.PageSize, _where);

            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _T_newsDAL.Bin_T_NewsVersionDynamic(PagerTwo.PageIndex - i, PagerTwo.PageSize, _where);
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            dgBaiDaXyLy.DataSource = _ds;
            dgBaiDaXyLy.DataBind();
            _ds.Clear();
            PagerTwo.TotalRecords = CurrentPageTwo.TotalRecords = TotalRecords;
            CurrentPageTwo.TotalPages = PagerTwo.CalculateTotalPages();
            CurrentPageTwo.PageIndex = PagerTwo.PageIndex;
            GetTotal();
        }

        private void LoadData_Baibixoa()
        {
            //string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            //pageBaixoa.PageSize = Global.MembersPerPage;
            //HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            //DataSet _ds;
            //_ds = _T_newsDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex, pageBaixoa.PageSize, BuildSQL(ConstNews.NewsDelete, sOrder));
            //int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            //int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //{
            //    for (int i = 1; i <= TotalRecords; i++)
            //    {
            //        _ds = _T_newsDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex - i, pageBaixoa.PageSize, BuildSQL(ConstNews.NewsDelete, sOrder));
            //        if (_ds.Tables[0].Rows.Count > 0)
            //            break;
            //    }
            //}

            //dgr_BaiXoa.DataSource = _ds;
            //dgr_BaiXoa.DataBind(); _ds.Clear();
            //pageBaixoa.TotalRecords = CurrentPageBaixoa.TotalRecords = TotalRecords;
            //CurrentPageBaixoa.TotalPages = pageBaixoa.CalculateTotalPages();
            //CurrentPageBaixoa.PageIndex = pageBaixoa.PageIndex;
            //GetTotal();
            string _where = " 1 = 1 ";
            if (TabContainer1.ActiveTabIndex == 3)
            {
                _where += " AND News_EditorID =" + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
            }
            _where += " AND News_Status = " + ConstNews.NewsDelete + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            if (ddlLang.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _where += "AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));
            _where += "Order by T_News.News_DateEdit DESC ";
            pageBaixoa.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex, pageBaixoa.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
            {
                for (int i = 1; i <= TotalRecords; i++)
                {
                    _ds = _untilDAL.BindGridT_NewsEditor(pageBaixoa.PageIndex - i, pageBaixoa.PageSize, _where);
                    if (_ds.Tables[0].Rows.Count > 0)
                        break;
                }
            }

            dgr_BaiXoa.DataSource = _ds.Tables[0].DefaultView;
            dgr_BaiXoa.DataBind(); _ds.Clear();
            pageBaixoa.TotalRecords = CurrentPageBaixoa.TotalRecords = TotalRecords;
            CurrentPageBaixoa.TotalPages = pageBaixoa.CalculateTotalPages();
            CurrentPageBaixoa.PageIndex = pageBaixoa.PageIndex;
            GetTotal();
        }
        #region Get total record from T_News and T_NewsVersion
        private void GetTotal()
        {
            string _dsDangCho, _dsDaXuLy, _dsDahenXB, _dsBaiBiXoa;
            _dsDangCho = UltilFunc.GetTotalCountStatus(WhereCondition(0), "CMS_CountListT_News").ToString();
            _dsDahenXB = UltilFunc.GetTotalCountStatus(WhereCondition(1), "CMS_CountListT_News").ToString();
            _dsDaXuLy = UltilFunc.GetTotalCountStatus(WhereCondition(2), "CMS_CountListT_NewsVersion").ToString();
            _dsBaiBiXoa = UltilFunc.GetTotalCountStatus(WhereCondition(3), "CMS_CountListT_News").ToString();
            _dsDangCho = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinchoxuatban") + " (" + _dsDangCho + ")";
            _dsDahenXB = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinhengio") + " (" + _dsDahenXB + ")";
            _dsDaXuLy = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindagui") + " (" + _dsDaXuLy + ")";
            _dsBaiBiXoa = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindaxoa") + " (" + _dsBaiBiXoa + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangCho + "','" + _dsDahenXB + "','" + _dsDaXuLy + "','" + _dsBaiBiXoa + "');", true);
        }
        #endregion
        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            if (status == 0)
                _whereNews += " AND News_Status = " + ConstNews.NewsApproving_tbt + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            else if (status == 1)
                _whereNews += " AND News_Status = " + ConstNews.NewsPublishingSchedule + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            else if (status == 2)
                _whereNews += " AND News_Status = " + ConstNews.NewsAppro_tbt + "  AND T_NewsVersion.News_EditorID = " + _user.UserID;
            else if (status == 3)
            {
                _whereNews += " AND News_EditorID =" + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID;
                _whereNews += " AND News_Status =  " + ConstNews.NewsDelete + " AND CAT_ID IN (SELECT distinct(tc.Ma_chuyenmuc) FROM T_Nguoidung_Chuyenmuc tc WHERE tc.[Ma_Nguoidung] = " + _user.UserID + ")";
            }
            if (ddlLang.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _whereNews += " AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));

            _whereNews += " Order by News_DateEdit DESC ";
            return _whereNews;
        }
        private void LinkDelete()
        {
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in this.grdList.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        //dung them vao
                        T_News _obj_T_News = new T_News();
                        _obj_T_News = _untilDAL.load_T_news(Convert.ToInt32(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                double News_ID = double.Parse(grdList.DataKeys[m_Item.ItemIndex].ToString());
                                _untilDAL.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                  Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            double News_ID = double.Parse(grdList.DataKeys[m_Item.ItemIndex].ToString());
                            _untilDAL.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                              Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
                        }
                    }
                }
                this.LoadData();
            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in this.dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        //dung them vao
                        T_News _obj_T_News = new T_News();
                        _obj_T_News = _untilDAL.load_T_news(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        double News_ID = double.Parse(dgr_BaiXoa.DataKeys[m_Item.ItemIndex].ToString());
                        _untilDAL.Update_Status_tintuc(News_ID, 55, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                          Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài bị xóa] [Xóa bài viết]", News_ID, ConstAction.BaoDT);
                    }
                }
                this.LoadData_Baibixoa();
            }
        }
        private void LinkGuiDuyet()
        {
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in this.grdList.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        //dung them vao
                        T_News _obj_T_News = new T_News();
                        _obj_T_News = _untilDAL.GetOneFromT_NewsByID(Convert.ToInt32(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        bool _CheckUnlockAndLockAction = true;
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                                _CheckUnlockAndLockAction = true;
                            else
                                _CheckUnlockAndLockAction = false;
                        }
                        else _CheckUnlockAndLockAction = true;
                        if (_CheckUnlockAndLockAction)
                        {
                            // dung them vao de Unlock truoc khi send
                            _untilDAL.IsLock(double.Parse(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);
                            //HPCInfo.T_News _obj = new HPCInfo.T_News();
                            //_obj = _untilDAL.GetOneFromT_NewsByID(_obj_T_News.News_ID);
                            if (_untilDAL.Get_NewsVersion(int.Parse(_obj_T_News.News_ID.ToString()), 9, 73) == true)
                                _untilDAL.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsPublishing, _user.UserID, _obj_T_News.News_DatePublished);
                            else
                                _untilDAL.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsPublishing, _user.UserID, DateTime.Now);

                            _untilDAL.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsPublishing, _user.UserID);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                      Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Xuất bản bài viết]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            #region Sync
                            // DONG BO FILE
                            try
                            {
                                SynFiles _syncfile = new SynFiles();
                                if (_obj_T_News.Images_Summary.Length > 0)
                                {
                                    _syncfile.SynData_UploadImgOne(_obj_T_News.Images_Summary, Global.ImagesService);
                                }

                                //Cap nhat anh trong bai viet - vao may dong bo
                                if (_obj_T_News.News_Body.Length > 5)
                                {
                                    _syncfile.SearchImgTag(_obj_T_News.News_Body);
                                    _syncfile.SearchTagSwf(_obj_T_News.News_Body);
                                    _syncfile.SearchTagFLV(_obj_T_News.News_Body);
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                            //END
                            #endregion
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                            return;
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in this.DataGrid_henXB.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        //dung them vao
                        T_News _obj = new T_News();
                        double _newsid = Convert.ToDouble(DataGrid_henXB.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _obj = _untilDAL.GetOneFromT_NewsByID(_newsid);
                        bool _CheckUnlockAndLockAction = true;
                        if (_obj.News_Lock)
                        {
                            if (_obj.News_EditorID == _user.UserID)
                                _CheckUnlockAndLockAction = true;
                            else
                                _CheckUnlockAndLockAction = false;
                        }
                        else
                            _CheckUnlockAndLockAction = true;
                        if (_CheckUnlockAndLockAction)
                        {
                            //dung them vao de Unlock truoc khi send
                            _untilDAL.IsLock(_newsid, 0);
                            if (_untilDAL.Get_NewsVersion(int.Parse(_obj.News_ID.ToString()), 9, 73) == true)
                                _untilDAL.Update_Status_tintuc(_obj.News_ID, ConstNews.NewsPublishing, _user.UserID, _obj.News_DatePublished);
                            else
                                _untilDAL.Update_Status_tintuc(_obj.News_ID, ConstNews.NewsPublishing, _user.UserID, DateTime.Now);

                            _untilDAL.Insert_Version_From_T_News_WithUserModify(_obj.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsPublishing, _user.UserID);
                            // DONg BO FILE

                            // END

                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj.News_Tittle,
                                      Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản đặt lịch] [Xuất bản bài viết]", _obj.News_ID, ConstAction.BaoDT);
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                            return;
                        }
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in this.dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        //dung them vao
                        T_News _obj_T_News = new T_News();
                        _obj_T_News = _untilDAL.GetOneFromT_NewsByID(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_untilDAL.Get_NewsVersion(int.Parse(_obj_T_News.News_ID.ToString()), 9, 73) == true)
                            _untilDAL.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsPublishing, _user.UserID, _obj_T_News.News_DatePublished);
                        else
                            _untilDAL.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsPublishing, _user.UserID, DateTime.Now);

                        _untilDAL.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsPublishing, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                  Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài bị xóa] [Xuất bản bài viết]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        #region Sync
                        // DONG BO FILE
                        try
                        {
                            SynFiles _syncfile = new SynFiles();
                            if (_obj_T_News.Images_Summary.Length > 0)
                            {
                                _syncfile.SynData_UploadImgOne(_obj_T_News.Images_Summary, Global.ImagesService);
                            }

                            //Cap nhat anh trong bai viet - vao may dong bo
                            if (_obj_T_News.News_Body.Length > 5)
                            {
                                _syncfile.SearchImgTag(_obj_T_News.News_Body);
                                _syncfile.SearchTagSwf(_obj_T_News.News_Body);
                                _syncfile.SearchTagFLV(_obj_T_News.News_Body);
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        //END
                        #endregion
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                this.LoadData();
            if (TabContainer1.ActiveTabIndex == 1)
                LoadDaHenXB();
            if (TabContainer1.ActiveTabIndex == 3)
                LoadData_Baibixoa();
        }
        private void TraLai()
        {
            T_NewsDAL tt = new T_NewsDAL();
            T_News _obj_T_News = new T_News();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in grdList.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                //if (_obj_T_News.Lang_ID == 1)
                                //{
                                //    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                                //    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tk, _user.UserID);
                                //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                //        Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Trả lại Trình bày tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                                //}
                                //else
                                //{
                                    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                                    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tb, _user.UserID);
                                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                        Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Trả lại Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                                //}
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            //if (_obj_T_News.Lang_ID == 1)
                            //{
                            //    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                            //    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tk, _user.UserID);
                            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            //    Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Trả lại Trình bày tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            //}
                            //else {
                                tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                                tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tb, _user.UserID);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài chờ xuất bản] [Trả lại Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            //}
                        }
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in DataGrid_henXB.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(DataGrid_henXB.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        if (_obj_T_News.News_Lock)
                        {
                            if (_obj_T_News.News_EditorID == _user.UserID)
                            {
                                //if (_obj_T_News.Lang_ID == 1)
                                //{
                                //    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                                //    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tk, _user.UserID);
                                //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                //    Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Duyệt tin bài đặt lịch] [Trả lại Trình bày tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                                //}
                                //else
                                //{
                                    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                                    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tb, _user.UserID);
                                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                    Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Duyệt tin bài đặt lịch] [Trả lại Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                                //}
                            }
                            else
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bài đang có người làm việc!');", true);
                                return;
                            }
                        }
                        else
                        {
                            //if (_obj_T_News.Lang_ID == 1)
                            //{
                            //    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                            //    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tk, _user.UserID);
                            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                            //    Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Duyệt tin bài đặt lịch] [Trả lại Trình bày tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            //}
                            //else
                            //{
                                tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                                tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tb, _user.UserID);
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Duyệt tin bài đặt lịch] [Trả lại Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                            //}
                        }
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                foreach (DataGridItem m_Item in dgr_BaiXoa.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj_T_News = tt.load_T_news(Convert.ToInt32(dgr_BaiXoa.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                        //if (_obj_T_News.Lang_ID == 1)
                        //{
                        //    tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tk, _user.UserID, DateTime.Now);
                        //    tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tk, _user.UserID);
                        //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                        //        Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài bị xóa] [Trả lại Trình bày tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                        //}
                        //else
                        //{
                            tt.Update_Status_tintuc(_obj_T_News.News_ID, ConstNews.NewsReturn_tb, _user.UserID, DateTime.Now);
                            tt.Insert_Version_From_T_News_WithUserModify(_obj_T_News.News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsReturn_tb, _user.UserID);
                            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _obj_T_News.News_Tittle,
                                Request["Menu_ID"].ToString(), "[Duyệt tin bài] [Bài bị xóa] [Trả lại Biên tập tin bài]", _obj_T_News.News_ID, ConstAction.BaoDT);
                       // }
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            if (TabContainer1.ActiveTabIndex == 1)
                LoadDaHenXB();
            if (TabContainer1.ActiveTabIndex == 3)
                LoadData_Baibixoa();
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (ddlLang.Items.Count >= 3)
            {
                ddlLang.SelectedIndex = Global.DefaultLangID;
            }
            else
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(ddlCategorysAll, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(ddlCategorysAll, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " and Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        public string Visible_CountImage(string countImage, string prmNews_Lock, string prmEditorID)
        {
            int count = 0;
            string _visible = "None";
            try
            {
                count = int.Parse(countImage);
            }
            catch { ;}
            //if (UltilFunc.IsEnable(_Role.R_Edit, prmNews_Lock, prmEditorID, _user.UserID) && count > 0)
            //    _visible = "Display";
            return _visible;
        }

        #endregion

        #region LoadFile Doc
        private void LoadFileDoc(int _ID)
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
            strFileName = _user.UserName + "_BaiChoDuyet_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            Page.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }
        #endregion END

        #region LOCK
        /*Set image*/
        private void LockedAction(int prmNewsID, int prmIsLock)
        {
            try
            {
                new T_NewsDAL().IsLock(prmNewsID, prmIsLock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion End
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
                foreach (DataGridItem m_Item in grdList.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            T_NewsDAL tt = new T_NewsDAL();
            NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
            T_NgonNgu _obj = new T_NgonNgu();

            LoadData();

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
                                        if (TabContainer1.ActiveTabIndex == 0)
                                            tt.Update_Status_tintuc(News_ID, ConstNews.NewsApproving_tbt, _user.UserID, DateTime.Now);
                                        tt.Insert_Version_From_T_News_WithUserModify(News_ID, ConstNews.NewsAppro_tbt, ConstNews.NewsApproving_tbt, _user.UserID);
                                    }
                                    else
                                    {
                                        HPCShareDLL.HPCDataProvider.Instance().Insert_T_NewsChuyenDe_From_T_NewsChuyenDe(int.Parse(News_ID.ToString()), int.Parse(News_ID.ToString()), Lang_ID, ConstNews.NewsApproving_tbt, _user.UserID, DateTime.Now);
                                        tt.Insert_Version_From_T_News_WithLanquage(News_ID, ConstNews.NewsApproving_tbt, ConstNews.NewsApproving_tbt, _user.UserID, Lang_ID, DateTime.Now);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn chưa chọn mục cần dịch ngữ!');", true);
                return;
            }
            ModalPopupExtender1.Hide();
            LoadData();
            Response.Redirect("~/BaoDienTu/ArticleApproveListTB.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + TabContainer1.ActiveTabIndex);
        }
        #endregion
    }
}
