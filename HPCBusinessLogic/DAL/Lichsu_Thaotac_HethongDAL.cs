using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using System.Collections;

namespace HPCBusinessLogic
{
  public  class Lichsu_Thaotac_HethongDAL
    {
      public void InserT_Lichsu_Thaotac_Hethong(T_Lichsu_Thaotac_Hethong objActionHistory)
        {
            HPCDataProvider.Instance().InsertObject(objActionHistory, "Sp_InsertT_Lichsu_Thaotac_Hethong");
        }
        public T_Lichsu_Thaotac_Hethong GetOneFromT_ActionHistoryByID(Int32 Log_ID)
        {
            try
            {
                return (T_Lichsu_Thaotac_Hethong)HPCDataProvider.Instance().GetObjectByID(Log_ID.ToString(), "T_Lichsu_Thaotac_Hethong", "Log_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_ActionHistoryByID(Int32 Log_ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_ActionHistory", new string[] { "@Log_ID" }, new object[] { Log_ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRow_T_ActionHistory(T_Lichsu_Thaotac_Hethong _t_ActionHistory)
        {
            try
            {
                HPCDataProvider.Instance().UpdateObject(_t_ActionHistory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_ActionHistoryDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_ActionHistoryDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public DataSet BindGridT_ActionHistory(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_Lichsu_Thaotac_HethongDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
