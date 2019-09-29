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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ToasoanTTXVN.Quytrinh
{
    public partial class Edit_PV : BasePage
    {

        TinBaiAnhDAL _DAL = new TinBaiAnhDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;
        AnhDAL _dalanh = new AnhDAL();
        T_Anh _objimg = new T_Anh();
        HPCBusinessLogic.DAL.TinBaiDAL _Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();

        T_TinBai _obj = new T_TinBai();
        TinBaiAnhDAL _daltinanh = new TinBaiAnhDAL();
        SSOLibDAL lib = new SSOLibDAL();
        UltilFunc Ulti = new UltilFunc();
        private double MaDetai = 0;
        private double _id_newsonline = 0;
        private int _statusvnonline = int.Parse(CommonLib.ReadXML("Status_BDT"));
        double _matinbai = 0;
        int Ma_QTBT = 0;

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
                    this.btnXoaAnh.Attributes.Add("onclick", "return ConfirmQuestion('" + (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanmuonxoa") + "','ctl00_MainContent_dgrListPhotoOfNews_ctl01_chkAll');");
                }
                Ma_QTBT = UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + _user.UserID);
                if (Request["DT_id"] != null)
                    MaDetai = double.Parse(Request["DT_id"]);
                else
                    MaDetai = 0;
                if (Request["ID"] != null)
                {
                    _id_newsonline = UltilFunc.GetColumnValuesOne("T_News", "News_ID", "RefID=" + Request["ID"]);
                    _statusvnonline = UltilFunc.GetColumnValuesOne("T_News", "News_Status", "RefID=" + Request["ID"]);
                    MaDetai = UltilFunc.GetColumnValuesOne("T_Vitri_Tinbai", "Ma_Congviec", "Ma_Tinbai=" + Request["ID"]);
                }
                if (!IsPostBack)
                {
                    if (_user == null)
                    {
                        Page.Response.Redirect("~/login.aspx", true);
                    }
                    else
                    {
                        cbo_AnPham.Enabled = true;
                        LoadCombox();
                        if (Request["ID"] != null)
                        {
                            txtID.Text = Request["ID"].ToString();
                            PopulateItem(int.Parse(Request["ID"].ToString()));
                        }
                        else
                            Session["matinbai"] = null;
                        BindDoiTuongGuiBai();
                    }

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

        protected void AceoffixCtrl1_Load(object sender, EventArgs e)
        {

            AceoffixCtrl_Noidung.ServerPage = "../aceoffix-runtime/server.aspx";
            // Create custom menu
            //AceoffixCtrl_Noidung.
            AceoffixCtrl_Noidung.Caption = "WORD-EDITOR ONLINE";
            AceoffixCtrl_Noidung.Menubar = false;
            AceoffixCtrl_Noidung.CustomToolbar = false;
            AceoffixCtrl_Noidung.OfficeToolbars = true;

            if (Request["ID"] != null)
                AceoffixCtrl_Noidung.SaveFilePage = "../word/savefile.aspx?matinbai=" + Request["ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString();
            else
                AceoffixCtrl_Noidung.SaveFilePage = "../word/savefile.aspx?MaDoiTuong=" + Request["MaDoiTuong"].ToString();
            if (Request["ID"] != null)
            {
                string pathfile = _Daltinbai.load_T_news(double.Parse(Request["ID"].ToString())).PathFileDocuments;
                if (pathfile != "" && pathfile != DBNull.Value.ToString())
                    if (File.Exists(Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + pathfile)))
                        AceoffixCtrl_Noidung.OpenDocument(Global.TinPath + pathfile, Aceoffix.OpenModeType.docNormalEdit, _user.UserName);
                    else
                        AceoffixCtrl_Noidung.CreateNewDocument(_user.UserName, Aceoffix.DocumentVersion.Word2007);
                else
                    AceoffixCtrl_Noidung.CreateNewDocument(_user.UserName, Aceoffix.DocumentVersion.Word2007);
            }
            else
            {
                AceoffixCtrl_Noidung.CreateNewDocument(_user.UserName, Aceoffix.DocumentVersion.Word2007);
            }

        }

        #region Method

        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbo_AnPham, "Ma_Anpham", "Ten_AnPham", "T_AnPham", " Ma_QT in (select Ma_QTBT from T_NguoidungQTBT where Ma_Nguoidung=" + _user.UserID + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
            //cbo_AnPham.SelectedValue = Global.DefaultCombobox;
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
        }
        private T_TinBai SetItem(string _Doituongxuly, int _status)
        {

            if (double.Parse(txtID.Text.Trim()) > 0)
                _obj.Ma_Tinbai = double.Parse(txtID.Text.Trim());
            else
                _obj.Ma_Tinbai = double.Parse(Session["matinbai"].ToString());
            _obj.Tieude = UltilFunc.CleanFormatTags(Txt_tieude.Text.Trim());

            _obj.TacGia = txt_PVCTV.Text.Trim().Replace(" -- ", "|").Split('|')[0];
            _obj.Ma_TacGia = int.Parse(HiddenFieldTacgiatin.Text);
            _obj.Ma_Anpham = int.Parse(cbo_AnPham.SelectedValue);
            _obj.Ma_Chuyenmuc = int.Parse(cbo_chuyenmuc.SelectedValue);
            _obj.NgayTao = DateTime.Now;
            _obj.Ma_Nguoitao = _user.UserID;
            _obj.Nguoi_Khoa = _user.UserID;
            _obj.Trangthai_Xoa = false;
            if (Txt_Comments.Text != "")
                _obj.GhiChu = UltilFunc.CleanFormatTags(Txt_Comments.Text);
            else
                _obj.GhiChu = "";
            _obj.Trangthai = _status;
            _obj.Doituong_DangXuly = _Doituongxuly;
            _obj.LuuVet = _Daltinbai.GetTrace(_obj.Ma_Tinbai);
            _obj.CopyFrom = 0;
            _obj.Ma_QTBT = Ma_QTBT;
            _obj.Bizhub = checkbizbub.Checked;
            _obj.VietNamNews = checkbaoonline.Checked;
            _obj.UpdateContents = 1;
            int startchar = Global.Pathfiledoc.Substring(1, Global.Pathfiledoc.Length - 1).IndexOf("/");
            startchar += 1;
            string _Filedoc = Global.Pathfiledoc.Substring(startchar, Global.Pathfiledoc.Length - startchar);
            if (Page.Request["ID"] != null)
                _obj.PathFileDocuments = _Filedoc + _user.UserID.ToString() + _user.UserName + Request["ID"].ToString() + "_" + Request["MaDoiTuong"].ToString() + ".doc";
            else
                _obj.PathFileDocuments = _Filedoc + _user.UserID.ToString() + _user.UserName + Session["matinbai"].ToString() + "_" + Request["MaDoiTuong"].ToString() + ".doc";
            return _obj;
        }
        private T_Tinbai_Anh SetTinAnhItem(int _Matinbai, int _Maanh, string _chuthich)
        {
            T_Tinbai_Anh _obj = new T_Tinbai_Anh();
            _obj.ID = 0;
            _obj.Ma_TinBai = _Matinbai;
            _obj.Ma_Anh = _Maanh;
            _obj.ChuThich = _chuthich;
            return _obj;
        }
        private T_NhuanBut SetNhuanButItem(int _Matinbai, int _Maanh, double _sotien)
        {
            T_NhuanBut _obj = new T_NhuanBut();
            _obj.Ma_NhuanBut = 0;
            _obj.Ma_TinBai = _Matinbai;
            _obj.Ma_Anh = _Maanh;
            _obj.Sotien = _sotien;
            _obj.Ma_tacgia = 0;
            _obj.GHICHU = "";
            return _obj;
        }
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        private void PopulateItem(int _id)
        {

            T_TinBai _obj = new T_TinBai();
            ChuyenmucDAL _dalcm = new ChuyenmucDAL();
            _obj = _Daltinbai.load_T_news(_id);
            Txt_tieude.Text = _obj.Tieude;
            Ma_QTBT = _obj.Ma_QTBT;
            DataTable dtnguoitao = _Daltinbai.Sp_SelectOneNguoitaoTinBai(_id).Tables[0];
            if (dtnguoitao != null && dtnguoitao.Rows.Count > 0)
            {
                if (int.Parse(dtnguoitao.Rows[0][0].ToString()) != _user.UserID)
                {
                    txt_PVCTV.Text = _obj.TacGia;
                    HiddenFieldTacgiatin.Text = _obj.Ma_TacGia.ToString();
                    txt_PVCTV.ReadOnly = true;

                }
                else
                {
                    txt_PVCTV.Text = _obj.TacGia;
                    HiddenFieldTacgiatin.Text = _obj.Ma_TacGia.ToString();
                }
            }
            else
            {
                txt_PVCTV.Text = _obj.TacGia;
                HiddenFieldTacgiatin.Text = _obj.Ma_TacGia.ToString();
            }

            if (_obj.GhiChu != "")
                Txt_Comments.Text = _obj.GhiChu;
            else
                Txt_Comments.Text = "";
            if (_obj.Ma_Anpham > 0)
                cbo_AnPham.SelectedIndex = CommonLib.GetIndexControl(cbo_AnPham, _obj.Ma_Anpham.ToString());

            cbo_chuyenmuc.Items.Clear();

            if (cbo_AnPham.SelectedIndex > 0)
            {
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_AnPham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");

                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, _obj.Ma_Chuyenmuc.ToString());

            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
            }
            checkbaoonline.Checked = _obj.VietNamNews;
            checkbizbub.Checked = _obj.Bizhub;

            LoadDataImage();

        }
        private void GuiTinBai(string MaDoiTuong)
        {
            string Thaotac = "";
            string _trace = string.Empty;

            _obj = SetItem(MaDoiTuong, 1);
            txtID.Text = Convert.ToString(_Daltinbai.SP_UpdateT_TinBai_WordOnline(_obj));
            _matinbai = double.Parse(txtID.Text.Trim());
            if (MaDetai != 0)
                _Daltinbai.InsertT_Vitri_Tinbai_FromT_Tinbai(0, 0, _matinbai, MaDetai);
            SaveImagesAttachAll();
            _trace = _Daltinbai.GetTrace(_matinbai) + Request["MaDoiTuong"] + "_" + _user.UserID + ";";
            _Daltinbai.Update_Status_tintuc(_matinbai, 1, _user.UserID, DateTime.Now, MaDoiTuong, _trace);
            _Daltinbai.Insert_Phienban_From_T_Tinbai(_matinbai, _user.UserID, DateTime.Now, Request["MaDoiTuong"].ToString());
            _Daltinbai.Insert_Tulieu_From_T_Tinbai(_matinbai);
            Thaotac = "Thao tác gửi tin bài từ " + CommonLib.GetTenDoiTuong(Request["MaDoiTuong"].ToString()) + " đến " + CommonLib.GetTenDoiTuong(MaDoiTuong.ToString()) + " - Tiêu đề:" + Txt_tieude.Text.Trim();
            UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, Thaotac, _matinbai);
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);

            if (checkbaoonline.Checked)
            {
                HPCBusinessLogic.DAL.T_NewsDAL _T_NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                T_News _objT_News = SetItemBaoDienTu(_matinbai);
                int _returnvnonline = 0;
                // Insert sang bao dien tu
                if (_statusvnonline == int.Parse(CommonLib.ReadXML("Status_BDT")) || _statusvnonline == 0)
                {
                    _returnvnonline = _T_NewsDAL.InsertT_news(_objT_News);
                    _T_NewsDAL.Update_Status_tintuc(_returnvnonline, int.Parse(CommonLib.ReadXML("Status_BDT")), _user.UserID, DateTime.Now);
                    UltilFunc.Insert_News_Image(_objT_News.News_Body.Trim(), Convert.ToDouble(_returnvnonline.ToString()));
                }
                //end
            }

            Response.Redirect("~/Quytrinh/List_PV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + Request["Tab"]);

        }

        #endregion

        #region Event click

        protected void cbo_AnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();

            if (cbo_AnPham.SelectedIndex > 0)
            {
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_AnPham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

            }

        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string Thaotac = "";
            T_TinBai objtinbai = new T_TinBai();

            if (Page.IsValid)
            {
                objtinbai = SetItem(Request["MaDoiTuong"].ToString(), 2);
                txtID.Text = _Daltinbai.SP_UpdateT_TinBai_WordOnline(objtinbai).ToString();
                _matinbai = double.Parse(txtID.Text.Trim());
                if (Request["ID"] != null)
                    Thaotac = "Thao tác sửa nội dung tin bài - tiêu đề:" + Txt_tieude.Text;
                else
                    Thaotac = "Thao tác thêm mới tin bài - tiêu đề:" + Txt_tieude.Text;

                if (MaDetai != 0)
                    _Daltinbai.InsertT_Vitri_Tinbai_FromT_Tinbai(0, 0, _matinbai, MaDetai);

                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                SaveImagesAttachAll();

                Response.Redirect("~/Quytrinh/Edit_PV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _matinbai.ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=0");

            }
        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            Session["matinbai"] = null;
            if (Request["ID"] != null)
                _Daltinbai.IsLock(int.Parse(Request["ID"]), 0);
            Response.Redirect("~/Quytrinh/List_PV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + Request["Tab"]);
        }


        #endregion

        #region Send Tin Bai Theo Doi Tuong
        protected void DataListDoiTuong_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string MaDoiTuong = e.CommandArgument.ToString();
            GuiTinBai(MaDoiTuong);
            Session["matinbai"] = null;
        }

        private void BindDoiTuongGuiBai()
        {
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            if (Request["MaDoiTuong"] != null && Ma_QTBT != 0)
            {
                DataSet _ds = _T_newsDAL.Bind_DoiTuongGui(Request["MaDoiTuong"], Ma_QTBT, Session["culture"].ToString());
                DataListDoiTuong.DataSource = _ds;
                DataListDoiTuong.DataBind();
            }
            else
            {
                DataListDoiTuong.DataSource = null;
                DataListDoiTuong.DataBind();

            }
        }
        #endregion

        #region Anh dinh kem

        protected void btnXoaAnh_Click(object sender, EventArgs e)
        {
            AnhDAL _dalanh = new AnhDAL();
            string SqlUpdate = string.Empty;
            foreach (DataListItem m_Item in dgrListImages.Items)
            {
                int _ID = int.Parse(this.dgrListImages.DataKeys[m_Item.ItemIndex].ToString());

                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;
                if (chk_Select != null && chk_Select.Checked)
                {
                    _dalanh.DeleteFromT_Anh(_ID);
                    string path = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lbFileAttach.Text);
                    System.IO.FileInfo fi = new System.IO.FileInfo(path);
                    try
                    {
                        if (File.Exists(path))
                            fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            LoadDataImage();

        }

        public void SaveImagesAttachAll()
        {
            T_Tinbai_Anh _obj = new T_Tinbai_Anh();

            int _ID = 0;
            if (dgrListImages.Items.Count > 0)
            {
                _daltinanh.DeleteAllTinbai_AnhByMatinbai(_matinbai);
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    _ID = Convert.ToInt32(dgrListImages.DataKeys[m_Item.ItemIndex].ToString());
                    TextBox txtTacgia = m_Item.FindControl("txtTacgia") as TextBox;
                    TextBox hdnValueTacGiaAnh = m_Item.FindControl("hdnValueTacGiaAnh") as TextBox;
                    TextBox txtChuthich = m_Item.FindControl("txtChuthich") as TextBox;
                    if (_ID != 0)
                    {
                        if (_matinbai != 0)
                        {
                            _obj.Ma_TinBai = _matinbai;
                            _obj.Ma_Anh = _ID;
                            _obj.ChuThich = "";
                            _daltinanh.InsertUpdateTin_Anh(_obj);
                        }
                        _objimg = _dalanh.GetOneFromT_AnhByID(_ID);
                        string _chuthich = "";
                        string _tacgia = "";
                        int _tacgiaID = 0;
                        if (txtChuthich.Text != "")
                        {
                            _chuthich = UltilFunc.RemoveEnterCode(txtChuthich.Text.Trim().Replace("'", "''"));
                        }
                        if (_objimg != null && _objimg.Ma_Nguoichup != 0)
                        {
                            _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                            _tacgiaID = _objimg.Ma_Nguoichup;
                        }
                        if (!String.IsNullOrEmpty(hdnValueTacGiaAnh.Text))
                        {
                            _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                            _tacgiaID = int.Parse(hdnValueTacGiaAnh.Text.ToString());
                        }


                        string SqlUpdate = "";
                        SqlUpdate = "update T_Anh set Chuthich=N'" + _chuthich + "',NguoiChup=N'" + _tacgia + "',Ma_Nguoichup=" + _tacgiaID + " where Ma_Anh=" + _ID;
                        Ulti.ExecSql(SqlUpdate);

                    }
                }

            }

        }

        public void LoadDataImage()
        {
            string where = "";
            if (Request["ID"] != null)
                where = " Duyet=1 and Ma_TinBai=" + Request["ID"].ToString();
            else
                where = " Duyet=1 and Ma_Anh not in (select Ma_Anh from T_Tinbai_Anh) and  NguoiTao=" + _user.UserID + " and NgayTao>='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";

            DataSet _ds = _daltinanh.Sp_SelectTinAnhDynamic(where, "NgayTao DESC");
            this.dgrListImages.DataSource = _ds;
            this.dgrListImages.DataBind();

            string _chuthich = string.Empty;
            string _tacgiaanh = string.Empty;
            if (dgrListImages.Items.Count > 0)
            {
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    int _ID = int.Parse(this.dgrListImages.DataKeys[m_Item.ItemIndex].ToString());
                    _objimg = _dalanh.GetOneFromT_AnhByID(_ID);
                    Label _labeldesc = m_Item.FindControl("lbdesc") as Label;
                    Label _Labellbtacgia = m_Item.FindControl("lbtacgia") as Label;
                    TextBox txtChuthich = m_Item.FindControl("txtChuthich") as TextBox;
                    TextBox txtTacgia = m_Item.FindControl("txtTacgia") as TextBox;

                    ImageButton btnUpdate = m_Item.FindControl("btnUpdate") as ImageButton;
                    ImageButton btnCancel = m_Item.FindControl("btnCancel") as ImageButton;

                    if (_objimg.Chuthich == "")
                    {
                        _labeldesc.Visible = false;
                        txtChuthich.Visible = true;

                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;
                    }
                    if (_objimg.Ma_Nguoichup == 0)
                    {
                        _Labellbtacgia.Visible = false;
                        txtTacgia.Visible = true;

                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;
                    }

                }
            }
        }

        public void dgrListImages_EditCommand(object source, DataListCommandEventArgs e)
        {

            ImageButton btnAdd = e.Item.FindControl("btnAdd") as ImageButton;
            ImageButton btnUpdate = e.Item.FindControl("btnUpdate") as ImageButton;
            ImageButton btnCancel = e.Item.FindControl("btnCancel") as ImageButton;
            ImageButton btndelete = e.Item.FindControl("Imagebuttondelete") as ImageButton;

            TextBox txtTacgia = e.Item.FindControl("txtTacgia") as TextBox;
            TextBox hdnValueTacGiaAnh = e.Item.FindControl("hdnValueTacGiaAnh") as TextBox;
            TextBox txtChuthich = e.Item.FindControl("txtChuthich") as TextBox;
            Label lbdesc = e.Item.FindControl("lbdesc") as Label;
            Label lbtacgia = e.Item.FindControl("lbtacgia") as Label;

            int _ID = Convert.ToInt32(dgrListImages.DataKeys[e.Item.ItemIndex].ToString());
            _objimg = _dalanh.GetOneFromT_AnhByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                Label lbFileAttach = e.Item.FindControl("lbFileAttach") as Label;
                string path = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lbFileAttach.Text);
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                try
                {
                    if (File.Exists(path))
                        fi.Delete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                _dalanh.DeleteFromT_Anh(_ID);
                string thaotac = "Thao tác xóa ảnh đính kèm: " + _objimg.Duongdan_Anh + " tại " + Request["MaDoiTuong"].ToString() + "  thuộc tin bài: " + Txt_tieude.Text.Trim();
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), thaotac);
                LoadDataImage();

            }
            if (e.CommandArgument.ToString().ToLower() == "editinfo")
            {
                btndelete.Visible = false;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                lbdesc.Visible = false;
                lbtacgia.Visible = false;
                txtTacgia.Visible = true;
                txtChuthich.Visible = true;

                if (_objimg != null && _objimg.Ma_Nguoichup != 0)
                    hdnValueTacGiaAnh.Text = _objimg.Ma_Nguoichup.ToString();
                else
                    hdnValueTacGiaAnh.Text = "";
                if (_objimg.NguoiChup != "")
                    txtTacgia.Text = _objimg.NguoiChup;
                else
                    txtTacgia.Text = "";
                if (_objimg.Chuthich.Length > 0)
                    txtChuthich.Text = _objimg.Chuthich;
                else
                    txtChuthich.Text = "";
            }
            if (e.CommandArgument.ToString().ToLower() == "update")
            {
                string _chuthich = "";
                string _tacgia = "";
                int _tacgiaID = 0;
                if (txtChuthich.Text != "")
                {
                    _chuthich = UltilFunc.RemoveEnterCode(txtChuthich.Text.Trim().Replace("'", "''"));
                }
                if (_objimg != null && _objimg.Ma_Nguoichup != 0)
                {
                    _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                    _tacgiaID = _objimg.Ma_Nguoichup;
                }
                if (!String.IsNullOrEmpty(hdnValueTacGiaAnh.Text))
                {
                    _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                    _tacgiaID = int.Parse(hdnValueTacGiaAnh.Text.ToString());
                }


                string SqlUpdate = "";
                SqlUpdate = "update T_Anh set Chuthich=N'" + _chuthich + "',NguoiChup=N'" + _tacgia + "',Ma_Nguoichup=" + _tacgiaID + " where Ma_Anh=" + _ID;
                Ulti.ExecSql(SqlUpdate);
                LoadDataImage();
                lbdesc.Text = txtChuthich.Text;
                lbtacgia.Text = txtTacgia.Text;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                lbdesc.Visible = true;
                lbtacgia.Visible = true;
                txtChuthich.Visible = false;
                txtTacgia.Visible = false;

            }
            if (e.CommandArgument.ToString().ToLower() == "cancel")
            {
                btndelete.Visible = true;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                lbdesc.Visible = true;
                lbtacgia.Visible = true;
                txtTacgia.Visible = false;
                txtChuthich.Visible = false;

            }
        }


        #endregion

        #region Methods vnnews online

        protected T_ImageFiles SetItemImgFile(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType, double Chuyenmuc)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            _obj.ImageFileName = _tenFile.ToString();
            _obj.ImageFileSize = _size;
            _obj.ImageFileExtension = _extenfile.ToString();
            _obj.ImageType = vType;
            _obj.ImgeFilePath = _pathfile.ToString();
            _obj.Status = 0;
            _obj.UserCreated = _userID;
            _obj.DateCreated = DateTime.Now;
            _obj.Categorys_ID = Chuyenmuc;

            return _obj;
        }
        private T_News SetItemBaoDienTu(double _id_newspaper)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            ImageFilesDAL _dalimgfile = new ImageFilesDAL();
            DataTable _dt_img = new DataTable();
            T_News obj_news = new T_News();
            T_TinBai _objbaoin = new T_TinBai();
            _objbaoin = _Daltinbai.load_T_news(_id_newspaper);

            if (_id_newsonline == 0)
                obj_news.News_ID = 0;
            else
                obj_news.News_ID = _id_newsonline;
            obj_news.News_Tittle = _objbaoin.Tieude;
            obj_news.CAT_ID = _objbaoin.Ma_Chuyenmuc;
            obj_news.Lang_ID = _objbaoin.Ma_NgonNgu;

            string _sqlimg = string.Empty;
            _sqlimg = "select TenFile_Hethong,Duongdan_Anh,Chuthich from t_anh where ma_anh in (select Ma_Anh from T_Tinbai_Anh where Ma_TinBai=" + _id_newspaper + ")";
            _dt_img = Ulti.ExecSqlDataSet(_sqlimg).Tables[0];
            string DesPath = string.Empty;
            string PathSource = string.Empty;
            string UrlImg = string.Empty;
            if (_dt_img != null && _dt_img.Rows.Count > 0)
            {
                for (int i = 0; i < _dt_img.Rows.Count; i++)
                {
                    DesPath = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                    DesPath = HttpContext.Current.Server.MapPath("/" + DesPath);
                    CreateFolderByUserName(DesPath);
                    PathSource = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + _dt_img.Rows[i]["Duongdan_Anh"].ToString());
                    DesPath += Path.GetFileName(PathSource);
                    UrlImg = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + Path.GetFileName(PathSource);
                    ResizeImages(PathSource, Convert.ToInt32(HPCComponents.Global.VNPResizeImagesContent), DesPath);
                    if (i == 0)
                        obj_news.News_Body = _objbaoin.Noidung;
                    //insert table T_ImageFiles
                    int _idImgFile = 0;

                    int startchar = UrlImg.Substring(1, UrlImg.Length - 1).IndexOf("/");
                    startchar += 1;
                    string _PathFile = UrlImg.Substring(startchar, UrlImg.Length - startchar);

                    _obj = SetItemImgFile(_dt_img.Rows[i]["TenFile_Hethong"].ToString(), 0, _PathFile, "", _user.UserID, 1, 0);
                    _idImgFile = _dalimgfile.InsertT_ImageFiles(_obj);

                    //end
                    obj_news.News_Body += "</br><img border=\"0\" hspace=\"3\" id=\"" + _idImgFile + "\" src=\"" + UrlImg + "\" style=\"cursor-pointer\" vspace=\"3\" /></br>" + _dt_img.Rows[i]["Chuthich"].ToString();

                }
            }
            else
                obj_news.News_Body = _objbaoin.Noidung;
            obj_news.News_PublishNumber = int.Parse(DateTime.Now.Month.ToString());
            obj_news.News_PublishYear = int.Parse(DateTime.Now.Year.ToString());
            obj_news.News_DateCreated = DateTime.Now;
            obj_news.News_DateEdit = DateTime.Now;
            obj_news.News_DatePublished = DateTime.Now;
            obj_news.News_DateApproved = DateTime.Now;
            obj_news.News_AuthorID = int.Parse(_objbaoin.Ma_Nguoitao.ToString());
            obj_news.News_AprovedID = _user.UserID;
            obj_news.News_EditorID = _user.UserID;
            obj_news.News_PublishedID = _user.UserID;
            obj_news.News_CopyFrom = 0;
            obj_news.RefID = int.Parse(_id_newspaper.ToString());
            obj_news.News_Status = int.Parse(CommonLib.ReadXML("Status_BDT"));

            return obj_news;
        }
        // Xử lý ảnh đẩy sang bao điện tử
        private void CreateFolderByUserName(string FolderName)
        {
            string strRootPath = "";
            strRootPath = FolderName;
            if (Directory.Exists(strRootPath) == false)
                Directory.CreateDirectory(strRootPath);
        }
        public string RemoveHTMLTag(string HTML)
        {
            // Xóa các thẻ html
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            return objRegEx.Replace(HTML, "");

        }
        public string ReplaceCharsRewrite(string input)
        {
            string str = "", StrTemp = RemoveHTMLTag(Convert.ToString(input));
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = StrTemp.Normalize(System.Text.NormalizationForm.FormD);
            str = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex objRegEx = new Regex("<[^>]*>");
            str = str.Replace(" ", "-");
            str = str.Replace(",", "");
            str = str.Replace(".", "");
            str = str.Replace(";", "");
            str = str.Replace(":", "");
            str = str.Replace("?", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("!", "");
            str = str.Replace("@", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("+", "");
            str = str.Replace("=", "");
            str = str.Replace("\\", "");
            str = str.Replace("|", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("”", "");
            str = str.Replace("“", "");
            str = str.Replace("-»", "");
            str = str.Replace("«-", "");
            str = str.Replace("»", "");
            str = str.Replace("»", "");
            str = str.Replace("«", "");
            str = str.Replace("’", "");
            str = str.Replace("--", "-");
            str = str.Replace("---", "-");
            str = str.Replace("----", "-");
            str = str.Replace("-----", "-");
            str = str.Replace(" ", "-");
            str = str.Replace("Ã°", "");
            str = str.Replace("â€", "");
            str = str.Replace("a€", "");
            str = str.Replace("a°", "");
            str = str.Replace("°", "");
            str = str.Replace("€", "");
            str = str.Replace("a", "");
            str = str.Replace("ß", "");
            str = str.Replace("¦", "");
            str = str.Replace("ßƒ", "");
            str = str.Replace("ƒ", "");
            str = str.Replace("v¿", "");
            str = str.Replace("¿", "");
            str = str.Replace("ð", "");
            str = str.Replace("¥", "");
            str = str.Replace("æ", "");
            str = str.Replace("¼", "");
            str = str.Replace("½", "");
            str = str.Replace("¬", "");
            str = str.Replace("cº", "");
            str = str.Replace("phº", "");
            str = str.Replace("ç", "");
            str = str.Replace("ßº", "");
            str = str.Replace("¢", "");
            str = str.Replace("Â¢", "");
            return str.ToLower();
        }
        private bool ResizeImages(string SourcePath, int Width, string Despath)
        {
            bool success;
            string path = SourcePath;
            Bitmap _obj;
            System.Drawing.Image objImage;
            int imgwidth = 0;
            int imgheight = 0;

            decimal lnRatio;
            try
            {
                objImage = System.Drawing.Image.FromFile(path);
                if (objImage.Width > objImage.Height)
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;

                }
                // Create thumbnail
                _obj = new Bitmap(imgwidth, imgheight);

                Graphics grWatermark = Graphics.FromImage(_obj);
                grWatermark.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grWatermark.SmoothingMode = SmoothingMode.HighQuality;
                grWatermark.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grWatermark.CompositingQuality = CompositingQuality.HighQuality;
                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, imgwidth, imgheight);
                grWatermark.DrawImage(objImage, imageRectangle);
                this.saveJpeg(Despath, _obj, 100L);
                grWatermark.Dispose();
                _obj.Dispose();
                objImage.Dispose();
                success = true;
            }
            catch //(Exception ex)
            {
                success = false;
                //throw ex;
            }
            return success;
        }
        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
            img.Dispose();
        }
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        #endregion
    }
}
