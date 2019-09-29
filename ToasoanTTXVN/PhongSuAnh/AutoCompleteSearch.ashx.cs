using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using HPCBusinessLogic;
using HPCBusinessLogic.DAL;
using System.Text;
using HPCComponents;
using System.IO;
using HPCInfo;

namespace ToasoanTTXVN.PhongSuAnh
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AutoCompleteSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
            SSOLib.ServiceAgent.T_Users _user = null;
            string prefixText = context.Request.QueryString["q"];
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            string userid = _user.UserID.ToString();// HPCSecurity.CurrentUser.Identity.ID.ToString();
            StringBuilder sb = new StringBuilder();
            T_ButdanhDAL _DAL = new T_ButdanhDAL();
            DataSet _ds;
            _ds = _DAL.Bin_T_ButdanhDynamic(userid, prefixText, false);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                for (int _i = 0; _i < _ds.Tables[0].Rows.Count; _i++)
                {
                    sb.Append(string.Format("{0}|{1}", _ds.Tables[0].Rows[_i]["BD_Name"].ToString(), _ds.Tables[0].Rows[_i]["BD_ID"].ToString())).Append(Environment.NewLine);
                }
            }
            else
            {
                sb.Append(string.Format("{0}|{1}", prefixText, "0")).Append(Environment.NewLine);
            }
            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
