using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCShareDLL;
using HPCInfo;
//using HPCComponents;
namespace HPCBusinessLogic.DAL
{
    public class LoginChatDAL
    {
        public int InsertT_UserLogin(T_UserLogin obj)
        {
            int _insert = 0;
            try
            {
                _insert = HPCDataProvider.Instance().InsertObjectReturn(obj, "Chat_InsertT_UserLogin");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _insert;
        }
        
        

        public string IsUserLoggedIn(string _username)
        {
            string username = string.Empty;

            try
            {
                username = UltilFunc.GetUserNameLogin(_username);
                if (username != null)
                {
                    return (username.ToString());
                }
                else
                {
                    return (null);
                }

            }
            catch (Exception e)
            {
                return (null);
            }
        }
        //public bool Logout(string username)
        //{

        //    string _sql = string.Format(" User_Name='{0}'", username);
        //    try
        //    {
        //        UpdateStatusT_UserLoginDynamic(_sql);
        //        if (IsUserLoggedIn(username) != null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
        //public void UpdateStatusT_UserLoginDynamic(string WhereCondition)
        //{
        //    try
        //    {
        //        HPCDataProvider.Instance().ExecStore("Chat_UpdateT_UserLoginDynamic", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public string GetOnlineUsersList(string _username)
        {
            string UserList = "";
            try
            {
                return UserList = UltilFunc.GetListOnline(_username);

            }
            catch (Exception e)
            {
                return (null);
            }
        }
        public string[] ReadMessage(string UserName)
        {
            if (UserName != null)
            {

                string[] MessageReadResult = new string[2];

                try
                {
                    MessageReadResult = HPCDataProvider.Instance().ReadMessage(UserName);
                    return MessageReadResult;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        
        public bool WriteMessage(string Sender, string Recepient, string Message)
        {
            if (Sender != null && Recepient != null)
            {
                try
                {
                    return HPCDataProvider.Instance().WriteMessage(Sender, Recepient, Message);
                }
                catch (Exception e)
                {
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
