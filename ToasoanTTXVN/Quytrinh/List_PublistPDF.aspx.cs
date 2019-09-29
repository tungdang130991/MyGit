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

namespace ToasoanTTXVN.Quytrinh
{
    public partial class List_PublistPDF : BasePage
    {
        private bool _refreshState;
        private bool _isRefresh;
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] AllStates = (object[])savedState;
                base.LoadViewState(AllStates[0]);
                _refreshState = bool.Parse(AllStates[1].ToString());
                _isRefresh = _refreshState ==
                bool.Parse(Session["__ISREFRESH"].ToString());
            }
            catch
            { }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));


                    if (!IsPostBack)
                    {
                        if (_user != null)
                        {
                            LoadCombox();

                            cbo_Sobao.Items.Clear();
                            if (cbo_Anpham.SelectedIndex > 0)
                            {
                                UltilFunc.BindComboxSoBao(cbo_Sobao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);
                                bintrang(int.Parse(cbo_Anpham.SelectedValue.ToString()));
                            }
                            else
                            {
                                cbo_Sobao.DataSource = null;
                                cbo_Sobao.DataBind();

                            }
                        }
                        else
                            Page.Response.Redirect("~/login.aspx", true);

                    }

                }
            }
        }
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
            UltilFunc.BindCombox(cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
        }
        public void LoadData_FilePDF()
        {
            string _where = "Status=3";
            if (cboPage.SelectedIndex > 0)
                _where += " and Page_Number=" + cboPage.SelectedValue;
            if (cbo_Sobao.SelectedIndex > 0)
                _where += " and Publish_Number_ID=" + cbo_Sobao.SelectedValue;
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Publish_Number_ID in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            string _sql = "select * from T_Publish_Pdf where " + _where;
            DataSet _ds = Daltinbai.Sp_SelectT_Publish_PdfDynamic(_where, " ID DESC");
            this.DataGrid_FilePDF.DataSource = _ds;
            this.DataGrid_FilePDF.DataBind();
        }

        protected void cbo_Anpham_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbo_Sobao.Items.Clear();
            if (cbo_Anpham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cbo_Sobao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);

            }
            else
            {
                cbo_Sobao.DataSource = null;
                cbo_Sobao.DataBind();

            }
            bintrang(int.Parse(cbo_Anpham.SelectedValue.ToString()));
        }
        protected void cboSoBao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Sobao.SelectedIndex > 0)
                LoadData_FilePDF();
        }
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonanpham"));
                return;
            }
            if (cbo_Sobao.SelectedIndex > 0)
            {
                LoadData_FilePDF();
            }
            else
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblBanchuachonsobao"));
                return;
            }
        }

        #region Event File PDF
        public void dgrListPDF_EditCommand(object source, DataListCommandEventArgs e)
        {
            int _ID = Convert.ToInt32(DataGrid_FilePDF.DataKeys[e.Item.ItemIndex].ToString());
            Label lblUrl = (Label)e.Item.FindControl("lbFileAttach");
            if (e.CommandArgument.ToString().ToLower() == "downloadfile")
            {
                string filePath = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lblUrl.Text);
                if (File.Exists(filePath))
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(lblUrl.Text));
                    Response.WriteFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    FuncAlert.AlertJS(this, "File không có trong Server");
                    return;
                }
            }

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbo_Sobao.SelectedIndex > 0)
                LoadData_FilePDF();
            else
                FuncAlert.AlertJS(this, "Bạn chưa chọn số báo");
        }        
        protected void btnbackpage_Click(object sender, EventArgs e)
        {
            
            if (!_isRefresh)
            {
                if (DataGrid_FilePDF.Items.Count > 0)
                {
                    foreach (DataListItem m_Item in DataGrid_FilePDF.Items)
                    {

                        CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                        TextBox txt_chuthich = (TextBox)m_Item.FindControl("txt_chuthich");
                        if (chk_Select != null && chk_Select.Checked)
                        {
                            int _ID = int.Parse(DataGrid_FilePDF.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                            string _sqlupdate = "update T_Publish_Pdf set Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
                            ulti.ExecSql(_sqlupdate);
                        }
                    }
                    LoadData_FilePDF();
                }
                else
                {
                    FuncAlert.AlertJS(this, "Không có layout!");
                    return;
                }
            }
        }
        #endregion

    }
}
