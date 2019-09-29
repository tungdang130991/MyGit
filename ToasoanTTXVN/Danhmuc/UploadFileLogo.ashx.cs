using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;
using HPCComponents;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ToasoanTTXVN.Danhmuc
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadFileLogo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            HttpPostedFile postedFile = context.Request.Files["Filedata"];

            string tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderLogo"].ToString() + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";

            //Create forder
            CreateFolderByUserName(tempPath);
            string savepath = context.Server.MapPath("~" + tempPath);
            string filename = postedFile.FileName;

            string[] sArrTenfile = null;
            char[] cat = { '.' };
            sArrTenfile = filename.ToString().Trim().Split(cat);
            string _extenfile = sArrTenfile[1].ToString();
            string _tenfile = GetDateTimeStringUnique() + "." + _extenfile.ToString();
            string _tenfilegoc = UltilFunc.RemoveSign4VietnameseString(Path.GetFileNameWithoutExtension(filename)) + "." + _extenfile.ToString();

            string fiName = _tenfile.Replace(" ", "");
            postedFile.SaveAs(savepath + @"\" + fiName);
            //Luu vao CSDL

            string _savePath = tempPath + "" + fiName;
            HPCBusinessLogic.DAL.T_LogoDAL _DAL = new HPCBusinessLogic.DAL.T_LogoDAL();
            T_Logo _obj = new T_Logo();
            _obj = SetItem(_savePath, _tenfilegoc);
            int _IDreturn = _DAL.InsertUpdateT_Logo(_obj);
            //end

            context.Response.Write(tempPath + "/" + filename);
            context.Response.StatusCode = 200;
        }
        public string GetDateTimeStringUnique()
        {
            string dateString = DateTime.Now.Millisecond.ToString();
            return dateString + DateTime.Now.ToLongTimeString().Replace("-", "").Replace(" ", "").Replace(":", "");
        }
        private void CreateFolderByUserName(string FolderName)
        {
            string strRootPath = "";
            strRootPath = HttpContext.Current.Server.MapPath("~" + FolderName);
            if (Directory.Exists(strRootPath) == false) Directory.CreateDirectory(strRootPath);

        }
        private T_Logo SetItem(string pathOrg, string _tenfilegoc)
        {
            T_Logo _objPhoto = new T_Logo();
            _objPhoto.Ma_Logo = 0;
            _objPhoto.Ten_Logo = "Nothing";
            _objPhoto.FileName_Logo = _tenfilegoc;
            _objPhoto.Path_Logo = pathOrg;

            return _objPhoto;
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
