using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class AnPhamDAL
    {
       public int  InsertT_AnPham(T_AnPham _T_AnPham)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_T_AnPham, "Sp_InsertT_AnPham");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_AnPham GetOneFromT_AnPhamByID(Int32 ID)
       {
           try
           {
               return (T_AnPham)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_AnPham", "Ma_AnPham");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteFromT_AnPhamByID(Int32 ID)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_AnPham", new string[] { "@Ma_AnPham" }, new object[] { ID });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataSet BindGridT_AnPham(int PageIndex, int PageSize, string WhereCondition)
       {
           try
           {
               return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectAllT_AnPham", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public int CheckExists_AnPham(int _ID)
       { 
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[sp_CheckExists_AnPham]", new string[] { "@Ma_AnPham" }, new object[] { _ID });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;}
        }
    
}
