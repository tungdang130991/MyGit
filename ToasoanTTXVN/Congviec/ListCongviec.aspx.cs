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
namespace ToasoanTTXVN.Congviec
{
    public partial class ListCongviec : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
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
                        BindCombo();
                        this.TabContainer1_ActiveTabChanged(sender, e);
                        BindList_CongViec();
                        BindList_CongViec_Theodoi();
                    }
                }
            }
        }

        #region Method CV can lam

        public void BindList_CongViec()
        {
            string where = " NguoiNhan = " + _user.UserID.ToString();

            if (ddl_User.SelectedIndex > 0)
                where += " AND ( NguoiTao = " + ddl_User.SelectedValue.ToString() + " OR NguoiGiaoViec = " + ddl_User.SelectedValue.ToString() + ")";
            if (!String.IsNullOrEmpty(txt_NgayTao.Text.Trim()))
                where += " AND " + string.Format(" NgayTao >='{0}'", this.txt_NgayTao.Text.Trim() + " 00:00:00");
            if (!String.IsNullOrEmpty(txt_Ngayhoanthanh.Text.Trim()))
                where += " AND " + string.Format(" NgayHoanthanh <='{0}'", this.txt_Ngayhoanthanh.Text.Trim() + " 23:59:59");
            CongviecDAL _DAL = new CongviecDAL();
            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_Congviec(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Congviec(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                grdListCVCanLam.DataSource = _ds;
                grdListCVCanLam.DataBind();
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                grdListCVCanLam.DataSource = _ds;
                grdListCVCanLam.DataBind();
                int columncount = grdListCVCanLam.Items[0].Cells.Count;
                grdListCVCanLam.Items[0].Cells.Clear();
                grdListCVCanLam.Items[0].Cells.Add(new TableCell());
                grdListCVCanLam.Items[0].Cells[0].ColumnSpan = columncount;
                grdListCVCanLam.Items[0].Cells[0].Text = "Không có bản ghi nào";
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }


        #endregion

        #region Method CV can theo doi
        public void BindList_CongViec_Theodoi()
        {
            string where = " ( NguoiTao = " + _user.UserID.ToString() + " OR NguoiGiaoViec = " + _user.UserID.ToString() + " ) ";
            if (ddl_User.SelectedIndex > 0)
                where += " AND NguoiNhan = " + ddl_User.SelectedValue.ToString();
            if (!String.IsNullOrEmpty(txt_NgayTao.Text.Trim()))
                where += " AND " + string.Format(" NgayTao >='{0}'", this.txt_NgayTao.Text.Trim() + " 00:00:00");
            if (!String.IsNullOrEmpty(txt_Ngayhoanthanh.Text.Trim()))
                where += " AND " + string.Format(" NgayHoanthanh <='{0}'", this.txt_Ngayhoanthanh.Text.Trim() + " 23:59:59");
            CongviecDAL _DAL = new CongviecDAL();
            DataSet _ds;
            pages1.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_Congviec(pages1.PageIndex, pages1.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Congviec(pages1.PageIndex - 1, pages1.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                grdListTheodoi.DataSource = _ds;
                grdListTheodoi.DataBind();
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                grdListTheodoi.DataSource = _ds;
                grdListTheodoi.DataBind();
                int columncount = grdListTheodoi.Items[0].Cells.Count;
                grdListTheodoi.Items[0].Cells.Clear();
                grdListTheodoi.Items[0].Cells.Add(new TableCell());
                grdListTheodoi.Items[0].Cells[0].ColumnSpan = columncount;
                grdListTheodoi.Items[0].Cells[0].Text = "Không có bản ghi nào";
            }
            pages1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = pages1.CalculateTotalPages();
            CurrentPage1.PageIndex = pages1.PageIndex;
            Session["CurrentPage"] = pages1.PageIndex;
        }
        #endregion

        #region Function
        private void BindCombo()
        {
            DataTable _dt = new DataTable();
            _dt = _userDAL.BindGridT_Users(0, 1000, " UserID <> " + _user.UserID.ToString()).Tables[0];

            ddl_User.Items.Clear();
            ddl_User.DataSource = _dt;
            ddl_User.DataBind();
            ddl_User.Items.Insert(0, "<<-------->>");

        }
        protected string GetURL(string str, string _ID)
        {
            string strReturn = "";
            CongviecDAL _dal = new CongviecDAL();
            if (str == "1") //da hoan thanh
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/OK.gif";
            if (str == "0") //chua duyet
            {
                //strReturn = Global.ApplicationPath + "/Dungchung/Images/Warning32.png";

            }

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
        protected string BindUserName(string _Id)
        {
            string strReturn = "";
            T_Users _nguoidung = new T_Users();

            if (!String.IsNullOrEmpty(_Id) && Convert.ToInt32(_Id) > 0)
            {
                _nguoidung = _userDAL.GetUserByUserName_ID(Convert.ToInt32(_Id));
                strReturn = _nguoidung.UserFullName;
            }
            else
                strReturn = "";
            return strReturn;
        }
        #endregion

        #region Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (TabContainerListCV.ActiveTabIndex == 0)
            {
                pages.PageIndex = 0;
                this.BindList_CongViec();
            }
            if (TabContainerListCV.ActiveTabIndex == 1)
            {
                pages1.PageIndex = 0;
                this.BindList_CongViec_Theodoi();
            }
        }
        protected void pages_IndexChangedCVCanlam(object sender, EventArgs e)
        {
            BindList_CongViec();
        }
        protected void pages_IndexChanged_Theodoi(object sender, EventArgs e)
        {
            BindList_CongViec_Theodoi();
        }
        public void grdListCVCanLam_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }

            Label _ngayHT = (Label)e.Item.FindControl("lblNgayHoanthanh");
            Label _status = (Label)e.Item.FindControl("lblStatus");
            if (_status != null)
            {
                if (_ngayHT != null)
                {
                    ImageButton _img = (ImageButton)e.Item.FindControl("imgStatus");
                    if (_status.Text.Trim() == "0")
                    {
                        _img.Enabled = true;
                        if (UltilFunc.ToDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy") > UltilFunc.ToDate(_ngayHT.Text, "dd/MM/yyyy"))
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Dialog_warning24.png";
                            _img.ToolTip = "Quá hạn hoàn thành công việc";
                            e.Item.Style.Add("background-color", "#FFEC8B");
                        }
                        else if (UltilFunc.ToDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy") >= UltilFunc.ToDate(_ngayHT.Text, "dd/MM/yyyy").AddDays(-2))
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Statusdialog_warning24.png";
                            _img.ToolTip = "Sắp hết hạn công việc";
                        }
                        else
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Document_edit24.png";
                            _img.ToolTip = "Đang thực hiện";
                        }
                    }
                    else
                    {
                        _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Sign_Select24.png";
                        _img.ToolTip = "Công việc đã hoàn thành";
                    }
                }
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdListCVCanLam_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            CongviecDAL objDAL = new CongviecDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int cvID = Convert.ToInt32(this.grdListCVCanLam.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + cvID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                //objDAL.DeleteOneFromT_Congviec(Convert.ToDouble(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                //action.Thaotac = "[Xóa T_Congviec]-->[Thao tác Xóa Trong Bảng T_Congviec][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                //this.BindList_CongViec();
            }
            if (e.CommandArgument.ToString().ToLower() == "isedit")
            {
                int cvID = Convert.ToInt32(this.grdListCVCanLam.DataKeys[e.Item.ItemIndex].ToString());
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                CongviecDAL _dal = new CongviecDAL();
                if (lblStatus != null)
                {
                    if (lblStatus.Text == "1")
                    {
                        _dal.UpdateStatusCongViec(cvID, 0);
                        action.Thaotac = "[Update T_Congviec]-->[Status = 0][ID:" + this.grdListCVCanLam.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                    }
                    else
                    {
                        _dal.UpdateStatusCongViec(cvID, 1);
                        action.Thaotac = "[Update T_Congviec]-->[Status = 1][ID:" + this.grdListCVCanLam.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                    }
                }
                BindList_CongViec();
            }
        }

        public void grdListTheodoi_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages1.PageIndex * pages1.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }

            Label _ngayHT = (Label)e.Item.FindControl("lblNgayHoanthanh");
            Label _status = (Label)e.Item.FindControl("lblStatus");
            if (_status != null)
            {
                if (_ngayHT != null)
                {
                    ImageButton _img = (ImageButton)e.Item.FindControl("imgStatus");
                    if (_status.Text.Trim() == "0")
                    {
                        _img.Enabled = true;
                        if (UltilFunc.ToDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy") > UltilFunc.ToDate(_ngayHT.Text, "dd/MM/yyyy"))
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Dialog_warning24.png";
                            _img.ToolTip = "Quá hạn hoàn thành công việc";
                            e.Item.Style.Add("background-color", "#FFEC8B");
                        }
                        else if (UltilFunc.ToDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy") >= UltilFunc.ToDate(_ngayHT.Text, "dd/MM/yyyy").AddDays(-2))
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Statusdialog_warning24.png";
                            _img.ToolTip = "Sắp hết hạn công việc";
                        }
                        else
                        {
                            _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Document_edit24.png";
                            _img.ToolTip = "Đang thực hiện";
                        }
                    }
                    else
                    {
                        _img.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/Sign_Select24.png";
                        _img.ToolTip = "Công việc đã hoàn thành";
                    }
                }
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainerListCV.ActiveTabIndex == 0)
            {
                this.lblUser.Text = "Người giao việc";
                this.BindList_CongViec();
            }
            if (TabContainerListCV.ActiveTabIndex == 1)
            {
                this.lblUser.Text = "Người nhận việc";
                this.BindList_CongViec_Theodoi();
            }
        }

        #endregion
    }
}
