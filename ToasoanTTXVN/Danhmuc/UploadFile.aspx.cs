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

namespace ToasoanTTXVN.Danhmuc
{
    public partial class UploadFile : System.Web.UI.Page
    {
        string _connString = HttpContext.Current.Server.MapPath("~" + ConfigurationManager.AppSettings["FolderPathAnPham"].ToString().ToString());
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

                DirectoryInfo dir = new DirectoryInfo(_connString);
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo oItem in files)
                {
                    if (oItem.Extension == ".jpg" || oItem.Extension == ".jpeg" || oItem.Extension == ".gif" || oItem.Extension == ".png")
                    {
                        dr = dt.NewRow();
                        string _linkImage = Global.ApplicationPath + System.Configuration.ConfigurationManager.AppSettings["FolderPathAnPham"].ToString() + oItem.Name.ToString();
                        dr[0] = _linkImage;
                        dr[1] = System.Configuration.ConfigurationManager.AppSettings["FolderPathAnPham"].ToString() + oItem.Name.ToString();
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
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString(), "");
        }
        protected string getPath2ImageFile(object objUrlPath)
        {
            string strPathFull = "";

            strPathFull = System.Configuration.ConfigurationManager.AppSettings["FolderPathAnPham"].ToString() + objUrlPath.ToString();

            return strPathFull;
        }

    }
}
