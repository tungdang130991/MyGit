using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Web.Services;
namespace ToasoanTTXVN.Quytrinh
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SearchTacGia : IHttpHandler
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        UltilFunc ulti = new UltilFunc();
        public void ProcessRequest(HttpContext context)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            string prefixText = context.Request.QueryString["q"];

            string userid = HPCSecurity.CurrentUser.Identity.ID.ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string sql = "select top 20 Ma_Nguoidung, LTRIM(RTRIM(Ten_Dangnhap))+case when Loai=1 then ' (CTV)' when Loai=0 then '' end as Ten_Dangnhap from T_Nguoidung where  (Trangthai_Xoa=0 or Trangthai_Xoa is null) and Ten_Dangnhap LIKE N'%" + prefixText + "%' and Ten_Dangnhap is not null ";
            DataTable dt=new DataTable();
            dt = ulti.ExecSqlDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append(string.Format("{0}|{1}", dt.Rows[i]["Ten_Dangnhap"].ToString(), dt.Rows[i]["Ma_Nguoidung"].ToString())).Append(Environment.NewLine);
                }
                context.Response.Write(sb.ToString());
            }
            else
            {
                
                sb.Append(string.Format("{0}|{1}", prefixText, "0")).Append(Environment.NewLine);
                context.Response.Write("Không có tác giả này!");
            }
            
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
