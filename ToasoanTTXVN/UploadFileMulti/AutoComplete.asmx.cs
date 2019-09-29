using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.UploadFileMulti
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class AutoComplete : System.Web.Services.WebService
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = new T_Users();

        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            if (count == 0)
            {
                count = 20;
            }
            DataTable dt = GetRecords(prefixText, contextKey);
            List<string> itemsAuthor = new List<string>(count);
            string item = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse(contextKey) != 3)
                    item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["Ten_Dangnhap"].ToString(), dt.Rows[i]["Ma_Nguoidung"].ToString());
                else
                    item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["UserFullName"].ToString(), dt.Rows[i]["UserID"].ToString());
                itemsAuthor.Add(item);
            }
            return itemsAuthor.ToArray();
        }

        public DataTable GetRecords(string prefixText, string contextKey)
        {
            DataTable _dt = new DataTable();
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
            string _sql = string.Empty;
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            UltilFunc ulti = new UltilFunc();
            if (int.Parse(contextKey) != 3)
            {
                _sql = "select top 20 Ma_Nguoidung, LTRIM(RTRIM(TenDaydu))+case when Loai=1 then ' -- (CTV)' when Loai=0 then '' end as Ten_Dangnhap from T_Nguoidung where (Trangthai_Xoa=0 or Trangthai_Xoa is null) and TenDaydu LIKE N'%" + prefixText.Trim() + "%' and Ten_Dangnhap is not null ";
                _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            }
            else
            {
                string _where = " IsDeleted = 0 and UserFullName LIKE N'%" + prefixText.Trim() + "%'";
                _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];
            }
            return _dt;

        }
    }
}
