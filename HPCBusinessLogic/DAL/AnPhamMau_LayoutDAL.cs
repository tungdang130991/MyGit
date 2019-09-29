using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public  class AnPhamMau_LayoutDAL
    {
       public int InsertT_Anphammau_Layout(T_AnPhamMau_Layout _Obj)
       {
           try
           {
               return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_AnPhamMau_Layout]");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataSet BindGridT_AnPhamLayout(int PageIndex, int PageSize, string WhereCondition)
       {
           try
           {
               return HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_AnPhamMau_LayoutDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public T_AnPhamMau_Layout GetOneFromT_AnPhamMau_LayoutByID(Int32 ID)
       {
           try
           {
               return (T_AnPhamMau_Layout)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_AnPhamMau_Layout", "ID");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteFromT_AnPhamMau_LayoutByID(Int32 ID)
       {
           try
           {
               HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_AnPhamMau_Layout]", new string[] { "@ID" }, new object[] { ID });

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
