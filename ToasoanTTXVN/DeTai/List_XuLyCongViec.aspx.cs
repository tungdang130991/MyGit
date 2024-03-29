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
using HPCBusinessLogic.DAL;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.DeTai
{
    public partial class List_XuLyCongViec : System.Web.UI.Page
    {
        #region Variable Member

        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Lbt_Send_Duyet.Attributes.Add("onclick", "return CheckConfirmGuiDuyet();");

            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    pages.PageIndex = 0;
                    if (!IsPostBack)
                    {
                        int tab_id = 0;
                        if (Request["Tab"] != null)
                        {
                            tab_id = Convert.ToInt32(Request["Tab"].ToString());

                        }
                        if (tab_id == -1)
                        {
                            this.TabContainer1.ActiveTabIndex = 0;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        else
                        {
                            this.TabContainer1.ActiveTabIndex = tab_id;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        LoadCombox();
                    }
                }
            }
        }
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/QuanLyDeTai/Edit_Idiea.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + -1);
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DelRecordsCheckedBox();
        }
        protected void Send_Duyet(object sender, EventArgs e)
        {
            Gui_Duyet();
        }
        private void Gui_Duyet()
        {
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();

            for (int i = 0; i < ar.Count; i++)
            {
                T_Idiea _obj = new T_Idiea();
                T_IdieaDAL _objDAL = new T_IdieaDAL();

                double _ID = double.Parse(ar[i].ToString());

                if (_objDAL.BinT_Idiea(_ID, 32) == true || _objDAL.BinT_Idiea(_ID, 33) == true)
                {
                    FuncAlert.AlertJS(this, "Bạn vẫn còn bài chưa gửi đi trong đề tài này!");
                    return;
                }
                T_AllotmentDAL DAL = new T_AllotmentDAL();
                DAL.Update_Status_tintuc(_ID, 33, _user.UserID, DateTime.Now);


                ActionsCode = "[Danh sách công việc đang xử lý:]-->[hoàn thành(CV)][ID: " + _ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
            }
            SetTotal();
        }
        public void SetTotal()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            T_AllotmentDAL _T_IdieaDAL = new T_AllotmentDAL();
            DataSet _dsReturn;
            DataSet _dsReturn1;
            _dsReturn = _T_IdieaDAL.BindGridT_AllotmentEditor(pages.PageIndex, pages.PageSize, BuildSQL(32, sOrder));
            _dsReturn1 = _T_IdieaDAL.BindGridT_AllotmentEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + _dsReturn1.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            _dsReturn1.Clear();
        }
        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData_DangXuly();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                pages.PageIndex = 0;
                this.LoadData_Bitralai();
            }

        }
        private void DelRecordsCheckedBox()
        {

            T_AllotmentDAL _T_IdieaDAL = new T_AllotmentDAL();
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_select != null && chk_select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_DangXuly();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();

            for (int i = 0; i < ar.Count; i++)
            {
                double Diea_ID = double.Parse(ar[i].ToString());

                _T_IdieaDAL.Update_Status_tintuc(Diea_ID, 55, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý PV:]-->[Xóa Tin][Diea_ID:" + Diea_ID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
            }
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();

            }
            SetTotal();
        }

        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;
                HPCBusinessLogic.DAL.T_AllotmentDAL Dal = new HPCBusinessLogic.DAL.T_AllotmentDAL();
                string _ID = dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString();
                Response.Redirect("List_VietBaiPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + _ID.ToString() + "&tab=" + tab.ToString());
            }

        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;
                HPCBusinessLogic.DAL.T_AllotmentDAL Dal = new HPCBusinessLogic.DAL.T_AllotmentDAL();
                string _ID = dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();
                Response.Redirect("List_VietBaiPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + _ID.ToString() + "&tab=" + tab.ToString());
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }

        }
        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData_DangXuly();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_Bitralai();
            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                //this.LoadData_Baidaxuly();
            }
        }
        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            LoadData_DangXuly();
        }
        public void pages_IndexChanged_baidaxuly(object sender, EventArgs e)
        {
            //LoadData_Baidaxuly();
        }
        public void pages_IndexChanged_baitralai(object sender, EventArgs e)
        {
            LoadData_Bitralai();
        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();

            pages.PageSize = Global.MembersPerPage;
            T_AllotmentDAL _DAL = new T_AllotmentDAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_AllotmentEditor(pages.PageIndex, pages.PageSize, BuildSQL(32, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_AllotmentEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(32, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;

            #region DETAI DANG XU LY
            tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _DAL.BindGridT_AllotmentEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(33, sOrder));
            this.TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + TotalRecords + "," + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
            _dsReturn.Clear();
            #endregion
        }
        private void LoadData_Bitralai()
        {
            string sOrder = GetOrderString1() == "" ? "" : " ORDER BY " + GetOrderString1();

            pages.PageSize = Global.MembersPerPage;
            T_AllotmentDAL _DAL = new T_AllotmentDAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_AllotmentEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_AllotmentEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(33, sOrder));
            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            #region DETAI DANG XU LY
            TabPanel1.HeaderText = "Đề tài đã hoàn thành (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _DAL.BindGridT_AllotmentEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(32, sOrder));
            this.tabpnltinXuly.HeaderText = "Đề tài đang xử lý (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetTotal(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + "," + TotalRecords + ");", true);
            _dsReturn.Clear();
            #endregion
        }
        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)

                    return " Date_Created DESC";
                else
                    return " ID DESC";
            }
        }
        private string GetOrderString1()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)

                    return " Date_Duyet DESC";
                else
                    return " ID DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string sql = "";
            string sClause = " 1=1 AND User_NguoiNhan =" + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + " AND Status = " + status + " and CAT_ID in ( SELECT tc.Ma_Chuyenmuc FROM T_Chuyenmuc tc )AND Lang_ID IN (SELECT Ma_NgonNgu FROM T_Nguoidung_Ngonngu WHERE Ma_Nguoidung = " + _user.UserID + ")";
            string sWhere = "";

            if (txt_tieude.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Title LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  CAT_ID=" + cbo_chuyenmuc.SelectedValue.ToString();
            }

            sql += sClause;
            if (sWhere.Trim().Length > 0)
                sql += " AND" + sWhere;

            return sql + sOrder;
        }

        private void LoadCombox()
        {

            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " AND ID IN ({0}) Order by TenNgonNgu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");

            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 and Ma_Anpham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        
        protected string HaveBaitralaiOfDetai(string prmNewsEditorID)
        {
            string _have = "";
            int _prmEditorID = Convert.ToInt32(prmNewsEditorID);
            if (UltilFunc.GetBaitralai_Display(_prmEditorID) != "0")
                _have = "<b> &nbsp;&nbsp;[ <font color='red'> Đề tài có bài bị trả lại </font> ]</b>";
            return _have;
        }
    }
}
