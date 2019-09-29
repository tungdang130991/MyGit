using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Danhmuc
{
    public partial class ListKichthuoc : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_Kichthuoc _kt = null;
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
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            BindList_Kichthuoc();
                        }
                        else
                        {
                            BindList_Kichthuoc();
                        }
                    }
                }
            }
        }

        #region Method
        public void BindList_Kichthuoc()
        {
            string where = " 1=1 ";

            KichthuocDAL _DAL = new KichthuocDAL();
            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_Kichthuoc(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Kichthuoc(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                GVKichthuoc.DataSource = _ds;
                GVKichthuoc.DataBind();               
                GVKichthuoc.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVKichthuoc.DataSource = _ds;
                GVKichthuoc.DataBind();
                int columncount = GVKichthuoc.Rows[0].Cells.Count;
                GVKichthuoc.Rows[0].Cells.Clear();
                GVKichthuoc.Rows[0].Cells.Add(new TableCell());
                GVKichthuoc.Rows[0].Cells[0].ColumnSpan = columncount;
                GVKichthuoc.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVKichthuoc.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }
        protected string TenAnpham(string _maAnpham)
        {
            string strReturn = "";
            if(!String.IsNullOrEmpty(_maAnpham) && Convert.ToInt32(_maAnpham)> 0)
           
            {
                T_AnPham _anpham;
                AnPhamDAL _anphamDAL = new AnPhamDAL();
                _anpham = _anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham));
                strReturn = _anpham.Ten_AnPham;
            }
            else
                strReturn = "";
            return strReturn;
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
            BindList_Kichthuoc();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GVKichthuoc.ShowFooter = true;
            BindList_Kichthuoc();
        }
        #endregion

        #region Gridview
        protected void GVKichthuoc_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVKichthuoc.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlAnPham = (DropDownList)e.Row.FindControl("ddl_AnPham");
                UltilFunc.BindCombox(ddlAnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham");
                Label lblAnpham = (Label)e.Row.FindControl("lblAnpham");
                if(Convert.ToInt32(lblAnpham.Text.Trim()) > 0)
                     ddlAnPham.Items.FindByValue(lblAnpham.Text).Selected = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlAnPham = (DropDownList)e.Row.FindControl("ddl_AnPham");
                UltilFunc.BindCombox(ddlAnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham");
            }
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
            if (btnDelete != null)
            {
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
            GVKichthuoc.ShowFooter = true;
            BindList_Kichthuoc();

        }
        protected void GVKichthuoc_OnRowCommand1(object sender, GridViewCommandEventArgs e)
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
                TextBox _ten = (TextBox)GVKichthuoc.FooterRow.FindControl("txt_Kichthuoc");
                TextBox _mota = (TextBox)GVKichthuoc.FooterRow.FindControl("txt_mota");
                DropDownList _ddlAnpham = (DropDownList)GVKichthuoc.FooterRow.FindControl("ddl_AnPham");
                int _return;
                KichthuocDAL _kichthuocDAL = new KichthuocDAL();
                _kt = new T_Kichthuoc();
                _kt.Ma_Kichthuoc = 0;
                _kt.Ten_Kichthuoc = _ten.Text.Trim();
                _kt.Mota = _mota.Text.Trim();
                if (Convert.ToInt32(_ddlAnpham.SelectedValue.ToString()) > 0)
                    _kt.Ma_Anpham = Convert.ToInt32(_ddlAnpham.SelectedValue.ToString());
                else
                    _kt.Ma_Anpham = 0;
                _kt.Ngaysua = DateTime.Now;
                _kt.Nguoitao = _user.UserID;
                _kt.Ngaytao = DateTime.Now;
                _kt.Nguoisua = _user.UserID;
                _return =  _kichthuocDAL.InsertT_Kichthuoc(_kt);
                action.Thaotac = "[Thêm mới kích thước]-->[Mã kích thước:" + _return.ToString() + " ]";               
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindList_Kichthuoc();
            }

        }
        protected void GVKichthuoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVKichthuoc.PageIndex = e.NewPageIndex;

        }
        protected void GVKichthuoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            int _id = Convert.ToInt32(GVKichthuoc.DataKeys[e.RowIndex].Values["Ma_Kichthuoc"].ToString());
            KichthuocDAL _ktDAL = new KichthuocDAL();
            _ktDAL.DeleteOneFromT_Kichthuoc(_id);

            action.Thaotac = "[Xóa kích thước]-->[Mã kích thước:" + _id.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            BindList_Kichthuoc();

        }
        protected void GVKichthuoc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVKichthuoc.EditIndex = e.NewEditIndex;
            BindList_Kichthuoc();

        }
        protected void GVKichthuoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            int _return;
            int _id = Convert.ToInt32(GVKichthuoc.DataKeys[e.RowIndex].Value.ToString());
       
            TextBox _ten = (TextBox)GVKichthuoc.Rows[e.RowIndex].FindControl("txt_Kichthuoc");
            TextBox _mota = (TextBox)GVKichthuoc.Rows[e.RowIndex].FindControl("txt_mota");
            DropDownList _ddlAnpham = (DropDownList)GVKichthuoc.Rows[e.RowIndex].FindControl("ddl_AnPham");

            KichthuocDAL _ktDAL = new KichthuocDAL();
            _kt = new T_Kichthuoc();
            _kt.Ma_Kichthuoc = _id;
            _kt.Ten_Kichthuoc = _ten.Text.Trim();
            _kt.Mota = _mota.Text.Trim();
            if (Convert.ToInt32(_ddlAnpham.SelectedValue.ToString()) > 0)
                _kt.Ma_Anpham = Convert.ToInt32(_ddlAnpham.SelectedValue.ToString());
            else
                _kt.Ma_Anpham = 0;
            _kt.Ngaysua = DateTime.Now;
            _kt.Nguoitao = _user.UserID;
            _kt.Ngaytao = DateTime.Now;
            _kt.Nguoisua = _user.UserID;
            _return = _ktDAL.InsertT_Kichthuoc(_kt);

            action.Thaotac = "[Sửa kích thước]-->[Mã kích thước:" + _return.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            GVKichthuoc.EditIndex = -1;             
            BindList_Kichthuoc();
        }
        protected void GVKichthuoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVKichthuoc.EditIndex = -1;
            BindList_Kichthuoc();
          
        }
        #endregion
    }
}
