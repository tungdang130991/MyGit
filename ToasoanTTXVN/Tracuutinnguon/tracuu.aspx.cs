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
namespace ToasoanTTXVN.Tracuutinnguon
{
    public partial class tracuu : System.Web.UI.Page
    {
        protected HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user == null) Response.Redirect(Global.ApplicationPath + "/login.aspx", true);
            txt_BlackList.Text = ConfigurationManager.AppSettings["BlackListCate_TinTuLieu"].ToString();
        }
    }
}
