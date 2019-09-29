using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class AutoSavesDAL
    {
        public DataSet BindT_AutoSavesDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_AutoSavesDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_AutoSaves GetOneFromT_AutoSavesByID(double _ID)
        {
            try
            {
                return (T_AutoSaves)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_AutoSaves", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_AutoSavesByID(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_AutoSaves]", new string[] { "@ID" }, new object[] { ID });
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
        }
        public int InsertAutoSaves(T_AutoSaves obj)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(obj, "[CMS_InsertUpdateT_AutoSaves]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _inserted;
        }
        public int Get_ID_AutoSave(int News_ID, int User_ID)
        {
            int id = 0;
            string sql = "select ID from T_AutoSaves where News_ID=" + News_ID + " and UserID=" + User_ID + "";
            UltilFunc _ul = new UltilFunc();
            DataTable dt = _ul.ExecSqlDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                id = int.Parse(dt.Rows[0][0].ToString());
            }
            return id;
        }
    }
}
