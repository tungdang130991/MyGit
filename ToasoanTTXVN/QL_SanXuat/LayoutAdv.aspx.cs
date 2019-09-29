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
using System.IO.Compression;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class LayoutAdv : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));

                    if (!IsPostBack)
                    {
                        LoadCombox();
                        DisableClientCaching();
                    }
                }
            }
            CreateGrid();
            this.Trang_Bao.Value = "";
        }
        private void CreateGrid()
        {
            StringBuilder bd = new StringBuilder();
            for (int k = 0; k < 45; k++)
            {
                bd.Append("<div style=\"width: 100%; height: " + ConfigurationManager.AppSettings["height"] + "; float: left; border-bottom: solid 1px #BBBBBB\">");
                for (int j = 0; j < 33; j++)
                {
                    bd.Append("<div style=\"width: " + ConfigurationManager.AppSettings["width"] + "; height: " + ConfigurationManager.AppSettings["height"] + "; border-right: solid 1px #BBBBBB; float: left\"></div>");
                }
                bd.Append("</div>");
            }
            creategrid.InnerHtml = bd.ToString();
        }
        private void DisableClientCaching()
        {
            // Do any of these result in META tags e.g. <META HTTP-EQUIV="Expire" CONTENT="-1">
            // HTTP Headers or both?

            // Does this only work for IE?
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Is this required for FireFox? Would be good to do this without magic strings.
            // Won't it overwrite the previous setting
            Response.Headers.Add("Cache-Control", "no-cache, no-store");

            // Why is it necessary to explicitly call SetExpires. Presume it is still better than calling
            // Response.Headers.Add( directly
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        }
        public void LoadCombox()
        {
            cboAnPham.Items.Clear();
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", "---Tất cả---");
            cboAnPham.SelectedValue = "0";
        }
        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSoBao.Items.Clear();
            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, "Ma_Sobao", "Ten_Sobao", "Ten_Sobao", "Ngay_Xuatban", "T_Sobao", "", cboAnPham.SelectedValue.ToString());
                cboSoBao.AutoPostBack = true;
            }
            else
            {
                cboSoBao.DataSource = null;
                cboSoBao.DataBind();
                cboSoBao.AutoPostBack = true;
            }
            BindListPageBySobao(0);
        }
        protected void cboSobao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex > 0)
            {
                int anpham = Convert.ToInt32(cboAnPham.SelectedValue.ToString());
                Session["LayMaAnPham"] = anpham.ToString();
                int sobao = Convert.ToInt32(cboSoBao.SelectedValue.ToString());
                BindListPageBySobao(sobao);
                GetURLMangXec(sobao);
            }
            else
            {
                BindListPageBySobao(0);
            }
        }
        protected void BindListPageBySobao(int _masobao)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                VitriTinbaiDAL _objDAL = new VitriTinbaiDAL();
                DataSet _ds;
                if (_masobao > 0)
                {
                    _ds = _objDAL.T_Layout_SoBao_GetPageBySobao(_masobao);
                    DataTable tb = _ds.Tables[0];
                    if (tb.Rows.Count > 0)
                    {

                        DataRow mrow = tb.Rows[0];
                        // int sotrang = Convert.ToInt32(mrow["Sotrang"].ToString());
                        sb.Append(" <div class=\"menuLeftLayoutTitle\"><span>" + mrow["Ten_Sobao"].ToString() + "(" + DateTime.Parse(mrow["Ngay_Xuatban"].ToString()).ToString("dd/MM/yyyy") + ")</span></div>");
                        sb.Append("<ul>");
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            DataRow mrow2 = tb.Rows[i];
                            sb.Append("<li onclick=\"SelectItem('" + mrow2["Trang"].ToString() + "');\"><div class=\"ItemLayout\"></div>");
                            sb.Append("<span>(Trang " + mrow2["Trang"].ToString() + ")</span>");
                            sb.Append("</li>");
                        }
                        sb.Append("</ul>");
                    }
                }
                ltrListLayout.Text = sb.ToString();
                Lib.RunJavaScriptCode("setValueSobao('" + _masobao + "');");
            }
            catch (Exception ex) { throw ex; }
        }
        public void GetURLMangXec(int sobao)
        {
            UltilFunc ulti = new UltilFunc();
            StringBuilder sb = new StringBuilder();
            if (sobao > 0)
            {
                string select = "select Logo_ID from T_Sobao where Ma_Sobao=" + sobao.ToString();
                DataTable _dt = ulti.ExecSqlDataSet(select).Tables[0];
                if (_dt.Rows[0][0].ToString() != "")
                {
                    string sql = "select Path_Logo from t_logo where Ma_logo=" + _dt.Rows[0][0].ToString();
                    DataTable dt = ulti.ExecSqlDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        sb.Append(" <div style=\"text-align: center\"><img src=\"" + Global.ApplicationPath + "" + dt.Rows[0][0].ToString() + "\" style=\"width: 1000px;height: 80px;\"align=\"middle\"  /></div>");
                        mangxec.InnerHtml = sb.ToString();
                    }
                    else
                    {

                        mangxec.InnerHtml = "";
                    }

                }
                else
                {

                    mangxec.InnerHtml = "";
                }
            }


        }
        protected string IpAddress()
        {
            string strIp;
            strIp = Page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = Page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            string viewState = Request.Form["__VSTATE"];
            byte[] bytes = Convert.FromBase64String(viewState);
            bytes = Compressors.Decompress(bytes);
            LosFormatter formatter = new LosFormatter();
            return formatter.Deserialize(Convert.ToBase64String(bytes));
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            LosFormatter formatter = new LosFormatter();
            StringWriter writer = new StringWriter();
            formatter.Serialize(writer, viewState);
            string viewStateString = writer.ToString();
            byte[] bytes = Convert.FromBase64String(viewStateString);
            bytes = Compressor.Compress(bytes);
            Page.ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(bytes));
        }
    }
    public class Compressors
    {
        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            GZipStream gzip = new GZipStream(output,
                              CompressionMode.Compress, true);
            gzip.Write(data, 0, data.Length);
            gzip.Close();
            return output.ToArray();
        }
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream();
            input.Write(data, 0, data.Length);
            input.Position = 0;
            GZipStream gzip = new GZipStream(input,
                              CompressionMode.Decompress, true);
            MemoryStream output = new MemoryStream();
            byte[] buff = new byte[64];
            int read = -1;
            read = gzip.Read(buff, 0, buff.Length);
            while (read > 0)
            {
                output.Write(buff, 0, read);
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            return output.ToArray();
        }
    }
}
