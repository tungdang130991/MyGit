using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;


namespace HPCBusinessLogic
{
   public class HopdongDAL
    {
       public DataSet BindGridT_Hopdong(int PageIndex, int PageSize, string WhereCondition)
       {
           DataSet _ds = null;
           try
           {
               _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_HopdongDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return _ds;
       }
       public int InsertT_Hopdong(T_Hopdong _Obj)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Hopdong]");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteOneFromT_Hopdong(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Hopdong]", new string[] { "@ID" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_Hopdong GetOneFromT_HopdongByID(Int32 ID)
       {
           try
           {
               return (T_Hopdong)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Hopdong", "ID");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
      // public bool Check_Hieu_luc_Hop_dong(int _ID)
       //{
       //    bool _return = false;          
       //    try
       //    {
       //        T_Hopdong _hopdong = new T_Hopdong();
       //        _hopdong = HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Hopdong", "ID");
               
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}
    }
}
