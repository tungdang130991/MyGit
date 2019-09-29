using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class T_CustomersDAL
    {
        public DataSet Bind_T_CustomersDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_CustomersDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Customers(HPCInfo.T_Customers obj)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(obj, "[CMS_InsertT_Customers]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public HPCInfo.T_Customers load_T_Customers(Int32 id)
        {

            return (HPCInfo.T_Customers)HPCDataProvider.Instance().GetObjectByID(id.ToString(), "T_Customers", "ID");

        }
        public void DeleteFrom_T_Customers(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_Customers]", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
