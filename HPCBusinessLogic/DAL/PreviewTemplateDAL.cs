using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
   public class PreviewTemplateDAL
    {
        public DataSet BindGridT_PrevTemplate(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_Prev_TemplateDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public DataSet BindList_PrevTemplate(int _TempType)
        {
            DataSet _ds = null;
            try
            {
                //_ds = HPCDataProvider.Instance().ExecStoreDataSet("Sp_GetListT_Prev_Template");
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_GetListT_Prev_Template", new string[] { "@TempType" }, new object[] { _TempType });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public int InsertT_PrevTemplate(Prev_Template _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertPrev_Template");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_PrevTemplateForNews(Prev_Template _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertPrev_TemplateForNews");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_PrevTemplate(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Prev_Template", new string[] { "@TempID" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Prev_Template GetOneFromT_PrevTemplateByID(Int32 ID)
        {
            try
            {
                return (Prev_Template)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "Prev_Template", "TempID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public DataSet GetOneFromT_PrevTemplateCommon(Int32 TempID, Int32 Ma_Tinbai, Int32 TempType)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_Prev_Template_GetOneCommon", new string[] { "@TempID", "@Ma_Tinbai", "@TempType" }, new object[] { TempID, Ma_Tinbai, TempType });
            }
            catch (Exception ex){ throw ex;}
            return _ds;
        }
    }
}
