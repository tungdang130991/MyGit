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
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ToasoanTTXVN.TimKiem
{
    public partial class TimkiemMaketPDF : BasePage
    {
        
        UltilFunc ulti = new UltilFunc();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        
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
                    }
                    else
                    {
                        string EventName = Request.Form["__EVENTTARGET"].ToString();
                        if (EventName == "UploadImageSuccess")
                        {
                            LoadData_FilePDF();
                        }
                    }
                }
            }
        }

        #region Method
        
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboPage.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChontrang"), "0", true));
                for (int j = 1; j < _sotrang + 1; j++)
                {
                    cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblTrang") + j.ToString(), j.ToString()));
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " 1=1 ", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));

        }
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        public string GetTrangBao()
        {
            if (cboPage.SelectedIndex != 0)
                return cboPage.SelectedValue;
            else
                return "0";
        }
        public string GetSoBao()
        {
            if (cboSoBao.SelectedIndex != 0)
                return cboSoBao.SelectedValue;
            else
                return "0";
        }

        #endregion

        #region Event Click
        protected void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null)
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Do yout want to delete?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        protected void cboSoBao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex > 0)
                LoadData_FilePDF();
        }

        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonanpham"));
                return;
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                LoadData_FilePDF();
            }
            else
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonsobao"));
                return;
            }
        }

        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonanpham"));
                return;
            }
            if (cboSoBao.SelectedIndex == 0 && txt_tungay.Text.Trim() == "" && txt_denngay.Text.Trim() == "")
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonsobao"));
                return;
            }
            LoadData_FilePDF();
        }

        protected void cbo_Anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSoBao.Items.Clear();

            if (cbo_Anpham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);

            }
            else
            {
                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }
            bintrang(int.Parse(cbo_Anpham.SelectedValue));
        }

        #endregion

        #region Attach File PDF
        public void LoadData_FilePDF()
        {
            string _where = "Status=3";
            if (cboPage.SelectedIndex > 0)
                _where += " and Page_Number=" + cboPage.SelectedValue;
            if (cboSoBao.SelectedIndex > 0)
                _where += " and Publish_Number_ID=" + cboSoBao.SelectedValue;
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Publish_Number_ID in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            string _sql = "select * from T_Publish_Pdf where " + _where;
            DataSet _ds = Daltinbai.Sp_SelectT_Publish_PdfDynamic(_where, " ID ASC");

            this.DataGrid_FilePDF.DataSource = _ds;
            this.DataGrid_FilePDF.DataBind();
        }

        #endregion
    }
}
