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

namespace ToasoanTTXVN.QlyAnh
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class HandlerUpload : IHttpHandler
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
                int matinbai = 0;
                if (sArrVkey[1].ToString() != "")
                    matinbai = int.Parse(sArrVkey[1].ToString());
                HttpPostedFile postedFile = context.Request.Files["Filedata"];

                string tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";

                //Create forder
                CreateFolderByUserName(tempPath);

                string savepath = context.Server.MapPath("/" + tempPath);
                string filename = postedFile.FileName;
                string[] sArrTenfile = null;
                char[] cat = { '.' };
                sArrTenfile = filename.ToString().Trim().Split(cat);
                string _extenfile = GetDateTimeStringUnique() + "." + sArrTenfile[1].ToString();
                string _tenfilegoc = UltilFunc.RemoveSign4VietnameseString(Path.GetFileNameWithoutExtension(filename)) + "." + _extenfile.ToString();

                _tenfilegoc = _tenfilegoc.Replace(" ", "");
                postedFile.SaveAs(savepath + @"\" + _tenfilegoc);

                int startchar = tempPath.Substring(1, tempPath.Length - 1).IndexOf("/");
                startchar += 1;
                string _PathFile = tempPath.Substring(startchar, tempPath.Length - startchar);
                string _savePath = _PathFile + "" + _tenfilegoc;

                AnhDAL _DAL = new AnhDAL();
                T_Anh _obj = new T_Anh();
                _obj = SetItem(_savePath, _tenfilegoc, _tenfilegoc);
                int _MaAnhReturn = _DAL.InsertUpdateT_Anh(_obj);
                if (matinbai > 0 && _MaAnhReturn > 0)
                {
                    TinBaiAnhDAL _daltinanh = new TinBaiAnhDAL();
                    T_Tinbai_Anh _objTA = new T_Tinbai_Anh();
                    _objTA.ID = 0;
                    _objTA.Ma_Anh = _MaAnhReturn;
                    _objTA.Ma_TinBai = matinbai;
                    _objTA.ChuThich = "";
                    _daltinanh.InsertUpdateTin_Anh(_objTA);
                }

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
        private T_Anh SetItem(string pathOrg, string _tenfilegoc, string _tenfileht)
        {
            T_Anh _objPhoto = new T_Anh();
            _objPhoto.Ma_Anh = 0;
            _objPhoto.TenFile_Goc = _tenfilegoc;
            _objPhoto.TenFile_Hethong = _tenfileht;
            _objPhoto.TieuDe = "";
            _objPhoto.Chuthich = "";
            _objPhoto.NgayTao = DateTime.Now;
            _objPhoto.Duongdan_Anh = pathOrg.ToString();
            _objPhoto.Nhuanbut = 0;
            _objPhoto.NguoiTao = user.UserID;
            _objPhoto.Thanhtoan = false;
            _objPhoto.Nguoithanhtoan = 0;
            _objPhoto.Nguoicham = 0;
            _objPhoto.TuKhoa = "";
            _objPhoto.NguoiChup = "";
            _objPhoto.Ma_Nguoichup = 0;
            _objPhoto.Duyet = true;
            _objPhoto.Nhanxet = "";
            return _objPhoto;
        }
    }
}
