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
using System.Collections.Generic;

namespace ToasoanTTXVN.Baocaothongke
{
    public partial class Baocaolichsuthaotachethong : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);

                    if (!IsPostBack)
                    {
                        if (_user == null)
                            Page.Response.Redirect("~/login.aspx", true);
                    }
                }
            }
        }
        private void LoadData()
        {
            string _where = " 1=1 ";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
            {
                if (UltilFunc.ToDate(txt_tungay.Text.Trim(), "dd/MM/yyyy") > UltilFunc.ToDate(txt_denngay.Text.Trim(), "dd/MM/yyyy"))
                {
                    FuncAlert.AlertJS(this, "Từ ngày phải nhỏ hơn đến ngày");
                    _where += " and 1=0";
                }
                else
                    _where += " and NgayThaoTac>='" + txt_tungay.Text.Trim() + " 00:00:00' and NgayThaoTac<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            }
            if (txt_tennguoidung.Text.Trim() != "")
                _where += " and TenDayDu like N'" + txt_tennguoidung.Text.Trim() + "'";
            pages.PageSize = Global.MembersPerPage;
            Lichsu_Thaotac_HethongDAL _DAL = new Lichsu_Thaotac_HethongDAL();
            DataSet _ds;
            _ds = _DAL.BindGridT_ActionHistory(pages.PageIndex, pages.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_ActionHistory(pages.PageIndex - 1, pages.PageSize, _where);
            DataGrid_ThaoTacHeThong.DataSource = _ds;
            DataGrid_ThaoTacHeThong.DataBind();

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
