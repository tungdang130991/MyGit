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

namespace ToasoanTTXVN.UploadFileMulti
{
    public partial class ViewImage : System.Web.UI.Page
    {
        public string Imagescr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Imagescr = System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + Page.Request.QueryString["Url"].ToString();
        }
    }
}
