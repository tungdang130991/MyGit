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
    public class YeucauDAL
    {
        public DataSet BindGridT_Yeucauquangcao(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_YeucauDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_YecauQuangCao(T_Yeucau obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_Yeucau");
        }
        public T_Yeucau GetOneFromT_YeuCauByID(double ID)
        {
            try
            {
                return (T_Yeucau)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_Yeucau", ID.ToString(), "T_Yeucau", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_YeuCauQCByID(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Yeucau", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateTrangthaiYeucauQC(double ID, int @Trangthai)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateTrangthaiYeuCauQuangCao", new string[] { "@ID", "@Trangthai" }, new object[] { ID, Trangthai });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateinfoT_Yeucau(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_UpdateT_YeucauDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
