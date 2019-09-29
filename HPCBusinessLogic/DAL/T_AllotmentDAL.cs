using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic.DAL
{
	public class T_AllotmentDAL
	{
		public int InsertT_Allotment(T_Allotments _objT_Allotment)
		{
			int _inserted;
			try
			{
				_inserted = HPCDataProvider.Instance().InsertObjectReturn(_objT_Allotment, "Sp_InsertT_Allotment"); 
			}
			catch(Exception ex) 
			{
				throw ex; 
			} 
			return _inserted;
		}
		public DataSet BindGridT_AllotmentEditor(int PageIndex, int PageSize, string WhereCondition)
		{
			try
			{
				return HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_AllotmentDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		public void DeleteFromT_Allotment(Int32 ID)
		{
			try
			{
				HPCDataProvider.Instance().ExecStore("Sp_DeleteOneT_Allotment", new string[]{ "@ID"},new object[] { ID });
			} 
			catch (Exception ex) 
			{ 
				throw ex; 
			}
		}
		public void DeleteT_AllotmentDynamic(string where) 
		{ 
			try 
			{
				HPCDataProvider.Instance().ExecStore("[Sp_DeleteT_AllotmentDynamic]",new string[] { "@WhereCondition" }, new object[] { where });
			}
			catch(Exception ex)
			{
				 throw ex;
			}
		}
		public T_Allotments GetOneFromT_AllotmentByID(Int32 _ID)
		{
			try
			{
				return (T_Allotments)HPCDataProvider.Instance().GetObjectByID(_ID.ToString(), "T_Allotments", "ID");
			}
			catch (Exception ex)
			{
				 throw ex;
			}
		}
        public T_Allotments GetOneFromT_AllotmentByIdieaID(Int32 _ID)
        {
            try
            {
                return (T_Allotments)HPCDataProvider.Instance().GetObjectByID("[Sp_SelectOneFromT_AllotmentsByDealID]",_ID.ToString(), "T_Allotments", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Status_tintuc(double ID, int Status, int nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_Update_Status_T_Allotment", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua" }, new object[] { ID, Status, nguoisua, ngaysua });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}
