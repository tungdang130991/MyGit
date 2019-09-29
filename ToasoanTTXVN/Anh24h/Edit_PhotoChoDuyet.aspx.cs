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
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.Anh24h
{
    public partial class Edit_PhotoChoDuyet : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        int tab = 0;
        int cat_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    this.LinkDanganh.Attributes.Add("onclick", "return confirm(\"Bạn có chắc đăng ảnh này?\");");
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                        //txt_PeopleCreator.Text = HPCBusinessLogic.UltilFunc.GetUserName(_user.UserID);
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
            T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
            if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            {
                try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
                catch
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                    return;
                }
            }
            T_Photo_Event _Obj = GetObject();
            int _return = 0;
            if (_Obj.Photo_ID == 0)
            {
                _return = _untilDAL.InsertT_Photo_Events(_Obj);
                string _ActionsCode = "[Thời sự qua ảnh] [Thao tác Thêm] [Ảnh: " + _Obj.Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Thêm mới]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
            }
            else
            {
                _return = _untilDAL.InsertT_Photo_Events(_Obj);
                string _ActionsCode = "[Thời sự qua ảnh] [Cập nhật ảnh trong ngày] [Ảnh: " + _Obj.Photo_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
            }
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
        }
        protected void LinkDanganh_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            #region SYNC
            T_Photo_EventDAL _untilDAL = new T_Photo_EventDAL();
            if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            {
                try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
                catch
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                    return;
                }
            }
            T_Photo_Event _Obj = GetObject();
            int _return = 0;
            if (Request["id"] == null)
            {
                _return = _untilDAL.InsertT_Photo_Events(_Obj);
                _untilDAL.UpdateStatus_Photo_Events(Convert.ToDouble(_return.ToString()), 3, _user.UserID, DateTime.Now);

            }
            else
            {
                _return = _untilDAL.InsertT_Photo_Events(_Obj);
                _untilDAL.UpdateStatus_Photo_Events(_Obj.Photo_ID, 3, _user.UserID, DateTime.Now);

            }
            #endregion
            // DONG BO ANH
            try
            {
                SynFiles _syncfile = new SynFiles();
                _Obj = _untilDAL.GetOneFromT_Photo_EventsByID(_return);
                if (_Obj.Photo_Medium.Length > 0)
                {
                    _syncfile.SynData_UploadImgOne(_Obj.Photo_Medium, HPCComponents.Global.ImagesService);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
            //END
            string _ActionsCode = "[Thời sự qua ảnh] [Duyệt ảnh] [Ảnh: " + _Obj.Photo_Name + "]";
            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Duyệt]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _return, ConstAction.TSAnh);
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
            
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            if (Page.Request["Tab"].ToString() != "-1")
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else return;
            }
            else
            {
                if (Page.Request["Menu_ID"] != null)
                    Page.Response.Redirect("~/Anh24h/List_PhotosChoDuyet.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                else return;
            }
        }
        #endregion

        #region User-methods
        private T_Photo_Event GetObject()
        {
            T_Photo_Event _objPoto = new T_Photo_Event();
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
            _objPoto.Author_Name = txt_Authod_Name.Text;
            int tien = 0;
            if (this.txtTienNhuanBut.Text.Trim().Length > 0)
            {
                tien = int.Parse(txtTienNhuanBut.Text.Replace(",", ""));
                if (tien > 0)
                    _objPoto.TienNB = tien;
                else
                    _objPoto.TienNB = 0;
            }
            _objPoto.Lang_ID = Convert.ToInt32(cboNgonNgu.SelectedValue);
            _objPoto.Date_Update = DateTime.Now;
            _objPoto.Photo_Desc = txtGhichu.Text;
            //_objPoto.Photo_Name = txt_Abl_Photo_Name.Text;
            //_objPoto.Photo_Medium = UrlPathImage_RemoveUpload(txtThumbnail.Text);
            //_objPoto.Author_Name = txt_Authod_Name.Text;
            //_objPoto.Lang_ID = Convert.ToInt32(cboNgonNgu.SelectedValue);

            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }
            if (tab == 0)
                _objPoto.Photo_Status = 8;
            else if (tab == -1)
                _objPoto.Photo_Status = 8;
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
                if (_Obj.TienNB > 0.0)
                    this.txtTienNhuanBut.Text = string.Format("{0:#,#}", _Obj.TienNB).Replace(".", ",");
                this.cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, _Obj.Lang_ID.ToString());
                this.txtGhichu.Text = _Obj.Photo_Desc.ToString();
            }
            //HPCInfo.T_Photo_Event _Obj = new HPCInfo.T_Photo_Event();
            //HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            //_Obj = _untilDAL.GetOneFromT_Photo_EventsByID(_id);
            //if (_Obj != null)
            //{
            //    txt_Abl_Photo_Name.Text = _Obj.Photo_Name.ToString();
            //    txtThumbnail.Text = _Obj.Photo_Medium.ToString();
            //    cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, _Obj.Lang_ID.ToString());
            //    this.txt_Authod_Name.Text = _Obj.Author_Name.ToString();
            //    if (_Obj.Photo_Thumnail.Length > 2)
            //        this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + _Obj.Photo_Thumnail;
            //}
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                HPCBusinessLogic.T_Photo_EventDAL _DAL = new HPCBusinessLogic.T_Photo_EventDAL();
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
            else
            {
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
            if (txt_Abl_Photo_Name.Text == "" || txtThumbnail.Text == "")
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn chưa chọn những phần bắt buộc thì sao dịch được!');", true);

        }
        #endregion

    }
}
