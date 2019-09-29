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
   public class Lichsu_Truycap_HethongDAL
    {
       public void InserT_Lichsu_Truycap_Hethong(T_Lichsu_Truycap_Hethong objActionHistory)
       {
           HPCDataProvider.Instance().InsertObject(objActionHistory, "Sp_InsertT_Lichsu_Truycap_Hethong");
       }
       public DataSet BindGridT_Lichsu_Truycap_Hethong(int PageIndex, int PageSize, string WhereCondition)
       {
           try
           {
               return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_Lichsu_Truycap_HethongDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
