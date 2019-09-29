using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class LayoutDAL
    {
       public DataSet BindGridT_Layout(int PageIndex, int PageSize, string WhereCondition)
       {
           DataSet _ds = null;
           try
           {
               _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_LayoutDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return _ds;
       }
       public int InsertT_Layout(T_Layout _Obj)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Layout]");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void InsertT_Layout(int _ma, string _mota, double _d, double _r)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_InsertT_Layout]", new string[] { "@Ma_Layout", "@Mota", "@Chieudai", "@ChieuRong" }, new object[] {_ma, _mota, _d, _r });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteOneFromT_Layout(int _Id)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Layout]", new string[] { "@Ma_Layout" }, new object[] { _Id });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_Layout GetOneFromT_LayoutByID(Int32 ID)
       {
           try
           {
               return (T_Layout)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Layout", "Ma_Layout");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateinfoT_Layout(string WhereCondition)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_UpdateT_LayoutDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int CheckExists_Layout(int _ID)
      { 
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_CheckExists_Layout]", new string[] { "@Ma_Layout" }, new object[] { _ID });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
       
        }
    }
}
