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
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;

namespace ToasoanTTXVN
{
    public partial class Login : System.Web.UI.Page
    {
        T_UserLogin _userLog = null;
        HPCBusinessLogic.DAL.LoginChatDAL _dalchat = new HPCBusinessLogic.DAL.LoginChatDAL();
        NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users user = null;
        UltilFunc _ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["capchaimgvna"] != null)
            {
                if (Convert.ToInt32(Session["capchaimgvna"]) >= 3)
                {
                    this.pnlcapcha.Visible = true;
                }
                else
                {
                    this.pnlcapcha.Visible = false;
                }
            }
            else
            {
                this.pnlcapcha.Visible = false;
            }
            HttpCookie cookie = Request.Cookies["hpcinfomation"];
            if (cookie != null)
            {
                object _Username = cookie["hpcUserNames"];
                object _Password = cookie["hpcPassword"];
                if (_Username != null && _Password != null)
                {
                    user = _userDAL.GetUserByUserPass(_Username.ToString(), HPCSecurity.Encrypt(_Password.ToString()));
                    if (user != null)
                        if (user.UserName.Trim() == _Username.ToString() && _Password.ToString() == user.UserPass.Trim())
                        {
                            FormsAuthentication.SetAuthCookie(user.UserName, false);
                            Response.Redirect(Global.ApplicationPath + "/Default.aspx");
                        }
                }
            }
            txtUsername.Focus();

            if (!IsPostBack)
            {
                FillCapctha();
            }

        }
        public T_UserLogin SetItem(T_Users _user)
        {

            T_UserLogin _obj = new T_UserLogin();
            _obj.ID = 0;
            _obj.Login_Date = DateTime.Now;
            _obj.Session_ID = Session.SessionID.ToString();
            _obj.User_Ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
            _obj.User_Name = _user.UserName;
            _obj.LastMessage_Time = DateTime.Now;
            _obj.Full_Name = _user.UserFullName;
            _obj.Loggedin = true;
            return _obj;
        }
        private string GenerateRandomCode()
        {
            var chars = "ABCDEFGHKMNPQRSTUVWXYZ123456789abcdefghkmnpqrstuvwxyz";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        void FillCapctha()
        {

            Random random = new Random();

            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            StringBuilder captcha = new StringBuilder();

            for (int i = 0; i < 6; i++)

                captcha.Append(combination[random.Next(combination.Length)]);

            Session["captcha"] = captcha.ToString();

            imgCaptcha.ImageUrl = "Captcha.ashx?" + DateTime.Now.Ticks.ToString();
        }

        protected void imbReLoad_Click(object sender, ImageClickEventArgs e)
        {
            FillCapctha();
        }
        protected void btLogon_Click(object sender, EventArgs e)
        {
            if (_userDAL.Login(txtUsername.Text, txtPassword.Text))
            {
                if (Convert.ToInt32(Session["capchaimgvna"]) >= 3)
                {
                    if (Session["captcha"] != null)
                    {
                        if (Session["captcha"].ToString() != txtCaptcha.Text)
                        {
                            FuncAlert.AlertJS(this, "Mã bảo mật không đúng!");
                            return;
                        }

                    }
                }
                if (this.txtUsername.Text.Length > 0 && this.txtPassword.Text.Length > 0)
                    user = _userDAL.GetUserByUserPass(txtUsername.Text, txtPassword.Text);
                if (user.UserActive)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    HttpCookie cookie = new HttpCookie("hpcinfomation");
                    cookie.Values.Add("hpcUserNames", txtUsername.Text.Trim());
                    cookie.Values.Add("hpcPassword", txtPassword.Text.Trim());
                    cookie.Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies.Add(cookie);
                    //Phan message
                    _userLog = SetItem(user);
                    _dalchat.InsertT_UserLogin(_userLog);
                    //end
                    UltilFunc.Log_Action(user.UserID, user.UserFullName, DateTime.Now, 0, "Đăng nhập hệ thống thành công");
                    
                    Response.Redirect(Global.ApplicationPath + "/Dungchung/Commons.aspx");

                }
                else
                {
                    if (Session["capchaimgvna"] == null)
                    {
                        Session["capchaimgvna"] = "1";
                    }
                    else
                    {
                        Session["capchaimgvna"] = Convert.ToInt32(Session["capchaimgvna"]) + 1;
                        if (Convert.ToInt32(Session["capchaimgvna"]) >= 3)
                        {
                            pnlcapcha.Visible = true;
                        }
                    }
                    FuncAlert.AlertJS(this, "Tài khoản của bạn đang bị khóa. Liên hệ quản trị!");
                    FillCapctha();

                }
            }
            else
            {
                if (Session["capchaimgvna"] == null)
                {
                    Session["capchaimgvna"] = "1";
                }
                else
                {
                    Session["capchaimgvna"] = Convert.ToInt32(Session["capchaimgvna"]) + 1;
                    if (Convert.ToInt32(Session["capchaimgvna"]) >= 3)
                    {
                        this.pnlcapcha.Visible = true;
                    }
                }
                FuncAlert.AlertJS(this, "Đăng nhập không thành công");
                FillCapctha();
            }

        }

    }
}
