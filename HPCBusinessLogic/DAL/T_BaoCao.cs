using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class T_BaoCao
    {
        public DataSet BindGridT_ActionHistory(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_List_ActionHistory]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_ActionHistory_GocAnh(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_List_ActionHistory_T_Album_Categories]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_ActionHistory_TsAnh(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_List_ActionHistory_T_Photo_Event]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_ActionHistory_AmThanhHinhAnh(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_List_ActionHistory_T_Multimedia]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_ActionHistory_Detail(double News_ID,int type)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ActionHistory_Details]", new string[] { "@News_ID", "@Type" }, new object[] { News_ID, type });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_Logfiles(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_Logfiles]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
