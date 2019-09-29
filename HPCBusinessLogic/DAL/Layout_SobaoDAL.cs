using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic
{
   public class Layout_SobaoDAL
    {
        public int InsertT_Anphammau_Layout(T_Layout_SoBao _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Layout_SoBao]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_LayoutSobao(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_Layout_SoBaoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Layout_SoBao GetOneFromT_Layout_SoBaoByID(Int32 ID)
        {
            try
            {
                return (T_Layout_SoBao)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Layout_SoBao", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Layout_SoBaoByID(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Layout_SoBao]", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_Layout_Sobao_FromSobaoForm(int _masobao, int _maAnphamMau)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[sp_InsertT_Layout_Sobao_FromSobaoForm]", new string[] { "@Ma_SoBao", "@Ma_AnPhamMau" }, new object[] { _masobao, _maAnphamMau });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AutoInsertT_Layout_Sobao(int _masobao, int _maAnpham)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[sp_AutoInsert_T_Layout_Sobao]", new string[] { "@Ma_SoBao", "@Ma_AnPham" }, new object[] { _masobao, _maAnpham });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CheckExistsMa_AnPhamMau(int _masobao)
        {
            int _return;
            DataSet _ds;
            try
            {
               _ds =  HPCDataProvider.Instance().ExecSqlDataSet("select distinct(Ma_Mau) from T_Layout_Sobao where Ma_Mau is not null and  Ma_sobao = " + _masobao.ToString());
               if (_ds.Tables[0].Rows.Count > 0)
                   _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0]);
               else
                   _return = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }

    }
}
