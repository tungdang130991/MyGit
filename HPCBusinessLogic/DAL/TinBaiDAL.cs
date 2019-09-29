using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using System.Collections;
using SSOLib;
using SSOLib.ServiceAgent;

namespace HPCBusinessLogic.DAL
{
    public class TinBaiDAL
    {
        public DataSet BindGridT_NewsEditor(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_TinBaiDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsFullTeztSearch(int PageIndex, int PageSize, string WhereCondition, string Fulltext)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_TinBaiTimKiemFullText", new string[] { "@PageIndex", "@PageSize", "@where", "@txtSearch" }, new object[] { PageIndex, PageSize, WhereCondition, Fulltext });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_Tulieu(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_TuLieuDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NhuanbutGop(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_NhuanButGop_Dynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void sp_updatenoidungtin(double matinbai, string noidung)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("sp_updatenoidungtin", new string[] { "@matinbai", "@noidung" }, new object[] { matinbai, noidung });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Tinbai(T_TinBai obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_TinBai");
        }
        public int InsertT_Tinbai_WordOnline(T_TinBai obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_TinBai_WordOnline");
        }
        public int SP_UpdateT_TinBai_WordOnline(T_TinBai obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "SP_UpdateT_TinBai_WordOnline");
        }
        public void InsertT_Publish_PDF(T_Publish_Pdf obj)
        {
            HPCDataProvider.Instance().InsertObject(obj, "Sp_InsertT_Publish_Pdf");
        }
        public DataSet Sp_SelectT_Publish_PdfDynamic(string WhereCondition, string order)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectT_Publish_PdfDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Bind_PhienBanDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_PhienBanDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_TinBai load_T_news(Double Ma_Tinbai)
        {

            return (T_TinBai)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_TinBai", Ma_Tinbai.ToString(), "T_TinBai", "Ma_Tinbai");

        }
        public T_Tulieu load_T_Tulieu(Double Ma_Tulieu)
        {

            return (T_Tulieu)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_TuLieu", Ma_Tulieu.ToString(), "T_Tulieu", "Ma_Tulieu");

        }
        public T_Publish_Pdf load_T_Publish_Pdf(string ID)
        {

            return (T_Publish_Pdf)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_Publish_Pdf", ID, "T_Publish_Pdf", "ID");

        }
        public T_PhienBan Sp_SelectOneFromT_PhienBan(Double Ma_Phienban)
        {

            return (T_PhienBan)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_PhienBan", Ma_Phienban.ToString(), "T_PhienBan", "Ma_Phienban");

        }
        public DataSet GetOneFromT_PhienbanByNews_ID(Int32 Ma_Tinbai, int Version, bool huong)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectOneFromT_Phienban_byNews_ID", new string[] { "@Ma_Tinbai", "@Version", "@huong" }, new object[] { Ma_Tinbai, Version, huong });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_SelectOneFromT_PhienbanWithMaTinbai(Int32 Ma_Tinbai)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectOneFromT_PhienbanWithMaTinbai", new string[] { "@Ma_Tinbai" }, new object[] { Ma_Tinbai });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckSendLanguages(int LanguagesID, double Ma_TinBai)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = HPCDataProvider.Instance().GetStoreDataSet("Sp_CheckSendLanguages", new string[] { "@Ma_NgonNgu", "@Ma_TinBai" }, new object[] { LanguagesID, Ma_TinBai }).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_SelectOneNguoitaoTinBai(double Ma_Tinbai)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectOneNguoitaoTinBai", new string[] { "@Ma_Tinbai" }, new object[] { Ma_Tinbai });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Status_tintuc(double ID, int Status, int nguoisua, DateTime ngaysua, string news_pos, string news_trace)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Update_Status_T_Tinbai", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua", "@news_pos", "@news_trace" }, new object[] { ID, Status, nguoisua, ngaysua, news_pos, news_trace });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_InsertTinBaiToBienDichNgu(double Ma_Tinbai, int LanguageID, string news_pos)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertTinBaiToBienDichNgu", new string[] { "@Ma_Tinbai", "@Ma_Ngonngu", "@Doituongxl" }, new object[] { Ma_Tinbai, LanguageID, news_pos });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_DeleteT_Tinbai_WithTrangthai_Xoa(double ID, int nguoixoa)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Tinbai_WithTrangthai_Xoa", new string[] { "@Matinbai", "@Nguoixoa" }, new object[] { ID, nguoixoa });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_DeleteT_Publish_Pdf(int _ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Publish_Pdf", new string[] { "@ID" }, new object[] { _ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Tulieu_From_T_Tinbai(Double news_id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("sp_insert_T_TuLieu_from_T_TinBai", new string[] { "@Ma_tinbai" }, new object[] { news_id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Phienban_From_T_Tinbai(Double news_id, int nguoisua, DateTime ngaysua, string Sender)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Insert_Phienban_From_T_Tinbai", new string[] { "@News_ID", "@News_EditorID", "@News_DateEdit", "@Sender" }, new object[] { news_id, nguoisua, ngaysua, Sender });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Phienban_To_BienTapNgu(Double news_id, int nguoisua, DateTime ngaysua, int LanguageID, string Doituongxl)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Insert_Phienban_To_BienTapNgu", new string[] { "@News_ID", "@News_EditorID", "@News_DateEdit", "@Ma_NgonNgu", "@DoiTuongxl" }, new object[] { news_id, nguoisua, ngaysua, LanguageID, Doituongxl });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_Vitri_Tinbai_FromT_Tinbai(int Ma_Sobao, int Trang, double Ma_Tinbai, double Macongviec)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_T_Vitri_Tinbai_InsertFromT_Tinbai", new string[] { "@Ma_Sobao", "@Trang", "@Ma_Tinbai", "@Ma_Congviec" }, new object[] { Ma_Sobao, Trang, Ma_Tinbai, Macongviec });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteAllNhuanbutByMatin(int _Matin, int maanh)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteNhuanButByMa_Tin]", new string[] { "@Ma_TinBai", "@Ma_Anh" }, new object[] { _Matin, maanh });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertInsertT_NhuanBut(T_NhuanBut obj)
        {
            HPCDataProvider.Instance().InsertObject(obj, "Sp_InsertT_NhuanButReturnID");
        }
        public void InsertT_NhuanBut_FromT_Tinbai(double @Ma_TinBai, double @Sotien, int @Ma_tacgia, double @Ma_Anh, string @GHICHU, int Nguoicham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_NhuanBut", new string[] { "@Ma_TinBai", "@Sotien", "@Ma_tacgia", "@Ma_Anh", "@GHICHU", "@Nguoicham" }, new object[] { @Ma_TinBai, @Sotien, @Ma_tacgia, @Ma_Anh, @GHICHU, Nguoicham });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_UpdateT_NhuanButTinbai(double @Ma_TinBai, double matacgia, double @Sotien, double @Ma_Anh, string @GHICHU, int Nguoicham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_NhuanButTinbai", new string[] { "@Ma_TinBai", "@Matacgia", "@Sotien", "@Ma_Anh", "@GHICHU", "@Nguoicham" }, new object[] { @Ma_TinBai, matacgia, @Sotien, @Ma_Anh, @GHICHU, Nguoicham });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_Cham_NhuanButAnh(double @Ma_TinBai, double @Sotien, int @Ma_tacgia, double @Ma_Anh, string @GHICHU, int Nguoicham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Cham_NhuanButAnh", new string[] { "@Ma_TinBai", "@Sotien", "@Ma_tacgia", "@Ma_Anh", "@GHICHU", "@Nguoicham" }, new object[] { @Ma_TinBai, @Sotien, @Ma_tacgia, @Ma_Anh, @GHICHU, Nguoicham });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IsLock(double prmNewsID, int IsUserLock)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateKhoaTinbai", new string[] { "@newsID", "@UserLock" }, new object[] { prmNewsID, IsUserLock });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTrace(Double News_id)
        {
            DataSet _ds = new DataSet();
            try
            {
                _ds = HPCDataProvider.Instance().ExecSqlDataSet("select LuuVet from T_Tinbai where Ma_Tinbai=" + News_id.ToString());
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    if (_ds.Tables[0].Rows[0][0] == DBNull.Value)
                        return "";
                    else
                        return _ds.Tables[0].Rows[0][0].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Bind_DoiTuongGui(string MaDoiTuong, int MaAnPham, string Langquage)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_BindataDoiTuongGui", new string[] { "@MaDoiTuong", "@MaAnPham", "@Langquage" }, new object[] { MaDoiTuong, MaAnPham, Langquage });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_ListThongKeNhuanBut(int PageIndex, int PageSize, int loaibao, int thanhtoan, string tungay, string denngay, int loai, int vungmien, string doituongxl)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListThongKeNhuanBut", new string[] { "@PageIndex", "@PageSize", "@LoaiAnpham", "@thanhtoan", "@FromDate", "@ToDate", "@Loai", "@Vungmien", "@Doituongxl" }, new object[] { PageIndex, PageSize, loaibao, thanhtoan, tungay, denngay, loai, vungmien, doituongxl });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_ListTraCuuNhuanBut(string _where)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListTraCuuNhuanBut", new string[] { "@where" }, new object[] { _where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_Inbienlaithanhtoannhuanbut(double id, string MaTacGia)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_Inbienlaithanhtoannhuanbut", new string[] { "@ID", "@MaTacGia" }, new object[] { id, MaTacGia });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_InbienlaithanhtoannhuanbutALL(int loaibao, string tungay, string denngay, int matacgia, string doituongxl, string SoCMTND)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_InBienLaiNhuanButTinBai_All", new string[] { "@Loaianpham", "@FromDate", "@ToDate", "@Matacgia", "@Doituongxl", "@SoCMTND" }, new object[] { loaibao, tungay, denngay, matacgia, doituongxl, SoCMTND });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_Thongkethanhtoannhuanbut(int loaibao, int thanhtoan, string tungay, string denngay, int Loai, int vungmien, string doituongxl)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_Thongkethanhtoannhuanbut", new string[] { "@loaibao", "@thanhtoan", "@tungay", "@denngay", "@Loai", "@Vungmien", "@Doituongxl" }, new object[] { loaibao, thanhtoan, tungay, denngay, Loai, vungmien, doituongxl });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_List_Thanhtoannhuanbuttinbai(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_List_Thanhtoannhuanbuttinbai", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_List_ThanhtoannhuanbutAnh(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_List_ThanhtoannhuanbutAnh", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_UpdateThanhToanNhuanButTinBai(double @MaTinbai, double Matacgia, bool @Thanhtoan, DateTime @Ngaythanhtoan, int @Nguoithanhtoan)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateThanhToanNhuanButTinBaiAnh", new string[] { "@MaTinbai", "@Matacgia", "@Thanhtoan", "@Ngaythanhtoan", "@Nguoithanhtoan" }, new object[] { @MaTinbai, Matacgia, @Thanhtoan, @Ngaythanhtoan, @Nguoithanhtoan });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_UpdateThanhToanNhuanButAnh(double @MaAnh, bool @Thanhtoan, DateTime @Ngaythanhtoan, int @Nguoithanhtoan)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateThanhToanNhuanButAnh", new string[] { "@MaAnh", "@Thanhtoan", "@Ngaythanhtoan", "@Nguoithanhtoan" }, new object[] { @MaAnh, @Thanhtoan, @Ngaythanhtoan, @Nguoithanhtoan });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void sp_insertT_Tacgiatin(Double ma_tinbai, string Ten_Tacgia)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("sp_insertT_Tacgiatin", new string[] { "@Ma_Tinbai", "@Ten_Tacgia" }, new object[] { ma_tinbai, Ten_Tacgia });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

