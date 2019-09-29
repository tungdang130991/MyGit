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
using SSOLib.ServiceAgent;

namespace HPCApplication.UploadVideos
{
    public partial class Videos_Managerment : System.Web.UI.Page
    {
        private string strRootPathVirtual;
        public string strNumberArg = string.Empty;
        public string strKeyLogo = "1";
        NguoidungDAL DAL = new NguoidungDAL();
        T_Users user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            strNumberArg = Request.QueryString["vType"].ToString();
            if (Request.QueryString["vKey"] != null)
                strKeyLogo = Request.QueryString["vKey"].ToString();
            user = DAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (user == null)
            {
                Show("Phiên làm việc hết hiệu lực !");
                Response.Redirect(Global.ApplicationPath + "/login.aspx", true);
            }
            if (!this.IsPostBack)
            {
                ListImages();
            }    
        }
        #region "File process"
        private void ListImages()
        {
            ImageFilesDAL obj = new ImageFilesDAL();
            DataSet objDataset = new DataSet();
            string where = " UserCreated =" + user.UserID;
            //if (!String.IsNullOrEmpty(txt_FromDate.Text.Trim()))
            //    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)>=0) ", UltilFunc.ToDate(this.txt_FromDate.Value.ToString().Trim(), "MM/dd/yyyy"));
            //if (!String.IsNullOrEmpty(txt_ToDate.Text.Trim()))
            //    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)<=0) ", UltilFunc.ToDate(this.txt_ToDate.Value.ToString().Trim(), "MM/dd/yyyy"));
            //else 
            where += " AND " + string.Format(" DATEDIFF(DAY,DateCreated,'{0}')=0 ", DateTime.Now.ToString("MM/dd/yyyy"));
            where += " ORDER BY DateCreated DESC";
            objDataset = obj.ListAllImages(where);
            if (objDataset != null)
            {
                DataView _dv = obj.BindGridListImages(objDataset.Tables[0]);
                dlImages.DataSource = _dv;
                dlImages.DataBind();
            }
        }
        public string Cut_Filename(object filename)
        {
            string _Name = string.Empty;
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
        public string GetFileURL(object Url)
        {
            string _url = string.Empty;
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
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/DungChung/Images/Video-icon.png";
                    else if (type == 3)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/DungChung/Images/AnotherFile.bmp";
                }
            }
            else
            {
                _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Images/NoImage.png";
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
            else
                return 3;
        }
        private void DisplayAllFiles(string strVirtualDirectory)
        {
            DataTable myDataTable;
            myDataTable = ReadAllFile2DataTable(strVirtualDirectory);
            DataView sortedView = new DataView(myDataTable);
            sortedView.Sort = "Created, FileName asc";

            dlImages.RepeatColumns = 4;
            dlImages.DataSource = sortedView;
            dlImages.DataBind();

            sortedView = null;
            myDataTable = null;
        }
        private DataTable ReadAllFile2DataTable(string strVirtualPath)
        {
            //string strPhysicalPath = Server.MapPath(strVirtualPath);
            string strExtenion = string.Empty;
            string strPhysicalPath = Server.MapPath(strVirtualPath);
            FileInfo[] fa;
            DirectoryInfo di = new DirectoryInfo(strPhysicalPath);
            fa = di.GetFiles();

            DataTable dt = GetFileInfoTable();
            dt.BeginLoadData();
            foreach (FileInfo f in fa)
            {
                strExtenion = f.Extension.ToLower();
                if (Convert.ToInt32(strNumberArg) == 1)
                {
                    if (strExtenion == ".gif" || strExtenion == ".png" || strExtenion == ".jpg" || strExtenion == ".bmp")
                        AddRowToFileInfoTable(f, dt);
                }
                else
                {
                    AddRowToFileInfoTable(f, dt);
                }
            }
            dt.EndLoadData();
            dt.AcceptChanges();
            return dt;
        }

