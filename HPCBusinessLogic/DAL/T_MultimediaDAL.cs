using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HPCInfo;
using HPCShareDLL;
using System.Collections;
using HPCBusinessLogic.DAL;
using HPCServerDataAccess;

namespace HPCBusinessLogic.DAL
{
    public class T_MultimediaDAL
    {
        public DataSet BindGridT_Multimedia(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_MultimediaDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Multimedia(T_Multimedia objNews)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objNews, "[CMS_InsertT_Multimedia]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public T_Multimedia GetOneFromT_MultimediaByID(Double ID)
        {
            try
            {
                return (T_Multimedia)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Multimedia", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Multimedia(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_Multimedia]", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStatusMultimedia(int AdsID, int Status, double userSend)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Send2ApproveMultimedia]", new string[] { "@Ads_ID", "@Status", "@UserModify" }, new object[] { AdsID, Status, userSend });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string getTotalRecord(int TypeRecord, double userID)
        {
            string strTemp = "0";
            DataSet objDataset = null;
            try
            {
                objDataset = HPCDataProvider.Instance().GetStoreDataSet("[CMS_getTotalRecordByStatus_Multimedia]", new string[] { "@TypeRecord", "@UserID" }, new object[] { TypeRecord, userID });
                strTemp = objDataset.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objDataset.Dispose();
                objDataset = null;
            }
            return strTemp;
        }
        public double Copy_To_T_Multimedia(int Cat_Album_ID, int Lang_ID, int Status, DateTime DateModify, int UserCreated, int UserModify)
        {
            double _ID = 0.0;
            try
            {
                _ID = HPCDataProvider.Instance().ExecStoreReturn("[CMS_Copy_T_Multimedia]", new string[] { "@ID", "@Lang_ID", "@Status", "@DateModify", "@UserCreated", "@UserModify" }, new object[] { Cat_Album_ID, Lang_ID, Status, DateModify, UserCreated, UserModify });
            }
            catch
            {
                _ID = 0.0;
            }
            return _ID;
        }
    }
}
