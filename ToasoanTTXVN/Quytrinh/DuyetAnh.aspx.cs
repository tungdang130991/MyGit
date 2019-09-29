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

namespace ToasoanTTXVN.Quytrinh
{
    public partial class DuyetAnh : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;

        AnhDAL _DalAnh = new AnhDAL();
        SSOLibDAL lib = new SSOLibDAL();
        UltilFunc Ulti = new UltilFunc();
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
                }
                if (!IsPostBack)
                {
                    UltilFunc.BindCombox(cbo_anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " 1=1", "---Chọn ấn phẩm---");
                    cbo_anpham.SelectedValue = Global.DefaultCombobox;
                    UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cbo_anpham.SelectedValue.ToString()), 0);
                    bintrang(int.Parse(cbo_anpham.SelectedValue.ToString()));
                }
            }
        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboPage.Items.Clear();

            if (_loaibao > 0)
            {
                cboPage.Items.Add("----Chọn trang----");
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                for (int i = 1; i < _sotrang + 1; i++)
                {
                    cboPage.Items.Add(new ListItem("Trang " + i.ToString(), i.ToString()));
                }
            }

        }
        public string GetWhere(int Duyet)
        {
            string _where = string.Empty;
            _where = " and Img.Duyet=" + Duyet;
            if (cbo_anpham.SelectedIndex > 0)
                _where += "AND TN.Ma_Anpham=" + cbo_anpham.SelectedValue;
            if (cboSoBao.SelectedIndex > 0)
                _where += " AND (TN.Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Ma_Sobao =" + cboSoBao.SelectedValue + ") OR TN.Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            if (cboPage.SelectedIndex > 0)
                _where += " AND TN.Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboPage.SelectedValue + ")";
            if (txt_tieude.Text.Trim() != "" && txt_tieude.Text.Trim() != "Nhập tiêu đề cần tìm")
                _where += " and TN.Tieude like N'%" + txt_tieude.Text.Trim() + "%'";
            if (txt_chuthich.Text.Trim() != "" && txt_chuthich.Text.Trim() != "Nhập chú thích cần tìm")
                _where += " and Img.Chuthich like N'%" + txt_chuthich.Text.Trim() + "%'";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " and Img.Ngaytao >='" + txt_tungay.Text.Trim() + " 00:00:00' and Img.Ngaytao<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            if (txt_PVCTV.Text.Trim() != "" && txt_PVCTV.Text.Trim() != "Nhập tác giả cần tìm")
                _where += " and Img.Nguoichup like N'%" + txt_PVCTV.Text.Trim().Replace(" -- ", "|").Split('|')[0] + "%'";
            if (txt_PVCTV.Text.Trim() == "Nhập tác giả cần tìm")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                _where += " and Img.Ma_Nguoichup=" + HiddenFieldTacgiatin.Value;
            _where += " Order by Img.Ngaytao DESC";
            return _where;
        }
        public DataTable LoadDataImageDuyetAnh()
        {
            DataTable _dt = _DalAnh.Sp_DuyetAnhDinhKem(GetWhere(1)).Tables[0];
            this.dgrListImages.DataSource = _dt;
            this.dgrListImages.DataBind();
            return _dt;
        }
        public DataTable LoadDataImageHuyAnh()
        {
            DataTable _dt = _DalAnh.Sp_DuyetAnhDinhKem(GetWhere(0)).Tables[0];
            this.DataListImgHuy.DataSource = _dt;
            this.DataListImgHuy.DataBind();
            return _dt;
        }
        public void GetTotalRecordImg()
        {
            string _imgduyet = "0", _imghuy = "0";

            _imgduyet = LoadDataImageDuyetAnh().Rows.Count.ToString();
            _imghuy = LoadDataImageHuyAnh().Rows.Count.ToString();

            this.TabPanelDuyetAnh.HeaderText = "Ảnh duyệt (" + _imgduyet + ")";
            this.TabPanelHuyDuyetAnh.HeaderText = "Ảnh hủy (" + _imghuy + ")";
        }
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn số báo!");
                return;
            }
            else
            {
                TabContainer1_ActiveTabChanged(sender, e);
                GetTotalRecordImg();
            }
        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn số báo!");
                return;
            }
            else
            {
                TabContainer1_ActiveTabChanged(sender, e);
                GetTotalRecordImg();
            }
        }
        public void dgrListImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            int _ID = 0;
            string _sql = string.Empty;
            if (TabContainer1.ActiveTabIndex == 0)
                _ID = Convert.ToInt32(dgrListImages.DataKeys[e.Item.ItemIndex].ToString());
            else
                _ID = Convert.ToInt32(DataListImgHuy.DataKeys[e.Item.ItemIndex].ToString());

            if (e.CommandArgument.ToString().ToLower() == "huyanh")
            {
                TextBox txt_nhanxet = e.Item.FindControl("txt_nhanxet") as TextBox;
                if (txt_nhanxet.Text.Trim() != "" && txt_nhanxet.Text.Trim() != "Nhận xét")
                    _sql = "update T_Anh set Duyet=0, nhanxet=N'" + txt_nhanxet.Text.Trim() + "' where Ma_Anh=" + _ID;
                else
                    _sql = "update T_Anh set Duyet=0 where Ma_Anh=" + _ID;
                Ulti.ExecSql(_sql);
                LoadDataImageDuyetAnh();
            }
            if (e.CommandArgument.ToString().ToLower() == "undo")
            {
                _sql = "update T_Anh set Duyet=1 where Ma_Anh=" + _ID;
                Ulti.ExecSql(_sql);
                LoadDataImageHuyAnh();
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
                LoadDataImageDuyetAnh();

            if (TabContainer1.ActiveTabIndex == 1)
                LoadDataImageHuyAnh();

        }
    }
}
