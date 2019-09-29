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
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Danhmuc
{
    public partial class ListLayout : BasePage
    {
        NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        DataTable _table;
        T_Layout _layout;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            BindList_Layout();
                        }
                        else
                        {
                            BindList_Layout();
                        }

                    }
                }
            }
        }

        #region Method
        public void BindList_Layout()
        {
            string where = " 1=1 ";

            LayoutDAL _DAL = new LayoutDAL();
            DataSet _ds;
            pages.PageSize = 20;//Global.MembersPerPage;
            _ds = _DAL.BindGridT_Layout(pages.PageIndex, pages.PageSize, where);
         
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Layout(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                GVLayout.DataSource = _ds;
                GVLayout.DataBind();
                GVLayout.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVLayout.DataSource = _ds;
                GVLayout.DataBind();
                int columncount = GVLayout.Rows[0].Cells.Count;
                GVLayout.Rows[0].Cells.Clear();
                GVLayout.Rows[0].Cells.Add(new TableCell());
                GVLayout.Rows[0].Cells[0].ColumnSpan = columncount;
                GVLayout.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVLayout.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;               
            panelContent.Visible = true;
               
           
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
        #endregion

        #region Click 
        
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindList_Layout();
        }
       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GVLayout.ShowFooter = true;
            BindList_Layout();
        }
        #endregion

        #region Gridview
        protected void GVLayout_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
            if (btnDelete != null)
            {
                LayoutDAL _dal = new LayoutDAL();
                int _id = Convert.ToInt32(GVLayout.DataKeys[e.Row.RowIndex].Value);
                if (_dal.CheckExists_Layout(_id) == 1)
                    btnDelete.Attributes.Add("onclick", "return alert(\"Layout này đang được sử dụng\");");
                else
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            Label lblSTT = (Label)e.Row.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Row.RowIndex + 1).ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void AddNewRecord(object sender, EventArgs e)
        {
            GVLayout.ShowFooter = true;
            BindList_Layout();

        }
        protected void GVLayout_OnRowCommand1(object sender, GridViewCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox _m = (TextBox)GVLayout.FooterRow.FindControl("txt_Mota");
                TextBox _dai = (TextBox)GVLayout.FooterRow.FindControl("txt_dai");
                TextBox _rong = (TextBox)GVLayout.FooterRow.FindControl("txt_rong");                
                double _d;
                double _r;
                int _return;
                if(!String.IsNullOrEmpty(_dai.Text.Trim()))
                    _d = Convert.ToDouble(_dai.Text.Trim());
                else
                    _d = 0;
                if(!String.IsNullOrEmpty(_rong.Text.Trim()))
                    _r = Convert.ToDouble(_rong.Text.Trim());
                else
                    _r = 0;
                LayoutDAL _layoutDAL = new LayoutDAL();
                _layout = new T_Layout();
                _layout.Ma_Layout = 0;
                _layout.Mota = _m.Text.Trim();
                _layout.Chieudai = _d;
                _layout.ChieuRong = _r;
                _return = _layoutDAL.InsertT_Layout(_layout);
                action.Thaotac = "[Thêm mới layout]-->[Mã layout:" + _return.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindList_Layout();
            }

        }
        protected void GVLayout_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVLayout.PageIndex = e.NewPageIndex;       

        }
        protected void GVLayout_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            int _id = Convert.ToInt32(GVLayout.DataKeys[e.RowIndex].Values["Ma_Layout"].ToString());
            LayoutDAL _layoutDAL = new LayoutDAL();
            if (_layoutDAL.CheckExists_Layout(_id) == 0)
            {
                _layoutDAL.DeleteOneFromT_Layout(_id);
                action.Thaotac = "[Xóa layout]-->[Mã layout:" + _id.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindList_Layout();
            }
            else
            {                
                BindList_Layout();
            }

        }
        protected void GVLayout_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVLayout.EditIndex = e.NewEditIndex;
            BindList_Layout();

        }
        protected void GVLayout_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            int _id = Convert.ToInt32(GVLayout.DataKeys[e.RowIndex].Value.ToString());
            LayoutDAL _layoutDAL = new LayoutDAL();
            TextBox _m = (TextBox)GVLayout.Rows[e.RowIndex].FindControl("txt_Mota");
            TextBox _dai = (TextBox)GVLayout.Rows[e.RowIndex].FindControl("txt_dai");
            TextBox _rong = (TextBox)GVLayout.Rows[e.RowIndex].FindControl("txt_rong");
            double _d;
            double _r;
            int _return;
            if (!String.IsNullOrEmpty(_dai.Text.Trim()))
                _d = Convert.ToDouble(_dai.Text.Trim());
            else
                _d = 0;
            if (!String.IsNullOrEmpty(_rong.Text.Trim()))
                _r = Convert.ToDouble(_rong.Text.Trim());
            else
                _r = 0;          
            _layout = new T_Layout();
            _layout.Ma_Layout = _id;
            _layout.Mota = _m.Text.Trim();
            _layout.Chieudai = _d;
            _layout.ChieuRong = _r;
            _return = _layoutDAL.InsertT_Layout(_layout);
            action.Thaotac = "[Sửa layout]-->[Mã layout:" + _return.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
            GVLayout.EditIndex = -1;
            BindList_Layout();
        }
        protected void GVLayout_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVLayout.EditIndex = -1;
            BindList_Layout();
        }
        #endregion
    }
}
