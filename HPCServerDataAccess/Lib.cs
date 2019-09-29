using System;
using System.Web;
using System.Collections;
using System.Web.Caching;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Data;
using HPCInfo;
using HPCShareDLL;
using System.Web.UI;

namespace HPCServerDataAccess
{
    public class Lib
    {
        public static string SqlFormatText(string text)
        {
            if (text == null) return "";
            else return text.Replace("'", "''");
        }
        public static string SqlDeFormatText(string text)
        {
            if (text == null) return "";
            else return text.Replace("''", "'");
        }

        public static void ShowAlertMessage(string error)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {

                error = error.Replace("'", "\'");

               page.ClientScript.RegisterStartupScript(page.GetType(), "err_msg", "alert('" + error + "');", true);

            }

        }
        public static void RunJavaScriptCode(string JavaScriptCode)
        {
            try
            {
                Page mPage = (Page)HttpContext.Current.Handler;
                mPage.ClientScript.RegisterStartupScript(typeof(Page), "", JavaScriptCode, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
    }
}
