using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using SSOLib.ServiceAgent;
using HPCComponents;
using HPCInfo;
using System.IO;
using System.Data;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ListArticleLock : BasePage
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user = null;
        protected T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    if (!IsPostBack)
                    {
                        int tab_id = 0;
                        if (Request["Tab"] != null)
                        {
                            tab_id = Convert.ToInt32(Request["Tab"].ToString());
                            if (tab_id == 0)
                            {
                                this.TabContainer1.ActiveTabIndex = 0;
                                this.TabContainer1_ActiveTabChanged(sender, e);
                            }
                            if (tab_id == 1)
                            {
                                this.TabContainer1.ActiveTabIndex = 1;
                                this.TabContainer1_ActiveTabChanged(sender, e);
                            }
                        }
                        else
                        {
                            this.TabContainer1.ActiveTabIndex = 0;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        LoadComboBox();
                    }
                }
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham"," 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (ddlLang.Items.Count == 2)
                ddlLang.SelectedIndex = 1;
            else
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex > 0)
                UltilFunc.BindCombox(ddlCategorysAll, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "isunlock")
            {
                double _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                bool check = _untilDAL.GetOneFromT_NewsByID(_ID).News_Lock;
                if (check)
                {
                    _untilDAL.UpdateFromT_NewsDynamic(" News_Lock = 0 Where News_ID = " + _ID);
                    string strLog = "[Update News_Lock T_News 0]-->[T_News :ID: " + _ID + " Tittle: " + _untilDAL.GetOneFromT_NewsByID(_ID).News_Tittle + "]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Un Lock]", Request["Menu_ID"].ToString(), strLog, _ID, ConstAction.BaoDT);
                    this.LoadData();
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                LoadData();
            }
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorysAll.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(ddlCategorysAll, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.ddlLang.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                ddlCategorysAll.UpdateAfterCallBack = true;
            }
            else
            {
                ddlCategorysAll.DataSource = null;
                ddlCategorysAll.DataBind();
                ddlCategorysAll.UpdateAfterCallBack = true;
            }
        }
        private void LoadData()
        {
            string _where = " 1 = 1 AND News_Status <> 55 AND T_News.News_Lock =1 ";
            //if (ddlLang.SelectedIndex > 0)
            //    _where += " AND " + string.Format(" T_News.Lang_ID = {0}", ddlLang.SelectedValue);
            if (ddlCategorysAll.SelectedIndex > 0)
                _where += " AND " + string.Format(" T_News.CAT_ID = {0}", ddlCategorysAll.SelectedValue);
            if (txt_tieude.Text.Length > 0)
                _where += "AND " + string.Format(" News_Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_tieude.Text.Trim()));
            _where += "Order by T_News.News_DateEdit DESC ";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, _where);
            grdList.DataSource = _ds.Tables[0];
            grdList.DataBind();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetInnerBaiDangxyLy(" + TotalRecords + ");", true);
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
        }
        public void dgrListDetai_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        #region LoadFile Doc
        private void LoadFileDoc(int _ID)
        {
            string strHTML = "";
            HPCBusinessLogic.DAL.T_NewsDAL dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News obj = dal.load_T_news(_ID);
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b>" + obj.News_Tittle + "<o:p></o:p></b></p>";
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b><br>" + obj.News_Summary + "<u1:p></u1:p></b></p>";
            strHTML += "<p style='text-align:justify'>" + obj.News_Body + "<o:p></o:p></p>";
            if (strHTML.Length > 0)
                SaveAsText(strHTML);
        }
        private void SaveAsText(string _arr_IN)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            DirectoryInfo r = new DirectoryInfo(HttpContext.Current.Server.MapPath(HPCComponents.Global.GetAppPath(Request)));
            FileInfo[] file;
            file = r.GetFiles("*.doc");
            foreach (FileInfo i in file)
            {
                File.Delete(r.FullName + "\\" + i.Name);
            }
            strFileName = _user.UserName + "_BaiChoDuyet_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            Page.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }
        #endregion END ---------------------------------------
    }
}
