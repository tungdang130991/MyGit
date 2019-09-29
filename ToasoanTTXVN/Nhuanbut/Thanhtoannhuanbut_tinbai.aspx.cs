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

namespace ToasoanTTXVN.Nhuanbut
{
    public partial class Thanhtoannhuanbut_tinbai : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        int Ma_QTBT = 0;
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
                    Ma_QTBT = UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + _user.UserID);
                    if (!IsPostBack)
                    {
                        if (Ma_QTBT == 0)
                        {
                            Page.Response.Redirect("~/login.aspx", true);
                        }
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

                        LoadData();
                    }
                }
            }

        }
        public void LoadCombox()
        {
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
                return "Ngaytao DESC";
            }
        }
        string BuildSQL(string sOrder)
        {

            string sWhere = " Trangthai_xoa=0 and Doituong_DangXuly=N'" + Global.MaXuatBan + "'";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cboAnPham.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_AnPham=" + cboAnPham.SelectedValue;
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where T_Vitri_Tinbai.Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            }
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
            {
                if (UltilFunc.ToDate(txt_tungay.Text.Trim(), "dd/MM/yyyy") > UltilFunc.ToDate(txt_denngay.Text.Trim(), "dd/MM/yyyy"))
                {
                    FuncAlert.AlertJS(this, "Từ ngày phải nhỏ hơn đến ngày");
                    return " and 1=0";
                }
                else
                {
                    if (sWhere.Trim() != "") sWhere += " AND ";
                    sWhere += " Ma_Sobao in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
                }
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();
            }
            if (txt_PVCTV.Text.Trim() == "")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Nhuanbut where Ma_tacgia=" + HiddenFieldTacgiatin.Value + ")";


            return sWhere + sOrder;
        }
        private void LoadData()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Sp_List_Thanhtoannhuanbuttinbai(pages.PageIndex, pages.PageSize, BuildSQL(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.Sp_List_Thanhtoannhuanbuttinbai(pages.PageIndex - 1, pages.PageSize, BuildSQL(sOrder));
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();


            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;


        }
        public void Listboxtrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        protected void btn_chamnhuanbut_click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in DataGrid_tinbai.Items)
            {
                TextBox txtsotientinbai = (TextBox)m_Item.FindControl("txtsotientinbai");
                TextBox txt_ghichu = (TextBox)m_Item.FindControl("txt_ghichu");
                Label lblMaTacGia = (Label)m_Item.FindControl("lblMaTacGia");
                Label lblsotientinbai = (Label)m_Item.FindControl("lblsotientinbai");
                Label LabelGhichu = (Label)m_Item.FindControl("LabelGhichu");
                double id = double.Parse(DataGrid_tinbai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                if (txtsotientinbai.Text.Trim() != "" && lblMaTacGia.Text.Trim() != "")
                    Daltinbai.Sp_UpdateT_NhuanButTinbai(id, double.Parse(lblMaTacGia.Text), double.Parse(txtsotientinbai.Text.Trim().Replace(",", "")), 0, txt_ghichu.Text.Trim(), _user.UserID);
                lblsotientinbai.Text = txtsotientinbai.Text.Trim();
                LabelGhichu.Text = txt_ghichu.Text.Trim();
                lblsotientinbai.Visible = true;
                LabelGhichu.Visible = true;
                txtsotientinbai.Visible = false;
                txt_ghichu.Visible = false;
            }

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
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
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
            double id = Convert.ToInt32(DataGrid_tinbai.DataKeys[e.Item.ItemIndex].ToString());
            TextBox txtsotientinbai = (TextBox)e.Item.FindControl("txtsotientinbai");
            TextBox txt_ghichu = (TextBox)e.Item.FindControl("txt_ghichu");
            Label lblsotientinbai = (Label)e.Item.FindControl("lblsotientinbai");
            Label LabelGhichu = (Label)e.Item.FindControl("LabelGhichu");
            Label lblMaTacGia = (Label)e.Item.FindControl("lblMaTacGia");
            ImageButton btnThanhtoan = (ImageButton)e.Item.FindControl("btnThanhtoan");
            ImageButton btnUpdate = (ImageButton)e.Item.FindControl("btnUpdate");
            ImageButton btnCancel = (ImageButton)e.Item.FindControl("btnCancel");
            if (e.CommandArgument.ToString().ToLower() == "editthanhtoan")
            {

                txtsotientinbai.Visible = true;
                txt_ghichu.Visible = true;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                btnThanhtoan.Visible = false;
                lblsotientinbai.Visible = false;
                LabelGhichu.Visible = false;

            }
            if (e.CommandArgument.ToString().ToLower() == "capnhat")
            {

                txtsotientinbai.Visible = false;
                txt_ghichu.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                btnThanhtoan.Visible = true;
                lblsotientinbai.Visible = true;
                LabelGhichu.Visible = true;
                if (txtsotientinbai.Text.Trim() != "")
                    Daltinbai.Sp_UpdateT_NhuanButTinbai(id, double.Parse(lblMaTacGia.Text), double.Parse(txtsotientinbai.Text.Trim().Replace(",", "")), 0, txt_ghichu.Text.Trim(), _user.UserID);
                lblsotientinbai.Text = txtsotientinbai.Text;
                LabelGhichu.Text = txt_ghichu.Text;
                LoadData();
            }
            if (e.CommandArgument.ToString().ToLower() == "cancel")
            {

                txtsotientinbai.Visible = false;
                txt_ghichu.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                btnThanhtoan.Visible = true;
                lblsotientinbai.Visible = true;
                LabelGhichu.Visible = true;

            }


        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

    }
}
