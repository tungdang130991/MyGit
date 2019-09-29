using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
namespace HPCBusinessLogic
{
    public class ChucnangDAL
    {
        public void InsertT_Chucnang(T_Chucnang _T_Chucnang)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_T_Chucnang);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Chucnang GetOneFromT_ChucnangByID(Int32 ID)
        {
            try
            {
                return (T_Chucnang)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Chucnang", "Ma_Chucnang");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_ChucnangByID(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Chucnang", new string[] { "@Ma_Chucnang" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRow_T_Chucnang(T_Chucnang _T_Chucnang)
        {
            try
            {
                HPCDataProvider.Instance().UpdateObject(_T_Chucnang);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public DataSet BindGridT_Chucnang(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_ChucnangDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataView BindGridT_Chucnang_dataview(DataTable _dt)
        {
            try
            {
                HPCBusinessLogic.UltilFunc _untilDAL = new HPCBusinessLogic.UltilFunc();
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("Ma_Chucnang", typeof(int)));
                dt.Columns.Add(new DataColumn("Ten_chucnang", typeof(string)));
                dt.Columns.Add(new DataColumn("STT", typeof(string)));
                dt.Columns.Add(new DataColumn("URL_Chucnang", typeof(string)));
                dt.Columns.Add(new DataColumn("Mota", typeof(string)));
                dt.Columns.Add(new DataColumn("HoatDong", typeof(bool)));
                dt.Columns.Add(new DataColumn("Quytrinh", typeof(bool)));
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _dt.Rows[i].ItemArray[0].ToString();
                        dr[1] = "<b>" + _dt.Rows[i].ItemArray[1].ToString();
                        dr[2] = _dt.Rows[i].ItemArray[2].ToString();
                        dr[3] = _dt.Rows[i]["URL_Chucnang"].ToString();
                        dr[4] = _dt.Rows[i]["Mota"].ToString();
                        dr[5] = _dt.Rows[i]["HoatDong"].ToString();
                        dr[6] = _dt.Rows[i]["Quytrinh"].ToString();
                        dt.Rows.Add(dr);
                        //Kiem tra xem chuc nang co chuyen muc con hay khong
                        if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chucnang", "Ma_Chucnang", "WHERE Ma_Chucnang_Cha=" + _dt.Rows[i].ItemArray[0].ToString()) > 0)
                        {
                            DataTable _dtChild = _untilDAL.GetDataSet("T_Chucnang", "*", " Ma_Chucnang_Cha=" + _dt.Rows[i].ItemArray[0].ToString() + " order by STT").Tables[0];
                            if (_dtChild.Rows.Count > 0)
                            {
                                for (int j = 0; j < _dtChild.Rows.Count; j++)
                                {
                                    dr = dt.NewRow();
                                    dr[0] = _dtChild.Rows[j].ItemArray[0].ToString();
                                    dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild.Rows[j].ItemArray[1].ToString();
                                    dr[2] = _dtChild.Rows[j].ItemArray[2].ToString();
                                    dr[3] = _dtChild.Rows[j]["URL_Chucnang"].ToString();
                                    dr[4] = _dtChild.Rows[j]["Mota"].ToString();
                                    dr[5] = _dtChild.Rows[j]["HoatDong"].ToString();
                                    dr[6] = _dtChild.Rows[j]["Quytrinh"].ToString();
                                    dt.Rows.Add(dr);
                                    // Kiem tra xem co chuc nang con cap 3 hay khong
                                    if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chucnang", "Ma_Chucnang", "WHERE Ma_Chucnang_Cha=" + _dtChild.Rows[j].ItemArray[0].ToString()) > 0)
                                    {
                                        DataTable _dtChild1 = _untilDAL.GetDataSet("T_Chucnang", "*", " Ma_Chucnang_Cha=" + _dtChild.Rows[j].ItemArray[0].ToString() + " order by STT").Tables[0];
                                        for (int j1 = 0; j1 < _dtChild1.Rows.Count; j1++)
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = _dtChild1.Rows[j1].ItemArray[0].ToString();
                                            dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild1.Rows[j1].ItemArray[1].ToString();
                                            dr[2] = _dtChild1.Rows[j1].ItemArray[2].ToString();
                                            dr[3] = _dtChild1.Rows[j1].ItemArray[3].ToString();
                                            dr[4] = _dtChild.Rows[j]["Mota"].ToString();
                                            dr[5] = _dtChild.Rows[j]["HoatDong"].ToString();
                                            dr[6] = _dtChild.Rows[j]["Quytrinh"].ToString();
                                            dt.Rows.Add(dr);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                DataView dv = new DataView(dt);
                return dv;
            }
            catch (Exception ex) { throw ex; }
        }

        public int Insert_T_Chucnang(T_Chucnang obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "[Sp_InsertT_Chucnang]");
        }
        public T_Chucnang GetMenusName(string MenuName, int MenuID)
        {
            return (T_Chucnang)HPCDataProvider.Instance().GetObjectByCondition("T_Chucnang", " Ten_chucnang = N'" + MenuName + "'  AND Ma_Chucnang <> " + MenuID + "");
        }
    }
}
