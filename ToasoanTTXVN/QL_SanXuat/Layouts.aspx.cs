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
    public partial class Layouts : System.Web.UI.Page
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
                        BindCombo();
                    }
                }
            }
            this.Trang_Bao.Value = "";
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
                GetSelectedNews(anpham, sobao);
                BindListPageBySobao(sobao);
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
                Lib.RunJavaScriptCode("setValueSobao('" + _masobao + "');");
                StringBuilder sb = new StringBuilder();
                VitriTinbaiDAL _objDAL = new VitriTinbaiDAL();
                DataSet _ds;
                if (_masobao > 0)
                {
                    _ds = _objDAL.T_Sobao_GetByID(_masobao);
                    DataTable tb = _ds.Tables[0];
                    if (tb.Rows.Count > 0)
                    {
                        DataRow mrow = tb.Rows[0];
                        int sotrang = Convert.ToInt32(mrow["Sotrang"].ToString());
                        sb.Append(" <div class=\"menuLeftLayoutTitle\"><span>" + mrow["Ten_Sobao"].ToString() + "(" + DateTime.Parse(mrow["Ngay_Xuatban"].ToString()).ToString("dd/MM/yyyy") + ")</span></div>");
                        sb.Append("<ul>");
                        for (int i = 1; i <= sotrang; i++)
                        {
                            sb.Append("<li onclick=\"SelectItem('" + i + "');\"><div class=\"ItemLayout\"></div>");
                            sb.Append("<span>(Trang " + i.ToString() + ")</span>");
                            sb.Append("</li>");
                        }
                        sb.Append("</ul>");
                    }
                }
                ltrListLayout.Text = sb.ToString();
            }
            catch (Exception ex) { throw ex; }
        }
        protected void GetSelectedNews(int anpham, int sobao)
        {
            StringBuilder sbs = new StringBuilder();
            VitriTinbaiDAL _objDAL = new VitriTinbaiDAL();
            try
            {
                DataSet _ds = _objDAL.T_Tinbai_GetByAnpham_And_Sobao(anpham, sobao);
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
        //Phần Thêm mới công việc
        #region Method
        private void BindCombo()
        {
            DataTable _dt = new DataTable();
            _dt = _NguoidungDAL.BindGridT_Users(0, 1000, " 1=1 ").Tables[0];
            ddl_NguoiNhan.Items.Clear();
            ddl_NguoiNhan.DataSource = _dt;
            ddl_NguoiNhan.DataBind();
            ddl_NguoiNhan.Items.Insert(0, "------ Chọn ------");

        }
        #endregion

    }
}
