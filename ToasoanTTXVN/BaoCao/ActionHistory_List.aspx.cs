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
using System.IO;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
//using Word;
using Microsoft.Office.Core;
using System.Text.RegularExpressions;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.BaoCao
{
    public partial class ActionHistory_List : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        public int type = 0;
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
                        LoadCombox();
                        type = int.Parse(cbo_types.SelectedValue);
                        if (type == 1)
                            LoadData();
                        else if (type == 2)
                            LoadGocAnh();
                        else if (type == 3)
                            LoadMutimedia();
                        else if (type == 4)
                            LoadThoiSuAnh();
                       
                    }
                }
            }
        }

        #region Method
        public void LoadCombox()
        {
            cbo_chuyenmuc.Items.Clear();
            cboNgonNgu.Items.Clear();
            UltilFunc.BindCombox(cboNgonNgu, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" hoatdong=1 AND ID IN ({0}) Order by ThuTu ASC ", UltilFunc.GetLanguagesByUser(_user.UserID)), CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
            {
                cboNgonNgu.SelectedIndex = HPCComponents.Global.DefaultLangID;
            }
            else
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
            if (cboNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.cboNgonNgu.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            cbo_chuyenmuc.UpdateAfterCallBack = true;
            cboNgonNgu.UpdateAfterCallBack = true;
        }

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cboNgonNgu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.cboNgonNgu.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
        private void LoadData()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txtTieude.Text.Trim()))
                where += " AND T_News.News_Tittle like N'%" + UltilFunc.SqlFormatText(this.txtTieude.Text.Trim()) + "%'";
            if (cbo_chuyenmuc.SelectedIndex > 0)
                where += " AND " + string.Format(" T_News.CAT_ID IN (SELECT * FROM [dbo].[fn_Return_Category_Tree] ({0}))", cbo_chuyenmuc.SelectedValue);
            if (!String.IsNullOrEmpty(this.txt_FromDate.Text.Trim()))
                where += " AND T_News.News_DatePublished  >='" + txt_FromDate.Text + " 00:00:01'";
            if (!String.IsNullOrEmpty(this.txt_ToDate.Text.Trim()))
                where += " AND T_News.News_DatePublished  <='" + txt_ToDate.Text + " 23:59:59'";
            pages.PageSize = 20;
            T_BaoCao _DAL = new T_BaoCao();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_ActionHistory(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds;
            grdList.DataBind(); _ds.Clear();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        private void LoadGocAnh()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txtTieude.Text.Trim()))
                where += " AND T_Album_Categories.Cat_Album_Name like N'%" + UltilFunc.SqlFormatText(this.txtTieude.Text.Trim()) + "%'";
            if (cboNgonNgu.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            if (cbo_chuyenmuc.SelectedIndex > 0)
                where += " AND " + string.Format(" T_Album_Categories.Cat_Album_CATID IN (SELECT * FROM [dbo].[fn_Return_Category_Tree] ({0}))", cbo_chuyenmuc.SelectedValue);
            if (!String.IsNullOrEmpty(this.txt_FromDate.Text.Trim()))
                where += " AND T_Album_Categories.Cat_Album_Status_Approve  >='" + txt_FromDate.Text + " 00:00:01'";
            if (!String.IsNullOrEmpty(this.txt_ToDate.Text.Trim()))
                where += " AND T_Album_Categories.Cat_Album_Status_Approve  <='" + txt_ToDate.Text + " 23:59:59'";
            pages.PageSize = 20;
            T_BaoCao _DAL = new T_BaoCao();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory_GocAnh(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_ActionHistory_GocAnh(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds;
            grdList.DataBind(); _ds.Clear();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        private void LoadMutimedia()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txtTieude.Text.Trim()))
                where += " AND T_Multimedia.Tittle like N'%" + UltilFunc.SqlFormatText(this.txtTieude.Text.Trim()) + "%'";
            if (cboNgonNgu.SelectedIndex > 0)
                where += " AND Languages_ID=" + cboNgonNgu.SelectedValue.ToString();
            if (cbo_chuyenmuc.SelectedIndex > 0)
                where += " AND " + string.Format(" T_Multimedia.Category IN (SELECT * FROM [dbo].[fn_Return_Category_Tree] ({0}))", cbo_chuyenmuc.SelectedValue);
            if (!String.IsNullOrEmpty(this.txt_FromDate.Text.Trim()))
                where += " AND T_Multimedia.DatePublish  >='" + txt_FromDate.Text + " 00:00:01'";
            if (!String.IsNullOrEmpty(this.txt_ToDate.Text.Trim()))
                where += " AND T_Multimedia.DatePublish  <='" + txt_ToDate.Text + " 23:59:59'";
            pages.PageSize = 20;
            T_BaoCao _DAL = new T_BaoCao();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory_AmThanhHinhAnh(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_ActionHistory_AmThanhHinhAnh(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds;
            grdList.DataBind(); _ds.Clear();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        private void LoadThoiSuAnh()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txtTieude.Text.Trim()))
                where += " AND T_Photo_Event.Photo_Name like N'%" + UltilFunc.SqlFormatText(this.txtTieude.Text.Trim()) + "%'";
            if (cboNgonNgu.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            if (!String.IsNullOrEmpty(this.txt_FromDate.Text.Trim()))
                where += " AND T_Photo_Event.Date_Update  >='" + txt_FromDate.Text + " 00:00:01'";
            if (!String.IsNullOrEmpty(this.txt_ToDate.Text.Trim()))
                where += " AND T_Photo_Event.Date_Update  <='" + txt_ToDate.Text + " 23:59:59'";
            pages.PageSize = 20;
            T_BaoCao _DAL = new T_BaoCao();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory_TsAnh(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_ActionHistory_TsAnh(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds;
            grdList.DataBind(); _ds.Clear();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        private void Load_Detail(double _id)
        {
            type = int.Parse(cbo_types.SelectedValue);
            T_BaoCao _DAL = new T_BaoCao();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory_Detail(_id, type);
            grdDetail.DataSource = _ds;
            grdDetail.DataBind();
            _ds.Clear();
        }
        public void SetAddEdit(bool isList, bool isEdit)
        {
            pnList.Visible = isList;
            pnView.Visible = isEdit;
        }
        protected string ImageStatus(string prmImgStatus)
        {
            type = int.Parse(cbo_types.SelectedValue);
            string strReturn = "";
            if (type == 1)
            {
                if (prmImgStatus == "6")
                    strReturn = Global.ApplicationPath + "/Dungchung/images/15200921245810.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/images/icons/edit.gif";
            }
            if (type == 2)
            {
                if (prmImgStatus == "4")
                    strReturn = Global.ApplicationPath + "/Dungchung/images/15200921245810.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/images/icons/edit.gif";
            }
            if (type == 3 || type == 4)
            {
                if (prmImgStatus == "3")
                    strReturn = Global.ApplicationPath + "/Dungchung/images/15200921245810.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/images/icons/edit.gif";
            }
            return strReturn;
        }
        protected string BindToolTip(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "6")
                strReturn = "Bài đã xuất bản";
            else
                strReturn = "Bài đang xử lý";
            return strReturn;
        }
        protected string GetNumberOfRead(object _Number)
        {
            string strReturn = "0";
            try
            {
                if (type == 1)
                {
                    if (_Number != null)
                    {
                        if (Convert.ToInt32(_Number.ToString()) >= 0)
                        {
                            strReturn = _Number.ToString();
                        }
                    }
                }
                else
                    strReturn = "";
            }
            catch { }
            return strReturn;
        }
        #endregion

        #region Event click
        protected void lbkSearch_Click(object sender, EventArgs e)
        {
            type = int.Parse(cbo_types.SelectedValue);
            if (type == 1)
                LoadData();
            else if (type == 2)
                LoadGocAnh();
            else if (type == 3)
                LoadMutimedia();
            else if (type == 4)
                LoadThoiSuAnh();
        }

        protected void lbkBack_OnClick(object sender, EventArgs e)
        {
            SetAddEdit(true, false);
        }
        protected void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                SetAddEdit(false, true);
                double _ID = Convert.ToDouble(grdList.DataKeys[e.Item.ItemIndex].ToString()); //dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();
                Load_Detail(_ID);
            }
        }
        protected void grdList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            Label lbSTT = (Label)e.Item.FindControl("lbSTT");
            if (lbSTT != null)
            {
                lbSTT.Text = (e.Item.ItemIndex + 1).ToString();
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void grdDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            type = int.Parse(cbo_types.SelectedValue);
            if (type == 1)
                LoadData();
            else if (type == 2)
                LoadGocAnh();
            else if (type == 3)
                LoadMutimedia();
            else if (type == 4)
                LoadThoiSuAnh();

        }
        #endregion
    }
}
