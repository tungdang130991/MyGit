using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using HPCBusinessLogic;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
namespace HPCComponents
{
    public class HPCSecurity
    {
        public static HPCPrincipal CurrentUser
        {
            get
            {
                HPCPrincipal r;

                if (HttpContext.Current.User is HPCPrincipal)
                    r = (HPCPrincipal)HttpContext.Current.User;

                else
                    r = new HPCPrincipal(HttpContext.Current.User.Identity, null);
                return r;
            }
            set
            {
                HttpContext.Current.User = value;
            }
        }
        public static string Encrypt(string cleanString)
        {
            byte[] clearBytes;
            byte[] hashedBytes;
            clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            hashedBytes = ((HashAlgorithm)(CryptoConfig.CreateFromName("MD5"))).ComputeHash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }
        public static bool IsAccept(int MenuID)
        {       
            try
            {
                HPCBusinessLogic.UltilFunc _untilDAL = new HPCBusinessLogic.UltilFunc();
                SSOLib.SSOLibDAL _ssolibdal = new SSOLib.SSOLibDAL();
                SSOLib.ServiceAgent.T_Users user = _ssolibdal.GetT_UsersByUserName(HPCSecurity.CurrentUser.Identity.Name);
                DataSet _ds = _untilDAL.GetStoreDataSet("SP_GetRole4UserMenu", new string[] { "@UserID", "@Menu_ID" }, new object[] { user.UserID, MenuID.ToString() });
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
