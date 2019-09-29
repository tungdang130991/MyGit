using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class PhongBan_DAL
    {
        public DataSet BindGridT_PhongBan(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_PhongbanDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public int InsertT_Phongban(T_Phongban _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_Phongban");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Phongban GetOneFromT_PhongbanByID(double ID)
        {
            try
            {
                return (T_Phongban)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Phongban", "Ma_Phongban");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_Phongban(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Phongban", new string[] { "@Ma_Phongban" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
