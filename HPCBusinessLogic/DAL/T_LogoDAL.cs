using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class T_LogoDAL
    {
        public DataSet BindGridT_Logo(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListLogoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }

        public int InsertUpdateT_Logo(HPCInfo.T_Logo _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_Insert_UpdateT_Logo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Logo(int _Malogo)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("sp_deletefromT_Logo", new string[] { "@Ma_Logo" }, new object[] { _Malogo });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateT_Logo(int malogo, string tenlogo)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("sp_update_T_Logo", new string[] { "@Ma_logo", "@Ten_Logo" }, new object[] { malogo,tenlogo });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
