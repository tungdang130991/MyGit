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
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN
{
    public partial class AnPhamBao : System.Web.UI.Page
    {
        protected T_Users _user;
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            Bindata();
        }
        protected void DataListAnpham_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["MaAnPham"] = e.CommandArgument.ToString();
            if (_user.UserName == Global.MaXuatBan.ToLower())
                Response.Redirect(Global.ApplicationPath + "/Xuatbandantrang.aspx");
            else
                Response.Redirect(Global.ApplicationPath + "/Dungchung/Commons.aspx");

        }
        private void Bindata()
        {
            HPCBusinessLogic.UltilFunc ulti = new UltilFunc();
            string _sql = "SELECT  ID,Ten_QuyTrinh,Url_Img FROM T_Ten_QuyTrinh Where 1=1 and Active=1";
            DataSet ds = ulti.ExecSqlDataSet(_sql);
            DataView dv = ds.Tables[0].DefaultView;
            DataListAnpham.DataSource = dv;
            DataListAnpham.DataBind();
        }
    }
}
