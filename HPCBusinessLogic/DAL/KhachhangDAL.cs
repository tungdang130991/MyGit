using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class KhachhangDAL
    {
        public DataSet BindGridT_Khachhang(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_KhachHangDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public int InsertT_Khachhang(T_KhachHang _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_KhachHang]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_KhachHang(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_KhachHang]", new string[] { "@Ma_KhachHang" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_KhachHang GetOneFromT_KhachHangByID(Int32 ID)
        {
            try
            {
                return (T_KhachHang)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_KhachHang", "Ma_KhachHang");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Select_Loai_KhachHang(string _prefixText)
        {            
            try
            {
                return HPCDataProvider.Instance().GetDataSet("T_KhachHang", "Ten_KhachHang, Ma_KhachHang ", " Loai_KH = 2 AND Ten_KhachHang like N'%" + _prefixText + "%'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
