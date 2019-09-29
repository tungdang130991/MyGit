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
    public class QuangCaoDAL
    {
        public DataSet BindGridT_Quangcao(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_QuangcaoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_PhienBanQuangcao(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_PhienBanQuangCaoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_InsertT_PhienBanQuangCao(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_PhienBanQuangCao", new string[] { "@Ma_Quangcao" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_InsertT_FileQuangCao(T_FileQuangCao obj)
        {
            HPCDataProvider.Instance().InsertObject(obj, "Sp_InsertT_FileQuangCao");
        }
        public DataSet Sp_SelectT_Publish_QuangCaoDynamic(string WhereCondition, string order)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectT_Publish_QuangCaoDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_UpdateRowT_QuangCao(double Ma_Quangcao, int Nguoitao, int trangthai)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateRowT_QuangCao", new string[] { "@Ma_Quangcao ", "@Nguoitao", "@Trangthai" }, new object[] { Ma_Quangcao, Nguoitao, trangthai });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_QuangCao(T_QuangCao obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_QuangCao");
        }
        public T_QuangCao GetOneFromT_QuangCaoByID(double ID)
        {
            try
            {
                return (T_QuangCao)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_QuangCao", ID.ToString(), "T_QuangCao", "Ma_Quangcao");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_SelectT_FileQuangCaoDynamic(string WhereCondition, string order)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectT_FileQuangCaoDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_DeleteOneFromT_FileQuangCao(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_FileQuangCao", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Sp_InsertT_Publish_QuangCao(T_Publish_QuangCao obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_Publish_QuangCao");
        }
        public void Sp_DeleteOneFromT_Publish_QuangCao(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Publish_QuangCao", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_QuangCaoByID(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_QuangCao", new string[] { "@Ma_Quangcao" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertQuangCaoSoBao(int Ma_Quangcao,int Ma_Sobao ,string Trang ,int Tien ,int Ma_Kichco)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_QuangCao_Sobao", new string[] { "@Ma_Quangcao" ,"@Ma_Sobao" ,"@Trang","@Tien","@Ma_Kichco" }, new object[] { Ma_Quangcao, Ma_Sobao , Trang , Tien , Ma_Kichco });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateTrangthaiQC(double ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateTrangthaiYeuCauQuangCao", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_PhienBanQuangCao Sp_SelectOneFromT_PhienBanQuangCao(double ID)
        {
            try
            {
                return (T_PhienBanQuangCao)HPCDataProvider.Instance().GetObjectByID("Sp_SelectOneFromT_PhienBanQuangCao", ID.ToString(), "T_PhienBanQuangCao", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
