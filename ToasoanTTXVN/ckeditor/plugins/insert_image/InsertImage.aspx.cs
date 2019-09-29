﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using SSOLib.ServiceAgent;
using ToasoanTTXVN;

namespace HPCApplication.ckeditor.plugins.insert_image
{
    public partial class InsertImage : System.Web.UI.Page
    {
        public string strNumberArg = "1";
        T_Users _user = new T_Users();
        HPCBusinessLogic.NguoidungDAL _DAL = new NguoidungDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["vType"] != null)
                strNumberArg = HttpContext.Current.Request.QueryString["vType"].ToString();
            _user = _DAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user == null)
            {
                Response.Redirect(Global.ApplicationPath + "/Login.aspx");
            }
            if (!this.IsPostBack)
            {
                if (_user != null)
                {
                    LoadCM();
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
        private void ListImages()
        {
            ImageFilesDAL obj = new ImageFilesDAL();
            DataSet objDataset = new DataSet();
            try
            {
                //string where = " UserCreated =" + _user.UserID;
                string where = string.Empty;
                if (ddlStock.SelectedValue.Trim() != "0")
                    where += " 1=1 And AuthorID =1 ";
                else
                    where += " 1=1 And (( AuthorID =0 ) OR (AuthorID is null ))";
                if (!string.IsNullOrEmpty(fileName.Text))
                    where += " AND ImageFileName like N'%" + fileName.Text + "%'";
                //if (Drop_Chuyenmuc.SelectedValue.Trim() != "0")
                //    where += string.Format(" AND Categorys_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.Drop_Chuyenmuc.SelectedValue);
                if (!String.IsNullOrEmpty(txt_FromDate.Text.Trim()))
                    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)>=0) ", UltilFunc.ToDate(this.txt_FromDate.Value.ToString().Trim(), "MM/dd/yyyy"));
                if (!String.IsNullOrEmpty(txt_ToDate.Text.Trim()))
                    where += " AND " + string.Format(" (Datediff(DAY,'{0}',DateCreated)<=0)", UltilFunc.ToDate(this.txt_ToDate.Value.ToString().Trim(), "MM/dd/yyyy"));
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
                objDataset.Clear();
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
        public string GetvType()
        {
            string strTemp = "1";
            if (HttpContext.Current.Request.QueryString["vType"] != null)
                strTemp = HttpContext.Current.Request.QueryString["vType"].ToString();
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
                UltilFunc.BindCombox(Drop_Chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HienThi_BDT = 1 AND Ma_AnPham= " + this.Drop_Lang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
                UltilFunc.BindCombox(Drop_Chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HienThi_BDT = 1 AND Ma_AnPham= " + this.Drop_Lang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
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
            int ImageID = int.Parse(lblid.Text);
            ImageFilesDAL obj = new ImageFilesDAL();
            try
            {
                string strRootPathVirtual = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + lblURL.Text;
                string savepath = Server.MapPath(strRootPathVirtual);
                if (File.Exists(savepath))
                {
                    File.Delete(savepath);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa ảnh]", Convert.ToInt32(Request["Menu_ID"]).ToString(), "[Xóa ảnh] [Thao tác xóa ảnh trên server: " + lblURL.Text + "]", 0, ConstAction.BaoDT);
                }
            }
            catch { ;}
            obj.Delete_Image(ImageID);
            ListImages();
        }

        protected void Drop_Chuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Images/Video-icon.png";
                    else if (type == 3)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Images/AnotherFile.bmp";
                    else if (type == 4)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Images/acces_file.png";
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
                || FileExtend.Trim().ToLower() == ".bmt" || FileExtend.Trim().ToLower() == ".jpeg"
                )
                return 1;
            else if (FileExtend.Trim().ToLower() == ".avi" || FileExtend.Trim().ToLower() == ".flv"
                || FileExtend.Trim().ToLower() == ".mp4" || FileExtend.Trim().ToLower() == ".wmv"
                || FileExtend.Trim().ToLower() == ".wma" || FileExtend.Trim().ToLower() == ".mpeg"
                || FileExtend.Trim().ToLower() == ".mpg" || FileExtend.Trim().ToLower() == ".3gp"
                )
                return 2;
            else if (FileExtend.Trim().ToLower() == ".pdf" || FileExtend.Trim().ToLower() == ".doc"
                || FileExtend.Trim().ToLower() == ".docx" || FileExtend.Trim().ToLower() == ".xls"
                || FileExtend.Trim().ToLower() == ".txt" || FileExtend.Trim().ToLower() == ".rar"
                )
                return 4;
            else
                return 3;
        }

        protected void DropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string GetWidthImage(object _Path)
        {
            string _return = "0";
            try
            {
                string sourcepath = ConfigurationManager.AppSettings["ServerPathDis"] + _Path.ToString();
                System.Drawing.Image objImage = System.Drawing.Image.FromFile(sourcepath);
                double width = objImage.Width;
                if (width.ToString() != "0")
                    _return = width.ToString();
            }
            catch
            {
                _return = "0";
            }
            return _return;
        }
        public string GetHeightImage(object _Path)
        {
            string _return = "0";
            try
            {
                string sourcepath = ConfigurationManager.AppSettings["ServerPathDis"] + _Path.ToString();
                System.Drawing.Image objImage = System.Drawing.Image.FromFile(sourcepath);
                double height = objImage.Height;
                if (height.ToString() != "0")
                    _return = height.ToString();
            }
            catch
            {
                _return = "0";
            }
            return _return;
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
