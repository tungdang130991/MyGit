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

namespace ToasoanTTXVN.Quangcao
{
    public partial class DanTrangQC : BasePage
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
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        ArrayList _arr = new ArrayList();
        HPCBusinessLogic.DAL.QuangCaoDAL _DalQC = new HPCBusinessLogic.DAL.QuangCaoDAL();
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

                    pages.PageIndex = 0;
                    if (!IsPostBack)
                    {
                        LoadCombox();
                        int tab_id = 0;
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());

                        }
                        this.TabContainer1.ActiveTabIndex = tab_id;
                        this.TabContainer1_ActiveTabChanged(sender, e);

                    }
                }
            }
        }

        #region Methods
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbokhachang, "Ma_KhachHang", "Ten_khachhang", "T_Khachhang", "1=1 and Loai_KH=1", "---Tất cả---");
            UltilFunc.BindCombox(cbo_loaibao, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", "---Chọn loại báo---");
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
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _Role.R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _Role.R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _Role.R_Read;
        }
        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {

                return " NgayTao DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sWhere = " Trangthai=" + status;
            if (txt_tenquangcao.Text.Length > 0)
                sWhere += " AND Ten_QuangCao LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tenquangcao.Text.Trim()));
            if (cbokhachang.SelectedIndex > 0)
                sWhere += " AND Ma_KhachHang=" + cbokhachang.SelectedValue.ToString();
            if (cbo_loaibao.SelectedIndex > 0)
                sWhere += " AND Ma_Loaibao=" + cbo_loaibao.SelectedValue.ToString();
            if (cbo_sobao.SelectedIndex > 0)
                sWhere += " AND Ma_Quangcao in (select Quangcao_ID from T_Publish_QuangCao where Ma_Sobao=" + cbo_sobao.SelectedValue.ToString() + ")";
            if (cbo_trang.SelectedIndex > 0)
                sWhere += " AND Trang=" + cbo_trang.SelectedValue.ToString();
            if (sOrder.Length > 0)
                return sWhere + sOrder;
            else
                return sWhere;
        }
        string BuildSQL_Phienban(string sOrder)
        {
            string sWhere = " Nguoitao=" + _user.UserID + "and Ngaytao>='" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'";
            if (txt_tenquangcao.Text.Length > 0)
                sWhere += " AND Ten_QuangCao LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tenquangcao.Text.Trim()));
            if (cbokhachang.SelectedIndex > 0)
                sWhere += " AND Ma_KhachHang=" + cbokhachang.SelectedValue.ToString();
            if (cbo_loaibao.SelectedIndex > 0)
                sWhere += " AND Ma_Loaibao=" + cbo_loaibao.SelectedValue.ToString();
            if (cbo_trang.SelectedIndex > 0)
                sWhere += " AND Trang=" + cbo_trang.SelectedValue.ToString();
            if (sOrder.Length > 0)
                return sWhere + sOrder;
            else
                return sWhere;
        }
        private void LoadData_QC_Dangxuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;

            DataSet _ds;
            _ds = _DalQC.BindGridT_Quangcao(pages.PageIndex, pages.PageSize, BuildSQL(6, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DalQC.BindGridT_Quangcao(pages.PageIndex - 1, pages.PageSize, BuildSQL(6, sOrder));
            DataGridQC_Choduyet.DataSource = _ds;
            DataGridQC_Choduyet.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
            GetTotalRecord();
        }
        private void LoadData_QC_Dagui()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;

            DataSet _ds;
            _ds = _DalQC.BindGridT_PhienBanQuangcao(pages.PageIndex, pages.PageSize, BuildSQL_Phienban(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DalQC.BindGridT_PhienBanQuangcao(pages.PageIndex - 1, pages.PageSize, BuildSQL_Phienban(sOrder));
            DataGridQC_Dagui.DataSource = _ds;
            DataGridQC_Dagui.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
            GetTotalRecord();
        }
        public void GetTotalRecord()
        {
            string _tindangxuly = "0", _tindaxuly = "0";
            _tindangxuly = ulti.GetColumnValuesTotal("T_QuangCao", "COUNT (Ma_Quangcao) as Total", BuildSQL(6, ""));
            _tindaxuly = ulti.GetColumnValuesTotal("T_PhienBanQuangCao", "COUNT (ID) as Total", BuildSQL_Phienban(""));
            this.TabPanelQC_Dangxuly.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblQCMoi") + " (" + _tindangxuly + ")";
            this.TabPanelQC_Daxuly.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblQCdagui") + " (" + _tindaxuly + ")";

        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cbo_trang.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cbo_trang.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChontrang"), "0", true));
                for (int j = 1; j < _sotrang + 1; j++)
                {
                    cbo_trang.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblTrang") + j.ToString(), j.ToString()));
                }
            }

        }
        private ArrayList GetDataKeysFromDataGrid()
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in DataGridQC_Choduyet.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _arr.Add(double.Parse(DataGridQC_Choduyet.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in DataGridQC_Dagui.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        _arr.Add(double.Parse(DataGridQC_Dagui.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            return _arr;
        }
        #endregion

        #region Event Click
        protected void cbo_loaibao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_loaibao.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cbo_sobao, int.Parse(cbo_loaibao.SelectedValue.ToString()), 0);
            }
            else
            {
                cbo_sobao.DataSource = null;
                cbo_sobao.DataBind();
            }
            bintrang(int.Parse(cbo_loaibao.SelectedValue.ToString()));
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pnlbutton.Visible = true;
                LoadData_QC_Dangxuly();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                pnlbutton.Visible = false;
                LoadData_QC_Dagui();
            }
        }

        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            if (!_isRefresh)
            {
                int Nguoitao = 0;
                string Thaotac = "";
                _arr = GetDataKeysFromDataGrid();
                for (int i = 0; i < _arr.Count; i++)
                {
                    double ID = double.Parse(_arr[i].ToString());
                    Nguoitao = UltilFunc.GetColumnValuesOne("T_PhienBanQuangCao", "Nguoitao", " Ma_Quangcao=" + ID.ToString() + " and Trangthai=21");
                    if (Nguoitao != 0)
                    {
                        _DalQC.Sp_UpdateRowT_QuangCao(ID, Nguoitao, 53);
                        _DalQC.Sp_InsertT_PhienBanQuangCao(ID);
                        Thaotac = "[Gửi trả lại quảng cáo từ DT-QC-->TLBP-QC:]-->[ID:]" + ID.ToString();
                        UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                    }
                    else
                    {
                        FuncAlert.AlertJS(this, "không thể trả lại vì QC không phải của NVQC");
                        return;
                    }
                }
                this.TabContainer1_ActiveTabChanged(null, null);
            }
        }

        public void pages_IndexChanged_Quangcao(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void dgr_quangcao_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        #endregion
    }
}
