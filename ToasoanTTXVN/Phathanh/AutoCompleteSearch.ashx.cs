using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using HPCBusinessLogic;
using System.Text;
namespace ToasoanTTXVN.Phathanh
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
            string prefixText = context.Request.QueryString["q"];
           
            StringBuilder sb = new StringBuilder();
            KhachhangDAL _DAL = new KhachhangDAL();
            DataSet _ds;
            _ds = _DAL.Select_Loai_KhachHang(prefixText);
          //  sb.Append(string.Format("{0}|{1}", "Tên", "0")).Append(Environment.NewLine);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                for (int _i = 0; _i < _ds.Tables[0].Rows.Count; _i++)
                {
                    sb.Append(string.Format("{0}|{1}", _ds.Tables[0].Rows[_i]["Ten_KhachHang"].ToString(), _ds.Tables[0].Rows[_i]["Ma_KhachHang"].ToString())).Append(Environment.NewLine);//(_ds.Tables[0].Rows[_i]["Ten_KhachHang"].ToString()).Append(Environment.NewLine);
                }
            }
            else
            {               
                sb.Append(string.Format("{0}|{1}","No records to display", "0")).Append(Environment.NewLine);//(_ds.Tables[0].Rows[_i]["Ten_KhachHang"].ToString()).Append(Environment.NewLine);
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
