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

namespace ToasoanTTXVN.Dungchung
{
    public partial class Commons : BasePage
    {
        HPCBusinessLogic.DAL.TinBaiDAL _daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc _ulti = new UltilFunc();
        int Ma_QTBT = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);

            if (_user == null)
                Response.Redirect(Global.ApplicationPath + "/Login.aspx");
            else
            {               
                Ma_QTBT = UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + _user.UserID);
                BindGridTinBai();
                GetTotalRecordCV();
            }
        }

        #region Methods
        public void BindGridTinBai()
        {
            DataTable dt = Sp_ListCongViec(_user.UserID);
            DataGridDanhSachTinBai.DataSource = dt.DefaultView;
            DataGridDanhSachTinBai.DataBind();
        }
        string BuildSQL(int status, string Madoituong)
        {
            string sql = "";
            sql = " Trangthai_Xoa=0 ";
            if (status != 1)
                sql += " and  Ma_Nguoitao =" + _user.UserID + " and Trangthai=" + status;
            else
                sql += " and Trangthai=" + status;

            string sClause = sql + " and Ma_Chuyenmuc in (select Ma_Chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") and Doituong_DangXuly= N'" + Madoituong + "'";
            return sClause;

        }
        public DataTable Sp_ListCongViec(int UserID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Menu_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Chucnang", typeof(string)));
            dt.Columns.Add(new DataColumn("Module_URL", typeof(string)));
            dt.Columns.Add(new DataColumn("MaDoiTuong", typeof(string)));
            dt.Columns.Add(new DataColumn("ChuaXL", typeof(string)));
            dt.Columns.Add(new DataColumn("DangXL", typeof(string)));
            dt.Columns.Add(new DataColumn("Tralai", typeof(string)));
            dt.Columns.Add(new DataColumn("Off", typeof(bool)));
            dt.Columns.Add(new DataColumn("href", typeof(string)));
            string _MaDoiTuong = "";
            DataTable _dt = _untilDAL.GetStoreDataSet("Sp_ListCongViec", new string[] { "@User_ID", "@Ma_QTBT" }, new object[] { UserID, Ma_QTBT }).Tables[0];

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {

                    dr = dt.NewRow();
                    dr["Menu_ID"] = Convert.ToInt32(_dt.Rows[i]["Ma_Chucnang"]);
                    dr["Chucnang"] = Convert.ToString(_dt.Rows[i]["Ten_chucnang"]);
                    dr["MaDoiTuong"] = Convert.ToString(_dt.Rows[i]["Ma_Doituong"]);
                    dr["Module_URL"] = Convert.ToString(_dt.Rows[i]["URL_Chucnang"]);
                    _MaDoiTuong = Convert.ToString(_dt.Rows[i]["Ma_Doituong"]);
                    dr["Off"] = true;

                    string _tinmoi = _ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(1, _dt.Rows[i]["Ma_Doituong"].ToString()));
                    string _tindangxuly = _ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(2, _dt.Rows[i]["Ma_Doituong"].ToString()));
                    string _tintralai = _ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(3, _dt.Rows[i]["Ma_Doituong"].ToString()));

                    dr["ChuaXL"] = _tinmoi;

                    dr["DangXL"] = _tindangxuly;

                    dr["Tralai"] = _tintralai;
                    if (dr["ChuaXL"].ToString() == "0" && dr["DangXL"].ToString() == "0" && dr["Tralai"].ToString() == "0")
                    {
                        dr["href"] = "";
                        dr["Off"] = false;
                    }
                    else
                    {
                        dr["href"] = "<a id=\"linkQT\" class=\"linkGridDanhsachcongviec\" href=\'" + Global.ApplicationPath + "/" + _dt.Rows[i]["URL_Chucnang"].ToString() + "?Menu_ID=" + _dt.Rows[i]["Ma_Chucnang"].ToString() + "&MaDoiTuong=" + _dt.Rows[i]["Ma_Doituong"].ToString() + "'>" + _dt.Rows[i]["Ten_chucnang"].ToString() + "</a>";
                        dr["Off"] = true;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        public void GetTotalRecordCV()
        {
            string _thuchiencv = "0", _giaoviec = "0";
            _giaoviec = _ulti.GetColumnValuesTotal("T_Congviec", "COUNT (Ma_Congviec) as Total", _GetWhereGiaoViec());
            _thuchiencv = _ulti.GetColumnValuesTotal("T_Congviec", "COUNT (Ma_Congviec) as Total", _GetWhereThuchienCV());
            lblthuchiencv.Text = "(" + _thuchiencv + ")";
            lblgiaoviec.Text = "(" + _giaoviec + ")";

        }
        public string _GetWhereThuchienCV()
        {
            DataTable _dt = new DataTable();
            string _where = string.Empty;
            string phong = string.Empty;
            string _sql = " IsDeleted = 0 and ProvinceID<>0 and UserID=" + _user.UserID;
            _dt = _NguoidungDAL.GetT_User_Dynamic(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                phong = _dt.Rows[0]["ProvinceID"].ToString();
            if (phong != string.Empty)
                _where = " Status=0 and (NguoiNhan = " + _user.UserID.ToString() + " or NguoiNhan =0) and (Phong_ID=0 or Phong_ID =" + _dt.Rows[0]["ProvinceID"].ToString() + ") and NguoiGiaoViec<>" + _user.UserID;
            else
                _where = " Status=0 and (NguoiNhan = " + _user.UserID.ToString() + " or NguoiNhan =0) and Phong_ID=0 and NguoiGiaoViec<>" + _user.UserID;


            return _where;
        }
        public string _GetWhereGiaoViec()
        {
            string where = " ( NguoiTao = " + _user.UserID.ToString() + " OR NguoiGiaoViec = " + _user.UserID.ToString() + " ) ";
            return where;
        }


        #endregion

        protected void DataGridDanhSachCV_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
    }
}
