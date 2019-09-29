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
namespace ToasoanTTXVN.TimKiem
{
    public partial class TimKiemTinBaiTrongQuyTrinh : BasePage
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
                        if (_user != null)
                        {
                            LoadCombox();

                            cbo_chuyenmuc.Items.Clear();
                            cboSoBao.Items.Clear();
                            if (cboAnPham.SelectedIndex > 0)
                            {
                                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
                                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
                                bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
                            }
                            else
                            {
                                cbo_chuyenmuc.DataSource = null;
                                cbo_chuyenmuc.DataBind();

                                cboSoBao.DataSource = null;
                                cboSoBao.DataBind();

                            }
                        }
                        else
                            Page.Response.Redirect("~/login.aspx", true);
                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") ", CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboPage.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonTrang"), "0", true));
                for (int j = 1; j < _sotrang + 1; j++)
                {
                    cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblTrang") + j.ToString(), j.ToString()));
                }
            }

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
        string BuildSql(string sOrder)
        {
            string sWhere = " 1=1 and Trangthai<>6";


            if (cboAnPham.SelectedIndex > 0)
                sWhere += " AND Ma_AnPham=" + cboAnPham.SelectedValue;

            if (cboSoBao.SelectedIndex > 0)
                sWhere += " AND Ma_Sobao=" + cboSoBao.SelectedValue;
            if (cboPage.SelectedIndex > 0)
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboPage.SelectedValue + ")";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                sWhere += " AND Ma_Sobao in(select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";

            if (cbo_chuyenmuc.SelectedIndex > 0)
                sWhere += " AND Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue;
            if (txt_PVCTV.Text.Trim() == "")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Nhuanbut where Ma_tacgia=" + HiddenFieldTacgiatin.Value + ")";


            return sWhere + sOrder;
        }
        private void LoadData()
        {
            try
            {
                string FulltextSearch = UltilFunc.ReplaceAll(txt_tieude.Text.Trim(), "'", "’");
                string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
                pages.PageSize = Global.MembersPerPage;
                HPCBusinessLogic.DAL.TinBaiDAL _NewsDal = new HPCBusinessLogic.DAL.TinBaiDAL();
                DataSet _ds;
                _ds = _NewsDal.BindGridT_NewsFullTeztSearch(pages.PageIndex, pages.PageSize, BuildSql(sOrder), UltilFunc.SplitString(FulltextSearch));
                int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
                if (TotalRecord == 0)
                    _ds = _NewsDal.BindGridT_NewsFullTeztSearch(pages.PageIndex - 1, pages.PageSize, BuildSql(sOrder), UltilFunc.SplitString(FulltextSearch));
                DataGrid_tinbai.DataSource = _ds;
                DataGrid_tinbai.DataBind();

                pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
                CurrentPage.TotalPages = pages.CalculateTotalPages();
                CurrentPage.PageIndex = pages.PageIndex;
            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();

            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");

            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }
            bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
        }
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
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
