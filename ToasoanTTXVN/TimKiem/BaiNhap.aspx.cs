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
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Collections.Generic;


namespace ToasoanTTXVN.TimKiem
{
    public partial class BaiNhap : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        T_AutoSave_DAL _Dal = new T_AutoSave_DAL();
        int Ma_QTBT = 0;
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
                    Ma_QTBT =UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + _user.UserID);
                    if (!IsPostBack)
                    {
                        if (Ma_QTBT != 0)
                            LoadData();
                        else
                            Page.Response.Redirect("~/login.aspx", true);
                    }
                }
            }

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
                return " Ngaytao DESC";
            }
        }
        string BuildSQL(string sOrder)
        {
            string sWhere = " Ngaytao>='" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "' and Ma_NguoiTao=" + _user.UserID;

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "Nhập tiêu đề cần tìm")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  Ngaytao>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngaytao<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            }

            return sWhere + sOrder;
        }
        private void LoadData()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;

            DataSet _ds;
            _ds = _Dal.Sp_ListT_AutoSaveDynamic(pages.PageIndex, pages.PageSize, BuildSQL(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _Dal.Sp_ListT_AutoSaveDynamic(pages.PageIndex - 1, pages.PageSize, BuildSQL(sOrder));
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;

        }

        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        protected void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            int _ID = int.Parse(DataGrid_tinbai.DataKeys[e.Item.ItemIndex].ToString());

            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                _Dal.Sp_DeleteOneFromT_AutoSave(_ID);
                LoadData();
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
    }
}
