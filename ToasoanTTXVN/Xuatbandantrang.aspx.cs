using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
namespace ToasoanTTXVN
{
    public partial class Xuatbandantrang : System.Web.UI.Page
    {

        public string Tieude;
        public string Noidung;
        public string Chuyenmuc;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {

            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);

            if (!IsPostBack)
            {
                if (Session["MaAnPham"] == null)
                {
                    Page.Response.Redirect("~/login.aspx", true);
                }
                PanelLisTin.Visible = true;
                PnlDetail.Visible = false;
                if (Session["MaAnPham"] != null)
                {

                    UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", "---Tất cả---");
                    cboSoBao.Items.Clear();
                    bool Flag = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Flag"].ToString());
                    if (!Flag)
                    {

                        cboAnPham.SelectedValue = Session["MaAnPham"].ToString();
                        if (cboAnPham.SelectedIndex > 0)
                        {
                            UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 0);

                            bintrang(int.Parse(cboAnPham.SelectedValue));
                        }
                        else
                        {

                            cboSoBao.DataSource = null;
                            cboSoBao.DataBind();

                        }
                        
                    }


                }
            }
        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboTrang.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cboTrang.Items.Add(new ListItem("--Tất cả-- ", "0"));
                for (int i = 1; i < _sotrang + 1; i++)
                {
                    cboTrang.Items.Add(new ListItem("Trang " + i.ToString(), i.ToString()));
                }

            }

        }
        private void bintrangTheoSoBao(int _sobao)
        {
            UltilFunc ulti = new UltilFunc();
            cboTrang.Items.Clear();

            if (_sobao > 0)
            {
                string sql = "select Trang from T_Layout_SoBao where Ma_SoBao=" + _sobao;
                DataTable dt = ulti.ExecSqlDataSet(sql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    cboTrang.Items.Add(new ListItem("--Tất cả-- ", "0"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cboTrang.Items.Add(new ListItem("Trang " + dt.Rows[i][0].ToString(), (i + 1).ToString()));
                    }
                }
            }

        }

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {

                return " Ma_Tinbai DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sWhere = " Trangthai_Xoa=0 and Trangthai=" + status;

            if (cboAnPham.SelectedIndex != 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Anpham=" + cboAnPham.SelectedValue;
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            }

            if (cboTrang.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";

                sWhere += "  Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboTrang.SelectedValue.ToString() + ")";
            }

            return sWhere + sOrder;
        }
        private void LoadData_ChoXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, BuildSQL(1, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(1, sOrder));
            dgr_tintuc.DataSource = _ds;
            dgr_tintuc.DataBind();


            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;

        }
        public bool CheckImageNews(Object id)
        {
            UltilFunc ulti = new UltilFunc();
            bool check = false;
            string sql = "";
            if (int.Parse(id.ToString()) > 0)
            {
                sql = "select Ma_Anh from T_Tinbai_Anh where Ma_TinBai=" + id.ToString();
                DataTable dt = ulti.ExecSqlDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0 && dt.Rows[0]["Ma_Anh"].ToString() != "")
                    check = true;
                else
                    check = false;
            }
            return check;
        }

        #region Event Click

        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {

            cboSoBao.Items.Clear();
            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 0);
                bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
            }
            else
            {
                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            PnlDetail.Visible = false;
            PanelLisTin.Visible = true;
            this.LoadData_ChoXuly();

        }

        public void pages_IndexChanged_baichoxuly(object sender, EventArgs e)
        {
            LoadData_ChoXuly();

        }
        protected void cboTrang_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PnlDetail.Visible = false;
            PanelLisTin.Visible = true;
            LoadData_ChoXuly();
        }
       
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {

            double _ID = double.Parse(dgr_tintuc.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                PnlDetail.Visible = true;
                PanelLisTin.Visible = false;
                ViewState["id"] = _ID;
                T_TinBai obj = new T_TinBai();
                obj = Daltinbai.load_T_news(_ID);
                Chuyenmuc = dalcm.GetOneFromT_ChuyenmucByID(obj.Ma_Chuyenmuc).Ten_ChuyenMuc;
                Tieude = obj.Tieude.ToString();
                Noidung = obj.Noidung.ToString();
                BindGridPhotosByMatin();
            }

        }
        public void BindGridPhotosByMatin()
        {
            TinBaiAnhDAL _DAL = new TinBaiAnhDAL();
            DataSet _ds = _DAL.ListPhotoByMatinbai(" Ma_TinBai =" + ViewState["id"].ToString());
            DataView _dv = BindGridPhotoOfNews(_ds.Tables[0], int.Parse(ViewState["id"].ToString()));
            if (_ds != null)
            {
                this.DataListAnh.DataSource = _dv;
                this.DataListAnh.DataBind();
            }
        }
        public DataView BindGridPhotoOfNews(DataTable _dt, int _ma_tin)
        {

            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("Ma_Tin", typeof(string)));
                dt.Columns.Add(new DataColumn("Ma_Anh", typeof(string)));
                dt.Columns.Add(new DataColumn("TieuDe", typeof(string)));
                dt.Columns.Add(new DataColumn("Chuthich", typeof(string)));
                dt.Columns.Add(new DataColumn("Duongdan_Anh", typeof(string)));
                dt.Columns.Add(new DataColumn("Sotien", typeof(string)));
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _ma_tin.ToString().Trim();
                        dr[1] = _dt.Rows[i]["Ma_Anh"].ToString();
                        if (_ma_tin != 0)
                        {
                            dr[2] = UltilFunc.GetTieude_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()));
                            if (UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 1) != "")
                                dr[3] = UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 1);
                            else
                                dr[3] = UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 2);
                            dr[4] = UltilFunc.GetPathPhoto_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()),1);
                        }
                        else
                        {
                            dr[2] = _dt.Rows[i]["TieuDe"].ToString();
                            dr[3] = _dt.Rows[i]["Chuthich"].ToString();
                            dr[4] = _dt.Rows[i]["Duongdan_Anh"].ToString();
                        }
                        dr[5] = UltilFunc.GetNhuanbut_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), _ma_tin);

                        dt.Rows.Add(dr);
                    }
                }
                DataView dv = new DataView(dt);
                return dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        #endregion
    }
}
