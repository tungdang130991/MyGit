﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using System.IO;
using System.Configuration;
using HPCServerDataAccess;
using SSOLib.ServiceAgent;
using System.Drawing;

namespace HPCApplication.Until
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class UploadImg : IHttpHandler
    {
        T_Users _user = new T_Users();
        //UserDAL _userDAL = new UserDAL();
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files["Filedata"];

                int inStock = 0;
                try
                {
                    inStock = int.Parse(context.Request.QueryString["inStock"].ToString().Trim());
                }
                catch { ;}

                string[] sArrProdID = null;
                char[] sep = { '?' };
                string[] sArrVkey = null;
                string strUserID = string.Empty;
                char[] sep2 = { ',' };
                sArrProdID = context.Request.QueryString["user"].ToString().Trim().Split(sep);
                sArrVkey = sArrProdID[0].ToString().Trim().Split(sep2);
                string chuyenmuc = sArrVkey[sArrVkey.Length - 1];
                string _urlSave = string.Empty;
                string FolderCat = string.Empty;
                string savepath = string.Empty;
                string tempPath = string.Empty;
                string strRootPathVirtual = string.Empty;
                _user = _userDAL.GetUserByUserName(sArrVkey[0].ToString());
                strUserID = _user.UserID.ToString();
                string vType = sArrVkey[1].ToString();
                if (vType == "1")
                {
                    if (inStock != 0)
                        FolderCat = "/InStock/";
                    else
                        FolderCat = "/Article/";                    
                }
                else if (vType == "2")
                    FolderCat = "/Ads/";
                else if (vType == "3")
                    FolderCat = "/Video/";
                else if (vType == "4")
                    FolderCat = "/Photo24/";
                else
                    FolderCat = string.Empty;
                tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + FolderCat + sArrVkey[0].ToString() + "/";
                strRootPathVirtual = tempPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                savepath = context.Server.MapPath(strRootPathVirtual);

                string filename = getFileNameUnique(savepath + @"\", postedFile.FileName, Path.GetFileNameWithoutExtension(postedFile.FileName), Path.GetExtension(postedFile.FileName.ToString()));
                string _extenfile = Path.GetExtension(filename.ToString());
                string strFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName).Trim();
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);
                if (_extenfile.ToLower().Contains(".jpg")
                   || _extenfile.ToLower().Contains(".gif")
                   || _extenfile.ToLower().Contains(".png")
                   || _extenfile.ToLower().Contains(".bmp")
                   || _extenfile.ToLower().Contains(".jpeg"))
                    postedFile.SaveAs(savepath + @"\" + filename);               


                double CATID = 0;
                try { CATID = double.Parse(chuyenmuc); }
                catch { ;}
                string _logo = context.Server.MapPath("../DungChung/Images/IconHPC/LoGoBaoNongNghiep.png");

                string _imagesEndWatermark = getFileNameUnique(savepath + @"\", postedFile.FileName, Path.GetFileNameWithoutExtension(filename), _extenfile);
                if (_extenfile.ToLower() != ".flv" && _extenfile.ToLower() != ".swf" && _extenfile.ToLower() != ".mp3" && _extenfile.ToLower() != ".mp4" && _extenfile.ToLower() != ".wmv" && _extenfile.ToLower() != ".doc" && _extenfile.ToLower() != ".docx" && _extenfile.ToLower() != ".xls" && _extenfile.ToLower() != ".rar" && _extenfile.ToLower() != ".txt" && _extenfile.ToLower() != ".pdf")
                {
                    if (Convert.ToBoolean(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WatermarkImages"])))
                    {
                        //Begin BO CT EDIT Đóng dấu ảnh
                        _imagesEndWatermark = "W_" + filename;
                        HPCImageResize.SaveImage2Server(savepath, filename, "rez_" + filename, _imagesEndWatermark, _logo, Convert.ToInt32(HPCComponents.Global.VNPResizeImages), Convert.ToInt32(HPCComponents.Global.VNPResizeImages));
                    }
                    else if (Convert.ToBoolean(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AutoProcessReszie"])))// Không đóng dấu ảnh //END
                    {
                        HPCImageResize.SaveImage2Server(savepath, filename, _imagesEndWatermark, Convert.ToInt32(HPCComponents.Global.VNPResizeImages), Convert.ToInt32(HPCComponents.Global.VNPResizeImages));
                    }
                }                
                else
                {
                    _imagesEndWatermark = filename;
                    postedFile.SaveAs(Path.Combine(savepath, filename));
                }

                //_urlSave = UrlPathImage_RemoveUpload(strRootPathVirtual + filename);
                _urlSave = UrlPathImage_RemoveUpload(strRootPathVirtual + _imagesEndWatermark);

                //phan insert co so du lieu
                T_ImageFiles _obj = new T_ImageFiles();
                ImageFilesDAL _DAL = new ImageFilesDAL();
                _obj = SetItem(_imagesEndWatermark, postedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(strUserID), Convert.ToInt16(vType), CATID);                
                
                int _idReturn = _DAL.InsertT_ImageFiles(_obj);
                if (inStock != 0)
                    _DAL.UpdateStatusDataByID(" AuthorID =1 Where ID =" + _idReturn);
                context.Response.Write(savepath + "/" + filename);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }

        }

        protected string getFileNameUnique(string ImageDirectory, string strFileName, string fileNameWithoutExtension,string FileExtension)
        {
            int count = 1;
            string strFileNameUnique = UltilFunc.ReplaceCharsRewrite(fileNameWithoutExtension) + FileExtension;
            string ImagePath = ImageDirectory + strFileNameUnique;
            while (System.IO.File.Exists(ImagePath))
            {

                ImagePath = string.Concat(ImageDirectory, UltilFunc.ReplaceCharsRewrite(fileNameWithoutExtension), "-", count.ToString(), FileExtension);
                strFileNameUnique = UltilFunc.ReplaceCharsRewrite(fileNameWithoutExtension) + "-" + count.ToString() + FileExtension;
                count++;

            }

            return strFileNameUnique;
        }



        protected T_ImageFiles SetItem(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType, double chuyenmuc)
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