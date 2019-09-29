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
    public partial class Taichinhthanhtoannhuanbut_Tinbai : BasePage
    {

        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
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
                    this.btn_chamnhuanbut.Attributes.Add("onclick", "return CheckConfirmGuiTinbai('Bạn có chắc chắn muốn thanh toán nhuận bút tin bài?',ctl00_MainContent_DataGrid_tinbai_ctl01_chkAll);");
                    if (!IsPostBack)
                    {
                        LoadCombox();
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

                        LoadData();
                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") ", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
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
            string sWhere = "  Trangthai_xoa=0 and Doituong_DangXuly=N'" + Global.MaXuatBan + "' ";

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
                sWhere += " Ma_Sobao=" + cboSoBao.SelectedValue;
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
        protected string IsThanhtoan(Object Thanhtoan)
        {
            string strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (Thanhtoan != DBNull.Value)
            {
                if (bool.Parse(Thanhtoan.ToString()) == true)
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/mark.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            return strReturn;
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
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                Label lblMaTacGia = (Label)m_Item.FindControl("lblMaTacGia");

                if (chk_Select != null && chk_Select.Checked)
                {
                    double id = double.Parse(DataGrid_tinbai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    string sql = "select THANHTOAN from T_NhuanBut where Ma_TinBai=" + id + " and  Ma_tacgia=" + lblMaTacGia.Text;
                    UltilFunc ulti = new UltilFunc();
                    DataTable dtthanhtoan = ulti.ExecSqlDataSet(sql).Tables[0];
                    bool check = false;
                    if (dtthanhtoan.Rows[0]["THANHTOAN"].ToString() != DBNull.Value.ToString())
                        check = (bool)dtthanhtoan.Rows[0]["THANHTOAN"];
                    if (check)
                        Daltinbai.Sp_UpdateThanhToanNhuanButTinBai(id, double.Parse(lblMaTacGia.Text), false, DateTime.Now, _user.UserID);
                    else
                        Daltinbai.Sp_UpdateThanhToanNhuanButTinBai(id, double.Parse(lblMaTacGia.Text), true, DateTime.Now, _user.UserID);
                }

            }
            LoadData();
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
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
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
            Label lblMaTacGia = (Label)e.Item.FindControl("lblMaTacGia");
            double id = Convert.ToInt32(DataGrid_tinbai.DataKeys[e.Item.ItemIndex].ToString());
            string sql = "select THANHTOAN from T_NhuanBut where Ma_TinBai=" + id + " and Ma_tacgia=" + lblMaTacGia.Text;
            UltilFunc ulti = new UltilFunc();
            DataTable dtthanhtoan = ulti.ExecSqlDataSet(sql).Tables[0];
            bool check = false;
            if (dtthanhtoan.Rows[0]["THANHTOAN"].ToString() != DBNull.Value.ToString())
                check = (bool)dtthanhtoan.Rows[0]["THANHTOAN"];

            if (e.CommandArgument.ToString().ToLower() == "editthanhtoan")
            {
                if (check)
                    Daltinbai.Sp_UpdateThanhToanNhuanButTinBai(id, double.Parse(lblMaTacGia.Text), false, DateTime.Now, _user.UserID);
                else
                    Daltinbai.Sp_UpdateThanhToanNhuanButTinBai(id, double.Parse(lblMaTacGia.Text), true, DateTime.Now, _user.UserID);
                LoadData();
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
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

    }
}
