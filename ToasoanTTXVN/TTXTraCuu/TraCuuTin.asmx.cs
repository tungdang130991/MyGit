using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Globalization;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using HPCBusinessLogic;
using System.Configuration;
using System.Net;
using System.IO;

namespace ToasoanTTXVN.TTXTraCuu
{
    /// <summary>
    /// Summary description for TraCuuTin1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TraCuuTin1 : System.Web.Services.WebService
    {

        WS_TTX.ListContent _listContent;
        WS_TTX.LoginResult _LoginResult;
        WS_TTX.WebService1SoapClient _ws;

        [WebMethod]
        public string Login(object IpClient)
        {
            string _strResult = "";
            try
            {
                _ws = new WS_TTX.WebService1SoapClient();
               
                _LoginResult = _ws.login("dat6447", "ncud2014", IpClient.ToString(), "", "");
                //_LoginResult.
                if (_LoginResult.authenticationResult == "Success" && _LoginResult.isAuthenticated == true)
                    _strResult = _LoginResult.authenticationTicket;
                else
                    _strResult = "unsuccess";
               
            }
            catch(Exception ex) { }
            return _strResult;
        }

        [WebMethod]
        public List<Type> GetListTypes(object Ticket)
        {
            _ws = new WS_TTX.WebService1SoapClient();
            var listData = new List<Type>();
            _listContent = _ws.getListType(Ticket.ToString());
            WS_TTX.TypeInfo[] Info = _listContent.listTypeInfo;
            if (Info != null)
            {
                for (int i = 0; i < Info.Length; i++)
                {
                    var type = new Type
                    {
                        TypeID = Info[i].TypeID,
                        TypeName = Info[i].TypeName
                    };
                    listData.Add(type);
                }
            }
            return listData;
        }

        [WebMethod]
        public List<Product> GetListProducts(object Ticket, object TypeID)
        {
            _ws = new WS_TTX.WebService1SoapClient();
            var listData = new List<Product>();
            _listContent = _ws.getListProduct(Ticket.ToString());
            WS_TTX.Product[] Info = _listContent.listProduct;
            if (Info != null)
            {
                for (int i = 0; i < Info.Length; i++)
                {
                    if (Info[i].TypeID == int.Parse(TypeID.ToString()))
                    {
                        var product = new Product
                        {
                            ProductID = Info[i].ProductID,
                            ProductName = Info[i].ProductName,
                            LanguageID = Info[i].LanguageID,
                            TypeID = Info[i].TypeID,
                            TypeName = Info[i].TypeName
                        };
                        listData.Add(product);
                    }
                }
            }
            return listData;
        }

        [WebMethod]
        public List<Language> GetListLanguages(object Ticket, object TypeID, object ProductID)
        {
            _ws = new WS_TTX.WebService1SoapClient();
            var listData = new List<Language>();
            _listContent = _ws.getListContent(Ticket.ToString());
            WS_TTX.Language[] Info = _listContent.listLanguage;
            if (Info != null)
            {
                for (int i = 0; i < Info.Length; i++)
                {
                    if (Info[i].TypeID == int.Parse(TypeID.ToString()) && Info[i].ProductID == int.Parse(ProductID.ToString()))
                    {
                        var lang = new Language
                        {
                            LanguageID = Info[i].LanguageID,
                            LanguageName = Info[i].LanguageName,
                            TypeID = Info[i].TypeID,
                            TypeName = Info[i].TypeName,
                            ProductID = Info[i].ProductID,
                            ProductName = Info[i].ProductName
                        };
                        listData.Add(lang);
                    }
                }
            }
            return listData;
        }

        [WebMethod]
        public List<Category> GetListCategorys(object Ticket, object TypeID, object ProductID, object LanguageID)
        {
            _ws = new WS_TTX.WebService1SoapClient();
            var listData = new List<Category>();
            _listContent = _ws.getListContent(Ticket.ToString());
            WS_TTX.Category[] Info = _listContent.listCategory;
            if (Info != null)
            {
                for (int i = 0; i < Info.Length; i++)
                {
                    if (Info[i].TypeID == int.Parse(TypeID.ToString()) && Info[i].ProductID == int.Parse(ProductID.ToString()) && Info[i].LanguageID == int.Parse(LanguageID.ToString()))
                    {
                        var cate = new Category
                        {
                            CategoryID = Info[i].CategoryID,
                            CategoryName = Info[i].CategoryName,
                            TypeID = Info[i].TypeID,
                            TypeName = Info[i].TypeName,
                            ProductID = Info[i].ProductID,
                            ProductName = Info[i].ProductName,
                            LanguageID = Info[i].LanguageID,
                            LanguageName = Info[i].LanguageName

                        };
                        listData.Add(cate);
                    }
                }
            }
            return listData;
        }
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public ListData BindGridData(object Ticket, object TypeID, object ProductID, object LanguageID, object CategoryID, object FromDate, object ToDate, object TuKhoa, object PageIndex, object PageSize)
        {
            try
            {
                string _TuKhoa = System.Uri.UnescapeDataString(TuKhoa.ToString());
                if (int.Parse(TypeID.ToString()) == 1)
                    _TuKhoa = SplitStringSearch(_TuKhoa);
                _ws = new WS_TTX.WebService1SoapClient();
                var data = new ListData();
                int total = 0;
                List<NewsData> objDataNews = new List<NewsData>();
                List<PhotosData> objDataPhotos = new List<PhotosData>();
                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                string fromdate = DateTime.ParseExact(FromDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string todate = DateTime.ParseExact(ToDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                dtfi.ShortDatePattern = @"M/d/yyyy";
                dtfi.DateSeparator = "/";
                DateTime _FromDate = Convert.ToDateTime(fromdate, dtfi);
                DateTime _ToDate = Convert.ToDateTime(todate, dtfi);
                int _TypeID = int.Parse(TypeID.ToString());
                int _LangID = int.Parse(LanguageID.ToString());
                int _ProductID = int.Parse(ProductID.ToString());
                int _CateID = int.Parse(CategoryID.ToString());
                int _PageIndex = int.Parse(PageIndex.ToString());
                int _PageSize = int.Parse(PageSize.ToString());
                if (int.Parse(TypeID.ToString()) == 1)
                {
                    WS_TTX.ListNews _listData = _ws.getListSearchNews(Ticket.ToString(), _TypeID, _LangID, _ProductID
                        , _CateID, _PageIndex, _PageSize, _FromDate, _ToDate, _TuKhoa);
                    WS_TTX.ObjectNews[] Info = _listData.listNews;
                    if (Info != null)
                    {
                        for (int i = 0; i < Info.Length; i++)
                        {
                            string _Title = "";
                            string _Content = "";
                            if (Info[i].Title != null)
                                _Title = System.Uri.EscapeDataString(Info[i].Title);
                            if (Info[i].Content != null)
                                _Content = System.Uri.EscapeDataString(Info[i].Content);
                            objDataNews.Add(new NewsData(Info[i].ID, Info[i].ProductID, _Title, _Content, Info[i].FileName,
                                Info[i].Author, Info[i].CategoryID, Info[i].CategoryName, Convert.ToDateTime(Info[i].DateCreate.ToString()).ToString("dd/MM/yyyy HH:mm:ss").ToString()));
                        }
                    }
                    total = _listData.total;
                    data.ListNews = objDataNews;
                }
                else
                {
                    WS_TTX.ListImage _listData = _ws.getListSearchImages(Ticket.ToString(), _TypeID, _LangID, _ProductID, _CateID
                        , _PageIndex, _PageSize, _FromDate, _ToDate, _TuKhoa);
                    WS_TTX.ObjectImage[] Info = _listData.listImage;
                    if (Info != null)
                    {
                        for (int i = 0; i < Info.Length; i++)
                        {
                            string _Title = "";
                            string _Summary = "";
                            if (Info[i].Title != null)
                                _Title = System.Uri.EscapeDataString(Info[i].Title);
                            if (Info[i].Summary != null)
                                _Summary = System.Uri.EscapeDataString(Info[i].Summary);
                            objDataPhotos.Add(new PhotosData(Info[i].ID, _Title, _Summary, Info[i].CategoryID, Info[i].CategoryName, Info[i].ImageThumbString, Info[i].Author
                                , Convert.ToDateTime(Info[i].DateCreate.ToString()).ToString("dd/MM/yyyy HH:mm:ss").ToString(), Info[i].FileName, Info[i].ProductID, Info[i].ProductName, Info[i].URLImg));
                        }
                    }
                    total = _listData.total;
                    data.ListPhotos = objDataPhotos;
                }
                data.TotalRecords = total;
                return data;
            }
            catch (Exception)
            {
                throw;
            }

        }
        [WebMethod]
        public News GetContentNewsByID(object Ticket, object ProductID, object ID)
        {
            News objNews = new News();
            _ws = new WS_TTX.WebService1SoapClient();
            WS_TTX.ObjectNews Info = _ws.getContentNewsByID(Ticket.ToString(), int.Parse(ProductID.ToString()), int.Parse(ID.ToString()));
            if (Info != null)
            {
                objNews.ID = Info.ID;
                objNews.Title = System.Uri.EscapeDataString(Info.Title);
                objNews.Summary = System.Uri.EscapeDataString(ConvertWordToHTML(Info.Summary));
                objNews.Content = System.Uri.EscapeDataString(ConvertWordToHTML(Info.Content));
                objNews.Author = Info.Author;
                objNews.ProductID = Info.ProductID;
                objNews.ProductName = Info.ProductName;
                objNews.CategoryID = Info.CategoryID;
                objNews.CategoryName = Info.CategoryName;
                objNews.DateCreate = Convert.ToDateTime(Info.DateCreate.ToString()).ToString("dd/MM/yyyy HH:mm:ss").ToString();
                objNews.FileName = Info.FileName;
            }
            return objNews;
        }
        [WebMethod]
        public Image GetContentImageByID(object Ticket, object ProductID, object ID)
        {
            Image objImage = new Image();
            _ws = new WS_TTX.WebService1SoapClient();
            WS_TTX.ObjectImage Info = _ws.getContentImageByID(Ticket.ToString(), int.Parse(ProductID.ToString()), int.Parse(ID.ToString()));
            if (Info != null)
            {
                objImage.ID = Info.ID;
                objImage.Title = System.Uri.EscapeDataString(Info.Title);
                objImage.Summary = System.Uri.EscapeDataString(ConvertWordToHTML(Info.Summary));
                objImage.dataBinary = Info.dataBinary;
                objImage.Author = Info.Author;
                objImage.ProductID = Info.ProductID;
                objImage.ProductName = Info.ProductName;
                objImage.CategoryID = Info.CategoryID;
                objImage.CategoryName = Info.CategoryName;
                objImage.DateCreate = Convert.ToDateTime(Info.DateCreate.ToString()).ToString("dd/MM/yyyy HH:mm:ss").ToString();
                objImage.URLImg = Info.URLImg;
            }
            return objImage;
        }
        protected string getFileNameUnique(string ImageDirectory, string strFileName, string fileNameWithoutExtension, string FileExtension)
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
        [WebMethod]
        public string Downloadimages(object Ticket, object ProductID, object ID, object MenuID, object UserID, object UserName)
        {
            int dung = 0;
            int sai = 0;
            string[] listID = null;
            if (ID != null)
                listID = ID.ToString().Split(',');
            string _return = string.Empty;
            try
            {
                if (listID.Length > 0)
                {
                    for (int i = 0; i < listID.Length; i++)
                    {
                        Image objPhoto = GetContentImageByID(Ticket, ProductID, listID[i]);
                        //string validate = Check_Url_Img(Ticket, ProductID, listID[i]);
                        //if (validate == "success")
                        //{
                        //string fileNew = objPhoto.URLImg.Replace(@"\", @"/");
                        ////Get Image From FTP To Server
                        //Save_Img_To_ServerNews(fileNew);
                        ////
                        //dung++;
                        //InsertLogImage(objPhoto.Title, UserID.ToString(), UserName.ToString(), objPhoto.CategoryName, MenuID.ToString());
                        //}
                        //else
                        //{
                        //    sai++;
                        //}
                        //string objPhoto.dataBinary BOCT ADD

                        string fileName = objPhoto.URLImg;
                        byte[] bytes = Convert.FromBase64String(objPhoto.dataBinary);
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            using (Bitmap bm2 = new Bitmap(ms))
                            {
                                string _path = Server.MapPath("/" + HPCComponents.Global.UploadPath + "") + @"\TempFile\" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString();
                                if (Directory.Exists(_path) == false)
                                    Directory.CreateDirectory(_path);
                                //_path = _path + @"\" + Path.GetFileName(fileName);
                                string fileNames = getFileNameUnique(_path + @"\", Path.GetFileName(fileName), Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                                _path = _path + @"\" + Path.GetFileName(fileNames);
                                this.saveJpeg(_path, bm2, 100L);
                                Save_Img_To_ServerNews(_path);
                                dung++;
                                InsertLogImage(System.Uri.UnescapeDataString(objPhoto.Title), UserID.ToString(), UserName.ToString(), System.Uri.UnescapeDataString(objPhoto.CategoryName), MenuID.ToString());
                            }
                        }

                    }
                }
                if (sai == 0)
                {
                    _return = "Bạn đã lấy thành công " + dung + " ảnh về hệ thống";
                }
                else
                {
                    _return = "Bạn đã lấy thành công " + dung + " ảnh về hệ thống và " + sai + " ảnh không tìm thấy";
                }
                if (dung == 0)
                {
                    _return = "Không tìm thấy " + sai + " ảnh muốn lấy về hệ thống";
                }
            }
            catch (Exception ex)
            {
                _return = "Có lỗi xảy ra trong quá trình lấy ảnh:" + ex.Message.ToString() + "\r\n" + ex.Source.ToString();
            }
            return _return;
        }

        protected void Save_Img_To_ServerNews(string _path)
        {
            try
            {
                //Save as File Temp
                //string filename = string.Empty;
                string TempFile = _path;
                string uploadPath = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString();
                string strRootPath = ConfigurationManager.AppSettings["ServerPathDis"].ToString() + uploadPath;
                if (Directory.Exists(strRootPath) == false)
                    Directory.CreateDirectory(strRootPath);
                string _filenameResize = getFileNameUnique(strRootPath + @"\", Path.GetFileName(_path), Path.GetFileNameWithoutExtension(_path), Path.GetExtension(_path));
                //string _tenResize = Path.GetFileNameWithoutExtension(TempFile) + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(TempFile);
                resizeimage(TempFile, strRootPath + @"\" + _filenameResize, Convert.ToInt32(HPCComponents.Global.VNPResizeImages));

            }
            catch
            {

            }

        }
        protected void Save_Img_To_Server(string url)
        {
            string username = ConfigurationManager.AppSettings["Username_FTP"].ToString();
            string password = ConfigurationManager.AppSettings["Password_FTP"].ToString();
            FtpWebRequest reqFTP;
            try
            {
                string url_img = ConfigurationManager.AppSettings["FTP_AnhTuLieu"].ToString() + url;
                string filename = Path.GetFileName(url_img);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url_img));
                reqFTP.Timeout = 60000;
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                //
                byte[] content;
                using (BinaryReader br = new BinaryReader(ftpStream))
                {
                    content = br.ReadBytes(10000000);
                    br.Close();
                }
                response.Close();
                //Save as File Temp
                string TempFile = Server.MapPath(@"../upload/TempFile/" + filename);
                //
                FileStream fs = new FileStream(TempFile, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(content);

                }
                finally
                {
                    fs.Close();
                    bw.Close();
                    ftpStream.Close();
                    response.Close();
                }
                string uploadPath = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString();
                string strRootPath = ConfigurationManager.AppSettings["ServerPathDis"].ToString() + uploadPath;
                if (Directory.Exists(strRootPath) == false)
                    Directory.CreateDirectory(strRootPath);
                string _tenResize = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + Path.GetExtension(TempFile);
                resizeimage(TempFile, strRootPath + @"\" + _tenResize, Convert.ToInt32(HPCComponents.Global.VNPResizeImages));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // Kiểm tra sự tồn tại của ảnh
        [WebMethod]
        public string Check_Url_Img(object Ticket, object ProductID, object ID)
        {
            string _return = "";
            string username = ConfigurationManager.AppSettings["Username_FTP"].ToString();
            string password = ConfigurationManager.AppSettings["Password_FTP"].ToString();
            Image objPhoto = GetContentImageByID(Ticket, ProductID, ID);

            string fileName = objPhoto.URLImg;
            //string objPhoto.dataBinary BOCT ADD
            byte[] bytes = Convert.FromBase64String(objPhoto.dataBinary);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (Bitmap bm2 = new Bitmap(ms))
                {
                    string _path = Server.MapPath("/" + HPCComponents.Global.UploadPath + "") + @"\TempFile\" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString();
                    if (Directory.Exists(_path) == false)
                        Directory.CreateDirectory(_path);
                    _path = _path + @"\" + Path.GetFileName(fileName);
                    //this.saveJpeg(_path, bm2, 100L);
                    _return = "success";
                }
            }
            return _return;
        }
        #region Ghi log
        [WebMethod]
        public string InsertLogImage(string tieude, string user_id, string user_name, string category, string menuid)
        {
            //string[] listTieuDe = null;
            //if (tieude != null)
            //    listTieuDe = tieude.ToString().Split(',');
            string _return = string.Empty;
            int _menuID = int.Parse(menuid);
            try
            {
                //if (listTieuDe.Length > 0)
                //{
                //    for (int i = 0; i < listTieuDe.Length; i++)
                //    {
                //Photo objPhoto = GetUrlImgByID(Convert.ToInt32(listTieuDe[i]));
                //string _tieude = objPhoto.Tieu_De;
                int _userID = int.Parse(user_id);
                string _node = "Tra cứu ảnh : Lấy ảnh về hệ thống";
                UltilFunc.WriteLogHistorySource(_userID, user_name, tieude, category, _menuID, _node, 3);
                //WriteLogHistory2Database.WriteHistory2Database(_userID, user_name, tieude, _menuID, _node, 0, 3);
                //    }
                //}
                _return = "Ghi Log thành công";
            }
            catch (Exception)
            {
                _return = "Có lỗi xảy ra trong quá trình ghi log";
            }
            return _return;
        }
        [WebMethod]
        public string InsertLogTin(object Ticket, object ProductID, object ID, object MenuID, object UserID, object UserName)
        {
            int _userID = int.Parse(UserID.ToString());
            News objNews = GetContentNewsByID(Ticket, ProductID, ID);
            string category = objNews.CategoryName;
            string _node = "";
            if (objNews.ProductID == 1)
            {
                _node = "Tra cứu tin nguồn";
            }
            else
            {
                _node = "Tra cứu tin tư liệu";
            }
            UltilFunc.WriteLogHistorySource(_userID, UserName.ToString(), System.Uri.UnescapeDataString(objNews.Title.ToString()), objNews.CategoryName.ToString(), 0, _node, objNews.ProductID);
            //WriteLogHistory2Database.WriteHistory2Database(_userID, UserName.ToString(), System.Uri.UnescapeDataString(objNews.Title.ToString()), MenuID, _node, 0, 3);
            return "success";
        }

        #endregion
        #region Resize Image
        private void resizeimage(string fileImage, string _strpath)
        {
            int _path = fileImage.IndexOf(":");
            if (_path < 1)
                fileImage = HttpContext.Current.Server.MapPath("../" + fileImage);
            ViewResize(fileImage, Convert.ToInt32(HPCComponents.Global.VNPResizeImagesPhoto), _strpath);
        }
        private void resizeimage(string fileImage, string _strpath, int _width)
        {
            int _path = fileImage.IndexOf(":");
            if (_path < 1)
                fileImage = HttpContext.Current.Server.MapPath("../" + fileImage);
            ViewResize(fileImage, _width, _strpath);
        }
        private void DeleteFile(string _pathFile)
        {
            try
            {
                if (File.Exists(_pathFile))
                    File.Delete(_pathFile);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void ViewResize(string ImagePath, int Width, string imgPath)
        {
            string path = ImagePath;
            //System.Drawing.Image objThumbnail;
            Bitmap _obj;
            System.Drawing.Image objImage;
            int imgwidth = 0;
            int imgheight = 0;
            decimal lnRatio;
            try
            {
                objImage = System.Drawing.Image.FromFile(path);
                if (objImage.Width > objImage.Height)
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;

                }
                // Create thumbnail
                _obj = new Bitmap(imgwidth, imgheight);
                Graphics grWatermark = Graphics.FromImage(_obj);
                grWatermark.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grWatermark.SmoothingMode = SmoothingMode.HighQuality;
                grWatermark.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grWatermark.CompositingQuality = CompositingQuality.HighQuality;

                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, imgwidth, imgheight);

                grWatermark.DrawImage(objImage, imageRectangle, 0, 0, objImage.Width, objImage.Height, GraphicsUnit.Pixel);
                this.saveJpeg(imgPath, _obj, 100L);
                grWatermark.Dispose();
                _obj.Dispose();
                objImage.Dispose();
                DeleteFile(ImagePath);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");
            if (jpegCodec == null)
                return;
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
            img.Dispose();

        }
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
        #endregion
        private string ConvertWordToHTML(string wordContent)
        {
            string strOutput = wordContent;
            strOutput = strOutput.Replace(((char)(160)).ToString(), "&nbsp");
            // Thay the ki tu xuong dong, ve dau dong
            strOutput = strOutput.Replace("\r\n", "<br>");
            //  Thay the ki tu xuong dong
            strOutput = strOutput.Replace("\r", "<br>");
            strOutput = strOutput.Replace("\n", "<br>");
            //  Thay the cac ki tu khac
            strOutput = strOutput.Replace("BLOCKQUOTE", "DIV");
            return strOutput.Trim();
        }
        public static string SplitStringSearch(string _root)
        {
            string _key = "";
            int _len = _root.Length;
            int checkand = 0, _continue = 0;
            for (int i = 0; i < _len; i++)
            {
                if (_root[i] == ' ' && _continue == 0)
                {
                    checkand = 0;
                    for (int j = i + 1; j < _len; j++)
                    {
                        if (_root[j] == ' ')
                        {
                        }
                        else if (_root[j] == '+')
                        {
                            checkand = 1;
                        }
                        else if (_root[j] != ' ' && _root[j] != '+')
                        {
                            if (checkand == 0)
                                _key = _key + " OR ";
                            else
                                _key = _key + " and ";
                            i = j - 1;
                            checkand = 0;
                            break;
                        }
                    }
                }
                else if (_root[i] == '+')
                {
                    for (int j = i + 1; j < _len; j++)
                    {
                        if (_root[j] == ' ' || _root[j] == '+')
                        {
                        }
                        else
                        {
                            _key = _key + " and ";
                            i = j - 1;
                            break;
                        }
                    }
                }
                else if (_root[i] == '"' && _continue == 0)
                {
                    //_continue = 1;
                    return _root;
                }

                else if (_root[i] == '"' && _continue == 1)
                {
                    _continue = 0;
                    if (i < _len - 1 && _root[i + 1] != ' ' && _root[i + 1] != '+')
                    {
                        _key = _key + " OR ";
                    }
                }
                else
                {
                    checkand = 0;
                    _key = _key + _root[i];
                }
            }
            _key = _key + "";
            return _key;

        }
    }
    public class Type
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int LanguageID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }
    public class Language
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
    public class Content
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    public class ListData
    {
        public List<NewsData> ListNews { get; set; }
        public List<PhotosData> ListPhotos { get; set; }
        public int TotalRecords { get; set; }
    }
    public class Image
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string dataBinary { get; set; }
        public string Author { get; set; }
        public string URLImg { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DateCreate { get; set; }
    }
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DateCreate { get; set; }
        public string FileName { get; set; }
    }
    public class NewsData
    {
        public NewsData()
        { }
        public NewsData(int ID, int ProductID, string Title, string Content, string FileName, string Author, int CategoryID, string CategoryName, string DateCreate)
        {
            _ID = ID;
            _ProductID = ProductID;
            _Title = Title;
            _Content = Content;
            _FileName = FileName;
            _Author = Author;
            _CategoryID = CategoryID;
            _CategoryName = CategoryName;
            _DateCreate = DateCreate;
        }
        private int _ID;
        private int _ProductID;
        private string _Title;
        private string _Content;
        private string _FileName;
        private string _DateCreate;
        private string _Author;
        private string _CategoryName;
        private int _CategoryID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public string DateCreate
        {
            get { return _DateCreate; }
            set { _DateCreate = value; }
        }
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
    }
    public class PhotosData
    {
        public PhotosData()
        { }
        public PhotosData(int ID, string Title, string Summary, int CategoryID, string CategoryName, string ImageThumbString, string Author, string DateCreate, string FileName
            , int ProductID, string ProductName, string URLImg)
        {
            _ID = ID;
            _Title = Title;
            _Summary = Summary;
            _CategoryID = CategoryID;
            _CategoryName = CategoryName;
            _ImageThumbString = ImageThumbString;
            _Author = Author;
            _DateCreate = DateCreate;
            _FileName = FileName;
            _ProductID = ProductID;
            _ProductName = ProductName;
            _URLImg = URLImg;
        }

        private int _ID;
        private string _Summary;
        private string _Title;
        private int _CategoryID;
        private string _CategoryName;
        private string _ImageThumbString;
        private string _Author;
        private string _DateCreate;
        private string _FileName;
        private int _ProductID;
        private string _ProductName;
        private string _URLImg;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public string ImageThumbString
        {
            get { return _ImageThumbString; }
            set { _ImageThumbString = value; }
        }
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public string DateCreate
        {
            get { return _DateCreate; }
            set { _DateCreate = value; }
        }
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public string URLImg
        {
            get { return _URLImg; }
            set { _URLImg = value; }
        }
    }
}
