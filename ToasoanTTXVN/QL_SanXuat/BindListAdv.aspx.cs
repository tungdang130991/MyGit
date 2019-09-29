using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;
using HPCServerDataAccess;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class BindListAdv : System.Web.UI.Page
    {
        private int mabao = 0;
        private string  trang = string.Empty;
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
                        trang = Request["trangbao"].ToString();
                    if (Request["maAnpham"] != null)
                        anpham = Convert.ToInt32(Request["maAnpham"].ToString());
                    GetListAdvertis(anpham, mabao, trang);
                }
                catch (Exception ex) { throw ex; }
            }
        }
        protected void GetListAdvertis(int anpham, int sobao,string _trang)
        {
            StringBuilder sbs = new StringBuilder();
            VitriTinbaiDAL _objDAL = new VitriTinbaiDAL();
            try
            {
                DataSet _ds = _objDAL.T_QuangCao_GetList(anpham, sobao, _trang);
                DataTable tb1 = _ds.Tables[0];
                if (tb1.Rows.Count > 0)
                {
                    for (int i = 0; i < tb1.Rows.Count; i++)
                    {
                        DataRow mrow = tb1.Rows[i];
                        sbs.Append("<div class=\"listTinbaiItem\">");
                        sbs.Append("<input type=\"checkbox\" name=\"chkQC" + i + "\" class=\"chkItemsQC\" id=\"chkQC" + i + "\" value=\"" + mrow["Ma_Quangcao"] + "\" />");
                        sbs.Append("<label for=\"chkCoDienThoai\" class=\"lblTitleTinbai\">" + mrow["Ten_QuangCao"] + "");
                        sbs.Append("</label></div>");
                    }
                }
                ltrListAdv.Text = sbs.ToString();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
