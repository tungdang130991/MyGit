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
using System.IO;
using HPCInfo;
using ToasoanTTXVN.BaoDienTu;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Anh24h
{
    public partial class AddMutiPhotos : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected T_RolePermission _Role = null;
        #endregion
        //protected int cat_id = 0;
        //protected double LangID = 0;
        protected int status = 0;
        protected int tab = 0;
        protected int pageback = 0;
        protected string menuName = "";
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

                    this.LinkCancel.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListCate');");
                    if (Page.Request.Params["status"] != null)
                    {
                        if (CommonLib.IsNumeric(Page.Request.Params["status"].ToString()))
                            status = int.Parse(Page.Request.Params["status"].ToString());
                    }
                    if (Page.Request.Params["Tab"] != null)
                    {
                        if (CommonLib.IsNumeric(Page.Request.Params["Tab"].ToString()))
                            tab = int.Parse(Page.Request.Params["Tab"].ToString());
                    }
                    //LangID = PopulateItem(cat_id).Lang_ID;
                    try { pageback = int.Parse(Session["PageFromID"].ToString()); }
                    catch { ;}
                    if (!IsPostBack)
                    {
                        CheckPermission();
                        //if ()
                        //{
                        if (Request["BackID"] != null && Request["BackID"] != "")
                        {
                            int page_index = 0;
                            try { page_index = int.Parse(Session["PageIndex_DetailCAT"].ToString()); }
                            catch { ;}
                            pages.PageIndex = page_index;
                        }
                        LoadData(status);
                        //}
                    }
                }
            }
        }
        public bool CheckPermission()
        {
            if (pageback == 1)// chuc nang bien tap
            {
                menuName = "[Danh sách ảnh đề xuất]";
                LinkDangAnh1.Text = "Gửi duyệt";
                this.LinkDangAnh1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn gửi Duyệt ?','ctl00_MainContent_grdListCate');");
                if (status == 5)
                {
                    FileUpload1.Visible = true;
                    linkSave.Visible = true;
                    LinkCancel.Visible = true;
                    LinkDangAnh1.Visible = true;
                    Link_Back.Visible = true;
                    LinkTrans.Visible = false;
                    LinkTra.Visible = false;
                    return true;
                }
                else
                {
                    FileUpload1.Visible = false;
                    LinkTrans.Visible = false;
                    LinkTra.Visible = false;
                    return false;
                }
            }
            else if (pageback == 2)// chuc nang duyet bai
            {
                menuName = "[Duyệt ảnh thời sự]";
                LinkDangAnh1.Text = "Đăng ảnh";
                this.LinkDangAnh1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có muốn Đăng ảnh ?','ctl00_MainContent_grdListCate');");
                if (status == 2)
                {
                    FileUpload1.Visible = false;
                    LinkTra.Visible = false;
                    LinkTrans.Visible = true;
                    return false;
                }
                else
                {
                    FileUpload1.Visible = false;
                    LinkTra.Visible = true;
                    LinkTrans.Visible = true;
                    return true;
                }
            }
            else if (pageback == 3)// chuc nang xuat ban
            {
                //if (status != 3)
                //{
                FileUpload1.Visible = false;
                //linkSave.Enabled = false;
                //LinkCancel.Enabled = false;
                return false;
                //}
                //else
                //{
                //    FileUpload1.Visible = true;
                //    linkSave.Enabled = true;
                //    LinkCancel.Enabled = true;
                //    return true;
                //}
            }
            else
            {
                FileUpload1.Visible = false;
                linkSave.Visible = false;
                LinkCancel.Visible = false;
                LinkTrans.Visible = false;
                LinkTra.Visible = false;
                return false;
            }

        }
        #region "Event Click and method"
        protected void LinkEdit_Click(object sender, EventArgs e) {
            int count = 0;
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
                T_Photo_Event _obj = new T_Photo_Event();
                ImageButton btnModify = m_Item.FindControl("btnModify") as ImageButton;
                ImageButton btnSave = m_Item.FindControl("btnSave") as ImageButton;
                ImageButton btnBack = m_Item.FindControl("btnBack") as ImageButton;
                Label lblNgonNgu = m_Item.FindControl("lblNgonNgu") as Label;
                LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                btnSave.Visible = true;
                btnBack.Visible = true;
                btnModify.Visible = false;
                lblNgonNgu.Visible = false;
                lblTacGia.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
                if (count == 0)
                {
                    txtTitle.Focus();
                }
                count++;
                litMessages.Text = "";
            }
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Duyet danh sach cac doi tuong tren luoi"
                T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
                T_Photo_Event _catObj = new T_Photo_Event();
                //HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                    int PhotoID = Convert.ToInt32(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text);
                    _cateDAL.InsertT_Photo_Events(_catObj);
                }
                LoadData(status);
                this.litMessages.Text = "Lưu giữ thành công";
                #endregion
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }

        }

        protected void btnLinkDuyetAnh_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
            T_Photo_Event _catObj = new T_Photo_Event();
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                double PhotoID = double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                _catObj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text);
                _cateDAL.InsertT_Photo_Events(_catObj);
                if (chk_Select != null && chk_Select.Checked)
                {
                    T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
                    T_Photo_Event _obj = new T_Photo_Event();
                    _obj = _untilDAL.GetOneFromT_Photo_EventsByID(PhotoID);
                    if (pageback == 1)
                    {
                        if (_obj.Photo_Name.Trim().Length > 0)
                        {
                            _untilDAL.UpdateStatus_Photo_Events(PhotoID, 8, _user.UserID, DateTime.Now);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                        }
                    }
                    else if (pageback == 2)
                    {
                        _untilDAL.UpdateStatus_Photo_Events(PhotoID, 3, _user.UserID, DateTime.Now);
                        #region Sync
                        // DONG BO ANH
                        SynFiles _syncfile = new SynFiles();
                        if (_obj.Photo_Medium.Length > 0)
                        {
                            _syncfile.SynData_UploadImgOne(_obj.Photo_Medium, HPCComponents.Global.ImagesService);
                        }
                        //END
                        #endregion
                    }
                    string _ActionsCode = "[Thời sự qua ảnh] " + menuName + " [Gửi duyệt Ảnh] [Ảnh: " + _untilDAL.GetOneFromT_Photo_EventsByID(PhotoID).Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Gửi duyệt]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, PhotoID,ConstAction.TSAnh);

                }
            }
            LoadData(status);
        }
        protected void btnLinkTra_Click(object sender, EventArgs e)
        {
            T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
            T_Photo_Event _catObj = new T_Photo_Event();
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
                double PhotoID = double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                _catObj = setItem(int.Parse(PhotoID.ToString()), lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text);
                _cateDAL.InsertT_Photo_Events(_catObj);
                if (chk_Select != null && chk_Select.Checked)
                {
                    T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
                    T_Photo_Event _obj = new T_Photo_Event();
                    _obj = _untilDAL.GetOneFromT_Photo_EventsByID(PhotoID);
                    _untilDAL.UpdateStatus_Photo_Events(PhotoID, 7, _user.UserID, DateTime.Now);
                    string _ActionsCode = "[Thời sự qua ảnh] " + menuName + " [Trả Ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Trả Ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, PhotoID, ConstAction.TSAnh);
                }
            }
            LoadData(status);
        }
        protected void Link_Back_Click(object sender, EventArgs e)
        {
            //T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
            //T_Photo_Event _catObj = new T_Photo_Event();
            ////HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            //foreach (DataGridItem m_Item in grdListCate.Items)
            //{
            //    TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
            //    TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
            //    Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
            //    DropDownList cboNgonNgu = (DropDownList)m_Item.FindControl("cboNgonNgu");
            //    int PhotoID = Convert.ToInt32(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
            //    _catObj = setItem(PhotoID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txttacgia.Text);
            //    _cateDAL.InsertT_Photo_Events(_catObj);
            //}
            if (pageback == 1)
            {
                Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + tab);
            }
            else if (pageback == 2)
                Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + tab);
            else if (pageback == 3)
                Page.Response.Redirect("~/Anh24h/List_PhotoDaDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + tab);
            else
                Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());

        }

        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    int ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
                    T_Photo_Event _obj = new T_Photo_Event();
                    _obj = _DAL.GetOneFromT_Photo_EventsByID(ID);
                    _DAL.DeleteFromT_Photo_Event(ID);
                    string _ActionsCode = "[Thời sự qua ảnh] " + menuName + " [Xóa Ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, ID, ConstAction.TSAnh);

                }
            }
            LoadData(status);
        }
        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            ImageButton btnModify = e.Item.FindControl("btnModify") as ImageButton;
            ImageButton btnSave = e.Item.FindControl("btnSave") as ImageButton;
            ImageButton btnBack = e.Item.FindControl("btnBack") as ImageButton;
            Label lblNgonNgu = e.Item.FindControl("lblNgonNgu") as Label;
            LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
            Label lblTacGia = e.Item.FindControl("lblTacGia") as Label;
            Label lblUrlPath = e.Item.FindControl("lblUrlPath") as Label;
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            TextBox txtTitle = e.Item.FindControl("txtTitle") as TextBox;
            TextBox txtTacGia = e.Item.FindControl("txt_tacgia") as TextBox;
            //Image imgView = e.Item.FindControl("imgView") as Image;
            //Image imgBrowse = e.Item.FindControl("imgBrowse") as Image;
            int _ID = Convert.ToInt32(grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "editphoto")
            {
                btnSave.Visible = true;
                btnBack.Visible = true;
                btnModify.Visible = false;
                lblNgonNgu.Visible = false;
                lblTacGia.Visible = false;
                btnEdit.Visible = false;
                cboNgonNgu.Visible = true;
                txtTitle.Visible = true;
                txtTacGia.Visible = true;
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
                    lblNgonNgu.Visible = true;
                    lblTacGia.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    //imgView.Visible = true;
                    //imgBrowse.Visible = false;
                    _obj = setItem(_ID, lblUrlPath.Text, txtTitle.Text, Convert.ToInt32(cboNgonNgu.SelectedValue), txtTacGia.Text);
                    _DAL.InsertT_Photo_Events(_obj);
                }
                else
                {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
                LoadData(status);
            }
            if (e.CommandArgument.ToString().ToLower() == "back")
            {
                if (txtTitle.Text.Trim().Length > 0)
                {
                    btnSave.Visible = false;
                    btnBack.Visible = false;
                    btnModify.Visible = true;
                    lblNgonNgu.Visible = true;
                    lblTacGia.Visible = true;
                    btnEdit.Visible = true;
                    cboNgonNgu.Visible = false;
                    txtTitle.Visible = false;
                    txtTacGia.Visible = false;
                    //imgView.Visible = true;
                    //imgBrowse.Visible = false;
                }
                else {
                    txtTitle.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                //if (TabContainer1.ActiveTabIndex == 0)
                //tab = 0;
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Anh24h/Edit_PhotoDexuat.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID + "&Tab=" + tab);
            }
        }
        public void grdListCategory_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DropDownList cboNgonNgu = e.Item.FindControl("cboNgonNgu") as DropDownList;
            Label lblLangId = e.Item.FindControl("lblLangId") as Label;
            if (cboNgonNgu != null)
            {
                UltilFunc.BindCombox(cboNgonNgu, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" hoatdong=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, lblLangId.Text.ToString());
            }
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData(status);
        }

        #endregion

        #region "Load and Populate data"

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
            return "Menu_ID=" + Request["Menu_ID"].ToString() + "&PageFromID=" + pageback + "&status=5";
        }

        private T_Photo_Event setItem(int PhotoID, string urlImage, string PhotoTitle, int LangId, string tacgia)
        {
            T_Photo_Event _objPoto = new T_Photo_Event();
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            _objPoto.Photo_ID = PhotoID;
            _objPoto = _DAL.GetOneFromT_Photo_EventsByID(PhotoID);
            if (_objPoto.Photo_Status == 7)
                _objPoto.Date_Update = _objPoto.Date_Update;
            else
                _objPoto.Date_Update = DateTime.Now;
            _objPoto.Photo_Name = PhotoTitle;
            _objPoto.Photo_Medium = urlImage;
            _objPoto.Author_Name = tacgia;
            _objPoto.Lang_ID = LangId;
            _objPoto.Date_Create = DateTime.Now;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Status = status;
            return _objPoto;
        }

        private T_Album_Categories PopulateItem(int _id)
        {
            HPCInfo.T_Album_Categories _cateObj = new HPCInfo.T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _cateObj = _cateDAL.load_T_Album_Categories(_id);
            return _cateObj;
        }

        private void LoadData(int status)
        {
            string where = " 1=1 ";
            if (status == 5 || status == 7)
            {
                where += " and Creator=" + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND Photo_Status = " + status + " AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
            }
            else
            {
                where += " and Photo_Status = " + status + " AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
            }
            where += " Order by Date_Create DESC";
            pages.PageSize = 50;
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
                    Label lblNgonNgu = m_Item.FindControl("lblNgonNgu") as Label;
                    LinkButton btnEdit = m_Item.FindControl("btnEdit") as LinkButton;
                    Label lblTacGia = m_Item.FindControl("lblTacGia") as Label;
                    DropDownList cboNgonNgu = m_Item.FindControl("cboNgonNgu") as DropDownList;
                    TextBox txtTitle = m_Item.FindControl("txtTitle") as TextBox;
                    TextBox txtTacGia = m_Item.FindControl("txt_tacgia") as TextBox;
                    //Image imgView = m_Item.FindControl("imgView") as Image;
                    //Image imgBrowse = m_Item.FindControl("imgBrowse") as Image;
                    if (txtTitle.Text.Trim().Length > 0)
                    {
                        btnSave.Visible = false;
                        btnBack.Visible = false;
                        btnModify.Visible = true;
                        lblNgonNgu.Visible = true;
                        lblTacGia.Visible = true;
                        btnEdit.Visible = true;
                        cboNgonNgu.Visible = false;
                        txtTitle.Visible = false;
                        txtTacGia.Visible = false;
                        //imgView.Visible = true;
                        //imgBrowse.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        btnModify.Visible = false;
                        lblNgonNgu.Visible = false;
                        lblTacGia.Visible = false;
                        btnEdit.Visible = false;
                        cboNgonNgu.Visible = true;
                        txtTitle.Visible = true;
                        txtTacGia.Visible = true;
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
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    arrTin.Add(double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
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
                                string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Dịch ngữ] [Ngữ: " + _obj.TenNgonNgu + "] [Ảnh: " + tt.GetOneFromT_Photo_EventsByID(News_ID).Photo_Name + "]";
                                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Dịch ngữ]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, int.Parse(News_ID.ToString()), ConstAction.TSAnh);
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
            LoadData(status);
            ModalPopupExtender1.Hide();
        }
        #endregion
    }
}
