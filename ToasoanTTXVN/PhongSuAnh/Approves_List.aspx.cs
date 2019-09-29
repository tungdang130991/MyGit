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
using HPCBusinessLogic.DAL;
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class Approves_List : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        private double PhotoID
        {
            get { if (ViewState["NewsID"] != null) return Convert.ToDouble(ViewState["NewsID"]); else return 0.0; }

            set { ViewState["NewsID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                Response.Redirect("~/Errors/AccessDenied.aspx");
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
            ActiverPemission();
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
                    try { tabindex = int.Parse(Session["DuyetPS_TabID"].ToString()); }
                    catch { ;}
                    try { text_search = Session["DuyetPS_TenPS"].ToString(); }
                    catch { ;}
                    try { langid = int.Parse(Session["DuyetPS_Langid"].ToString()); }
                    catch { ;}
                    try { pageindex = int.Parse(Session["DuyetPS_pagesindex"].ToString()); }
                    catch { ;}
                    txtSearch_Cate.Text = text_search;
                    cboNgonNgu.SelectedValue = langid.ToString();
                    if (tabindex == 0)
                    {
                        TabContainer1.ActiveTabIndex = 0;
                        Pager_choduyet.PageIndex = pageindex;
                    }
                    else if (tabindex == 1)
                    {
                        TabContainer1.ActiveTabIndex = 1;
                        Pager_HuyXB.PageIndex = pageindex;
                    }
                    else
                    {
                        Pager_choduyet.PageIndex = 0;
                    }
                }
                else
                {
                    Pager_choduyet.PageIndex = 0;
                }
                LoadPSchoduyet();
                LoadPSdaduyet();
                LoadPShuyXB();
            }
        }
        protected void ActiverPemission()
        {
            //this.btnSendXuLyOn.Visible = _Role.R_Pub; 
            this.btnSendXuLyOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn xuất bản?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
            //this.btnSendXuLyBottom.Visible = _Role.R_Pub; 
            this.btnSendXuLyBottom.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn xuất bản?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
            //this.btnReturnXuLyOn.Visible = _Role.R_Pub; 
            this.btnReturnXuLyOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn trả lại?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
            //this.btnReturnXuLyBottom.Visible = _Role.R_Pub; 
            this.btnReturnXuLyBottom.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn trả lại?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
            //this.btnSendTwoOn.Visible = _Role.R_Pub; 
            this.btnSendTwoOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn xuất bản?','ctl00_MainContent_TabContainer1_TabPanel_HuyXB_DataGrid_HuyXB_ctl01_chkAll');");
            //this.btnReturnTwoOn.Visible = _Role.R_Pub; 
            this.btnReturnTwoOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn trả lại?','ctl00_MainContent_TabContainer1_TabPanel_HuyXB_DataGrid_HuyXB_ctl01_chkAll');");
            //this.btnSendTwoBottom.Visible = _Role.R_Pub; 
            this.btnSendTwoBottom.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn xuất bản?','ctl00_MainContent_TabContainer1_TabPanel_HuyXB_DataGrid_HuyXB_ctl01_chkAll');");
            //this.btnReturnTwoBottom.Visible = _Role.R_Pub; 
            this.btnReturnTwoBottom.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn trả lại?','ctl00_MainContent_TabContainer1_TabPanel_HuyXB_DataGrid_HuyXB_ctl01_chkAll');");
            //this.btnTranslateOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn dịch ngữ?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
            //this.btnTranslateBottom.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn dịch ngữ?','ctl00_MainContent_TabContainer1_TabPanel_HuyXB_DataGrid_HuyXB_ctl01_chkAll');");
        }
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    LoadPSchoduyet(); break;
                case 1:
                    LoadPShuyXB(); break;
                case 2:
                    LoadPSdaduyet(); break;
            }
        }

        protected void link_duyet_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem item in DataGrid_Choduyet.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                    {
                        int _id = Convert.ToInt32(lblcatid.Text);
                        T_Album.Update_Status(_id, 4, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xuất bản", Request["Menu_ID"].ToString(), "[Phóng sự ảnh chờ duyệt] [Duyệt Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
                        #region Sync
                        // DONG BO ANH
                        try
                        {
                            SynFiles _syncfile = new SynFiles();
                            T_Album_PhotoDAL _cateDAL = new T_Album_PhotoDAL();
                            DataSet _ds = _cateDAL.Bind_T_Album_Photo(int.Parse(lblcatid.Text));
                            foreach (DataRow theRow in _ds.Tables[0].Rows)
                            {
                                string _img = theRow["Abl_Photo_Origin"].ToString();
                                if (_img.Trim().Length > 0)
                                {
                                    _syncfile.SynData_UploadImgOne(_img, Global.ImagesService);
                                }
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
                LoadPSchoduyet();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem item in DataGrid_HuyXB.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                    {
                        int _id = int.Parse(lblcatid.Text);
                        T_Album.Update_Status(_id, 4, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xuất bản", Request["Menu_ID"].ToString(), "[Phóng sự ảnh hủy xuất bản] [Duyệt Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
                        #region Sync
                        // DONG BO ANH
                        try
                        {
                            SynFiles _syncfile = new SynFiles();
                            T_Album_PhotoDAL _cateDAL = new T_Album_PhotoDAL();
                            DataSet _ds = _cateDAL.Bind_T_Album_Photo(int.Parse(lblcatid.Text));
                            foreach (DataRow theRow in _ds.Tables[0].Rows)
                            {
                                string _img = theRow["Abl_Photo_Origin"].ToString();
                                if (_img.Trim().Length > 0)
                                {
                                    _syncfile.SynData_UploadImgOne(_img, Global.ImagesService);
                                }
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
                LoadPShuyXB();
            }
        }
        protected void link_copy_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadCM();
                ModalPopupExtender1.Show();
            }
            
        }
        protected void dgCategorysCopy_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "editcopy")
            {
                double _IDAlbum = 0.0;
                DataGridItem m_ItemCat = e.Item;
                int Lang_ID = int.Parse(dgCategorysCopy.DataKeys[m_ItemCat.ItemIndex].ToString());
                ArrayList arrTin = new ArrayList();
                foreach (DataGridItem m_Item in DataGrid_Choduyet.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(DataGrid_Choduyet.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
                T_Album_PhotoDAL tt = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
                T_Album_CategoriesDAL _Album_CateDAL = new T_Album_CategoriesDAL();
                NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
                if (arrTin.Count > 0)
                {
                    for (int j = 0; j < arrTin.Count; j++)
                    {
                        double News_ID = double.Parse(arrTin[j].ToString());
                        if (_Album_CateDAL.load_T_Album_Categories(int.Parse(News_ID.ToString())).Lang_ID == 1)
                        {
                            if (!HPCShareDLL.HPCDataProvider.Instance().ExitsTranlate_T_Album_Photo(int.Parse(News_ID.ToString()), Lang_ID))
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                                //return;
                            }
                            else
                            {
                                _IDAlbum = _Album_CateDAL.Copy_To_T_Album_Categories(int.Parse(News_ID.ToString()), Lang_ID, 2, DateTime.Now, _user.UserID, _user.UserID);
                                if (_IDAlbum > 0)
                                {
                                    tt.Copy_To_T_Album_Photo(int.Parse(News_ID.ToString()), int.Parse(_IDAlbum.ToString()), Lang_ID, DateTime.Now, _user.UserID);
                                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Copy]", Request["Menu_ID"], "[Copy] [Phóng sự ảnh]: [Duyệt phóng sự ảnh] [Thao tác copy bài sang chuyên trang: " + UltilFunc.GetTenNgonNgu(Lang_ID) + "]", _IDAlbum, ConstAction.GocAnh);
                                    CheckBox checkcopy = (CheckBox)m_ItemCat.FindControl("optSelect");
                                    ImageButton btnCopy = (ImageButton)m_ItemCat.FindControl("btnCopy");
                                    btnCopy.Visible = false;
                                    checkcopy.Visible = false;
                                    checkcopy.Enabled = false;
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
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn chưa chọn danh mục cần dịch ngữ!');", true);
                    ModalPopupExtender1.Hide();
                }  
                if (_IDAlbum > 0)
                {
                    //ModalPopupExtender1.Hide();
                    this.LoadPSchoduyet();
                }
            }
        }
        #region COPY NEWS CATEGORYS
        public void LoadCM()
        {
            string where = string.Format(" hoatdong=1 AND ID!=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID));
            NgonNgu_DAL _DAL = new NgonNgu_DAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_NgonNgu(0, 5000, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            DataTable _dv = _ds.Tables[0];
            dgCategorysCopy.DataSource = _dv;
            dgCategorysCopy.DataBind();
        }
        protected void but_Trans_Click(object sender, EventArgs e)
        {
            double _IDAlbum = 0.0;
            ArrayList arNgu = new ArrayList();
            foreach (DataGridItem m_Item in dgCategorysCopy.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    arNgu.Add(double.Parse(dgCategorysCopy.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            ArrayList arrTin = new ArrayList();
            foreach (DataGridItem m_Item in DataGrid_Choduyet.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    arrTin.Add(double.Parse(DataGrid_Choduyet.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            T_Album_PhotoDAL tt = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
            T_Album_CategoriesDAL _Album_CateDAL = new T_Album_CategoriesDAL();
            NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
            if (arrTin.Count > 0)
            {
                for (int j = 0; j < arrTin.Count; j++)
                {
                    double News_ID = double.Parse(arrTin[j].ToString());
                    if (_Album_CateDAL.load_T_Album_Categories(int.Parse(News_ID.ToString())).Lang_ID == 1)
                    {
                        for (int i = 0; i < arNgu.Count; i++)
                        {
                            //Thực hiện dịch ngữ
                            int Lang_ID = int.Parse(arNgu[i].ToString());
                            if (!HPCShareDLL.HPCDataProvider.Instance().ExitsTranlate_T_Album_Photo(int.Parse(News_ID.ToString()), Lang_ID))
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                                //return;
                            }
                            else
                            {
                                _IDAlbum = _Album_CateDAL.Copy_To_T_Album_Categories(int.Parse(News_ID.ToString()), Lang_ID, 2, DateTime.Now, _user.UserID, _user.UserID);
                                if (_IDAlbum > 0)
                                {
                                    tt.Copy_To_T_Album_Photo(int.Parse(News_ID.ToString()), int.Parse(_IDAlbum.ToString()), Lang_ID, DateTime.Now, _user.UserID);
                                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Copy]", Request["Menu_ID"], "[Copy] [Phóng sự ảnh]: [Duyệt Phóng sự ảnh] [Thao tác copy bài sang chuyên trang: " + UltilFunc.GetTenNgonNgu(Lang_ID) + "]", _IDAlbum, ConstAction.GocAnh);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhandich") + "');", true);
            if (_IDAlbum > 0)
            {
                ModalPopupExtender1.Hide();
                this.LoadPSchoduyet();
            }
            ////Tao cache
            ////UltilFunc.GenCacheHTML();
            
        }
        #endregion END
        protected void link_tralai_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem item in DataGrid_Choduyet.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    int _id = int.Parse(lblcatid.Text);
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                        T_Album.Update_Status(_id, 3, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Trả lại", Request["Menu_ID"].ToString(), "[Phóng sự ảnh chờ duyệt] [Trả lại Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
                }
                LoadPSchoduyet();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem item in DataGrid_HuyXB.Items)
                {
                    Label lblcatid = (Label)item.FindControl("lblcatid");
                    int _id = int.Parse(lblcatid.Text);
                    CheckBox check = (CheckBox)item.FindControl("optSelect");
                    if (check.Checked)
                        T_Album.Update_Status(_id, 3, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Trả lại", Request["Menu_ID"].ToString(), "[Phóng sự ảnh hủy xuất bản] [Trả lại Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
                }
                LoadPShuyXB();
            }
        }

        protected void Pager_choduyet_IndexChanged(object sender, EventArgs e)
        {
            LoadPSchoduyet();
        }

        protected void Pager_HuyXB_IndexChanged(object sender, EventArgs e)
        {
            LoadPShuyXB();
        }

        protected void Pager_daduyet_IndexChanged(object sender, EventArgs e)
        {
            LoadPSdaduyet();
        }

        public void DataGrid_Choduyet_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.DataGrid_Choduyet.DataKeys[e.Item.ItemIndex].ToString());
                Session["DuyetPS_pagesindex"] = Pager_choduyet.PageIndex;
                Session["DuyetPS_TabID"] = 0;
                Session["DuyetPS_Langid"] = cboNgonNgu.SelectedValue;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["DuyetPS_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 2;
                Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }

            else if (e.CommandArgument.ToString().ToLower() == "inputphoto")
            {
                int catID = Convert.ToInt32(this.DataGrid_Choduyet.DataKeys[e.Item.ItemIndex].ToString());
                Session["DuyetPS_pagesindex"] = Pager_choduyet.PageIndex;
                Session["DuyetPS_TabID"] = 0;
                Session["DuyetPS_Langid"] = cboNgonNgu.SelectedValue;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["DuyetPS_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 2;
                Response.Redirect("~/PhongSuAnh/PhotoAlbum_EditMullti.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }

        public void DataGrid_HuyXB_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.DataGrid_HuyXB.DataKeys[e.Item.ItemIndex].ToString());
                Session["DuyetPS_pagesindex"] = Pager_HuyXB.PageIndex;
                Session["DuyetPS_Langid"] = cboNgonNgu.SelectedValue;
                Session["DuyetPS_TabID"] = 1;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["DuyetPS_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 2;
                Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
            else if (e.CommandArgument.ToString().ToLower() == "inputphoto")
            {
                int catID = Convert.ToInt32(this.DataGrid_HuyXB.DataKeys[e.Item.ItemIndex].ToString());
                Session["DuyetPS_pagesindex"] = Pager_HuyXB.PageIndex;
                Session["DuyetPS_TabID"] = 1;
                Session["DuyetPS_Langid"] = cboNgonNgu.SelectedValue;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["DuyetPS_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 2;
                Response.Redirect("~/PhongSuAnh/PhotoAlbum_EditMullti.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    LoadPSchoduyet(); break;
                case 1:
                    LoadPShuyXB(); break;
                case 2:
                    LoadPSdaduyet(); break;
            }
        }

        public void LoadPSchoduyet()
        {
            Pager_choduyet.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_choduyet.PageIndex, Pager_choduyet.PageSize, WhereCondition(0));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_choduyet.PageIndex - 1, Pager_choduyet.PageSize, WhereCondition(0));
            DataGrid_Choduyet.DataSource = _ds;
            DataGrid_Choduyet.DataBind();
            Pager_choduyet.TotalRecords = curentPages_choduyet.TotalRecords = TotalRecords;
            curentPages_choduyet.TotalPages = Pager_choduyet.CalculateTotalPages();
            curentPages_choduyet.PageIndex = Pager_choduyet.PageIndex;
            GetTotal();
            //TabPanel_choduyet.HeaderText = "Phóng sự ảnh chờ duyệt ("+TotalRecords.ToString() + ")";
            foreach (DataGridItem item in DataGrid_Choduyet.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        public void LoadPShuyXB()
        {
            Pager_HuyXB.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_HuyXB.PageIndex, Pager_HuyXB.PageSize, WhereCondition(1));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_HuyXB.PageIndex - 1, Pager_HuyXB.PageSize, WhereCondition(1));
            DataGrid_HuyXB.DataSource = _ds;
            DataGrid_HuyXB.DataBind();
            Pager_HuyXB.TotalRecords = CurrentPage_HuyXB.TotalRecords = TotalRecords;
            CurrentPage_HuyXB.TotalPages = Pager_HuyXB.CalculateTotalPages();
            CurrentPage_HuyXB.PageIndex = Pager_HuyXB.PageIndex;
            GetTotal();
            foreach (DataGridItem item in DataGrid_HuyXB.Items)
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
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_daduyet.PageIndex, Pager_daduyet.PageSize, WhereCondition(2));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_daduyet.PageIndex - 1, Pager_daduyet.PageSize, WhereCondition(2));
            DataGrid_daduyet.DataSource = _ds;
            DataGrid_daduyet.DataBind(); _ds.Clear();
            Pager_daduyet.TotalRecords = CurrentPage_daduyet.TotalRecords = TotalRecords;
            CurrentPage_daduyet.TotalPages = Pager_daduyet.CalculateTotalPages();
            CurrentPage_daduyet.PageIndex = Pager_daduyet.PageIndex;
            GetTotal();
            foreach (DataGridItem item in DataGrid_daduyet.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        private void LoadComboBox()
        {
            cboNgonNgu.Items.Clear();
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
                cboNgonNgu.SelectedIndex = Global.DefaultLangID;
            else cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
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

        protected string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            return strReturn;
        }
        #region TONG SO
        public void GetTotal()
        {
            string _dsDangchoduyet, _dsDadaduyet, _dsDatralai;
            _dsDangchoduyet = UltilFunc.GetTotalCountStatus(WhereCondition(0), "CMS_CountListT_Album_Categories").ToString();
            _dsDatralai = UltilFunc.GetTotalCountStatus(WhereCondition(1), "CMS_CountListT_Album_Categories").ToString();
            _dsDadaduyet = UltilFunc.GetTotalCountStatus(WhereCondition(2), "CMS_CountListT_Album_Categories").ToString();
            
            _dsDangchoduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhchoduyet") + " (" + _dsDangchoduyet + ")";
            _dsDatralai = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhhuyXB") + " (" + _dsDatralai + ")";
            _dsDadaduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblGocanhXB") + " (" + _dsDadaduyet + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangchoduyet + "','" + _dsDatralai + "','" + _dsDadaduyet + "');", true);
        }
        #endregion
        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            switch (status)
            {
                case 0:
                    _whereNews += " and Cat_Album_Status_Approve = 2"; // AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                case 1:
                    _whereNews += " and Cat_Album_Status_Approve = 5"; // AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
                case 2:
                    _whereNews += " and Cat_Album_Status_Approve = 4"; // AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
                    break;
            }
            if (cboNgonNgu.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Lang_ID = {0}", cboNgonNgu.SelectedValue);
            if (txtSearch_Cate.Text.Length > 0)
                _whereNews += " AND " + string.Format(" Cat_Album_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            _whereNews += " Order by Cat_Album_ID DESC";
            return _whereNews;
        }

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
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion end
    }
}
