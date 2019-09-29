using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HPCInfo;
using HPCShareDLL;
using System.Collections;
using HPCBusinessLogic.DAL;
using HPCServerDataAccess;

namespace HPCBusinessLogic.DAL
{
    public class T_WebLinksDAL
    {
        public DataSet Bind_T_WebLinksDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_WebLinksDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_WebLinks(HPCInfo.T_WebLinks obj)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(obj, "[CMS_InsertT_WebLinks]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public HPCInfo.T_WebLinks load_T_WebLinks(Int32 id)
        {

            return (HPCInfo.T_WebLinks)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_WebLinks]", id.ToString(), "T_WebLinks", "ID");

        }
        public void DeleteFrom_T_WebLinks(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_WebLinks]", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
