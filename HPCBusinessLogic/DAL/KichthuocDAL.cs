using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class KichthuocDAL
    {
       public DataSet BindGridT_Kichthuoc(int PageIndex, int PageSize, string WhereCondition)
       {
           DataSet _ds = null;
           try
           {
               _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_KichthuocDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return _ds;
       }

       public int InsertT_Kichthuoc(T_Kichthuoc _Obj)
       {
           try
           {
              return   HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_Kichthuoc");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteOneFromT_Kichthuoc(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Kichthuoc", new string[] { "@Ma_Kichthuoc" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_Kichthuoc GetOneFromT_KichthuocByID(Int32 ID)
       {
           try
           {
               return (T_Kichthuoc)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Kichthuoc", "Ma_Kichthuoc");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //public void UpdateinfoT_Layout(string WhereCondition)
       //{
       //    try
       //    {
       //        HPCDataProvider.Instance().ExecStore("[Sp_UpdateT_LayoutDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
    }
}
