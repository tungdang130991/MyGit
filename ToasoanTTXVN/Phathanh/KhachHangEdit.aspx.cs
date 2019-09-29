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


namespace ToasoanTTXVN.Phathanh
{
    public partial class KhachHangEdit : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        T_RolePermission _Role = null;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                    Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, MenuID);
                    this.linkSave.Visible = _Role.R_Write;
                    if (!IsPostBack)
                    {
                        BindData();
                    }
                }
            }
        }

        #region Method
        private void Clear()
        {
            this.txt_TenKH.Text = "";
            this.txt_Diachi.Text = "";
            this.txt_Dienthoai.Text = "";
            this.txt_Email.Text = "";
            this.txt_Fax.Text = "";
            this.txt_Masothue.Text = "";
            this.txt_Mota.Text = "";
            this.txt_Nguoidaidien.Text = "";
            this.txt_Taikhoan.Text = "";
            this.txt_Tendaydu.Text = "";          
        }
        private void GetItem(int _id)
        {
            T_KhachHang _obj = new T_KhachHang();
            KhachhangDAL _objDAL = new KhachhangDAL();
            _obj = _objDAL.GetOneFromT_KhachHangByID(_id);
            this.txt_TenKH.Text = _obj.Ten_KhachHang;
            this.txt_Diachi.Text = _obj.DiaChi;
            this.txt_Dienthoai.Text = _obj.SoDienThoai;
            this.txt_Email.Text = _obj.Email;
            this.txt_Fax.Text = _obj.FAX;
            this.txt_Masothue.Text = _obj.MaSoThue;
            this.txt_Mota.Text = _obj.Ghichu;
            this.txt_Nguoidaidien.Text = _obj.NguoiDaiDien;
            this.txt_Taikhoan.Text = _obj.TaiKhoan;
            this.txt_Tendaydu.Text = _obj.Tendaydu;           

        }
        private T_KhachHang SetItem()
        {
            T_KhachHang _obj = new T_KhachHang();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Ma_KhachHang = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Ma_KhachHang = 0;
            _obj.Ten_KhachHang = UltilFunc.SqlFormatText(this.txt_TenKH.Text.Trim());
            _obj.DiaChi = this.txt_Diachi.Text.Trim();
            _obj.SoDienThoai = this.txt_Dienthoai.Text.Trim();
            _obj.Email = this.txt_Email.Text.Trim();
            _obj.FAX = this.txt_Fax.Text.Trim();
            _obj.MaSoThue = this.txt_Masothue.Text.Trim();
            _obj.NguoiDaiDien = this.txt_Nguoidaidien.Text.Trim();
            _obj.Tendaydu = this.txt_Tendaydu.Text.Trim();
            _obj.Ghichu = this.txt_Mota.Text.Trim();
            _obj.TaiKhoan = this.txt_Taikhoan.Text.Trim();
            _obj.Loai_KH = 2; //1=Quảng cáo; 2=Phát hành
            _obj.Ngaytao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;
            _obj.Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;

            return _obj;
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
            Page.Response.Redirect("~/Phathanh/KhachHangList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            this.Page.Validate(vs_Themmoi.ValidationGroup);
            if (!Page.IsValid) return;
            KhachhangDAL _KhachHangDAL = new KhachhangDAL();
            T_KhachHang _KhachHang = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _KhachHangDAL.InsertT_Khachhang(_KhachHang);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới khách hàng]-->[mã khách hàng:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa thông tin khách hàng]-->[Mã KH:" + Page.Request["id"].ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                return;
            }

        }
        #endregion
    }
}
