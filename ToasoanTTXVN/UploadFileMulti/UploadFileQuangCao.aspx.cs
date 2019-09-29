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
using System.IO;

namespace ToasoanTTXVN.UploadFileMulti
{
    public partial class UploadFileQuangCao : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindImages();
            }
        }

        public DataView BindGridPhotoUploaded()
        {

            try
            {

                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("PhotoPath", typeof(string)));
                dt.Columns.Add(new DataColumn("LinkPhotoPath", typeof(string)));

                DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["FolderQuangCao"]));
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo oItem in files)
                {
                    if (oItem.Extension.ToLower() == ".jpg" || oItem.Extension.ToLower() == ".doc" || oItem.Extension.ToLower() == ".jpeg" || oItem.Extension.ToLower() == ".gif" || oItem.Extension.ToLower() == ".png")
                    {
                        dr = dt.NewRow();
                        string _linkImage = Global.ApplicationPath + Global.PathQuangCao + oItem.Name.ToString();
                        dr[0] = _linkImage;
                        dr[1] = Global.PathQuangCao + oItem.Name.ToString();
                        dt.Rows.Add(dr);
                    }

                }
                DataView dv = new DataView(dt);
                return dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void BindImages()
        {
            DataView dv = BindGridPhotoUploaded();
            dlImages.DataSource = dv;
            dlImages.DataBind();
        }

    }
}
