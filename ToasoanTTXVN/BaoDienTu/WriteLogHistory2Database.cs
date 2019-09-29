using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCBusinessLogic;
using HPCInfo;

namespace HPCBusinessLogic
{
    public class WriteLogHistory2Database
    {
      
        public static void WriteHistory2Database(double UserID,string UserFullName,string strNotes, object MenuID, string ActionCode, double intNewID, int type)
        {
            ActionHistoryDAL actionDAL = new ActionHistoryDAL();
            T_ActionHistory action = new T_ActionHistory();
            try
            {                
                action.UserID = UserID;
                action.FullName = UserFullName;
                action.Notes = strNotes;
                action.ActionsCode = ActionCode;
                action.HostIP = IpAddress();
                action.DateModify = DateTime.Now;
                action.News_ID = intNewID;
                action.Menu_ID = Convert.ToInt32(MenuID);
                action.Types = type;
                actionDAL.InserT_Action(action);
            }
            catch (Exception ex)
            {
 
            }

        }
        protected static string IpAddress()
        {
            string strIp;
            strIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
        
    }
}
