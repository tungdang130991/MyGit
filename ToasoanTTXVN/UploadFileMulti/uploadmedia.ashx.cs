using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using System.IO;
using System.Text.RegularExpressions;
using SSOLib.ServiceAgent;
using HPCInfo;

namespace CMSVNPInternet.UploadVideos
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class uploadmedia : IHttpHandler
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users user = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files["Filedata"];
                string[] sArrProdID = null;
                char[] sep = { '?' };
                string[] sArrVkey = null;
                string strUserID = string.Empty;
                char[] sep2 = { ',' };
                sArrProdID = context.Request.QueryString["user"].ToString().Trim().Split(sep);
                sArrVkey = sArrProdID[0].ToString().Trim().Split(sep2);
                string chuyenmuc = sArrVkey[sArrVkey.Length - 1];
                string FolderCat = string.Empty;
                string savepath = string.Empty;
                string tempPath = string.Empty;
                string strRootPathVirtual = string.Empty;
                user = _NguoidungDAL.GetUserByUserName(sArrVkey[0].ToString());
                strUserID = user.UserID.ToString();
                string vType = sArrVkey[1].ToString();
                if (vType == "1")
                    FolderCat = "/Adv/";
                else if (vType == "2")
                    FolderCat = "/Videos/";
                else
                    FolderCat = string.Empty;
                tempPath = "/" + System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + FolderCat;
                strRootPathVirtual = tempPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                savepath = context.Server.MapPath(strRootPathVirtual);

                string filename = getFileNameUnique(savepath + @"\", postedFile.FileName, Path.GetFileNameWithoutExtension(postedFile.FileName), Path.GetExtension(postedFile.FileName.ToString()));
                string _extenfile = Path.GetExtension(filename.ToString());
                string strFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName).Trim();
                if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);
                if (_extenfile.ToLower().Contains(".mp4")
                   || _extenfile.ToLower().Contains(".flv")
                   || _extenfile.ToLower().Contains(".mp3")
                   || _extenfile.ToLower().Contains(".jpg")
                   || _extenfile.ToLower().Contains(".gif")
                   || _extenfile.ToLower().Contains(".bmp")
                   || _extenfile.ToLower().Contains(".png")
                   || _extenfile.ToLower().Contains(".doc")
                   || _extenfile.ToLower().Contains(".docx")
                   || _extenfile.ToLower().Contains(".pdf"))
                    postedFile.SaveAs(savepath + @"\" + filename);

                string _urlSave = UrlPathImage_RemoveUpload(strRootPathVirtual + filename);

                //phan insert co so du lieu
                T_ImageFiles _obj = new T_ImageFiles();
                ImageFilesDAL _DAL = new ImageFilesDAL();
                _obj = SetItem(filename, postedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(strUserID), Convert.ToInt16(vType), 0);

                int _idReturn = _DAL.InsertT_ImageFiles(_obj);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }

        }
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
        }
        protected T_ImageFiles SetItem(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType, Int16 intFileType)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            _obj.ID = 0;
            _obj.ImageFileName = _tenFile.ToString();
            _obj.ImageFileSize = _size;
            _obj.ImageFileExtension = _extenfile.ToString();
            _obj.ImageType = vType;
            _obj.ImgeFilePath = _pathfile.ToString();
            _obj.Status = 0;
            _obj.UserCreated = _userID;
            _obj.DateCreated = DateTime.Now;
            _obj.FileType = intFileType;

            return _obj;
        }
        protected string getFileNameUnique(string ImageDirectory, string strFileName, string fileNameWithoutExtension, string FileExtension)
        {
            int count = 1;
            string strFileNameUnique = ReplaceCharsRewrite(fileNameWithoutExtension) + FileExtension;
            string ImagePath = ImageDirectory + strFileNameUnique;
            while (System.IO.File.Exists(ImagePath))
            {

                ImagePath = string.Concat(ImageDirectory, ReplaceCharsRewrite(fileNameWithoutExtension), "-", count.ToString(), FileExtension);
                strFileNameUnique = ReplaceCharsRewrite(fileNameWithoutExtension) + "-" + count.ToString() + FileExtension;
                count++;

            }

            return strFileNameUnique;
        }
        protected string RemoveHTMLTag(string HTML)
        {
            // Xóa các thẻ html
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            return objRegEx.Replace(HTML, "");

        }
        protected string ReplaceCharsRewrite(object input)
        {
            string str = "", StrTemp = RemoveHTMLTag(Convert.ToString(input));
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = StrTemp.Normalize(System.Text.NormalizationForm.FormD);
            str = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex objRegEx = new Regex("<[^>]*>");
            str = str.Replace(" ", "-");
            str = str.Replace(",", "");
            str = str.Replace(".", "");
            str = str.Replace(";", "");
            str = str.Replace(":", "");
            str = str.Replace("?", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("!", "");
            str = str.Replace("@", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("+", "");
            str = str.Replace("=", "");
            str = str.Replace("\\", "");
            str = str.Replace("|", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("”", "");
            str = str.Replace("“", "");
            str = str.Replace("-»", "");
            str = str.Replace("«-", "");
            str = str.Replace("»", "");
            str = str.Replace("»", "");
            str = str.Replace("«", "");
            str = str.Replace("’", "");
            str = str.Replace("--", "-");
            str = str.Replace("---", "-");
            str = str.Replace("----", "-");
            str = str.Replace("-----", "-");
            str = str.Replace(" ", "-");
            //Add by nvthai
            str = str.Replace("Ã°", "");
            str = str.Replace("â€", "");
            str = str.Replace("a€", "");
            str = str.Replace("a°", "");
            return str.ToLower();
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
