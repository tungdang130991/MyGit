using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Globalization;
namespace ToasoanTTXVN
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string lang = "vi";
            if (Session["culture"] != null)
            {
                lang = Convert.ToString(Session["culture"]);
            }

            string culture = string.Empty;
            if (lang.ToLower().CompareTo("en") == 0 || string.IsNullOrEmpty(culture))
            {
                culture = "en-US";
            }
            if (lang.ToLower().CompareTo("vi") == 0)
            {
                culture = "vi-VN";
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }
    }
}
