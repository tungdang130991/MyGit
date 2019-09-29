using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;


namespace HPCBusinessLogic
{
    public class DoituongDAL
    {
        public DataSet BindGridT_Doituong(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_DoituongDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }

        public DataSet BindT_Doituong_AnPham(int maAnPham)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_BindT_Doituong_AnPham", new string[] { "@Ma_AnPham" }, new object[] { maAnPham });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }

        public DataSet Bind_MaDoituong()
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_BindMaDoiTuong", new string[] { }, new object[] { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }

        public int InsertT_Doituong(T_Doituong _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_Doituong");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOneFromT_Doituong(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Doituong", new string[] { "@ID" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_Doituong_AnPham(int _Id, int _maanpham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Doituong_AnPham", new string[] { "@ID", "@Ma_Anpham" }, new object[] { _Id, _maanpham });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePosition_Doituong(string MaDT, int MaAnpham, string cssLeft, string cssTop)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdatePosition_DoiTuong", new string[] { "@Ma_Doituong", "@Ma_Anpham", "@CssLeft", "@CssTop" }, new object[] { MaDT, MaAnpham, cssLeft, cssTop });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_Doituong_AnPham(string MaDT, int MaAnPham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_Doituong_Anpham", new string[] { "@MaDT", "@MaAnPham" }, new object[] { MaDT, MaAnPham });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Check_Doituong(string MaDT, string TenDT)
        {
            try
            {
                return HPCDataProvider.Instance().GetDataSet("T_Doituong", "Ten_Doituong, Ma_Doituong", " Ma_Doituong = N'" + MaDT + "' or Ten_Doituong = N'" + TenDT + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CheckExists_Madoituong(int _ID, int _loai)
        {
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_CheckExists_MaDoituong", new string[] { "@ID", "@Loai" }, new object[] { _ID, _loai });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public int Check_Madoituong(int _ID, string _MaDT)
        {
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_CheckMaDoituong", new string[] { "@ID", "@MaDT" }, new object[] { _ID, _MaDT });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public int Check_STT(int _ID, int _STT)
        {
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_CheckSTT", new string[] { "@ID", "@STT" }, new object[] { _ID, _STT });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public DataSet Select_TenDoiTuong_By_Stt(string _stt)
        {
            try
            {
                int stt = int.Parse(_stt);
                return HPCDataProvider.Instance().GetDataSet("T_Doituong", "Ten_Doituong", " STT = '" + stt + "' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Select_TenDoiTuong(string _tenDT)
        {
            try
            {
                return HPCDataProvider.Instance().GetDataSet("T_Doituong", "Ten_Doituong", " Ten_Doituong like '%" + _tenDT + "%' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Select_MaDoiTuong(string _maDT)
        {
            try
            {
                return HPCDataProvider.Instance().GetDataSet("T_Doituong", "Ma_Doituong", " Ma_Doituong like '%" + _maDT + "%' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Select_STT(string _maDT)
        {
            try
            {
                return HPCDataProvider.Instance().GetDataSet("T_Doituong", "STT", " Ma_Doituong like '%" + _maDT + "%' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
