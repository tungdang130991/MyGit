using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Danhmuc
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadFile1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files["Filedata"];

                string savepath = "";
                string tempPath = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPathAnPham"];
                savepath = context.Server.MapPath("~/"+tempPath);
                string filename = postedFile.FileName;
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                postedFile.SaveAs(savepath + @"\" + filename);
                context.Response.Write(tempPath + "/" + filename);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
