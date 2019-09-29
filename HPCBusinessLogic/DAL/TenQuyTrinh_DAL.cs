using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class TenQuyTrinh_DAL
    {
        public DataSet BindGridT_TenQuyTrinh(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_Ten_QuyTrinhDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public T_Ten_Quytrinh GetOneFromT_TenQuyTrinhByID(Int32 ID)
        {
            try
            {
                return (T_Ten_Quytrinh)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Ten_Quytrinh", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertUpdateT_TenQuyTrinh(T_Ten_Quytrinh _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_Ten_QuyTrinh");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_TenQuyTrinh(int _ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_SelectOneFromT_Ten_QuyTrinh", new string[] { "@ID" }, new object[] { _ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
