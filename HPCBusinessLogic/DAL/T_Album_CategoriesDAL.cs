using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class T_Album_CategoriesDAL
    {
        public DataSet Bind_T_Album_CategoriesDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_Album_CategoriesDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Album_Categories(HPCInfo.T_Album_Categories objAlbum_Categories)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objAlbum_Categories, "[CMS_InsertT_Album_Categories]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public HPCInfo.T_Album_Categories load_T_Album_Categories(Int32 id)
        {

            return (HPCInfo.T_Album_Categories)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_Album_Categories]", id.ToString(), "T_Album_Categories", "Cat_Album_ID");

        }
        public void DeleteFrom_T_Album_Categories(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_Album_Categories]", new string[] { "@Cat_Album_ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFromT_T_Album_CategoriesDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Update_Album_CategoriesDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_T_Album_Categories_From_T_Album_Categories(int Cat_Album_ID, int CopyFrom, int Lang_Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Insert_T_Album_Categories_From_T_Album_Categories]", new string[] { "@Cat_Album_ID", "@CopyFrom", "@Lang_Id " }, new object[] { Cat_Album_ID, CopyFrom, Lang_Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckDelete_T_Album_Categories(int prm_EventID)
        {
            DataSet _dsEventContent = null;

            try
            {
                _dsEventContent = HPCDataProvider.Instance().GetStoreDataSet("[CMS_CheckDelete_T_Album_Categories]", new string[] { "@Cat_album_ID" }, new object[] { prm_EventID });
                if (_dsEventContent.Tables[0].Rows.Count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public bool CheckExitscat_album_name(string cat_album_name)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_CheckExitscat_album_name]", new string[] { "@cat_album_name" }, new object[] { cat_album_name });
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public bool Update_Status(int catid, int status, int userid)
        {
            bool _ds = true;
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_Update_Status_Album_App", new string[] { "@CatID", "@status", "@UserID" },
                    new object[] { catid, status, userid });

            }
            catch (Exception ex)
            {
                _ds = false;
            }
            return _ds;
        }
        public double Copy_To_T_Album_Categories(int Cat_Album_ID, int Lang_ID, int Status, DateTime DateModify, int UserCreated, int UserModify)
        {
            double _ID = 0.0;
            try
            {
                _ID = HPCDataProvider.Instance().ExecStoreReturn("[CMS_Copy_T_Album]", new string[] { "@Cat_Album_ID", "@Lang_ID", "@Cat_Album_Status_Approve", "@DateModify", "@UserCreated", "@UserModify" }, new object[] { Cat_Album_ID, Lang_ID, Status, DateModify, UserCreated, UserModify });
            }
            catch
            {
                _ID = 0.0;
            }
            return _ID;
        }
    }
}
