using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic
{
    public class T_Photo_EventDAL
    {
        public DataSet BindGridT_Photo_Events(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_Photo_EventDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return _ds;
        }
        //public DataSet BindGridT_Photo_EventsVersion(int PageIndex, int PageSize, string WhereCondition)
        //{
        //    DataSet _ds = null;
        //    try
        //    {
        //        _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_Photo_VersionDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //    }
        //    return _ds;
        //}
        public void UpdateStatus_Photo_Events(double ID, int Status, int Creator, DateTime Date_Update)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[UpdateStatus_Photo_Event]", new string[] { "@Photo_ID", "@Photo_Status", "@Creator", "@Date_Update" }, new object[] { ID, Status, Creator, Date_Update });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Version_From_T_PhotoEvent_WithUserModify(double ID, int status,DateTime date_edit)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_Insert_PhotoVersion_From_T_PhotoEvent_WithUserModify]", new string[] { "@ID", "@status","@date_edit" }, new object[] { ID, status,date_edit });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Photo_Events(T_Photo_Event _obj)
        {
            int _inserted = 0;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(_obj, "[Sp_InsertT_Photo_Event]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _inserted;
        }
        public double InsertT_Photo_Events_SendLanger(int Photo_ID, int CopyFrom, int Lang_ID)
        {
            double _ID = 0.0;
            try
            {
                _ID = HPCDataProvider.Instance().ExecStoreReturn("[Sp_InsertT_Photo_Event_SendLanger]", new string[] { "@Photo_ID", "@Copy_From", "@Lang_ID" }, new object[] { Photo_ID, CopyFrom, Lang_ID });
            }
            catch
            {
                _ID = 0.0;
            }
            return _ID;
        }
        public T_Photo_Event GetOneFromT_Photo_EventsByID(Double ID)
        {
            try
            {
                return (T_Photo_Event)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Photo_Event", "Photo_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Photo_Event(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Photo_Event]", new string[] { "@Photo_ID" }, new object[] { ID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Photo_Version(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Photo_Version]", new string[] { "@Photo_ID" }, new object[] { ID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
