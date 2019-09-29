using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WDF.Component;
using HPCComponents;
using HPCBusinessLogic;

namespace ToasoanTTXVN.TTXTraCuu
{
    public partial class TraCuuTin : BasePage
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user == null)
            {
                Response.Redirect(HPCComponents.Global.ApplicationPath + "/login.aspx", true);
            }
            else
            {
                this.txt_FromDate.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected string IpClient()
        {
            string strIp;
            strIp = Page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = Page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
    }
}
