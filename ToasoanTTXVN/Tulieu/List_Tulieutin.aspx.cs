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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Collections.Generic;

namespace ToasoanTTXVN.Tulieu
{
    public partial class List_Tulieutin : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    if (!IsPostBack)
                    {
                        LoadCombox();
                        cbo_anpham.SelectedValue = Global.DefaultCombobox;
                        cbo_chuyenmuc.Items.Clear();

                        if (cbo_anpham.SelectedIndex > 0)
                        {
                            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_anpham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
                        }
                        else
                        {
                            cbo_chuyenmuc.DataSource = null;
                            cbo_chuyenmuc.DataBind();

                        }

                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbo_anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") ", CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
        }
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _Role.R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _Role.R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _Role.R_Read;
        }

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                return " Ngaytao DESC";
            }
        }
        string BuildSQL(string sOrder)
        {
            string sWhere = " 1=1";

            if (txt_tieude.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cbo_anpham.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Anpham=" + cbo_anpham.SelectedValue.ToString();
            }

            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ngaytao>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngaytao<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();
            }

            if (txt_PVCTV.Text.Trim() == "")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "" || txt_PVCTV.Text.Trim() != "")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_TacGia=" + HiddenFieldTacgiatin.Value;
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  TacGia LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_PVCTV.Text.Trim()));
            }

            return sWhere + sOrder;
        }
        private void LoadData()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_Tulieu(pages.PageIndex, pages.PageSize, BuildSQL(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_Tulieu(pages.PageIndex - 1, pages.PageSize, BuildSQL(sOrder));
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;

        }
        protected void cbo_anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();


            if (cbo_anpham.SelectedIndex > 0)
            {
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_anpham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
            }

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
    }
}
