using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class TinBaiAnhDAL
    {
        public void InsertUpdateTin_Anh(T_Tinbai_Anh _Obj)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_Obj, "Sp_InsertT_Tinbai_Anh");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListPhotoByMatinbai(string _where)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListPhotosOfNews", new string[] { "@WhereCondition" }, new object[] { _where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateinfoTin_Anh(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertTinbai_AnhDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_SelectTinAnhDynamic(string WhereCondition, string order)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectTinAnhDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void DeleteAllTinbai_AnhByMatinbai(double _Matin)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteAllMa_TinBai", new string[] { "@Ma_TinBai" }, new object[] { _Matin });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePhotosByMaanh(int _Maanh)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeletePhotoByMa_Anh", new string[] { "@Ma_Anh" }, new object[] { _Maanh });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
