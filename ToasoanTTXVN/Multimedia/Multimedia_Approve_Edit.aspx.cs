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
using HPCBusinessLogic;
using HPCComponents;
using HPCBusinessLogic.DAL;
using HPCInfo;
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.Multimedia
{
    public partial class Multimedia_Approve_Edit : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    this.btnXuatban.Attributes.Add("onclick", "return confirm('" + CommonLib.ReadXML("lblBanmuonXB") + "');");
                    this.btnTraLai.Attributes.Add("onclick", "return confirm('" + CommonLib.ReadXML("lblBanmuontralai") + "');");
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                    }
                }
            }
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txt_tien.Text))
                {
                    int tien = 0; try
                    {
                        tien = int.Parse(txt_tien.Text.Replace(",", ""));
                    }

                    catch
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                        return;
                    }
                }
                T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
                HPCInfo.T_Multimedia obj = SetItem(2);
                if (obj.URL_Images.Length > 2)
                    this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + obj.URL_Images;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                if (obj.ID > 0)
                {
                    int _return = _untilDAL.InsertT_Multimedia(obj);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.Tittle, Request["Menu_ID"].ToString(), "CHỜ XUẤT BẢN - SỬA ĐỔI THÔNG TIN", _return, ConstAction.AmThanhHinhAnh);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "message", "alert('Bạn đã cập nhật thành công');", true);
                }
            }
        }
        protected void linkPub_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txt_tien.Text))
                {
                    int tien = 0; try
                    {
                        tien = int.Parse(txt_tien.Text.Replace(",", ""));
                    }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true); return; }
                }
                T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
                HPCInfo.T_Multimedia obj = SetItem(3);
                int _return = _untilDAL.InsertT_Multimedia(obj);
                #region Sync
                // DONG BO FILE
                try
                {
                    T_Multimedia _objSet = new T_Multimedia();
                    _objSet = _untilDAL.GetOneFromT_MultimediaByID(_return);
                    SynFiles _syncfile = new SynFiles();
                    if (_objSet.URL_Images.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_objSet.URL_Images, HPCComponents.Global.ImagesService);
                    }
                    if (_objSet.URLPath.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_objSet.URLPath, HPCComponents.Global.ImagesService);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                //END
                #endregion
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.Tittle, Request["Menu_ID"].ToString(), "XUẤT BẢN", _return, ConstAction.AmThanhHinhAnh);
                Page.Response.Redirect("~/Multimedia/Multimedia_Approve.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + Convert.ToInt32(Request["TabID"]));
            }
        }
        protected void linkTralai_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txt_tien.Text))
                {
                    int tien = 0; try
                    {
                        tien = int.Parse(txt_tien.Text.Replace(",", ""));
                    }
                    catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Tiền nhận bút phải là số nguyên !');", true); return; }
                }
                T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
                HPCInfo.T_Multimedia obj = SetItem(21);
                int _return = _untilDAL.InsertT_Multimedia(obj);
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.Tittle, Request["Menu_ID"].ToString(), "CHỜ DUYỆT TRẢ LẠI", _return, ConstAction.AmThanhHinhAnh);
                Page.Response.Redirect("~/Multimedia/Multimedia_Approve.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + Convert.ToInt32(Request["TabID"]));
            }
        }
        protected void ddlLang_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorys.Items.Clear();
            if (ddlLang.SelectedIndex >= 0)
            {
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                ddlCategorys.UpdateAfterCallBack = true;
            }
            else
            {
                this.ddlCategorys.DataSource = null;
                this.ddlCategorys.DataBind();
                this.ddlCategorys.UpdateAfterCallBack = true;
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (ddlLang.Items.Count >= 3)
                ddlLang.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(this.ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Multimedia/Multimedia_Approve.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + Convert.ToInt32(Request["TabID"]));
        }
        private T_Multimedia SetItem(int Status)
        {
            T_MultimediaDAL _DAL = new T_MultimediaDAL();
            HPCInfo.T_Multimedia _obj = new HPCInfo.T_Multimedia();
            if (Page.Request.Params["id"] != null)
            {
                _obj.ID = int.Parse(Page.Request["ID"].ToString());
                _obj = _DAL.GetOneFromT_MultimediaByID(_obj.ID);
            }
            else
                _obj.ID = 0;
            _obj.Copy_From = 0;
            _obj.Tittle = this.txtTitle.Text.Trim();
            if (ddlLang.SelectedIndex > 0)
            {
                _obj.Languages_ID = Convert.ToInt32(this.ddlLang.SelectedValue);
                _obj.Category = Convert.ToInt32(this.ddlCategorys.SelectedValue.ToString());
            }
            _obj.URLPath = UrlPathImage_RemoveUpload(this.txtVideoPath.Text);
            _obj.URL_Images = UrlPathImage_RemoveUpload(txtThumbnail.Text);
            //_obj.DateCreated = DateTime.Now;
            int butdanhID = 0;
            HPCInfo.T_Butdanh obj_BD = new HPCInfo.T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(txt_tacgia.Text.Trim()))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = txt_tacgia.Text.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _obj.AuthorID = butdanhID;
            _obj.DateModify = DateTime.Now;
            _obj.Contents = this.Txt_Desc.Text.Trim();
            //_obj.UserCreated = _user.UserID;
            _obj.UserModify = _user.UserID;
            _obj.UserPublish = _user.UserID;
            _obj.DatePublish = DateTime.Now;
            _obj.Status = Status;
            _obj.Display = true;
            
            _obj.IsLog = false;
            _obj.Tacgia = txt_tacgia.Text;
            _obj.Comment = txtGhichu.Text;
            //_obj.Chatluong = ddl_chatluong.SelectedIndex;
            if (!string.IsNullOrEmpty(txt_tien.Text))
                _obj.TienNB = int.Parse(txt_tien.Text.Replace(",", ""));
            else
                _obj.TienNB = 0;
            return _obj;
        }
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
        }
        private void PopulateItem(int _id)
        {
            HPCInfo.T_Multimedia _obj = new HPCInfo.T_Multimedia();
            T_MultimediaDAL _DAL = new T_MultimediaDAL();
            _obj = _DAL.GetOneFromT_MultimediaByID(_id);
            if (_obj != null)
            {
                this.txtVideoID.Text = _obj.ID.ToString();
                this.txtTitle.Text = _obj.Tittle;
                this.Txt_Desc.Text = _obj.Contents;
                this.txtVideoPath.Text = _obj.URLPath;
                txtThumbnail.Text = _obj.URL_Images.ToString();
                if (_obj.URL_Images.Length > 2)
                    this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _obj.URL_Images;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, _obj.Languages_ID.ToString());
                this.ddlCategorys.Items.Clear();
                HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                this.txt_tacgia.Text = obj.GetBD_Name(_obj.AuthorID);
                if (ddlLang.SelectedIndex > 0)
                {
                    UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by Ten_ChuyenMuc ASC");
                    this.ddlCategorys.UpdateAfterCallBack = true;
                    this.ddlCategorys.SelectedIndex = CommonLib.GetIndexControl(this.ddlCategorys, _obj.Category.ToString());
                }
                else
                {
                    this.ddlCategorys.DataSource = null;
                    this.ddlCategorys.DataBind();
                    this.ddlCategorys.UpdateAfterCallBack = true;
                }
                txt_tacgia.Text = _obj.Tacgia;
                this.txtGhichu.Text = _obj.Comment;
                if (_obj.TienNB > 0)
                {
                    this.txt_tien.Text = string.Format("{0:#,#}", Convert.ToDouble(_obj.TienNB.ToString())).Replace(".", ",");
                }
                //else
                //    txt_tien.Text = "0";
                //try { ddl_chatluong.SelectedIndex = _obj.Chatluong; }
                //catch { ;}

            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    PopulateItem(Convert.ToInt32(Request["ID"].ToString()));
                }
            }
            else
            {
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
            }
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
