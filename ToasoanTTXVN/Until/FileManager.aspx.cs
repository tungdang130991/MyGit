using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using System.IO;
using System.Drawing;
using HPCServerDataAccess;
using SSOLib.ServiceAgent;

namespace HPCApplication.Until
{
    public partial class FileManager : System.Web.UI.Page
    {
        public string strNumberArg = "1";
        public string strPathImage = "";
        T_Users _user = new T_Users();
        //UserDAL _DAL = new UserDAL();
        HPCBusinessLogic.NguoidungDAL _DAL = new NguoidungDAL();
        public int CurrentPage
        {
            get
            {
                // look for current page in ViewState
                object o = this.ViewState["_CurrentPage"];
                if (o == null)
                    return 0;	// default to showing the first page
                else
                    return (int)o;
            }
            set
            {
                this.ViewState["_CurrentPage"] = value;
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            strNumberArg = HttpContext.Current.Request.QueryString["vType"].ToString();

            _user = _DAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user == null)
            {
                Response.Redirect(Global.ApplicationPath + "/Login.aspx", true);
            }
            if (!this.IsPostBack)
            {
                if (HttpContext.Current.Request.QueryString["imgPath"] != null && HttpContext.Current.Request.QueryString["imgPath"].ToString() != "")
                    strPathImage = HttpContext.Current.Request.QueryString["imgPath"].ToString();
                if (strPathImage.Length > 0)
                {
                    lblFilename.InnerHtml = "Thư mục:" + strPathImage;
                    string v_path = strPathImage.Replace(Global.UploadPathBDT.ToString(), "");
                    txt_UrlImage.Text = v_path;
                    ContainerCrop.InnerHtml = "<img style=\"cursor:pointer;\" id=\"imgCrop\" onclick=\"getImgSrc('" + strPathImage + "','','','');\"  src=\"" + strPathImage + "\" />";
                }
                txt_FromDate.Text = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
                txt_ToDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                if (_user != null)
                {

                    LoadCM();
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
        private void ListImages()
        {
            try
            {
                ImageFilesDAL obj = new ImageFilesDAL();
                DataSet objDataset = new DataSet();

                // Populate the repeater control with the Items DataSet
                PagedDataSource objPds = new PagedDataSource();
                //string where = " UserCreated =" + _user.UserID;
                string where = "";
                if (ddlStock.SelectedValue.Trim() != "0")
                    where += " 1=1 And AuthorID =1 ";
                else
                    where += " 1=1 And (( AuthorID =0 ) OR (AuthorID is null ))";
                if (!string.IsNullOrEmpty(txtTenfile.Text))
                    where += " AND ImageFileName like N'%" + txtTenfile.Text + "%'";
                if (Drop_Chuyenmuc.SelectedValue.Trim() != "0")
                    where += string.Format(" AND Categorys_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.Drop_Chuyenmuc.SelectedValue);
                if (!String.IsNullOrEmpty(txt_FromDate.Text.Trim()))
                    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)>=0)", UltilFunc.ToDate(this.txt_FromDate.Value.ToString().Trim(), "MM/dd/yyyy"));
                if (!String.IsNullOrEmpty(txt_ToDate.Text.Trim()))
                    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)<=0) ", UltilFunc.ToDate(this.txt_ToDate.Value.ToString().Trim(), "MM/dd/yyyy"));
                else
                {
                    if (ddlStock.SelectedValue.Trim() == "0")
                        where += " AND " + string.Format(" DATEDIFF(DAY,DateCreated,'{0}')=0 ", DateTime.Now.ToString("MM/dd/yyyy"));
                }
                where += " ORDER BY DateCreated DESC";
                objDataset = obj.ListAllImages(where);
                if (objDataset != null && objDataset.Tables[0].Rows.Count > 0)
                {
                    DataTable _dv = obj.BindGridListImages2Table(objDataset.Tables[0]);
                    objPds.DataSource = _dv.DefaultView;
                    objPds.AllowPaging = true;
                    if (ddlStock.SelectedValue.Trim() != "0")
                        objPds.PageSize = 30;
                    else
                        objPds.PageSize = 12;

                    objPds.CurrentPageIndex = CurrentPage;

                    lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " / " + objPds.PageCount.ToString();

                    // Disable Prev or Next buttons if necessary
                    cmdNext.Visible = true;
                    cmdPrev.Visible = true;
                    cmdPrev.Enabled = !objPds.IsFirstPage;
                    cmdNext.Enabled = !objPds.IsLastPage;

                    //dlImages.DataSource = _dv;
                    dlImages.DataSource = objPds;
                    dlImages.DataBind();
                }
                else
                {
                    lblCurrentPage.Text = "";
                    cmdNext.Visible = false;
                    cmdPrev.Visible = false;
                    dlImages.DataSource = null;
                    dlImages.DataBind();
                }
                objDataset.Dispose();
            }
            catch
            {
                dlImages.DataSource = null;
                dlImages.DataBind();
            }
        }
        public string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString() + "," + GetvType() + "," + HPCSecurity.CurrentUser.Identity.ID.ToString() + "," + GetChuyenmuc();
            return strTemp;
        }
        public string GetUserID()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;
        }
        public string GetvType()
        {
            string strTemp = HttpContext.Current.Request.QueryString["vType"].ToString();
            return strTemp;
        }
        public string GetChuyenmuc()
        {
            string strTemp = Drop_Chuyenmuc.SelectedValue.Trim();
            return strTemp;
        }
        public void LoadCM()
        {
            UltilFunc.BindCombox(Drop_Lang, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" 1=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");

            if (Drop_Lang.Items.Count >= 3)
                Drop_Lang.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else Drop_Lang.SelectedIndex = UltilFunc.GetIndexControl(Drop_Lang, HPCComponents.Global.DefaultCombobox);
            if (Drop_Lang.SelectedIndex != 0)
                UltilFunc.BindCombox(Drop_Chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 AND Ma_AnPham=" + this.Drop_Lang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(Drop_Chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 AND Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ViewState["_CurrentPage"] = 0;
            ListImages();
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
        protected void Drop_Lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drop_Chuyenmuc.Items.Clear();
            if (Drop_Lang.SelectedIndex >= 0)
            {
                UltilFunc.BindCombox(Drop_Chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 AND Ma_AnPham= " + this.Drop_Lang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                //Drop_Chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                this.Drop_Chuyenmuc.DataSource = null;
                this.Drop_Chuyenmuc.DataBind();
                //this.Drop_Chuyenmuc.UpdateAfterCallBack = true;
            }
        }

        protected void dlImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            // xóa ảnh
            Label lblid = (Label)e.Item.FindControl("lbl_ID");
            Label lblURL = (Label)e.Item.FindControl("lbl_URL");
            txt_UrlImage.Text = lblURL.Text;
            int ImageID = int.Parse(lblid.Text);
            ImageFilesDAL obj = new ImageFilesDAL();
            try
            {
                string strRootPathVirtual = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + lblURL.Text;
                string savepath = Server.MapPath(strRootPathVirtual);
                if (File.Exists(savepath))
                {
                    File.Delete(savepath);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]).ToString(), "[Xóa ảnh] [Thao tác xóa ảnh trên server: " + lblURL.Text + "]", 0, 0);
                }
            }
            catch { ;}
            obj.Delete_Image(ImageID);
            ListImages();
        }
        public string GetFileURL(object Url)
        {
            string _url = string.Empty;
            try { _url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_url))
            {
                //_url = Global.TinPath + _url;
                _url = Global.TinPathBDT + _url; //_url;
                string ex = Path.GetExtension(_url);
                if (!string.IsNullOrEmpty(ex))
                {
                    int type = GetFileType(ex);
                    if (type == 2)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/DungChung/Images/Video-icon.png";
                    else if (type == 3)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/DungChung/Images/AnotherFile.bmp";
                }
            }
            else
            {
                _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/DungChung/Images/NoImage.png";
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
                || FileExtend.Trim().ToLower() == ".swf"
                )
                return 2;
            else
                return 3;
        }

        protected void cmdPrev_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the previous page
            CurrentPage -= 1;

            // Reload control
            ListImages();
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the next page
            CurrentPage += 1;

            // Reload control
            ListImages();
        }
        protected void cmd_watermark_Click(object sender, EventArgs e)
        {

            string url1 = txt_UrlImage.Text.Trim();
            if (!string.IsNullOrEmpty(url1))
            {
                string url = ConfigurationManager.AppSettings["ServerPathDis"].ToString() + url1;
                if (File.Exists(url))
                {
                    int _instock = Convert.ToInt32(ddlStock.SelectedValue.Trim());
                    string strPhysLocal = "";
                    if (strNumberArg == "1")
                    {
                        if (_instock != 0)
                            strPhysLocal = "/" + "InStock/Thumnail/";
                        else
                            strPhysLocal = "/" + "Article/Thumnail/";
                    }
                    if (strNumberArg == "2")
                        strPhysLocal = "/" + Global.UploadPhotoAlbum + "/";
                    if (strNumberArg == "3")
                        strPhysLocal = "/" + Global.UploadPhotoEvent + "/";

                    //string width = DropSize.SelectedValue;
                    System.Drawing.Bitmap sourceImage = new System.Drawing.Bitmap(url);
                    string _extension = Path.GetExtension(url);

                    string filename = "WaterMark_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + DateTime.Now.Millisecond.ToString() + Path.GetExtension(url);
                    Bitmap imgsave = null;
                    string newfolder = strPhysLocal + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                    string pathsave = ConfigurationManager.AppSettings["ServerPathDis"].ToString() + newfolder;
                    string _logo = Server.MapPath("../Images/IconHPC/LoGoBaoAnhDong.png");
                    Bitmap Imagemark = new Bitmap(_logo);
                    int spacevalues = 0;
                    try { spacevalues = int.Parse(ConfigurationManager.AppSettings["SpaceValue"].ToString()); }
                    catch { ;}

                    //imgsave = HPCImages.WatermarkImages(sourceImage, Imagemark, int.Parse(DropStyle.SelectedValue), spacevalues);
                    imgsave = HPCImages.WatermarkImages(sourceImage, Imagemark, int.Parse(X11.Value), int.Parse(Y11.Value));

                    if (Directory.Exists(pathsave) == false) Directory.CreateDirectory(pathsave);

                    imgsave.Save(pathsave + @"\" + filename);
                    imgsave.Dispose();
                    Imagemark.Dispose();
                    sourceImage.Dispose();

                    txt_UrlImage.Text = newfolder + "/" + filename;

                    T_ImageFiles _obj = new T_ImageFiles();
                    ImageFilesDAL _DAL = new ImageFilesDAL();
                    _obj = SetItem(filename, 0, txt_UrlImage.Text, _extension, _user.UserID, Convert.ToInt16(strNumberArg), Convert.ToInt32(Drop_Chuyenmuc.SelectedValue));

                    int _idReturn = _DAL.InsertT_ImageFiles(_obj);
                    if (_instock != 0)
                        _DAL.UpdateStatusDataByID(" AuthorID =1 Where ID =" + _idReturn);
                    ListImages();
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "PreviewImage('" + newfolder + "/" + filename + "','" + strNumberArg + "','0','" + _extension + "');", true);
                }

            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Hãy chọn ảnh để đóng dấu ');", true);
            }
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
