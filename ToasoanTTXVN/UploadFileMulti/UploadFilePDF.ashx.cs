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
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.UploadFileMulti
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadFilePDF : IHttpHandler
    {
        T_Users user = new T_Users();
        NguoidungDAL DAL = new NguoidungDAL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            try
            {
                string[] sArrProdID = null;
                char[] sep = { '?' };
                string[] sArrVkey = null;
                string strUserID = "";
                char[] sep2 = { ',' };
                sArrProdID = context.Request.QueryString["user"].ToString().Trim().Split(sep);
                sArrVkey = sArrProdID[0].ToString().Trim().Split(sep2);
                user = DAL.GetUserByUserName(sArrVkey[0].ToString());
                strUserID = user.UserID.ToString();
                int _Trang = 0;
                double _Sobao = 0;
                int _Index = 0;
                if (sArrVkey[1].ToString() != "")
                    _Trang = int.Parse(sArrVkey[1].ToString());
                if (sArrVkey[2].ToString() != "")
                    _Sobao = double.Parse(sArrVkey[2].ToString());
                if (sArrVkey[3].ToString() != "")
                    _Index = int.Parse(sArrVkey[3].ToString());
                HttpPostedFile postedFile = context.Request.Files["Filedata"];

                string tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString() + "PDF/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";

                //Create forder
                CreateFolderByUserName(tempPath);

                string savepath = context.Server.MapPath("/" + tempPath);
                string filename = postedFile.FileName;
                string[] sArrTenfile = null;
                char[] cat = { '.' };
                sArrTenfile = filename.ToString().Trim().Split(cat);
                string _extenfile = GetDateTimeStringUnique() + "." + sArrTenfile[1].ToString();
                string _tenfilegoc = UltilFunc.RemoveSign4VietnameseString(Path.GetFileNameWithoutExtension(filename)) + _extenfile;

                _tenfilegoc = _tenfilegoc.Replace(" ", "");
                postedFile.SaveAs(savepath + @"\" + _tenfilegoc);
                int startchar = tempPath.Substring(1, tempPath.Length - 1).IndexOf("/");
                startchar += 1;
                string _PathFile = tempPath.Substring(startchar, tempPath.Length - startchar);
                string _savePath = _PathFile + "" + _tenfilegoc;

                HPCBusinessLogic.DAL.TinBaiDAL _DAL = new HPCBusinessLogic.DAL.TinBaiDAL();
                T_Publish_Pdf _obj = new T_Publish_Pdf();
                _obj = SetItem(_savePath, _Trang, _Sobao, _Index);
                _DAL.InsertT_Publish_PDF(_obj);

                context.Response.Write(tempPath + "/" + _tenfilegoc);
                context.Response.StatusCode = 200;



            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }
        private T_Publish_Pdf SetItem(string PathFilePDF, int _trang, double _sobao, int _index)
        {
            T_Publish_Pdf _objpdf = new T_Publish_Pdf();
            _objpdf.Publish_Pdf = PathFilePDF;
            _objpdf.Page_Number = _trang.ToString();
            _objpdf.Publish_Number_ID = _sobao;
            if (_index == 1)
                _objpdf.Status = 1;
            if (_index == 2)
                _objpdf.Status = 2;
            if (_index == 3)
                _objpdf.Status = 3;
            _objpdf.Comments = "";
            return _objpdf;
        }

        public string GetDateTimeStringUnique()
        {
            string dateString = DateTime.Now.Millisecond.ToString();
            return dateString + DateTime.Now.ToLongTimeString().Replace("-", "").Replace(" ", "").Replace(":", "");
        }
        private void CreateFolderByUserName(string FolderName)
        {
            string strRootPath = "";
            strRootPath = HttpContext.Current.Server.MapPath("/" + FolderName);
            if (Directory.Exists(strRootPath) == false)
                Directory.CreateDirectory(strRootPath);
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
