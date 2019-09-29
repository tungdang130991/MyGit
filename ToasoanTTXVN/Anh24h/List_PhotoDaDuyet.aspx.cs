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
using HPCShareDLL;

namespace ToasoanTTXVN.Anh24h
{
    public partial class List_PhotoDaDuyet : BasePage
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
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        LoadData(this.txtPageIndex.Text.Trim());
                    }
                }
            }
        }
        #region Methods
        protected void ActiverPermission()
        {
            this.LinkNgungDang.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn ngừng đăng ảnh ?','ctl00_MainContent_LinkNgungDang');");
            this.LinkNgungDang1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn ngừng đăng ảnh ?','ctl00_MainContent_LinkNgungDang1');");
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
        }
        public void LoadData(string PagesTextbox)
        {
            int PagesIndex = 0;
            bool boolCheckPages = true;
            if (PagesTextbox.Length > 0 && UltilFunc.IsNumeric(PagesTextbox.ToString()))
            {
                if (Convert.ToInt32(PagesTextbox) >= 1)
                    PagesIndex = Convert.ToInt32(PagesTextbox) - 1;
                else boolCheckPages = false;
            }
            else boolCheckPages = false;
            string where = " 1=1 and Photo_Status=3 ";//AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += " AND " + string.Format(" Photo_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            if (cboNgonNgu.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            if (txt_FromDate.Text.Length > 0 && txt_ToDate.Text.Length == 0)
                where += " and Date_Create>='" + txt_FromDate.Text + " 00:00:01'";
            else if (txt_FromDate.Text.Length == 0 && txt_ToDate.Text.Length > 0)
                where += " AND Date_Update < ='" + txt_ToDate.Text.Trim() + " 23:59:59'";
            else if (txt_FromDate.Text.Length > 0 && txt_ToDate.Text.Length > 0)
                where += " and Date_Update>='" + txt_FromDate.Text + " 00:00:01' and Date_Create<='" + txt_ToDate.Text + " 23:59:59'";
            where += " Order by Date_Update DESC";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.T_Photo_EventDAL _cateDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            DataSet _ds;
            if (boolCheckPages)
                _ds = _cateDAL.BindGridT_Photo_Events(PagesIndex, pages.PageSize, where);
            else _ds = _cateDAL.BindGridT_Photo_Events(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
            {
                if (boolCheckPages)
                    _ds = _cateDAL.BindGridT_Photo_Events(PagesIndex - 1, pages.PageSize, where);
                else _ds = _cateDAL.BindGridT_Photo_Events(pages.PageIndex - 1, pages.PageSize, where);
            }
            grdListCate.DataSource = _ds;
            grdListCate.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            if (boolCheckPages)
                curentPages.PageIndex = PagesIndex;
            else curentPages.PageIndex = pages.PageIndex;
        }
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

        #endregion

        #region Event Click
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            this.LoadData("");
        }
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            if (this.txtPageIndex.Text.Trim().Length > 0 && UltilFunc.IsNumeric(this.txtPageIndex.Text.Trim()))
            {
                if (Convert.ToInt32(this.txtPageIndex.Text.Trim()) >= 1)
                    pages.PageIndex = Convert.ToInt32(this.txtPageIndex.Text.Trim()) - 1;
            }
            this.LoadData(this.txtPageIndex.Text.Trim());
        }
        protected void LinkNgungDang_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.T_Photo_EventDAL _untilDAL = new HPCBusinessLogic.T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            T_Photo_EventDAL DAL = new T_Photo_EventDAL();
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                TextBox txtGhichu = (TextBox)m_Item.FindControl("txtGhichu");
                TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
                if (chk_Select != null && chk_Select.Checked)
                {
                    double id = double.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    _obj = DAL.GetOneFromT_Photo_EventsByID(id);
                    //ghi chu
                    T_Photo_Event _objNew = new T_Photo_Event();
                    _objNew = setItem(_obj.Photo_ID, _obj.Photo_Medium, _obj.Photo_Name, _obj.Lang_ID, _obj.Author_Name, txt_tienNB.Text, txtGhichu.Text, _obj.Photo_Status);
                    int _return = _untilDAL.InsertT_Photo_Events(_objNew);
                    //string _ActionsCode1 = "[Thời sự qua ảnh] [Duyệt ảnh thời sự] [Cập nhật ảnh] [Ảnh: " + _obj.Photo_Name + "]";
                    //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode1, _return, ConstAction.TSAnh);
                    // Update on Server Destinations
                    _untilDAL.UpdateStatus_Photo_Events(id, 2, _user.UserID, DateTime.Now);
                    string _ActionsCode = "[Thời sự qua ảnh] [Ngừng đăng ][Ảnh: " + _obj.Photo_Name + "";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Ngừng đăng]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, id, ConstAction.TSAnh);
                }
            }
            LoadData(this.txtPageIndex.Text.Trim());
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
                T_Photo_Event _obj = new T_Photo_Event();

                #region "Duyet danh sach cac doi tuong tren luoi"
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    TextBox txt_tienNB = m_Item.FindControl("txt_tienNB") as TextBox;
                    double _ID = Convert.ToDouble(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
                    int tien = 0;
                    if (!string.IsNullOrEmpty(txt_tienNB.Text))
                    {
                        try { tien = int.Parse(txt_tienNB.Text.Replace(",", "")); }
                        catch { ;}
                    }
                    if (txt_tienNB.Text.Trim().Length > 0)
                    {
                        string sql = "Update T_Photo_Event set TienNB = " + tien + " where Photo_ID = " + _ID;
                        HPCDataProvider.Instance().ExecSql(sql);
                        string _ActionsCode = "[Thời sự qua ảnh] [Xuất bản ảnh thời sự] [Chấm nhuận bút ảnh trong ngày] [Ảnh: " + _obj.Photo_Name + "]";
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _ID, ConstAction.TSAnh);
                        
                    }
                }

                LoadData(this.txtPageIndex.Text.Trim());
                this.litMessages.Text = "Chấm nhuận bút thành công";
                #endregion
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }
        }
        private T_Photo_Event setItem(double PhotoID, string urlImage, string PhotoTitle, double LangId, string tacgia, string tienNB, string ghichu, double status)
        {
            T_Photo_Event _objPoto = new T_Photo_Event();
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(tacgia))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = tacgia.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _objPoto.AuthorID = butdanhID;
            int tien = 0;
            if (!string.IsNullOrEmpty(tienNB))
            {
                try { tien = int.Parse(tienNB.Replace(",", "")); }
                catch { ;}
            }
            _objPoto.Photo_ID = PhotoID;
            //_objPoto = _DAL.GetOneFromT_Photo_EventsByID(PhotoID);
            _objPoto.Date_Update = DateTime.Now;
            _objPoto.Photo_Name = PhotoTitle;
            _objPoto.Photo_Medium = urlImage;
            _objPoto.Author_Name = tacgia;
            _objPoto.TienNB = tien;
            _objPoto.Lang_ID = LangId;
            _objPoto.Creator = _user.UserID;
            _objPoto.Photo_Status = status;
            _objPoto.Photo_Desc = ghichu;
            return _objPoto;
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData("");
        }
        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            T_Photo_EventDAL _DAL = new T_Photo_EventDAL();
            T_Photo_Event _obj = new T_Photo_Event();
            TextBox txt_tienNB = e.Item.FindControl("txt_tienNB") as TextBox;
            int _ID = Convert.ToInt32(grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            _obj = _DAL.GetOneFromT_Photo_EventsByID(_ID);
            if (e.CommandArgument.ToString().ToLower() == "savephoto")
            {
                int tien = 0;
                if (!string.IsNullOrEmpty(txt_tienNB.Text))
                {
                    try { tien = int.Parse(txt_tienNB.Text.Replace(",", "")); }
                    catch { ;}
                }
                if (txt_tienNB.Text.Trim().Length > 0)
                {
                    string sql = "Update T_Photo_Event set TienNB = " + tien + " where Photo_ID = " + _ID;
                    HPCDataProvider.Instance().ExecSql(sql);
                    string _ActionsCode = "[Thời sự qua ảnh] [Xuất bản ảnh thời sự] [Chấm nhuận bút ảnh trong ngày] [Ảnh: " + _obj.Photo_Name + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Convert.ToInt32(Request["Menu_ID"]), _ActionsCode, _ID, ConstAction.TSAnh);
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn đã chấm nhuận bút thành công !');", true);
                }
                else
                {
                    txt_tienNB.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('Bạn chưa nhập tiêu đề ảnh !');", true);
                }
            }
        }
        #endregion

        //#region SYNC
        //private void SynData_UpdataStatusT_Photo_EventAny(string strwhere, ArrayList _arr)
        //{
        //    if (_arr.Count > 0)
        //    {
        //        for (int i = 0; i < _arr.Count; i++)
        //        {
        //            SynData_UpdataStatusT_Photo_Event(strwhere, _arr[i].ToString());
        //        }
        //    }
        //}
        //private void SynData_UpdataStatusT_Photo_Event(string strwhere, string urlService)
        //{
        //    if (urlService.Length > 0)
        //    {
        //        string _sql = "[Syn_UpdateStatus_T_Photo_Event]";
        //        PutDataBusinessLogic.UltilFunc _untilDAL = new PutDataBusinessLogic.UltilFunc(urlService);
        //        try
        //        {
        //            _untilDAL.ExecStore(_sql, new string[] { "@WhereCondition" }, new object[] { strwhere });
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        //#endregion end
    }
}
