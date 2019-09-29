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
using HPCBusinessLogic.DAL;
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.Anh24h
{
    public partial class List_PhotosChoDuyet : BasePage
    {
        protected int status = 0;
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
                        int tab_id = 0;
                        LoadComboBox();
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
        protected void ActiverPermission()
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                status = 8;
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                status = 3;
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                status = 2;
            }
            this.LinkDangAnh1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn Đăng ảnh ?','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkDangAnh1');");
            this.btnXoa.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa ?','ctl00_MainContent_TabContainer1_tabpnltinXuly_btnXoa');");
            this.btnDangAnh2.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn gửi Duyệt ?','ctl00_MainContent_TabContainer1_tabpnltinbtl_btnDangAnh2');");
            this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn Trả lại ?','ctl00_MainContent_TabContainer1_tabpnltinXuly_LinkDelete');");
            this.btnXoa2.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa ?','ctl00_MainContent_TabContainer1_tabpnltinbtl_btnXoa2');");
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cboLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
        }
        public void LoadData()
        {
            string where = " 1=1 and Photo_Status = 8 ";//AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where += " Order by Date_Update DESC";
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
                CurrentPage2.Visible = false;
            }
            pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
            CurrentPage2.TotalPages = pages.CalculateTotalPages();
            CurrentPage2.PageIndex = pages.PageIndex;
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
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    Label lblTienNB = m_Item.FindControl("lblTienNB") as Label;
                    TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
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
                        lblTienNB.Visible = true;
                        txt_tienNB.Visible = false;
                    }
                    else
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
                        lblTienNB.Visible = false;
                        txt_tienNB.Visible = true;
                        if (count == 0)
                        {
                            txtTitle.Focus();
                        }
                        count++;
                    }
                }
            }

            #region PHAN TINH TONG SO
            SetTotal();
            #endregion
            

        }
        public void LoadData_Daduyet()
        {
            string where2 = " 1=1 and Photo_Status= 3 ";// AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where2 += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            else if (cboLang.SelectedIndex > 0)
                where2 += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where2 += " Order by Date_Update DESC";
            pages1.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Photo_Events(pages1.PageIndex, pages1.PageSize, where2);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Photo_Events(pages1.PageIndex - 1, pages1.PageSize, where2);
            DataGrid1.DataSource = _ds;
            DataGrid1.DataBind(); _ds.Clear();
            pages1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            #region PHAN TINH TONG SO
            SetTotal();
            #endregion
            CurrentPage1.TotalPages = pages1.CalculateTotalPages();
            CurrentPage1.PageIndex = pages1.PageIndex;
        }
        public void LoadDataAnhBientaplai()
        {
            string where = " 1=1 and Photo_Status=2";//  AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where += " Order by Date_Update DESC";
            pageNgung.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Photo_Events(pageNgung.PageIndex, pageNgung.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Photo_Events(pageNgung.PageIndex - 1, pageNgung.PageSize, where);
            dgr_anhbientaplai.DataSource = _ds;
            dgr_anhbientaplai.DataBind(); _ds.Clear();
            if (TotalRecords == 0)
            {
                pageNgung.Visible = false;
                curentPagesNgung.Visible = false;
            }
            pageNgung.TotalRecords = curentPagesNgung.TotalRecords = TotalRecords;
            curentPagesNgung.TotalPages = pageNgung.CalculateTotalPages();
            curentPagesNgung.PageIndex = pageNgung.PageIndex;
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
                    Label lblGhichu = m_Item.FindControl("lblGhichu") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    TextBox txtGhichu = m_Item.FindControl("txtGhichu") as TextBox;
                    Label lblTienNB = m_Item.FindControl("lblTienNB") as Label;
                    TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
                    //Image imgView = m_Item.FindControl("imgView") as Image;
                    //Image imgBrowse = m_Item.FindControl("imgBrowse") as Image;
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
                        lblTienNB.Visible = true;
                        txt_tienNB.Visible = false;
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
                        lblGhichu.Visible = false;
                        btnEdit.Visible = false;
                        cboNgonNgu.Visible = true;
                        txtTitle.Visible = true;
                        txtTacGia.Visible = true;
                        txtGhichu.Visible = true;
                        lblTienNB.Visible = false;
                        txt_tienNB.Visible = true;
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
            SetTotal();
            #endregion

            
        }
        public void SetTotal()
        {
            HPCBusinessLogic.T_Photo_EventDAL _cateDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            string where1 = " 1=1 and Photo_Status = 8";// AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where1 += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            if (cboLang.SelectedIndex > 0)
                where1 += " AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where1 += " Order by Date_Update DESC";
            string where2 = " 1=1 and Photo_Status= 3";//  AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where2 += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            else if (cboLang.SelectedIndex > 0)
                where2 += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where2 += " Order by Date_Update DESC";
            string where3 = " 1=1 and Photo_Status= 2";//  AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where3 += "AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            else if (cboLang.SelectedIndex > 0)
                where3 += "AND Lang_ID=" + cboLang.SelectedValue.ToString();
            where3 += " Order by Date_Update DESC";
            DataSet _dsReturn1;
            DataSet _dsReturn2;
            DataSet _dsReturn3;
            _dsReturn1 = _cateDAL.BindGridT_Photo_Events(pages.PageIndex, pages.PageSize, where1);
            _dsReturn2 = _cateDAL.BindGridT_Photo_Events(pages1.PageIndex, pages1.PageSize, where2);
            _dsReturn3 = _cateDAL.BindGridT_Photo_Events(pageNgung.PageIndex, pageNgung.PageSize, where3);
            string choduyet = CommonLib.ReadXML("lblAnhchoduyet") + " (" + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            string dadang = CommonLib.ReadXML("lblAnhdadang") + " (" + _dsReturn2.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            string bientaplai = CommonLib.ReadXML("lblAnhbientaplai") + " (" + _dsReturn3.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal('" + choduyet + "','" + dadang + "','" + bientaplai + "');", true);
            _dsReturn1.Clear();
            _dsReturn2.Clear();
            _dsReturn3.Clear();
        }
        private T_Photo_Event setItem(int PhotoID, string urlImage, string PhotoTitle, int LangId, string tacgia, string tienNB, string ghichu, DateTime date)
        {
            int tien = 0;
            if (!string.IsNullOrEmpty(tienNB))
            {
                try { tien = int.Parse(tienNB.Replace(",", "")); }
                catch { ;}
            }
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
            _objPoto.TienNB = tien;
            _objPoto.Lang_ID = LangId;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Status = status;
            _objPoto.Photo_Desc = ghichu;
            return _objPoto;
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
        #endregion
        #region Event Click
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                pages1.PageIndex = 0;
                this.LoadData_Daduyet();
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                pageNgung.PageIndex = 0;
                this.LoadDataAnhBientaplai();
            }
        }
        protected void LinkButtonXoa_Click(object sender, EventArgs e)
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
                foreach (DataGridItem m_Item in DataGrid1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                string _ActionsCode = "[Thời sự qua ảnh] [Danh sách Đăng ảnh thời sự] [Xóa Ảnh] [Ảnh: " + _untilDAL.GetOneFromT_Photo_EventsByID(News_ID).Photo_Name + "]";
                _untilDAL.DeleteFromT_Photo_Version(int.Parse(News_ID.ToString()));
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, News_ID, ConstAction.TSAnh);
                this.TabContainer1_ActiveTabChanged(null, null);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            SetTotal();
        }
        protected void LinkEdit_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            int count = 0;
            if (TabContainer1.ActiveTabIndex == 0)
            {
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
                    Label lblTienNB = m_Item.FindControl("lblTienNB") as Label;
                    TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
                    btnSave.Visible = true;
                    btnBack.Visible = true;
                    btnModify.Visible = false;
                    lblAnpham.Visible = false;
                    lblTacGia.Visible = false;
                    lblTienNB.Visible = false;
                    lblGhichu.Visible = false;
                    btnEdit.Visible = false;
                    cboNgonNgu.Visible = true;
                    txtTitle.Visible = true;
                    txtTacGia.Visible = true;
                    txt_tienNB.Visible = true;
                    txtGhichu.Visible = true;
                    if (count == 0)
                    {
                        txtTitle.Focus();
                    }
                    count++;
                    litMessages.Text = "";
                }
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
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
                    Label lblTienNB = m_Item.FindControl("lblTienNB") as Label;
                    TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
                    btnSave.Visible = true;
                    btnBack.Visible = true;
                    btnModify.Visible = false;
                    lblAnpham.Visible = false;
                    lblTacGia.Visible = false;
                    lblTienNB.Visible = false;
                    lblGhichu.Visible = false;
                    btnEdit.Visible = false;
                    cboNgonNgu.Visible = true;
                    txtTitle.Visible = true;
                    txtTacGia.Visible = true;
                    txt_tienNB.Visible = true;
                    txtGhichu.Visible = true;
                    if (count == 0)
                    {
                        txtTitle.Focus();
                    }
                    count++;
                    litMessages1.Text = "";
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
                        TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                        TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                        Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                        DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                        int PhotoID = Convert.ToInt32(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _catObj = _cateDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(PhotoID));
                        _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txt_tienNB.Text, txtGhichu.Text, _catObj.Date_Update);
                        int _return = _cateDAL.InsertT_Photo_Events(_catObj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _catObj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                    }
                    LoadData();
                    this.litMessages.Text = "Lưu giữ thành công";
                    #endregion
                }
                if (TabContainer1.ActiveTabIndex == 2)
                {
                    #region "Duyet danh sach cac doi tuong tren luoi"
                    foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                    {
                        TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                        TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                        TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                        TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                        Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                        DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                        int PhotoID = Convert.ToInt32(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _catObj = _cateDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(PhotoID));
                        _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txt_tienNB.Text, txtGhichu.Text,_catObj.Date_Update);
                        int _return = _cateDAL.InsertT_Photo_Events(_catObj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _catObj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                    }
                    LoadDataAnhBientaplai();
                    this.litMessages1.Text = "Lưu giữ thành công";
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
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                    TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        int PhotoID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _obj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txt_tienNB.Text, txtGhichu.Text, DateTime.Now);
                        int _return = _untilDAL.InsertT_Photo_Events(_obj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                        ar.Add(double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                    TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        int PhotoID = int.Parse(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        _obj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txt_tienNB.Text, txtGhichu.Text, DateTime.Now);
                        int _return = _untilDAL.InsertT_Photo_Events(_obj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                        ar.Add(double.Parse(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                _obj = _untilDAL.GetOneFromT_Photo_EventsByID(News_ID);
                //dung sua: bai tra lai thi khong update Date_Update
                if (TabContainer1.ActiveTabIndex == 1)
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 3, _user.UserID, DateTime.Parse(_obj.Date_Update.ToString()));
                else
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 3, _user.UserID, DateTime.Now);
                _untilDAL.Insert_Version_From_T_PhotoEvent_WithUserModify(News_ID, 3, DateTime.Now);
                this.TabContainer1_ActiveTabChanged(null, null);
                string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Đăng Ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Đăng Ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, News_ID, ConstAction.TSAnh);
                #region Sync
                // DONG BO ANH
                try
                {
                    SynFiles _syncfile = new SynFiles();
                    if (_obj.Photo_Medium.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_obj.Photo_Medium, HPCComponents.Global.ImagesService);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                //END
                #endregion
            }
           
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 2)
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
            else if (TabContainer1.ActiveTabIndex == 2)
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
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
                T_Photo_Event _obj = new T_Photo_Event();
                _obj = _untilDAL.GetOneFromT_Photo_EventsByID(News_ID);
                string imgName = _obj.Photo_Name;
                if (imgName == "")
                {
                    imgName = _obj.Photo_Medium;
                }
                //dung sua: bai tra lai thi khong update Date_Update
                if (TabContainer1.ActiveTabIndex == 1)
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 7, _user.UserID, DateTime.Parse(_obj.Date_Update.ToString()));
                else
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 7, _user.UserID, DateTime.Now);
                this.TabContainer1_ActiveTabChanged(null, null);
                string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Xóa Ảnh] [Ảnh: " + imgName + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa Ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, News_ID, ConstAction.TSAnh);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            SetTotal();

        }
        protected void btnLinkTra_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                    TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    int PhotoID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _obj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text, txt_tienNB.Text, txtGhichu.Text, DateTime.Now);
                        int _return = _untilDAL.InsertT_Photo_Events(_obj);
                        string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                        ar.Add(double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            //if (TabContainer1.ActiveTabIndex == 0)
            //    LoadData();
            //else if (TabContainer1.ActiveTabIndex == 1)
            //    LoadData_Daduyet();
            //else if (TabContainer1.ActiveTabIndex == 2)
            //    LoadDataAnhBientaplai();
            for (int i = 0; i < ar.Count; i++)
            {
                double News_ID = double.Parse(ar[i].ToString());
                _obj = _untilDAL.GetOneFromT_Photo_EventsByID(News_ID);
                //dung sua: bai tra lai thi khong update Date_Update
                if (TabContainer1.ActiveTabIndex == 1)
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 7, _user.UserID, DateTime.Parse(_obj.Date_Update.ToString()));
                else
                    _untilDAL.UpdateStatus_Photo_Events(News_ID, 7, _user.UserID, DateTime.Now);
                this.TabContainer1_ActiveTabChanged(null, null);
                string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Trả Ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Trả Ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, News_ID, ConstAction.TSAnh);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            //SetTotal();

        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_Daduyet();
            }
            else if (TabContainer1.ActiveTabIndex == 2)
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
                imgbtnDuyet.Attributes.Add("onclick", "return confirm(\"Bạn có chắc muốn duyệt không?\");");

            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListCategory_ItemDataBound1(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete2 = (ImageButton)e.Item.FindControl("btnDelete2");

            if (btnDelete2 != null)
            {
                btnDelete2.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
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
            //    if (TabContainer1.ActiveTabIndex == 0)
            //        tab = 0;
            //    int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //    Response.Redirect("~/Anh24h/Edit_PhotoChoDuyet.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            //}
            //if (e.CommandArgument.ToString().ToLower() == "btnduyet")
            //{

            //    HPCInfo.T_Photo_Event _obj = new T_Photo_Event();
            //    double id = Convert.ToDouble(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //    _obj = _untilDAL.GetOneFromT_Photo_EventsByID(id);
            //    _untilDAL.UpdateStatus_Photo_Events(_obj.Photo_ID, 1, _user.UserID, DateTime.Now);
            //    //UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), "[Đăng Ảnh] [Ảnh: " + _obj.Photo_Name + "]", 0, "[Trả ảnh]", Convert.ToInt32(Request["Menu_ID"]));
            //    string _ActionsCode = "[Đăng ảnh] [Ảnh: " + _obj.Photo_Name + "]";
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Đăng Ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id);
            //}
            //if (e.CommandArgument.ToString().ToLower() == "delete")
            //{
            //    int id = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //    _untilDAL.DeleteFromT_Photo_Event(id);
            //string _ActionsCode = "[Xóa T_Photo_Event]-->[Thao tác Xóa Trong Bảng T_Photo_Event][ID:" + id + " ]";
            ////UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), _ActionsCode, 0, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]));
            //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id);
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
            Label lblTienNB = e.Item.FindControl("lblTienNB") as Label;
            Label lblGhichu = e.Item.FindControl("lblGhichu") as Label;
            Label lblUrlPath = e.Item.FindControl("lblUrlPath") as Label;
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            TextBox txtTacGia = e.Item.FindControl("txt_tacgia") as TextBox;
            TextBox txt_tienNB = e.Item.FindControl("txt_tienNB") as TextBox;
            TextBox txtGhichu = e.Item.FindControl("txtGhichu") as TextBox;
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
                lblTienNB.Visible = false;
                lblGhichu.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
                txt_tienNB.Visible = true;
                txtGhichu.Visible = true;
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
                    lblTienNB.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txt_tienNB.Visible = false;
                    _obj = setItem(_ID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txtTacGia.Text, txt_tienNB.Text, txtGhichu.Text, _obj.Date_Update);
                    int _return = _DAL.InsertT_Photo_Events(_obj);
                    btnEdit.Text = txtTitle.Text;
                    lblAnpham.Text = UltilFunc.GetTenAnpham(Convert.ToInt32(cboNgonNgu.SelectedValue));
                    lblTacGia.Text = txtTacGia.Text;
                    lblTienNB.Text = txt_tienNB.Text;
                    lblGhichu.Text = txtGhichu.Text;
                    string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh trong ngày] [Ảnh: " + _obj.Photo_Name + "]";
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
                    lblTienNB.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txt_tienNB.Visible = false;
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
                Response.Redirect("~/Anh24h/Edit_PhotoChoDuyet.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
            SetTotal();
        }
        public void grdListCategory_EditCommand1(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                if (TabContainer1.ActiveTabIndex == 1)
                    tab = 1;
                int catID = Convert.ToInt32(this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Anh24h/Edit_PhotoChoDuyet.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                int id = Convert.ToInt32(this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString());
                _untilDAL.DeleteFromT_Photo_Version(id);
                string _ActionsCode = "[Xóa T_Photo_Version]-->[Thao tác Xóa Trong Bảng T_Photo_Version][ID:" + this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id, ConstAction.TSAnh);
                this.LoadData();
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Daduyet();
            else if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
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
            Label lblTienNB = e.Item.FindControl("lblTienNB") as Label;
            Label lblGhichu = e.Item.FindControl("lblGhichu") as Label;
            Label lblUrlPath = e.Item.FindControl("lblUrlPath") as Label;
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            TextBox txtTacGia = e.Item.FindControl("txt_tacgia") as TextBox;
            TextBox txt_tienNB = e.Item.FindControl("txt_tienNB") as TextBox;
            TextBox txtGhichu = e.Item.FindControl("txtGhichu") as TextBox;
            int _ID = Convert.ToInt32(dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
            _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "editphoto")
            {
                btnSave.Visible = true;
                btnBack.Visible = true;
                btnModify.Visible = false;
                lblAnpham.Visible = false;
                lblTacGia.Visible = false;
                lblTienNB.Visible = false;
                lblGhichu.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
                txt_tienNB.Visible = true;
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
                    lblTienNB.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txt_tienNB.Visible = false;
                    txtGhichu.Visible = false;
                    _obj = setItem(_ID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txtTacGia.Text, txt_tienNB.Text, txtGhichu.Text, _obj.Date_Update);
                    int _return = _DAL.InsertT_Photo_Events(_obj);
                    btnEdit.Text = txtTitle.Text;
                    lblAnpham.Text = UltilFunc.GetTenAnpham(Convert.ToInt32(cboNgonNgu.SelectedValue));
                    lblTacGia.Text = txtTacGia.Text;
                    lblTienNB.Text = txt_tienNB.Text;
                    lblGhichu.Text = txtGhichu.Text;
                    string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh trong ngày] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
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
                    lblTienNB.Visible = true;
                    lblGhichu.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    txt_tienNB.Visible = false;
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
                if (TabContainer1.ActiveTabIndex ==2)
                    tab = 2;
                int catID = Convert.ToInt32(this.dgr_anhbientaplai.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Anh24h/Edit_PhotoChoDuyet.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
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
                imgbtnDuyet.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn đăng ảnh?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void pages_IndexChanged_AnhNgung(object sender, EventArgs e)
        {
            LoadDataAnhBientaplai();
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
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in dgr_anhbientaplai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        arrTin.Add(double.Parse(dgr_anhbientaplai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            T_Photo_EventDAL tt = new T_Photo_EventDAL();
            NgonNgu_DAL _LanguagesDAL = new NgonNgu_DAL();
            T_NgonNgu _obj = new T_NgonNgu();
            if (arrTin.Count > 0)
            {
                for (int j = 0; j < arrTin.Count; j++)
                {
                    double News_ID = double.Parse(arrTin[j].ToString());
                    if (tt.GetOneFromT_Photo_EventsByID(int.Parse(News_ID.ToString())).Lang_ID == 1)
                    {
                        for (int i = 0; i < arNgu.Count; i++)
                        {
                            //Thực hiện dịch ngữ
                            int Lang_ID = int.Parse(arNgu[i].ToString());
                            if (!HPCShareDLL.HPCDataProvider.Instance().ExitsTranlate_T_Photo_Even(int.Parse(News_ID.ToString()), Lang_ID))
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("LANQUAGE_ALERT") + "');", true);
                                //return;
                            }
                            else
                            {
                                tt.InsertT_Photo_Events_SendLanger(int.Parse(News_ID.ToString()), int.Parse(News_ID.ToString()), Lang_ID);
                                _obj = _LanguagesDAL.GetOneFromT_NgonNguByID(Lang_ID);
                                string _ActionsCode = "[Thời sự qua ảnh] [Dịch ngữ] [Ngữ: " + _obj.TenNgonNgu + "] [Ảnh: " + tt.GetOneFromT_Photo_EventsByID(News_ID).Photo_Name + "]";
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Dịch ngữ]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, 0, ConstAction.TSAnh);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn chưa chọn mục cần dịch ngữ!');", true);
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData();
            if (TabContainer1.ActiveTabIndex == 2)
                LoadDataAnhBientaplai();
            ModalPopupExtender1.Hide();
        }
        #endregion
    }
}
