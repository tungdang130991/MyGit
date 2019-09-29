using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic.DAL
{
    public class T_IdieaDAL
    {
        public int InsertT_Idiea(T_Idiea _objT_Idiea)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(_objT_Idiea, "Sp_InsertT_Idiea");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _inserted;
        }
        public DataSet BindGridT_IdieaEditor(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_IdieaDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool BinT_Idiea(double _id, int _status)
        {
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_List_Idiea", new string[] { "@CV_id", "@Status" }, new object[] { _id, _status });
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public void DeleteFromT_Idiea(Int32 Diea_ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneT_Idiea", new string[] { "@Diea_ID" }, new object[] { Diea_ID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteT_IdieaDynamic(string where)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteT_IdieaDynamic]", new string[] { "@WhereCondition" }, new object[] { where });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Idiea GetOneFromT_IdieaByID(Int32 _ID)
        {
            try
            {
                return (T_Idiea)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_Idiea", "Diea_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_IdieaVersion GetOneFromT_IdieaVersionByIDVersion(Int32 _ID, Int32 _Status, Int32 _Action)
        {
            try
            {
                return (T_IdieaVersion)HPCDataProvider.Instance().GetObjectByCondition("T_IdieaVersion", " Diea_ID =" + _ID + " And Action=" + _Action + " And Status =" + _Status + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GetOneFromT_IdieaVersionByID(int prmNes_ID, int pmr_News_Statu, int pmr_Action)
        {
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectOneFromT_IdieaVersion", new string[] { "@Diea_ID", "@Action", "@Status" }, new object[] { prmNes_ID, pmr_Action, pmr_News_Statu });
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public void Update_Status_tintuc(double ID, int Status, int nguoisua, DateTime ngaysua, int _num)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Update_Status_T_Idiea", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua", "@Number" }, new object[] { ID, Status, nguoisua, ngaysua, _num });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Status_Detai(double ID, int Status, int nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_Update_Status_T_IdieaVersion]", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua" }, new object[] { ID, Status, nguoisua, ngaysua });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Version_From_T_idiea_WithUserModify(double ID, int status, int action, int user_edit, DateTime date_edit)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Insert_Version_From_T_idiea_WithUserModify", new string[] { "@ID", "@status", "@action", "@user_edit", "@date_edit" }, new object[] { ID, status, action, user_edit, date_edit });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IsLock(double prmDieaID, int prmIsLock, double userID, DateTime DateEdit)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Lock_T_Idiea", new string[] { "@DieaID", "@isLock", "@User_Edit", "@Date_Edit" }, new object[] { prmDieaID, prmIsLock, userID, DateEdit });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Bin_T_IdieaVersionDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_IdieaVersionDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Lock_Detai(double ID, int Status, int nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_Update_Lock_T_Idiea]", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua" }, new object[] { ID, Status, nguoisua, ngaysua });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTitleByCV_ID(Int32 id, Int32 cv_id)
        {

            string resul = "";
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_GetTitleDynamicByCV_ID]", new string[] { "@ID", "@CV_ID" }, new object[] { id, cv_id });
                resul = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resul;
        }
    }
}
