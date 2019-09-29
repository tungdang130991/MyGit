using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class CongviecDAL
    {
        public DataSet BindGridT_Congviec(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[Sp_ListT_CongviecDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public double InsertT_Congviec(T_Congviec _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "Sp_InsertT_Congviec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_Congviec(double _Id, int _Nguoitao)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Congviec", new string[] { "@Ma_Congviec", "@NguoiTao" }, new object[] { _Id, _Nguoitao });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Congviec GetOneFromT_CongviecByID(Double ID)
        {
            try
            {
                return (T_Congviec)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Congviec", "Ma_Congviec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Check_Nguoitao_Congviec(double _Id, int _Nguoitao)
        {
            DataSet _ds;
            bool _return = true;
            try
            {
                _ds = HPCDataProvider.Instance().ExecSqlDataSet("select Ma_Congviec  from T_Congviec where [Ma_Congviec]= " + _Id.ToString() + " and NguoiTao = " + _Nguoitao.ToString() + "");
                if (_ds.Tables[0].Rows.Count > 0)
                    _return = false;
                else
                    _return = true;
                return _return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Check_Deadline(string _Id)
        {
            try
            {
                return HPCDataProvider.Instance().GetColumnValues("T_Congviec", "NgayHoanthanh", "Ma_Congviec = " + _Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStatusCongViec(int _id, int _status)
        {
            try
            {
                HPCDataProvider.Instance().ExecSql("update T_Congviec set Status = " + _status.ToString() + " where Ma_Congviec = " + _id.ToString());//GetColumnValues("T_Congviec", "NgayHoanthanh", "Ma_Congviec = " + _Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
