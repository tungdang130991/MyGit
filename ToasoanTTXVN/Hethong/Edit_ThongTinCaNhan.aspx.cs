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
using System.Text.RegularExpressions;

namespace ToasoanTTXVN.Hethong
{
    public partial class Edit_ThongTinCaNhan : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        UltilFunc ulti = new UltilFunc();
        SSOLibDAL _DalSSO = new SSOLibDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (!IsPostBack)
            {
                if (Request["key"].ToString() == "1")
                {
                    PanelInfor.Visible = true;
                    PanelPassword.Visible = false;
                    btnSave.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblLuuthongtin");
                }
                else
                {
                    btnSave.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblLuumatkhau");
                    PanelInfor.Visible = false;
                    PanelPassword.Visible = true;
                }
                if (_user != null)
                {
                    LoadComBo();
                    PopulateItem(_user.UserID);
                }
                else
                    Page.Response.Redirect("~/login.aspx", true);

            }

        }
        private void LoadComBo()
        {
            cbo_vungmien.Items.Clear();
            cbo_phongban.Items.Clear();
            UltilFunc.BindCombox(cbo_vungmien, "Ma_Vung", "Ten_Vung", "T_Vung", " 1=1  Order by Ten_Vung", "");
            UltilFunc.BindCombox(cbo_phongban, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1  Order by Ten_Phongban", "");
        }
        public string GetCMTND(int _userid, string CustomerName)
        {
            string CMTND = string.Empty;
            DataTable _dt = new DataTable();
            string _sql = "select top 1 CMTND from T_Nguoidung where Loai=0 and Nguoitao=" + _userid;
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                CMTND = _dt.Rows[0]["CMTND"].ToString();
            else
                CMTND = "";
            return CMTND;
        }
        public T_Users SetItem()
        {
            T_Users _obj = new T_Users();
            _obj.UserID = _user.UserID;
            _obj.UserName = _user.UserName;
            _obj.UserFullName = this.txtFullName.Text.Trim();
            _obj.UserMobile = this.txtPhoneNumber.Text.Trim();
            _obj.UserEmail = this.txtEmail.Text.Trim();
            if (this.txtBirth.Text.Length > 0)
                _obj.UserBirthday = UltilFunc.ToDate(this.txtBirth.Text, "dd/MM/yyyy");
            _obj.UserAddress = this.txtAddress.Text.Trim();
            _obj.DateCreated = DateTime.Now;
            _obj.DateModify = DateTime.Now;
            _obj.UserCreate = 0;
            _obj.IsLoai = 0;
            _obj.RegionID = int.Parse(cbo_vungmien.SelectedValue.ToString());
            _obj.UserActive = true;
            _obj.ProvinceID = int.Parse(cbo_phongban.SelectedValue);
            return _obj;
        }
        private void PopulateItem(int _id)
        {
            T_Users obj = new T_Users();
            obj = _userDAL.GetUserByUserName_ID(_id);

            txt_CMTND.Text = GetCMTND(obj.UserID, obj.UserName);

            this.txtFullName.Text = obj.UserFullName;
            this.txtPhoneNumber.Text = obj.UserMobile;

            if (obj.UserBirthday != null && obj.UserBirthday != DateTime.MaxValue && obj.UserBirthday != DateTime.MinValue)
                this.txtBirth.Text = obj.UserBirthday.ToString("dd/MM/yyyy");
            else
                this.txtBirth.Text = "";
            this.txtAddress.Text = obj.UserAddress;
            this.txtEmail.Text = obj.UserEmail;
            cbo_vungmien.SelectedIndex = CommonLib.GetIndexControl(cbo_vungmien, obj.RegionID.ToString());
            cbo_phongban.SelectedIndex = CommonLib.GetIndexControl(cbo_phongban, obj.ProvinceID.ToString());
        }
        private bool checkform()
        {
            if (FormsAuthentication.HashPasswordForStoringInConfigFile(password.Text.Trim(), "sha1") != _user.UserPass)
            {
                UltilFunc.AlertJS("Mật khẩu cũ bạn nhập chưa đúng!");
                return false;
            }

            if (password1.Text.Trim() != password2.Text.Trim())
            {
                FuncAlert.AlertJS(this, "Mật khẩu mới và xác nhận mật khẩu không phù hợp!");
                return false;
            }
            Regex regex5 = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@$%^&*-])(?=^.{6,100}$)");

            Match match5 = regex5.Match(password1.Text);
            if (!match5.Success)
            {
                FuncAlert.AlertJS(this, "Mật khẩu mới phải là 6 hoặc nhiều ký tự,trong đó có số, chữ hoa, thường và ký tự đặc biệt như là: (^!@#$%&~)");
                return false;
            }
            return true;
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string Thaotac = string.Empty;
            if (Request["key"].ToString() == "2")
            {
                if (checkform())
                {
                    T_Users _obj = SetItem();
                    _userDAL.InsertT_Users(_obj);
                    _DalSSO.ChangePassUser(_user.UserID, password.Text.Trim(), password1.Text.Trim());
                    string _sql_update = "update T_Nguoidung set  TenDaydu=N'" + txtFullName.Text.Trim() + "', Ngaysinh=convert(datetime,'" + txtBirth.Text.Trim() + "',103),CMTND=N'" + txt_CMTND.Text.Trim() + "',Diachi=N'" + txtAddress.Text.Trim() + "', Mobile=N'" + txtPhoneNumber.Text.Trim() + "', Ma_Vung=" + cbo_vungmien.SelectedValue + " where loai=0 and NguoiTao=" + _user.UserID;
                    ulti.ExecSql(_sql_update);
                    Thaotac = "Thao tác đổi mật khẩu của người dùng:" + _user.UserFullName;
                    UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["key"].ToString()), Thaotac);
                    FuncAlert.AlertJS(this, "Đổi mật khẩu thành công!");
                    return;
                }
            }
            else
            {
                if (cbo_vungmien.SelectedIndex == 0)
                {
                    UltilFunc.AlertJS("Bạn chưa chọn vùng miền");
                    return;
                }
                T_Users _obj = SetItem();
                _userDAL.InsertT_Users(_obj);
                _DalSSO.ChangePassUser(_user.UserID, password.Text.Trim(), password1.Text.Trim());
                string _sql_update = "update T_Nguoidung set  TenDaydu=N'" + txtFullName.Text.Trim() + "', Ngaysinh=convert(datetime,'" + txtBirth.Text.Trim() + "',103),CMTND=N'" + txt_CMTND.Text.Trim() + "',Diachi=N'" + txtAddress.Text.Trim() + "', Mobile=N'" + txtPhoneNumber.Text.Trim() + "', Ma_Vung=" + cbo_vungmien.SelectedValue + " where loai=0 and NguoiTao=" + _user.UserID;
                ulti.ExecSql(_sql_update);
                Thaotac = "Thao tác đổi thông tin của người dùng:" + _user.UserFullName;
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["key"].ToString()), Thaotac);
                FuncAlert.AlertJS(this, "Đổi thông tin người dùng thành công!");
                return;
            }

        }

    }
}