        private DataTable GetFileInfoTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileName", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("FilePath", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("FileExtension", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Size", Type.GetType("System.Int64")));
            dt.Columns.Add(new DataColumn("Modified", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Created", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("FileView", Type.GetType("System.String")));
            return dt;
        }
        private void AddRowToFileInfoTable(FileSystemInfo fi, DataTable dt)
        {
            string PicturePreview = Server.MapPath("~/images/PicturePreview/");
            string strExtenion = fi.Extension.ToLower();
            DataRow dr = dt.NewRow();
            dr["FileName"] = fi.Name;
            if (strExtenion == ".gif" || strExtenion == ".png" || strExtenion == ".jpg" || strExtenion == ".bmp")
                dr["FilePath"] = Path.GetFullPath(fi.FullName);
            else
                dr["FilePath"] = Path.GetFullPath(PicturePreview + "\\" + strExtenion.Replace(".", "").Trim() + ".flv");

            dr["FileExtension"] = Path.GetExtension(fi.Name);
            dr["Size"] = new FileInfo(fi.FullName).Length;
            dr["Modified"] = fi.LastWriteTime;
            dr["Created"] = fi.CreationTime;
            dr["FileView"] = Path.GetFullPath(fi.FullName);
            dt.Rows.Add(dr);
        }
        #endregion
        #region "Folder process"
        private void CreateFolderByUserName(string FolderName)
        {
            string strRootPath = "";
            strRootPath = Server.MapPath(FolderName);
            if (Directory.Exists(strRootPath) == false) Directory.CreateDirectory(strRootPath);

        }
        private bool checkFolderExist(string FolderName)
        {
            string strRootPath = "";
            strRootPath = Server.MapPath(FolderName);
            if (Directory.Exists(strRootPath))
                return true;
            else
                return false;

        }
        private void DisplayAllFolder(string strVirtualDirectory)
        {
            DataTable myDataTable;
            myDataTable = ReadAllFolder2DataTable(strVirtualDirectory);
            DataView sortedView = new DataView(myDataTable);
            sortedView.Sort = "Created, FileName asc";

            dlFolder.RepeatColumns = 4;
            dlFolder.DataSource = sortedView;
            dlFolder.DataBind();

            sortedView = null;
            myDataTable = null;
        }
        private DataTable ReadAllFolder2DataTable(string strVirtualPath)
        {
            //string strPhysicalPath = Server.MapPath(strVirtualPath);
            string strPhysicalPath = Server.MapPath(strVirtualPath);
            DirectoryInfo[] da;
            DirectoryInfo di = new DirectoryInfo(strPhysicalPath);
            da = di.GetDirectories();
            DataTable dt = GetFolderInfoTable();

            dt.BeginLoadData();
            foreach (DirectoryInfo d in da)
            {
                AddRowToFolderInfoTable(d, dt);
            }
            dt.EndLoadData();
            dt.AcceptChanges();
            return dt;
        }

        private DataTable GetFolderInfoTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileName", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("FilePath", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("FileExtension", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Size", Type.GetType("System.Int64")));
            dt.Columns.Add(new DataColumn("Modified", Type.GetType("System.DateTime")));
            dt.Columns.Add(new DataColumn("Created", Type.GetType("System.DateTime")));
            return dt;
        }
        private void AddRowToFolderInfoTable(FileSystemInfo fi, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["FileName"] = fi.Name;
            dr["FilePath"] = Path.GetFullPath(fi.FullName);
            dr["FileExtension"] = Path.GetExtension(fi.Name);
            dr["Size"] = 0;
            dr["Modified"] = fi.LastWriteTime;
            dr["Created"] = fi.CreationTime;
            dt.Rows.Add(dr);
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dlFolder.EditCommand += new DataListCommandEventHandler(dlFolder_EditCommand);
            this.btnSearchFolder.Click += new EventHandler(btnSearchFolder_Click);
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        #region "Show Alert Message"
        public void Show(string message)
        {
            // Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

            // Gets the executing web page 
            Page page = HttpContext.Current.Handler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page 
            if (page != null && !page.IsClientScriptBlockRegistered("alert"))
            {
                page.RegisterClientScriptBlock("alert", script);
            }

        }

        #endregion
        #region "Upload process"
        private void SaveFile(string strDestDirectory)
        {

            string strClientFullPath = "";
            string strFileName = null;
            string strInputFilePath = null;
            if (strDestDirectory == "" || strDestDirectory == null)
            {
                Show("Mời bạn chọn thư mục để Upload.");
                return;
            }
            else
            {
                strInputFilePath = Server.MapPath(strDestDirectory);
                if (Directory.Exists(strInputFilePath) == false) Directory.CreateDirectory(strInputFilePath);
            }
            if (txtFile.PostedFile.FileName != null && txtFile.PostedFile.FileName != "")
            {
                strClientFullPath = txtFile.PostedFile.FileName;
                char[] ch = { '\\' };
                string[] arr = strClientFullPath.Split(ch);
                strFileName = arr[arr.Length - 1];
                strFileName = ProcessExistFileName(strFileName, strDestDirectory);
                txtFile.PostedFile.SaveAs(strInputFilePath + "\\" + strFileName);
                Show("Cập nhật thành công!");
            }
            else
            {
                Show("Mời bạn chọn một File để Upload.");
            }
        }
        private string ProcessExistFileName(string strFileName, string strDestDir)
        {
            string strFileTempName = "";
            string strInputFilePath = "";
            strInputFilePath = Server.MapPath(strDestDir) + "\\" + strFileName;

            if (System.IO.File.Exists(strInputFilePath))
            {
                string strFile = strFileName;
                string strFileExtension = strFile.Substring(strFile.LastIndexOf('.'));
                string strFileName1 = strFile.Substring(0, strFile.LastIndexOf('.'));
                strFileTempName = strFileName1 + "-" + DateTime.Now.Ticks.ToString() + strFileExtension;
            }
            else
                strFileTempName = strFileName;

            return strFileTempName;

        }
        #endregion
        private void btnSearchFolder_Click(object sender, System.EventArgs e)
        {

            string strPhysLocal="";
            try
            {
                if (user.UserName != null)
                {
                    if (strKeyLogo == "2")
                        strPhysLocal = "/Upload/Videos/";// + user.UserName.ToString() + "/";
                     if (strKeyLogo == "1")
                         strPhysLocal = "/Upload/Adv/";// + user.UserName.ToString() + "/";
                }   
                else
                {
                    Show("Phiên làm việc hết hiệu lực !");
                    return;
                }
                strPhysLocal = strPhysLocal + cbo_Nam.SelectedValue.ToString() + "/" + cbo_Thang.SelectedValue.ToString() + "/" + combo_Ngay.SelectedValue.ToString() + "/";
                //Show(strPhysLocal);
                if (checkFolderExist(strPhysLocal))
                {
                    DisplayAllFolder(strPhysLocal);
                    DisplayAllFiles(strPhysLocal);
                    lblFolder2Upload.Text = strPhysLocal;
                }
                else
                {
                    Show("Thư mục này chưa tồn tại!");
                }

            }
            catch (Exception ex)
            {
                Show(ex.Message.ToString());
            }

        }
        private void btnUpload_Click(object sender, System.EventArgs e)
        {
            if (user.UserName != null)
            {
                string strPhysFullPath = string.Empty;
                if (strKeyLogo == "2")
                    strPhysFullPath = "/Upload/Videos/";// +user.UserName.ToString() + "/";
                if (strKeyLogo == "1") strPhysFullPath = "/Upload/Adv/";// +user.UserName.ToString() + "/";
                strPhysFullPath = strPhysFullPath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                SaveFile(strPhysFullPath);
                DisplayAllFiles(strPhysFullPath);
                DisplayAllFolder(strPhysFullPath);
                lblFolder2Upload.Text = strPhysFullPath;
                cbo_Nam.SelectedValue = DateTime.Now.Year.ToString();
                cbo_Thang.SelectedValue = DateTime.Now.Month.ToString();
                combo_Ngay.SelectedValue = DateTime.Now.Day.ToString();
            }
            else
                Show("Bạn không có quyền tải ảnh lên máy chủ!");

        }
        public string UrlPathImage_Display(object PhysPathFull)
        {
            //string strLocalRootPath = Server.MapPath("/UploadVideos/");
            string strLocalRootPath = "";// Server.MapPath("/Upload/Videos/");
            if (strKeyLogo == "2")
                strLocalRootPath = Server.MapPath("/Upload/Videos/");
            if (strKeyLogo == "1")
                strLocalRootPath = Server.MapPath("/Upload/Adv/");

            string PicturePreview = Server.MapPath("~");
            string strTemp = "";
            strTemp = WebPath(PhysPathFull.ToString());
            if (strTemp.ToLower().IndexOf("Videos") > 0)
            {
                strTemp = strTemp.Substring(strLocalRootPath.Length);
                if (strKeyLogo == "2")
                    strTemp = "/Upload/Videos/" + strTemp;//+ user.UserName + "/";
                if (strKeyLogo == "1")
                    strTemp = "/Upload/Adv/" + strTemp;//+ user.UserName + "/";
            }
            else
            {
                strTemp = strTemp.Substring(PicturePreview.Length);
                if(strKeyLogo=="2")
                    strTemp = Global.ApplicationPath + "/Images/Icons/MediaFileIcon.png";
                else 
                    strTemp = Global.ApplicationPath + strTemp;
            }


            return strTemp;
        }
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
        }
        private string WebPath(string path1)
        {
            string strTemp = path1.Replace("\\", "/");
            return strTemp;
        }
        private string UrlPathImage(string PhysPathFull)
        {
            string strLocalRootPath = string.Empty;//Server.MapPath("/Upload/Videos/");
            if (strKeyLogo == "2")
                strLocalRootPath = "/Upload/Videos/";//+ user.UserName + "/";
            if (strKeyLogo == "1")
                strLocalRootPath = "/Upload/Adv/";//+ user.UserName + "/";

            string strTemp = "";
            strTemp = WebPath(PhysPathFull);
            strTemp = strTemp.Substring(strLocalRootPath.Length);
            return strTemp;
        }

        private void dlFolder_EditCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            if (e.CommandName.ToString().ToLower() == "edit")
            {
                lblFolder2Upload.Text = UrlPathImage_Display(this.dlFolder.DataKeys[e.Item.ItemIndex].ToString());
                DisplayAllFolder(lblFolder2Upload.Text);
                DisplayAllFiles(lblFolder2Upload.Text);
            }
        }

        public string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString() + "," + GetvType() + "," + HPCSecurity.CurrentUser.Identity.ID.ToString();
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
    }
}
