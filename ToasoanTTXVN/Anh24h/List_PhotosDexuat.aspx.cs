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
using System.IO;
using System.Collections.Generic;
using SSOLib.ServiceAgent;
using ToasoanTTXVN;

namespace HPCApplication.Anh24h
{
    public partial class List_PhotosDexuat : BasePage
    {
        protected int status = 0;
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
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
                        LoadComboBox();
                        int tab_id = 0;
                        if (Request["Tab"] != null)
                            tab_id = Convert.ToInt32(Request["Tab"]);
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);
                    }
                }
            }
        }
        #region Methods
        protected void ActiverPermission()
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                status = 5;
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                status = 7; //FileUpload1.Visible = true;
            }
            this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonxoa") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll');");
            this.LinkDangAnh1.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuongui") + "','ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll');");

        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cboLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
        }
        public void LoadData()
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: setdisplay(1);", true);
            string where = " 1=1 ";
            where += " and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND Photo_Status = 5 ";//AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where += " Order by Date_Create DESC";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Photo_Events(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Photo_Events(pages.PageIndex - 1, pages.PageSize, where);
            grdListCate.DataSource = _ds;
            grdListCate.DataBind(); _ds.Clear();
            if (TotalRecords == 0)
            {
                pages.Visible = false;
                curentPages.Visible = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            if (grdListCate.Items.Count > 0)
            {
                int count = 0;
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
                    T_Photo_Event _obj = new T_Photo_Event();
                    ImageButton btnModify = m_Item.FindControl("btnModify") as ImageButton;
                    ImageButton btnSave = m_Item.FindControl("btnSave") as ImageButton;
                    ImageButton btnBack = m_Item.FindControl("btnBack") as ImageButton;
                    Label lblAnpham = m_Item.FindControl("lblAnpham") as Label;
                    LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                    Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    //Image imgView = m_Item.FindControl("imgView") as Image;
                    //Image imgBrowse = m_Item.FindControl("imgBrowse") as Image;
                    if (txtTitle.Text.Trim().Length > 0)
                    {
                        btnSave.Visible = false;
                        btnBack.Visible = false;
                        btnModify.Visible = true;
                        lblAnpham.Visible = true;
                        lblTacGia.Visible = true;
                        btnEdit.Visible = true;
                        cboNgonNgu.Visible = false;
                        txtTitle.Visible = false;
                        txtTacGia.Visible = false;
                        lblGhichu.Visible = true;
                        txtGhichu.Visible = false;
                        //imgView.Visible = true;
                        //imgBrowse.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        btnModify.Visible = false;
                        lblAnpham.Visible = false;
                        lblTacGia.Visible = false;
                        btnEdit.Visible = false;
                        cboNgonNgu.Visible = true;
                        txtTitle.Visible = true;
                        txtTacGia.Visible = true;
                        lblGhichu.Visible = false;
                        txtGhichu.Visible = true;
                        //imgView.Visible = false;
                        //imgBrowse.Visible = true;
                        if (count == 0)
                        {
                            txtTitle.Focus();
                        }
                        count++;
                    }
                }
            }

            #region PHAN TINH TONG SO
            tabpnltinXuly.HeaderText = CommonLib.ReadXML("lblAnhchoxuly") + " (" + TotalRecords + ")";
            DataSet _dsReturn;
            string where2 = " 1=1 and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " and Photo_Status= 7 ";// AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where2 += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            else if (cboLang.SelectedIndex > 0)
                where2 += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where2 += " Order by Date_Update DESC";
            _dsReturn = _untilDAL.BindGridT_Photo_Events(pages1.PageIndex - 1, pages1.PageSize, where2);
            this.TabPanel1.HeaderText = CommonLib.ReadXML("lblAnhtralai") + " (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            #endregion

            //CurrentPage2.TotalPages = pages.CalculateTotalPages();
            //CurrentPage2.PageIndex = pages.PageIndex;
        }
        public void LoadDataAnhBientaplai()
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: setdisplay(0);", true);
            //FileUpload1.Visible = false;
            //string where = " 1=1 and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " and Photo_Status= 7 AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";// and Creator=" + _user.UserID;
            //if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
            //    where += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            //else if (cboLang.SelectedIndex > 0)
            //    where += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            //where += " Order by Date_Update DESC";
            //pages1.PageSize = Global.MembersPerPage;
            //HPCBusinessLogic.T_Photo_EventDAL _cateDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            //DataSet _ds;
            //_ds = _cateDAL.BindGridT_Photo_Events(pages1.PageIndex, pages1.PageSize, where);
            //int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            //int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            //if (TotalRecord == 0)
            //    _ds = _cateDAL.BindGridT_Photo_Events(pages1.PageIndex - 1, pages1.PageSize, where);
            //dgr_anhbientaplai.DataSource = _ds;
            //dgr_anhbientaplai.DataBind(); _ds.Clear();
            //pages1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            string where = " 1=1 ";
            where += " and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND Photo_Status = 7 ";//AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where += " Order by Date_Update DESC";
            pages1.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Photo_Events(pages1.PageIndex, pages1.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Photo_Events(pages1.PageIndex - 1, pages1.PageSize, where);
            dgr_anhbientaplai.DataSource = _ds;
            dgr_anhbientaplai.DataBind(); _ds.Clear();
            if (TotalRecords == 0)
            {
                pages1.Visible = false;
                CurrentPage1.Visible = false;
            }
            pages1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = pages1.CalculateTotalPages();
            CurrentPage1.PageIndex = pages1.PageIndex;
            if (dgr_anhbientaplai.Items.Count > 0)
            {
                int count = 0;
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
                    T_Photo_Event _obj = new T_Photo_Event();
                    ImageButton btnModify = m_Item.FindControl("btnModify") as ImageButton;
                    ImageButton btnSave = m_Item.FindControl("btnSave") as ImageButton;
                    ImageButton btnBack = m_Item.FindControl("btnBack") as ImageButton;
                    Label lblAnpham = m_Item.FindControl("lblAnpham") as Label;
                    LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                    Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    //Image imgView = m_Item.FindControl("imgView") as Image;
                    //Image imgBrowse = m_Item.FindControl("imgBrowse") as Image;
                    if (txtTitle.Text.Trim().Length > 0)
                    {
                        btnSave.Visible = false;
                        btnBack.Visible = false;
                        btnModify.Visible = true;
                        lblAnpham.Visible = true;
                        lblTacGia.Visible = true;
                        btnEdit.Visible = true;
                        cboNgonNgu.Visible = false;
                        txtTitle.Visible = false;
                        txtTacGia.Visible = false;
                        lblGhichu.Visible = true;
                        txtGhichu.Visible = false;
                        //imgView.Visible = true;
                        //imgBrowse.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        btnModify.Visible = false;
                        lblAnpham.Visible = false;
                        lblTacGia.Visible = false;
                        btnEdit.Visible = false;
                        cboNgonNgu.Visible = true;
                        txtTitle.Visible = true;
                        txtTacGia.Visible = true;
                        txtGhichu.Visible = true;
                        lblGhichu.Visible = false;
                        //imgView.Visible = false;
                        //imgBrowse.Visible = true;
                        if (count == 0)
                        {
                            txtTitle.Focus();
                        }
                        count++;
                    }
                }
            }
            #region PHAN TINH TONG SO
            TabPanel1.HeaderText = CommonLib.ReadXML("lblAnhtralai") + " (" + TotalRecords + ")";
            DataSet _dsReturn;
            string where1 = " 1=1 and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " and Photo_Status = 5 AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where1 += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where1 += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where1 += " Order by Date_Create DESC";
            _dsReturn = _untilDAL.BindGridT_Photo_Events(pages.PageIndex - 1, pages.PageSize, where1);
            this.tabpnltinXuly.HeaderText = CommonLib.ReadXML("lblAnhchoxuly") + " (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            #endregion
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
        protected string GetUserName()
        {
            string strTemp = "";
            if (_user != null)
                strTemp = _user.UserName + "," + GetvType() + "," + _user.UserID.ToString();
            return strTemp;

        }
        protected string GetvType()
        {
            string strTemp = "4,0";
            return strTemp;
        }
        protected string getUrlParameter()
        {
            return "Menu_ID=" + Request["Menu_ID"].ToString();
        }
        #endregion

        #region Event Click
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData();
            }
            else
            {
                pages1.PageIndex = 0;
                this.LoadDataAnhBientaplai();
            }
        }
        public void SetTotal()
        {
            HPCBusinessLogic.T_Photo_EventDAL _cateDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            string where1 = " 1=1 and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " and Photo_Status = 5 ";//AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where1 += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where1 += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where1 += " Order by Date_Create DESC";
            string where2 = " 1=1 and Creator=" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " and Photo_Status= 7 ";// AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                where2 += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            else if (cboLang.SelectedIndex > 0)
                where2 += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where2 += " Order by Date_Update DESC";
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            _dsReturn1 = _cateDAL.BindGridT_Photo_Events(pages.PageIndex, pages.PageSize, where1);
            _dsReturn2 = _cateDAL.BindGridT_Photo_Events(pages1.PageIndex, pages1.PageSize, where2);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn1.Clear();
            _dsReturn2.Clear();
        }
        private T_Photo_Event setItem(int PhotoID, string urlImage, string PhotoTitle, int LangId, string tacgia, string ghichu, DateTime date)
        {
            T_Photo_Event _objPoto = new T_Photo_Event();
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(tacgia))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = tacgia.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _objPoto.AuthorID = butdanhID;
            _objPoto.Photo_ID = PhotoID;
            //_objPoto = _DAL.GetOneFromT_Photo_EventsByID(PhotoID);
            _objPoto.Date_Update = date;
            _objPoto.Photo_Name = PhotoTitle;
            _objPoto.Photo_Medium = urlImage;
            _objPoto.Author_Name = tacgia;
            _objPoto.Lang_ID = LangId;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Status = status;
            _objPoto.Photo_Desc = ghichu;
            return _objPoto;
        }
        protected void LinkEdit_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            int count = 0;
            if (TabContainer1.ActiveTabIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: setdisplay(1);", true);
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    ImageButton btnModify = m_Item.FindControl("btnModify") as ImageButton;
                    ImageButton btnSave = m_Item.FindControl("btnSave") as ImageButton;
                    ImageButton btnBack = m_Item.FindControl("btnBack") as ImageButton;
                    Label lblAnpham = m_Item.FindControl("lblAnpham") as Label;
                    LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                    Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    btnSave.Visible = true;
                    btnBack.Visible = true;
                    btnModify.Visible = false;
                    lblAnpham.Visible = false;
                    lblTacGia.Visible = false;
                    lblGhichu.Visible = false;
                    btnEdit.Visible = false;
                    cboNgonNgu.Visible = true;
                    txtTitle.Visible = true;
                    txtTacGia.Visible = true;
                    txtGhichu.Visible = true;
                    if (count == 0)
                    {
                        txtTitle.Focus();
                    }
                    count++;
                    litMessages.Text = "";
                } 
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: setdisplay(0);", true);
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    ImageButton btnModify = m_Item.FindControl("btnModify") as ImageButton;
                    ImageButton btnSave = m_Item.FindControl("btnSave") as ImageButton;
                    ImageButton btnBack = m_Item.FindControl("btnBack") as ImageButton;
                    Label lblAnpham = m_Item.FindControl("lblAnpham") as Label;
                    LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                    Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    btnSave.Visible = true;
                    btnBack.Visible = true;
                    btnModify.Visible = false;
                    lblAnpham.Visible = false;
                    lblTacGia.Visible = false;
                    lblGhichu.Visible = false;
                    btnEdit.Visible = false;
                    cboNgonNgu.Visible = true;
                    txtTitle.Visible = true;
                    txtTacGia.Visible = true;
                    txtGhichu.Visible = true;
                    if (count == 0)
                    {
                        txtTitle.Focus();
                    }
                    count++;
                    litMessages.Text = "";
                }
            }
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
                T_Photo_Event _catObj = new T_Photo_Event();
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    #region "Duyet danh sach cac doi tuong tren luoi"
                    foreach (DataGridItem m_Item in grdListCate.Items)
                    {
                        TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                        TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                        TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                        Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                        DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                        int PhotoID = Convert.ToInt32(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _catObj = _cateDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(PhotoID));
                        _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txtGhichu.Text, _catObj.Date_Update);
                        int _return = _cateDAL.InsertT_Photo_Events(_catObj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _catObj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                    }
                    LoadData();
                    this.litMessages.Text = "Lưu giữ thành công";
                    #endregion
                }
                if (TabContainer1.ActiveTabIndex == 1)
                {
                    #region "Duyet danh sach cac doi tuong tren luoi"
                    foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                    {
                        TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                        TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                        TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                        Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                        DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                        int PhotoID = Convert.ToInt32(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _catObj = _cateDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(PhotoID));
                        _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txtGhichu.Text, _catObj.Date_Update);
                        int _return = _cateDAL.InsertT_Photo_Events(_catObj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _catObj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                    }
                    LoadDataAnhBientaplai();
                    this.litMessages.Text = "Lưu giữ thành công";
                    #endregion
                }
                
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }

        }
        protected void btnLinkDuyetAnh_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        int PhotoID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _obj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txtGhichu.Text, DateTime.Now);
                        int _return = _untilDAL.InsertT_Photo_Events(_obj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                        ar.Add(double.Parse(PhotoID.ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        int PhotoID = int.Parse(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _obj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txtGhichu.Text, DateTime.Now);
                        int _return = _untilDAL.InsertT_Photo_Events(_obj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                        ar.Add(double.Parse(PhotoID.ToString()));
                    }
                }
            }
            for (int i = 0; i < ar.Count; i++)
            {
                double PhotoID = double.Parse(ar[i].ToString());
                _obj = _untilDAL.GetOneFromT_Photo_EventsByID(PhotoID);
                //dung sua: bai tra lai thi khong update Date_Update
                if (_obj.Photo_Name.Trim().Length > 0)
                {
                    if (TabContainer1.ActiveTabIndex == 1)
                        _untilDAL.UpdateStatus_Photo_Events(PhotoID, 8, _user.UserID, DateTime.Parse(_obj.Date_Update.ToString()));
                    else
                        _untilDAL.UpdateStatus_Photo_Events(PhotoID, 8, _user.UserID, DateTime.Now);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
                string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Gửi duyệt Ảnh] [Ảnh: " + _untilDAL.GetOneFromT_Photo_EventsByID(PhotoID).Photo_Name + "]";
                //UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), _ActionsCode, 0, "[Gửi duyệt]", Convert.ToInt32(Request["Menu_ID"]));
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Gửi duyệt]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, PhotoID, ConstAction.TSAnh);
            }

            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadDataAnhBientaplai();
            SetTotal();
        }
        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
                T_Photo_Event _obj = new T_Photo_Event();
                _obj = _untilDAL.GetOneFromT_Photo_EventsByID(News_ID);
                string imgName = _obj.Photo_Name;
                if (imgName == "")
                {
                    imgName = _obj.Photo_Medium;
                }
                _untilDAL.DeleteFromT_Photo_Event(int.Parse(News_ID.ToString()));
                this.TabContainer1_ActiveTabChanged(null, null);
                string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Xóa Ảnh] [Ảnh: " + imgName + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, News_ID, ConstAction.TSAnh);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadDataAnhBientaplai();
            SetTotal();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void pages_IndexChanged_Anhtralai(object sender, EventArgs e)
        {
            LoadDataAnhBientaplai();
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadDataAnhBientaplai();
            }
        }
        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton imgbtnDuyet = (ImageButton)e.Item.FindControl("btnDuyet");
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            Label lblLangId = e.Item.FindControl("lblLangId") as Label;
            if (cboNgonNgu != null)
            {
                UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, lblLangId.Text.ToString());
            }
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
                imgbtnDuyet.Attributes.Add("onclick", "return confirm(\"Bạn có chắc muốn gửi duyệt không?\");");

            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void dgData_ItemDataBound1(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton imgbtnDuyet = (ImageButton)e.Item.FindControl("btnDuyet");
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            Label lblLangId = e.Item.FindControl("lblLangId") as Label;
            if (cboNgonNgu != null)
            {
                UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, lblLangId.Text.ToString());
            }
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
                imgbtnDuyet.Attributes.Add("onclick", "return confirm(\"Bạn có chắc muốn gửi duyệt không?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            //HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            //if (e.CommandArgument.ToString().ToLower() == "edit")
            //{
            //    int tab = 0;
            //if (TabContainer1.ActiveTabIndex == 0)
            //    tab = 0;
            //int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //Session["PageFromID"] = 1;
            //Response.Redirect("~/Anh24h/Edit_PhotoDexuat.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab + "&PageFromID=1");
            //}
            //if (e.CommandArgument.ToString().ToLower() == "btnduyet")
            //{
            //    HPCInfo.T_Photo_Event _obj = new T_Photo_Event();
            //    _obj = _untilDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString()));
            //    _untilDAL.UpdateStatus_Photo_Events(_obj.Photo_ID, 8, _user.UserID, DateTime.Now);
            //    //UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), "[Duyệt ảnh] [Ảnh: " + _obj.Photo_Name + "]", 0, "[Duyệt ảnh]", Convert.ToInt32(Request["Menu_ID"]));
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Duyệt ảnh]", Convert.ToInt32(Request["Menu_ID"]), "[Duyệt ảnh] [Ảnh: " + _obj.Photo_Name + "]", 0);
            //}
            //if (e.CommandArgument.ToString().ToLower() == "delete")
            //{
            //    int id = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //    _untilDAL.DeleteFromT_Photo_Event(id);
            //    string _ActionsCode = "[Danh sách ảnh đề xuất]-->[Thao tác : Xóa ][ID:" + id + " ]";
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id);
            //    //UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), _ActionsCode, 0, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]));
            //    this.LoadData();
            //}
            litMessages.Text = "";
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            ImageButton btnModify = e.Item.FindControl("btnModify") as ImageButton;
            ImageButton btnSave = e.Item.FindControl("btnSave") as ImageButton;
            ImageButton btnBack = e.Item.FindControl("btnBack") as ImageButton;
            Label lblAnpham = e.Item.FindControl("lblAnpham") as Label;
            LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
            Label lblTacGia = e.Item.FindControl("lblTacGia") as Label;
            Label lblUrlPath = e.Item.FindControl("lblUrlPath") as Label;
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            TextBox txtTacGia = e.Item.FindControl("txt_tacgia") as TextBox;
            TextBox txtGhichu = e.Item.FindControl("txtGhichu") as TextBox;
            Label lblGhichu = e.Item.FindControl("lblGhichu") as Label;
            //Image imgView = e.Item.FindControl("imgView") as Image;
            //Image imgBrowse = e.Item.FindControl("imgBrowse") as Image;
            int _ID = Convert.ToInt32(grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "editphoto")
            {
                btnSave.Visible = true;
                btnBack.Visible = true;
                btnModify.Visible = false;
                lblAnpham.Visible = false;
                lblTacGia.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
                txtGhichu.Visible = true;
                lblGhichu.Visible = false;
                //imgView.Visible = false;
                //imgBrowse.Visible = true;
            }
            if (e.CommandArgument.ToString().ToLower() == "savephoto")
            {
                if (txtTitle.Text.Trim().Length > 0)
                {
                    btnSave.Visible = false;
                    btnBack.Visible = false;
                    btnModify.Visible = true;
                    lblAnpham.Visible = true;
                    lblTacGia.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txtGhichu.Visible = false;
                    lblGhichu.Visible = true;
                    //imgView.Visible = true;
                    //imgBrowse.Visible = false;
                    _obj = setItem(_ID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txtTacGia.Text, txtGhichu.Text, _obj.Date_Update);
                    int _return = _DAL.InsertT_Photo_Events(_obj);
                    btnEdit.Text = txtTitle.Text;
                    lblAnpham.Text = UltilFunc.GetTenAnpham(Convert.ToInt32(cboNgonNgu.SelectedValue));
                    lblTacGia.Text = txtTacGia.Text;
                    lblGhichu.Text = txtGhichu.Text;
                    string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "back")
            {
                if (txtTitle.Text.Trim().Length > 0)
                {
                    btnSave.Visible = false;
                    btnBack.Visible = false;
                    btnModify.Visible = true;
                    lblAnpham.Visible = true;
                    lblTacGia.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txtGhichu.Visible = false;
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 0)
                    tab = 0;
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Anh24h/Edit_PhotoDexuat.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
            SetTotal();
        }
        public void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {
            litMessages.Text = "";
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            ImageButton btnModify = e.Item.FindControl("btnModify") as ImageButton;
            ImageButton btnSave = e.Item.FindControl("btnSave") as ImageButton;
            ImageButton btnBack = e.Item.FindControl("btnBack") as ImageButton;
            Label lblAnpham = e.Item.FindControl("lblAnpham") as Label;
            LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
            Label lblTacGia = e.Item.FindControl("lblTacGia") as Label;
            Label lblGhichu = e.Item.FindControl("lblGhichu") as Label;
            Label lblUrlPath = e.Item.FindControl("lblUrlPath") as Label;
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            TextBox txtTacGia = e.Item.FindControl("txt_tacgia") as TextBox;
            TextBox txtGhichu = e.Item.FindControl("txtGhichu") as TextBox;
            //Image imgView = e.Item.FindControl("imgView") as Image;
            //Image imgBrowse = e.Item.FindControl("imgBrowse") as Image;
            int _ID = Convert.ToInt32(dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
            _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "editphoto")
            {
                btnSave.Visible = true;
                btnBack.Visible = true;
                btnModify.Visible = false;
                lblAnpham.Visible = false;
                lblTacGia.Visible = false;
                lblGhichu.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
                txtGhichu.Visible = true;
                //imgView.Visible = false;
                //imgBrowse.Visible = true;
            }
            if (e.CommandArgument.ToString().ToLower() == "savephoto")
            {
                if (txtTitle.Text.Trim().Length > 0)
                {
                    btnSave.Visible = false;
                    btnBack.Visible = false;
                    btnModify.Visible = true;
                    lblAnpham.Visible = true;
                    lblTacGia.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txtGhichu.Visible = false;
                    //imgView.Visible = true;
                    //imgBrowse.Visible = false;
                    _obj = setItem(_ID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txtTacGia.Text, txtGhichu.Text, _obj.Date_Update);
                    int _return = _DAL.InsertT_Photo_Events(_obj);
                    btnEdit.Text = txtTitle.Text;
                    lblAnpham.Text = UltilFunc.GetTenAnpham(Convert.ToInt32(cboNgonNgu.SelectedValue));
                    lblTacGia.Text = txtTacGia.Text;
                    lblGhichu.Text = txtGhichu.Text;
                    string _ActionsCode = "[Thời sự qua ảnh] [Danh sách ảnh đề xuất] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
                //if (TabContainer1.ActiveTabIndex == 0)
                //    LoadData();
                //else if (TabContainer1.ActiveTabIndex == 1)
                //    LoadDataAnhBientaplai();
                SetTotal();
            }
            if (e.CommandArgument.ToString().ToLower() == "back")
            {
                if (txtTitle.Text.Trim().Length > 0)
                {
                    btnSave.Visible = false;
                    btnBack.Visible = false;
                    btnModify.Visible = true;
                    lblAnpham.Visible = true;
                    lblTacGia.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txtGhichu.Visible = false;
                    //imgView.Visible = true;
                    //imgBrowse.Visible = false;
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 1)
                    tab = 1;
                int catID = Convert.ToInt32(this.dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Anh24h/Edit_PhotoDexuat.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
            //HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            //if (e.CommandArgument.ToString().ToLower() == "edit")
            //{
            //    int tab = 0;
            //    if (TabContainer1.ActiveTabIndex == 1)
            //        tab = 1;
            //    int catID = Convert.ToInt32(this.dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
            //    Response.Redirect("~/Anh24h/Edit_PhotoDexuat.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            //}
            //if (e.CommandArgument.ToString().ToLower() == "btnduyet")
            //{
            //    HPCInfo.T_Photo_Event _obj = new T_Photo_Event();
            //    double id = Convert.ToDouble(this.dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
            //    _obj = _untilDAL.GetOneFromT_Photo_EventsByID(id);
            //    _untilDAL.UpdateStatus_Photo_Events(_obj.Photo_ID, 8, _user.UserID, DateTime.Parse(_obj.Date_Update.ToString()));
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Duyệt ảnh]", Convert.ToInt32(Request["Menu_ID"]), "[Ảnh: " + _obj.Photo_Name + "]", id);
            //}
            //if (e.CommandArgument.ToString().ToLower() == "delete")
            //{
            //    int id = Convert.ToInt32(this.dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
            //    _untilDAL.DeleteFromT_Photo_Event(id);
            //    string _ActionsCode = "[Danh sách ảnh trả lại]-->[Thao tác : Xóa ][ID:" + id + " ]";
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id);
            //    this.LoadDataAnhBientaplai();
            //}
            //if (TabContainer1.ActiveTabIndex == 0)
            //    LoadData();
            //if (TabContainer1.ActiveTabIndex == 1)
            //    LoadDataAnhBientaplai();
            //SetTotal();
        }
        #endregion

    }
}
