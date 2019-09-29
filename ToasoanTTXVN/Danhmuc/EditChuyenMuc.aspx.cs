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
using SSOLib;
using SSOLib.ServiceAgent;
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.Danhmuc
{
    public partial class EditChuyenMuc : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        public string urlimages = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        BindCombo();
                        BindData();
                    }
                }
            }
        }

        #region Method
        private void Clear()
        {
            this.txtTenCM.Text = string.Empty;
            this.ddl_ChuyenMucCha.Items.Clear();
            this.cbo_Anpham.Items.Clear();
            this.chk_Hoatdong.Checked = false;
        }
        private void GetItem(int _id)
        {
            T_ChuyenMuc _objChuyenmuc = new T_ChuyenMuc();
            ChuyenmucDAL _chuyenmucDAL = new ChuyenmucDAL();
            _objChuyenmuc = _chuyenmucDAL.GetOneFromT_ChuyenmucByID(_id);
            this.txtTenCM.Text = _objChuyenmuc.Ten_ChuyenMuc;
            this.txtThum.Text = _objChuyenmuc.Anh_ChuyenMuc;
            if (_objChuyenmuc.Anh_ChuyenMuc.ToString().Length > 0)
                this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _objChuyenmuc.Anh_ChuyenMuc;
            else this.ImgTemp.Attributes.CssStyle.Add("display", "none");

            this.ddl_ChuyenMucCha.SelectedIndex = UltilFunc.GetIndexControl(ddl_ChuyenMucCha, _objChuyenmuc.Ma_Chuyenmuc_Cha.ToString());
            this.cbo_Anpham.SelectedIndex = UltilFunc.GetIndexControl(cbo_Anpham, _objChuyenmuc.Ma_AnPham.ToString());
            this.chk_Hoatdong.Checked = _objChuyenmuc.HoatDong;
            this.txt_stt.Text = _objChuyenmuc.ThuTuHienThi.ToString();
            this.CheckBoxBaoIn.Checked = _objChuyenmuc.HienThi_BaoIn;
            this.CheckBoxBaoDT.Checked = _objChuyenmuc.HienThi_BDT;
            this.CheckBoxRss.Checked = _objChuyenmuc.HienThi_RSS;
            this.CheckBoxCD.Checked = _objChuyenmuc.ChuyenDe;
            this.CheckBoxButtom.Checked = _objChuyenmuc.HienThiDuoi;
            this.CheckBoxCenter.Checked = _objChuyenmuc.HienThiGiua;
            this.CheckBoxRight.Checked = _objChuyenmuc.HienThiPhai;
            this.CheckBoxLeft.Checked = _objChuyenmuc.HienThiTrai;
            this.CheckBoxTop.Checked = _objChuyenmuc.HienThiTren;
            this.chkHienThi.Checked = _objChuyenmuc.HienThi;
        }
        private T_ChuyenMuc SetItem()
        {
            T_ChuyenMuc _obj = new T_ChuyenMuc();
            if (Request["id"] != null)
                _obj.Ma_ChuyenMuc = int.Parse(Request["id"]);
            else _obj.Ma_ChuyenMuc = 0;
            _obj.Ten_ChuyenMuc = UltilFunc.SqlFormatText(this.txtTenCM.Text.Trim());

            _obj.NgayTao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;
            _obj.HoatDong = this.chk_Hoatdong.Checked;

            _obj.Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;
            _obj.Ma_Chuyenmuc_Cha = Convert.ToInt32(this.ddl_ChuyenMucCha.SelectedValue.ToString());
            _obj.Ma_AnPham = Convert.ToInt32(this.cbo_Anpham.SelectedValue.ToString());
            if (txt_stt.Text.Trim().Length > 0)
                _obj.ThuTuHienThi = int.Parse(this.txt_stt.Text.Trim());
            else
                _obj.ThuTuHienThi = 0;
            if (this.txtThum.Text.Trim().Length > 0)
                _obj.Anh_ChuyenMuc = this.txtThum.Text.Trim();

            _obj.HienThi_BaoIn = this.CheckBoxBaoIn.Checked;
            _obj.HienThi_BDT = this.CheckBoxBaoDT.Checked;
            _obj.HienThi_RSS = this.CheckBoxRss.Checked;
            _obj.ChuyenDe = this.CheckBoxCD.Checked;
            _obj.HienThiDuoi = this.CheckBoxButtom.Checked;
            _obj.HienThiGiua = this.CheckBoxCenter.Checked;
            _obj.HienThiPhai = this.CheckBoxRight.Checked;
            _obj.HienThiTrai = this.CheckBoxLeft.Checked;
            _obj.HienThiTren = this.CheckBoxTop.Checked;
            _obj.HienThi = this.chkHienThi.Checked;
            return _obj;
        }
        private void BindCombo()
        {
            ddl_ChuyenMucCha.Items.Clear();
            UltilFunc.BindCombox(this.ddl_ChuyenMucCha, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " Hoatdong=1 ", "---Chọn chuyên mục cha", "Ma_Chuyenmuc_Cha");
            cbo_Anpham.Items.Clear();
            UltilFunc.BindCombox(this.cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " 1=1 ", "---Chọn ấn phẩm");
        }

        public void BindData()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" &&
                Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    GetItem(_id);
                }
            }
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

        #region Click
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Session["CurrentPage"] = Session["PageIndex"];
            Page.Response.Redirect("~/Danhmuc/ListChuyenMuc.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string Thaotac = string.Empty;
            this.Page.Validate(vs_Themmoi.ValidationGroup);
            if (!Page.IsValid) return;
            ChuyenmucDAL _ChuyenMucDAL = new ChuyenmucDAL();
            T_ChuyenMuc _ChuyenMuc = SetItem();
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn ấn phẩm!");
                return;
            }
            else
            {
                int menuID = 0;
                if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                    menuID = int.Parse(Request["ID"].ToString());
                int _return = _ChuyenMucDAL.Insert_T_Chuyenmuc(_ChuyenMuc);
                // DONG BO FILE
                _ChuyenMuc = _ChuyenMucDAL.GetOneFromT_ChuyenmucByID(_return);
                SynFiles _syncfile = new SynFiles();
                if (_ChuyenMuc.Anh_ChuyenMuc.Length > 0)
                {
                    _syncfile.SynData_UploadImgOne(_ChuyenMuc.Anh_ChuyenMuc, HPCComponents.Global.ImagesService);
                }
                //END
                if (Page.Request.Params["id"] == null)
                {
                    Thaotac = "[Thêm mới Chuyên mục]-->[mã chuyên mục:" + _return.ToString() + " ]";
                    Clear();
                    BindCombo();
                }
                if (Page.Request.Params["id"] != null)
                {
                    Thaotac = "[Sửa chuyên mục]-->[Mã chuyên mục:" + Page.Request["id"].ToString() + " ]";
                    Clear();
                    BindCombo();
                }
            }
        }
        #endregion
    }
}
