using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
  public class AnPhamMauDAL
    {
        public DataSet BindGridT_Anphammau(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_AnPhamMauDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }

        public int InsertT_Anphammau(T_AnPhamMau _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_Insert_UpdateT_AnPhamMau");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_Anphammau(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_AnPhamMau", new string[] { "@Ma_Mau" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
