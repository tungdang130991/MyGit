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
   public class LichsuthanhtoanDAL
    {
       public DataSet BindGridT_LichsuThanhtoan(int PageIndex, int PageSize, string WhereCondition)
       {
           try
           {
               return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_LichsuThanhtoanDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int Sp_InsertT_LichsuThanhtoan(T_LichsuThanhtoan obj)
       {
           return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_LichsuThanhtoan");
       }
       public T_LichsuThanhtoan GetOneFromT_LichsuthanhtoanByID(Int32 ID)
       {
           try
           {
               return (T_LichsuThanhtoan)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_LichsuThanhtoan", ID.ToString(), "T_LichsuThanhtoan", "ID");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateT_LichsuThanhtoan(int id, double sotien, DateTime Ngaythu, int nguoithu, string Nguoithanhtoan)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("Sp_UpdateT_LichsuThanhtoanDynamic", new string[] { "@ID","@SOTIEN","@NGAYTHU","@NGUOITHU","@TENNGUOINOP" }, new object[] {id,sotien,  Ngaythu,  nguoithu,  Nguoithanhtoan });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void Sp_DeleteOneFromT_LichsuThanhtoan(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_LichsuThanhtoan", new string[] { "@ID" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
