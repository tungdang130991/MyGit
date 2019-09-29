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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Baocaothongke
{
    public partial class Lichsuthaotactinbai : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc Ulti = new UltilFunc();
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
                        cbo_chuyenmuc.Items.Clear();
                        cboSoBao.Items.Clear();
                        if (cboAnPham.SelectedIndex > 0)
                        {
                            UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
                            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");

                        }
                        else
                        {
                            cbo_chuyenmuc.DataSource = null;
                            cbo_chuyenmuc.DataBind();

                            cboSoBao.DataSource = null;
                            cboSoBao.DataBind();

                        }


                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbodoituong, "Ma_Doituong", "Ten_Doituong", "T_Doituong", "1=1", CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") ", CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
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

            string sWhere = " Trangthai_Xoa=0 ";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "Nhập tiêu đề cần tìm")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cboAnPham.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Anpham=" + cboAnPham.SelectedValue.ToString();
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();
            }
            if (cbodoituong.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Doituong_DangXuly=N'" + cbodoituong.SelectedValue.ToString() + "'";
            }

            if (txt_PVCTV.Text.Trim() == "Nhập tác giả cần tìm")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                sWhere += " AND Ma_TacGia=" + HiddenFieldTacgiatin.Value;
            else if (txt_PVCTV.Text.Trim() != "" && txt_PVCTV.Text.Trim() != "Nhập tác giả cần tìm")
                sWhere += " AND TacGia LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_PVCTV.Text.Trim().Replace(" -- ", "|").Split('|')[0]));
            return sWhere + sOrder;
        }
        private void LoadData()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, BuildSQL(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(sOrder));
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;


        }


        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupThaotactinbai.Hide();
        }
        public void pages_IndexChanged_Trang(object sender, EventArgs e)
        {
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();


            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }


        }
        protected void DataGrid_tinbai_EditCommand(object source, DataGridCommandEventArgs e)
        {
            DataSet ds = new DataSet();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                HPCBusinessLogic.DAL.TinBaiDAL Dal = new HPCBusinessLogic.DAL.TinBaiDAL();
                string _matinbai = DataGrid_tinbai.DataKeys[e.Item.ItemIndex].ToString();
                string sqlselect = "select * from T_Lichsu_Thaotac_TinBai where Ma_TinBai=" + _matinbai + " order by NgayThaotac";
                ds = Ulti.ExecSqlDataSet(sqlselect);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataGridLichsuthaotactinbai.DataSource = ds;
                    DataGridLichsuthaotactinbai.DataBind();
                    ModalPopupThaotactinbai.Show();
                }

            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {

                LinkButton linkTittle = (LinkButton)e.Item.FindControl("linkTittle");
                if (!_Role.R_Write)
                    linkTittle.Enabled = _Role.R_Write;
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }


    }
}
