using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib.ServiceAgent;
using HPCShareDLL;

[ScriptService]
public class InboxService : System.Web.Services.WebService
{
    [WebMethod]
    
    public string GetLatestNumberOfEmails()
    {
        T_Users _user = new T_Users();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        string doituong = string.Empty;

        try
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            DataTable dt = new DataTable();
            if (_user != null)
            {
                dt = HPCDataProvider.Instance().GetStoreDataSet("sp_gettotal_tinbai", new string[] { "@UserID" }, new object[] { _user.UserID }).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    doituong = dt.Rows[0]["Total"].ToString() + ';' + dt.Rows[0]["MenuLink"].ToString();
            }
        }
        catch { }
        return doituong;
    }
}
