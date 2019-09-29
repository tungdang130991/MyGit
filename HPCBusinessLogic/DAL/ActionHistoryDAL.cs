using System;
using HPCInfo;
using System.Data;
using HPCShareDLL;
namespace HPCBusinessLogic
{
    public class ActionHistoryDAL
    {
        public void InserT_Action(T_ActionHistory objActionHistory)
        {
            HPCDataProvider.Instance().InsertObject(objActionHistory, "[CMS_InsertT_ActionHistory]");
        }
        public DataSet BindGridT_ActionHistory(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("CMS_ListT_ActionHistoryDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}