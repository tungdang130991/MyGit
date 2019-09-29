using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;

namespace ToasoanTTXVN.PhongSuAnh
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadImgMulti : IHttpHandler
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
                string strUserID ="";
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
                else if (vType=="4")
                    FolderCat = "/Photo24/";
                else
                    FolderCat = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + FolderCat + sArrVkey[0].ToString() + "/";
                strRootPathVirtual = tempPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                savepath = context.Server.MapPath(strRootPathVirtual);
                string filename = DateTime.Now.Millisecond.ToString() +"_"+ postedFile.FileName;
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
                HPCInfo.T_Album_Photo _obj = new HPCInfo.T_Album_Photo();
                HPCBusinessLogic.DAL.T_Album_PhotoDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
                _obj = setItem_Album_Photo(Convert.ToInt32(AlbumID.ToString()), filename, filename, postedFile.ContentLength.ToString(), "", "", _extenfile, 1, 0, _urlSave);
                _cateDAL.InsertT_Album_Photo(_obj);
                context.Response.Write(savepath + "/" + filename);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        protected T_ImageFiles SetItem(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            //_obj.ImageFileName = "";
            _obj.ImageFileSize = _size;
            _obj.ImageFileExtension = _extenfile.ToString();
            _obj.ImageType = vType;
            _obj.ImgeFilePath = _pathfile.ToString();
            _obj.Status = 0;
            _obj.UserCreated = _userID;
            _obj.DateCreated = DateTime.Now;
            return _obj;
        }

        private HPCInfo.T_Album_Photo setItem_Album_Photo(int AlbumID, string Abl_Photo_Name, string Abl_Photo_Desc, string File_Size, string FileSquare,
            string Authod_Name, string File_Type, int Lang_ID, int OrderByPhoto, string Abl_Photo_Origin)
        {
            HPCInfo.T_Album_Photo _objPoto = new HPCInfo.T_Album_Photo();
            _objPoto.Alb_Photo_ID = 0;
            //_objPoto.Abl_Photo_Name = Abl_Photo_Name;
            //_objPoto.Abl_Photo_Desc = Abl_Photo_Desc;
            _objPoto.Abl_Photo_Origin = Abl_Photo_Origin;
            _objPoto.File_Size = File_Size;
            _objPoto.File_Type = File_Type;
            _objPoto.Authod_Name = "";
            _objPoto.Cat_Album_ID = AlbumID;
            _objPoto.Lang_ID = Lang_ID;
            _objPoto.Abl_Isweek_Photo = false;
            _objPoto.Date_Create = DateTime.Now;
            _objPoto.Date_Modify = DateTime.Now;
            _objPoto.OrderByPhoto = OrderByPhoto;
            _objPoto.Creator = _user.UserID;
            _objPoto.UserModify = _user.UserID;
            _objPoto.Abl_Photo_Status = 1;
            _objPoto.Copy_From = 0;
            return _objPoto;
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
