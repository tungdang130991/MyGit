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
    public partial class EditUser : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        UltilFunc ulti = new UltilFunc();
        int UserID_SSO = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadComBo();
                        DataBind();

                    }
                }
            }
        }

        public string GetCMTND(int _userid, string CustomerName)
        {
            string CMTND = string.Empty;
            DataTable _dt = new DataTable();
            string _sql = "select top 1 CMTND from T_Nguoidung where  Loai=0 and Nguoitao=" + _userid;
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                CMTND = _dt.Rows[0]["CMTND"].ToString();
            else
                CMTND = "";
            return CMTND;
        }
        private bool isExist_SSO(string CustomerName, double ID)
        {
            DataTable d = _userDAL.GetT_User_Dynamic(" UserName=N'" + CustomerName + "'  and UserID<>" + ID.ToString()).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExist_SSO(string CustomerName)
        {
            DataTable d = _userDAL.GetT_User_Dynamic(" UserName=N'" + CustomerName + "'").Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExistCMTND(int _userid, string CustomerName, string CMTND)
        {
            DataTable _dt = new DataTable();
            string _sql = "select top 1 CMTND from T_Nguoidung where Ten_Dangnhap=N'" + CustomerName + "' and CMTND=N'" + CMTND + "' and Loai=0 and Nguoitao<>" + _userid.ToString();
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExistCMTND(string CustomerName, string CMTND)
        {
            DataTable _dt = new DataTable();
            string _sql = "select top 1 CMTND from T_Nguoidung where Ten_Dangnhap=N'" + CustomerName + "' and CMTND=N'" + CMTND + "' and Loai=0 ";
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private void LoadComBo()
        {
            cbo_vungmien.Items.Clear();
            cbo_phongban.Items.Clear();
            UltilFunc.BindCombox(cbo_vungmien, "Ma_Vung", "Ten_Vung", "T_Vung", " 1=1  Order by Ten_Vung", CommonLib.ReadXML("lblChongiatri"));
            UltilFunc.BindCombox(cbo_phongban, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1  Order by Ten_Phongban", CommonLib.ReadXML("lblChongiatri"));
        }
        public T_Users SetItem()
        {
            T_Users _obj = new T_Users();
            if (Page.Request.Params["id"] != null)
            {
                _obj.UserID = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.UserID = 0;
            _obj.UserName = UltilFunc.SqlFormatText(this.txtUserName.Text.Trim());
            _obj.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(password1.Text, "sha1");
            _obj.UserEmail = this.txtEmail.Text.Trim();
            if (this.txtBirth.Text.Length > 0)
                _obj.UserBirthday = UltilFunc.ToDate(this.txtBirth.Text, "dd/MM/yyyy");
            _obj.UserAddress = this.txtAddress.Text.Trim();
            _obj.UserFullName = this.txtFullName.Text.Trim();
            _obj.UserMobile = this.txtPhoneNumber.Text.Trim();
            _obj.DateCreated = DateTime.Now;
            _obj.DateModify = DateTime.Now;
            _obj.UserCreate = 0;
            _obj.IsLoai = 0;
            if (cbo_phongban.SelectedIndex > 0)
                _obj.ProvinceID = int.Parse(cbo_phongban.SelectedValue);
            else
                _obj.ProvinceID = 0;
            _obj.UserActive = this.ckActivec.Checked;
            _obj.RegionID = int.Parse(cbo_vungmien.SelectedValue.ToString());
            return _obj;
        }
        public T_Nguoidung SetItemNguoidung(int UserID)
        {
            T_Nguoidung _obj = new T_Nguoidung();

            _obj.Ma_Nguoidung = 0;
            _obj.Ten_Dangnhap = UltilFunc.SqlFormatText(txtUserName.Text.Trim());
            if (txtBirth.Text.Length > 0)
                _obj.Ngaysinh = UltilFunc.ToDate(this.txtBirth.Text, "dd/MM/yyyy");
            _obj.Diachi = this.txtAddress.Text.Trim();
            _obj.TenDaydu = this.txtFullName.Text.Trim();
            if (txt_CMTND.Text.Trim().Length > 0)
                _obj.CMTND = txt_CMTND.Text.Trim();
            else
                _obj.CMTND = "";
            _obj.Mobile = this.txtPhoneNumber.Text.Trim();
            _obj.Email = this.txtEmail.Text.Trim();
            _obj.Hoatdong = ckActivec.Checked;
            _obj.NgayTao = DateTime.Now;
            _obj.NguoiTao = UserID;
            _obj.Loai = 0;
            _obj.Ma_Vung = int.Parse(cbo_vungmien.SelectedValue.ToString());
            _obj.Ma_PhongBan = int.Parse(cbo_phongban.SelectedValue);
            return _obj;
        }
        private void PopulateItem(int _id)
        {
            T_Users obj = new T_Users();
            obj = _userDAL.GetUserByUserName_ID(_id);
            this.txtUserName.Text = obj.UserName;
            txtUserName.ReadOnly = true;
            txt_CMTND.Text = GetCMTND(obj.UserID, obj.UserName);
            this.txtEmail.Text = obj.UserEmail;
            this.txtFullName.Text = obj.UserFullName;
            this.txtPhoneNumber.Text = obj.UserMobile;
            this.txtAddress.Text = obj.UserAddress;
            this.ckActivec.Checked = obj.UserActive;
            this.txtAddress.Text = obj.UserAddress;
            if (obj.UserBirthday != null && obj.UserBirthday != DateTime.MaxValue && obj.UserBirthday != DateTime.MinValue)
                this.txtBirth.Text = obj.UserBirthday.ToString("dd/MM/yyyy");
            else
                this.txtBirth.Text = "";
            cbo_vungmien.SelectedIndex = CommonLib.GetIndexControl(cbo_vungmien, obj.RegionID.ToString());
            cbo_phongban.SelectedIndex = CommonLib.GetIndexControl(cbo_phongban, obj.ProvinceID.ToString());
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
        private bool checkform()
        {
            if (cbo_vungmien.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Bạn phải chọn vùng miền!");
                return false;
            }

            return true;
        }
        private void Clear()
        {
            this.txtUserName.Text = "";
            this.txtFullName.Text = "";
            password1.Text = "";
            password2.Text = "";
            txt_CMTND.Text = "";
            this.txtEmail.Text = "";
            this.txtPhoneNumber.Text = "";
            this.txtAddress.Text = "";
            this.ckActivec.Checked = false;
            this.txtAddress.Text = "";
            this.txtBirth.Text = "";
            cbo_vungmien.SelectedIndex = 0;
            cbo_phongban.SelectedIndex = 0;
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {

            string _thaotac = string.Empty;
            T_Users _obj = SetItem();
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                UserID_SSO = int.Parse(Request["ID"].ToString());
            if (UserID_SSO == 0)
            {
                if (password1.Text.Trim() != password2.Text.Trim())
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblMatkhaukhongphuhop"));
                    return;
                }
                Regex regex5 = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@$%^&*-])(?=^.{6,100}$)");

                Match match5 = regex5.Match(password1.Text);
                if (!match5.Success)
                {
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblMatkhauthongbao"));
                    return;
                }
                if (!isExist_SSO(txtUserName.Text.Trim()))
                {
                    if (!isExistCMTND(txtUserName.Text.Trim(), txt_CMTND.Text.Trim()))
                    {
                        if (!Page.IsValid) return;
                        UserID_SSO = _userDAL.InsertT_Users(_obj);
                        T_Nguoidung _objnguoidung = SetItemNguoidung(UserID_SSO);
                        _userDAL.InsertT_Nguoidung(_objnguoidung);
                        _thaotac = "Thao tác thêm mới người dùng:" + _obj.UserFullName + "-->[USERID:" + UserID_SSO.ToString() + " ]";
                        Clear();
                    }
                    else
                    {
                        FuncAlert.AlertJS(this, CommonLib.ReadXML("lblCMdatontai"));
                        return;
                    }
                }
                else
                {

                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblTaikhoandatontai"));
                    return;

                }

            }
            else
            {
                if (!isExist_SSO(txtUserName.Text.Trim(), double.Parse(Page.Request["ID"].ToString())))
                {
                    if (!isExistCMTND(UserID_SSO, txtUserName.Text.Trim(), txt_CMTND.Text.Trim()))
                    {
                        UserID_SSO = _userDAL.InsertT_Users(_obj);
                        string _sql_update = "update T_Nguoidung set Ngaysinh=convert(datetime,'" + txtBirth.Text.Trim() + "',103),CMTND=N'" + txt_CMTND.Text.Trim() + "',Diachi=N'" + txtAddress.Text.Trim() + "', Mobile=N'" + txtPhoneNumber.Text.Trim() + "', Ma_Vung=" + cbo_vungmien.SelectedValue + ",Ma_PhongBan=" + cbo_phongban.SelectedValue + " where Loai=0 and NguoiTao=" + UserID_SSO;
                        ulti.ExecSql(_sql_update);
                        _thaotac = "Thao tác sửa thông tin người dùng: " + _obj.UserFullName + "[USERID:" + UserID_SSO.ToString() + " ]";

                    }
                    else
                    {
                        FuncAlert.AlertJS(this, CommonLib.ReadXML("lblCMdatontai"));
                        return;
                    }
                }
                else
                {

                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblTaikhoandatontai"));
                    return;

                }
                Clear();
            }
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), _thaotac);
            FuncAlert.AlertJS(this, "Thêm mới thành công");

        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Hethong/ListUser.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                pnlPass.Visible = false;
                lblkhuyencao.Visible = false;
                int _id = Convert.ToInt32(Request["ID"].ToString());
                PopulateItem(_id);
            }
            else
            {
                lblkhuyencao.Visible = true;
                Clear();
                pnlPass.Visible = true;
                ckActivec.Checked = true;

            }
        }
    }
}
