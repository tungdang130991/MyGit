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
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using System.Collections.Generic;

namespace ToasoanTTXVN.Quangcao
{
    public partial class WeblinksList : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
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
                        LoadData();
                        LoadComboBox();
                    }
                }
            }
        }
        private void LoadComboBox()
        {
            ddlLang.Items.Clear();
            UltilFunc.BindCombox(ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1", CommonLib.ReadXML("lblTatca"));
            if (ddlLang.Items.Count >= 3)
                ddlLang.SelectedIndex = Global.DefaultLangID;
            else ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, Global.DefaultCombobox);

        }
        #region Methods
        public void LoadData()
        {
            string where = " 1=1 ";
            if (ddlLang.SelectedIndex > 0)
                where += string.Format(" AND Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlType.SelectedIndex > 0)
                where += string.Format(" AND IsType = {0}", ddlType.SelectedValue);
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += string.Format(" AND URL like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            where += " Order by ID DESC";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_WebLinksDAL _cateDAL = new HPCBusinessLogic.DAL.T_WebLinksDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_WebLinksDynamic(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_WebLinksDynamic(pages.PageIndex - 1, pages.PageSize, where);
            grdListCate.DataSource = _ds;
            grdListCate.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
        }
        protected string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            return strReturn;
        }
        public string GetTypeLink(string type)
        {
            string strReturn = "";
            if (type == "1")
                strReturn = "Web Links";
            else if (type == "2")
                strReturn = "Sponsored Links";
            return strReturn;
        }
        #endregion

        #region Event Click
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quangcao/WebLinksEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }

        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            ArrayList ar = new ArrayList();
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                    ar.Add(int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
            }
            LoadData();
            for (int i = 0; i < ar.Count; i++)
            {
                int News_ID = int.Parse(ar[i].ToString());
                string _Url = "";
                HPCBusinessLogic.DAL.T_WebLinksDAL tt = new HPCBusinessLogic.DAL.T_WebLinksDAL();
                _Url = tt.load_T_WebLinks(News_ID).URL;
                tt.DeleteFrom_T_WebLinks(News_ID);
                string strLog = "[Danh sách liên kết website:]-->[Xóa liên kết] [URL:" + _Url + "";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa]", Request["Menu_ID"].ToString(), strLog, 0, 0);
            }
            LoadData();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_WebLinksDAL obj_Cate = new HPCBusinessLogic.DAL.T_WebLinksDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Quangcao/WebLinksEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }
        #endregion
    }
}
