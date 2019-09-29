using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.TimKiem
{
    public partial class ViewBaiNhap : System.Web.UI.Page
    {
        T_AutoSave_DAL _Dal = new T_AutoSave_DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                if (!IsPostBack)
                {

                    T_AutoSave obj = new T_AutoSave();
                    obj = _Dal.Sp_SelectOneFromT_AutoSave(id);
                    CKE_Noidung.Text = obj.Noidung;

                }
            }

        }
    }
}
