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
    public class QuyTrinhDAL
    {
        public DataSet BindGridT_QuyTrinh(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectAllT_AnPham", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Bind_DoituongGui(int maAnpham)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_BindDoiTuongGui", new string[] { "@Ma_AnPham" }, new object[] { maAnpham });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public DataSet Bind_DoituongNhan(int maAnpham)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_BindDoiTuongNhan", new string[] { "@Ma_AnPham" }, new object[] { maAnpham });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public void InsertT_Quytrinh(string Masend, string MaNhan, int MaAnpham, int Nguoitao, DateTime Ngaytao, int Nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_Quytrinh", new string[] { "@Ma_Doituong_Gui", "@Ma_Doituong_Nhan", "@Ma_AnPham", "@Nguoitao", "@Ngaytao", "@Nguoisua", "@Ngaysua" }, new object[] { Masend, MaNhan, MaAnpham, Nguoitao, Ngaytao, Nguoisua, ngaysua });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CopyQuyTrinhBienTap(int _Loaibao)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_CopyQuyTrinhBienTap", new string[] { "@MaAnPham" }, new object[] { _Loaibao });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_QuytrinhTemp(string Masend, string MaNhan, int MaAnpham, int Nguoitao, int Nguoisua)
        {
            DataSet _dsQT;
            int _returnQT;
            try
            {
                _dsQT = HPCDataProvider.Instance().GetStoreDataSet("Sp_InsertT_QuytrinhTemp", new string[] { "@Ma_Doituong_Gui", "@Ma_Doituong_Nhan", "@Ma_AnPham", "@Nguoitao", "@Nguoisua" }, new object[] { Masend, MaNhan, MaAnpham, Nguoitao, Nguoisua });
                _returnQT = Convert.ToInt32(_dsQT.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _returnQT;
        }

        public int Check_QuyTrinh(string DTGui, string DTNhan, int MaAnPham)
        {
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_Check_QuyTrinh", new string[] { "@Ma_Doituong_Gui", "@Ma_Doituong_Nhan", "@Ma_AnPham" }, new object[] { DTGui, DTNhan, MaAnPham });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public T_Doituong GetOneFromT_DoiTuongByID(string ID)
        {
            try
            {
                return (T_Doituong)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_Doituong", ID, "T_Doituong", "Ma_Doituong");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGrid_DoiTuongGui(string Ma_Doituong_Gui)
        {
            try
            {
                return HPCShareDLL.HPCDataProvider.Instance().GetStoreDataSet("sp_BindGrid_DoiTuongGui", new string[] { "@Ma_Doituong_Gui" }, new object[] { Ma_Doituong_Gui }).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable BindGridT_QuytrinhTemp()
        {
            try
            {
                return HPCShareDLL.HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridT_QuytrinhTemp", new string[] { }, new object[] { }).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable BindGridT_DoiTuong(string MaQT, int MaLoaiAnPham, int STT)
        {
            try
            {
                return HPCShareDLL.HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridT_QuytrinhWorkFlowDynamic", new string[] { "@MaQT", "@MaLoaiAnpham", "@STT" }, new object[] { MaQT, MaLoaiAnPham, STT }).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteT_QuytrinhTemp(string DT_Gui, string DT_Nhan, int MaAnPham)
        {
            try
            {
                HPCShareDLL.HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_QuytrinhTemp", new string[] { "@DTGui", "@DTNhan", "@MaAnPham" }, new object[] { DT_Gui, DT_Nhan, MaAnPham });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGridT_T_QuytrinhWorkFlow(string MaQT, int MaLoaiAnPham, int STT)
        {
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Ma_Doituong", typeof(string)));
            dt.Columns.Add(new DataColumn("Ten_Doituong", typeof(string)));
            dt.Columns.Add(new DataColumn("STT", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));
            DataTable _dt = _untilDAL.GetStoreDataSet("sp_BindGridT_QuytrinhWorkFlow", new string[] { "@MaQT", "@MaLoaiAnpham", "@STT" }, new object[] { MaQT, MaLoaiAnPham, STT }).Tables[0];

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {

                    dr = dt.NewRow();
                    dr[0] = _dt.Rows[i]["ID"].ToString();
                    dr[1] = _dt.Rows[i]["Ma_Doituong"].ToString();
                    dr[2] = _dt.Rows[i]["Ten_Doituong"].ToString();
                    dr[3] = _dt.Rows[i]["STT"].ToString();
                    if (_dt.Rows[i]["Role"] != DBNull.Value)
                        dr[4] = Convert.ToBoolean(_dt.Rows[i]["Role"]);
                    else
                        dr[4] = false;
                    dt.Rows.Add(dr);

                }
            }
            return dt;
        }

    }
}
