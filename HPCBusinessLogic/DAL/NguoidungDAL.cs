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

namespace HPCBusinessLogic
{

    public class NguoidungDAL
    {
        #region SSO Authenticaton
        SSOLibDAL Dal = new SSOLibDAL();
        public int InsertT_Users(T_Users obj)
        {
            SSOLib.SSOLibDAL _ssolibdal = new SSOLib.SSOLibDAL();
            return _ssolibdal.InsertUpdateT_Users(obj);
        }
        public DataSet BindGridT_Users(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {

                return Dal.GetListObject("Sp_ListT_UsersDynamic", PageIndex, PageSize, WhereCondition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Login(string userName, string passWord)
        {
            try
            {
                T_Users _user = new T_Users();
                _user = Dal.LogInAuthen(userName, passWord);
                if (_user != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public T_Users GetUserByUserPass(string userName, string passWord)
        {
            try
            {
                return HPCDataProvider.Instance().GetUserByUserPass(userName, passWord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_Users GetUserByUserName(string userName)
        {
            try
            {
                return HPCDataProvider.Instance().GetUserByUserName(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetT_User_Dynamic(string WhereCondition)
        {
            SSOLib.SSOLibDAL _objssolibdal = new SSOLib.SSOLibDAL();
            try
            {
                return _objssolibdal.GetListUserByWhere(WhereCondition);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_UserByID(int ID)
        {
            try
            {
                SSOLib.SSOLibDAL _objssolibdal = new SSOLib.SSOLibDAL();
                _objssolibdal.DeletByID("Sp_DeleteOneFromT_Users", "@UserID", ID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckDelete_users(int userid)
        {
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("CheckDelete_Users", new string[] { "@UserID" }, new object[] { userid });
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_UserMenu(int UserID, int MenuID, bool Doc, bool Ghi, bool Xoa, int GroupID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_InsertT_UserMenuDynamic", new string[] { "@Ma_Nguoidung", "@Ma_ChucNang", "@Doc", "@Ghi", "@Xoa", "@Ma_Nhom" }, new object[] { UserID, MenuID, Doc, Ghi, Xoa, GroupID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_UserCategory(int UserID, int Categorys_ID, int GroupID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_InsertT_UserCategoryDynamic", new string[] { "@User_ID", "@Categorys_ID", "@group_ID" }, new object[] { UserID, Categorys_ID, GroupID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertT_UserLanguages(int UserID, int LangID, int GroupID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_InsertT_Nguoidung_Ngonngu", new string[] { "@Ma_Nguoidung", "@Languages_ID", "@Group_ID" }, new object[] { UserID, LangID, GroupID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetT_GroupMenuDynamic(int ID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            return _untilDAL.GetStoreDataSet("Sp_SelectT_GroupMenuDynamic", new string[] { "@WhereCondition" }, new object[] { " Ma_Nhom = " + ID }).Tables[0];
        }
        public DataTable GetT_GroupCategoryDynamic(int ID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            return _untilDAL.GetStoreDataSet("Sp_SelectT_GroupCategoryDynamic", new string[] { "@WhereCondition" }, new object[] { " Ma_Nhom = " + ID }).Tables[0];
        }
        public DataTable GetT_GroupLanguagesDynamic(int ID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            return _untilDAL.GetStoreDataSet("Sp_SelectT_Nhom_NgonNguDynamic", new string[] { "@WhereCondition" }, new object[] { " Ma_Nhom = " + ID }).Tables[0];
        }
        public DataTable GetT_Nhom_QTBTDynamic(int ID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            return _untilDAL.GetStoreDataSet("Sp_SelectT_Nhom_QTBTDynamic", new string[] { "@WhereCondition" }, new object[] { " Ma_Nhom = " + ID }).Tables[0];
        }
        public void DeleteFromT_UserMenuDynamic(string strSql)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_DeleteT_UserMenuDynamic", new string[] { "@WhereCondition" }, new object[] { strSql });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_UserCategoryDynamic(string strSql)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_DeleteT_UserCategoryDynamic", new string[] { "@WhereCondition" }, new object[] { strSql });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_UserLanguageDynamic(string strSql)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Nguoidung_NgonNguDynamic", new string[] { "@WhereCondition" }, new object[] { strSql });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertT_UserGroups(int UserID, int GroupID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_UserGroups", new string[] { "@User_ID", "@group_ID" }, new object[] { UserID, GroupID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteT_UserGroups(int UserID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_UserGroups", new string[] { "@User_ID" }, new object[] { UserID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ArrayList GetAllFrom_T_GroupNotInUser(string pane, int id)
        {
            UltilFunc _untilDAL = new UltilFunc();
            ArrayList _arr = new ArrayList();
            DataTable _dt;
            try
            {
                _dt = _untilDAL.GetStoreDataSet("GetAllFrom_T_GroupNotInUser", new string[] { "@User_ID" }, new object[] { id }).Tables[0];

                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        T_Nhom g = new T_Nhom();
                        if (_dt.Rows[i]["Ma_Nhom"] != DBNull.Value)
                            g.Ma_Nhom = Convert.ToInt32(_dt.Rows[i]["Ma_Nhom"]);
                        if (_dt.Rows[i]["Ten_Nhom"] != DBNull.Value)
                            g.Ten_Nhom = Convert.ToString(_dt.Rows[i]["Ten_Nhom"]);
                        _arr.Add(g);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _arr;
        }
        public ArrayList GetAllFrom_T_GroupInUser(string pane, int id)
        {
            UltilFunc _untilDAL = new UltilFunc();
            ArrayList _arr = new ArrayList();
            DataTable _dt;
            try
            {
                _dt = _untilDAL.GetStoreDataSet("GetAllFrom_T_GroupInUser", new string[] { "@User_ID" }, new object[] { id }).Tables[0];

                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        T_Nhom g = new T_Nhom();
                        if (_dt.Rows[i]["Ma_Nhom"] != DBNull.Value) g.Ma_Nhom = Convert.ToInt32(_dt.Rows[i]["Ma_Nhom"]);
                        if (_dt.Rows[i]["Ten_Nhom"] != DBNull.Value) g.Ten_Nhom = Convert.ToString(_dt.Rows[i]["Ten_Nhom"]);
                        _arr.Add(g);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _arr;
        }
        public DataTable BindGridMenuByUser(int UserID, string LangID)
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
            dt.Columns.Add(new DataColumn("Role_Group", typeof(bool)));
            dt.Columns.Add(new DataColumn("Doc", typeof(bool)));
            dt.Columns.Add(new DataColumn("Xoa", typeof(bool)));
            dt.Columns.Add(new DataColumn("Ghi", typeof(bool)));

            DataTable _dt = _untilDAL.GetStoreDataSet("sp_BindGridMenuByUser", new string[] { "@Parrent_ID", "@User_ID", "@Lang_ID" }, new object[] { 0, UserID, LangID }).Tables[0];

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
                    dr[6] = !Convert.ToBoolean(_dt.Rows[i]["ingroup"]);
                    if (_dt.Rows[i]["Doc"] != DBNull.Value)
                        dr[7] = Convert.ToBoolean(_dt.Rows[i]["Doc"]);
                    else dr[7] = false;
                    if (_dt.Rows[i]["Xoa"] != DBNull.Value)
                        dr[8] = Convert.ToBoolean(_dt.Rows[i]["Xoa"]);
                    else dr[8] = false;
                    if (_dt.Rows[i]["Ghi"] != DBNull.Value)
                        dr[9] = Convert.ToBoolean(_dt.Rows[i]["Ghi"]);
                    else dr[9] = false;

                    dt.Rows.Add(dr);
                    //}
                    //Kiem tra xem chuc nang co chuyen muc con hay khong

                    if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chucnang", "Ma_Chucnang", "WHERE Ma_Chucnang_Cha=" + _dt.Rows[i].ItemArray[0].ToString()) > 0)
                    {
                        DataTable _dtChild = _untilDAL.GetStoreDataSet("sp_BindGridMenuByUser", new string[] { "@Parrent_ID", "@User_ID", "@Lang_ID" }, new object[] { _dt.Rows[i].ItemArray[0], UserID, LangID }).Tables[0];
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
                                dr[6] = !Convert.ToBoolean(_dtChild.Rows[j]["ingroup"]);

                                if (_dtChild.Rows[j]["Doc"] != DBNull.Value)
                                    dr[7] = Convert.ToBoolean(_dtChild.Rows[j]["Doc"]);
                                else dr[7] = false;
                                if (_dtChild.Rows[j]["Xoa"] != DBNull.Value)
                                    dr[8] = Convert.ToBoolean(_dtChild.Rows[j]["Xoa"]);
                                else dr[8] = false;
                                if (_dtChild.Rows[j]["Ghi"] != DBNull.Value)
                                    dr[9] = Convert.ToBoolean(_dtChild.Rows[j]["Ghi"]);
                                else dr[9] = false;

                                dt.Rows.Add(dr);

                                // Kiem tra xem chuc nang hien tai co chuyen muc cap 3 hay khong
                                if (HPCBusinessLogic.UltilFunc.GetLatestID("T_Chucnang", "Ma_Chucnang", "WHERE Ma_Chucnang_Cha=" + _dtChild.Rows[j].ItemArray[0].ToString()) > 0)
                                {
                                    DataTable _dtChild1 = _untilDAL.GetStoreDataSet("sp_BindGridMenuByUser", new string[] { "@Parrent_ID", "@User_ID", "@Lang_ID" }, new object[] { _dtChild.Rows[j].ItemArray[0], UserID, LangID }).Tables[0];

                                    for (int j1 = 0; j1 < _dtChild1.Rows.Count; j1++)
                                    {
                                        dr = dt.NewRow();
                                        dr[0] = _dtChild1.Rows[j1]["Ma_Chucnang"].ToString();
                                        dr[1] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild1.Rows[j1]["Ten_chucnang"].ToString();
                                        dr[2] = _dtChild1.Rows[j1]["STT"].ToString();
                                        dr[3] = _dtChild1.Rows[j1]["URL_Chucnang"].ToString();
                                        dr[4] = _dtChild1.Rows[j1]["Mota"].ToString();
                                        dr[5] = Convert.ToBoolean(_dtChild1.Rows[j1]["role"]);
                                        dr[6] = !Convert.ToBoolean(_dtChild1.Rows[j1]["ingroup"]);

                                        if (_dtChild1.Rows[j1]["Doc"] != DBNull.Value)
                                            dr[7] = Convert.ToBoolean(_dtChild1.Rows[j1]["Doc"]);
                                        else dr[7] = false;
                                        if (_dtChild1.Rows[j1]["Xoa"] != DBNull.Value)
                                            dr[8] = Convert.ToBoolean(_dtChild1.Rows[j1]["Xoa"]);
                                        else dr[8] = false;
                                        if (_dtChild1.Rows[j1]["Ghi"] != DBNull.Value)
                                            dr[9] = Convert.ToBoolean(_dtChild1.Rows[j1]["Ghi"]);
                                        else dr[9] = false;

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
        #endregion

        #region Thong tin nguoi dung
        public DataSet BindGridT_Nguoidung(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_NguoidungDynamic", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Nguoidung(T_Nguoidung obj)
        {
            return HPCDataProvider.Instance().InsertObjectReturn(obj, "SP_InsertT_Nguoidung");
        }
        public T_Nguoidung GetOneFromT_NguoidungByID(int ID)
        {
            try
            {
                return (T_Nguoidung)HPCDataProvider.Instance().GetObjectByID(ID.ToString(), "T_Nguoidung", "Ma_Nguoidung");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRow_T_Nguoidung(T_Nguoidung _T_Nguoidung)
        {
            try
            {
                HPCDataProvider.Instance().UpdateObject(_T_Nguoidung);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_Nguoidung(int Manguoidung)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteOneFromT_Nguoidung", new string[] { "@Ma_Nguoidung" }, new object[] { Manguoidung });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFromT_NguoidungDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_UpdateT_NguoidungDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTenNguoiDungDynamic(string WhereCondition, string OrderByExpression)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_SelectT_NguoidungDynamic", new string[] { "@WhereCondition", "@OrderByExpression" }, new object[] { WhereCondition, OrderByExpression });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Roll quytrinh
        public void DeleteFromT_Nguoidung_QTBTDynamic(string strSql)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_DeleteT_Nguoidung_QTBTDynamic", new string[] { "@WhereCondition" }, new object[] { strSql });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SP_InsertT_Nguoidung_QTBT(int UserID, int ma_qtbt, int GroupID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("SP_InsertT_Nguoidung_QTBT", new string[] { "@Ma_Nguoidung", "@QTBT_ID", "@Group_ID" }, new object[] { UserID, ma_qtbt, GroupID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGridT_NguoiDungQTBT(int Ma_Nguoidung)
        {
            DataTable _dt = new DataTable();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_Quytrinh", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));
            dt.Columns.Add(new DataColumn("RoleGroup", typeof(bool)));
            dt.Columns.Add(new DataColumn("Mota", typeof(string)));
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridT_NguoidungQTBT_ByUser", new string[] { "@User_ID" }, new object[] { Ma_Nguoidung });
                if (_ds != null)
                    _dt = _ds.Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _dt.Rows[i]["ID"].ToString();
                        dr[1] = _dt.Rows[i]["Ten_Quytrinh"].ToString();
                        dr[2] = Convert.ToBoolean(_dt.Rows[i]["Role"]);
                        dr[3] = !Convert.ToBoolean(_dt.Rows[i]["ingroup"]);
                        dr[4] = _dt.Rows[i]["Mota"].ToString();
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
        #endregion

        #region Roll Ngôn Ngữ
        public DataTable BindGridT_NguoiDungNgonNgu(int Ma_Nguoidung)
        {
            DataTable _dt = new DataTable();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("TenNgonNgu", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(bool)));
            dt.Columns.Add(new DataColumn("RoleGroup", typeof(bool)));
            dt.Columns.Add(new DataColumn("Mota", typeof(string)));
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("sp_BindGridT_NguoidungLanguagesByUser", new string[] { "@User_ID" }, new object[] { Ma_Nguoidung });
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
                        if (Convert.ToBoolean(_dt.Rows[i]["ingroup"]))
                            dr[3] = false;
                        else
                            dr[3] = true;
                        dr[4] = _dt.Rows[i]["Mota"].ToString();
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
        #endregion

        #region Roll Chuyên Mục
        string _machuyenmuc = string.Empty;
        public DataTable BindGridT_UserCategoryByUser(int UserID, int Ma_AnPham)
        {
            DataTable _dt = new DataTable();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Ma_Chuyenmuc", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_Chuyenmuc", typeof(string)));
            dt.Columns.Add(new DataColumn("Role_Cate", typeof(string)));
            dt.Columns.Add(new DataColumn("Role_Group", typeof(string)));
            dt.Columns.Add(new DataColumn("RoleCheck", typeof(bool)));
            dt.Columns.Add(new DataColumn("Chuyenmuccha", typeof(int)));

            DataSet _ds = _untilDAL.GetStoreDataSet("sp_BindGridT_Nguoidung_ChuyenmucByUserDeQuy", new string[] { "@Ma_AnPham", "@User_ID", "@Category_ParrentID" }, new object[] { Ma_AnPham, UserID, 0 });
            if (_ds != null)
                _dt = _ds.Tables[0];
            int dem = 0;
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {

                    dr = dt.NewRow();
                    dr[0] = _dt.Rows[i]["Ma_ChuyenMuc"].ToString();
                    if (int.Parse(_dt.Rows[i]["Ma_Chuyenmuc_Cha"].ToString()) == 0)
                        dr[1] = "<b>" + _dt.Rows[i]["Ten_ChuyenMuc"].ToString() + "</b>";
                    else
                        dr[1] = _dt.Rows[i]["Ten_ChuyenMuc"].ToString();
                    if (Convert.ToBoolean(_dt.Rows[i]["ROLE"]))
                        dr[2] = "checked";
                    else
                        dr[2] = "";
                    if (Convert.ToBoolean(_dt.Rows[i]["ingroup"]))
                        dr[3] = "disabled";
                    else
                        dr[3] = "";

                    if (Convert.ToBoolean(_dt.Rows[i]["ROLE"]))
                        dr[4] = true;
                    else
                        dr[4] = false;
                    dr[5] = int.Parse(_dt.Rows[i]["Ma_ChuyenMuc"].ToString());

                    dt.Rows.Add(dr);
                    _machuyenmuc = _dt.Rows[i]["Ma_ChuyenMuc"].ToString();
                    dt = GetChild(dt, dr, _dt.Rows[i]["Ma_ChuyenMuc"].ToString(), dem, UserID, Ma_AnPham);
                }
            }
            return dt;
        }
        public DataTable GetChild(DataTable dt, DataRow dr, string ParentID, int Rank, int userid, int ma_anpham)
        {
            UltilFunc ulti = new UltilFunc();

            try
            {
                DataTable _dt = ulti.GetStoreDataSet("sp_BindGridT_Nguoidung_ChuyenmucByUserDeQuy", new string[] { "@Ma_AnPham", "@User_ID", "@Category_ParrentID" }, new object[] { ma_anpham, userid, ParentID }).Tables[0];

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
                            blank += blank;
                        }

                        dr[1] = System.Web.HttpUtility.HtmlDecode(blank) + _dt.Rows[i]["Ten_ChuyenMuc"].ToString();

                        if (Convert.ToBoolean(_dt.Rows[i]["ROLE"]))
                            dr[2] = "checked";
                        else
                            dr[2] = "";
                        if (Convert.ToBoolean(_dt.Rows[i]["ingroup"]))
                            dr[3] = "disabled";
                        else
                            dr[3] = "";

                        if (Convert.ToBoolean(_dt.Rows[i]["ROLE"]))
                            dr[4] = true;
                        else
                            dr[4] = false;
                        _machuyenmuc += ParentID;
                        dr[5] = int.Parse(_dt.Rows[i]["Ma_Chuyenmuc_Cha"].ToString());

                        dt.Rows.Add(dr);
                        dt = GetChild(dt, dr, _dt.Rows[i]["Ma_ChuyenMuc"].ToString(), Rank, userid, ma_anpham);

                    }
                    Rank--;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        #endregion

        #region ext - user-role

        public T_RolePermission GetRole4UserMenu(int User_ID, int Menu_ID)
        {
            return HPCDataProvider.Instance().GetRole4UserMenu(User_ID, Menu_ID);
        }

        public T_Users GetUSERNAME(string UserName, int UserID)
        {
            return (T_Users)HPCDataProvider.Instance().GetObjectByCondition("T_Nguoidung", " UserName = N'" + UserName + "'  AND UserID <> " + UserID + "");
        }
        public T_Users GetUserByUserName_ID(int userID)
        {
            try
            {
                return HPCDataProvider.Instance().GetUserByUserName_ID(userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ResetPass(string pass, int userID)
        {
            try
            {
                SSOLib.SSOLibDAL _objssolibdal = new SSOLib.SSOLibDAL();
                _objssolibdal.ResetPassByID("Sp_UpdatePassReset", "@UserPass", "@ID", pass, userID); ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActiveUser(bool UserActive, int userID)
        {
            try
            {
                SSOLib.SSOLibDAL _objssolibdal = new SSOLib.SSOLibDAL();
                _objssolibdal.ActiveOrNotActiveByID("Sp_UpdateUserActive", "@UserActive", "@ID", UserActive, userID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public DataTable GetAllUser_By_CatID(int CatID)
        {
            UltilFunc _untilDAL = new UltilFunc();
            ArrayList _arr = new ArrayList();
            DataTable _dt = new DataTable(); ;
            try
            {
                _dt = HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetAll_User]", new string[] { "@CATID" }, new object[] { CatID }).Tables[0];
            }
            catch { ;}
            return _dt;
        }
    }
}
