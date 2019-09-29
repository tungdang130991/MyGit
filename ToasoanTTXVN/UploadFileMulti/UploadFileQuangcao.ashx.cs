using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.UploadFileMulti
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadFileQuangcao : IHttpHandler
    {
        HPCBusinessLogic.DAL.QuangCaoDAL _dalqc = new HPCBusinessLogic.DAL.QuangCaoDAL();
        T_Users user = new T_Users();
        NguoidungDAL DAL = new NguoidungDAL();

        double _maquangcao = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                string strParram = context.Request.QueryString["userid"] == null ? "" : context.Request.QueryString["userid"].ToString();
                string[] parramList = strParram.Split(',');
                if (parramList[0].ToString() != "")
                    user = DAL.GetUserByUserName(parramList[0].ToString());
                if (parramList[1].ToString() != "")
                    _maquangcao = int.Parse(parramList[1].ToString());

                HttpPostedFile postedFile = context.Request.Files["Filedata"];


                string tempPath = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderQuangCao"] + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";

                string filename = postedFile.FileName;
                string[] sArrTenfile = null;
                char[] cat = { '.' };
                sArrTenfile = filename.ToString().Trim().Split(cat);
                string _extenfile = GetDateTimeStringUnique() + "." + sArrTenfile[1].ToString();

                string _tenfilegoc = UltilFunc.RemoveSign4VietnameseString(Path.GetFileNameWithoutExtension(filename)) + "." + _extenfile.ToString();
                _tenfilegoc = _tenfilegoc.Replace(" ", "");
                string _PathFileAtt = tempPath + _tenfilegoc;
                if (ImageExtention("." + _extenfile))
                {
                    // Upload FTP
                    string ftpuser = ConfigurationManager.AppSettings["FTP_Username"].ToString();
                    string password = ConfigurationManager.AppSettings["FTP_Password"].ToString();
                    string ftpServer = ConfigurationManager.AppSettings["FTP_Server"].ToString();
                    FtpClient ftp = new FtpClient(ftpServer, ftpuser, password, "");
                    BinaryReader b = new BinaryReader(postedFile.InputStream);
                    byte[] binData = b.ReadBytes(postedFile.ContentLength);
                    ftp.UploadFile(binData, _PathFileAtt);
                    //end
                    T_FileQuangCao _obj = new T_FileQuangCao();
                    _obj = SetItem(_PathFileAtt, _tenfilegoc);
                    _dalqc.Sp_InsertT_FileQuangCao(_obj);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }
        private T_FileQuangCao SetItem(string _PathFile, string _FileGoc)
        {
            T_FileQuangCao _objqc = new T_FileQuangCao();
            _objqc.ID = 0;
            _objqc.Ma_Quangcao = _maquangcao;
            _objqc.PathFile = _PathFile;
            _objqc.FileRoot = _FileGoc;
            _objqc.Ngaytao = DateTime.Now;
            _objqc.Nguoitao = user.UserID;
            return _objqc;
        }
        public bool ImageExtention(string extention)
        {
            if (extention.ToLower().Contains(".jpg")
                  || extention.ToLower().Contains(".gif")
                  || extention.ToLower().Contains(".png")
                  || extention.ToLower().Contains(".bmp")
                  || extention.ToLower().Contains(".jpeg")
                || extention.ToLower().Contains(".doc")
                || extention.ToLower().Contains(".docx")
                || extention.ToLower().Contains(".pdf"))
            {
                return true;
            }
            else
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
            if (Directory.Exists(FolderName) == false)
                Directory.CreateDirectory(FolderName);
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
