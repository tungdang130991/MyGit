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
using Rainbow.MergeEngine;
namespace ToasoanTTXVN.Quangcao
{
    public partial class ViewPhienBanQC : System.Web.UI.Page
    {
        public string Tenquangcao;
        public string Noidung;
        public string Loaibao;
        public string Kichthuoc;
        public string Sotrang;
        public string Ngaydang;
        public string Nguoinhap;
        public string nguoisua;
        public string nguoisuaprev;
        HPCBusinessLogic.DAL.QuangCaoDAL _DalQC = new HPCBusinessLogic.DAL.QuangCaoDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        UltilFunc _ulti = new UltilFunc();
        int _MaPhienbanQC = 0;
        int _MaNguoinhap = 0;
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                if (!IsPostBack)
                {
                    T_QuangCao obj = new T_QuangCao();
                    obj = _DalQC.GetOneFromT_QuangCaoByID(id);

                    Tenquangcao = obj.Ten_QuangCao.ToString();
                    Noidung = obj.Noidung.ToString();
                    Kichthuoc = UltilFunc.GetKichCoQuangCao(obj.Kichthuoc);
                    Loaibao = UltilFunc.GetTenAnpham(obj.Ma_Loaibao);
                    Ngaydang = obj.Ngaydang.ToString("dd/MM/yyyy");

                    if (obj.Trang > 0)
                        Sotrang = " Trang :" + obj.Trang.ToString();
                    else
                        Sotrang = "";

                    _MaNguoinhap = int.Parse(_ulti.GetColumnValuesDynamic("T_PhienBanQuangCao", " top 1 Nguoitao", "Nguoitao", "Ma_Quangcao=" + id + " order by ID ASC"));
                    if (_MaNguoinhap != 0)
                    {
                        if (_NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap) != null)
                            Nguoinhap = _NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap).UserFullName;
                        else
                            Nguoinhap = "";
                    }
                    else
                    {
                        Nguoinhap = "";
                    }
                    if (_NguoidungDAL.GetUserByUserName_ID((int)obj.Nguoitao) != null)
                        nguoisua = _NguoidungDAL.GetUserByUserName_ID((int)obj.Nguoitao).UserFullName;
                    else
                        nguoisua = "User does not exist";

                    _MaPhienbanQC = int.Parse(_ulti.GetColumnValuesDynamic("T_PhienBanQuangCao", " top 1 ID", "ID", "Ma_Quangcao=" + id + " order by ID DESC"));

                    if (_MaPhienbanQC != 0)
                    {

                        ViewState["ver"] = _MaPhienbanQC.ToString();
                    }
                    else
                        ViewState["ver"] = -1;
                    LoadDataImageTinBai(id.ToString());
                    BindGridNguoiXuly();
                }
            }
        }
        public void LoadDataImageTinBai(string _maquangcao)
        {
            string where = string.Empty;
            where = " Ma_Quangcao=" + _maquangcao;
            DataSet _ds = _DalQC.Sp_SelectT_FileQuangCaoDynamic(where, " NgayTao DESC ");
            this.DataListAnh.DataSource = _ds;
            this.DataListAnh.DataBind();
        }

        public void BindGridNguoiXuly()
        {
            UltilFunc Ulti = new UltilFunc();
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                string _Sql = "select ID,Nguoitao,NgayTao from T_PhienBanQuangCao  where Ma_Quangcao=" + id;
                DataSet ds = Ulti.ExecSqlDataSet(_Sql);
                DataGridTacGiaTinBai.Visible = true;
                DataGridTacGiaTinBai.DataSource = ds;
                DataGridTacGiaTinBai.DataBind();
            }

        }
        public string MergeEngineCompare(string str1, string str2)
        {
            string Noidung = "";
            Merger merger = new Merger(str1, str2);
            Noidung = merger.merge();
            return Noidung;
        }
        public void DataGridTacGiaTinBai_EditCommand(object source, DataGridCommandEventArgs e)
        {
            double news_id = double.Parse(Page.Request.QueryString["ID"].ToString());
            double id_select = int.Parse(this.DataGridTacGiaTinBai.DataKeys[e.Item.ItemIndex].ToString());
            double id_current = int.Parse(ViewState["ver"].ToString());
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                T_PhienBanQuangCao objver_current = new T_PhienBanQuangCao();
                T_PhienBanQuangCao objver_select = new T_PhienBanQuangCao();

                objver_current = _DalQC.Sp_SelectOneFromT_PhienBanQuangCao(id_current);
                objver_select = _DalQC.Sp_SelectOneFromT_PhienBanQuangCao(id_select);

                Tenquangcao = objver_select.Ten_QuangCao.ToString();

                if (checkversion.Checked == true)
                    if (id_select > id_current)
                        Noidung = MergeEngineCompare(objver_current.Noidung.ToString(), objver_select.Noidung.ToString());
                    else
                        Noidung = MergeEngineCompare(objver_select.Noidung.ToString(), objver_current.Noidung.ToString());
                else
                    Noidung = UltilFunc.CleanWordHtml(objver_select.Noidung.ToString());

                Kichthuoc = UltilFunc.GetKichCoQuangCao(objver_select.Kichthuoc);
                Loaibao = UltilFunc.GetTenAnpham(objver_select.Ma_Loaibao);
                if (objver_select.Trang != 0)
                    Sotrang = objver_select.Trang.ToString();
                else
                    Sotrang = "0";
                Ngaydang = objver_select.Ngaydang.ToString("dd/MM/yyyy");
                _MaNguoinhap = int.Parse(_ulti.GetColumnValuesDynamic("T_PhienBanQuangCao", " top 1 Nguoitao", "Nguoitao", "Ma_Quangcao=" + id + " order by ID ASC"));
                if (_MaNguoinhap != 0)
                {
                    if (_NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap) != null)
                        Nguoinhap = _NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap).UserFullName;
                    else
                        Nguoinhap = "User does not exist";
                }
                else
                {
                    Nguoinhap = "User does not exist";
                }
                if (_NguoidungDAL.GetUserByUserName_ID((int)objver_select.Nguoitao) != null)
                    nguoisua = _NguoidungDAL.GetUserByUserName_ID((int)objver_select.Nguoitao).UserFullName;
                else
                    nguoisua = "User does not exist";

                if (objver_current != null)
                {
                    nguoisuaprev = _NguoidungDAL.GetUserByUserName_ID((int)objver_current.Nguoitao).UserFullName;
                    if (id_select > id_current)
                        checkversion.Text = nguoisuaprev + " <img src=\"../Dungchung/Images/rt1.gif\" /> " + nguoisua;
                    else
                        checkversion.Text = nguoisua + "<img src=\"../Dungchung/Images/lt1.gif\" /> " + nguoisuaprev;
                }
                ViewState["ver"] = objver_select.ID;
            }
        }
    }
}
