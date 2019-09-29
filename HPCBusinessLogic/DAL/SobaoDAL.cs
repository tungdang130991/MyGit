﻿using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic
{
    public class SobaoDAL
    {
        public DataSet BindGridT_Sobao(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_SobaoDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public int InsertT_Sobao(T_Sobao _Obj)
        {
            try
            {
                return HPCDataProvider.Instance().InsertObjectReturn(_Obj, "[Sp_InsertT_Sobao]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOneFromT_Sobao(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_DeleteOneFromT_Sobao]", new string[] { "@Ma_Sobao" }, new object[] { _Id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Sobao GetOneFromT_SobaoByID(Int32 ID)
        {
            try
            {
                return (T_Sobao)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Sobao", "Ma_Sobao");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
