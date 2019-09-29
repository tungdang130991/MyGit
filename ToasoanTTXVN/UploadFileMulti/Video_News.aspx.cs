using System;
using System.IO;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using HPCServerDataAccess;
using JockerSoft.Media;

namespace ToasoanTTXVN.UploadFileMulti
{
    public partial class Video_News : System.Web.UI.Page
    {
        protected string strNumberArg = "1";
        protected string vType = "1";
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["vType"] != null)
                vType = Request.QueryString["vType"].ToString();
            if (Request.QueryString["vKey"] != null)
                strNumberArg = Request.QueryString["vKey"].ToString();
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user == null)
            {
                Response.Redirect(Global.ApplicationPath + "/Login.aspx", true);
            }
            if (!this.IsPostBack)
            {
                if (_user != null)
                {
                    txt_FromDate.Text = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
                    txt_ToDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    ListImages();
                }
            }
            else
            {
                string EventName = Request.Form["__EVENTTARGET"].ToString();
                if (EventName == "UploadSuccess")
                {
                    ListImages();
                }
            }
        }
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
        }
        protected string getFileName(object objFileType, object objFileName)
        {
            string strFilename = "";
            if (objFileName.ToString() != "")
            {
                if (objFileType.ToString() == "1")
                    strFilename = objFileName.ToString().Substring(4);
                else
                    strFilename = objFileName.ToString();
            }
            return strFilename;
        }
        private void ListImages()
        {
            ImageFilesDAL obj = new ImageFilesDAL();
            DataSet objDataset = new DataSet();
            //string where = " UserCreated =" + _user.UserID;
            string where = "";
            if (ddlStock.SelectedValue.Trim() != "0")
                where += " 1=1 And AuthorID =1 ";
            else
                where += " 1=1 And (( AuthorID =0 ) OR (AuthorID is null ))";
            if (!String.IsNullOrEmpty(txt_FromDate.Text.Trim()))
                where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)>=0) ", UltilFunc.ToDate(this.txt_FromDate.Value.ToString().Trim(), "MM/dd/yyyy"));
            if (!String.IsNullOrEmpty(txt_ToDate.Text.Trim()))
                where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)<=0) ", UltilFunc.ToDate(this.txt_ToDate.Value.ToString().Trim(), "MM/dd/yyyy"));
            else
            {
                if (ddlStock.SelectedValue.Trim() == "0")
                    where += " AND " + string.Format(" DATEDIFF(DAY,DateCreated,'{0}')=0 ", DateTime.Now.ToString("MM/dd/yyyy"));
            }
            where += " ORDER BY DateCreated DESC";
            objDataset = obj.ListAllImages(where);
            if (objDataset != null)
            {
                DataView _dv = obj.BindGridListImages(objDataset.Tables[0]);
                dlImages.DataSource = _dv;
                dlImages.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListImages();
        }

        private void SaveFile(string vType)
        {
            //string _urlSave = "";
            //string FolderCat = "";
            //string savepath = "";
            //string tempPath = "";
            //string strRootPathVirtual = "";

            //if (vType == "1")
            //    FolderCat = "/Article/";
            //else if (vType == "2")
            //    FolderCat = "/Ads/";
            //else if (vType == "3")
            //    FolderCat = "/Video/";
            //else if (vType == "4")
            //    FolderCat = "/Photo24/";
            //else
            //    FolderCat = "";
            //tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + FolderCat + _user.UserName.ToString() + "/";
            //strRootPathVirtual = tempPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
            //savepath = Server.MapPath(strRootPathVirtual);
            //string filename = DateTime.Now.Millisecond.ToString() + "_" + txtFile.PostedFile.FileName;
            //string _extenfile = Path.GetExtension(filename.ToString().Trim()).Replace(".", "");
            //if (!Directory.Exists(savepath))
            //    Directory.CreateDirectory(savepath);
            ////phan insert co so du lieu
            //T_ImageFiles _obj = new T_ImageFiles();
            //ImageFilesDAL _DAL = new ImageFilesDAL();
            //Int16 intFileType = getFileType(_extenfile);
            //string _imagesEnd = DateTime.Now.ToString("yyyyMMdd").ToString() + DateTime.Now.ToString("HHmmss") + DateTime.Now.Millisecond + UltilFunc.ReplaceCharsRewrite(filename.Replace(Path.GetExtension(txtFile.PostedFile.FileName), "")) + "." + _extenfile;
            //_urlSave = UrlPathImage_RemoveUpload(strRootPathVirtual + _imagesEnd);
            //if (Convert.ToInt32(vType) == 2)
            //{
            //    txtFile.PostedFile.SaveAs(savepath + @"\" + _imagesEnd);

            //    _obj = SetItem(_imagesEnd, txtFile.PostedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(_user.UserID), Convert.ToInt16(vType), intFileType);
            //}
            //else
            //{
            //    if (intFileType == 1)
            //    {
            //        txtFile.PostedFile.SaveAs(savepath + @"\" + filename);
            //        string _logo = Server.MapPath("../Dungchung/Images/IconHPC/LoGoLecourrier.png");
            //        HPCImageResize.SaveImage2Server(savepath, filename, _imagesEnd, Convert.ToInt32(HPCComponents.Global.VNPResizeImages), Convert.ToInt32(HPCComponents.Global.VNPResizeImages));
            //        _obj = SetItem(_imagesEnd, txtFile.PostedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(_user.UserID), Convert.ToInt16(vType), intFileType);
            //    }
            //    else
            //    {
            //        txtFile.PostedFile.SaveAs(savepath + @"\" + _imagesEnd);
            //        _obj = SetItem(_imagesEnd, txtFile.PostedFile.ContentLength, _urlSave, _extenfile, Convert.ToInt16(_user.UserID), Convert.ToInt16(vType), intFileType);
            //    }
            //}
            //_DAL.InsertT_ImageFiles(_obj);
            //try
            //{
            //    if (_extenfile.ToLower().Contains(".flv") || _extenfile.ToLower().Contains(".mp4"))
            //    {
            //        //crop video
            //        // Path of the video and frame storing path
            //        string _videopath = savepath + _imagesEnd;
            //        string _imagepath = savepath + _imagesEnd + "Frame.jpg";
            //        Bitmap bmp = FrameGrabber.GetFrameFromVideo(_videopath, 0.2d);
            //        bmp.Save(_imagepath, System.Drawing.Imaging.ImageFormat.Gif);
            //        // Save directly frame on specified location
            //        FrameGrabber.SaveFrameFromVideo(_videopath, 0.5d, _imagepath);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (_user.UserName != null)
            {

                SaveFile(vType);
                ListImages();

            }
        }
        protected Int16 getFileType(string FileExtension)
        {
            Int16 intFileType = 0;
            if (FileExtension.Trim() != null)
            {
                if (FileExtension.ToLower() == "png" || FileExtension.ToLower() == "jpeg" || FileExtension.ToLower() == "jpg" || FileExtension.ToLower() == "bmp" || FileExtension.ToLower() == "gif")
                    intFileType = 1;
                else if (FileExtension.ToLower() == "wmv" || FileExtension.ToLower() == "wma" || FileExtension.ToLower() == "swf" || FileExtension.ToLower() == "flv" || FileExtension.ToLower() == "mpeg")
                    intFileType = 2;
                else
                    intFileType = 3;
            }

            return intFileType;
        }
        public string Cut_Filename(object filename)
        {
            string _Name = "";
            try
            {
                _Name = filename.ToString();
                if (_Name.Length > 13)
                {
                    _Name = _Name.Substring(0, 11) + "...";
                }
            }
            catch { ;}
            return _Name;
        }
        protected void dlImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            // xóa ảnh
            Label lblid = (Label)e.Item.FindControl("lbl_ID");
            Label lblURL = (Label)e.Item.FindControl("lbl_URL");
            int ImageID = int.Parse(lblid.Text);
            ImageFilesDAL obj = new ImageFilesDAL();
            try
            {
                string strRootPathVirtual = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + lblURL.Text;
                string savepath = Server.MapPath(strRootPathVirtual);
                if (File.Exists(savepath))
                    File.Delete(savepath);
            }
            catch { ;}
            obj.Delete_Image(ImageID);
            ListImages();
        }


        public string GetFileURL(object Url)
        {
            string _url = "";
            try { _url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_url))
            {
                _url = Global.TinPathBDT + _url;
                string ex = Path.GetExtension(_url);
                if (!string.IsNullOrEmpty(ex))
                {
                    int type = GetFileType(ex);
                    if (type == 2)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/Video-icon.png";
                    else if (type == 3)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/AnotherFile.bmp";
                    else if (type == 4)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/acces_file.png";
                }
            }
            else
            {
                _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/NoImage.png";
            }
            return _url;
        }

        public int GetFileType(string FileExtend)
        {
            if (FileExtend.Trim().ToLower() == ".jpg" || FileExtend.Trim().ToLower() == ".png"
                || FileExtend.Trim().ToLower() == ".gif" || FileExtend.Trim().ToLower() == ".tif"
                || FileExtend.Trim().ToLower() == ".bmp" || FileExtend.Trim().ToLower() == ".jpeg"
                )
                return 1;
            else if (FileExtend.Trim().ToLower() == ".avi" || FileExtend.Trim().ToLower() == ".flv"
                || FileExtend.Trim().ToLower() == ".mp4" || FileExtend.Trim().ToLower() == ".wmv"
                || FileExtend.Trim().ToLower() == ".mpg" || FileExtend.Trim().ToLower() == ".3gp"
                || FileExtend.Trim().ToLower() == ".wma" || FileExtend.Trim().ToLower() == ".mpeg"
                )
                return 2;
            else if (FileExtend.Trim().ToLower() == ".pdf" || FileExtend.Trim().ToLower() == ".doc"
                || FileExtend.Trim().ToLower() == ".docx" || FileExtend.Trim().ToLower() == ".xls"
                )
                return 4;
            else
                return 3;
        }
        public string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString() + "," + GetvType() + "," + HPCSecurity.CurrentUser.Identity.ID.ToString() + "," + GetChuyenmuc();
            return strTemp;
        }
        public string GetvType()
        {
            string strTemp = HttpContext.Current.Request.QueryString["vType"].ToString();
            return strTemp;
        }
        public string GetvKey()
        {
            string strTemp = HttpContext.Current.Request.QueryString["vKey"].ToString();
            return strTemp;
        }
        public string GetChuyenmuc()
        {
            string strTemp = "0";
            return strTemp;
        }
        protected void ddlStock_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStock.SelectedIndex > 0)
            {
                txt_FromDate.Text = "";
                txt_ToDate.Text = "";
                ListImages();
            }
            else
            {
                txt_FromDate.Text = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
                txt_ToDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                ListImages();
            }
        }
    }
}
