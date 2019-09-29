using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HPCInfo;
using HPCShareDLL;
using System.Collections;
using HPCBusinessLogic.DAL;
using HPCServerDataAccess;

namespace HPCBusinessLogic.DAL
{
    public class T_NewsDAL
    {

        public DataSet Bin_T_NewsVersionDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_NewsVersionDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFromT_NewsDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_UpdateT_NewsDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_news(T_News objNews)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objNews, "[CMS_InsertT_News]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public int InsertT_newsXb(T_News objNews)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objNews, "[CMS_InsertT_News_AfterPub]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public int InsertT_news_ByRollXb(T_News objNews)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(objNews, "[CMS_InsertT_News_RollXb]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        public T_News GetOneFromT_NewsByID(Double _ID)
        {
            try
            {
                return (T_News)HPCDataProvider.Instance().GetObjectByID(@"CMS_SelectOneFromT_News", _ID.ToString(), "T_News", "News_ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_News load_T_news(Int32 id)
        {

            return (T_News)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_News]", id.ToString(), "T_News", "News_ID");

        }
        public T_NewsVersion load_T_news_Version(Int32 id)
        {
            return (T_NewsVersion)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_NewsVersion]", id.ToString(), "T_NewsVersion", "ID");
        }    
        public void Update_Status_tintuc(double ID, int Status,int nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Update_Status_T_news]", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua" }, new object[] { ID, Status, nguoisua, ngaysua });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Status_tintuc(double _ID, int _UserEdit)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Update_LockT_News]", new string[] { "@ID", "@UserEdit" }, new object[] { _ID, _UserEdit});

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Status_T_NewsVersion(double ID, int Status, int nguoisua, DateTime ngaysua)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Update_Status_T_NewsVersion]", new string[] { "@ID", "@trangthai", "@nguoisua", "@ngaysua" }, new object[] { ID, Status, nguoisua, ngaysua });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Version_From_T_News_WithUserModify(Double news_id, int _status, int _Action, int intUserModify)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_Insert_Version_From_T_News_WithUserModify", new string[] { "@ID", "@Status", "@Action", "@UserModify" }, new object[] { news_id, _status, _Action, intUserModify });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsEditor(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_NewsDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsSearchEditor(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_NewsSearchDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsDynamic(int PageIndex, int PageSize, string WhereCondition, string Search)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("CMS_ListT_News_FullTextSearch", new string[] { "@PageIndex", "@PageSize", "@where", "@search" }, new object[] { PageIndex, PageSize, WhereCondition, Search });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsLienQuan(int PageIndex, int PageSize, string WhereCondition, string Search)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("CMS_ListT_News_FullTextSearch", new string[] { "@PageIndex", "@PageSize", "@where", "@search" }, new object[] { PageIndex, PageSize, WhereCondition, Search });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Version_From_T_News_WithLanquage(Double news_id, int _status, int _Action, int intUserModify, int langID, DateTime DateEdit)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Insert_Version_From_T_News_WithLanquage]", new string[] { "@ID", "@Status", "@Action", "@UserModify", "@lang_id", "@News_DateEdit" }, new object[] { news_id, _status, _Action, intUserModify, langID, DateEdit });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckLangquageStartnotVietNam(int News_id, int Lang_ID)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("CheckLangquageStartnotVietNam", new string[] { "@New_ID", "@Languages_ID" }, new object[] { News_id, Lang_ID });
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        /*add by haolm 
         *desc: Lock action
         *prmIsLock: 
         *  1 - Locked
         *  0 - Unlock
         */
        public void IsLock(double prmNewsID, int prmIsLock)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_LockNews]", new string[] { "@newsID", "@isLock" }, new object[] { prmNewsID, prmIsLock });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IsLock(double prmNewsID, int prmIsLock,int _userLock)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_LockNewsByUser]", new string[] { "@newsID", "@isLock", "@editorID" }, new object[] { prmNewsID, prmIsLock, _userLock });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IsLock(double p)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        //Dung them vao
        public bool Get_NewsVersion(int prmNes_ID, int pmr_News_Statu, int pmr_Action)//
        {
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetNewsVersion]", new string[] { "@News_ID", "@News_Status", "@Action" }, new object[] { prmNes_ID, pmr_News_Statu, pmr_Action });//, pmr_News_Statu
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            { throw ex; }
            return false;
        }
        public string getSenderIDFrom_NewsID_Version(int News_ID, int intFieldIndex, int _Status)
        {
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("CMS_getSenderIDFrom_NewsID_Version", new string[] { "@News_ID", "@Status", }, new object[] { News_ID, _Status });
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (intFieldIndex == 0)
                        return dt.Rows[0]["News_EditorID"].ToString();
                    else
                        return dt.Rows[0]["News_DateEdit"].ToString();
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetNews_IDFrom_NewsID_Version(int News_ID, int intFieldIndex)
        {
            string str = "";
            T_NewsDAL dalnews = new T_NewsDAL();
            try
            {
                DataSet ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_getSenderIDFrom_NewsID]", new string[] { "@News_ID" }, new object[] { News_ID });
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (intFieldIndex == 0)
                        str = UltilFunc.GetUserName(int.Parse(dt.Rows[0]["News_EditorID"].ToString()));
                    else
                        str = Convert.ToDateTime(dt.Rows[0]["News_DateEdit"]).ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    if (intFieldIndex == 0)
                    {
                        int EditorID = dalnews.load_T_news(News_ID).News_EditorID;
                        str = UltilFunc.GetUserName(EditorID);
                    }
                    else
                    {
                        str = Convert.ToDateTime(dalnews.load_T_news(News_ID).News_DateEdit).ToString("dd/MM/yyyy HH:mm");
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStatus_T_News_ex_New_HV(double _news_id, int _status, int User_Modify, DateTime Date_Modify)
        {

            string strUserID = "";
            if (_status == 23)
                strUserID = getSenderIDFrom_NewsID_Version(Convert.ToInt32(_news_id), 0, 2);
            else if (_status == 73)
                strUserID = getSenderIDFrom_NewsID_Version(Convert.ToInt32(_news_id), 0, 7);

            if (strUserID == "")
                strUserID = User_Modify.ToString();

            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_UpdateStatus_T_News]", new string[] { "@News_ID", "@Status", "@User_Modify", "@Date_Modify" }, new object[] { _news_id, _status, Int32.Parse(strUserID), Date_Modify });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //Add by linhln
        public bool Update_Thutu_Noibat(double ID, int Thutu)
        {
            bool _Success = true;
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_ListT_News_Update_Position", new string[] { "@ID", "@Thutu" }, new object[] { ID, Thutu });
            }
            catch
            {
                _Success = false;
            }

            return _Success;
        }
        public bool Update_Tiennhanbut(int Tin, int NewsID, int ImageID, int Tien, int UserID )
        {
            bool _Success = true;
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_Thantoan_Tin_Anh",
                    new string[] { "@Tin", "@NewsID", "@ImageID", "@Tien", "@UserID" },
                    new object[] { Tin, NewsID, ImageID, Tien, UserID });
            }
            catch
            {
                _Success = false;
            }

            return _Success;
        }
        

        public DataSet GetCatalog(int News_ID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_Get_Catalog]", new string[] { "@NewsID" }, new object[] { News_ID });
            }
            catch { ;}
            return ds;
        }

        public DataSet Search_All_News(int pageindex, int pagesize, string where)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("CMS_Search_All_News", new string[] { "@PageIndex", "@Pagesize", "@Where" }
                    , new object[] { pageindex, pagesize, where });
            }
            catch { ;}
            return ds;
        }
        public DataSet Search_All_News_Nhanbut(int pageindex, int pagesize, string where)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("CMS_Search_All_News_Nhanbut", new string[] { "@PageIndex", "@Pagesize", "@Where" }
                    , new object[] { pageindex, pagesize, where });
            }
            catch { ;}
            return ds;
        }
        public DataSet GetHistory(int NewsID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetHistory]", new string[] { "@News_ID" }
                    , new object[] { NewsID });
            }
            catch { ;}
            return ds;
        }
        public DataSet Search_All_News_Nhanbut(int pageindex, int pagesize, int searchNews, int searchPic, int searchVideo, int searchAnhTS, string WhereNews, string WherePic, string WhereVideo, string WhereAnhTS)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("CMS_Search_All_News_Nhanbut",
                    new string[] { "@PageIndex", "@Pagesize", "@SearchNews", "@SearchPic", "@SearchVideo", "@SearchAnhTS", "@WhereNews", "@WherePic", "@WhereVideo", "@WhereAnhTS" }
                  , new object[] { pageindex, pagesize, searchNews, searchPic, searchVideo, searchAnhTS, WhereNews, WherePic, WhereVideo, WhereAnhTS });
            }
            catch { ;}
            return ds;
        }

        public DataSet Search_All_NhuanBut_News(int pageindex, int pagesize,string WhereNews)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_Search_All_NewsNhuanBut]",
                    new string[] { "@PageIndex", "@Pagesize","@WhereNews"}
                  , new object[] { pageindex, pagesize, WhereNews });
            }
            catch { ;}
            return ds;
        }

        public bool Update_TiennhanbutTin(int Tin, int NewsID, int ImageID, int Tien, int UserID)
        {
            bool _Success = true;
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_Thantoan_Tin",
                    new string[] { "@Tin", "@NewsID", "@ImageID","@Tien", "@UserID" },
                    new object[] { Tin, NewsID, ImageID,  Tien, UserID });
            }
            catch
            {
                _Success = false;
            }

            return _Success;
        }

        public bool Update_TiennhanbutAnh(int Tin, int NewsID, int ImageID,  int Tien, int UserID)
        {
            bool _Success = true;
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_Thantoan_Anh",
                    new string[] { "@Tin", "@NewsID", "@ImageID", "@Tien", "@UserID" },
                    new object[] { Tin, NewsID, ImageID, Tien, UserID });
            }
            catch
            {
                _Success = false;
            }

            return _Success;
        }

        public DataSet GetDetailHistory(int LogID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetDetailHistory]", new string[] { "@LogID" }
                    , new object[] { LogID });
            }
            catch { ;}
            return ds;
        }

        //---end add----

        //Get bai viet lien quan
        public DataSet BindGridT_NewsRealates(string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetTop50NewsRealates]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_NewsRealates(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetNewsRealates]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //End
        //keyword
        public void InsertT_Keywords(string Keyword, double NewsID, int UserID)
        {

            string _sql = "CMS_InsertT_Keywords_Dynamic";
            try
            {
                string sql = "Delete T_KeyNews where IDNews = " + NewsID;
                HPCDataProvider.Instance().ExecSql(sql);
                string[] _Keyword = Keyword.ToString().Trim().Split(',');
                for (int i = 0; i < _Keyword.Length; i++)
                {
                    string _key = _Keyword[i].Trim();
                    if (_key.Length > 1)
                    {
                        SqlService _sqlservice = new SqlService();
                        _sqlservice.AddParameter("@Keyword", SqlDbType.NVarChar, _key, true);
                        _sqlservice.AddParameter("@NewsID", SqlDbType.Float, NewsID, true);
                        _sqlservice.AddParameter("@UserID", SqlDbType.Float, UserID, true);
                        _sqlservice.ExecuteSP(_sql);
                        _sqlservice.CloseConnect(); _sqlservice.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetKeywordsByNewsID(double News_ID)
        {
            string _keyword = "";
            try
            {
                string sql = "select T_KeyWords.KeyWord from T_KeyWords INNER JOIN T_KeyNews on T_KeyWords.ID = T_KeyNews.IDKeyword where T_KeyNews.IDNews = " + News_ID;
                DataSet ds = HPCDataProvider.Instance().ExecSqlDataSet(sql);
                DataTable dt = ds.Tables[0];
                //int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _keyword += dr["Keyword"].ToString() + ", ";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _keyword;
        }
        //end
        public double Copy_To_Categorys(Double ID, int CAT_ID, int Status, DateTime News_DateEdit, int News_EditorID, int News_AuthorID, string News_AuthorName)
        {
            double _ID = 0.0;
            try
            {
                _ID = HPCDataProvider.Instance().ExecStoreReturn("[CMS_Copy_T_News]", new string[] { "@NewsID", "@Cat_ID", "@Status", "@News_DateEdit", "@News_EditorID", "@News_AuthorID", "@News_AuthorName" }, new object[] { ID, CAT_ID, Status, News_DateEdit, News_EditorID, News_AuthorID, News_AuthorName });
            }
            catch
            {
                _ID = 0.0;
            }
            return _ID;
        }
        //Add by Nvthai
        public DataSet GetDataTinMoiDang(string WhereCondition)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListNewsTopXuatBan]", new string[] { "@WhereCondition" }
                    , new object[] { WhereCondition });
            }
            catch { ;}
            return ds;
            
        }

        //End
    }
}
