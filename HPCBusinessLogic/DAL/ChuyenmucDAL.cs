using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class ChuyenmucDAL
    {
        public DataSet BindGridT_Cagegorys(int PageIndex, int PageSize, string WhereCondition)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_ChuyenMucDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        public DataTable BindGridT_Cagegorys(int LangID, int AdsLogoID)
        {
            DataSet _ds = null;
            DataTable _dtChild = null;
            DataTable _dtChild2 = null;
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow drtop;
            DataRow dr;
            DataRow dr2;
            DataRow dr3;
            dt.Columns.Add(new DataColumn("Ma_ChuyenMuc", typeof(string)));
            dt.Columns.Add(new DataColumn("Ten_ChuyenMuc", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));
            DataTable _dt = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_getAllCategorysByLangID]", new string[] { "@LangID", "@AdsLogoID" }, new object[] { LangID, AdsLogoID });
                if (_ds != null)
                {
                    _dt = _ds.Tables[0];

                    drtop = dt.NewRow();
                    drtop[0] = "0";
                    drtop[1] = "<b>" + "Trang chủ";
                    drtop[2] = Check_RoleByAdvID(AdsLogoID, 0);
                    dt.Rows.Add(drtop);

                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {

                            dr = dt.NewRow();
                            dr[0] = _dt.Rows[i]["Ma_ChuyenMuc"].ToString();
                            dr[1] = "<b>" + _dt.Rows[i]["Ten_ChuyenMuc"].ToString();
                            if (_dt.Rows[i]["Role"].ToString() == "1")
                                dr[2] = true;
                            else
                                dr[2] = false;
                            dt.Rows.Add(dr);

                            DataSet ds2 = new DataSet();

                            ds2 = HPCDataProvider.Instance().GetStoreDataSet("[Get_ChildCAT_RoleAds]",
                                new string[] { "@ParentID", "@AdsLogoID" }, new object[] { _dt.Rows[i]["Ma_ChuyenMuc"], AdsLogoID });
                            if (ds2 != null && ds2.Tables.Count > 0)
                            {
                                _dtChild = ds2.Tables[0];
                                if (_dtChild.Rows.Count > 0)
                                {
                                    for (int m = 0; m < _dtChild.Rows.Count; m++)
                                    {
                                        dr2 = dt.NewRow();
                                        dr2[0] = _dtChild.Rows[m]["Ma_ChuyenMuc"].ToString();
                                        dr2[1] = "&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild.Rows[m]["Ten_ChuyenMuc"].ToString();
                                        dr2[2] = Check_RoleByAdvID(AdsLogoID, Convert.ToInt32(_dtChild.Rows[m]["Ma_ChuyenMuc"].ToString()));
                                        dt.Rows.Add(dr2);


                                        DataSet ds3 = new DataSet();

                                        ds3 = HPCDataProvider.Instance().GetStoreDataSet("[Get_ChildCAT_RoleAds]",
                                            new string[] { "@ParentID", "@AdsLogoID" }, new object[] { _dtChild.Rows[m]["Ma_ChuyenMuc"], AdsLogoID });

                                        if (ds3 != null && ds3.Tables.Count > 0)
                                        {
                                            _dtChild2 = ds3.Tables[0];
                                            if (_dtChild2.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < _dtChild2.Rows.Count; k++)
                                                {
                                                    dr3 = dt.NewRow();
                                                    dr3[0] = _dtChild2.Rows[k]["Ma_ChuyenMuc"].ToString();
                                                    dr3[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild2.Rows[k]["Ten_ChuyenMuc"].ToString();
                                                    dr3[2] = Check_RoleByAdvID(AdsLogoID, Convert.ToInt32(_dtChild2.Rows[k]["Ma_ChuyenMuc"].ToString()));
                                                    dt.Rows.Add(dr3);


                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ds = null;
                _dt = null;
                _dtChild = null;
                _dtChild2 = null;
            }
            return dt;
        }
        public bool Check_RoleByAdvID(int _advID, int _catID)
        {
            bool _check = false;
            try
            {
                string sql = "select Cat_ID from T_Customer_Ads where ID  = " + _advID;
                DataSet ds = HPCDataProvider.Instance().ExecSqlDataSet(sql);
                string _list = ds.Tables[0].Rows[0][0].ToString();
                char[] sep = { ',' };
                string[] sArrProdID = null;
                if (_list.ToString() != "")
                {
                    sArrProdID = _list.Split(sep);
                    for (int i = 0; i < sArrProdID.Length; i++)
                    {
                        if (sArrProdID[i].ToString().ToUpper() == _catID.ToString().ToUpper())
                        {
                            _check = true;
                            break;
                        }

                    }
                }
                ds.Clear();
            }
            catch
            {
                //throw ex;
                _check = false;
            }
            return _check;
        }
        public void InsertT_Chucnang(T_ChuyenMuc _T_Chuyenmuc)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_T_Chuyenmuc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_ChuyenMuc GetOneFromT_ChuyenmucByID(Int32 ID)
        {
            try
            {
                return (T_ChuyenMuc)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_ChuyenMuc", "Ma_ChuyenMuc");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_ChuyenmucByID(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_ChuyenMuc", new string[] { "@Ma_ChuyenMuc" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGridCategory(DataTable _dt)
        {


            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Ma_ChuyenMuc", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_ChuyenMuc", typeof(string)));
            dt.Columns.Add(new DataColumn("Ma_AnPham", typeof(string)));
            dt.Columns.Add(new DataColumn("HoatDong", typeof(bool)));
            dt.Columns.Add(new DataColumn("HienThi_BDT", typeof(bool)));
            dt.Columns.Add(new DataColumn("ThuTuHienThi", typeof(string)));
            try
            {
                int dem = 0;

                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();

                        dr[0] = _dt.Rows[i]["Ma_ChuyenMuc"].ToString();
                        if (int.Parse(_dt.Rows[i]["Ma_Chuyenmuc_Cha"].ToString()) == 0)
                            dr[1] = "<b>" + _dt.Rows[i]["Ten_ChuyenMuc"].ToString() + "</b>";
                        else
                            dr[1] = _dt.Rows[i]["Ten_ChuyenMuc"].ToString();
                        dr[2] = _dt.Rows[i]["Ma_AnPham"].ToString();
                        dr[3] = bool.Parse(_dt.Rows[i]["Hoatdong"].ToString());
                        dr[4] = bool.Parse(_dt.Rows[i]["HienThi_BDT"].ToString());
                        dr[5] = _dt.Rows[i]["ThuTuHienThi"].ToString();
                        dt.Rows.Add(dr);
                        dt = GetChild(dt, dr, _dt.Rows[i]["Ma_ChuyenMuc"].ToString(), dem);
                    }


                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dt;

        }

        public DataTable GetChild(DataTable dt, DataRow dr, string ParentID, int Rank)
        {
            UltilFunc ulti = new UltilFunc();

            if (HPCBusinessLogic.UltilFunc.GetLatestID("T_ChuyenMuc", "Ma_ChuyenMuc", "WHERE Ma_Chuyenmuc_Cha=" + ParentID) > 0)
            {
                try
                {
                    DataTable _dt = ulti.GetDataSet("T_ChuyenMuc", "*", " Ma_Chuyenmuc_Cha=" + ParentID.ToString() + " order by Ten_ChuyenMuc").Tables[0];

                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        Rank++;

                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            dr = dt.NewRow();
                            dr[0] = _dt.Rows[i]["Ma_ChuyenMuc"].ToString();
                            string blank = "&nbsp;&nbsp;&nbsp;&nbsp;";
                            for (int k = 1; k < Rank; k++)
                            {
                                blank = "&nbsp;&nbsp;&nbsp;&nbsp;" + blank;
                            }
                            dr[1] = System.Web.HttpUtility.HtmlDecode(blank) + _dt.Rows[i]["Ten_ChuyenMuc"].ToString();
                            dr[2] = _dt.Rows[i]["Ma_AnPham"].ToString();
                            dr[3] = bool.Parse(_dt.Rows[i]["HoatDong"].ToString());
                            dr[4] = bool.Parse(_dt.Rows[i]["HienThi_BDT"].ToString());
                            dr[5] = _dt.Rows[i]["ThuTuHienThi"].ToString();
                            dt.Rows.Add(dr);
                            dt = GetChild(dt, dr, _dt.Rows[i]["Ma_ChuyenMuc"].ToString(), Rank);

                        }
                        Rank--;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public int Insert_T_Chuyenmuc(T_ChuyenMuc obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_ChuyenMuc");
        }
        public void UpdateFromT_ChuyenMucDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_ChuyenMucDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckExists_ChuyenMuc(int _ID)
        {
            DataSet _ds = null;
            int _return;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_Check_Chuyenmuc", new string[] { "@Ma_ChuyenMuc" }, new object[] { _ID });
                _return = Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public string GetCateNameByID(int _ID)
        {

            try
            {
                return UltilFunc.GetCategoryNameByAdsID(_ID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
