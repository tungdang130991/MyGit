using System;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using HPCServerDataAccess;
using System.Web.UI.WebControls;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Resources;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using SSOLib.ServiceAgent;


namespace HPCBusinessLogic
{
    public class UltilFunc
    {
        public static string TinPath = ConfigurationManager.AppSettings["tinpath"];
        public static string _PathServerDisc = ConfigurationManager.AppSettings["ServerPathDis"];
        public static int PhotosPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PhotosPerPage"]);

        public static string WidthThumbnail = ConfigurationManager.AppSettings["WidthThumbnail"];
        public static string HeightThumbnail = ConfigurationManager.AppSettings["HeightThumbnail"];

        public DataSet GetDataSet(string TableName, string ColumnList, string Where)
        {
            try
            {
                return HPCDataProvider.Instance().GetDataSet(TableName, ColumnList, Where);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ReturnPath_Images(Object _path)
        {
            string strReturn = "";
            if (_path.ToString() != "")
            {
                string _linkImage = ConfigurationManager.AppSettings["ServerPathDis"] + UrlPathImage_RemoveUpload(_path.ToString());
                strReturn = "<img style=\"cursor:pointer;border:0px;\" alt=\"Xem ảnh\" title=\"Xem ảnh\" src=\"" + ConfigurationManager.AppSettings["tinpathbdt"] + _path.ToString() + "\" onclick=\"openNewImage(this,'Đóng');\" />";
            }
            else
                strReturn = "<img style=\"cursor:pointer;border:0px;\" alt=\"Xem ảnh\" title=\"Xem ảnh\" src=\"" + ConfigurationManager.AppSettings["ApplicationPath"] + "/Dungchung/Images/no_images.jpg" + "\" />";

            return strReturn;
        }
        public static string UrlPathImage_RemoveUpload(object PhysPathFull)
        {
            return PhysPathFull.ToString().Replace(System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString(), "");
        }
        public static string SplitString(string _root)
        {
            string _key = "";
            int _len = _root.Length;
            int checkand = 0, _continue = 0;
            for (int i = 0; i < _len; i++)
            {
                if (_root[i] == ' ' && _continue == 0)
                {
                    checkand = 0;
                    for (int j = i + 1; j < _len; j++)
                    {
                        if (_root[j] == ' ')
                        {
                        }
                        else if (_root[j] == '+')
                        {
                            checkand = 1;
                        }
                        else if (_root[j] != ' ' && _root[j] != '+')
                        {
                            if (checkand == 0)
                                _key = _key + "\" OR \"";
                            else
                                _key = _key + "\" and \"";
                            i = j - 1;
                            checkand = 0;
                            break;
                        }
                    }
                }
                else if (_root[i] == '+')
                {
                    for (int j = i + 1; j < _len; j++)
                    {
                        if (_root[j] == ' ' || _root[j] == '+')
                        {
                        }
                        else
                        {
                            _key = _key + "\" and \"";
                            i = j - 1;
                            break;
                        }
                    }
                }
                else if (_root[i] == '"' && _continue == 0)
                {
                    _continue = 1;
                }

                else if (_root[i] == '"' && _continue == 1)
                {
                    _continue = 0;
                    if (i < _len - 1 && _root[i + 1] != ' ' && _root[i + 1] != '+')
                    {
                        _key = _key + "\" OR \"";
                    }
                }
                else
                {
                    checkand = 0;
                    _key = _key + _root[i];
                }
            }
            _key = _key + "";
            return _key;

        }
        public static string ReplaceAll(string source, string stringToFind, string stringToReplace)
        {
            var temp = source;
            var index = temp.IndexOf(stringToFind);
            while (index != -1)
            {
                temp = temp.Replace(stringToFind, stringToReplace);
                index = temp.IndexOf(stringToFind);
            }
            return temp;
        }
        /// <summary>
        /// Disable or Enable T_News
        /// </summary>
        /// <param name="_Role"></param>
        /// <param name="prmNews_Lock"></param>
        /// <param name="prmEditorID"></param>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public static bool IsEnable(bool _Role, string prmNews_Lock, string prmEditorID, int _userID)
        {
            bool _isEnabled = true;
            int _prmEditorID = Convert.ToInt32(prmEditorID);
            if (prmNews_Lock == "True" && _prmEditorID != _userID)
                _isEnabled = false;
            else
            {
                _isEnabled = _Role;
            }
            return _isEnabled;
        }
        public static string IsGetTrangThai(object str)
        {
            string strReturn = "";
            if (str != null)
            {
                if (str.ToString() == "1")
                    strReturn = "Bình thường";
                if (str.ToString() == "2")
                    strReturn = "Nổi bật chuyên mục";
                if (str.ToString() == "3")
                    strReturn = "Nổi bật trang chủ";
                if (str.ToString() == "4")
                    strReturn = "Nổi bật chuyên mục cha";
                if (str.ToString() == "5")
                    strReturn = "Tin vắn";
            }
            return strReturn;
        }
        public static string RemoveHTMLTag(string HTML)
        {
            // Xóa các thẻ html
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            return objRegEx.Replace(HTML, "");

        }
        public static string RemoveHTMLTagNotImg(string HTML, string tagNotRemove)
        {
            // Xóa các thẻ html tru the img
            //string AcceptableTags = "i|b|u|sup|sub|ol|ul|li|br|h2|h3|h4|h5|span|div|p|a|blockquote";           
            string stringPattern = @"</?(?(?=" + tagNotRemove + @")notag|[a-z,A-Z,0-9]+)(?:\s[a-z,A-Z,0-9,\-]+=?(?:(["",']?).*?\1?))*\s*/?>";
            return Regex.Replace(HTML, stringPattern, "");

        }
        public static string ReplaceCharsRewrite(object input)
        {
            string str = "", StrTemp = RemoveHTMLTag(Convert.ToString(input));
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = StrTemp.Normalize(System.Text.NormalizationForm.FormD);
            str = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex objRegEx = new Regex("<[^>]*>");
            str = str.Replace(" ", "-");
            str = str.Replace(",", "");
            str = str.Replace(".", "");
            str = str.Replace(";", "");
            str = str.Replace(":", "");
            str = str.Replace("?", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("!", "");
            str = str.Replace("@", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("+", "");
            str = str.Replace("=", "");
            str = str.Replace("\\", "");
            str = str.Replace("|", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("”", "");
            str = str.Replace("“", "");
            str = str.Replace("-»", "");
            str = str.Replace("«-", "");
            str = str.Replace("»", "");
            str = str.Replace("»", "");
            str = str.Replace("«", "");
            str = str.Replace("’", "");
            str = str.Replace("--", "-");
            str = str.Replace("---", "-");
            str = str.Replace("----", "-");
            str = str.Replace("-----", "-");
            str = str.Replace(" ", "-");
            //Add by nvthai
            str = str.Replace("Ã°", "");
            str = str.Replace("â€", "");
            str = str.Replace("a€", "");
            str = str.Replace("a°", "");
            return str.ToLower();
        }
        public static string LockedUser(string prm_news_Lock, string prmNewsEditorID, int _userID)
        {
            string _userLock = "";
            int _prmEditorID = 0;
            if (prmNewsEditorID != "")
                _prmEditorID = Convert.ToInt32(prmNewsEditorID);
            if (prm_news_Lock == "True" && _prmEditorID != _userID)
                _userLock = "<b> &nbsp;&nbsp;[ <font color='red'> User locked: " + UltilFunc.GetUserName(prmNewsEditorID) + "</font> ]</b>";
            return _userLock;
        }
        public static string IsStatusGet(object _images, object _video)
        {
            string strReturn = "";

            if (_images != null && _video != null)
            {
                if (_images.ToString().ToLower() == "true")
                    strReturn += "&nbsp;&nbsp;<img src=\"" + ConfigurationManager.AppSettings["ApplicationPath"] + "/DungChung/Images/Icons/i-image.png\" alt=\"Tin Ảnh\" title=\"Tin Ảnh\" />";
                if (_video.ToString().ToLower() == "true")
                    strReturn += "&nbsp;<img src=\"" + ConfigurationManager.AppSettings["ApplicationPath"] + "/DungChung/Images/Icons/i-video.png\" alt=\"Tin Video\" title=\"Tin Video\" />";
            }
            return strReturn;
        }
        public static string IsStatusImages(string str)
        {
            string strReturn = "";
            int _count = CountImgTag(str);
            if (_count != 0)
                strReturn = "<b> &nbsp;&nbsp;[ <font color='#980700'> Có " + _count.ToString() + " ảnh trong bài</font> ]</b>";
            return strReturn;
        }

        public static string GetCategoryName(Object ID)
        {
            string str = string.Empty;
            try
            {
                ChuyenmucDAL _dal = new ChuyenmucDAL();

                if (_dal.GetOneFromT_ChuyenmucByID(Convert.ToInt32(ID)) == null)
                    str = "";
                else
                    str = HPCDataProvider.Instance().GetStoreDataSet("[CMS_GetCategoryNameAll]", new string[] { "@CatID" }, new object[] { ID }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string CheckLayoutTinVan(Object ID)
        {
            T_News _obj_T_News = new T_News();
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            string str = "";
            try
            {
                _obj_T_News.News_CopyFrom = tt.load_T_news(Convert.ToInt32(ID)).News_CopyFrom;
                if (tt.Get_NewsVersion(Convert.ToInt32(_obj_T_News.News_CopyFrom.ToString()), 7, 92)
                    || tt.Get_NewsVersion(Convert.ToInt32(_obj_T_News.News_CopyFrom.ToString()), 7, 82))
                {
                    str = "<b style=\"color:#FF3300\">Có Layout</b>";
                }
                else
                {
                    str = "Không có Layout";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetStyleComments(Object _id)
        {
            string _return = null;
            string _comment = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("Select News_Comment FROM T_News Where News_ID =" + _id);

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_comment == null)
                        {
                            _comment += _dt.Rows[i]["News_Comment"].ToString();
                            if (_comment.Length > 0)
                                _return = "linkEditCommend";
                        }
                    }
                }
                else
                    _return = "linkEdit";
            }
            catch (Exception ex)
            {
                _return = "linkEdit";

            }
            return _return;
        }
        public static int CountImgTag(string str)
        {
            int _return = 0;
            try
            {
                Regex regex = new Regex(
                    @"(?<=<img[^<]+?src=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                _return = matchCollect.Count;
            }
            catch { }
            return _return;
        }
        public static int Check_SyncDatabase(int menuid)
        {
            int id = 0;
            try
            {
                string sql = "select ActiveSync from T_Menus where ID  = " + menuid;
                DataSet ds = HPCDataProvider.Instance().ExecSqlDataSet(sql);
                id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                //throw ex;
                id = 0;
            }
            return id;
        }
        public static bool Check_WarterMark()
        {
            bool _chekTemp = true;
            try
            {
                string sql = "select ID_CHECK from T_WK";
                DataSet ds = HPCDataProvider.Instance().ExecSqlDataSet(sql);
                _chekTemp = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                //throw ex;
                _chekTemp = true;
            }
            return _chekTemp;
        }
        public static string GetColumnValues(string TableName, string ColumnName, string Where)
        {
            try
            {

                return HPCDataProvider.Instance().GetColumnValues(TableName, ColumnName, Where);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int GetColumnValuesOne(string TableName, string ColumnName, string Where)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = HPCDataProvider.Instance().GetStoreDataSet("Sp_GetColumnValues", new string[] { "@TableName", "@ColumnList", "@Where" }, new object[] { TableName, ColumnName, Where }).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                    return int.Parse(_dt.Rows[0][0].ToString());
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetColumnValuesTotal(string TableName, string ColumnName, string Where)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = HPCDataProvider.Instance().GetStoreDataSet("Sp_GetColumnValues", new string[] { "@TableName", "@ColumnList", "@Where" }, new object[] { TableName, ColumnName, Where }).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                    return _dt.Rows[0]["Total"].ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetStoreDataSet(string StoreName, string[] param1, object[] value)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet(StoreName, param1, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecStore(string StoreName, string[] param1, object[] value)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore(StoreName, param1, value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecStore(string StoreName)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore(StoreName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecSql(string sql)
        {
            try
            {
                HPCDataProvider.Instance().ExecSql(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ExecSqlDataSet(string sql)
        {
            try
            {
                return HPCDataProvider.Instance().ExecSqlDataSet(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LoadCombo(DropDownList cbo, string strTableName, string strTextField, string strValueField, string strBlank)
        {
            cbo.DataSource = HPCDataProvider.Instance().ExecSqlDataSet("SELECT * FROM " + strTableName);
            cbo.DataTextField = strTextField;
            cbo.DataValueField = strValueField;
            cbo.DataBind();
            if (!(strBlank == null))
            {
                ListItem item = new ListItem();
                item.Text = strBlank;
                item.Value = "";
                cbo.Items.Insert(0, item);
            }
        }
        public static void LoadCombo(DropDownList cbo, string strTableName, string strTextField, string strValueField, string valField, string strBlank)
        {
            cbo.DataSource = HPCDataProvider.Instance().ExecSqlDataSet("SELECT * FROM " + strTableName + " WHERE " + valField);
            cbo.DataTextField = strTextField;
            cbo.DataValueField = strValueField;
            cbo.DataBind();
            if (!(strBlank == null))
            {
                ListItem item = new ListItem();
                item.Text = strBlank;
                item.Value = "";
                cbo.Items.Insert(0, item);
            }
        }
        public static void Insert_News_Image(string strcontents, double News_ID)
        {
            HPCBusinessLogic.ImageFilesDAL imageDAL = new ImageFilesDAL();
            UltilFunc _untilDAL = new UltilFunc();
            string _sqldelete = "delete from T_NewsImages where  News_ID = " + News_ID;
            _untilDAL.ExecSql(_sqldelete);
            try
            {
                Regex regex = new Regex(
                    @"(?<=<img[^<]+?id=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(strcontents);
                string listId = "-1";
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _id = matchCollect[i].Value.Trim();
                    if (_id.Length > 0 && IsNumeric(_id))
                    {
                        listId += "," + _id;
                        imageDAL.Insert_ImagesInNews(News_ID, Convert.ToInt32(_id));
                    }
                }
                //delete

                string _sql = string.Format("delete from T_NewsImages where  Image_ID not in ({0}) and News_ID = " + News_ID, listId);
                _untilDAL.ExecSql(_sql);
            }
            catch { }
        }
        public static DataTable GetAllNewsRelation(string _listID)
        {
            DataTable _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("Select News_ID,News_Tittle FROM T_News Where News_ID in (" + _listID + ")");

                _return = _untilDAL.ExecSqlDataSet(_sql).Tables[0];

            }
            catch (Exception ex)
            {
                _return = null;
            }
            return _return;
        }
        /// <summary>
        /// Get Total Record with Status ALL for Data
        /// </summary>
        /// <param name="WhereCondition"></param>
        /// <param name="_store"></param>
        /// <returns></returns>
        public static int GetTotalCountStatus(string WhereCondition, string _store)
        {
            int _Id = 0;
            try
            {
                DataSet _ds = HPCDataProvider.Instance().GetStoreDataSet(_store, new string[] { "@where" }, new object[] { WhereCondition });
                _Id = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                _ds.Clear();
                _ds = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Id;
        }
        public static int GetTotalCountT_NewsStatus(string WhereCondition, string fulltext, string _store)
        {
            int _Id = 0;
            try
            {
                DataSet _ds = HPCDataProvider.Instance().GetStoreDataSet(_store, new string[] { "@where", "@search" }, new object[] { WhereCondition, fulltext });
                _Id = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                _ds.Clear();
                _ds = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Id;
        }
        /// <summary>
        /// Email check invalid for all
        /// </summary>
        /// <param name="_EmailCheck"></param>
        /// <returns></returns>
        public static string GetLanguagesByUser(int UserID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT DISTINCT(Ma_Ngonngu) FROM T_Nguoidung_NgonNgu Where [Ma_Nguoidung] = {0} ", UserID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Ma_Ngonngu"].ToString();
                        else _return += "," + _dt.Rows[i]["Ma_Ngonngu"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static int GetLatestID(string TableName, string IDFieldName)
        {
            HPCBusinessLogic.UltilFunc _untilFuncDAL = new HPCBusinessLogic.UltilFunc();
            try
            {
                DataSet ds = _untilFuncDAL.GetStoreDataSet("SP_GetLatestID", new string[] { "@TableName", "@IDFieldName" }, new object[] { TableName, IDFieldName });
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
            }
            catch
            {
                return 0;
            }
        }
        public static int SPGet_ChuyenMucDefault()
        {
            HPCBusinessLogic.UltilFunc _untilFuncDAL = new HPCBusinessLogic.UltilFunc();
            try
            {
                DataSet ds = _untilFuncDAL.GetStoreDataSet("SPGet_ChuyenMucDefault", new string[] { }, new object[] { });
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
            }
            catch
            {
                return 0;
            }
        }
        public static int GetLatestID(string TableName, string IDFieldName, string Condition)
        {

            HPCBusinessLogic.UltilFunc _untilFuncDAL = new HPCBusinessLogic.UltilFunc();
            try
            {
                DataSet ds = _untilFuncDAL.GetStoreDataSet("SP_GetLatestID", new string[] { "@TableName", "@IDFieldName", "@Condition" }, new object[] { TableName, IDFieldName, Condition });
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
            }
            catch
            {
                return 0;
            }
        }
        public string GetColumnValuesDynamic(string TableName, string ColumnName, string ReturnColum, string Where)
        {
            DataTable _dt = new DataTable();
            try
            {
                _dt = HPCDataProvider.Instance().GetStoreDataSet("Sp_GetColumnValues", new string[] { "@TableName", "@ColumnList", "@Where" }, new object[] { TableName, ColumnName, Where }).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                    return _dt.Rows[0][ReturnColum].ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BindCombox(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable)
        {
            BindCombox(oObject, mField_ID, mField_Value, mTable, "", "", "");
        }

        public static void BindCombox(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere)
        {
            BindCombox(oObject, mField_ID, mField_Value, mTable, sWhere, "", "");
        }
        public static void BindCombox(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere, string sText)
        {
            BindCombox(oObject, mField_ID, mField_Value, mTable, sWhere, sText, "");
        }
        public static void BindCombox(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere, string sText, string Parrent_ID)
        {
            UltilFunc _untilFuncDAL = new UltilFunc();
            DataTable _dt;
            DataTable _dtChild;
            DataTable _dtThree;
            int i = 0;
            oObject.DataSource = null;
            oObject.DataBind();
            if (sText != "")
            {
                if (sText != " ")
                {
                    oObject.Items.Add("--" + sText + "--");
                    oObject.Items[i].Value = "0"; ++i;
                }
            }
            else
            {
                oObject.Items.Add("-- Chọn giá trị --");
                oObject.Items[i].Value = "0"; ++i;
            }
            if (Parrent_ID != "")
            {
                _dt = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = 0 ").Tables[0];
                try
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int n = 0; n < _dt.Rows.Count; n++)
                        {
                            oObject.Items.Add(_dt.Rows[n][mField_Value].ToString());
                            oObject.Items[i].Value = _dt.Rows[n][mField_ID].ToString();
                            i += 1;
                            if (HPCBusinessLogic.UltilFunc.GetLatestID(mTable, Parrent_ID, "WHERE " + Parrent_ID + "=" + _dt.Rows[n][mField_ID]) > 0)
                            {
                                _dtChild = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = " + _dt.Rows[n][mField_ID] + " ORDER BY " + mField_ID).Tables[0];
                                if (_dtChild.Rows.Count > 0)
                                {
                                    for (int m = 0; m < _dtChild.Rows.Count; m++)
                                    {
                                        oObject.Items.Add(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + _dtChild.Rows[m][mField_Value].ToString());
                                        //oObject.Items.Add(HttpContext.Current.Server.UrlDecode("&nbsp;&nbsp;&nbsp;&nbsp;" + _dtChild.Rows[m][mField_Value].ToString()));
                                        oObject.Items[i].Value = _dtChild.Rows[m][mField_ID].ToString();
                                        i += 1;
                                        if (HPCBusinessLogic.UltilFunc.GetLatestID(mTable, Parrent_ID, "WHERE " + Parrent_ID + "=" + _dtChild.Rows[m][mField_ID]) > 0)
                                        {
                                            _dtThree = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = " + _dtChild.Rows[m][mField_ID] + " ORDER BY " + mField_ID).Tables[0];
                                            if (_dtThree.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < _dtThree.Rows.Count; k++)
                                                {
                                                    oObject.Items.Add(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + _dtThree.Rows[k][mField_Value].ToString());
                                                    oObject.Items[i].Value = _dtThree.Rows[k][mField_ID].ToString();
                                                    i += 1;
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
            }
            else
            {
                _dt = _untilFuncDAL.GetDataSet(mTable, mField_ID + "," + mField_Value, " " + sWhere).Tables[0];
                try
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int n = 0; n < _dt.Rows.Count; n++)
                        {
                            oObject.Items.Add(_dt.Rows[n][mField_Value].ToString());
                            oObject.Items[i].Value = _dt.Rows[n][mField_ID].ToString();
                            i += 1;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static void BindCombox_CategoryDequy(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere, string sText, string Parrent_ID)
        {

            string _sql = string.Empty;
            string _sqlChild = string.Empty;
            SqlService _sqlservice = new SqlService();
            SqlDataReader _reader;

            int i = 0;
            oObject.Items.Clear();
            oObject.DataSource = null;
            oObject.DataBind();
            if (sText != "")
            {
                if (sText != " ")
                {
                    oObject.Items.Add("---" + sText + "---");
                    oObject.Items[0].Value = "0"; ++i;

                }
            }
            else
            {
                oObject.Items.Add("-- Chọn giá trị --");
                oObject.Items[0].Value = "0"; ++i;
            }
            int Rank = 0;
            if (Parrent_ID != "")
            {
                _sql = "set dateformat dmy; SELECT " + mField_ID + "," + mField_Value + " FROM " + mTable + " " + sWhere + " AND " + Parrent_ID + " = 0 ";
                try
                {
                    _reader = _sqlservice.ExecuteSqlReader(_sql);
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {

                            ListItem item = new ListItem();
                            item.Attributes.Add("style", "font-weight: bold;font-family: Arial;color:#A52A2A");
                            item.Value = _reader[mField_ID].ToString();
                            item.Text = _reader[mField_Value].ToString();
                            oObject.Items.Add(item);
                            BinTreeCategorys(oObject, mField_ID, mField_Value, mTable, sWhere, sText, Parrent_ID, Rank, _reader[mField_ID].ToString());
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _sqlservice.Disconnect();
                }
            }
            else
            {
                _sql = "set dateformat dmy; SELECT [" + mField_ID + "],[" + mField_Value + "] FROM [" + mTable + "] " + sWhere;
                try
                {
                    _reader = _sqlservice.ExecuteSqlReader(_sql);
                    while (_reader.Read())
                    {
                        oObject.Items.Add(_reader[mField_Value].ToString());
                        oObject.Items[i].Value = _reader[mField_ID].ToString();
                        i += 1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _sqlservice.Disconnect();
                }
            }
        }
        public static void BinTreeCategorys(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere, string sText, string Parrent_ID, int Rank, string CategorysID)
        {
            Rank++;
            string _sqlChild = string.Empty;
            SqlService _sqlserviceChild = new SqlService();
            SqlDataReader _readerChild;
            if (HPCBusinessLogic.UltilFunc.GetLatestID(mTable, Parrent_ID, "WHERE " + Parrent_ID + "=" + CategorysID) > 0)
            {
                _sqlChild = "set dateformat dmy; SELECT " + mField_ID + "," + mField_Value + " FROM " + mTable + " WHERE " + Parrent_ID + " = " + CategorysID + " ORDER BY " + mField_ID;

                try
                {
                    _readerChild = _sqlserviceChild.ExecuteSqlReader(_sqlChild);

                    if (_readerChild.HasRows)
                    {
                        while (_readerChild.Read())
                        {
                            string blank = "";
                            for (int k = 0; k < Rank; k++)
                            {
                                blank = "&nbsp;&nbsp;&nbsp;&nbsp;" + blank;
                            }
                            ListItem item = new ListItem();
                            item.Value = _readerChild[mField_ID].ToString();
                            item.Text = HttpUtility.HtmlDecode(blank) + _readerChild[mField_Value].ToString();
                            oObject.Items.Add(item);

                            BinTreeCategorys(oObject, mField_ID, mField_Value, mTable, sWhere, sText, Parrent_ID, Rank, _readerChild[mField_ID].ToString());
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _sqlserviceChild.Disconnect();
                }

            }
            Rank--;
        }
        public static void BindCombox(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mTable, string sWhere, string sText, string Parrent_ID, string OrderBy)
        {
            UltilFunc _untilFuncDAL = new UltilFunc();
            DataTable _dt;
            DataTable _dtChild;
            DataTable _dtThree;
            int i = 0;
            oObject.DataSource = null;
            oObject.DataBind();

            if (sText != "")
            {
                if (sText != " ")
                {
                    oObject.Items.Add("--" + sText + "--");
                    oObject.Items[i].Value = "0"; ++i;
                }
            }
            else
            {
                oObject.Items.Add("--Chọn giá trị--");
                oObject.Items[i].Value = "0"; ++i;
            }
            if (Parrent_ID != "")
            {
                _dt = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = 0 " + OrderBy).Tables[0];
                try
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int n = 0; n < _dt.Rows.Count; n++)
                        {
                            oObject.Items.Add(_dt.Rows[n][mField_Value].ToString());
                            oObject.Items[i].Value = _dt.Rows[n][mField_ID].ToString();
                            i += 1;
                            if (HPCBusinessLogic.UltilFunc.GetLatestID(mTable, Parrent_ID, "WHERE " + Parrent_ID + "=" + _dt.Rows[n][mField_ID]) > 0)
                            {
                                _dtChild = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = " + _dt.Rows[n][mField_ID] + OrderBy).Tables[0];
                                if (_dtChild.Rows.Count > 0)
                                {
                                    for (int m = 0; m < _dtChild.Rows.Count; m++)
                                    {
                                        oObject.Items.Add(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + _dtChild.Rows[m][mField_Value].ToString());
                                        oObject.Items[i].Value = _dtChild.Rows[m][mField_ID].ToString();
                                        i += 1;
                                        if (HPCBusinessLogic.UltilFunc.GetLatestID(mTable, Parrent_ID, "WHERE " + Parrent_ID + "=" + _dtChild.Rows[m][mField_ID]) > 0)
                                        {
                                            _dtThree = _untilFuncDAL.GetDataSet(mTable, ' ' + mField_ID + "," + mField_Value, " " + sWhere + " AND " + Parrent_ID + " = " + _dtChild.Rows[m][mField_ID] + OrderBy).Tables[0];
                                            if (_dtThree.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < _dtThree.Rows.Count; k++)
                                                {
                                                    oObject.Items.Add(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + _dtThree.Rows[k][mField_Value].ToString());
                                                    oObject.Items[i].Value = _dtThree.Rows[k][mField_ID].ToString();
                                                    i += 1;
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
            }
            else
            {
                _dt = _untilFuncDAL.GetDataSet(mTable, mField_ID + "," + mField_Value, " " + sWhere + " " + OrderBy).Tables[0];
                try
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int n = 0; n < _dt.Rows.Count; n++)
                        {
                            oObject.Items.Add(_dt.Rows[n][mField_Value].ToString());
                            oObject.Items[i].Value = _dt.Rows[n][mField_ID].ToString();
                            i += 1;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static void BindComboxSoBao(System.Web.UI.WebControls.DropDownList oObject, string mField_ID, string mField_Value, string mField_Value1, string mField_Value2, string mTable, string sText, string Loaibao)
        {
            string _sql = string.Empty;
            string _sqlChild = string.Empty;
            SqlService _sqlservice = new SqlService();
            SqlDataReader _reader;

            int i = 0;
            oObject.DataSource = null;
            oObject.DataBind();
            if (sText != "")
            {
                if (sText != " ")
                {
                    oObject.Items.Add("--" + sText + "--");
                    oObject.Items[i].Value = "0"; ++i;

                }
            }
            else
            {
                oObject.Items.Add("-- Lựa chọn giá trị --");
                oObject.Items[i].Value = "0"; ++i;
            }

            _sql = "set dateformat dmy; SELECT [" + mField_ID + "], ([" + mField_Value1 + " ] + '  ---  '+CONVERT(nvarchar(30), [" + mField_Value2 + " ], 103)) as [" + mField_Value + "] FROM [" + mTable + "]  where Ma_AnPham=" + Loaibao + " and Ngay_Xuatban >=(GETDATE()-30) and Ngay_Xuatban <= (GETDATE()+30) order by Ngay_Xuatban DESC ";


            try
            {
                _reader = _sqlservice.ExecuteSqlReader(_sql);
                while (_reader.Read())
                {
                    oObject.Items.Add(_reader[mField_Value].ToString());
                    oObject.Items[i].Value = _reader[mField_ID].ToString();
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlservice.Disconnect();
            }

        }
        public static void BindComboxSoBao(System.Web.UI.WebControls.DropDownList oObject, int Loaibao, int Index)
        {
            string _sql = string.Empty;
            SqlService _sqlservice = new SqlService();

            _sql = "SP_BindDropDownListSoBao";
            try
            {
                _sqlservice.AddParameter("@Loaibao", SqlDbType.Int, Loaibao);
                _sqlservice.AddParameter("@index", SqlDbType.Int, Index);
                DataSet ds = _sqlservice.ExecuteSPDataSet(_sql);
                oObject.Items.Clear();
                oObject.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonsobao"), "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oObject.Items.Add(new ListItem(ds.Tables[0].Rows[i]["Name"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                _sqlservice.Disconnect();
            }

        }
        public static string Message(string str)
        {
            return "<script>alert('" + str + "');</script>";
        }
        public static int GetIndexControl(System.Web.UI.WebControls.DropDownList sControl, string iValue)
        {
            int iCount;
            int retVal = 0;
            iCount = sControl.Items.Count;
            for (int i = 0; i <= iCount - 1; i++)
            {
                if (sControl.Items[i].Value == iValue)
                {
                    return i;
                }
            }
            return retVal;
        }
        public static int GetIndexControlOne(System.Web.UI.WebControls.DropDownList sControl, string iValue)
        {
            int iCount;
            int retVal = 0;
            iCount = sControl.Items.Count;
            for (int i = 0; i <= iCount - 1; i++)
            {
                if (sControl.Items[i].Value == iValue)
                {
                    retVal = i;
                    goto exitForStatement0;
                }
            }
        exitForStatement0: ;
            return retVal;
        }
        public static string ApplicationPath()
        {
            return ConfigurationManager.AppSettings["ApplicationPath"];
        }
        public static string ServerPathDick()
        {
            return ConfigurationManager.AppSettings["ServerPathDis"];
        }
        public static string PhongVien()
        {
            return ConfigurationManager.AppSettings["PhongVien"];
        }
        public static string BientapVien()
        {
            return ConfigurationManager.AppSettings["BienTap"];
        }
        public static void BindComboboxYears(System.Web.UI.WebControls.DropDownList sControl, int iValue)
        {
            sControl.Items.Add(new ListItem("-- Chọn -- ", "0", true));
            for (int i = 2003; i <= iValue; i++)
            {
                sControl.Items.Add(i.ToString());
            }
        }
        public static void BindComboboxMonth(System.Web.UI.WebControls.DropDownList sControl, int iValue)
        {
            sControl.Items.Add(new ListItem("-- Chọn -- ", "0", true));
            for (int i = 1; i <= iValue; i++)
            {
                sControl.Items.Add(i.ToString());
            }
        }



        #region GET WHERE DIEU KIEN CHUYEN MUC
        public static string GetCategory4User(int UserID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Ma_Chuyenmuc FROM T_Nguoidung_Chuyenmuc Where Ma_Nguoidung = {0} ", UserID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Ma_Chuyenmuc"].ToString();
                        else _return += "," + _dt.Rows[i]["Ma_Chuyenmuc"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        #endregion

        #region GET WHERE DIEU KIEN ID CHUYEN MUC // ADD BY NVTHAI
        public static string GetPosition_Display(int _ID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Position_Display FROM T_Categorys Where [Categorys_ID] = {0} ", _ID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Position_Display"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static string GetBaivietLienquan_Display(int _ID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT News_Tittle FROM T_News Where [News_ID] = {0} ", _ID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["News_Tittle"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetChuthich_Anh(int _Matin, int _Maanh, int loai)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                if (loai == 1)
                    _sql = string.Format("SELECT ChuThich FROM T_Tinbai_Anh Where Ma_TinBai = {0} And Ma_Anh={1} ", _Matin, _Maanh);
                else
                    _sql = string.Format("SELECT ChuThich FROM T_Anh Where Ma_Anh={0} ", _Maanh);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["ChuThich"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetTieude_Anh(int _Maanh)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT TieuDe FROM T_Anh Where Ma_Anh = {0} ", _Maanh);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["TieuDe"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetPathPhoto_Anh(int _Maanh, int index)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Duongdan_Anh,TenFile_Goc FROM T_Anh Where Ma_Anh = {0} ", _Maanh);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (index == 1)
                            _return = _dt.Rows[i]["Duongdan_Anh"].ToString();
                        else
                            _return = _dt.Rows[i]["TenFile_Goc"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetNhuanbut_Anh(int _Maanh, int _matin)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Sotien FROM T_NhuanBut Where Ma_Anh = {0} And Ma_TinBai = {1} ", _Maanh, _matin);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Sotien"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetTenAnpham_Display(int _ID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Ten_AnPham FROM T_Anpham Where Ma_AnPham = {0} ", _ID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Ten_AnPham"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        #endregion

        #region Clear Dieu Kien TIM KIEM
        public static string SqlFormatText(string text)
        {
            if (text == null) return "";
            else return text.Replace("'", "''");
        }
        public static bool IsNumeric(string strcheck)
        {
            Regex regex = new Regex(
                @"^\d+([\.|,] \d+)?$",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                );
            return regex.IsMatch(strcheck) ? true : false;
        }
        #endregion

        #region ToDateTime
        public static DateTime ToDate(string x, string kieu)
        {
            try
            {
                int sp1 = x.IndexOf('/'), sp2 = x.LastIndexOf('/');
                int day, month, year;
                if (kieu.Equals("MM/dd/yyyy")) // MM/dd/yyyy hoac M/d/yyyy
                {
                    day = int.Parse(x.Substring(sp1 + 1, sp2 - sp1 - 1));
                    month = int.Parse(x.Substring(0, sp1));
                }
                else //'dd/MM/yyyy' hoac d/M/yyyy
                {
                    day = int.Parse(x.Substring(0, sp1));
                    month = int.Parse(x.Substring(sp1 + 1, sp2 - sp1 - 1));
                }
                year = int.Parse(x.Substring(sp2 + 1, 4));
                return new DateTime(year, month, day);
            }
            catch
            {
                throw new Exception("Sai kieu ngay thang");
            }
        }

        public static string CleanFormatTags(string Contents)
        {
            //Contents = Regex.Replace(Contents, "<(select|option|script|style|title)(.*?)>((.|\n)*?)</(select|option|script|style|title)>", " ", RegexOptions.IgnoreCase);
            //Contents = Regex.Replace(Contents, "&(nbsp|quot|copy);", "");
            Contents = Regex.Replace(Contents, "&(nbsp|quot);", "");
            //Contents = Regex.Replace(Contents, "'", "");
            //Contents = Regex.Replace(Contents, "~", "");
            //Contents = Regex.Replace(Contents, "\"", "");//dung sua      
            //Contents = Regex.Replace(Contents, "!", "");
            //Contents = Regex.Replace(Contents, "@", "");
            //Contents = Regex.Replace(Contents, "#", "");
            //Contents = Regex.Replace(Contents, "(;|--|create|drop|select|insert|delete|update|union|sp_|xp_)", "");
            Contents = Regex.Replace(Contents, "<([\\s\\S])+?>", " ", RegexOptions.IgnoreCase).Replace("  ", " ");

            return Contents;
        }

        #endregion

        #region REPLATE IP
        static public ArrayList Path_Replate
        {
            get
            {
                ArrayList _arr = new ArrayList();

                string _str = System.Configuration.ConfigurationSettings.AppSettings["Path_Replate"];
                char[] ch = { ';' };
                string[] arr = _str.Split(ch);
                for (int i = 0; i < arr.Length; i++)
                {
                    _arr.Add(arr[i]);
                }
                return _arr;

            }
        }
        public static string Rep_NewsOne(string _str, ArrayList _arr)
        {
            string _return = "";
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    _return = Rep_News(_str, _arr[i].ToString());
                }
            }
            return _return;
        }

        private static string Rep_News(string _str, string urlService)
        {
            try
            {
                return _str.Replace(urlService, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }
        public static bool checkExitsStatus(int ID, int status)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("Sp_CheckExitFromT_IdieaVersion", new string[] { "@Diea_ID", "@Status" }, new object[] { ID, status });
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public static bool checkExitsNewsRealates(string _ID, string _listID)
        {
            bool _return = false;
            try
            {
                string[] sArrProdID = null;
                char[] sep = { ',' };
                sArrProdID = _listID.ToString().Trim().Split(sep);
                for (int i = 0; i < sArrProdID.Length; i++)
                {
                    if (_ID.ToString() == sArrProdID[i].ToString())
                    {
                        _return = true;
                        break;
                    }
                    else
                        _return = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public static string GetTacgiaID(string _where)
        {
            if (string.IsNullOrEmpty(_where)) return string.Empty;
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                SSOLib.SSOLibDAL ssoDal = new SSOLib.SSOLibDAL();
                DataTable _dt = ssoDal.GetListUserByWhere(" IsDeleted=0  AND UserFullName LIKE N'%" + _where + "%' Order by UserFullName").Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["UserID"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static int WordCount(string Text)
        {
            string tmpStr;
            string tt = CleanHTMLSummary(Text);

            tmpStr = tt.Replace("\t", " ").Trim();
            tmpStr = tmpStr.Replace("\n", " ");
            tmpStr = tmpStr.Replace("\r", " ");

            while (tmpStr.IndexOf("  ") != -1)
                tmpStr = tmpStr.Replace("  ", " ");
            if (tt != "")
                return tmpStr.Split(' ').Length;
            else
                return 0;
        }
        public static string CleanHTMLSummary(string Contents)
        {
            Contents = Regex.Replace(Contents, "<(select|option|script|style|title)(.*?)>((.|\n)*?)</(select|option|script|style|title)>", " ", RegexOptions.IgnoreCase);

            Contents = Regex.Replace(Contents, "<div>", "");

            Contents = Regex.Replace(Contents, "</div>", "");
            Contents = Regex.Replace(Contents, "(;|--|create|drop|select|insert|delete|update|union|sp_|xp_)", "");
            Contents = Regex.Replace(Contents, "<([\\s\\S])+?>", " ", RegexOptions.IgnoreCase).Replace("  ", " ");
            Contents = Regex.Replace(Contents, "\r\n", "").Trim();

            Contents = Regex.Replace(Contents.Trim(), "Normal 0 false false false MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal 0  false false false     MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal 0 false false false MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal  0    false  false  false         MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal  0    false  false  false          MicrosoftInternetExplorer4", "");

            return (Contents);
        }
        public static DateTime CheckNullDate(object Value)
        {
            if (Value == DBNull.Value) return DateTime.MinValue;
            else return Convert.ToDateTime(Value);
        }
        public static void RunJavaScriptCode(string JavaScriptCode)
        {
            try
            {
                Page mPage = (Page)HttpContext.Current.Handler;
                mPage.ClientScript.RegisterStartupScript(typeof(Page), "", JavaScriptCode, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string GetTieuDetin(int _where)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT News_Tittle FROM  T_News Where News_ID = " + _where);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["News_Tittle"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetListIDTin(string Wherecondition)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT DISTINCT News_ID FROM  T_Suggestions " + Wherecondition);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["News_ID"].ToString();
                        else _return += "," + _dt.Rows[i]["News_ID"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static DateTime ToDateddMMyyyhhmm(string x)
        {
            try
            {
                int sp1 = x.IndexOf('/'), sp2 = x.LastIndexOf('/');
                int day, month, year, hh, mm;
                day = int.Parse(x.Substring(0, sp1));
                month = int.Parse(x.Substring(sp1 + 1, sp2 - sp1 - 1));
                year = int.Parse(x.Substring(sp2 + 1, 4));
                hh = int.Parse(x.Substring(sp2 + 6, 2));
                mm = int.Parse(x.Substring(sp2 + 9, 2));
                return new DateTime(year, month, day, hh, mm, 0);
            }
            catch
            {
                throw new Exception("Sai kieu ngay thang");
            }
        }
        public static string checkUrlExternal(string UrlLocal, string Ads_Images)
        {
            string strTemp = "";
            if (Ads_Images.Trim() != "")
            {
                if (!Ads_Images.Trim().StartsWith("http"))
                {
                    strTemp = UrlLocal + Ads_Images;
                }
                else
                    strTemp = Ads_Images;
            }
            return strTemp;
        }
        public static bool GetRoleEdit4User(int _userID, string _path)
        {
            bool _return = false;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                int _MenuID = UltilFunc.GetIDOfMenu(_path);
                string _sql = string.Format("SELECT R_Edit,R_Del,R_Add,R_Pub FROM T_UserMenu Where [User_ID] = {0} And [Menu_ID]={1} ", _userID, _MenuID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        bool R_Edit = Convert.ToBoolean(_dt.Rows[i]["R_Edit"].ToString());
                        bool R_Del = Convert.ToBoolean(_dt.Rows[i]["R_Del"].ToString());
                        bool R_Add = Convert.ToBoolean(_dt.Rows[i]["R_Add"].ToString());
                        bool R_Pub = Convert.ToBoolean(_dt.Rows[i]["R_Pub"].ToString());
                        if (R_Edit == true)
                        {
                            _return = R_Edit;
                            break;
                        }
                        else if (R_Del == true)
                        {
                            _return = R_Del;
                            break;
                        }
                        else if (R_Add == true)
                        {
                            _return = R_Add;
                            break;
                        }
                        else
                        {
                            _return = R_Pub;
                            break;
                        }

                    }
                }
                else
                    _return = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public static int GetIDOfMenu(string _where)
        {
            int _return = 0;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("SELECT ID FROM  T_Menus Where MenuURL ='" + _where + "'");

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == 0)
                            _return = Convert.ToInt32(_dt.Rows[i]["ID"].ToString());
                    }
                }
                else
                    _return = 0;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return _return;
        }
        public static int GetLoaiBao_Trang(int index)
        {
            HPCBusinessLogic.AnPhamDAL _DAL = new HPCBusinessLogic.AnPhamDAL();
            int _return = 0;
            try
            {
                if (index == 1)
                    _return = UltilFunc.GetColumnValuesOne("T_AnPham", "Ma_AnPham", " 1=1");
                else
                    _return = UltilFunc.GetColumnValuesOne("T_AnPham", "Sotrang", " 1=1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public static string GetTenNgonNgu(Object ID)
        {
            HPCBusinessLogic.DAL.NgonNgu_DAL _DAL = new HPCBusinessLogic.DAL.NgonNgu_DAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (_DAL.GetOneFromT_NgonNguByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = _DAL.GetOneFromT_NgonNguByID(Convert.ToInt32(ID)).TenNgonNgu.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenQuyTrinh(Object ID)
        {
            HPCBusinessLogic.DAL.TenQuyTrinh_DAL PDAL = new HPCBusinessLogic.DAL.TenQuyTrinh_DAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (PDAL.GetOneFromT_TenQuyTrinhByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = PDAL.GetOneFromT_TenQuyTrinhByID(Convert.ToInt32(ID)).Ten_Quytrinh.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenPhongBan(Object ID)
        {
            HPCBusinessLogic.DAL.PhongBan_DAL PDAL = new HPCBusinessLogic.DAL.PhongBan_DAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (PDAL.GetOneFromT_PhongbanByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = PDAL.GetOneFromT_PhongbanByID(Convert.ToInt32(ID)).Ten_Phongban.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenChuyenMuc(Object ID)
        {
            HPCBusinessLogic.ChuyenmucDAL dalcm = new ChuyenmucDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dalcm.GetOneFromT_ChuyenmucByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = dalcm.GetOneFromT_ChuyenmucByID(Convert.ToInt32(ID)).Ten_ChuyenMuc.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenAnpham(Object ID)
        {
            HPCBusinessLogic.AnPhamDAL dalap = new AnPhamDAL();
            ChuyenmucDAL _dalcm = new ChuyenmucDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dalap.GetOneFromT_AnPhamByID(Convert.ToInt32(ID)) != null)
                        str = dalap.GetOneFromT_AnPhamByID(Convert.ToInt32(ID)).Ten_AnPham;
                    else
                        str = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenSoBaoFromT_Vitri_Tinbai(Object ID, int index)
        {
            UltilFunc Ulti = new UltilFunc();
            HPCBusinessLogic.SobaoDAL sb = new SobaoDAL();
            DataTable dt = new DataTable();
            if (ID != DBNull.Value && ID.ToString() != "")
            {
                string sql = "select Ma_Sobao,Trang from T_Vitri_Tinbai where Ma_Tinbai=" + ID.ToString();
                DataSet ds = Ulti.ExecSqlDataSet(sql);
                dt = ds.Tables[0];
            }
            string str = string.Empty;
            try
            {
                if (dt.Rows.Count > 0)
                    if (index == 0)
                    {
                        if (sb.GetOneFromT_SobaoByID(Convert.ToInt32(dt.Rows[0][0].ToString())) == null)
                            str = "";
                        else
                        {
                            str = sb.GetOneFromT_SobaoByID(Convert.ToInt32(dt.Rows[0][0].ToString())).Ten_Sobao.ToString();
                            DateTime Ngayxuatban = sb.GetOneFromT_SobaoByID(Convert.ToInt32(dt.Rows[0][0].ToString())).Ngay_Xuatban;
                            str += " <br/>" + Ngayxuatban.ToString("dd/MM/yyyy");
                        }
                    }
                    else
                    {
                        if (int.Parse(dt.Rows[0][1].ToString()) > -1 && int.Parse(dt.Rows[0][1].ToString()) > 0)
                            str = dt.Rows[0][1].ToString();
                        else
                            str = "";
                    }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenTacGiaTinBai(Object ID)
        {
            HPCBusinessLogic.NguoidungDAL daluser = new NguoidungDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)).TenDaydu.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetUserFullName(Object ID)
        {
            HPCBusinessLogic.NguoidungDAL daluser = new NguoidungDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (daluser.GetUserByUserName_ID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = daluser.GetUserByUserName_ID(Convert.ToInt32(ID)).UserFullName.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetDiaChiTacGia(Object ID, int index)
        {
            HPCBusinessLogic.NguoidungDAL daluser = new NguoidungDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        if (index == 1)
                            str = daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)).Diachi.ToString();
                        else if (index == 2)
                            str = daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)).Loai.ToString();
                        else if (index == 3)
                            str = daluser.GetOneFromT_NguoidungByID(Convert.ToInt32(ID)).Ma_Vung.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenKhachHang(Object ID)
        {
            KhachhangDAL dal = new KhachhangDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dal.GetOneFromT_KhachHangByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = dal.GetOneFromT_KhachHangByID(Convert.ToInt32(ID)).Ten_KhachHang.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetSobao(Object ID)
        {
            SobaoDAL dal = new SobaoDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dal.GetOneFromT_SobaoByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                    {
                        str = dal.GetOneFromT_SobaoByID(Convert.ToInt32(ID)).Ten_Sobao.ToString();
                        DateTime datexb = dal.GetOneFromT_SobaoByID(Convert.ToInt32(ID)).Ngay_Xuatban;
                        str += " <br/> <b> " + datexb.ToString("dd/MM/yyyy");
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTrangbao(Object ID)
        {
            string str = "";
            try
            {
                if (ID.ToString() == "")
                    str = "";
                else
                {
                    str = "Trang " + ID.ToString();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetKichCoQuangCao(Object ID)
        {
            KichthuocDAL dal = new KichthuocDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dal.GetOneFromT_KichthuocByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = dal.GetOneFromT_KichthuocByID(Convert.ToInt32(ID)).Ten_Kichthuoc.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTenHopDong(Object ID)
        {
            HopdongDAL dal = new HopdongDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    if (dal.GetOneFromT_HopdongByID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = dal.GetOneFromT_HopdongByID(Convert.ToInt32(ID)).hopdongso.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetNhuanButAnh(Object ID)
        {
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    str = ID.ToString();
                else
                    str = "Nhập nhuận bút ảnh";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static DataTable GetTentacgiaAutoSuggestBox(string strName, string where)
        {

            SSOLib.SSOLibDAL ssoDal = new SSOLib.SSOLibDAL();
            DataTable dt = ssoDal.GetListUserByWhere(where + " AND UserFullName LIKE N'%" + strName + "%' Order by UserName").Tables[0];
            return dt;

        }
        public static DataTable GetTrangSoBaoFrom_T_VitriTiBai(double id)
        {
            UltilFunc ulti = new UltilFunc();
            string _Sql = "select Trang,Ma_Sobao,Ma_Congviec from T_Vitri_Tinbai where Ma_Tinbai=" + id;
            DataTable dt = ulti.ExecSqlDataSet(_Sql).Tables[0];
            return dt;
        }
        public static DataTable GetNhuanbuttinbaiAnhFromT_Nhuanbut(double id, int Matacgia, int index)
        {
            string _Sql = "";
            DataTable dt = new DataTable();
            UltilFunc ulti = new UltilFunc();
            if (index == 0)
                _Sql = "select Sotien,Ma_NhuanBut from T_NhuanBut where (Ma_anh=0 or Ma_anh is null) and Ma_tacgia=" + Matacgia + " and  Ma_TinBai=" + id;
            else
                _Sql = "select Sotien from T_NhuanBut where Ma_anh<>0 and Ma_NhuanBut=" + id;
            dt = ulti.ExecSqlDataSet(_Sql).Tables[0];
            return dt;
        }
        public static string GetNguoiTralaiNgayTralai(Object ID, int index)
        {
            HPCBusinessLogic.DAL.TinBaiDAL dal = new HPCBusinessLogic.DAL.TinBaiDAL();
            string str = "";

            DataTable dt = new DataTable();
            if (ID != DBNull.Value)
                dt = dal.Sp_SelectOneFromT_PhienbanWithMaTinbai(int.Parse(ID.ToString())).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (index == 0)
                    str = dt.Rows[0]["Ma_Nguoitao"].ToString();
                else
                {
                    str = dt.Rows[0]["NgayTao"].ToString();
                    str = DateTime.Parse(str).ToString("dd/MM/yyyy HH:mm:ss");
                }
            }
            else
            {
                if (dal.load_T_news(double.Parse(ID.ToString())).Ma_Nguoitao != 0)
                {
                    if (index == 0)
                        str = dal.load_T_news(double.Parse(ID.ToString())).Ma_Nguoitao.ToString();
                    else
                    {
                        str = dal.load_T_news(double.Parse(ID.ToString())).NgayTao.ToString();
                        str = DateTime.Parse(str).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }

            return str;
        }
        public static bool getnhuanbutgop(Object ID, Object Matacgia)
        {
            UltilFunc ulti = new UltilFunc();
            bool check = false;
            if (ID != DBNull.Value && Matacgia != DBNull.Value)
            {
                string sql = "select Sotien from T_NhuanBut where Ma_TinBai=" + ID + " and Ma_tacgia<>" + Matacgia + " and (Ma_Anh=0 or Ma_Anh is null)";
                DataTable dt = ulti.ExecSqlDataSet(sql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    return true;
            }
            return check;
        }
        public static string GetBaitralai_Display(int _ID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Diea_ID FROM [T_Idiea] Where [CV_id] = {0} AND Status=33 ", _ID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Diea_ID"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static string GetPath_PDF(Object _ID)
        {
            if (_ID != DBNull.Value)
                return "Trang " + _ID.ToString();
            else
                return "";
        }
        public static string SeeMore(Object ghichu)
        {
            string str = string.Empty;

            if (ghichu.ToString() != "" && ghichu != DBNull.Value)
            {
                str = "<span class=\"ChuthichDefault\">" + ghichu + "</span>";
                if (ghichu.ToString().Length > 45)
                    str += "...<a style='display:inline-block;width:60px;height:24px;line-height:24px;color:blue;font-size:13px;float:right;cursor:pointer' class=\"viewmore\">See more</a>";
            }
            return str;
        }
        public static string GetImageAttach(Object ID)
        {
            DataTable _dt = new DataTable();
            TinBaiAnhDAL _dal = new TinBaiAnhDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value && ID.ToString() != "")
                    _dt = _dal.Sp_SelectTinAnhDynamic("Ma_TinBai=" + ID.ToString(), "").Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    str = "<div style=\"float: left; width: 30%; text-align: right; padding-top: 3px\"><span class=\"linkGridForm\" style=\"font-size: 16px;\">" + _dt.Rows.Count + "</span></div>"
                         + " <div style=\"float: left; width: 70%; text-align: left\"><img src=\"../Dungchung/Images/Image-JPEG-icon.png\" alt='' /></div>";
                }
                else
                    str = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        #region KEYWORD ADD BY NVTHAI

        public static void InsertKeywords(string _listKey, int _userID)
        {
            string sWhere = "";
            string[] sArrProdID = null;
            char[] sep = { ',' };
            sArrProdID = _listKey.ToString().Trim().Split(sep);
            for (int i = 0; i < sArrProdID.Length; i++)
            {
                sWhere = " KeyWord LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(sArrProdID[i].ToString().Trim()));
                if (UltilFunc.checkExitsKeyWord(sWhere) != false)
                {
                    UltilFunc.InsertKeyWords(sArrProdID[i].ToString().Trim(), DateTime.Now, _userID);
                }

            }

        }
        public static void InsertKeyWords(string _keywords, DateTime _date, int _userID)
        {
            SqlService _CommonSQL = new SqlService();
            string strSP = "CMS_InsertT_KeyWords";
            try
            {
                _CommonSQL.AddParameter("@KeyWord", SqlDbType.NVarChar, _keywords);
                _CommonSQL.AddParameter("@DateCreate", SqlDbType.DateTime, _date);
                _CommonSQL.AddParameter("@UserCreate", SqlDbType.Int, _userID);
                _CommonSQL.ExecuteSP(strSP);
            }
            catch (Exception ex)
            { }
            finally
            {
                _CommonSQL.CloseConnect();
                _CommonSQL.Disconnect();
            }

        }
        public static bool checkExitsKeyWord(string _keyword)
        {
            DataSet _ds = null;
            try
            {
                _ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_SelectOneFromT_KeyWords]", new string[] { "@WhereCondition" }, new object[] { _keyword });
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public static string GetUserByGroupID(int GroupID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT DISTINCT User_ID FROM T_UserGroups Where [Group_ID] = {0} ", GroupID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["User_ID"].ToString();
                        else
                            _return += "," + _dt.Rows[i]["User_ID"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }

        public static string GetFileNameAndTitle(int _where, bool _boolApp)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                if (_boolApp == false)
                {
                    _sql = string.Format("SELECT PhotoTitles FROM  T_Photos Where ID = " + _where);
                }
                else
                {
                    _sql = string.Format("SELECT FileName FROM  T_Photos Where ID = " + _where);
                }
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            if (_boolApp == false)
                                _return += _dt.Rows[i]["PhotoTitles"].ToString();
                            else
                                _return += _dt.Rows[i]["FileName"].ToString();
                        }
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetCategoryNameOrLinhvucName(int _where, bool _boolApp)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                if (_boolApp == false)
                {
                    _sql = string.Format("SELECT Category_Name FROM  T_Categorys Where Categorys_ID = " + _where);
                }
                else
                {
                    _sql = string.Format("SELECT Linhvuc_Name FROM  T_Linhvuc Where ID = " + _where);
                }
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            if (_boolApp == false)
                                _return += _dt.Rows[i]["Category_Name"].ToString();
                            else
                                _return += _dt.Rows[i]["Linhvuc_Name"].ToString();
                        }
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }

        public static string GetPathFile(string _where)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("SELECT PhotoPath2Orginal FROM  T_Photos Where ID = " + _where);

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            _return += GetPathPhotosRelation(UltilFunc.ApplicationPath()) + _dt.Rows[i]["PhotoPath2Orginal"].ToString();
                        }
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetPathPhotosRelation(string _value)
        {
            string _return = "";
            string[] sArrProdID = null;
            char[] sep = { '/' };
            if (_value.ToString() != null)
            {
                sArrProdID = _value.ToString().Trim().Split(sep);
                _return = sArrProdID[0].ToString() + "/" + sArrProdID[1].ToString() + "/" + sArrProdID[2].ToString();
            }
            return _return;
        }
        public static string GetFileName(string _where)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("SELECT FileName FROM  T_Photos Where ID = " + _where);

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            _return += _dt.Rows[i]["FileName"].ToString();
                        }
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static string GetCountPhotoOfUser(int _userID, string _status)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("Select COUNT(ID) AS PhotoID FROM T_Photos Where User_Upload = " + _userID + " And PhotoStatus in (" + _status + ")");

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            _return += _dt.Rows[i]["PhotoID"].ToString();
                        }
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        public static string GetCountSMSNewsOfUser(int _userID, string _status)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "";
                _sql = string.Format("Select COUNT(ID) AS SMSID FROM T_MessageReceid Where Daxoa=0 And Manguoinhan = " + _userID + " And Trangthai = " + _status);

                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                        {
                            _return += _dt.Rows[i]["SMSID"].ToString();
                        }
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                _return = "0";
                throw ex;

            }
            return _return;
        }
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"            
            

        };
        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = CleanFormatTags(str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]));

            }

            return str;
        }
        public static string GetPathPhotos(int _where)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT PhotoPath2Orginal FROM  T_Photos Where ID = " + _where);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["PhotoPath2Orginal"].ToString();
                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }

        public static string ReturnDatetime(string str)
        {
            string[] sArrProdID = null;
            char[] sep = { '-' };
            sArrProdID = str.ToString().Trim().Split(sep);
            string strReturn = "";
            if (sArrProdID.Length == 3)
                strReturn = sArrProdID[2].ToString() + "/" + sArrProdID[1].ToString() + "/" + sArrProdID[0].ToString();
            return strReturn;
        }
        public static string GetListIDPhotos(DataTable _dt)
        {
            string _return = null;
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["ID"].ToString();
                        else _return += "," + _dt.Rows[i]["ID"].ToString();
                    }
                }
                else
                    _return = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        #endregion


        #region Ghi Log T_ActionHistory and Lich su thao tac tin bai
        public static void Log_Action(int UserID, string Fullname, DateTime datemodify, int Machucnang, string ActionsCode)
        {
            Lichsu_Thaotac_HethongDAL dal = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong _t_action = new T_Lichsu_Thaotac_Hethong();
            _t_action.Ma_Nguoidung = UserID;
            _t_action.TenDaydu = Fullname;
            _t_action.HostIP = IpAddress();
            _t_action.NgayThaotac = datemodify;
            _t_action.Thaotac = ActionsCode;
            _t_action.Ma_Chucnang = Machucnang;
            dal.InserT_Lichsu_Thaotac_Hethong(_t_action);
        }
        public static void Log_Thaotactinbai(int UserID, string Fullname, DateTime Ngaythaotac, string Thaotac, double MaTinBai)
        {
            HPCBusinessLogic.DAL.T_LichsuthaotactinbaiDAL dal = new HPCBusinessLogic.DAL.T_LichsuthaotactinbaiDAL();
            T_Lichsu_Thaotac_TinBai _t_action = new T_Lichsu_Thaotac_TinBai();
            _t_action.Log_ID = 0;
            _t_action.Ma_Nguoidung = UserID;
            _t_action.TenDaydu = Fullname;
            _t_action.HostIP = IpAddress();
            _t_action.Thaotac = Thaotac;
            _t_action.NgayThaotac = Ngaythaotac;
            _t_action.Ma_TinBai = MaTinBai;
            dal.InserT_Lichsu_Thaotac_TinBai(_t_action);
        }
        public static string IpAddress()
        {
            HttpContext hc1 = HttpContext.Current;
            string strIp;
            strIp = hc1.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = hc1.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
        #endregion

        public static void AlertJS(string Msg)
        {
            System.Web.UI.Page currentPage;
            currentPage = (System.Web.UI.Page)HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "AlertMessage", "alert('" + Msg + "');", true);

        }
        public static string GetUserName(Object ID)
        {
            string str = "";
            if (ID == DBNull.Value)
                str = "";
            else
            {
                try
                {
                    if (HPCBusinessLogic.UltilFunc.GetUserByUserName_ID(Convert.ToInt32(ID)) == null)
                        str = "";
                    else
                        str = HPCBusinessLogic.UltilFunc.GetUserByUserName_ID(Convert.ToInt32(ID)).UserName;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return str;
        }
        public static T_Users GetUserByUserName_ID(int userID)
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
        #region RemoveEnterCode
        public static string RemoveMultiSpaces(string strSrc)
        {
            Regex r = new Regex(@"\s+");
            return r.Replace(strSrc, @" ");
        }
        public static string RemoveEnterCode(string strSrc)
        {
            return RemoveMultiSpaces(strSrc.Replace("\t", " ").Replace("\r", " ").Replace("\n", " "));
        }
        #endregion

        #region ClearHTML in Noi dung tin bai
        public static string CleanWordHtml(string html)
        {
            StringCollection sc = new StringCollection();
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");

            sc.Add(@"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|!\[)[^>]*?>");

            foreach (string s in sc)
            {
                html = Regex.Replace(html, s, "", RegexOptions.IgnoreCase);
            }
            html = Regex.Replace(html, @"DISPLAY\s?:\s?none", "", RegexOptions.IgnoreCase);
            html = RemoveTagAttribute(html, "i", "style");
            html = RemoveTagAttribute(html, "b", "style");

            return html;
        }
        public static string RemoveTagAttribute(string Source, string Tag, string Attribute)
        {
            return Regex.Replace(Source, string.Format(@"(<{0}\b[^>]*?\b)({1}=""(?:[^""]*)"")", Tag, Attribute), "$1");
        }
        public static string CleanUpHTMLCode(string strHTML)
        {
            string cleanstring;
            cleanstring = strHTML;

            //1. Replace all Div Tags with P tags
            cleanstring = ConvertDIV2P(cleanstring);
            cleanstring = RemoveTextAlignAtributeWithEmpty(cleanstring, "");
            cleanstring = ProcessSPANWithFONT(cleanstring);

            cleanstring = Regex.Replace(cleanstring, @"<([^>]*)(?:class|lang|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            cleanstring = Regex.Replace(cleanstring, @"<([^>]*)(?:class|lang|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);

            //2. Replace <p>&nbsp;</p> with ""
            cleanstring = Regex.Replace(cleanstring, @"<p> *(?:&nbsp;)* *(?:<b>)? *(?:&nbsp;)* *(?:</b>)? *(?:&nbsp;)* *</p>\r\n", "", RegexOptions.IgnoreCase);
            cleanstring = Regex.Replace(cleanstring, @"<p> *(?:&nbsp;)* *(?:<b>)? *(?:&nbsp;)* *(?:</b>)? *(?:&nbsp;)* *</p>\r\n", "", RegexOptions.IgnoreCase);




            //3. Remove attribute Display:none of SPAN Tags
            cleanstring = Regex.Replace(cleanstring, @"DISPLAY\s?:\s?none", "", RegexOptions.IgnoreCase);



            //4. Replace <p>&nbsp;</p> with ""
            cleanstring = Regex.Replace(cleanstring, @"<b> *(?:&nbsp;)* *(?:<b>)? *(?:&nbsp;)* *(?:</b>)? *(?:&nbsp;)* *</b>", "", RegexOptions.IgnoreCase);
            cleanstring = Regex.Replace(cleanstring, @"<b> *(?:&nbsp;)* *(?:<b>)? *(?:&nbsp;)* *(?:</b>)? *(?:&nbsp;)* *</b>", "", RegexOptions.IgnoreCase);


            return cleanstring;
        }
        private static string ConvertDIV2P(string strInPut)
        {
            string strTemp;
            // Remove Begin tag DIV 
            strTemp = System.Text.RegularExpressions.Regex.Replace(strInPut, @"<( )*div([^>])*>", "<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //Remove end tags Div 
            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"(<( )*(/)( )*div( )*>)", "</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return strTemp;
        }
        private static string ProcessSPANWithFONT(string strOrg)
        {
            string strResult;
            string strTemp;
            try
            {
                strTemp = strOrg;
                strResult = ConvertSpan2FontTags(strTemp, "<font size=\"4\" face=\"Times New Roman\">");
                strResult = ConvertCloseSpan2CloseFontTags(strResult, "</font>");
            }
            catch (Exception ex)
            {
                strResult = strOrg;
                throw ex;
            }
            return strResult;
        }
        private static string RemoveTextAlignAtributeWithEmpty(string strOrg, string strReplace)
        {
            //string strReplace = "text-align:(.*?);"; Begin<text-align:> ---- innner (.*?)----- end ;
            //string pattern = @"text-align:(.*?);";
            string pattern = @"text-align\s?:\s?center";
            string pattern1 = @"text-align\s?:\s?left";
            string pattern2 = @"text-align\s?:\s?justify";
            string pattern3 = @"text-align\s?:\s?right";
            string pattern4 = @"style\s?=\s?"""""; // Remove style=""

            string resultString = null;
            try
            {
                resultString = Regex.Replace(strOrg, pattern, strReplace, RegexOptions.IgnoreCase);
                resultString = Regex.Replace(resultString, pattern1, strReplace, RegexOptions.IgnoreCase);
                resultString = Regex.Replace(resultString, pattern2, strReplace, RegexOptions.IgnoreCase);
                resultString = Regex.Replace(resultString, pattern3, strReplace, RegexOptions.IgnoreCase);
                resultString = Regex.Replace(resultString, pattern4, strReplace, RegexOptions.IgnoreCase);

            }
            catch (ArgumentException ex)
            {
                resultString = strOrg;
                throw ex;
            }
            return resultString;
        }
        private static string ConvertSpan2FontTags(string strOrg, string strReplace)
        {
            //string strReplace = "<font size=\"4\" face=\"Times New Roman\">"; // font-size: 14pt
            string pattern = @"<span[^>]*>";
            string resultString = null;
            try
            {
                resultString = Regex.Replace(strOrg, pattern, strReplace, RegexOptions.IgnoreCase);
            }
            catch (ArgumentException ex)
            {
                resultString = strOrg;
            }
            return resultString;
        }
        private static string ConvertCloseSpan2CloseFontTags(string strOrg, string strReplace)
        {
            string pattern = @"</span>";
            string resultString = null;
            try
            {
                resultString = Regex.Replace(strOrg, pattern, strReplace, RegexOptions.IgnoreCase);
            }
            catch (ArgumentException ex)
            {
                resultString = strOrg;
                throw ex;
            }
            return resultString;
        }
        #endregion

        #region PHAN CHAT HE THONG

        public static string GetUserNameLogin(string username)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("select User_Name from T_UserLogin where User_Name='{0}' AND Loggedin='true'", username);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                    _return = _dt.Rows[0]["User_Name"].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }

        public static string GetListOnline(string _username)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT User_Name,Full_Name,Loggedin FROM T_UserLogin Where User_Name<> N'" + _username + "' ");
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["User_Name"].ToString() + ";" + _dt.Rows[i]["Full_Name"].ToString() + ";" + _dt.Rows[i]["Loggedin"].ToString();
                        else
                            _return += "|" + _dt.Rows[i]["User_Name"].ToString() + ";" + _dt.Rows[i]["Full_Name"].ToString() + ";" + _dt.Rows[i]["Loggedin"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        public static string ReadMessageHistory(string _nguoigui, string _nguoinhan)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = "select sender,[message] from [Messages] where (recepient=N'" + _nguoinhan + "' and sender=N'" + _nguoigui + "' OR recepient=N'" + _nguoigui + "' and sender=N'" + _nguoinhan + "') ";
                _sql += "and senddate>=convert(datetime,'" + DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy") + "',104) and  (delivered <> 1 or delivered is null) order by senddate asc";
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["sender"].ToString() + ";" + _dt.Rows[i]["message"].ToString();
                        else
                            _return += "|" + _dt.Rows[i]["sender"].ToString() + ";" + _dt.Rows[i]["message"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _return;
        }
        #endregion
        public static string ReplapceYoutoubeWidth(string str, string _Width)
        {
            try
            {
                Regex regex = new Regex(
                    @"(?<=<iframe[^<]+?width=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _url = matchCollect[i].Value.Trim();
                    if (_url.Length > 0)
                    {
                        if (!_url.StartsWith("http"))
                        {
                            string _urlImg = matchCollect[i].Value.Trim();
                            string _urlImgReplate = _Width;
                            if (!str.ToLower().Contains(_urlImgReplate.Trim().ToLower()))
                                str = System.Text.RegularExpressions.Regex.Replace(str, _urlImg, _urlImgReplate, RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            catch { }
            return str;
        }
        public static string ReplapceYoutoubeHight(string str, string _Hight)
        {
            try
            {
                Regex regex = new Regex(
                    @"(?<=<iframe[^<]+?height=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _url = matchCollect[i].Value.Trim();
                    if (_url.Length > 0)
                    {
                        if (!_url.StartsWith("http"))
                        {
                            string _urlImg = matchCollect[i].Value.Trim();
                            string _urlImgReplate = _Hight;
                            if (!str.ToLower().Contains(_urlImgReplate.Trim().ToLower()))
                                str = System.Text.RegularExpressions.Regex.Replace(str, _urlImg, _urlImgReplate, RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            catch { }
            return str;
        }
        public static bool CheckFrames(string str)
        {
            bool _frame = false;
            try
            {
                Regex regex = new Regex(
                    @"(?<=<iframe[^<]+?height=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _url = matchCollect[i].Value.Trim();
                    if (_url.Length > 0)
                    {
                        if (!_url.StartsWith("http"))
                        {
                            _frame = true;
                        }
                    }
                }
            }
            catch { }
            return _frame;
        }

        public static string GetCategoryNameByAdsID(int _ID)
        {
            string _return = null;
            try
            {
                UltilFunc _untilDAL = new UltilFunc();
                string _sql = string.Format("SELECT Ten_ChuyenMuc FROM T_ChuyenMuc Where [Ma_ChuyenMuc] = {0} ", _ID);
                DataTable _dt = _untilDAL.ExecSqlDataSet(_sql).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_return == null)
                            _return += _dt.Rows[i]["Ten_ChuyenMuc"].ToString();

                    }
                }
                else
                    _return = "";
            }
            catch (Exception ex)
            {
                _return = "";
                throw ex;

            }
            return _return;
        }
        public static int ReturnTotalNhuanbut(DataTable _dtReport)
        {
            int _return = 0;
            try
            {
                if (_dtReport.Rows.Count > 0)
                {
                    for (int i = 0; i <= _dtReport.Rows.Count - 1; i++)
                    {
                        if (_dtReport.Rows[i]["Total"].ToString() != "")
                            _return += Convert.ToInt32(_dtReport.Rows[i]["Total"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                _return = 0;
            }
            return _return;
        }
        #region T_HistorySource
        public static void WriteLogHistorySource(int _UserID, string _UserName, string _Tittle, string _Category, int _MenuID, string _Note, int _Type)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_InsertT_HistorySource", new string[] { "@User_ID", "@UserName", "@Tittle", "@Category", "@Menu_ID", "@Note", "@Type" }, new object[] { _UserID, _UserName, _Tittle, _Category, _MenuID, _Note, _Type });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet BindGridT_HistorySource(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_ListT_HistorySource", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    public class PageView
    {
        DataView _viewsource = new DataView();
        int totalRecords = 0;
        public int TotalRecords
        {
            get
            {
                return totalRecords;
            }
            set
            {
                totalRecords = value;
            }
        }
        public DataView ViewSource
        {
            get
            {
                return _viewsource;
            }
            set
            {
                _viewsource = value;
            }
        }
        public bool HasResults
        {
            get
            {
                if (_viewsource.Count > 0)
                    return true;
                return false;
            }
        }
    }
    public class DataReaderConverters
    {
        //		public DataReaderConverters()
        //		{}
        public static DataSet ConvertDataReaderToDataSet(System.Data.SqlClient.SqlDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                // Create new data table
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        string columnName = (string)dataRow["ColumnName"];
                        // Add the column definition to the data table
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            reader.Close();
            return dataSet;
        }

        /// <summary>
        /// [static] PAB.Data.Utils.DataReaderConverters.ConvertDataReaderToDataTable
        /// converts SqlDataReader to a DataTable
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns>System.Data.DataTable</returns>
        public static DataTable ConvertDataReaderToDataTable(System.Data.SqlClient.SqlDataReader reader)
        {
            System.Data.DataTable table = reader.GetSchemaTable();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn dc;
            System.Data.DataRow row;
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dc = new System.Data.DataColumn();
                if (!dt.Columns.Contains(table.Rows[i]["ColumnName"].ToString()))
                {
                    dc.ColumnName = table.Rows[i]["ColumnName"].ToString();
                    dc.Unique = Convert.ToBoolean(table.Rows[i]["IsUnique"]);
                    dc.AllowDBNull = Convert.ToBoolean(table.Rows[i]["AllowDBNull"]);
                    dc.ReadOnly = Convert.ToBoolean(table.Rows[i]["IsReadOnly"]);
                    al.Add(dc.ColumnName);
                    dt.Columns.Add(dc);
                }
            }
            while (reader.Read())
            {
                row = dt.NewRow();
                for (int i = 0; i < al.Count; i++)
                {
                    row[((System.String)al[i])] = reader[(System.String)al[i]];
                }
                dt.Rows.Add(row);
            }
            reader.Close();
            return dt;
        }
        public static DataTable ConvertDataReaderToDataTable_NotClose(System.Data.SqlClient.SqlDataReader reader)
        {
            System.Data.DataTable table = reader.GetSchemaTable();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn dc;
            System.Data.DataRow row;
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dc = new System.Data.DataColumn();
                if (!dt.Columns.Contains(table.Rows[i]["ColumnName"].ToString()))
                {
                    dc.ColumnName = table.Rows[i]["ColumnName"].ToString();
                    dc.Unique = Convert.ToBoolean(table.Rows[i]["IsUnique"]);
                    dc.AllowDBNull = Convert.ToBoolean(table.Rows[i]["AllowDBNull"]);
                    dc.ReadOnly = Convert.ToBoolean(table.Rows[i]["IsReadOnly"]);
                    al.Add(dc.ColumnName);
                    dt.Columns.Add(dc);
                }
            }
            while (reader.Read())
            {
                row = dt.NewRow();
                for (int i = 0; i < al.Count; i++)
                {
                    row[((System.String)al[i])] = reader[(System.String)al[i]];
                }
                dt.Rows.Add(row);
            }
            //reader.Close();
            return dt;
        }

    }

}
