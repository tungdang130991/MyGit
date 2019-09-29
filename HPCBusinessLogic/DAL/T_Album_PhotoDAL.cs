using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class T_Album_PhotoDAL
    {
        public DataSet Bind_T_Album_PhotoDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_Album_PhotoDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Bind_T_Album_Photo(int CatID)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_Album_Photo]", new string[] { "@CatID" }, new object[] { CatID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Album_Photo(HPCInfo.T_Album_Photo objAlbum_photo)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objAlbum_photo, "[CMS_InsertT_Album_Photo]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public HPCInfo.T_Album_Photo load_T_Album_Photo(Int32 id)
        {

            return (HPCInfo.T_Album_Photo)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_Album_Photo]", id.ToString(), "T_Album_Photo", "Alb_Photo_ID");

        }
        public void DeleteFrom_T_Album_Photo(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_Album_Photo]", new string[] { "@Alb_Photo_ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_T_Album_Photo_From_T_Album_Photo(int Cat_Album_ID, int CopyFrom, int Lang_Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Insert_T_Album_Photo_From_T_Album_Photo]", new string[] { "@Alb_Photo_ID", "@CopyFrom", "@Lang_Id " }, new object[] { Cat_Album_ID, CopyFrom, Lang_Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStatus_AlbumPhoto(int Cat_Album_ID, int Status, int Creator, DateTime Date_Create)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_UpdateStatus_AlbumPhoto]", new string[] { "@Alb_Photo_ID", "@Status", "@Creator", "@Date_Create" }, new object[] { Cat_Album_ID, Status, Creator, Date_Create });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Album_Photo GetOneFromT_Album_PhotoByID(Int32 _ID)
        {
            try
            {
                return (T_Album_Photo)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_Album_Photo", "Alb_Photo_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatusNoiBat_AlbumPhoto(int Cat_Album_ID, int Status, int Creator, DateTime Date_Create)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_UpdateStatusNoiBat_AlbumPhoto]", new string[] { "@Alb_Photo_ID", "@Status", "@Creator", "@Date_Create" }, new object[] { Cat_Album_ID, Status, Creator, Date_Create });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Copy_To_T_Album_Photo(int Cat_Album_ID, int Cat_Album_ID_New, int Lang_ID, DateTime DateModify, int UserModify)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Copy_T_Album_Photo]", new string[] { "@Cat_Album_ID", "@Cat_Album_ID_New", "@Lang_ID", "@DateModify", "@UserModify" }, new object[] { Cat_Album_ID, Cat_Album_ID_New, Lang_ID, DateModify, UserModify });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
