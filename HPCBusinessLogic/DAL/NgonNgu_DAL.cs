using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class NgonNgu_DAL
    {
        public DataSet BindGridT_NgonNgu(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_NgonNguDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public DataSet Sp_SelectT_NgonNguDynamic(string WhereCondition, string Order)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectT_NgonNguDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, Order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public int InsertT_NgonNgu(T_NgonNgu _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_NgonNgu");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_NgonNgu GetOneFromT_NgonNguByID(double ID)
        {
            try
            {
                return (T_NgonNgu)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_NgonNgu", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_NgonNgu(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_NgonNgu", new string[] { "@ID" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
