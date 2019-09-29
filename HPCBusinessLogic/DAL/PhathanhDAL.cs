using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class PhathanhDAL
    {
       public DataSet BindGridT_Phathanh(int PageIndex, int PageSize, string WhereCondition)
       {
           DataSet _ds = null;
           try
           {
               _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_PhathanhDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return _ds;
       }
       public int InsertT_Phathanh(T_Phathanh _Obj)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Phathanh]");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteOneFromT_Phathanh(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Phathanh]", new string[] { "@Id" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_Phathanh GetOneFromT_PhathanhByID(Int32 ID)
       {
           try
           {
               return (T_Phathanh)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Phathanh", "Id");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
