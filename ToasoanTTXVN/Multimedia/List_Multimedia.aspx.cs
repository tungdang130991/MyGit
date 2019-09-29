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

namespace ToasoanTTXVN.Multimedia
{
    public partial class List_Multimedia : BasePage
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
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"]))) Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    ActivePermission();
                    if (!IsPostBack)
                    {
                        int tab = 0;
                        if (Request["TabID"] != null)
                        {
                            if (UltilFunc.IsNumeric(Request["TabID"]))
                            {
                                tab = Convert.ToInt32(Request["TabID"].ToString());
                            }
                        }
                        LoadComboBox();
                        TabContainer1.ActiveTabIndex = tab;
                        TabContainer1_ActiveTabChanged(null, null);
                    }
                }
            }
        }
        #region Methods
        protected void ActivePermission()
        {
            this.btnGuiDuyetOnXuLy.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_dgDataEditor_ctl01_chkAll');");
            this.btnDeleteOnXuLy.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_dgDataEditor_ctl01_chkAll');");
            this.btnGuiDuyetTraLaiOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_TabPanel1_DataGrid_Tralai_ctl01_chkAll');");
            this.btnReturnXoaOn.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonxoa") + "','ctl00_MainContent_TabContainer1_TabPanel1_DataGrid_Tralai_ctl01_chkAll');");
        }


        protected string ReturnObjectDisplay(object objPath, object objimg, object _ID)
        {
            string _extend = string.Empty;
            string _return = string.Empty;
            if (objPath.ToString().StartsWith("<iframe"))
            {
                try
                {
                    string _youReziWith = UltilFunc.ReplapceYoutoubeWidth(objPath.ToString(), "200");
                    string _youReziHeigh = UltilFunc.ReplapceYoutoubeHight(_youReziWith.ToString(), "180");
                    _return = _youReziHeigh.ToString();
                }
                catch
                { _return = string.Empty; }
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
        private void LoadData()
        {
            pagesEditor.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(pagesEditor.PageIndex, pagesEditor.PageSize, WhereCondition(0));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(pagesEditor.PageIndex - 1, pagesEditor.PageSize, WhereCondition(0));
            dgDataEditor.DataSource = _ds.Tables[0];
            dgDataEditor.DataBind(); _ds.Clear();
            pagesEditor.TotalRecords = CurrentPageEditor.TotalRecords = TotalRecords;
            CurrentPageEditor.TotalPages = pagesEditor.CalculateTotalPages();
            CurrentPageEditor.PageIndex = pagesEditor.PageIndex;
            GetTotal();
        }
        
        public void Load_Tralai()
        {
            Pager_Tralai.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(Pager_Tralai.PageIndex, Pager_Tralai.PageSize, WhereCondition(2));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(Pager_Tralai.PageIndex - 1, Pager_Tralai.PageSize, WhereCondition(2));
            DataGrid_Tralai.DataSource = _ds.Tables[0];
            DataGrid_Tralai.DataBind(); _ds.Clear();
            Pager_Tralai.TotalRecords = CurrentPage_Tralai.TotalRecords = TotalRecords;
            CurrentPage_Tralai.TotalPages = Pager_Tralai.CalculateTotalPages();
            CurrentPage_Tralai.PageIndex = Pager_Tralai.PageIndex;
            GetTotal();
        }
        public void Load_Choduyet()
        {
            Pager_Choduyet.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(Pager_Choduyet.PageIndex, Pager_Choduyet.PageSize, WhereCondition(1));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(Pager_Choduyet.PageIndex - 1, Pager_Choduyet.PageSize, WhereCondition(1));
            DataGrid_choduyet.DataSource = _ds.Tables[0];
            DataGrid_choduyet.DataBind(); _ds.Clear();
            Pager_Choduyet.TotalRecords = CurrentPage_Choduyet.TotalRecords = TotalRecords;
            CurrentPage_Choduyet.TotalPages = Pager_Choduyet.CalculateTotalPages();
            CurrentPage_Choduyet.PageIndex = Pager_Choduyet.PageIndex;
            GetTotal();
        }

        public void Load_XB()
        {
            Pager_XB.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(Pager_XB.PageIndex, Pager_XB.PageSize, WhereCondition(3));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(Pager_XB.PageIndex - 1, Pager_XB.PageSize, WhereCondition(3));
            DataGrid_XB.DataSource = _ds.Tables[0];
            DataGrid_XB.DataBind(); _ds.Clear();
            Pager_XB.TotalRecords = CurrentPage_XB.TotalRecords = TotalRecords;
            CurrentPage_XB.TotalPages = Pager_XB.CalculateTotalPages();
            CurrentPage_XB.PageIndex = Pager_XB.PageIndex;
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
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        public void GetTotal()
        {
            // Total record mutilmedia
            #region TONG SO
            string _dsDangnew = UltilFunc.GetTotalCountStatus(WhereCondition(0), "CMS_CountListT_Multimedia").ToString();
            string _dsDangchoduyet = UltilFunc.GetTotalCountStatus(WhereCondition(1), "CMS_CountListT_Multimedia").ToString();
            string _dsDatralai = UltilFunc.GetTotalCountStatus(WhereCondition(2), "CMS_CountListT_Multimedia").ToString();
            string _dsDadaduyet = UltilFunc.GetTotalCountStatus(WhereCondition(3), "CMS_CountListT_Multimedia").ToString();
            _dsDangnew = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblDangcapnhat") + " (" + _dsDangnew + ")";
            _dsDatralai = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTralai") + " (" + _dsDatralai + ")";
            _dsDangchoduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChoXB") + " (" + _dsDangchoduyet + ")";
            _dsDadaduyet = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblDaXB") + " (" + _dsDadaduyet + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerProcess('" + _dsDangnew + "','" + _dsDangchoduyet + "','" + _dsDatralai + "','" + _dsDadaduyet + "');", true);
            #endregion
        }

        private string WhereCondition(int status)
        {
            string _whereNews = " 1=1 ";
            if (this.ddlLang.SelectedIndex > 0)
                _whereNews += " AND " + string.Format(" Languages_ID = {0}", ddlLang.SelectedValue);

            if (this.ddlCategorys.SelectedIndex > 0)
                _whereNews += " AND" + string.Format(" Category IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorys.SelectedValue);

            if (txtSearch.Text.Length > 0)
                _whereNews += " AND " + string.Format(" Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            switch (status)
            {
                case 0:
                    _whereNews += " and  (Status = 1 or Status is null)  and UserCreated=" + _user.UserID.ToString();//+
                    _whereNews += " Order by T_Multimedia.DateCreated DESC";
                //"  and Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
                    break;
                case 1:
                    _whereNews += " and  Status = 2 and UserCreated=" + _user.UserID.ToString();// +
                    _whereNews += " Order by T_Multimedia.DateModify DESC";
                //" and Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") and UserCreated=" + _user.UserID.ToString();
                    break;
                case 2:
                    _whereNews += " and  Status = 21 and UserCreated=" + _user.UserID.ToString();// +
                    _whereNews += " Order by T_Multimedia.DateModify DESC";
                //" and  Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") And UserCreated= " + _user.UserID.ToString();
                    break;
                case 3:
                    _whereNews += " and  Status = 3 ";//+//and UserCreated=" + _user.UserID.ToString() +
                    _whereNews += " Order by T_Multimedia.DatePublish DESC";    
                //"  and Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
                    break;
                default:
                    _whereNews += " and  (Status = 1 or Status is null)  and UserCreated=" + _user.UserID.ToString(); //+
                    //"  and Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
                    break;
            }
            return _whereNews;
        }

        #endregion

        #region Event Handle

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Lang_ID=" + ddlLang.SelectedValue.Trim());//+ "&ID=" +0
        }

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    pagesEditor.PageIndex = 0;
                    LoadData();
                    break;
                case 1:
                    Pager_Choduyet.PageIndex = 0;
                    Load_Choduyet();
                    break;
                case 2:
                    Pager_Tralai.PageIndex = 0;
                    Load_Tralai();
                    break;
                case 3:
                    Pager_XB.PageIndex = 0;
                    Load_XB();
                    break;
                default:
                    pagesEditor.PageIndex = 0;
                    LoadData();
                    break;
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            switch (TabContainer1.ActiveTabIndex)
            {
                case 0:
                    LoadData();
                    break;
                case 1:
                    Load_Tralai();
                    break;
                case 2:
                    Load_Choduyet();
                    break;
                case 3:
                    Load_XB();
                    break;
                default:
                    LoadData();
                    break;
            }
        }

        protected void lbt_Guiduyet_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in dgDataEditor.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    try
                    {
                        Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                        int _ID = Convert.ToInt32(this.dgDataEditor.DataKeys[item.ItemIndex].ToString());
                        _untilDAL.UpdateStatusMultimedia(_ID, 2, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "GỬI DUYỆT MULTIMEDIA", _ID, ConstAction.AmThanhHinhAnh);
                    }
                    catch (Exception ex) { }
                }
            }
            LoadData();
        }

        protected void lbt_xoa_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in dgDataEditor.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    try
                    {
                        int _ID = Convert.ToInt32(this.dgDataEditor.DataKeys[item.ItemIndex].ToString());
                        _untilDAL.DeleteFromT_Multimedia(_ID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xóa", Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA", 0, ConstAction.AmThanhHinhAnh);
                    }
                    catch (Exception ex) { }
                }
            }
            LoadData();
        }

        protected void lbt_tabtralai_gui_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in DataGrid_Tralai.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    try
                    {
                        int _ID = Convert.ToInt32(this.DataGrid_Tralai.DataKeys[item.ItemIndex].ToString());
                        _untilDAL.UpdateStatusMultimedia(_ID, 2, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xóa", Request["Menu_ID"].ToString(), "GỬI DUYỆT MULTIMEDIA BỊ TRẢ LẠI", _ID, ConstAction.AmThanhHinhAnh);
                    }
                    catch (Exception ex) { }
                }
            }
            Load_Tralai();
        }

        protected void lbt_tabtralai_xoa_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in DataGrid_Tralai.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    try
                    {
                        int _ID = Convert.ToInt32(this.DataGrid_Tralai.DataKeys[item.ItemIndex].ToString());
                        Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                        _untilDAL.DeleteFromT_Multimedia(_ID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA BỊ TRẢ LẠI", 0, ConstAction.AmThanhHinhAnh);
                    }
                    catch (Exception ex) { }
                }
            }
            Load_Tralai();
        }

        #endregion

        #region Datagrid Handle

        public void dgData_ItemDataBoundEditor(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void dgData_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.dgDataEditor.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&TabID=0");
            }
            else if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                try
                {
                    Label lblLogTitle = (Label)e.Item.FindControl("lblLogTitle");
                    int _ID = Convert.ToInt32(this.dgDataEditor.DataKeys[e.Item.ItemIndex].ToString());
                    _untilDAL.DeleteFromT_Multimedia(_ID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA VỪA MỚI CẬP NHẬT", 0, ConstAction.AmThanhHinhAnh);
                    LoadData();
                }
                catch (Exception ex) { }
            }
        }

        public void DataGrid_choduyet_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.DataGrid_choduyet.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString());
            }
        }

        public void DataGrid_Tralai_ItemDataBoundEditor(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void DataGrid_Tralai_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {

            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.DataGrid_Tralai.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() + "&TabID=1");
            }
            else if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                try
                {
                    Label lblLogTitle = (Label)e.Item.FindControl("lblLogTitle");
                    int _ID = Convert.ToInt32(this.DataGrid_Tralai.DataKeys[e.Item.ItemIndex].ToString());
                    _untilDAL.DeleteFromT_Multimedia(_ID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "XÓA MULTIMEDIA BỊ TRẢ LẠI", 0, ConstAction.AmThanhHinhAnh);
                    Load_Tralai();
                }
                catch (Exception ex) { }
            }
        }

        public void DataGrid_XB_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.DataGrid_XB.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString());
            }
        }

        #endregion

        #region PageIndexChange

        protected void pages_IndexChanged_Editor(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager_Choduyet_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_Choduyet();
        }

        protected void Pager_Tralai_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_Tralai();
        }

        protected void Pager_XB_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_XB();
        }

        #endregion
    }
}
