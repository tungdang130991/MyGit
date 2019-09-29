<%@ WebHandler Language="C#" Class="SecureChatServer" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using System.Text;
using HPCBusinessLogic;
using SSOLib.ServiceAgent;
using HPCComponents;
public class SecureChatServer : IHttpHandler, IRequiresSessionState
{
    HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
    T_Users _user = new T_Users();
    UltilFunc Ulti = new UltilFunc();
    public void ProcessRequest(HttpContext context)
    {
        _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
        int EncryptionKey = 3; //Encryption Key: 0 to disable encryption
        string RequestUserId = context.Request.Headers["UserID"];
        string RequestCode;
        RequestCode = context.Request.Headers["RequestCode"];

        switch (RequestCode)
        {

            #region  Handle Recieve Message Request
            case "SC003":  //indicates request to poll for incomming messages
                {

                    string LoggedInUser, Message, Sender;
                    string[] MessageReadResult = new string[2];

                    //Recepient = context.Request.Headers["Recepient"];
                    try
                    {
                        HPCBusinessLogic.DAL.LoginChatDAL userlogin = new HPCBusinessLogic.DAL.LoginChatDAL();
                        LoggedInUser = userlogin.IsUserLoggedIn(_user.UserName);

                        if (LoggedInUser != null)
                        {

                            MessageReadResult = userlogin.ReadMessage(LoggedInUser);

                            if (MessageReadResult != null && (MessageReadResult[0] != "" || MessageReadResult[1] != ""))
                            {
                                Sender = Encrypt(MessageReadResult[0], EncryptionKey);
                                //Message = Encrypt(MessageReadResult[1], EncryptionKey);
                                Message = MessageReadResult[1].ToString();
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-OK',Sender:'" + Sender + "'");
                                context.Response.Write(Message);

                            }
                            else
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                            }
                        }
                        else
                        {
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-LoggedOut'");

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed Request SC003 : " + e.ToString());
                        context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Exception'");

                    }


                }
                break;

            #endregion

            #region Handle User List Request
            case "SC004":  //indicates request to get online users list
                {
                    string UserList, LoggedInUser;
                    try
                    {
                        HPCBusinessLogic.DAL.LoginChatDAL userlogin = new HPCBusinessLogic.DAL.LoginChatDAL();
                        LoggedInUser = userlogin.IsUserLoggedIn(_user.UserName);
                        if (LoggedInUser != null)
                        {
                            UserList = userlogin.GetOnlineUsersList(LoggedInUser);

                            if (UserList != null || UserList != "|")
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-OK'");
                                context.Response.Write(UserList);
                            }
                            else
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                            }
                        }
                        else
                        {
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-LoggedOut'");

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed Request SC004 : " + e.ToString());
                        context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Exception'");

                    }

                }
                break;

            #endregion

            #region Handle Send Message Request
            case "SC005":  //indicates request to send message
                {
                    string Recepient, Message, LoggedInUser;

                    try
                    {
                        HPCBusinessLogic.DAL.LoginChatDAL userlogin = new HPCBusinessLogic.DAL.LoginChatDAL();
                        LoggedInUser = userlogin.IsUserLoggedIn(_user.UserName);
                        if (LoggedInUser != null)
                        {

                            //Message = Decrypt(context.Request.Params["Message"], EncryptionKey);
                            Recepient = Decrypt(context.Request.Params["Recepient"], EncryptionKey);
                            Message = context.Request.Params["Message"].ToString();


                            if (userlogin.WriteMessage(LoggedInUser, Recepient, Message))
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-OK'");
                            }
                            else
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                            }

                        }
                        else
                        {
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-LoggedOut'");

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed Request SC005 : " + e.ToString());
                        context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Exception'");

                    }

                }
                break;

            #endregion

            #region Handle Check loggedin Session  Request

            case "SC007":
                {
                    string LoggedInUser;
                    string UserFullName = string.Empty;
                    try
                    {

                        HPCBusinessLogic.DAL.LoginChatDAL userlogin = new HPCBusinessLogic.DAL.LoginChatDAL();
                        LoggedInUser = userlogin.IsUserLoggedIn(_user.UserName);
                        if (LoggedInUser != null)
                        {
                            UserFullName = _user.UserFullName;
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-OK',RUN:'" + LoggedInUser + "',ResponseUserFullName:'" + UserFullName + "'");
                        }
                        else
                        {
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed Request SC007 : " + e.ToString());
                        context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Exception'");

                    }
                }
                break;
            #endregion
            #region  Handle Recieve Message Request History
            case "SC008":  //indicates request to poll for incomming messages
                {

                    string LoggedInUser;
                    string MessageReadResult;

                    //Recepient = context.Request.Headers["Recepient"];
                    try
                    {
                        HPCBusinessLogic.DAL.LoginChatDAL userlogin = new HPCBusinessLogic.DAL.LoginChatDAL();
                        LoggedInUser = userlogin.IsUserLoggedIn(_user.UserName);

                        if (LoggedInUser != null)
                        {

                            MessageReadResult = UltilFunc.ReadMessageHistory(LoggedInUser, RequestUserId);

                            if (MessageReadResult != null || MessageReadResult != "/")
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-OK'");
                                context.Response.Write(MessageReadResult);
                            }
                            else
                            {
                                context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                            }
                        }
                        else
                        {
                            context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-LoggedOut'");

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed Request SC008 : " + e.ToString());
                        context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Exception'");

                    }


                }
                break;

            #endregion
            default:
                {
                    context.Response.AddHeader("CustomHeaderJSON", "ResponseStatus:'RS-Failed'");
                }
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public static string Encrypt(string textToEncrypt, int key)
    {
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
        char c;
        for (int i = 0; i < textToEncrypt.Length; i++)
        {
            c = inSb[i];
            //////c = (char)((c ^ key));        //Nozel:  Xor Cipher . But encoded characters are not always allowed in http headers
            if (c <= 32)
            {
                //Keep c as it is              //Nozel:   Bypass Invalid characters which are not supported in Http headers
            }
            else
            {
                c = (char)((c - key));           //Nozel:   Normal substitution Cipher
            }
            outSb.Append(c);
        }
        return outSb.ToString();
    }
    public static string Decrypt(string textToEncrypt, int key)
    {
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
        char c;
        for (int i = 0; i < textToEncrypt.Length; i++)
        {
            c = inSb[i];
            //////c = (char)((c ^ key));        //Nozel:  Xor Cipher . But encoded characters are not always allowed in http headers

            if (c <= 32)
            {
                //Keep c as it is               //Nozel:   Bypass Invalid characters which are not supported in Http headers
            }
            else
            {
                c = (char)((c + key));            //Nozel:   Normal substitution Cipher
            }
            outSb.Append(c);
        }
        return outSb.ToString();


    }

}