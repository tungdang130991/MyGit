using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using SSOLib.ServiceAgent;
using HPCInfo;
using HPCComponents;
using System.Data;

using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Quangcao
{
    public partial class CustomerList : System.Web.UI.Page
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
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }
        protected void ActiverPermission()
        {
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
        }

        #region Methods

        public void LoadData()
        {
            string where = " 1=1 ";
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += " AND " + string.Format(" Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            where += " Order by ID DESC";
            pages.PageSize = Global.MembersPerPage;
            T_CustomersDAL _cateDAL = new T_CustomersDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_CustomersDynamic(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_CustomersDynamic(pages.PageIndex - 1, pages.PageSize, where);
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

        #endregion

        #region Event Click

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }

        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quangcao/CustomerEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }

        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    LinkButton linkname = (LinkButton)m_Item.FindControl("btnEdit");
                    int News_ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    HPCBusinessLogic.DAL.T_CustomersDAL tt = new HPCBusinessLogic.DAL.T_CustomersDAL();
                    tt.DeleteFrom_T_Customers(News_ID);
                    //#region SYNC TEXT
                    //if (UltilFunc.Check_SyncNewsDatabase(Request["Menu_ID"]))
                    //    SynData_DeleteT_T_CustumerAny(News_ID, Global.Path_Service);
                    //#endregion
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                     Request["Menu_ID"].ToString(), "[Danh sách khách hàng]-->[Thao tác Xóa khách hàng]ID:" + News_ID.ToString() + " ]", 0, 0);
                }
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
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_CustomersDAL obj_Cate = new HPCBusinessLogic.DAL.T_CustomersDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Quangcao/CustomerEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                obj_Cate.DeleteFrom_T_Customers(Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString()));
                //#region SYNC
                //if (UltilFunc.Check_SyncNewsDatabase(Request["Menu_ID"]))
                //    SynData_DeleteT_T_CustumerAny(Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString()), Global.Path_Service);
                //#endregion
                string strLog = "[Xóa khách hàng]-->[Thao tác khách hàng][ID:" + this.grdListCate.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Xóa]", Request["Menu_ID"].ToString(), strLog, 0, ConstAction.TSAnh);
                this.LoadData();
            }
            else if (e.CommandArgument.ToString().ToLower() == "editorder")
            {
                string _catid = this.grdListCate.DataKeys[e.Item.ItemIndex].ToString();
                this.LoadData();
            }
        }
        #endregion

        //#region Syn --- DELETE T_Caterorys

        //private void SynData_DeleteT_T_CustumerAny(int _ID, ArrayList _arr)
        //{
        //    if (_arr.Count > 0)
        //    {
        //        for (int i = 0; i < _arr.Count; i++)
        //        {
        //            SynData_DeleteT_Custumers(_ID, _arr[i].ToString());
        //        }
        //    }
        //}

        //private void SynData_DeleteT_Custumers(int _ID, string urlService)
        //{
        //    string _sql = "[Syn_DeleteOneFromT_Customers]";
        //    ServicesPutDataBusines.UltilFunc _untilDAL = new ServicesPutDataBusines.UltilFunc(urlService);
        //    try
        //    {

        //        _untilDAL.ExecStore(_sql, new string[] { "@ID" }, new object[] { _ID });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion end
    }
}
