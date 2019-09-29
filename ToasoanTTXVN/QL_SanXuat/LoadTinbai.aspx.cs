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
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;
using HPCServerDataAccess;

namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class LoadTinbai : System.Web.UI.Page
    {
        private int mabao = 0;
        private int trang = 0;
        private int anpham = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request["mabao"] != null)
                        mabao = Convert.ToInt32(Request["mabao"].ToString());
                    if (Request["trangbao"] != null)
                        trang =Convert.ToInt32(Request["trangbao"].ToString());
                    if (Request["maAnpham"] != null)
                        anpham = Convert.ToInt32(Request["maAnpham"].ToString());
                    GetSelectedNews(anpham, mabao, trang);
                }
                catch (Exception ex) { throw ex; }
            }

        }
        protected void GetSelectedNews(int anpham, int sobao, int page)
        {
            StringBuilder sbs = new StringBuilder();
            VitriTinbaiDAL _objDAL = new VitriTinbaiDAL();
            try
            {
                DataSet _ds = _objDAL.T_Tinbai_GetByAnpham_And_Sobao(anpham, sobao, page,Global.MaXuatBan);
                DataTable tb1 = _ds.Tables[0];
                if (tb1.Rows.Count > 0)
                {
                    for (int i = 0; i < tb1.Rows.Count; i++)
                    {
                        DataRow mrow = tb1.Rows[i];
                        sbs.Append("<div class=\"listTinbaiItem\">");
                        sbs.Append("<input type=\"checkbox\" name=\"chk" + i + "\" class=\"chkItems\" id=\"chk" + i + "\" value=\"" + mrow["Ma_Tinbai"] + "\" />");
                        sbs.Append("<label for=\"chkCoDienThoai\" class=\"lblTitleTinbai\">" + mrow["Tieude"] + "");
                        sbs.Append("</label></div>");
                    }
                }
                ltrChonbaiviet.Text = sbs.ToString();
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
