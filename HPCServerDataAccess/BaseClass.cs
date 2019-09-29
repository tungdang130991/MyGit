using System;
using HPCShareDLL;
using System.Data.SqlClient;
using System.Data;
using HPCInfo;
using System.Text;
using System.Reflection;
using System.IO;
using SSOLib;
using SSOLib.ServiceAgent;


namespace HPCServerDataAccess
{
    public class BaseClass : HPCDataProvider
    {
        #region Private Property
        string databaseOwner = "dbo";
        string connectionString;
        #endregion

        #region Constructor
        public BaseClass(string databaseOwner, string connectionString)
        {
            this.connectionString = connectionString;
            this.databaseOwner = databaseOwner;
        }
        #endregion


        public override void UpdateObject(object obj, string spName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = spName;
            foreach (PropertyInfo propertyinfo in obj.GetType().GetProperties())
            {
                if (propertyinfo.PropertyType.ToString() == "System.DateTime")
                {
                    if ((DateTime)propertyinfo.GetValue(obj, null) == DateTime.MinValue || (DateTime)propertyinfo.GetValue(obj, null) == DateTime.MaxValue)
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, DBNull.Value));
                    else
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                }
                else
                {
                    _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                }
            }
            try
            {
                _sqlservice.ExecuteSP(sql);
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
        public override object GetObjectByID(string sp_name, string Id, string TypeName, string FieldIDName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = sp_name;
            SqlDataReader reader;
            _sqlservice.AddParameter(new SqlParameter("@" + FieldIDName, Id));
            try
            {
                reader = _sqlservice.ExecuteSPReader(sql);
                return CBO.FillObject(reader, Type.GetType("HPCInfo." + TypeName + ",HPCInfo"));
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

        public override int InsertObjectReturn(object obj, string spName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = spName;
            foreach (PropertyInfo propertyinfo in obj.GetType().GetProperties())
            {
                if (propertyinfo.PropertyType.ToString() == "System.DateTime")
                {
                    if ((DateTime)propertyinfo.GetValue(obj, null) == DateTime.MinValue || (DateTime)propertyinfo.GetValue(obj, null) == DateTime.MaxValue)
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, DBNull.Value));
                    else
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                }
                else
                    _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
            }
            SqlParameter paraOutput = new SqlParameter("@ReturnValue", SqlDbType.Int);
            paraOutput.Value = 0;
            paraOutput.Direction = ParameterDirection.Output;
            _sqlservice.AddParameter(paraOutput);
            try
            {
                _sqlservice.ExecuteSP(sql);
                return (int)paraOutput.Value;
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
        public override void InsertObject(object obj, string spName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = spName;
            foreach (PropertyInfo propertyinfo in obj.GetType().GetProperties())
            {
                if (propertyinfo.PropertyType.ToString() == "System.DateTime")
                {
                    if ((DateTime)propertyinfo.GetValue(obj, null) == DateTime.MinValue || (DateTime)propertyinfo.GetValue(obj, null) == DateTime.MaxValue)
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, DBNull.Value));
                    else
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                }
                else
                    _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
            }
            try
            {
                _sqlservice.ExecuteSP(sql);
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

        public override DataSet GetDataSet(string TableName, string ColumnList, string Where)
        {
            string _sql = "Sp_GetColumnValues";
            SqlService _sqlservice = new SqlService(connectionString);
            _sqlservice.AddParameter("@TableName", SqlDbType.NVarChar, TableName);
            _sqlservice.AddParameter("@ColumnList", SqlDbType.NVarChar, ColumnList);
            _sqlservice.AddParameter("@Where", SqlDbType.NVarChar, Where);
            try
            {
                return _sqlservice.ExecuteSPDataSet(_sql);
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
        public override string GetColumnValues(string TableName, string ColumnName, string Where)
        {
            string _sql = "Sp_GetColumnValues";
            SqlService _sqlservice = new SqlService(connectionString);
            SqlDataReader _reader;
            StringBuilder _result = new StringBuilder();
            string tmp = string.Empty;
            _sqlservice.AddParameter("@TableName", SqlDbType.NVarChar, TableName);
            _sqlservice.AddParameter("@ColumnList", SqlDbType.NVarChar, ColumnName);
            _sqlservice.AddParameter("@Where", SqlDbType.NVarChar, Where);
            try
            {
                _reader = _sqlservice.ExecuteSPReader(_sql);
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        if (_reader[ColumnName] != DBNull.Value)
                            _result.Append(_reader[ColumnName].ToString() + ";");
                        else
                            _result.Append(" ;");
                    }
                }
                tmp = _result.ToString();
                return tmp.EndsWith(";") ? tmp.Substring(0, tmp.Length - 1) : tmp;

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
        public override DataSet GetStoreDataSet(string StoreName, string[] param1, object[] value)
        {
            string _sql = StoreName;
            SqlService _sqlservice = new SqlService(connectionString);
            for (int i = 0; i < param1.Length; i++)
            {
                _sqlservice.AddParameter(new SqlParameter(param1[i], value[i]));
            }
            try
            {
                return _sqlservice.ExecuteSPDataSet(_sql);
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
        public override double ExecStoreReturn(string StoreName, string[] param1, object[] value)
        {
            string _sql = StoreName;
            SqlService _sqlservice = new SqlService(connectionString);
            for (int i = 0; i < param1.Length; i++)
            {
                _sqlservice.AddParameter(new SqlParameter(param1[i], value[i]));
            }
            SqlParameter paraOutput = new SqlParameter("@ReturnValue", SqlDbType.Float);
            paraOutput.Value = 0;
            paraOutput.Direction = ParameterDirection.Output;
            _sqlservice.AddParameter(paraOutput);
            try
            {
                _sqlservice.ExecuteSP(_sql);
                return (double)paraOutput.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlservice.CloseConnect();
                _sqlservice.Disconnect();
            }
        }
        public override void ExecStore(string StoreName, string[] param1, object[] value)
        {
            string _sql = StoreName;
            SqlService _sqlservice = new SqlService(connectionString);
            for (int i = 0; i < param1.Length; i++)
            {
                _sqlservice.AddParameter(new SqlParameter(param1[i], value[i]));
            }
            try
            {
                _sqlservice.ExecuteSP(_sql);
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
        public override void ExecStore(string StoreName)
        {
            string _sql = StoreName;
            SqlService _sqlservice = new SqlService(connectionString);
            _sqlservice.ExecuteSP(_sql);
            _sqlservice.Disconnect();
        }

        public override void ExecSql(string sql)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            try
            {
                _sqlservice.ExecuteSql(sql);
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

        public override DataSet ExecStoreDataSet(string StoreName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            try
            {
                return _sqlservice.ExecuteSPDataSet(StoreName);
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
        public override DataSet ExecSqlDataSet(string sql)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            try
            {
                return _sqlservice.ExecuteSqlDataSet(sql);
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
        public override void InsertObject(object obj)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = "Sp_Insert" + obj.GetType().Name;
            foreach (PropertyInfo propertyinfo in obj.GetType().GetProperties())
            {
                if (propertyinfo.PropertyType.ToString() == "System.DateTime")
                {
                    if ((DateTime)propertyinfo.GetValue(obj, null) == DateTime.MinValue || (DateTime)propertyinfo.GetValue(obj, null) == DateTime.MaxValue)
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, DBNull.Value));
                    else
                        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                }
                else
                    _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
            }
            try
            {
                _sqlservice.ExecuteSP(sql);
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
        public override void UpdateObject(object obj)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = "Sp_UpdateRow" + obj.GetType().Name;
            foreach (PropertyInfo propertyinfo in obj.GetType().GetProperties())
            {
                //if (propertyinfo.PropertyType.ToString() == "System.DateTime")
                //{
                //    if ((DateTime)propertyinfo.GetValue(obj, null) == DateTime.MinValue || (DateTime)propertyinfo.GetValue(obj, null) == DateTime.MaxValue)
                //        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, DBNull.Value));
                //    else
                //        _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                //}
                //else
                //{
                _sqlservice.AddParameter(new SqlParameter("@" + propertyinfo.Name, propertyinfo.GetValue(obj, null)));
                // }
            }
            try
            {
                _sqlservice.ExecuteSP(sql);
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
        public override object GetObjectByID(string Id, string TypeName, string FieldIDName)
        {
            SqlService _sqlservice = new SqlService(connectionString);
            string sql = "Sp_SelectOneFrom" + TypeName;
            SqlDataReader reader;
            _sqlservice.AddParameter(new SqlParameter("@" + FieldIDName, Id));
            try
            {
                reader = _sqlservice.ExecuteSPReader(sql);
                return CBO.FillObject(reader, Type.GetType("HPCInfo." + TypeName + ",HPCInfo"));
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

        public override object GetObjectByCondition(string TypeName, string whereCondition)
        {
            object obj;
            SqlService _sqlservice = new SqlService(connectionString);

            string sql = "SP_GetObjectByCondition";
            try
            {
                _sqlservice.AddParameter("@TypeName", SqlDbType.NVarChar, TypeName);
                _sqlservice.AddParameter("@whereCondition", SqlDbType.NVarChar, whereCondition);
                obj = CBO.FillObject(_sqlservice.ExecuteSPReader(sql), Type.GetType("HPCInfo." + TypeName + ",HPCInfo"));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _sqlservice.Disconnect();
            }
            return obj;
        }
        
        public override bool Login(string userName, string passWord)
        {
            string _sql = "Sp_UserLogin";
            SqlService _sqlservice = new SqlService(connectionString);
            _sqlservice.AddParameter("@userName", SqlDbType.NVarChar, userName);
            _sqlservice.AddParameter("@passWord", SqlDbType.NVarChar, passWord);
            try
            {
                return _sqlservice.ExecuteSPReader(_sql).HasRows ? true : false;
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

        public override void Insert_T_NewsEvent_Content_From_T_NewsEvent_Content(int news_id, int _CopyFrom)
        {
            string _sql = "Insert_T_NewsEvent_Content_From_T_NewsEvent_Content";
            SqlService _sqlservice = new SqlService();
            try
            {
                _sqlservice.AddParameter("@ID", SqlDbType.Int, news_id, true);
                _sqlservice.AddParameter("@Copyfrom", SqlDbType.Int, _CopyFrom, true);

                _sqlservice.ExecuteSP(_sql);
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
        public override void Insert_T_Event_Category_From_T_Event_Category(int news_id)
        {
            string _sql = "[Insert_T_Event_Category_From_T_Event_Category]";
            SqlService _sqlservice = new SqlService();
            try
            {
                _sqlservice.AddParameter("@ID", SqlDbType.Int, news_id, true);

                _sqlservice.ExecuteSP(_sql);
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
        public override void Insert_T_CategoryChuyenDe_From_T_CategoryChuyenDe(int news_id, int copyfrom)
        {
            string _sql = "[Insert_T_CategoryChuyenDe_From_T_CategoryChuyenDe]";
            SqlService _sqlservice = new SqlService();
            try
            {
                _sqlservice.AddParameter("@ID", SqlDbType.Int, news_id, true);
                _sqlservice.AddParameter("@CopyFrom", SqlDbType.Int, copyfrom, true);

                _sqlservice.ExecuteSP(_sql);
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
        public override void Insert_T_NewsChuyenDe_From_T_NewsChuyenDe(int news_id, int copyfrom, int Lang_id, int status, int nguoisua, DateTime ngaysua)
        {
            string _sql = "[Insert_T_NewsChuyenDe_From_T_NewsChuyenDe]";
            SqlService _sqlservice = new SqlService();
            try
            {
                _sqlservice.AddParameter("@ID", SqlDbType.Int, news_id, true);
                _sqlservice.AddParameter("@CopyFrom", SqlDbType.Int, copyfrom, true);
                _sqlservice.AddParameter("@Lang_Id", SqlDbType.Int, Lang_id, true);
                _sqlservice.AddParameter("@News_Status", SqlDbType.Int, status, true);
                _sqlservice.AddParameter("@News_EditorID", SqlDbType.Int, nguoisua, true);
                _sqlservice.AddParameter("@News_DateEdit", SqlDbType.DateTime, ngaysua, true);

                _sqlservice.ExecuteSP(_sql);
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
        public override bool Check_Nhanban_T_Categorys_Chuyende(int id)
        {
            string _sql = "Check_Nhanban_T_Category_ChuyenDe";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@ID", SqlDbType.Int, id, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override bool LanquageExitsTranlate(int id, int langId)
        {
            string _sql = "LanquageExitsTranlate";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@New_ID", SqlDbType.Int, id, true);
                _sqlservice.AddParameter("@Languages_ID", SqlDbType.Int, langId, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override bool LanquageExitsTranlatein_T_Album_Categories(int id, int langId)
        {
            string _sql = "[LanquageExitsTranlatein_T_Album_Categories]";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@Cat_Album_ID", SqlDbType.Int, id, true);
                _sqlservice.AddParameter("@Languages_ID", SqlDbType.Int, langId, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override bool ExitsTranlate_T_Album_Photo(int id, int langId)
        {
            string _sql = "[ExitsTranlate_T_Album_Photo]";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@Alb_Photo_ID", SqlDbType.Int, id, true);
                _sqlservice.AddParameter("@Languages_ID", SqlDbType.Int, langId, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override bool ExitsTranlate_T_Multimedia(int id, int langId)
        {
            string _sql = "[Sp_ExitsTranlate_T_Multimedia]";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@id", SqlDbType.Int, id, true);
                _sqlservice.AddParameter("@Languages_ID", SqlDbType.Int, langId, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override bool ExitsTranlate_T_Photo_Even(int id, int langId)
        {
            string _sql = "[ExitsTranlate_T_Photo_Event]";
            SqlService _sqlservice = new SqlService();
            DataSet _ds;
            try
            {
                _sqlservice.AddParameter("@Photo_ID", SqlDbType.Int, id, true);
                _sqlservice.AddParameter("@Languages_ID", SqlDbType.Int, langId, true);
                _ds = _sqlservice.ExecuteSPDataSet(_sql);
                if (_ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;

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
        public override T_Users GetUserByUserPass(string userName, string passWord)
        {
            SSOLib.SSOLibDAL _ssolibdal = new SSOLib.SSOLibDAL();
            return _ssolibdal.LogInAuthen(userName, passWord);
        }
        public override T_Users GetUserByUserName(string userName)
        {
            SSOLib.SSOLibDAL _ssolibdal = new SSOLib.SSOLibDAL();
            return _ssolibdal.GetT_UsersByUserName(userName);
        }
        public override T_Users GetUserByUserName_ID(int userID)
        {
            SSOLib.SSOLibDAL _ssolibdal = new SSOLib.SSOLibDAL();
            return _ssolibdal.GetT_UsersByUserId(userID);
        }
        private T_Users GetFromReader_T_Users(SqlDataReader _reader)
        {
            T_Users t_users = null;
            if (!_reader.IsClosed)
            {
                t_users = new T_Users();
                t_users.UserID = Convert.ToInt32(_reader["UserID"]);
                if (_reader["UserName"] != DBNull.Value) t_users.UserName = Convert.ToString(_reader["Username"]);
                if (_reader["UserPass"] != DBNull.Value) t_users.UserPass = Convert.ToString(_reader["UserPass"]);
                if (_reader["UserFullName"] != DBNull.Value) t_users.UserFullName = Convert.ToString(_reader["UserFullName"]);
                if (_reader["UserEmail"] != DBNull.Value) t_users.UserEmail = Convert.ToString(_reader["UserEmail"]);
                if (_reader["UserMobile"] != DBNull.Value) t_users.UserMobile = Convert.ToString(_reader["UserMobile"]);
                if (_reader["UserAddress"] != DBNull.Value) t_users.UserAddress = Convert.ToString(_reader["UserAddress"]);
                if (_reader["UserBirthday"] != DBNull.Value) t_users.UserBirthday = Convert.ToDateTime(_reader["UserBirthday"]);
                if (_reader["UserActive"] != DBNull.Value) t_users.UserActive = Convert.ToBoolean(_reader["UserActive"]);
                if (_reader["DateCreated"] != DBNull.Value) t_users.DateCreated = Convert.ToDateTime(_reader["DateCreated"]);
                if (_reader["DateModify"] != DBNull.Value) t_users.DateModify = Convert.ToDateTime(_reader["DateModify"]);
                if (_reader["UserCreate"] != DBNull.Value) t_users.UserCreate = Convert.ToInt32(_reader["UserCreate"]);


            }
            return t_users;
        }
        public override T_RolePermission GetRole4UserMenu(int User_ID, int Menu_ID)
        {
            string _sql = "Sp_GetRoleForUserMenu";
            SqlService _sqlservice = new SqlService(connectionString);
            T_RolePermission _role = new T_RolePermission();
            SqlDataReader _reader;
            try
            {
                _sqlservice.AddParameter("@User_ID", SqlDbType.Int, User_ID);

                _sqlservice.AddParameter("@Menu_ID", SqlDbType.Int, Menu_ID);
                _reader = _sqlservice.ExecuteSPReader(_sql);
                if (_reader.HasRows)
                {
                    _reader.Read();
                    if (_reader["Doc"] != DBNull.Value)
                        _role.R_Read = Convert.ToBoolean(_reader["Doc"]);
                    if (_reader["Ghi"] != DBNull.Value)
                        _role.R_Write = Convert.ToBoolean(_reader["Ghi"]);
                    if (_reader["Xoa"] != DBNull.Value)
                        _role.R_Delete = Convert.ToBoolean(_reader["Xoa"]);
                    _reader.Close();
                }
                else
                {
                    _role.R_Read = false;
                    _role.R_Write = false;
                    _role.R_Delete = false;
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
            return _role;
        }
        public override string[] ReadMessage(string UserName)
        {
            if (UserName != null)
            {
                string Message, Sender;
                string[] MessageReadResult = new string[2];
                SqlConnection sqlconn = new SqlConnection(connectionString);
                sqlconn.Open();
                SqlParameter recepientParam = new SqlParameter("@Recepient", UserName);
                SqlParameter MessageParam = new SqlParameter("@Message", "");
                SqlParameter SenderParam = new SqlParameter("@Sender", "");

                MessageParam.Direction = ParameterDirection.Output;
                MessageParam.DbType = DbType.String;
                MessageParam.Size = 1073741823;
                SenderParam.Direction = ParameterDirection.Output;
                SenderParam.Size = 2147483647;

                SqlCommand queryAddMessage = new SqlCommand("ReadMessage");
                queryAddMessage.CommandType = CommandType.StoredProcedure;

                queryAddMessage.Connection = sqlconn;
                queryAddMessage.Parameters.Add(recepientParam);
                queryAddMessage.Parameters.Add(MessageParam);
                queryAddMessage.Parameters.Add(SenderParam);

                try
                {
                    queryAddMessage.ExecuteNonQuery();
                    sqlconn.Close();
                    Message = queryAddMessage.Parameters["@Message"].Value.ToString();
                    Sender = queryAddMessage.Parameters["@Sender"].Value.ToString();
                    MessageReadResult[0] = Sender;
                    MessageReadResult[1] = Message;
                    return MessageReadResult;
                }
                catch (Exception e)
                {
                    sqlconn.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }

        }        
        public override bool WriteMessage(string Sender, string Recepient, string Message)
        {
            if (Sender != null && Recepient != null)
            {
                string MessageDateTime = DateTime.Now.ToString();
                SqlConnection sqlconn = new SqlConnection(connectionString);
                sqlconn.Open();
                SqlParameter senderParam = new SqlParameter("@sender", Sender);
                SqlParameter recepient = new SqlParameter("@recepient", Recepient);
                SqlParameter senddateParam = new SqlParameter("@senddate", MessageDateTime);
                SqlParameter messageParam = new SqlParameter("@message", Message);
                senddateParam.DbType = DbType.DateTime;

                SqlCommand queryAddMessage = new SqlCommand("insert into Messages(sender,recepient,senddate,message,status) values(@sender,@recepient,@senddate,@message,0)");
                queryAddMessage.Connection = sqlconn;
                queryAddMessage.Parameters.Add(senderParam);
                queryAddMessage.Parameters.Add(recepient);
                queryAddMessage.Parameters.Add(senddateParam);
                queryAddMessage.Parameters.Add(messageParam);
                try
                {
                    queryAddMessage.ExecuteNonQuery();
                    sqlconn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    sqlconn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
    }
}
