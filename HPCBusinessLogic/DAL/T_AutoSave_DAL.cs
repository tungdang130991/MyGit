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
    public class T_AutoSave_DAL
    {
        public DataSet Sp_ListT_AutoSaveDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_AutoSaveDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Sp_InsertT_AutoSave(T_AutoSave obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_AutoSave");
        }
        public T_AutoSave Sp_SelectOneFromT_AutoSave(Double ID)
        {

            return (T_AutoSave)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_AutoSave", ID.ToString(), "T_AutoSave", "ID");

        }
        public void Sp_DeleteOneFromT_AutoSave(int _ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_AutoSave", new string[] { "@ID" }, new object[] { _ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
