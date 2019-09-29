using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using System.IO;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Multimedia
{
    public partial class Edit_Multimedia : BasePage
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
                string TabID = "0";
                if (Request["TabID"] != null)
                    TabID = Request["TabID"].ToString();
                string strAction = "";
                T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
                HPCInfo.T_Multimedia obj = null;
                if (TabID == "0")
                    obj = SetItem(1);
                else if (TabID == "1")
                    obj = SetItem(21);

                if (obj.ID == 0)
                    strAction = "THÊM MỚI";
                else
                    strAction = "SỬA ĐỔI THÔNG TIN";
                int _return = _untilDAL.InsertT_Multimedia(obj);

                if (obj.URL_Images.Length > 2)
                    this.ImgTemp.Src = obj.URL_Images;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");

                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.Tittle, Request["Menu_ID"].ToString(), strAction, _return, ConstAction.AmThanhHinhAnh);
                Page.Response.Redirect("~/Multimedia/List_Multimedia.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + TabID);
            }
        }
        protected void ddlLang_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorys.Items.Clear();
            if (ddlNgonNgu.SelectedIndex >= 0)
            {
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                ddlCategorys.UpdateAfterCallBack = true;
            }
            else
            {
                this.ddlCategorys.DataSource = null;
                this.ddlCategorys.DataBind();
                this.ddlCategorys.UpdateAfterCallBack = true;
            }
        }
        protected void linkSendPub_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strAction = ""; string TabID = "0";
                if (Request["TabID"] != null)
                    TabID = Request["TabID"].ToString();
                T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
                HPCInfo.T_Multimedia obj = null;
                if (TabID == "0")
                    strAction = "THÊM MỚI VÀ GỬI DUYỆT";
                else if (TabID == "1")
                    strAction = "SỬA ĐỔI BÀI TRẢ LẠI VÀ GỬI DUYỆT";
                obj = SetItem(2);
                int _return = _untilDAL.InsertT_Multimedia(obj);
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.Tittle, Request["Menu_ID"].ToString(), strAction, _return, ConstAction.AmThanhHinhAnh);
                Page.Response.Redirect("~/Multimedia/List_Multimedia.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + TabID);
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (ddlNgonNgu.Items.Count >= 3)
                ddlNgonNgu.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                ddlNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(this.ddlNgonNgu, HPCComponents.Global.DefaultCombobox);
            if (ddlNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            string TabID = "0";
            if (Request["TabID"] != null)
                TabID = Request["TabID"].ToString();
            Page.Response.Redirect("~/Multimedia/List_Multimedia.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&TabID=" + TabID);
        }
        private T_Multimedia SetItem(int Status)
        {
            T_MultimediaDAL video = new T_MultimediaDAL();
            HPCInfo.T_Multimedia _obj = new HPCInfo.T_Multimedia();
            if (Page.Request.Params["id"] != null)
            {
                _obj.ID = int.Parse(Page.Request["ID"].ToString());
                _obj = video.GetOneFromT_MultimediaByID(_obj.ID);
            }
            else
            {
                _obj.ID = 0;
                _obj.DateCreated = DateTime.Now;
                _obj.UserCreated = _user.UserID;
            }
            _obj.Tittle = this.txtTitle.Text.Trim();
            if (ddlNgonNgu.SelectedIndex > 0)
            {
                _obj.Languages_ID = Convert.ToInt32(this.ddlNgonNgu.SelectedValue);
                _obj.Category = Convert.ToInt32(this.ddlCategorys.SelectedValue.ToString());
            }
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(txt_tacgia.Text.Trim()))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = txt_tacgia.Text.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _obj.AuthorID = butdanhID;
            _obj.URLPath = UrlPathImage_RemoveUpload(this.txtVideoPath.Text);
            _obj.URL_Images = UrlPathImage_RemoveUpload(txtThumbnail.Text);
            _obj.DateModify = DateTime.Now;
            _obj.Contents = this.Txt_Desc.Text.Trim();
            _obj.UserModify = _user.UserID;
            _obj.IsLog = false;
            _obj.Status = Status;
            _obj.Display = true;
            _obj.Tacgia = txt_tacgia.Text;
            _obj.Comment = txtGhichu.Text;
            //_obj.Chatluong = ddl_chatluong.SelectedIndex;
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

                this.txtThumbnail.Text = _obj.URL_Images.ToString();
                if (_obj.URL_Images.Length > 2)
                    this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _obj.URL_Images;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                this.txt_tacgia.Text = obj.GetBD_Name(_obj.AuthorID);
                this.ddlNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(this.ddlNgonNgu, _obj.Languages_ID.ToString());
                this.ddlCategorys.Items.Clear();
                if (ddlNgonNgu.SelectedIndex > 0)
                {
                    UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by Ten_ChuyenMuc ASC");
                    this.ddlCategorys.UpdateAfterCallBack = true;
                    this.ddlCategorys.SelectedIndex = CommonLib.GetIndexControl(this.ddlCategorys, _obj.Category.ToString());
                }
                else
                {
                    this.ddlCategorys.DataSource = null;
                    this.ddlCategorys.DataBind();
                    this.ddlCategorys.UpdateAfterCallBack = true;
                }
                this.txt_tacgia.Text = _obj.Tacgia;
                this.txtGhichu.Text = _obj.Comment;
                //try { ddl_chatluong.SelectedIndex = _obj.Chatluong; }
                //catch { ;}

            }
        }
        private void ClearInputText()
        {
            this.txtTitle.Text = string.Empty;
            this.txtThumbnail.Text = string.Empty;
            this.txtVideoPath.Text = string.Empty;
            this.Txt_Desc.Text = string.Empty;
            this.txt_tacgia.Text = string.Empty;
            this.txtGhichu.Text = string.Empty;
            this.ddlNgonNgu.SelectedIndex = 0;
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
