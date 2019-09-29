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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Quangcao
{
    public partial class Edit_TPQC : BasePage
    {
        private bool _refreshState;
        private bool _isRefresh;
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] AllStates = (object[])savedState;
                base.LoadViewState(AllStates[0]);
                _refreshState = bool.Parse(AllStates[1].ToString());
                _isRefresh = _refreshState ==
                bool.Parse(Session["__ISREFRESH"].ToString());
            }
            catch
            { }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;
        UltilFunc _ulti = new UltilFunc();
        HPCBusinessLogic.DAL.QuangCaoDAL dal = new HPCBusinessLogic.DAL.QuangCaoDAL();
        T_QuangCao obj = new T_QuangCao();
        string ftpuser = ConfigurationManager.AppSettings["FTP_Username"].ToString();
        string password = ConfigurationManager.AppSettings["FTP_Password"].ToString();
        string ftpServer = ConfigurationManager.AppSettings["FTP_Server"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                }
                if (!IsPostBack)
                {
                    LoadCombox();
                    if (Request["ID"] != null)
                        PopulateItem(int.Parse(Request["ID"].ToString()));
                    else
                        LinkButtonBack.Visible = false;
                }
                else
                {
                    string EventName = Request.Form["__EVENTTARGET"].ToString();
                    if (EventName == "UploadImageSuccess")
                    {
                        LoadDataImage();
                    }
                }
            }
        }

        #region Methods
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboKhachhang, "Ma_KhachHang", "Ten_khachhang", "T_Khachhang", " 1=1 and Loai_KH=1", "---Select all---");
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", "---Select all---");
            UltilFunc.BindCombox(cbokichthuoc, "Ma_Kichthuoc", "Ten_Kichthuoc", "T_Kichthuoc", "1=1", "---Select all---");
        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboTrang.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cboTrang.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChontrang"), "0", true));
                for (int j = 1; j < _sotrang + 1; j++)
                {
                    cboTrang.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblTrang") + j.ToString(), j.ToString()));
                }
            }

        }
        private T_QuangCao SetItem(int _status)
        {
            T_QuangCao _obj = new T_QuangCao();
            if (Page.Request.Params["id"] != null)
                _obj.Ma_Quangcao = int.Parse(Page.Request["id"].ToString());
            else
                _obj.Ma_Quangcao = 0;
            _obj.Ten_QuangCao = txt_tenquangcao.Text.Trim();
            _obj.Noidung = ckeNoidung.Text.Trim();
            _obj.Trang = int.Parse(cboTrang.SelectedValue.ToString());
            _obj.Kichthuoc = int.Parse(cbokichthuoc.SelectedValue);
            _obj.Ma_KhachHang = int.Parse(cboKhachhang.SelectedValue.ToString());
            _obj.Ma_Loaibao = int.Parse(cboAnPham.SelectedValue.ToString());
            _obj.Ngaydang = UltilFunc.ToDate(txtNgaybatdau.Text.Trim(), "dd/MM/yyyy");
            _obj.NgayTao = DateTime.Now;
            _obj.Nguoitao = _user.UserID;
            _obj.Trangthai = _status;

            return _obj;
        }
        private void PopulateItem(int _id)
        {
            T_QuangCao _objqc = new T_QuangCao();
            HPCBusinessLogic.DAL.QuangCaoDAL _QCDAL = new HPCBusinessLogic.DAL.QuangCaoDAL();
            _objqc = _QCDAL.GetOneFromT_QuangCaoByID(_id);
            txt_tenquangcao.Text = _objqc.Ten_QuangCao;
            ckeNoidung.Text = _objqc.Noidung;
            cboKhachhang.SelectedIndex = CommonLib.GetIndexControl(cboKhachhang, _objqc.Ma_KhachHang.ToString());
            cbokichthuoc.SelectedIndex = CommonLib.GetIndexControl(cbokichthuoc, _objqc.Kichthuoc.ToString());
            cboAnPham.SelectedIndex = CommonLib.GetIndexControl(cboAnPham, _objqc.Ma_Loaibao.ToString());

            bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
            if (_objqc.Trang != 0)
            {
                string _sotrang = _objqc.Trang.ToString();
                cboTrang.SelectedIndex = int.Parse(_sotrang);
            }
            else
                cboTrang.SelectedIndex = 0;

            txtNgaybatdau.Text = _objqc.Ngaydang.ToString("dd/MM/yyyy");
            LoadDataImage();
            ListNgaydangQC();
        }
        #endregion

        #region Event Click
        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string Thaotac = "";
            if (!_isRefresh)
            {
                obj = SetItem(21);
                int _return = dal.InsertT_QuangCao(obj);
                SaveNgaydangQC(_return);
                SaveImagesAttachAll(_return);
                if (Request["ID"] != null)
                    Thaotac = "Sửa thông tin quảng cáo ID: " + Request["ID"].ToString();
                else
                    Thaotac = "Thêm mới thông tin quảng cáo ID: " + _return.ToString();
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);

                Page.Response.Redirect("~/Quangcao/Edit_TPQC.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _return + "&Tab=" + Request["Tab"].ToString());
            }
        }
        protected void linkSend_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                obj = SetItem(41);
                obj.Ma_Quangcao = dal.InsertT_QuangCao(obj);
                dal.Sp_InsertT_PhienBanQuangCao(obj.Ma_Quangcao);
                SaveImagesAttachAll(obj.Ma_Quangcao);
                SaveNgaydangQC(obj.Ma_Quangcao);
                string Thaotac = "Gửi quảng cáo từ TP-QC đến TBT-QC ID: " + obj.Ma_Quangcao.ToString();
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                Page.Response.Redirect("~/Quangcao/List_TPQC.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
            }
        }
        protected void LinkGuiDT_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                obj = SetItem(6);
                obj.Ma_Quangcao = dal.InsertT_QuangCao(obj);
                dal.Sp_InsertT_PhienBanQuangCao(obj.Ma_Quangcao);
                SaveImagesAttachAll(obj.Ma_Quangcao);
                SaveNgaydangQC(obj.Ma_Quangcao);
                string Thaotac = "Gửi quảng cáo từ TP-QC đến DT-QC ID: " + obj.Ma_Quangcao.ToString();
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);

                Page.Response.Redirect("~/Quangcao/List_TPQC.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
            }
        }
        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                int Nguoitao = 0;
                Nguoitao = UltilFunc.GetColumnValuesOne("T_PhienBanQuangCao", "Nguoitao", " Ma_Quangcao=" + Request["ID"].ToString() + " and Trangthai=21");
                if (Nguoitao != 0)
                {
                    obj = SetItem(13);
                    obj.Ma_Quangcao = dal.InsertT_QuangCao(obj);
                    dal.Sp_UpdateRowT_QuangCao(obj.Ma_Quangcao, Nguoitao, 13);
                    dal.Sp_InsertT_PhienBanQuangCao(obj.Ma_Quangcao);
                    string Thaotac = "Gửi trả lại quảng cáo từ TP.QC về NV-QC ID: " + obj.Ma_Quangcao.ToString();
                    UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                    Page.Response.Redirect("~/Quangcao/List_TPQC.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
                }
                else
                {
                    FuncAlert.AlertJS(this, "không thể trả lại vì QC không phải của NVQC");
                    return;
                }
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Quangcao/List_TPQC.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
        }
        #endregion

        #region File Attach
        public void LoadDataImage()
        {
            string where = "";
            if (Request["ID"] != null)
                where = " Ma_Quangcao=" + Request["ID"].ToString();
            else
                where = " Ma_Quangcao=0 and NguoiTao=" + _user.UserID + " and NgayTao>='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";

            DataSet _ds = dal.Sp_SelectT_FileQuangCaoDynamic(where, " NgayTao DESC ");
            this.dgrListImages.DataSource = _ds;
            this.dgrListImages.DataBind();

        }
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        public bool SaveImagesAttachAll(double _maquangcao)
        {
            int _ID = 0;
            if (dgrListImages.Items.Count > 0)
            {
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    _ID = Convert.ToInt32(dgrListImages.DataKeys[m_Item.ItemIndex].ToString());
                    if (_ID != 0)
                    {
                        string SqlUpdate = "";
                        SqlUpdate = "update T_FileQuangCao set Ma_Quangcao=" + _maquangcao + " where ID=" + _ID;
                        _ulti.ExecSql(SqlUpdate);

                    }
                }

            }
            return true;
        }
        protected void btnXoaAnh_Click(object sender, EventArgs e)
        {
            UploadFileMulti.FtpClient _Ftp = new ToasoanTTXVN.UploadFileMulti.FtpClient(ftpServer, ftpuser, password, "");
            string SqlUpdate = string.Empty;
            foreach (DataListItem m_Item in dgrListImages.Items)
            {
                int _ID = int.Parse(this.dgrListImages.DataKeys[m_Item.ItemIndex].ToString());

                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;
                if (chk_Select != null && chk_Select.Checked)
                {
                    dal.Sp_DeleteOneFromT_FileQuangCao(_ID);
                    string thaotac = "Thao tác xóa ảnh đính kèm: " + lbFileAttach.Text + " tại NV QC: " + txt_tenquangcao.Text.Trim();
                    UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), thaotac);
                    try
                    {
                        _Ftp.DeleteFile(lbFileAttach.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            LoadDataImage();

        }
        public void dgrListImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            int _ID = Convert.ToInt32(dgrListImages.DataKeys[e.Item.ItemIndex].ToString());
            UploadFileMulti.FtpClient _Ftp = new ToasoanTTXVN.UploadFileMulti.FtpClient(ftpServer, ftpuser, password, "");
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                Label lbFileAttach = e.Item.FindControl("lbFileAttach") as Label;
                try
                {
                    if (_Ftp.IsExistFolder(lbFileAttach.Text.Trim()))
                        _Ftp.DeleteFile(lbFileAttach.Text.Trim());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                dal.Sp_DeleteOneFromT_FileQuangCao(_ID);
                string thaotac = "Thao tác xóa ảnh đính kèm: " + lbFileAttach.Text + " tại NV QC: " + txt_tenquangcao.Text.Trim();
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), thaotac);
                LoadDataImage();
            }
        }
        #endregion

        #region Add ngay dang QC

        public void ListNgaydangQC()
        {
            string where = "";
            if (Request["ID"] != null)
                where = " Quangcao_ID=" + Request["ID"].ToString();
            else
                where = " Quangcao_ID=0 and NguoiTao=" + _user.UserID + " and Ngaydang>='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";

            DataSet _ds = dal.Sp_SelectT_Publish_QuangCaoDynamic(where, " Ngaydang DESC ");
            this.DataGridDatePublish.DataSource = _ds;
            this.DataGridDatePublish.DataBind();

        }
        public bool SaveNgaydangQC(double _maquangcao)
        {
            int _ID = 0;
            if (DataGridDatePublish.Items.Count > 0)
            {
                foreach (DataGridItem m_Item in DataGridDatePublish.Items)
                {
                    _ID = Convert.ToInt32(DataGridDatePublish.DataKeys[m_Item.ItemIndex].ToString());
                    if (_ID != 0)
                    {
                        string SqlUpdate = "";
                        SqlUpdate = "update T_Publish_QuangCao set Quangcao_ID=" + _maquangcao + " where ID=" + _ID;
                        _ulti.ExecSql(SqlUpdate);

                    }
                }

            }
            return true;
        }
        protected void LinkAddNgayDang_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                if (txtNgaybatdau.Text.Trim() == "")
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonngaydangquangcao"));
                    return;
                }
                if (UltilFunc.ToDate(this.txtNgaybatdau.Text, "dd/MM/yyyy") < DateTime.Now)
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblCanhbaongaydangQCphailonhonngayhientai"));
                    return;
                }
                if (cboAnPham.SelectedIndex == 0)
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonanpham"));
                    return;
                }
                string _sql = " set dateformat dmy select Ma_Sobao from T_Sobao where Ngay_Xuatban='" + txtNgaybatdau.Text.Trim() + "' and Ma_AnPham=" + cboAnPham.SelectedValue;
                DataTable _dt = _ulti.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0 && _dt != null)
                {
                    T_Publish_QuangCao _obj = new T_Publish_QuangCao();
                    _obj.ID = 0;
                    if (Request["ID"] != null)
                        _obj.Quangcao_ID = int.Parse(Request["ID"].ToString());
                    else
                        _obj.Quangcao_ID = 0;
                    _obj.Ma_Loaibao = int.Parse(cboAnPham.SelectedValue);
                    _obj.Ma_Sobao = int.Parse(_dt.Rows[0]["Ma_Sobao"].ToString());
                    _obj.Trang = int.Parse(cboTrang.SelectedValue);
                    _obj.Ngaydang = UltilFunc.ToDate(txtNgaybatdau.Text.Trim(), "dd/MM/yyyy");
                    _obj.Nguoitao = _user.UserID;
                    dal.Sp_InsertT_Publish_QuangCao(_obj);
                    ListNgaydangQC();
                }
                else
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblCanhbaongaydangQC"));
                    return;
                }
            }
        }
        protected void DataGridDatePublish_EditCommand(object source, DataGridCommandEventArgs e)
        {
            double _ID = double.Parse(DataGridDatePublish.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                dal.Sp_DeleteOneFromT_Publish_QuangCao(_ID);
                ListNgaydangQC();
            }

        }
        protected void DataGridDatePublish_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null)
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion
    }
}
