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

namespace ToasoanTTXVN.Quytrinh
{
    public partial class Send_Email : System.Web.UI.Page
    {
        TinBaiAnhDAL _DAL = new TinBaiAnhDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;
        double matinbai = 0;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        TinBaiAnhDAL _daltinanh = new TinBaiAnhDAL();
        SSOLibDAL lib = new SSOLibDAL();
        UltilFunc Ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    this.btnXoaAnh.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_dgrListPhotoOfNews_ctl01_chkAll');");
                }


                if (!IsPostBack)
                {

                    if (_user == null)
                    {
                        Page.Response.Redirect("~/login.aspx", true);
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

        #region Method

        private T_TinBai SetItem(string _Doituongxuly, int _status)
        {
            T_TinBai _obj = new T_TinBai();
            if (int.Parse(txtID.Text.Trim()) > 0)
                _obj.Ma_Tinbai = int.Parse(txtID.Text.Trim());
            else
                _obj.Ma_Tinbai = 0;
            _obj.Tieude = UltilFunc.CleanFormatTags(Txt_tieude.Text.Trim());
            _obj.Noidung = CKE_Noidung.Text;
            _obj.TacGia = "";
            _obj.Ma_TacGia = int.Parse(HiddenFieldTacgiatin.Value.ToString());
            _obj.Ma_NgonNgu = 1;
            _obj.Ma_Chuyenmuc = 0;
            _obj.NgayTao = DateTime.Now;
            _obj.Ma_Nguoitao = _user.UserID;
            _obj.Nguoi_Khoa = _user.UserID;
            _obj.Sotu = double.Parse(word_count.Value.ToString());
            _obj.Trangthai_Xoa = false;
            if (Txt_Comments.Text.Trim() != "Nhập góp ý")
                _obj.GhiChu = UltilFunc.CleanFormatTags(Txt_Comments.Text);
            else
                _obj.GhiChu = "";
            _obj.Trangthai = _status;
            _obj.Doituong_DangXuly = _Doituongxuly;
            _obj.LuuVet = Daltinbai.GetTrace(_obj.Ma_Tinbai);

            return _obj;
        }
        private void ClearForm()
        {
            txt_PVCTV.Text = "";
            HiddenFieldTacgiatin.Value = "";
            Txt_tieude.Text = "";
            CKE_Noidung.Text = "";
            Txt_Comments.Text = "";
            LoadDataImage();
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
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        private void PopulateItem(int _id)
        {
            T_TinBai _obj = new T_TinBai();
            ChuyenmucDAL _dalcm = new ChuyenmucDAL();
            _obj = Daltinbai.load_T_news(_id);
            Txt_tieude.Text = _obj.Tieude;
            CKE_Noidung.Text = _obj.Noidung;
            txt_PVCTV.Text = _obj.TacGia;
            HiddenFieldTacgiatin.Value = _obj.Ma_TacGia.ToString();
            Txt_Comments.Text = _obj.GhiChu;
            LoadDataImage();

        }
        private bool checkform()
        {
            if (Txt_tieude.Text.Trim().Length == 0 || Txt_tieude.Text.Trim() == "Nhập tiêu đề")
            {
                UltilFunc.AlertJS("Bạn chưa nhập tiêu đề tin bài");
                return false;
            }
            if (CKE_Noidung.Text.Length == 0)
            {
                UltilFunc.AlertJS("Bạn chưa nhập nội dung tin bài");
                return false;
            }

            if (txt_PVCTV.Text.Trim().Length == 0 || HiddenFieldTacgiatin.Value == "" || txt_PVCTV.Text.Trim() == "Nhập người nhận")
            {
                UltilFunc.AlertJS("Bạn chưa chọn người nhận");
                return false;
            }


            return true;
        }
        #endregion

        #region Event click
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string Thaotac = "";
            T_TinBai objtinbai = new T_TinBai();

            if (Page.IsValid)
            {
                if (checkform())
                {
                    objtinbai = SetItem("", 6);
                    if (Request["ID"] != null)
                    {
                        matinbai = Daltinbai.InsertT_Tinbai(objtinbai);
                        Thaotac = "Thao tác sửa thông tin cá nhân - tiêu đề:" + Txt_tieude.Text;
                    }
                    else
                    {
                        matinbai = Daltinbai.InsertT_Tinbai(objtinbai);
                        Thaotac = "Thao tác gửi thông tin cá nhân - tiêu đề:" + Txt_tieude.Text;
                    }
                    Daltinbai.Insert_Phienban_From_T_Tinbai(matinbai, _user.UserID, DateTime.Now,"email");
                    if (matinbai > 0)
                        SaveImagesAttach(matinbai);
                    if (SaveImagesAttachAll())
                    {
                        UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                        UltilFunc.AlertJS("Bạn đã gửi thành công!");
                        ClearForm();
                    }
                }

            }
        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quytrinh/List_PV.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        #endregion

        #region Anh dinh kem

        protected void btnXoaAnh_Click(object sender, EventArgs e)
        {
            AnhDAL _dalanh = new AnhDAL();
            foreach (DataListItem m_Item in dgrListImages.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;
                if (chk_Select != null && chk_Select.Checked)
                {

                    int _ID = int.Parse(this.dgrListImages.DataKeys[m_Item.ItemIndex].ToString());
                    _dalanh.DeleteFromT_Anh(_ID);
                    string path = HttpContext.Current.Server.MapPath("~" + lbFileAttach.Text);
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

        public void SaveImagesAttach(double _matinbai)
        {
            T_Tinbai_Anh _obj = new T_Tinbai_Anh();
            int _ID = 0;
            if (dgrListImages.Items.Count > 0)
            {
                _daltinanh.DeleteAllTinbai_AnhByMatinbai(_matinbai);
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    _ID = Convert.ToInt32(dgrListImages.DataKeys[m_Item.ItemIndex].ToString());

                    if (_matinbai != 0)
                    {
                        _obj.Ma_TinBai = _matinbai;
                        _obj.Ma_Anh = _ID;
                        _obj.ChuThich = "";
                        _daltinanh.InsertUpdateTin_Anh(_obj);
                    }
                }
            }

        }

        public bool SaveImagesAttachAll()
        {
            T_Anh objimage = new T_Anh();
            AnhDAL _dalanh = new AnhDAL();
            int _ID = 0;
            if (dgrListImages.Items.Count > 0)
            {
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    _ID = Convert.ToInt32(dgrListImages.DataKeys[m_Item.ItemIndex].ToString());
                    TextBox txtTacgia = m_Item.FindControl("txtTacgia") as TextBox;
                    TextBox hdnValueTacGiaAnh = m_Item.FindControl("hdnValueTacGiaAnh") as TextBox;
                    TextBox txtChuthich = m_Item.FindControl("txtChuthich") as TextBox;
                    if (_ID != 0)
                    {
                        objimage = _dalanh.GetOneFromT_AnhByID(_ID);
                        string _chuthich = "";
                        string _tacgia = "";
                        int _tacgiaID = 0;
                        if (txtChuthich.Text != Global.RM.GetString("Nhapchuthichanh"))
                        {
                            _chuthich = UltilFunc.RemoveEnterCode(txtChuthich.Text.Trim().Replace("'", "''"));
                        }
                        if (objimage != null && objimage.Ma_Nguoichup != 0)
                        {
                            _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                            _tacgiaID = objimage.Ma_Nguoichup;
                        }
                        if (!String.IsNullOrEmpty(hdnValueTacGiaAnh.Text))
                        {
                            _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                            _tacgiaID = int.Parse(hdnValueTacGiaAnh.Text.ToString());
                        }
                        if (txtChuthich.Text == Global.RM.GetString("Nhapchuthichanh"))
                        {
                            FuncAlert.AlertJS(this, "bạn chưa nhập chú thích ảnh");
                            return false;
                        }

                        if (_tacgiaID == 0)
                        {
                            FuncAlert.AlertJS(this, "bạn chưa nhập tác giả ảnh");
                            return false;
                        }

                        string SqlUpdate = "";
                        SqlUpdate = "update T_Anh set Chuthich=N'" + _chuthich + "',NguoiChup=N'" + _tacgia + "',Ma_Nguoichup=" + _tacgiaID + " where Ma_Anh=" + _ID;
                        Ulti.ExecSql(SqlUpdate);

                    }
                }

            }
            return true;
        }

        public void LoadDataImage()
        {
            string where = "";
            if (Request["ID"] != null)
                where = " Ma_TinBai=" + Request["ID"].ToString();
            else
                where = " Ma_Anh not in (select Ma_Anh from T_Tinbai_Anh) and  NguoiTao=" + _user.UserID + " and NgayTao>='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";

            DataSet _ds = _daltinanh.Sp_SelectTinAnhDynamic(where, "NgayTao DESC");
            this.dgrListImages.DataSource = _ds;
            this.dgrListImages.DataBind();

            string _chuthich = string.Empty;
            string _tacgiaanh = string.Empty;
            if (dgrListImages.Items.Count > 0)
            {
                foreach (DataListItem m_Item in dgrListImages.Items)
                {
                    Label _labeldesc = m_Item.FindControl("lbdesc") as Label;
                    Label _Labellbtacgia = m_Item.FindControl("lbtacgia") as Label;
                    TextBox txtChuthich = m_Item.FindControl("txtChuthich") as TextBox;
                    TextBox txtTacgia = m_Item.FindControl("txtTacgia") as TextBox;
                    ImageButton btnUpdate = m_Item.FindControl("btnUpdate") as ImageButton;
                    ImageButton btnCancel = m_Item.FindControl("btnCancel") as ImageButton;
                    if (_labeldesc != null)
                        _chuthich = _labeldesc.Text.Trim();
                    if (_Labellbtacgia != null)
                        _tacgiaanh = _Labellbtacgia.Text.Trim();
                    if (_chuthich == "")
                    {
                        _labeldesc.Visible = false;
                        txtChuthich.Visible = true;
                        txtChuthich.Text = Global.RM.GetString("Nhapchuthichanh");
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;
                    }
                    if (_tacgiaanh == "")
                    {
                        _Labellbtacgia.Visible = false;
                        txtTacgia.Visible = true;
                        txtTacgia.Text = Global.RM.GetString("Nhaptacgiaanh");
                        btnUpdate.Visible = true;
                        btnCancel.Visible = true;
                    }

                }
            }
        }

        public void dgrListImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            T_Anh objimage = new T_Anh();
            AnhDAL _dalanh = new AnhDAL();
            ImageButton btnAdd = e.Item.FindControl("btnAdd") as ImageButton;
            ImageButton btnUpdate = e.Item.FindControl("btnUpdate") as ImageButton;
            ImageButton btnCancel = e.Item.FindControl("btnCancel") as ImageButton;
            ImageButton btndelete = e.Item.FindControl("Imagebuttondelete") as ImageButton;
            TextBox hdnValueTacGiaAnh = e.Item.FindControl("hdnValueTacGiaAnh") as TextBox;
            TextBox txtTacgia = e.Item.FindControl("txtTacgia") as TextBox;
            TextBox txtChuthich = e.Item.FindControl("txtChuthich") as TextBox;
            Label lbdesc = e.Item.FindControl("lbdesc") as Label;
            Label lbtacgia = e.Item.FindControl("lbtacgia") as Label;

            int _ID = Convert.ToInt32(dgrListImages.DataKeys[e.Item.ItemIndex].ToString());
            objimage = _dalanh.GetOneFromT_AnhByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                Label lbFileAttach = e.Item.FindControl("lbFileAttach") as Label;
                string path = HttpContext.Current.Server.MapPath("~" + lbFileAttach.Text);
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
                string thaotac = "Thao tác xóa ảnh đính kèm: " + objimage.Duongdan_Anh + " tại " + Request["MaDoiTuong"].ToString() + "  thuộc tin bài: " + Txt_tieude.Text.Trim();
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

                if (objimage != null && objimage.Ma_Nguoichup != 0)
                    hdnValueTacGiaAnh.Text = objimage.Ma_Nguoichup.ToString();
                else
                    hdnValueTacGiaAnh.Text = "";
                if (objimage.NguoiChup != "")
                    txtTacgia.Text = objimage.NguoiChup;
                else
                    txtTacgia.Text = Global.RM.GetString("Nhaptacgiaanh");
                if (objimage.Chuthich.Length > 0)
                    txtChuthich.Text = objimage.Chuthich;
                else
                    txtChuthich.Text = Global.RM.GetString("Nhapchuthichanh");
            }
            if (e.CommandArgument.ToString().ToLower() == "update")
            {
                string _chuthich = "";
                string _tacgia = "";
                int _tacgiaID = 0;
                if (txtChuthich.Text != Global.RM.GetString("Nhapchuthichanh"))
                    _chuthich = UltilFunc.RemoveEnterCode(txtChuthich.Text.Trim().Replace("'", "''"));
                if (objimage != null && objimage.Ma_Nguoichup != 0)
                {
                    _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                    _tacgiaID = objimage.Ma_Nguoichup;
                }
                if (!String.IsNullOrEmpty(hdnValueTacGiaAnh.Text))
                {
                    _tacgia = txtTacgia.Text.Replace(" -- ", "|").Split('|')[0];
                    _tacgiaID = int.Parse(hdnValueTacGiaAnh.Text.ToString());
                }
                if (txtChuthich.Text == Global.RM.GetString("Nhapchuthichanh"))
                {
                    FuncAlert.AlertJS(this, "bạn chưa nhập chú thích ảnh");
                    return;
                }

                if (_tacgiaID == 0)
                {
                    FuncAlert.AlertJS(this, "bạn chưa nhập tác giả ảnh");
                    return;
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

        public void dgrListImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ImageButton btndelete = e.Item.FindControl("Imagebuttondelete") as ImageButton;
            btndelete.Attributes.Add("onclick", "return confirm('" + "Bạn có muốn xóa ảnh" + "?')");
        }
        #endregion
    }
}
