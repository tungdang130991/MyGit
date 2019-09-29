using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using System.Collections;
using SSOLib;
using SSOLib.ServiceAgent;

namespace HPCBusinessLogic.DAL
{
    public class NhomDAL
    {
        UltilFunc _lib = new UltilFunc();
        public void InsertT_Nhom(T_Nhom _T_Nhom)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_T_Nhom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Nhom GetOneFromT_NhomByID(double Group_ID)
        {
            try
            {
                return (T_Nhom)HPCDataProvider.Instance().GetObjectByID(Group_ID.ToString(), "T_Nhom", "Ma_nhom");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_NhomByID(int Group_ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Nhom", new string[] { "@Ma_Nhom" }, new object[] { Group_ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRow_T_Nhom(T_Nhom _T_Nhom)
        {
            try
            {
                HPCDataProvider.Instance().UpdateObject(_T_Nhom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_NhomDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_NhomDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_GroupCategory(int Ma_nhom)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("DeleteFromT_GroupCategoryDynamic", new string[] { "@Ma_nhom" }, new object[] { Ma_nhom });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_GroupQTBT(string where)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Nhom_QTBTDynamic", new string[] { "@WhereCondition" }, new object[] { where });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_GroupLanguages(string where)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Nhom_NgonNguDynamic", new string[] { "@WhereCondition" }, new object[] { where });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFromT_NhomDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_NhomDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_Nhom(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_NhomDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGridMenuByGroup(int GroupID, string langid)
        {
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Ma_Chucnang", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_chucnang", typeof(string)));
            dt.Columns.Add(new DataColumn("STT", typeof(string)));
            dt.Columns.Add(new DataColumn("URL_Chucnang", typeof(string)));
            dt.Columns.Add(new DataColumn("Mota", typeof(string)));
            dt.Columns.Add(new DataColumn("Role_Menu", typeof(bool)));

            dt.Columns.Add(new DataColumn("Doc", typeof(bool)));
            dt.Columns.Add(new DataColumn("Ghi", typeof(bool)));
            dt.Columns.Add(new DataColumn("Xoa", typeof(bool)));

            DataTable _dt = _untilDAL.GetStoreDataSet("sp_BindGridMenuByGroup", new string[] { "@Parrent_ID", "@Group_ID", "@lang_ID" }, new object[] { 0, GroupID, langid }).Tables[0];

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = _dt.Rows[i]["Ma_Chucnang"].ToString();
                    dr[1] = _dt.Rows[i]["Ten_chucnang"].ToString();
                    dr[2] = _dt.Rows[i]["STT"].ToString();
                    dr[3] = _dt.Rows[i]["URL_Chucnang"].ToString();
                    dr[4] = _dt.Rows[i]["Mota"].ToString();

                    dr[5] = Convert.ToBoolean(_dt.Rows[i]["role"]);
                    if (_dt.Rows[i]["Doc"] != DBNull.Value)
                        dr[6] = Convert.ToBoolean(_dt.Rows[i]["Doc"]);
                    else dr[6] = false;
                    if (_dt.Rows[i]["Ghi"] != DBNull.Value)
                        dr[7] = Convert.ToBoolean(_dt.Rows[i]["Ghi"]);
                    else dr[7] = false;
                    if (_dt.Rows[i]["Xoa"] != DBNull.Value)
                        dr[8] = Convert.ToBoolean(_dt.Rows[i]["Xoa"]);
                    else dr[8] = false;

                    dt.Rows.Add(dr);
                    //Kiem tra xem chuc nang co chuyen muc con hay khong
                    if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chucnang", "Ma_Chucnang", "WHERE Ma_Chucnang_Cha=" + _dt.Rows[i].ItemArray[0].ToString()) > 0)
                    {
                        DataTable _dtChild = _untilDAL.GetStoreDataSet("sp_BindGridMenuByGroup", new string[] { "@Parrent_ID", "@Group_ID", "@lang_ID" }, new object[] { _dt.Rows[i].ItemArray[0], GroupID, langid }).Tables[0];
                        if (_dtChild.Rows.Count > 0)
                        {
                            for (int j = 0; j < _dtChild.Rows.Count; j++)
                            {
                                dr = dt.NewRow();
                                dr[0] = _dtChild.Rows[j]["Ma_Chucnang"].ToString();
                                dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild.Rows[j]["Ten_chucnang"].ToString();
                                dr[2] = _dtChild.Rows[j]["STT"].ToString();
                                dr[3] = _dtChild.Rows[j]["URL_Chucnang"].ToString();
                                dr[4] = _dtChild.Rows[j]["Mota"].ToString();
                                dr[5] = Convert.ToBoolean(_dtChild.Rows[j]["role"]);

                                if (_dtChild.Rows[j]["Doc"] != DBNull.Value)
                                    dr[6] = Convert.ToBoolean(_dtChild.Rows[j]["Doc"]);
                                else dr[6] = false;
                                if (_dtChild.Rows[j]["Ghi"] != DBNull.Value)
                                    dr[7] = Convert.ToBoolean(_dtChild.Rows[j]["Ghi"]);
                                else dr[7] = false;
                                if (_dtChild.Rows[j]["Xoa"] != DBNull.Value)
                                    dr[8] = Convert.ToBoolean(_dtChild.Rows[j]["Xoa"]);
                                else dr[8] = false;

                                dt.Rows.Add(dr);

                                //// Kiem tra xem chuc nang hien tai co chuyen muc cap 3 hay khong
                                if (HPCBusinessLogic.UltilFunc.GetLatestID("T_chucnang", "Ma_chucnang", "WHERE Ma_chucnangcha=" + _dtChild.Rows[j].ItemArray[0].ToString()) > 0)
                                {
                                    DataTable _dtChild1 = _untilDAL.GetStoreDataSet("sp_BindGridMenuByGroup", new string[] { "@Parrent_ID", "@Group_ID", "@lang_ID" }, new object[] { _dtChild.Rows[j].ItemArray[0], GroupID, langid }).Tables[0];

                                    for (int j1 = 0; j1 < _dtChild1.Rows.Count; j1++)
                                    {
                                        dr = dt.NewRow();
                                        dr[0] = _dtChild1.Rows[j1]["Ma_chucnang"].ToString();
                                        dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild1.Rows[j1]["Ten_chucnang"].ToString();
                                        dr[2] = _dtChild1.Rows[j1]["STT"].ToString();
                                        dr[3] = _dtChild1.Rows[j1]["URL_Chucnang"].ToString();
                                        dr[4] = _dtChild1.Rows[j1]["Mota"].ToString();
                                        dr[5] = Convert.ToBoolean(_dtChild1.Rows[j1]["role"]);

                                        if (_dtChild1.Rows[j1]["Doc"] != DBNull.Value)
                                            dr[6] = Convert.ToBoolean(_dtChild1.Rows[j1]["Doc"]);
                                        else dr[6] = false;
                                        if (_dtChild1.Rows[j1]["Ghi"] != DBNull.Value)
                                            dr[7] = Convert.ToBoolean(_dtChild1.Rows[j1]["Ghi"]);
                                        else dr[7] = false;
                                        if (_dtChild1.Rows[j1]["Xoa"] != DBNull.Value)
                                            dr[8] = Convert.ToBoolean(_dtChild1.Rows[j1]["Xoa"]);
                                        else dr[8] = false;

                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable BindGridGroupQTBT(int GroupID)
        {
            DataTable _dt = new DataTable();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_Quytrinh", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));

            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridQTBTByGroup", new string[] { "@Group_ID" }, new object[] { GroupID });
                if (_ds != null)
                    _dt = _ds.Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _dt.Rows[i]["ID"].ToString();
                        dr[1] = _dt.Rows[i]["Ten_Quytrinh"].ToString();
                        if (Convert.ToBoolean(_dt.Rows[i]["Role"]))
                            dr[2] = true;
                        else
                            dr[2] = false;

                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable BindGridGroupLanguages(int GroupID)
        {
            DataTable _dt = new DataTable();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("TenNgonNgu", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));

            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridLanguagesByGroup", new string[] { "@Group_ID" }, new object[] { GroupID });
                if (_ds != null)
                    _dt = _ds.Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _dt.Rows[i]["ID"].ToString();
                        dr[1] = _dt.Rows[i]["TenNgonNgu"].ToString();
                        if (Convert.ToBoolean(_dt.Rows[i]["Role"]))
                            dr[2] = true;
                        else
                            dr[2] = false;

                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void XoaChucnangNhomNguoidung(int ID)
        {
            _lib.ExecStore("Sp_DeleteOneFromT_Nhom_Chucnang", new string[] { "@ID" }, new object[] { ID });
        }
        public void InsertT_GroupMenu(int Menu_ID, int Group_ID, bool _R_Edit, bool _R_Del, bool _R_Add)
        {
            _lib.ExecStore("SP_InsertT_GroupMenu", new string[] { "@Menu_ID", "@Group_ID", "@R_Edit", "@R_Del", "@R_Add" }, new object[] { Menu_ID, Group_ID, _R_Edit, _R_Del, _R_Add });
        }
        public void InsertT_GroupCategory(int Categorys_ID, int Group_ID)
        {
            _lib.ExecStore("SP_InsertT_GroupCategory", new string[] { "@Group_ID", "@Categorys_ID" }, new object[] { Group_ID, Categorys_ID });
        }
        public void InsertT_GroupQTBT(int Group_ID, int QTBT_ID)
        {
            _lib.ExecStore("Sp_InsertT_Nhom_QTBT", new string[] { "@Ma_Nhom", "@Ma_QTBT" }, new object[] { Group_ID, QTBT_ID });
        }
        public void InsertT_GroupLanguages(int Group_ID, int Lang_ID, DateTime ngaytao)
        {
            _lib.ExecStore("Sp_InsertT_Nhom_NgonNgu", new string[] { "@Ma_Nhom", "@Ma_NgonNgu", "@Ngaytao" }, new object[] { Group_ID, Lang_ID, ngaytao });
        }
        public int Insert_T_Group(T_Nhom obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "Sp_InsertT_Nhom");
        }
        public T_Nhom GetGroupName(string groupName, int groupId)
        {
            return (T_Nhom)HPCDataProvider.Instance().GetObjectByCondition("T_Nhom", " Ten_Nhom = N'" + groupName + "' AND Ma_nhom <> " + groupId + "");
        }
        public void Sp_AutoInsertCategoryFromGroup(int Categorys_ID, int Group_ID, DateTime datecreate)
        {
            _lib.ExecStore("Sp_AutoInsertCategoryFromGroup", new string[] { "@Categorys_ID", "@Group_ID", "@DateCreated" }, new object[] { Categorys_ID, Group_ID, datecreate });
        }
        public DataTable BindGridCategoryByGroup(int Group_ID, int MaAnPham)
        {
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Ma_Chuyenmuc", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_Chuyenmuc", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(string)));
            dt.Columns.Add(new DataColumn("CategoryParrent", typeof(string)));
            dt.Columns.Add(new DataColumn("OnClick_Event", typeof(string)));
            DataTable _dt = _untilDAL.GetStoreDataSet("sp_BindGridCategoryByGroup", new string[] { "@Ma_AnPham", "@Category_ParrentID", "@Group_ID" }, new object[] { MaAnPham, 0, Group_ID }).Tables[0];

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = _dt.Rows[i]["Ma_chuyenmuc"].ToString();
                    dr[1] = "<b>" + _dt.Rows[i]["Ten_chuyenmuc"].ToString();
                    if (Convert.ToBoolean(_dt.Rows[i]["Role"]))
                        dr[2] = "checked";
                    else
                        dr[2] = "";
                    dr[3] = "Parrent" + _dt.Rows[i]["Ma_chuyenmuc"].ToString();
                    if (UltilFunc.GetLatestID("T_ChuyenMuc", "Ma_chuyenmuc", " WHERE Ma_Chuyenmuc_Cha=" + _dt.Rows[i]["Ma_chuyenmuc"].ToString()) > 0)
                        dr[4] = " ParrentClick(" + _dt.Rows[i]["Ma_chuyenmuc"].ToString() + ")";
                    else
                        dr[4] = " ChildeClick(" + _dt.Rows[i]["Ma_chuyenmuc"].ToString() + ")";
                    dt.Rows.Add(dr);
                    //Kiem tra xem chuc nang co chuyen muc con hay khong
                    if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chuyenmuc", "Ma_chuyenmuc", "WHERE Ma_Chuyenmuc_Cha=" + _dt.Rows[i]["Ma_chuyenmuc"].ToString()) > 0)
                    {
                        DataTable _dtChild = _untilDAL.GetStoreDataSet("sp_BindGridCategoryByGroup", new string[] { "@Ma_AnPham", "@Category_ParrentID", "@Group_ID" }, new object[] { MaAnPham, _dt.Rows[i]["Ma_chuyenmuc"].ToString(), Group_ID }).Tables[0];
                        if (_dtChild.Rows.Count > 0)
                        {
                            for (int j = 0; j < _dtChild.Rows.Count; j++)
                            {
                                dr = dt.NewRow();
                                dr[0] = _dtChild.Rows[j]["Ma_chuyenmuc"].ToString();
                                dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild.Rows[j]["Ten_chuyenmuc"].ToString();

                                if (Convert.ToBoolean(_dtChild.Rows[j]["Role"]))
                                    dr[2] = "checked";
                                else
                                    dr[2] = "";
                                dr[3] = "Parrent" + _dtChild.Rows[j]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild.Rows[j]["Ma_chuyenmuc"].ToString();
                                if (UltilFunc.GetLatestID("T_ChuyenMuc", "Ma_chuyenmuc", " WHERE Ma_Chuyenmuc_Cha=" + _dtChild.Rows[j]["Ma_chuyenmuc"].ToString()) > 0)
                                    dr[4] = " ParrentClick(" + _dtChild.Rows[j]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild.Rows[j]["Ma_chuyenmuc"].ToString() + ")";
                                else
                                    dr[4] = " ChildeClick(" + _dtChild.Rows[j]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild.Rows[j]["Ma_chuyenmuc"].ToString() + ")";

                                dt.Rows.Add(dr);
                                //// Kiem tra xem chuc nang hien tai co chuyen muc cap 3 hay khong
                                if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chuyenmuc", "Ma_chuyenmuc", "WHERE Ma_Chuyenmuc_Cha=" + _dtChild.Rows[j]["Ma_chuyenmuc"].ToString()) > 0)
                                {
                                    DataTable _dtChild1 = _untilDAL.GetStoreDataSet("sp_BindGridCategoryByGroup", new string[] { "@Ma_AnPham", "@Category_ParrentID", "@Group_ID" }, new object[] { MaAnPham, _dtChild.Rows[j]["Ma_chuyenmuc"].ToString(), Group_ID }).Tables[0];

                                    for (int j1 = 0; j1 < _dtChild1.Rows.Count; j1++)
                                    {
                                        dr = dt.NewRow();
                                        dr[0] = _dtChild1.Rows[j1]["Ma_chuyenmuc"].ToString();
                                        dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild1.Rows[j1]["Ten_chuyenmuc"].ToString();
                                        if (Convert.ToBoolean(_dtChild1.Rows[j1]["Role"]))
                                            dr[2] = "checked";
                                        else
                                            dr[2] = "";
                                        dr[3] = "Parrent" + _dtChild.Rows[j]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild1.Rows[j1]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild1.Rows[j1]["Ma_chuyenmuc"].ToString();
                                        dr[4] = " ChildeClick(" + _dtChild.Rows[j]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild1.Rows[j1]["Ma_Chuyenmuc_Cha"].ToString() + _dtChild1.Rows[j1]["Ma_chuyenmuc"].ToString() + ")";
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dt;
        }
    }
}
