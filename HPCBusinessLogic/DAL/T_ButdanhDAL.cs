using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using System.Data;
using HPCShareDLL;

namespace HPCBusinessLogic.DAL
{
    public class T_ButdanhDAL
    {
        public int Insert_Butdang(T_Butdanh _obj)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(_obj, "CMS_InsertT_Butdanh");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }

        public T_Butdanh GetOneFrom_BDID(Double _ID)
        {
            try
            {
                return (T_Butdanh)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_Butdanh", "BD_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Bin_T_ButdanhDynamic(string userID, string _name, bool all)
        {
            string where = " 1 =1 ";
            if (!all)
            {
                where += " and UserID =" + userID;
            }
            where += " and BD_Name Like N'%" + _name + "%'";
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_ButdanhDynamic]", new string[] { "@where" }, new object[] { where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetBD_Name(Double _ID)
        {
            T_Butdanh obj = new T_Butdanh();
            try
            {
                obj= (T_Butdanh)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_Butdanh", "BD_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (obj != null)
                return obj.BD_Name;
            else
                return " ";
        }


    }
}
