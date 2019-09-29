using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;


namespace HPCBusinessLogic
{
   public class DatbaoDAL
    {
       public DataSet BindGridT_Datbao(int PageIndex, int PageSize, string WhereCondition)
       {
           DataSet _ds = null;
           try
           {
               _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_DatbaoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return _ds;
       }
       public int InsertT_Datbao(T_Datbao _Obj)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Datbao]");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteOneFromT_Datbao(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Datbao", new string[] { "@ID" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_Datbao GetOneFromT_DatbaoByID(int ID)
       {
           try
           {
               return (T_Datbao)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Datbao", "ID");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
