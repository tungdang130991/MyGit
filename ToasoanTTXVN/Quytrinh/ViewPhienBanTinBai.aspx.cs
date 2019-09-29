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
namespace ToasoanTTXVN.Quytrinh
{
    public partial class ViewPhienBanTinBai : BasePage
    {
        public string Tieude;
        public string Noidung;
        public string Chuyenmuc;
        public string Sotu;
        public string Sotrang;
        public string Nhuanbut;
        public string Tacgia;
        public string nguoisua;
        public string Ghichu;
        public string nguoisuaprev;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        UltilFunc _ulti = new UltilFunc();
        string pathfiledoc = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                if (!IsPostBack)
                {
                    T_TinBai obj = new T_TinBai();
                    obj = Daltinbai.load_T_news(id);
                    Tieude = obj.Tieude;
                    if (obj.Sotu != 0)
                        Sotu = obj.Sotu.ToString();
                    else
                        Sotu = "0";
                    if (obj.Tiennhuanbut > 0)
                        Nhuanbut = String.Format("{0:00,0}", Convert.ToDecimal(obj.Tiennhuanbut.ToString()));
                    else
                        Nhuanbut = String.Format("{0:00,0}", "0");
                    //string sqltien = "select Sotien,Ma_tacgia from T_NhuanBut where Ma_tacgia<>" + obj.Ma_TacGia + " and Ma_TinBai=" + id;
                    //DataTable dtnhuanbut = _ulti.ExecSqlDataSet(sqltien).Tables[0];
                    //if (dtnhuanbut != null && dtnhuanbut.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtnhuanbut.Rows.Count; i++)
                    //    {
                    //        if (i == 0)
                    //            Nhuanbut += "; " + String.Format("{0:00,0}", Convert.ToDecimal(dtnhuanbut.Rows[i]["Sotien"].ToString())) + "; ";
                    //        else
                    //            Nhuanbut += String.Format("{0:00,0}", Convert.ToDecimal(dtnhuanbut.Rows[i]["Sotien"].ToString())) + "; ";
                    //    }
                    //}


                    if (dalcm.GetOneFromT_ChuyenmucByID(obj.Ma_Chuyenmuc) != null)
                        Chuyenmuc = dalcm.GetOneFromT_ChuyenmucByID(obj.Ma_Chuyenmuc).Ten_ChuyenMuc;
                    else
                        Chuyenmuc = "";
                    Tieude = obj.Tieude.ToString();
                    Noidung = obj.Noidung.ToString();
                    Ghichu = obj.GhiChu;
                    Tacgia = obj.TacGia;

                    DataTable dttrangbaosobao = UltilFunc.GetTrangSoBaoFrom_T_VitriTiBai(id);
                    if (dttrangbaosobao != null && dttrangbaosobao.Rows.Count > 0)
                        Sotrang = " Trang: " + dttrangbaosobao.Rows[0]["Trang"].ToString();
                    else
                        Sotrang = "";

                    if (_NguoidungDAL.GetUserByUserName_ID((int)obj.Ma_Nguoitao) != null)
                        nguoisua = _NguoidungDAL.GetUserByUserName_ID((int)obj.Ma_Nguoitao).UserFullName;
                    else
                        nguoisua = "User does not exist";
                    DataTable dtphienban = Daltinbai.GetOneFromT_PhienbanByNews_ID(id, -1, false).Tables[0];
                    if (dtphienban != null && dtphienban.Rows.Count > 0)
                    {

                        ViewState["ver"] = dtphienban.Rows[0]["Ma_Phienban"].ToString();
                    }
                    else
                        ViewState["ver"] = -1;
                    LoadDataImageTinBai(id.ToString());
                    BindGrid();
                }
            }
        }

        public void LoadDataImageTinBai(string _matinbai)
        {
            TinBaiAnhDAL _daltinanh = new TinBaiAnhDAL();
            string where = string.Empty;

            where = " Ma_TinBai=" + _matinbai;
            DataSet _ds = _daltinanh.Sp_SelectTinAnhDynamic(where, "NgayTao DESC");
            this.DataListAnh.DataSource = _ds;
            this.DataListAnh.DataBind();
        }
        public void LoadDataImagePhienban(string _maphienban)
        {
            TinBaiAnhDAL _daltinanh = new TinBaiAnhDAL();
            string _sql = string.Empty;

            _sql = "select * from VersionImg where  Ma_phienban=" + _maphienban + " order by NgayTao DESC";
            DataSet _ds = _ulti.ExecSqlDataSet(_sql);
            this.DataListAnh.DataSource = _ds;
            this.DataListAnh.DataBind();
        }
        public void BindGrid()
        {
            UltilFunc Ulti = new UltilFunc();
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                string _Sql = "select Ma_Phienban, Ma_Nguoitao,NgayTao from T_PhienBan  where Ma_Tinbai=" + id;
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
                T_TinBai objtinbai = new T_TinBai();
                objtinbai = Daltinbai.load_T_news(news_id);
                if (dalcm.GetOneFromT_ChuyenmucByID(objtinbai.Ma_Chuyenmuc) != null)
                    Chuyenmuc = dalcm.GetOneFromT_ChuyenmucByID(objtinbai.Ma_Chuyenmuc).Ten_ChuyenMuc;
                else
                    Chuyenmuc = "";
                T_PhienBan objver_current = new T_PhienBan();
                T_PhienBan objver_select = new T_PhienBan();

                objver_current = Daltinbai.Sp_SelectOneFromT_PhienBan(id_current);
                objver_select = Daltinbai.Sp_SelectOneFromT_PhienBan(id_select);

                if (objver_select.Sotu != 0)
                    Sotu = objver_select.Sotu.ToString();
                else
                    Sotu = "0";
                if (objver_select.Tiennhuanbut > 0)
                    Nhuanbut = String.Format("{0:00,0}", Convert.ToDecimal(objver_select.Tiennhuanbut.ToString()));
                else
                    Nhuanbut = String.Format("{0:00,0}", "0");
                //string sqltien = "select Sotien,Ma_tacgia from T_NhuanBut where Ma_tacgia<>" + objver_select.Ma_TacGia + " and Ma_TinBai=" + news_id + " and Nguoicham=" + objver_select.Ma_Nguoitao;
                //DataTable dtnhuanbut = _ulti.ExecSqlDataSet(sqltien).Tables[0];
                //if (dtnhuanbut != null && dtnhuanbut.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtnhuanbut.Rows.Count; i++)
                //    {
                //        if (i == 0)
                //            Nhuanbut += "; " + String.Format("{0:00,0}", Convert.ToDecimal(dtnhuanbut.Rows[i]["Sotien"].ToString())) + "; ";
                //        else
                //            Nhuanbut += String.Format("{0:00,0}", Convert.ToDecimal(dtnhuanbut.Rows[i]["Sotien"].ToString())) + "; ";
                //    }
                //}
                DataTable dttrangbaosobao = UltilFunc.GetTrangSoBaoFrom_T_VitriTiBai(int.Parse(news_id.ToString()));
                if (dttrangbaosobao != null && dttrangbaosobao.Rows.Count > 0)
                    Sotrang = " Trang: " + dttrangbaosobao.Rows[0]["Trang"].ToString();
                else
                    Sotrang = "";


                if (checkversion.Checked == true)
                    if (id_select > id_current)
                    {
                        Tieude = MergeEngineCompare(objver_current.Tieude, objver_select.Tieude.ToString());
                        Noidung = MergeEngineCompare(objver_current.Noidung.ToString(), objver_select.Noidung.ToString());
                    }
                    else
                    {
                        Tieude = MergeEngineCompare(objver_select.Tieude, objver_current.Tieude.ToString());
                        Noidung = MergeEngineCompare(objver_select.Noidung.ToString(), objver_current.Noidung.ToString());
                    }
                else
                    Noidung = UltilFunc.CleanWordHtml(objver_select.Noidung.ToString());

                Ghichu = objver_select.GhiChu;
                Tacgia = objver_select.TacGia;


                if (_NguoidungDAL.GetUserByUserName_ID((int)objver_select.Ma_Nguoitao) != null)
                    nguoisua = _NguoidungDAL.GetUserByUserName_ID((int)objver_select.Ma_Nguoitao).UserFullName;
                else
                    nguoisua = "User does not exist";

                if (objver_current != null)
                {
                    nguoisuaprev = _NguoidungDAL.GetUserByUserName_ID((int)objver_current.Ma_Nguoitao).UserFullName;
                    if (id_select > id_current)
                        checkversion.Text = nguoisuaprev + " <img src=\"../Dungchung/Images/rt1.gif\" /> " + nguoisua;
                    else
                        checkversion.Text = nguoisua + "<img src=\"../Dungchung/Images/lt1.gif\" /> " + nguoisuaprev;
                }
                ViewState["ver"] = objver_select.Ma_Phienban;
                LoadDataImagePhienban(ViewState["ver"].ToString());
            }
        }
    }
}
