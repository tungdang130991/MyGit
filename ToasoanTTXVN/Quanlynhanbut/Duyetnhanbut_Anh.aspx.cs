using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCComponents;
using System.Data;
using System.Configuration;
using HPCInfo;
using System.IO;

namespace ToasoanTTXVN.Quanlynhanbut
{
    public partial class Duyetnhanbut_Anh : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
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
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    if (!IsPostBack)
                    {
                        //HPCBusinessLogic.DAL.T_ThantoanTinbai obj = new HPCBusinessLogic.DAL.T_ThantoanTinbai();
                        ////obj.GetLuongtoithieu();
                        ////txt_luong.Text = obj.GetLuongtoithieu().ToString();
                        try
                        {
                            lbl_News_ID.Text = Request["ID"].ToString();
                            LoadImage();
                        }
                        catch { ;}
                    }
                }
            }
        }
        public void LoadImage()
        {
            int NewsID = int.Parse(lbl_News_ID.Text);
            int type = 0; try { type = int.Parse(Request["TypeID"].ToString()); }
            catch { ;}
            HPCBusinessLogic.ImageFilesDAL image = new HPCBusinessLogic.ImageFilesDAL();
            DataSet ds = new DataSet();
            string loaitinbaiID = "", loaianhID = "", loaivideoID = "";
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

            ds = image.ListAllImagesInNews(NewsID, type);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dgr_anh.DataSource = dt.DefaultView;
                dgr_anh.DataBind();
                foreach (DataGridItem item in dgr_anh.Items)
                {
                    item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                    item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
                    TextBox txt_tien = (TextBox)item.FindControl("txt_tien");
                    //txt_tien.Text = string.Format("{0:#,#}", txt_tien.Text).Replace(".", ",");
                    double _money = 0;
                    try
                    {
                        _money = double.Parse(txt_tien.Text);
                    }
                    catch
                    { _money = 0; }
                    txt_tien.Text = string.Format("{0:#,#}", _money).Replace(".", ",");
                }
            }
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

        protected void cmd_Chamnhanbut_Click(object sender, EventArgs e)
        {
            int type = 0; try { type = int.Parse(Request["TypeID"].ToString()); }
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
                        _Obj.Update_TiennhanbutAnh(type, int.Parse(lbl_News_ID.Text), int.Parse(lbl_Image_ID.Text), tien, _user.UserID);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lbl_tieude.Text,
                            Request["Menu_ID"].ToString(), "Chấm nhận bút ảnh ", int.Parse(lbl_Image_ID.Text), type);
                    }
                }
            }
            else
            {
                foreach (DataGridItem item in dgr_anh.Items)
                {
                    TextBox txt_tiennhanbut = (TextBox)item.FindControl("txt_tien");
                    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
                    {
                        try { int.Parse(txt_tiennhanbut.Text.Replace(",", "")); }
                        catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alrt('Tiền phải nhập kiểu số nguyên');", true); return; }
                    }
                }
                foreach (DataGridItem item in dgr_anh.Items)
                {
                    T_Butdanh obj_BD = new T_Butdanh();
                    TextBox txt_tiennhanbut = (TextBox)item.FindControl("txt_tien");
                    int newid = int.Parse(lbl_News_ID.Text);
                    Label lbl_Image_ID = (Label)item.FindControl("lbl_Image_ID");
                    int ImageID = int.Parse(lbl_Image_ID.Text);
                    int tien = 0;
                    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
                    {
                        tien = int.Parse(txt_tiennhanbut.Text.Replace(",", ""));
                    }
                    HPCBusinessLogic.DAL.T_NewsDAL _Obj = new HPCBusinessLogic.DAL.T_NewsDAL();
                    _Obj.Update_TiennhanbutAnh(type, int.Parse(lbl_News_ID.Text), int.Parse(lbl_Image_ID.Text), tien, _user.UserID);

                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lbl_tieude.Text,
                            Request["Menu_ID"].ToString(), "Chấm nhận bút ảnh ", int.Parse(lbl_Image_ID.Text), type);
                }
            }
            LoadImage();
        }

        public string GetFileURL(object Url)
        {
            string _url = "";
            try { _url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_url))
            {
                _url = Global.UploadPath + _url;
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

        public string GetChatluong(object cl)
        {
            string Chatluong = "Copy";
            try
            {
                int _cl = int.Parse(cl.ToString());
                switch (_cl)
                {
                    case 0:
                        Chatluong = "Copy";
                        break;
                    case 1:
                        Chatluong = "Thực hiện";
                        break;
                    default:
                        Chatluong = "Copy";
                        break;
                }
            }
            catch { ;}
            return Chatluong;
        }

        protected void dgr_anh_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edittt")
            {
                Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
                Label lbl_CLID = (Label)e.Item.FindControl("lblchatluongID");
                DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
                ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
                ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
                ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");

                if (lbl_CLID.Text.ToLower() != "null" && !string.IsNullOrEmpty(lbl_CLID.Text))
                    Drop_CL.SelectedIndex = int.Parse(lbl_CLID.Text.Trim());
                Image_Edit.Visible = false;
                Image_Update.Visible = true;
                Image_Cancel.Visible = true;
                lbl_CL.Visible = false;
                Drop_CL.Visible = true;
            }
            else if (e.CommandArgument.ToString().ToLower() == "update")
            {
                Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
                Label lbl_Nguonanh = (Label)e.Item.FindControl("lbl_Nguonanh");
                Label lbl_CLID = (Label)e.Item.FindControl("lblchatluongID");//
                DropDownList drop_heso = (DropDownList)e.Item.FindControl("Drop_heso");
                DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
                ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
                ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
                ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");
                int ImageID = int.Parse(dgr_anh.DataKeys[e.Item.ItemIndex].ToString());
                lbl_CL.Text = Drop_CL.SelectedItem.Text;
                lbl_CLID.Text = Drop_CL.SelectedIndex.ToString();
                Image_Edit.Visible = true;
                Image_Update.Visible = false;
                Image_Cancel.Visible = false;
                lbl_CL.Visible = true;
                Drop_CL.Visible = false;
                if (!string.IsNullOrEmpty(lbl_CLID.Text.Trim()))
                {
                    drop_heso.Items.Clear();
                    int chatluong = 0; try { chatluong = int.Parse(lbl_CLID.Text.Trim()); }
                    catch { ;}
                    UltilFunc.BindCombox(drop_heso, "HesoID", "Heso", "T_HesoTT",
                        " 1 = 1 and Heso>= (select LoaiTT_Tuheso from T_LoaihinhTT where LoaiTT_TLID=3 and LoaiTT_CLID = " + chatluong.ToString() + ")" +
                        " and  Heso <= (select LoaiTT_Denheso from T_LoaihinhTT where LoaiTT_TLID=3 and LoaiTT_CLID = " + chatluong.ToString() + ") order by Heso", "0");

                    //if (lbl_CLID.Text.Trim() == "1")
                    //{
                    //    UltilFunc.BindCombox(drop_heso, "HesoID", "Heso", "T_HesoTT", " 1 = 1 and Heso>= (select LoaiTT_Tuheso from T_LoaihinhTT where  LoaiTT_Type=2 )" +
                    //        " and  Heso <= (select LoaiTT_Denheso from T_LoaihinhTT where   LoaiTT_Type=2 ) order by Heso", "0");
                    //}
                    //else
                    //{
                    //    drop_heso.DataSource = null;
                    //    drop_heso.DataBind();
                    //}
                }
                int type = 0; try { type = int.Parse(Request["TypeID"].ToString()); }
                catch { ;}
                HPCBusinessLogic.ImageFilesDAL obj = new HPCBusinessLogic.ImageFilesDAL();
                //obj.UpdateChatluongAnh(ImageID, Drop_CL.SelectedIndex, lbl_Nguonanh.Text.Trim(), 0, type);

            }
            else if (e.CommandArgument.ToString().ToLower() == "back")
            {
                Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
                DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
                ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
                ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
                ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");
                Image_Edit.Visible = true;
                Image_Update.Visible = false;
                Image_Cancel.Visible = false;
                lbl_CL.Visible = true;
                Drop_CL.Visible = false;
            }
        }

        public string GetHeso(object obj)
        {
            string heso = "0";
            try
            {
                heso = obj.ToString().Replace(',', '.');
            }
            catch { ;}
            return heso;
        }
    }
}
