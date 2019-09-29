using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using HPCBusinessLogic;
using HPCBusinessLogic.DAL;
using System.Data;
using System.Data.SqlClient;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using WDF.Component;
using System.Globalization;
namespace ToasoanTTXVN.Quytrinh
{
    /// <summary>
    /// Summary description for AutoSave
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class AutoSave : System.Web.Services.WebService
    {
        UltilFunc ulti = new UltilFunc();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = new T_Users();
        List<ChuyenmucDetails> details = new List<ChuyenmucDetails>();
        HPCBusinessLogic.DAL.TinBaiDAL _Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();


        List<T_TinBai> _objtinbai = new List<T_TinBai>();
        [WebMethod]
        public string SaveData(object id_autosave, object Tieude, object Noidung, object Matinbai)
        {

            T_AutoSave_DAL _dal = new T_AutoSave_DAL();
            T_AutoSave _obj = new T_AutoSave();
            double _returnid = 0;
            try
            {
                if (id_autosave.ToString() != "")
                    _obj.ID = double.Parse(id_autosave.ToString());
                else
                    _obj.ID = 0;
                if (Matinbai.ToString() != "")
                    _obj.Ma_TinBai = int.Parse(Matinbai.ToString());
                else
                    _obj.Ma_TinBai = 0;
                if (UltilFunc.CleanFormatTags(Tieude.ToString().Trim()) != "Nhập tiêu đề")
                    _obj.Tieude = UltilFunc.CleanFormatTags(Tieude.ToString().Trim());
                else
                    _obj.Tieude = "Không tiêu đề";
                _obj.Noidung = Noidung.ToString();

                _obj.NgayTao = DateTime.Now;
                _obj.Ma_Nguoitao = _user.UserID;

                _returnid = _dal.Sp_InsertT_AutoSave(_obj);

            }
            catch (Exception ex)
            { throw ex; }
            return _returnid.ToString();
        }
        [WebMethod]
        public List<LoaibaoDetails> BindDatatoDropdownAnpham()
        {

            DataTable dt = new DataTable();
            List<LoaibaoDetails> details = new List<LoaibaoDetails>();

            string sql = "select ma_anpham, ten_anpham from t_anpham";
            dt = ulti.ExecSqlDataSet(sql).Tables[0];
            foreach (DataRow dtrow in dt.Rows)
            {
                LoaibaoDetails loaibao = new LoaibaoDetails();
                loaibao.Ma_LoaiBao = Convert.ToInt32(dtrow["ma_anpham"].ToString());
                loaibao.Ten_LoaiBao = dtrow["ten_anpham"].ToString();
                details.Add(loaibao);
            }

            return details;
        }
        [WebMethod]
        public SobaoDetails[] BindDatatoDropdownSobao(object AnphamID)
        {
            DataTable dt = new DataTable();
            List<SobaoDetails> details = new List<SobaoDetails>();
            string _sql = string.Empty;


            _sql = "set dateformat dmy select id,Name+' -- '+CONVERT(nvarchar(30),Publish_Date,103) as Name from [dbo].[fn_SoBao](" + AnphamID + ",0) order by Publish_Date DESC";

            try
            {
                dt = ulti.ExecSqlDataSet(_sql).Tables[0];
                foreach (DataRow dtrow in dt.Rows)
                {
                    SobaoDetails sobao = new SobaoDetails();
                    sobao.ID = Convert.ToInt32(dtrow["ID"].ToString());
                    sobao.Name = dtrow["Name"].ToString();
                    details.Add(sobao);
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return details.ToArray();
        }
        [WebMethod]
        public TrangDetails[] BindDatatoDropdownTrang(object AnphamID)
        {
            List<TrangDetails> _Trangdetails = new List<TrangDetails>();

            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();

            try
            {
                if (int.Parse(AnphamID.ToString()) > 0)
                {
                    int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(int.Parse(AnphamID.ToString())).Sotrang.ToString());

                    for (int j = 1; j < _sotrang + 1; j++)
                    {
                        TrangDetails _Trang = new TrangDetails();
                        _Trang.ID = Convert.ToInt32(j.ToString());
                        _Trang.Name = "Page " + j.ToString();
                        _Trangdetails.Add(_Trang);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return _Trangdetails.ToArray();
        }
        [WebMethod]
        public ChuyenmucDetails[] BindDatatoDropdownChuyenmuc(object AnphamID)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            DataTable dt = new DataTable();

            string _sql = string.Empty;
            _sql = "set dateformat dmy; SELECT Ma_ChuyenMuc,Ten_ChuyenMuc FROM T_ChuyenMuc where Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + AnphamID + " AND Ma_Chuyenmuc_Cha = 0 ";
            int Rank = 0;
            try
            {
                dt = ulti.ExecSqlDataSet(_sql).Tables[0];
                foreach (DataRow dtrow in dt.Rows)
                {
                    ChuyenmucDetails _listchuyenmuc = new ChuyenmucDetails();

                    _listchuyenmuc.Ma_ChuyenMuc = Convert.ToInt32(dtrow["Ma_ChuyenMuc"].ToString());
                    _listchuyenmuc.Ten_ChuyenMuc = dtrow["Ten_ChuyenMuc"].ToString();
                    details.Add(_listchuyenmuc);
                    BinTreeCategorys(Rank, dtrow["Ma_ChuyenMuc"].ToString());

                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return details.ToArray();
        }
        [WebMethod]
        public void BinTreeCategorys(int Rank, string CategorysID)
        {
            Rank++;
            string _sqlChild = string.Empty;
            DataTable _dtchild = new DataTable();
            if (HPCBusinessLogic.UltilFunc.GetLatestID("T_ChuyenMuc", "Ma_Chuyenmuc_Cha", "WHERE Ma_Chuyenmuc_Cha=" + CategorysID) > 0)
            {
                _sqlChild = "set dateformat dmy; SELECT Ma_ChuyenMuc,Ten_ChuyenMuc FROM T_ChuyenMuc WHERE  Ma_Chuyenmuc_Cha= " + CategorysID + " ORDER BY Ma_ChuyenMuc";

                try
                {
                    _dtchild = ulti.ExecSqlDataSet(_sqlChild).Tables[0];

                    if (_dtchild != null && _dtchild.Rows.Count > 0)
                    {
                        foreach (DataRow dtrow in _dtchild.Rows)
                        {
                            string blank = "";
                            for (int k = 0; k < Rank; k++)
                            {
                                blank = "&nbsp;&nbsp;&nbsp;&nbsp;" + blank;
                            }
                            ChuyenmucDetails _listchuyenmuc = new ChuyenmucDetails();
                            _listchuyenmuc.Ma_ChuyenMuc = Convert.ToInt32(dtrow["Ma_ChuyenMuc"].ToString());
                            _listchuyenmuc.Ten_ChuyenMuc = HttpUtility.HtmlDecode(blank) + dtrow["Ten_ChuyenMuc"].ToString();
                            details.Add(_listchuyenmuc);
                            BinTreeCategorys(Rank, dtrow["Ma_ChuyenMuc"].ToString());

                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            Rank--;

        }
        [WebMethod]
        public ListData LoadDataPaging(object Tieude, object Anpham, object Sobao, object Trang, object Chuyenmuc, object Tungay, object Denngay, object Tacgia, object Matacgia, object PageIndex, object PageSize)
        {

            int TotalRecords = 0;
            string sOrder = string.Empty;
            DataSet _ds = new DataSet();
            DataTable _dt = new DataTable();
            var data = new ListData();
            try
            {
                string _where = GetWhere(Tieude.ToString(), Anpham.ToString(), Sobao.ToString(), Trang.ToString(), Chuyenmuc.ToString(), Tungay.ToString(), Denngay.ToString(), Tacgia.ToString(), Matacgia.ToString());
                _ds = _Daltinbai.BindGridT_NewsEditor(int.Parse(PageIndex.ToString()), int.Parse(PageSize.ToString()), _where);
                TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());

                _dt = _ds.Tables[0];
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    _objtinbai.Add(new T_TinBai()
                    {
                        Ma_Tinbai = double.Parse(_dt.Rows[i]["Ma_Tinbai"].ToString()),
                        Tieude = _dt.Rows[i]["Tieude"].ToString(),
                        Ma_TacGia = int.Parse(_dt.Rows[i]["Ma_TacGia"].ToString()),
                        Ma_Anpham = int.Parse(_dt.Rows[i]["Ma_Anpham"].ToString()),
                        Ma_Chuyenmuc = int.Parse(_dt.Rows[i]["Ma_Chuyenmuc"].ToString()),
                        Ma_Sobao = int.Parse(_dt.Rows[i]["Ma_Sobao"].ToString())
                    });
                }
                data.ListNews = _objtinbai;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            data.TotalRecords = TotalRecords;
            return data;
        }
        public string GetWhere(string _Tieude, string _AnphamID, string _SobaoID, string _Trang, string _ChuyenmucID, string _Tungay, string _Denngay, string _Tacgia, string _Matacgia)
        {
            string sOrder = " ORDER BY Ngaytao DESC";
            string sWhere = " 1=1 and Trangthai<>6";

            if (_Tieude.Length > 0 && _Tieude.Trim() != "")
                sWhere += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(_Tieude.Trim()));

            if (_AnphamID != "0")
                sWhere += " AND Ma_AnPham=" + _AnphamID;

            if (_SobaoID != "0")
                sWhere += " AND Ma_Sobao=" + _SobaoID;
            if (_Trang != "0")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + _Trang + ")";
            if (_Tungay.Trim() != "" && _Denngay.Trim() != "")
                sWhere += " AND Ma_Sobao in(select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + _Tungay.Trim() + " 00:00:00' and Ngay_Xuatban<='" + _Denngay.Trim() + " 23:59:59')";

            if (_ChuyenmucID != "0")
                sWhere += " AND Ma_Chuyenmuc=" + _ChuyenmucID;
            if (_Tacgia == "")
                _Matacgia = "";
            if (_Matacgia != "")
                sWhere += " AND Ma_TacGia=" + _Matacgia;
            if (_Tacgia != "")
                sWhere += " AND TacGia LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(_Tacgia.Trim().Replace(" -- ", "|").Split('|')[0]));
            
            return sWhere + sOrder;
        }
    }

    public class LoaibaoDetails
    {
        public int Ma_LoaiBao { get; set; }
        public string Ten_LoaiBao { get; set; }
    }
    public class SobaoDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class TrangDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ChuyenmucDetails
    {
        public int Ma_ChuyenMuc { get; set; }
        public string Ten_ChuyenMuc { get; set; }
    }
    public class ListData
    {
        public List<T_TinBai> ListNews { get; set; }
        public int TotalRecords { get; set; }
    }

}
