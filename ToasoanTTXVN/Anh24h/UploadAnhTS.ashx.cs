using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.BaoDienTu
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadAnhTS : IHttpHandler
    {

        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
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
                string strUserID = "";
                char[] sep2 = { ',' };
                sArrProdID = context.Request.QueryString["user"].ToString().Trim().Split(sep);
                sArrVkey = sArrProdID[0].ToString().Trim().Split(sep2);
                string _urlSave = "";
                string FolderCat = "";
                string savepath = "";
                string tempPath = "";
                string strRootPathVirtual = "";
                _user = _userDAL.GetUserByUserName(sArrVkey[0].ToString());
                strUserID = _user.UserID.ToString();
                string vType = sArrVkey[1].ToString();
                string AlbumID = sArrVkey[2].ToString();
                if (vType == "1")
                    FolderCat = "/Article/";
                else if (vType == "2")
                    FolderCat = "/Ads/";
                else if (vType == "3")
                    FolderCat = "/Video/";
                else if (vType == "4")
                    FolderCat = "/Photo24/";
                else
                    FolderCat = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + FolderCat + sArrVkey[0].ToString() + "/";
                strRootPathVirtual = tempPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                savepath = context.Server.MapPath(strRootPathVirtual);
                string filename = DateTime.Now.Millisecond.ToString() + "_" + postedFile.FileName;
                string _extenfile = Path.GetExtension(filename.ToString().Trim()).Replace(".", "");
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);
                postedFile.SaveAs(savepath + @"\" + filename);
                string _logo = context.Server.MapPath("../Dungchung/Images/IconHPC/LoGoDongDau.png");
                string _imagesEndWatermark = DateTime.Now.ToString("yyyyMMdd").ToString() + DateTime.Now.ToString("HHmmss") + DateTime.Now.Millisecond + UltilFunc.ReplaceCharsRewrite(filename.Replace(Path.GetExtension(filename.ToString().Trim()), "")) + "." + _extenfile;
                _urlSave = UrlPathImage_RemoveUpload(strRootPathVirtual + _imagesEndWatermark);
                // Begin BO CT EDIT Đóng dấu ảnh
                if (Convert.ToBoolean(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WatermarkImages"])))
                    HPCImageResize.SaveImage2Server(savepath, filename, "rez_" + filename, _imagesEndWatermark, _logo, Convert.ToInt32(HPCComponents.Global.VNPResizeImages), Convert.ToInt32(HPCComponents.Global.VNPResizeImages));
                else// Không đóng dấu ảnh //END
                    HPCImageResize.SaveImage2Server(savepath, filename, _imagesEndWatermark, Convert.ToInt32(HPCComponents.Global.VNPResizeImages), Convert.ToInt32(HPCComponents.Global.VNPResizeImages));
                // INSERT DATABASE
                //phan insert co so du lieu
                T_ImageFiles _objImage = new T_ImageFiles();
                ImageFilesDAL _DAL = new ImageFilesDAL();
                _objImage = SetItemImageFiles(filename, postedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(strUserID), Convert.ToInt16(vType), 0);
                int _idReturn = _DAL.InsertT_ImageFiles(_objImage);
                //phan insert Anh phong su
                T_Photo_EventDAL _cateDAL = new T_Photo_EventDAL();
                T_Photo_Event _obj = new T_Photo_Event();
                _obj = setItem(_urlSave, postedFile.FileName);
                int _return = _cateDAL.InsertT_Photo_Events(_obj);
                context.Response.Write(savepath + "/" + filename);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        private T_Photo_Event setItem(string urlImage, string photoName)
        {
            photoName = photoName.Substring(0, (photoName.IndexOf(".")-1));
            T_Photo_Event _objPoto = new T_Photo_Event();
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            _objPoto.Photo_ID = 0;
            //_objPoto.Photo_Name = "Nhập tiêu đề ảnh";
            _objPoto.Photo_Medium = urlImage;
            _objPoto.Date_Create = DateTime.Now;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Status = 5;
            _objPoto.Lang_ID = 1;
            _objPoto.Copy_From = 0;
            return _objPoto;
        }

        protected T_ImageFiles SetItemImageFiles(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType, double chuyenmuc)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            _obj.ImageFileName = _tenFile.ToString();
            _obj.ImageFileSize = _size;
            _obj.ImageFileExtension = _extenfile.ToString();
            _obj.ImageType = vType;
            _obj.ImgeFilePath = _pathfile.ToString();
            _obj.Status = 0;
            _obj.UserCreated = _userID;
            _obj.DateCreated = DateTime.Now;
            _obj.Categorys_ID = chuyenmuc;

            return _obj;
        }

        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
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
