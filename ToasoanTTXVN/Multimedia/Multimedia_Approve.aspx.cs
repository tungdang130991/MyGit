using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCBusinessLogic;
using HPCComponents;
using HPCBusinessLogic.DAL;
using ToasoanTTXVN.BaoDienTu;
using HPCInfo;

namespace ToasoanTTXVN.Multimedia
{
    public partial class Multimedia_Approve : BasePage
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
                    ActiverPermisstion();
                    if (!IsPostBack)
                    {
                        int tab = 0;
                        if (Request["TabID"] != null && !string.IsNullOrEmpty(Request["TabID"].ToString()))
                        {
                            if (UltilFunc.IsNumeric(Request["TabID"]))
                            {
                                tab = int.Parse(Request["TabID"].ToString());                                
                            }
                        }
                        LoadComboBox();
                        TabContainer1.ActiveTabIndex = tab;
                        TabContainer1_ActiveTabChanged(sender, e);
                    }
                }
            }
        }
        #region Methods

        protected void ActiverPermisstion()
        {
            this.btnReturnOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuontralai") + "','ctl00_MainContent_TabContainer1_tabChoduyet_dgData_ChoXuatban_ctl01_chkAll');");
            this.btnXuatBanOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonXB") + "','ctl00_MainContent_TabContainer1_tabChoduyet_dgData_ChoXuatban_ctl01_chkAll');");
            this.btnXoaOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabChoduyet_dgData_ChoXuatban_ctl01_chkAll');");
            //this.btnTranslateOn.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn dịch ngữ?','ctl00_MainContent_TabContainer1_TabPanel_choduyet_DataGrid_Choduyet_ctl01_chkAll');");
        }
        protected string ReturnObjectDisplay(object objPath, object objimg, object _ID)
        {
            string _extend = "";
            string _return = "";
            if (objPath.ToString().StartsWith("<iframe"))
            {
                try
                {
                    string _youReziWith = UltilFunc.ReplapceYoutoubeWidth(objPath.ToString(), "200");
                    string _youReziHeigh = UltilFunc.ReplapceYoutoubeHight(_youReziWith.ToString(), "180");
                    _return = _youReziHeigh.ToString();
                }
                catch
                { _return = ""; }
            }
            else
            {
                _extend = System.IO.Path.GetExtension(objPath.ToString().Split('/').GetValue(objPath.ToString().Split('/').Length - 1).ToString().Trim());
                if (_extend.ToLower() == ".jpg" || _extend.ToLower() == ".png" || _extend.ToLower() == ".gif" || _extend.ToLower() == ".jpeg" || _extend.ToLower() == ".bmp")
                {
                    _return = "<img style=\"max-width:180px;max-height:180px;\"  src=\"" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "\" alt=\"View\" onclick=\"openNewImage(this,'Close');\" />";
                }
                else if (_extend.ToLower() == ".flv" || _extend.ToLower() == ".wmv" || _extend.ToLower() == ".mp4")
                {
                    //_return = "<img style=\"max-width:180px;\" src=\"" + HPCComponents.Global.TinPathBDT + "/upload/Ads/admin/Icons/ico_video.jpg" + "\" alt=\"View\" onclick=\"xemquangcao('"+HPCComponents.Global.TinPathBDT + objPath.ToString() +"','');\" />";
                    string _viewFile = "";
                    _viewFile += "<div id=\"MediaPlayer\">";
                    _viewFile += "<div id=\"liveTV" + _ID + "\"></div>";
                    _viewFile += "<script type=\"text/javascript\">";
                    _viewFile += "jwplayer(\"liveTV" + _ID + "\").setup({";
                    _viewFile += " image: '" + HPCComponents.Global.TinPathBDT + objimg.ToString() + "',";
                    _viewFile += " file: '" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "',";
                    _viewFile += " width:200, height:180,primary: \"flash\"";
                    _viewFile += " });";
                    _viewFile += " </script>";
                    _return = _viewFile.ToString();
                }
                else if (_extend.ToLower() == ".swf")
                {
                    string _viewFile = "";
                    _viewFile += "<style>a:visited{color:blue;text-decoration:none}</style>";
                    _viewFile += "<body topmargin=\"0\" leftmargin=\"0\" marginheight=\"0\" marginwidth=\"0\"><center>";
                    _viewFile += "<div style=\"width:100%;height:100%;overflow:auto;\">";
                    _viewFile += "<embed width=\"200px\" height=\"45px\" type=\"application/x-shockwave-flash\"";
                    _viewFile += "src=\"" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "\" style=\"undefined\" id=\"Advertisement\" name=\"Advertisement\"";
                    _viewFile += "quality=\"high\" wmode=\"transparent\" allowscriptaccess=\"always\" flashvars=\"clickTARGET=_self&amp;clickTAG=#\"></embed></div>";

                    _return = _viewFile.ToString();
                }
                else
                {
                    _return = "<img  src=\"\" alt=\"No Image\" />";
                }

            }
            return _return;

        }
        #endregion

        #region Load Methods

        public void Load_Choxuatban()
        {
            pages_Choxuatban.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(pages_Choxuatban.PageIndex, pages_Choxuatban.PageSize, WhereCondition(0));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(pages_Choxuatban.PageIndex - 1, pages_Choxuatban.PageSize, WhereCondition(0));
            dgData_ChoXuatban.DataSource = _ds.Tables[0];
            dgData_ChoXuatban.DataBind(); _ds.Clear();
            pages_Choxuatban.TotalRecords = CurrentChoxuatban.TotalRecords = TotalRecords;
            CurrentChoxuatban.TotalPages = pages_Choxuatban.CalculateTotalPages();
            CurrentChoxuatban.PageIndex = pages_Choxuatban.PageIndex;
            GetTotal();
        }

        public void Load_XB()
        {
            Pager_Xuatban.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(Pager_Xuatban.PageIndex, Pager_Xuatban.PageSize, WhereCondition(1));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(Pager_Xuatban.PageIndex - 1, Pager_Xuatban.PageSize, WhereCondition(1));
            DataGrid_Daxuatban.DataSource = _ds.Tables[0];
            DataGrid_Daxuatban.DataBind(); _ds.Clear();
            Pager_Xuatban.TotalRecords = CurrentPage_Xuatban.TotalRecords = TotalRecords;
            CurrentPage_Xuatban.TotalPages = Pager_Xuatban.CalculateTotalPages();
            CurrentPage_Xuatban.PageIndex = Pager_Xuatban.PageIndex;
            GetTotal();
        }
        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorys.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                //ddlCategorys.UpdateAfterCallBack = true;
            }
            else
            {
                ddlCategorys.DataSource = null;
                ddlCategorys.DataBind();
                //ddlCategorys.UpdateAfterCallBack = true;
            }
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
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        public void GetTotal()
        {
            #region TONG SO
            string _dsDangchoduyet = UltilFunc.GetTotalCountStatus(WhereCondition(0), "CMS_CountListT_Multimedia").ToString();
            string _dsDadaduyet = UltilFunc.GetTotalCountStatus(WhereCondition(1), "CMS_CountListT_Multimedia").ToString();
            _dsDangchoduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChoduyet") + " (" + _dsDangchoduyet + ")";
            _dsDadaduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblDaXB") + " (" + _dsDadaduyet + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangchoduyet + "','" + _dsDadaduyet + "');", true);
            #endregion
        }
        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            if (ddlLang.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Languages_ID = {0}", ddlLang.SelectedValue);

            if (this.ddlCategorys.SelectedIndex > 0)
                _whereNews += " AND" + string.Format(" Category IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorys.SelectedValue);

            if (txtSearch.Text.Length > 0)
                _whereNews += " AND " + string.Format(" Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (status == 0)
            {
                //_whereNews += " AND Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") and Status = 2 ";
                _whereNews += " and Status = 2 ";
                _whereNews += " Order by T_Multimedia.DateModify DESC";
            }
            if (status == 1)
            {
                //_whereNews += " AND Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") and Status = 3 and UserModify=" + _user.UserID.ToString() + " ";
                _whereNews += " and Status = 3 and UserModify=" + _user.UserID.ToString() + " ";
                _whereNews += " Order by T_Multimedia.DatePublish DESC";
            }
            return _whereNews;
        }

        #endregion

        #region Event Handle

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());//+ "&ID=" +0
        }

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages_Choxuatban.PageIndex = 0;
                Load_Choxuatban();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                Pager_Xuatban.PageIndex = 0;
                Load_XB();
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
                Load_Choxuatban();
            if (TabContainer1.ActiveTabIndex == 1)
                Load_XB();
        }

        protected void lbt_Xuatban_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in dgData_ChoXuatban.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    try
                    {
                        Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                        int _ID = Convert.ToInt32(this.dgData_ChoXuatban.DataKeys[item.ItemIndex].ToString());
                        _untilDAL.UpdateStatusMultimedia(_ID, 3, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XUẤT BẢN MULTIMEDIA", _ID, ConstAction.AmThanhHinhAnh);
                        #region Sync
                        // DONG BO FILE
                        try
                        {
                            T_Multimedia _objSet = new T_Multimedia();
                            _objSet = _untilDAL.GetOneFromT_MultimediaByID(_ID);
                            SynFiles _syncfile = new SynFiles();
                            if (_objSet.URL_Images.Length > 0)
                            {
                                _syncfile.SynData_UploadImgOne(_objSet.URL_Images, HPCComponents.Global.ImagesService);
                            }
                            if (_objSet.URLPath.Length > 0)
                            {
                                _syncfile.SynData_UploadImgOne(_objSet.URLPath, HPCComponents.Global.ImagesService);
                            }
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        
                        //END
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        //HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
                    }
                }
            }

            Load_Choxuatban();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _untilDAL.getTotalRecord(4, _user.UserID) + "','" + _untilDAL.getTotalRecord(5, _user.UserID) + "');", true);
        }
        protected void lbt_Tralai_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in dgData_ChoXuatban.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check != null && check.Checked)
                {
                    try
                    {
                        Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                        int _ID = Convert.ToInt32(this.dgData_ChoXuatban.DataKeys[item.ItemIndex].ToString());
                        _untilDAL.UpdateStatusMultimedia(_ID, 21, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "TRẢ LẠI MULTIMEDIA", _ID, ConstAction.AmThanhHinhAnh);

                    }
                    catch (Exception ex) { }
                }
            }
            Load_Choxuatban();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _untilDAL.getTotalRecord(4, _user.UserID) + "','" + _untilDAL.getTotalRecord(5, _user.UserID) + "');", true);
        }
        protected void lbt_xoa_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in dgData_ChoXuatban.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check != null && check.Checked)
                {
                    try
                    {
                        int _ID = Convert.ToInt32(this.dgData_ChoXuatban.DataKeys[item.ItemIndex].ToString());
                        Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                        _untilDAL.DeleteFromT_Multimedia(_ID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA", _ID, ConstAction.AmThanhHinhAnh);

                    }
                    catch (Exception ex) { }
                }
            }
            Load_Choxuatban();
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _untilDAL.getTotalRecord(4, _user.UserID) + "','" + _untilDAL.getTotalRecord(5, _user.UserID) + "');", true);
        }
        
        #endregion
        #region Dich Ngu

        protected void link_copy_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                //string listId = "";
                //int count = 0;
                //foreach (DataGridItem item in dgData_ChoXuatban.Items)
                //{
                //    Label lblcatid = (Label)item.FindControl("lblcatid");
                //    CheckBox check = (CheckBox)item.FindControl("optSelect");
                //    if (check.Checked)
                //    {
                //        if (count == 0)
                //        {
                //            listId = lblcatid.Text;
                //        }
                //        else
                //        {
                //            listId += "," + lblcatid.Text;
                //        }
                //        count++;
                //    }
                //}
                //lbl_News_ID.Text = listId;
                LoadCM();
                ModalPopupExtender1.Show();
            }
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
            dgCategorysCopy.DataSource = _dv;
            dgCategorysCopy.DataBind();
        }
        protected void but_Trans_Click(object sender, EventArgs e)
        {
            double _IDVideo = 0.0;
            ArrayList arNgu = new ArrayList();
            foreach (DataGridItem m_Item in this.dgCategorysCopy.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    arNgu.Add(double.Parse(this.dgCategorysCopy.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            ArrayList arrTin = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgData_ChoXuatban.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(dgData_ChoXuatban.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            T_MultimediaDAL _DAL = new T_MultimediaDAL();
            NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
            if (arrTin.Count > 0)
            {
                for (int j = 0; j < arrTin.Count; j++)
                {
                    double Video_ID = double.Parse(arrTin[j].ToString());
                    if (_DAL.GetOneFromT_MultimediaByID(int.Parse(Video_ID.ToString())).Languages_ID == 1)
                    {
                        for (int i = 0; i < arNgu.Count; i++)
                        {
                            //Thực hiện dịch ngữ
                            int Lang_ID = int.Parse(arNgu[i].ToString());
                            if (!HPCShareDLL.HPCDataProvider.Instance().ExitsTranlate_T_Multimedia(int.Parse(Video_ID.ToString()), Lang_ID))
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                                //return;
                            }
                            else
                            {
                                _IDVideo = _DAL.Copy_To_T_Multimedia(int.Parse(Video_ID.ToString()), Lang_ID, 2, DateTime.Now, _user.UserID, _user.UserID);
                                if (_IDVideo > 0)
                                {
                                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Copy]", Request["Menu_ID"], "[Copy] [Media]: [Multimedia chờ xuất bản] [Thao tác copy bài sang chuyên trang: " + UltilFunc.GetTenNgonNgu(Lang_ID) + "]", _IDVideo, ConstAction.AmThanhHinhAnh);
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
                ModalPopupExtender1.Hide();
            } 
            if (_IDVideo > 0)
            {
                ModalPopupExtender1.Hide();
                Load_Choxuatban();
            }
        }
        #endregion
        #region Datagrid Handle

        public void dgData_ChoXuatban_ItemDataBoundEditor(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void dgData_ChoXuatban_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int TabID = TabContainer1.ActiveTabIndex;
                int _ID = Convert.ToInt32(this.dgData_ChoXuatban.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Multimedia_Approve_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&TabID=" + TabID.ToString());
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                try
                {
                    Label lblLogTitle = (Label)e.Item.FindControl("lblLogTitle");
                    int _ID = Convert.ToInt32(this.dgData_ChoXuatban.DataKeys[e.Item.ItemIndex].ToString());
                    _untilDAL.DeleteFromT_Multimedia(_ID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA VỪA MỚI CẬP NHẬT", _ID, ConstAction.AmThanhHinhAnh);
                    Load_Choxuatban();
                }
                catch (Exception ex) { }
            }
        }

        public void DataGrid_Daxuatban_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int TabID = TabContainer1.ActiveTabIndex;
                int _ID = Convert.ToInt32(this.DataGrid_Daxuatban.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Multimedia_Approve_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&TabID=" + TabID.ToString());
            }
        }

        #endregion

        #region PageIndexChange

        protected void pages_Choxuatban_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_Choxuatban();
        }
        protected void Pager_Xuatban_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_XB();
        }
        #endregion
    }
}
