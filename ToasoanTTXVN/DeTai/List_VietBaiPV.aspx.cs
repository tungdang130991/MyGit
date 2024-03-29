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
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Office.Core;
using HPCBusinessLogic.DAL;
namespace ToasoanTTXVN.DeTai
{
    public partial class List_VietBaiPV : System.Web.UI.Page
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new HPCBusinessLogic.NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
        private int ChildID
        {
            get
            {
                if (ViewState["ChildID"] != null) return Convert.ToInt32(ViewState["ChildID"]);
                else return 0;
            }
            set { ViewState["ChildID"] = value; }
        }
        private int MaCM
        {
            get
            {
                if (ViewState["MaCM"] != null) return Convert.ToInt32(ViewState["MaCM"]);
                else return 0;
            }
            set { ViewState["MaCM"] = value; }
        }
        UltilFunc ulti = new UltilFunc();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);

                    //this.LinkButton6.Attributes.Add("onclick", "return CheckConfirmDelete();");
                    //this.LinkButton5.Attributes.Add("onclick", "return CheckConfirmGuiThuKyToaSoanDangXyLy();");
                    this.LinkButton3.Attributes.Add("onclick", "return CheckConfirmGuiThuKyToaSoanReturn();");
                    this.LinkButton4.Attributes.Add("onclick", "return CheckConfirmDeleteReturn();");
                    pages.PageIndex = 0;
                    if (!IsPostBack)
                    {

                        this.TabContainer1.ActiveTabIndex = 0;
                        this.TabContainer1_ActiveTabChanged(sender, e);

                        LoadCombox();
                        DataBind();

                    }
                }
            }
        }

        #region Methods
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboNgonNgu, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " and ID IN ({0}) Order by TenNgonNgu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Chọn ngôn ngữ---");

            cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
            if (cboNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 and Ma_Anpham=" + this.cboNgonNgu.SelectedValue.ToString() + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

        }
        public override void DataBind()
        {
            T_Idiea obj = new T_Idiea();
            T_Allotments _Allotment = new T_Allotments();
            T_IdieaDAL dal = new T_IdieaDAL();
            T_AllotmentDAL _AllotmentDAL = new T_AllotmentDAL();
            ChuyenmucDAL caDal = new ChuyenmucDAL();
            if (Request["DT_id"] != null && Request["DT_id"].ToString() != "" && Request["DT_id"].ToString() != String.Empty)
            {
                int id = int.Parse(Page.Request["DT_id"].ToString());

                _Allotment = _AllotmentDAL.GetOneFromT_AllotmentByID(id);
                obj = dal.GetOneFromT_IdieaByID(_Allotment.Idiea_ID);

                this.T_AllotmentNgayHT.Text = _Allotment.Date_End.ToString();
                this.ltrYeuCau.Text = _Allotment.Request.ToString();
                if (obj.Cat_ID > 0)
                {
                    this.lblNameCM.Text = caDal.GetOneFromT_ChuyenmucByID(int.Parse(obj.Cat_ID.ToString())).Ten_ChuyenMuc;
                }
                if (obj.Title.ToString().Length > 0)
                    this.lbtieude.Text = obj.Title.ToString();

                if (_Allotment.Type == 1)
                    ltr_loaibai1.Text = "Bài viết";
                else
                    ltr_loaibai1.Text = "Bài ảnh";

                MaCM = obj.Cat_ID;
            }



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

                    return " Ngaytao DESC";
                else
                    return " Ma_Tinbai DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {

            string sql = "";
            string sClause = " 1=1 AND Status = " + status + " AND CV_id=" + Page.Request["DT_id"].ToString() + " and CAT_ID in ( SELECT Ma_Chuyenmuc FROM T_Nguoidung_chuyenmuc WHERE Ma_Nguoidung = " + _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + ")AND Lang_ID IN (SELECT Ma_Ngonngu FROM T_Nguoidung_Ngonngu WHERE Ma_Nguoidung = " + _user.UserID + ")";
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
        string GetWhereTinBai(int status, string sOrder)
        {
            string _where = " Trangthai_Xoa=0 and Ma_tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Congviec=" + Page.Request["DT_id"].ToString() + ")";

            if (txt_tieude.Text.Length > 0)

                _where += " and Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));

            if (cbo_chuyenmuc.SelectedIndex > 0)
                _where += " and Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();

            if (sOrder.Length > 0)
                return _where + sOrder;
            else
                return _where;
        }
        private void LoadData_TinBaiTheoDeTai()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, GetWhereTinBai(2, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, GetWhereTinBai(2, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();


            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
            GetTotalRecordTinBai();
        }
        public void GetTotalRecordTinBai()
        {
            string _tindangxuly = "0";
            _tindangxuly = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", GetWhereTinBai(2, ""));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _tindangxuly + ");", true);
            //this.TabPanel1.HeaderText = "Tin biên tập (" + _tindangxuly + ")";
        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY Date_Duyet";

            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(32, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(32, sOrder));
            dgr_tintuc1.DataSource = _ds;
            dgr_tintuc1.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;

            #region DETAI BI TRA LAI
            tabpnltinXuly.HeaderText = "Bài đang xử lý (" + TotalRecords + ")";
            DataSet _dsReturn;
            _dsReturn = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
            this.TabPanel1.HeaderText = "Bài bị trả lại (" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ")";
            _dsReturn.Clear();
            #endregion

            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
        }
        private void LoadData_Bitralai()
        {
            string sOrder = " ORDER BY Date_Duyet DESC";

            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_IdieaDAL _T_IdieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            DataSet _ds;
            _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_IdieaDAL.BindGridT_IdieaEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(33, sOrder));
            dgr_tintuc2.DataSource = _ds;
            dgr_tintuc2.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            #region DETAI BI TRA LAI
            TabPanel1.HeaderText = "Bài bị trả lại (" + TotalRecords + ")";
            GetTotalRecordTinBai();
            #endregion

        }
        protected string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/images/document_new.png";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/images/lock.jpg";
            return strReturn;
        }
        protected string LockedUser(string prm_news_Lock, string prmNewsEditorID)
        {
            string _userLock = "";
            int _prmEditorID = Convert.ToInt32(prmNewsEditorID);

            if (prm_news_Lock == "True" && _prmEditorID != _user.UserID)
                _userLock = "<b> &nbsp;&nbsp;[ <font color='red'> User locked: " + UltilFunc.GetUserName(prmNewsEditorID) + "</font> ]</b>";

            return _userLock;
        }
        protected bool IsEnable(string prmNews_Lock, string prmEditorID)
        {
            bool _isEnabled = true;
            int _prmEditorID = Convert.ToInt32(prmEditorID);
            if (prmNews_Lock == "True" && _prmEditorID != _user.UserID)
                _isEnabled = false;
            else
                _isEnabled = true;
            return _isEnabled;
        }
        private void DelRecordsCheckedBox()
        {

            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            HPCBusinessLogic.DAL.T_IdieaDAL tt = new HPCBusinessLogic.DAL.T_IdieaDAL();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_TinBaiTheoDeTai();
            else if (TabContainer1.ActiveTabIndex == 1)
                LoadData_Bitralai();

            for (int i = 0; i < ar.Count; i++)
            {
                double _ID = double.Parse(ar[i].ToString());
                tt.Update_Status_tintuc(_ID, 55, _user.UserID, DateTime.Now, 0);
                this.TabContainer1_ActiveTabChanged(null, null);

                ActionsCode = "[Danh sách đề tài đang chờ xử lý PV:]-->[Xoá tin][T_Idiea_ID=" + double.Parse(_ID.ToString()) + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }

            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_TinBaiTheoDeTai();
                //DataSet _dsReturn;
                //_dsReturn = tt.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(32, sOrder));
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                //_dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
                DataSet _dsReturn;
                _dsReturn = tt.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiTraLai(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
        }
        private void Send_TKTS()
        {

            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            HPCBusinessLogic.DAL.T_IdieaDAL _ideal = new HPCBusinessLogic.DAL.T_IdieaDAL();
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in dgr_tintuc1.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                        tt.IsLock(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);

                        ar.Add(double.Parse(dgr_tintuc1.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in dgr_tintuc2.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
                        tt.IsLock(double.Parse(dgr_tintuc2.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()), 0);

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
                double _ID = double.Parse(ar[i].ToString());
                _ideal.Update_Status_tintuc(_ID, 23, _user.UserID, DateTime.Now, 0);
                _ideal.Insert_Version_From_T_idiea_WithUserModify(_ID, 3, 23, _user.UserID, DateTime.Now);
                this.TabContainer1_ActiveTabChanged(null, null);

                ActionsCode = "[Danh sách tin bài đang chờ xử lý PV:]-->[Gửi TPPV][TinTuc_ID:" + double.Parse(_ID.ToString()) + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
            }

            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadData_DangXuly();
                DataSet _dsReturn;
                _dsReturn = _ideal.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(32, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                LoadData_Bitralai();
                DataSet _dsReturn;
                _dsReturn = _ideal.BindGridT_IdieaEditor(pages.PageIndex, pages.PageSize, BuildSQL(33, sOrder));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiTraLai(" + _dsReturn.Tables[1].Rows[0].ItemArray[0].ToString() + ");", true);
                _dsReturn.Clear();
            }
        }
        #endregion

        #region Event click
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("List_XuLyCongViec.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.LoadData_TinBaiTheoDeTai();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                pages.PageIndex = 0;
                this.LoadData_Bitralai();
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData_TinBaiTheoDeTai();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_Bitralai();
            }

        }
        public void pages_IndexChanged_baidangxuly(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData_TinBaiTheoDeTai();
            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_Bitralai();
            }

        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Quytrinh/Edit_PV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&DT_id=" + Request["DT_id"].ToString() + "&MaDoiTuong=PV&Tab=" + -1 + "&MaCM=" + MaCM);
        }
        protected void Send_TKTS(object sender, EventArgs e)
        {

            Send_TKTS();

        }
        protected void Delete_Click(object sender, EventArgs e)
        {

            DelRecordsCheckedBox();

        }
        
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString();

                Response.Redirect("Edit_XuLyCongViec.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + Request["DT_id"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }

        }
        protected void dgData_EditCommand1(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = TabContainer1.ActiveTabIndex;

                HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
                string _ID = dgr_tintuc2.DataKeys[e.Item.ItemIndex].ToString();
                string _catID = cbo_chuyenmuc.SelectedValue.ToString();

                Response.Redirect("Edit_XuLyCongViec.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + Request["DT_id"].ToString() + "&ID=" + _ID.ToString() + "&Tab=" + tab);
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgData_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null) btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void Dgr_Baidaxuly_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion

    }
}
