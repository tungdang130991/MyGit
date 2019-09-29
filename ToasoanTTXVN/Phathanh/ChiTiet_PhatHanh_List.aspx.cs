﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Phathanh
{
    public partial class ChiTiet_PhatHanh_List : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_RolePermission _Role = null;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UltilFunc.IsNumeric(Request["Menu_ID"]))
            {
                if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                    Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                _Role = _userDAL.GetRole4UserMenu(_user.UserID, MenuID);
                this.btnAdd.Visible = _Role.R_Read;
                if (!IsPostBack)
                {
                    if (Session["CurrentPage"] != null)
                    {
                        pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                        Session["CurrentPage"] = null;
                        ChiTiet_PhatHanh();                       
                    }
                    else
                        ChiTiet_PhatHanh();
                    BindCombo();
                }
            }
        }
        #region Methods
        public void ChiTiet_PhatHanh()
        {
            string where = " 1=1 ";
            if (ddl_Anpham.SelectedIndex > 0)
                where += " AND Ma_Anpham =" + ddl_Anpham.SelectedValue.ToString();
            if (ddl_Sobao.SelectedIndex > 0)
                where += " AND Ma_Sobao =" + ddl_Sobao.SelectedValue.ToString();
            //if (!String.IsNullOrEmpty(this.txtTen_Khachhang.Text.Trim()))
            //    where += " AND " + string.Format(" Ten_KhachHang like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTen_Khachhang.Text.Trim()));

            pages.PageSize = Global.MembersPerPage;
            PhathanhDAL _phathanhDAL = new PhathanhDAL();
            DataSet _ds;
            _ds = _phathanhDAL.BindGridT_Phathanh(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _phathanhDAL.BindGridT_Phathanh(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds;
            grdList.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
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
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Read;
        }
        protected string TenAnpham(string _maAnpham)
        {
            string strReturn = "";
            T_AnPham _anpham;
            AnPhamDAL _anphamDAL = new AnPhamDAL();
            _anpham = _anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham));
            if (_anpham != null)
                strReturn = _anpham.Ten_AnPham;
            return strReturn;
        }
        public void BindCombo()
        {
            ddl_Anpham.Items.Clear();
            UltilFunc.BindCombox(ddl_Anpham, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ");           
        }
        #endregion

        #region Click
        protected void ddl_Anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Anpham.SelectedIndex > 0)
            {
                ddl_Sobao.Items.Clear();
                UltilFunc.BindCombox(ddl_Sobao, "Ma_Sobao", "Ten_Sobao", "T_Sobao", " Ma_AnPham = " + ddl_Anpham.SelectedValue.ToString());
                ddl_Sobao.UpdateAfterCallBack = true;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Phathanh/ChiTiet_PhatHanh_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            ChiTiet_PhatHanh();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            ChiTiet_PhatHanh();
        }

        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            PhathanhDAL objDAL = new PhathanhDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Phathanh/ChiTiet_PhatHanh_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                objDAL.DeleteOneFromT_Phathanh(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                action.Thaotac = "[Xóa T_PhatHanh]-->[Thao tác Xóa Trong Bảng T_PhatHanh][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                this.ChiTiet_PhatHanh();
            }
        }
        #endregion
    }
}
