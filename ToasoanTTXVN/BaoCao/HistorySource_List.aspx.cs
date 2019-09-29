using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using System.Data;
using HPCComponents;

namespace ToasoanTTXVN.BaoCao
{
    public partial class HistorySource_List : BasePage
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadCombo();
                        LoadData();
                    }
                }
            }
        }
        public void gdListActionHistorys_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        private void LoadCombo()
        {
            //UltilFunc.BindCombox(ddlTenTruyCap, "UserID", "UserFullName", "T_U", " 1=1 Order by Ten_Dangnhap ASC", CommonLib.ReadXML("lblTatca"));
            ddlTraCuu.Items.Insert(0, new ListItem(CommonLib.ReadXML("lblTatca"), "0"));
            ddlTraCuu.Items.Insert(1, new ListItem("Tra cứu tin nguồn", "1"));
            ddlTraCuu.Items.Insert(2, new ListItem("Tra cứu tin tư liệu", "2"));
            ddlTraCuu.Items.Insert(3, new ListItem("Tra cứu ảnh", "3"));
            ddlTraCuu.SelectedIndex = 0;
            string _where = string.Empty;
            _where = " IsDeleted = 0 and UserActive=1 ";
            DataTable _dt = _userDAL.GetT_User_Dynamic(_where).Tables[0];

           ddlTenTruyCap.DataSource = _dt;
           ddlTenTruyCap.DataTextField = "UserFullName";
           ddlTenTruyCap.DataValueField = "UserID";
           ddlTenTruyCap.DataBind();
           ddlTenTruyCap.Items.Insert(0, "---All---");
        }

        //private void ListItem(string text, string value)
        //{
        //    throw new NotImplementedException();
        //}
        protected void cmdSeek_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string where = "1=1 ";
            if (ddlTenTruyCap.SelectedIndex > 0)
                where += " And User_ID = " + ddlTenTruyCap.SelectedValue + " ";
            if (ddlTraCuu.SelectedIndex > 0)
                where += " And Type = " + ddlTraCuu.SelectedValue + " ";
            if (txt_FromDate.Text != "" && txt_ToDate.Text != "")
                where += " and DateCreated>='" + txt_FromDate.Text + " 00:00:59' and DateCreated<='" + txt_ToDate.Text + " 23:59:59'";
            where += " Order by DateCreated DESC";
            pages.PageSize = Global.MembersPerPage;
            UltilFunc _DAL = new UltilFunc();
            DataSet _ds = _DAL.BindGridT_HistorySource(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_HistorySource(pages.PageIndex - 1, pages.PageSize, where);
            gdListActionHistorys.DataSource = _ds;
            gdListActionHistorys.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
        }
    }
}
