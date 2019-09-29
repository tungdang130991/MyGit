using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using HPCBusinessLogic;


namespace ToasoanTTXVN.QlyAnh
{
    [WebService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : WebService
    {
       
        public AutoComplete()
        {
        }

        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {

            if (count == 0)
            {
                count = 10;
            }
            DataTable dt = GetRecords(prefixText);
            List<string> items = new List<string>(count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strName = dt.Rows[i][3].ToString();// + "( " + dt.Rows[i][1].ToString() + " )"; ;
                items.Add(strName);
            }
            return items.ToArray();
        }

        public DataTable GetRecords(string strName)
        {
           
            SSOLib.SSOLibDAL ssoDal = new SSOLib.SSOLibDAL();
            DataTable dt = ssoDal.GetListUserByWhere(" IsDeleted=0  AND UserFullName LIKE N'%" + strName + "%' Order by UserFullName").Tables[0];
            return dt;

        }
    }
    
}
