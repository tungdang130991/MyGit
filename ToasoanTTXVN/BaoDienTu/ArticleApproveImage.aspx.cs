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

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ArticleApproveImage : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        lbl_News_ID.Text = Request["ID"].ToString();
                        LoadImage();
                    }
                }
            }
        }

        public void LoadImage()
        {
            int NewsID = int.Parse(lbl_News_ID.Text);
            int type = 1; try
            {
                if (Request["TypeID"] != null)
                    type = int.Parse(Request["TypeID"].ToString());
            }
            catch { ;}
            string loaitinbaiID = string.Empty, loaianhID = string.Empty, loaivideoID = string.Empty;
            loaitinbaiID = ConfigurationManager.AppSettings["NewsType"].ToString();
            loaianhID = ConfigurationManager.AppSettings["ImageType"].ToString();
            loaivideoID = ConfigurationManager.AppSettings["VideoType"].ToString();
            if (type.ToString() == loaitinbaiID)
            {
                HPCBusinessLogic.DAL.T_NewsDAL objnews = new HPCBusinessLogic.DAL.T_NewsDAL();
                lbl_tieude.Text = objnews.GetOneFromT_NewsByID(NewsID).News_Tittle;
            }
            else if (type.ToString() == loaianhID)
            {
                HPCBusinessLogic.DAL.T_Album_CategoriesDAL objnews = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                lbl_tieude.Text = objnews.load_T_Album_Categories(NewsID).Cat_Album_Name;
            }
            HPCBusinessLogic.ImageFilesDAL image = new HPCBusinessLogic.ImageFilesDAL();
            DataSet ds = new DataSet();
            ds = image.ListAllImagesInNews(NewsID, type);
            DataTable dt = ds.Tables[0];
            dgr_anh.DataSource = dt.DefaultView;
            dgr_anh.DataBind();
            //string _str_ID = "'";
            //string _strtextID = "'";
            //int i = 0;
            foreach (DataGridItem item in dgr_anh.Items)
            {
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");

                TextBox txt_tienNB = (TextBox)item.FindControl("txt_tiennhanbut");
                //TextBox txt_tacgia = (TextBox)item.FindControl("txt_nguonanh");
                //TextBox txt_tacgiaID = (TextBox)item.FindControl("txt_tacgiaID");
                //if (i == 0)
                //{
                //    _str_ID = _str_ID + txt_tacgiaID.ClientID;
                //    _strtextID = _strtextID + txt_tacgia.ClientID;
                //}
                //else
                //{
                //    _str_ID = _str_ID + "," + txt_tacgiaID.ClientID;
                //    _strtextID = _strtextID + "," + txt_tacgia.ClientID;
                //}
                //i++;
                if (!string.IsNullOrEmpty(txt_tienNB.Text))
                    txt_tienNB.Text = string.Format("{0:#,#}", double.Parse(txt_tienNB.Text.Replace(",", ""))).Replace(".", ",");

            }
            //_str_ID = _str_ID + "'";
            //_strtextID = _strtextID + "'";
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", " AutoCompleteSearch_Author(" + _strtextID + "," + _str_ID + ");", true);
        }

        public string GetFileURL(object Url)
        {
            string _url = "";
            try { _url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_url))
            {
                _url = Global.UploadPathBDT + _url;
                string ex = Path.GetExtension(_url);
                if (!string.IsNullOrEmpty(ex))
                {
                    int type = GetFileType(ex);
                    if (type == 2)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/Video-icon.png";
                    else if (type == 3)
                        _url = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString() + "/Dungchung/Images/AnotherFile.bmp";
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
                || FileExtend.Trim().ToLower() == ".swf"
                )
                return 2;
            else
                return 3;
        }

        protected void dgr_anh_EditCommand(object source, DataGridCommandEventArgs e)
        {
            //if (e.CommandArgument.ToString().ToLower() == "update")
            //{
            //    HPCBusinessLogic.DAL.T_ButdanhDAL obj_BD_DAL = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            //    T_Butdanh obj_BD = new T_Butdanh();
            //    //TextBox txt_tacgia = (TextBox)e.Item.FindControl("txt_nguonanh");
            //    TextBox txt_tiennhanbut = (TextBox)e.Item.FindControl("txt_tiennhanbut");
            //    int newid = int.Parse(lbl_News_ID.Text);
            //    Label lbl_Image_ID = (Label)e.Item.FindControl("lbl_Image_ID");
            //    int ImageID = int.Parse(lbl_Image_ID.Text);
            //    HPCBusinessLogic.ImageFilesDAL obj = new HPCBusinessLogic.ImageFilesDAL();
            //    int tien = 0;
            //    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
            //    {
            //        try { tien = int.Parse(txt_tiennhanbut.Text.Replace(",", "")); }
            //        catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alrt('Tiền phải nhập kiểu số nguyên');", true); return;}
            //    }
            //    int butdanhID = 0;

            //    //if (!string.IsNullOrEmpty(txt_tacgia.Text.Trim()))
            //    //{
            //        obj_BD.BD_ID = 0;
            //        obj_BD.UserID = _user.UserID;
            //        //obj_BD.BD_Name = txt_tacgia.Text.Trim();
            //        butdanhID = obj_BD_DAL.Insert_Butdang(obj_BD);
            //    //}
            //    obj.Chamnhanbutanh(0, ImageID, newid, tien, "", butdanhID);
            //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, " ",
            //    Request["Menu_ID"].ToString(), "Duyệt ảnh ", int.Parse(lbl_Image_ID.Text));
            //}
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

        protected void cmd_chamall_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_ButdanhDAL obj_BD_DAL = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            HPCBusinessLogic.ImageFilesDAL obj = new HPCBusinessLogic.ImageFilesDAL();
            int type = 1; try { type = int.Parse(Request["TypeID"].ToString()); }
            catch { ;}
            if (checkCham.Checked)
            {
                int tien = 0;
                if (!string.IsNullOrEmpty(txt_tienNB.Text.Trim()))
                {
                    try { tien = int.Parse(txt_tienNB.Text.Trim().Replace(",", "")); }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alrt('Tiền phải nhập kiểu số nguyên');", true); return; }
                    foreach (DataGridItem item in dgr_anh.Items)
                    {
                        int newid = int.Parse(lbl_News_ID.Text);
                        Label lbl_Image_ID = (Label)item.FindControl("lbl_Image_ID");
                        int ImageID = int.Parse(lbl_Image_ID.Text);

                        HPCBusinessLogic.DAL.T_NewsDAL _Obj = new HPCBusinessLogic.DAL.T_NewsDAL();
                        _Obj.Update_TiennhanbutAnh(type, newid, int.Parse(lbl_Image_ID.Text), tien, _user.UserID);

                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, " ",
                            Request["Menu_ID"].ToString(), "Duyệt ảnh ", int.Parse(lbl_Image_ID.Text), ConstAction.BaoDT);
                    }
                }
            }
            else
            {
                foreach (DataGridItem item in dgr_anh.Items)
                {
                    TextBox txt_tiennhanbut = (TextBox)item.FindControl("txt_tiennhanbut");
                    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
                    {
                        try { int.Parse(txt_tiennhanbut.Text.Replace(",", "")); }
                        catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alrt('Tiền phải nhập kiểu số nguyên');", true); return; }
                    }
                }
                foreach (DataGridItem item in dgr_anh.Items)
                {
                    TextBox txt_tiennhanbut = (TextBox)item.FindControl("txt_tiennhanbut");
                    int newid = int.Parse(lbl_News_ID.Text);
                    Label lbl_Image_ID = (Label)item.FindControl("lbl_Image_ID");
                    int ImageID = int.Parse(lbl_Image_ID.Text);
                    int tien = 0;
                    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
                    {
                        tien = int.Parse(txt_tiennhanbut.Text.Replace(",", ""));
                    }

                    HPCBusinessLogic.DAL.T_NewsDAL _Obj = new HPCBusinessLogic.DAL.T_NewsDAL();
                    _Obj.Update_TiennhanbutAnh(type, newid, ImageID, tien, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, " ",
                            Request["Menu_ID"].ToString(), "Duyệt ảnh ", int.Parse(lbl_Image_ID.Text), ConstAction.BaoDT);
                }
            }
            LoadImage();
        }
    }
}
