using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using HPCComponents;
using System.Collections;
using System.Text.RegularExpressions;
using HPCInfo;

namespace ToasoanTTXVN.BaoDienTu
{
    public class SynFiles
    {
        #region DONG BO FILE CLIENT
        public void SynData_UploadImg(string path, string urlService)
        {
            if (urlService.Length > 6)
            {
                PutDataBusinessLogic.UltilFunc _untilDAL = new PutDataBusinessLogic.UltilFunc(urlService);
                string fileName = string.Empty;
                string fullPath = string.Empty;
                string Upload = Global.UploadPathBDT;
                fullPath = HttpContext.Current.Server.MapPath(Upload) + path.Replace("/", "\\");
                fileName = Path.GetFileName(fullPath);
                string path_Directory = path.Replace("/" + fileName, "");
                try
                {
                    FileInfo fInfo = new FileInfo(fullPath);
                    long numBytes = fInfo.Length;
                    double dLen = Convert.ToDouble(fInfo.Length / 1000000);
                    //if (dLen < 4)
                    //{
                    // set up a file stream and binary reader for the
                    // selected file
                    FileStream fStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();
                    //_untilDAL.UploadFile(data, fileName, path_Directory);
                    _untilDAL.UploadFile(data, fileName, path_Directory.ToLower().Replace(Upload, ""));

                    fStream.Close();
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void SynData_UploadImgContents(string path, string urlService)
        {
            if (urlService.Length > 6)
            {
                PutDataBusinessLogic.UltilFunc _untilDAL = new PutDataBusinessLogic.UltilFunc(urlService);
                string fileName = string.Empty;
                string fullPath = string.Empty;
                string Upload = Global.UploadPathBDT;
                fullPath = HttpContext.Current.Server.MapPath(path);// +path.Replace("/", "\\");
                fileName = Path.GetFileName(fullPath);
                string path_Directory = path.Replace("/" + fileName, "");
                try
                {
                    FileInfo fInfo = new FileInfo(fullPath);
                    long numBytes = fInfo.Length;
                    double dLen = Convert.ToDouble(fInfo.Length / 1000000);
                    if (dLen < 4)
                    {
                        // set up a file stream and binary reader for the
                        // selected file
                        FileStream fStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fStream);
                        // convert the file to a byte array
                        byte[] data = br.ReadBytes((int)numBytes);
                        br.Close();
                        //_untilDAL.UploadFile(data, fileName, path_Directory);
                        _untilDAL.UploadFile(data, fileName, path_Directory.ToLower().Replace(Upload, ""));

                        fStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void SynData_UploadImgOne(string path, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {

                    SynData_UploadImg(path, _arr[i].ToString());

                }
            }
        }
        public void SynData_UploadImgOneContents(string path, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {

                    SynData_UploadImgContents(path, _arr[i].ToString());

                }
            }
        }
        #endregion End

        #region SEARCH FILE

        public bool SearchImgTag(string str)
        {
            bool _return = false;
            try
            {
                if (Global.ImagesService.Count > 0)
                {
                    Regex regex = new Regex(
                        @"(?<=<img[^<]+?src=\"")[^\""]+  ",
                        RegexOptions.IgnoreCase
                        | RegexOptions.Multiline
                        | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.Compiled
                        );
                    MatchCollection matchCollect = regex.Matches(str);
                    for (int i = 0; i < matchCollect.Count; i++)
                    {
                        //"/Upload/"
                        string _url = matchCollect[i].Value.Trim();

                        if (_url.IndexOf("http://") <= 0 && _url.Length > 0)
                        {
                            _url = _url.Replace("http://localhost/", "").Trim();
                            string Upload = Global.UploadPathBDT;

                            string fullPath = HttpContext.Current.Server.MapPath(_url);
                            if (File.Exists(fullPath))
                            {
                                SynData_UploadImgOneContents(_url, Global.ImagesService);
                            }
                        }
                    }
                    _return = true;
                }
            }
            catch
            {
                _return = false;
            }
            return _return;
        }
        public bool SearchTagSwf(string str)
        {
            bool _return = false;
            try
            {
                if (Global.ImagesService.Count > 0)
                {
                    Regex regex = new Regex(
                        @"(?<=<embed[^<]+?src=\"")[^\""]+  ",
                        RegexOptions.IgnoreCase
                        | RegexOptions.Multiline
                        | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.Compiled
                        );
                    MatchCollection matchCollect = regex.Matches(str);
                    for (int i = 0; i < matchCollect.Count; i++)
                    {
                        //"/Upload/"
                        string _url = matchCollect[i].Value.Trim();

                        if (_url.IndexOf("http://") <= 0 && _url.Length > 0)
                        {
                            _url = _url.Replace("http://localhost/", "").Trim();
                            string Upload = HPCComponents.Global.UploadPathBDT;
                            //string fullPath = HttpContext.Current.Server.MapPath(Upload) + _url.Replace("/", "\\");
                            string fullPath = HttpContext.Current.Server.MapPath(_url);// +_url.Replace("/", "\\");
                            if (File.Exists(fullPath))
                            {
                                SynData_UploadImgOneContents(_url, HPCComponents.Global.ImagesService);
                            }
                        }
                    }

                    _return = true;
                }
            }
            catch
            {
                _return = false;
            }
            return _return;
        }
        public bool SearchTagFLV(string str)
        {
            bool _return = false;
            try
            {
                if (Global.ImagesService.Count > 0)
                {
                    Regex regex = new Regex(
                        @"(?<=<embed[^<]+?flashvars=\"")[^\""]+  ",
                        RegexOptions.IgnoreCase
                        | RegexOptions.Multiline
                        | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.Compiled
                        );
                    MatchCollection matchCollect = regex.Matches(str);
                    for (int i = 0; i < matchCollect.Count; i++)
                    {
                        //"/Upload/"
                        string _url = matchCollect[i].Value.Trim().Replace("file=", "");

                        if (_url.IndexOf("http://") <= 0 && _url.Length > 0)
                        {
                            _url = _url.Replace("http://localhost/", "").Trim();
                            string Upload = HPCComponents.Global.UploadPathBDT;
                            string fullPath = HttpContext.Current.Server.MapPath(_url);// +_url.Replace("/", "\\");
                            if (File.Exists(fullPath))
                            {
                                SynData_UploadImgOneContents(_url, HPCComponents.Global.ImagesService);
                            }
                        }
                    }
                    _return = true;
                }
            }
            catch
            {
                _return = false;
            }
            return _return;
        }

        #endregion End
    }

    public class FilesDoc
    {
        public void LoadFileDoc(string _username, int _ID)
        {
            string strHTML = string.Empty;
            HPCBusinessLogic.DAL.T_NewsDAL dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News obj = dal.load_T_news(_ID);
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b>" + obj.News_Tittle + "<o:p></o:p></b></p>";
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b><br>" + obj.News_Summary + "<u1:p></u1:p></b></p>";
            strHTML += "<p style='text-align:justify'>" + obj.News_Body + "<o:p></o:p></p>";
            if (strHTML.Length > 0)
                SaveAsText(_username, strHTML);
        }
        private void SaveAsText(string _username, string _arr_IN)
        {
            string strFileName;
            string strHTML = string.Empty;
            strHTML += "<html><BODY>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            DirectoryInfo r = new DirectoryInfo(HttpContext.Current.Server.MapPath(HPCComponents.Global.GetAppPath(HttpContext.Current.Request)));
            FileInfo[] file;
            file = r.GetFiles("*.doc");
            foreach (FileInfo i in file)
            {
                File.Delete(r.FullName + "\\" + i.Name);
            }
            strFileName = _username + "_" + string.Format("{0:dd-MM-yyyy_HH-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            HttpContext.Current.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }
    }
}
