using System;
using HPCInfo;
using System.Data;
using HPCShareDLL;
namespace HPCBusinessLogic
{

    public class NguoidungChucnangDAL
    {
        public void InsertT_Nguoidung_Chucnang(T_Nguoidung_Chucnang _T_UserMenu)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_T_UserMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Nguoidung_Chucnang GetOneFromT_UserMenuByID(Int32 UserMenu_ID)
        {
            try
            {
                return (T_Nguoidung_Chucnang)HPCDataProvider.Instance().GetObjectByID(UserMenu_ID.ToString(), "T_UserMenu", "UserMenu_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Nguoidung_ChucnangByID(Int32 UserMenu_ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_UserMenu", new string[] { "@UserMenu_ID" }, new object[] { UserMenu_ID });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRow_T_Nguoidung_Chucnang(T_Nguoidung_Chucnang _T_UserMenu)
        {
            try
            {
                HPCDataProvider.Instance().UpdateObject(_T_UserMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Nguoidung_ChucnangDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_UserMenuDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFromT_Nguoidung_ChucnangDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_UserMenuDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BindGridT_Nguoidung_Chucnang(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_UserMenuDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });				
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
