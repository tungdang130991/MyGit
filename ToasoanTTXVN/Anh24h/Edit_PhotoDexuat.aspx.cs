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
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using System.IO;
using System.Collections.Generic;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Anh24h
{
    public partial class Edit_PhotoDexuat : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        int tab = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    this.LinkDanganh.Attributes.Add("onclick", "return confirm(\"Bạn có chắc muốn gửi duyệt?\");");
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                    }
                }
            }
        }

        #region Event Click
        protected string IpAddress()
        {
            string strIp;
            strIp = Page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = Page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            HPCInfo.T_Photo_Event _Obj = GetObject();
            if (_Obj.Photo_ID == 0)
            {
                int _return = _untilDAL.InsertT_Photo_Events(_Obj);
                string _ActionsCode = "[Thời sự qua ảnh] [Thêm mới ảnh] [Ảnh: " + _untilDAL.GetOneFromT_Photo_EventsByID(Convert.ToDouble(_return.ToString())).Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Thêm mới]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
            }
            else
            {
                int _return = _untilDAL.InsertT_Photo_Events(_Obj);
                string _ActionsCode = "[Thời sự qua ảnh] [Cập nhật ảnh] [Ảnh: " + _Obj.Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
            }
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
        }
        protected void LinkDanganh_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            #region SYNC
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            T_Photo_Event _Obj = GetObject();
            if (Request["id"] == null)
            {
                int _return = _untilDAL.InsertT_Photo_Events(_Obj);
                _untilDAL.UpdateStatus_Photo_Events(Convert.ToDouble(_return.ToString()), 8, _user.UserID, DateTime.Now);

            }
            else
            {
                _untilDAL.InsertT_Photo_Events(_Obj);
                _untilDAL.UpdateStatus_Photo_Events(_Obj.Photo_ID, 8, _user.UserID, DateTime.Now);

            }
            #endregion
            string _ActionsCode = "[Thời sự qua ảnh] [Gửi duyệt Ảnh] [Ảnh: " + _Obj.Photo_Name + "]";
            //UltilFunc.WriteLogActionHistory(_user.UserID, _user.UserFullName, IpAddress(), _ActionsCode, 0, "[Gửi duyệt]", Convert.ToInt32(Request["Menu_ID"]));
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosDexuat.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
        }
        #endregion

        #region User-methods
        private HPCInfo.T_Photo_Event GetObject()
        {
            HPCInfo.T_Photo_Event _objPoto = new HPCInfo.T_Photo_Event();
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            if (Page.Request.Params["id"] != null)
            {
                _objPoto.Photo_ID = int.Parse(Page.Request["id"].ToString());
                _objPoto = _DAL.GetOneFromT_Photo_EventsByID(double.Parse(Page.Request["id"].ToString()));
                _objPoto.Date_Update = DateTime.Now;
            }
            else
            {
                _objPoto.Photo_ID = 0;
                _objPoto.Date_Update = DateTime.Now;
            }
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(txt_Authod_Name.Text))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = txt_Authod_Name.Text.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _objPoto.AuthorID = butdanhID;
            _objPoto.Photo_Name = txt_Abl_Photo_Name.Text;
            _objPoto.Photo_Medium = UrlPathImage_RemoveUpload(txtThumbnail.Text);
            //_objPoto.File_Size = this.txt_Dungluong.Text.Trim();
            //if (txt_Dungluong.Text.Length > 0)
            //    if (UltilFunc.IsNumeric(this.txt_Dungluong.Text.Trim()))
            //        _objPoto.FileSquare = this.txt_Dungluong.Text.Trim();
            //_objPoto.File_Type = txt_loaifile.Text;
            _objPoto.Author_Name = txt_Authod_Name.Text;
            _objPoto.Lang_ID = Convert.ToInt32(cboNgonNgu.SelectedValue);
            _objPoto.Date_Create = DateTime.Now;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Desc = txtGhichu.Text;
            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }
            if (tab == 0)
                _objPoto.Photo_Status = 5;
            else if (tab == -1)
                _objPoto.Photo_Status = 5;
            else if (tab == 1)
                _objPoto.Photo_Status = 7;
            _objPoto.Copy_From = 0;
            return _objPoto;
        }
        public string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"].ToString(), "");
        }
        private void PopulateItem(int _id)
        {
            HPCInfo.T_Photo_Event _Obj = new HPCInfo.T_Photo_Event();
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            _Obj = _untilDAL.GetOneFromT_Photo_EventsByID(_id);
            if (_Obj != null)
            {
                this.txt_Abl_Photo_Name.Text = _Obj.Photo_Name.ToString();
                this.txtThumbnail.Text = _Obj.Photo_Medium.ToString();
                if (_Obj.Photo_Thumnail.Length > 2)
                    this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + _Obj.Photo_Thumnail;
                this.txt_Authod_Name.Text = _Obj.Author_Name.ToString();              
                this.cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, _Obj.Lang_ID.ToString());
                this.txtGhichu.Text = _Obj.Photo_Desc.ToString();
            }
        }

        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                this.lblTitleCaption.Text = "Cập nhật ảnh";
                HPCBusinessLogic.T_Photo_EventDAL _DAL = new HPCBusinessLogic.T_Photo_EventDAL();
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
            else
            {
                this.lblTitleCaption.Text = "Thêm mới ảnh";
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                if (cboNgonNgu.Items.Count == 2)
                {
                    cboNgonNgu.SelectedIndex = 1;
                }
                else cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
            }
        }

        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
        }
        private void checkpage()
        {
            if (this.txt_Abl_Photo_Name.Text.Length == 0 || this.txtThumbnail.Text.Length == 0)
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn chưa chọn những phần bắt buộc thì sao dịch được!');", true);

        }
        #endregion

    }
}
