using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class AnhDAL
    {
        public DataSet BindGridT_Anh(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_SelectAllT_Anh]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public DataSet Sp_SelectTulieuanhDynamic(string WhereCondition, string order)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectTuLieuAnh_Dynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, order });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetListImageByTinbai(Int32 ID)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_GetListImageByTinbai", new string[] { "@Ma_TinBai" }, new object[] { ID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Sp_DuyetAnhDinhKem(string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_DuyetAnhDinhKem", new string[] { "@Where" }, new object[] { WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Anh GetOneFromT_AnhByID(Int32 ID)
        {
            try
            {
                return (T_Anh)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Anh", "Ma_Anh");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListPhotoforSelect(string _where)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListPhotosTopForSelect", new string[] { "@WhereCondition" }, new object[] { _where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ListPhotoOfNews(string _where)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListPhotosTopForSelect]", new string[] { "@WhereCondition" }, new object[] { _where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertUpdateT_Anh(T_Anh _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_Insert_UpdateT_Anh");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Anh(int _Maanh)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Anh", new string[] { "@Ma_Anh" }, new object[] { _Maanh });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateinfoT_Anh(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_AnhDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckHasTitles(string _keyword)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("CMS_SelectOneFromT_Anh", new string[] { "@WhereCondition" }, new object[] { _keyword });
                if (_ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}
