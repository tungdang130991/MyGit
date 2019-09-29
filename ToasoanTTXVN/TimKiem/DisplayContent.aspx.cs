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
using System.Web.Services;

namespace ToasoanTTXVN.TimKiem
{


    public partial class DisplayContent : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        HPCBusinessLogic.DAL.TinBaiDAL _Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        UltilFunc _ulti = new UltilFunc();

        private string _AnphamID = "";
        private string _SobaoID = "";
        private string _Trang = "";
        private string _ChuyenmucID = "";
        private string _Tieude = "";
        private string _Tungay = "";
        private string _Denngay = "";
        private string _Tacgia = "";
        private string _Matacgia = "";
        int PageIndex = 0, PageSize = 0;
        protected DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["AnphamID"]))
                    _AnphamID = Request.QueryString["AnphamID"];
                if (!string.IsNullOrEmpty(Request.QueryString["SobaoID"]))
                    _SobaoID = Request.QueryString["SobaoID"];
                if (!string.IsNullOrEmpty(Request.QueryString["Trang"]))
                    _Trang = Request.QueryString["Trang"];
                if (!string.IsNullOrEmpty(Request.QueryString["ChuyenmucID"]))
                    _ChuyenmucID = Request.QueryString["ChuyenmucID"];
                if (!string.IsNullOrEmpty(Request.QueryString["Tieude"]))
                    _Tieude = Request.QueryString["Tieude"];
                if (!string.IsNullOrEmpty(Request.QueryString["Tungay"]))
                    _Tungay = Request.QueryString["Tungay"];
                if (!string.IsNullOrEmpty(Request.QueryString["Denngay"]))
                    _Denngay = Request.QueryString["Denngay"];
                if (!string.IsNullOrEmpty(Request.QueryString["Tacgia"]))
                    _Tacgia = Request.QueryString["Tacgia"];
                if (!string.IsNullOrEmpty(Request.QueryString["Matacgia"]))
                    _Matacgia = Request.QueryString["Matacgia"];
                if (!string.IsNullOrEmpty(Request.QueryString["PageIndex"]))
                    PageIndex = int.Parse(Request.QueryString["PageIndex"]);

            }
            LoadDataListNews();

        }

        public string GetWhere(string sOrder)
        {
            string sWhere = " 1=1 and Trangthai<>6";

            if (_AnphamID != "0")
                sWhere += " AND Ma_AnPham=" + _AnphamID;

            if (_SobaoID != "0")
                sWhere += " AND  Ma_Sobao=" + _SobaoID;
            if (_Trang != "0")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + _Trang + ")";
            if (_Tungay.Trim() != "" && _Denngay.Trim() != "")
                sWhere += " AND Ma_Sobao in(select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + _Tungay.Trim() + " 00:00:00' and Ngay_Xuatban<='" + _Denngay.Trim() + " 23:59:59')";

            if (_ChuyenmucID != "0")
                sWhere += " AND Ma_Chuyenmuc=" + _ChuyenmucID;
            if (_Tacgia == "")
                _Matacgia = "";
            if (_Matacgia != "")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Nhuanbut where Ma_tacgia=" + _Matacgia + ")";

            return sWhere + sOrder;
        }

        public void LoadDataListNews()
        {
            string FulltextSearch = UltilFunc.ReplaceAll(_Tieude.Trim(), "'", "’");
            string sOrder = " ORDER BY Ngaytao DESC";
            try
            {
                if (PageIndex == 1 || PageIndex == 0)
                    PageIndex = 0;
                else
                    PageIndex = PageIndex - 1;
                PageSize = Global.MembersPerPage;
                DataSet _ds;
                _ds = _Daltinbai.BindGridT_NewsFullTeztSearch(PageIndex, PageSize, GetWhere(sOrder), UltilFunc.SplitString(FulltextSearch));
                int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
                if (TotalRecord == 0 && PageIndex > 0)
                    _ds = _Daltinbai.BindGridT_NewsFullTeztSearch(PageIndex - 1, PageSize, GetWhere(sOrder), UltilFunc.SplitString(FulltextSearch));
                _dt = _ds.Tables[0];
                totalItem1.Value = TotalRecords.ToString();
                pagesize1.Value = PageSize.ToString();
                currentpage1.Value = (PageIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}
