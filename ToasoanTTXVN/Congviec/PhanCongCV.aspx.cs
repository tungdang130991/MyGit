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
    public partial class PhanCongCV : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        CongviecDAL _DALCV = new CongviecDAL();
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
                    btnAdd.Visible = _Role.R_Read;
                    pages.PageIndex = 0;
                    if (!IsPostBack)
                    {
                        UltilFunc.BindCombox(cbo_room, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1", "---All---");
                        BinddDropDownList(0);
                        int tab_id = 0;

                        if (Request["Tab"] != null)
                        {
                            tab_id = Convert.ToInt32(Request["Tab"].ToString());

                        }
                        if (tab_id == -1)
                        {
                            this.TabContainerListCV.ActiveTabIndex = 0;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
                        else
                        {
                            this.TabContainerListCV.ActiveTabIndex = tab_id;
                            this.TabContainer1_ActiveTabChanged(sender, e);
                        }
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

        #region Methods
        public void BinddDropDownList(double maphong)
        {
            DataTable _dt = new DataTable();
            string _where = string.Empty;
            if (maphong > 0)
                _where = " IsDeleted = 0 and ProvinceID=" + maphong;
            else
                _where = " IsDeleted = 0";
            _dt = _userDAL.GetT_User_Dynamic(_where).Tables[0];

            cbo_nguoinhan.DataSource = _dt;
            cbo_nguoinhan.DataTextField = "UserFullName";
            cbo_nguoinhan.DataValueField = "UserID";
            cbo_nguoinhan.DataBind();
            cbo_nguoinhan.Items.Insert(0, "---All---");
        }
        public string _GetWhereThuchienCV()
        {
            DataTable _dt = new DataTable();
            string _where = string.Empty;
            string phong = string.Empty;
            string _sql = " IsDeleted = 0 and ProvinceID<>0 and UserID=" + _user.UserID;
            _dt = _userDAL.GetT_User_Dynamic(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                phong = _dt.Rows[0]["ProvinceID"].ToString();
            if (phong != string.Empty)
                _where = "(NguoiNhan = " + _user.UserID.ToString() + " or NguoiNhan =0) and (Phong_ID=0 or Phong_ID =" + _dt.Rows[0]["ProvinceID"].ToString() + ") and NguoiGiaoViec<>" + _user.UserID;
            else
                _where = "(NguoiNhan = " + _user.UserID.ToString() + " or NguoiNhan =0) and Phong_ID=0 and NguoiGiaoViec<>" + _user.UserID;

            if (cbo_nguoinhan.SelectedIndex > 0)
                _where += " AND ( NguoiTao = " + cbo_nguoinhan.SelectedValue + " OR NguoiGiaoViec = " + cbo_nguoinhan.SelectedValue + ")";
            if (cbo_room.SelectedIndex > 0)
                _where += " AND Phong_ID=" + cbo_room.SelectedValue;
            if (!String.IsNullOrEmpty(txt_denngay.Text.Trim()))
                _where += " AND " + string.Format(" NgayHoanthanh >='{0}'", this.txt_denngay.Text.Trim() + " 23:59:59");
            return _where;
        }
        public string _GetWhereGiaoViec()
        {
            string where = " ( NguoiTao = " + _user.UserID.ToString() + " OR NguoiGiaoViec = " + _user.UserID.ToString() + " ) ";

            if (cbo_nguoinhan.SelectedIndex > 0)
                where += " AND NguoiNhan = " + cbo_nguoinhan.SelectedValue + " ";
            if (cbo_room.SelectedIndex > 0)
                where += " AND Phong_ID=" + cbo_room.SelectedValue;
            if (!String.IsNullOrEmpty(txt_denngay.Text.Trim()))
                where += " AND " + string.Format(" NgayHoanthanh >='{0}'", this.txt_denngay.Text.Trim() + " 23:59:59");
            return where;
        }
        public void BindList_CongViec()
        {
            string where = _GetWhereThuchienCV();

            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DALCV.BindGridT_Congviec(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DALCV.BindGridT_Congviec(pages.PageIndex - 1, pages.PageSize, where);
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
        public void GetTotalRecordCV()
        {
            string _thuchiencv = "0", _giaoviec = "0";
            _giaoviec = ulti.GetColumnValuesTotal("T_Congviec", "COUNT (Ma_Congviec) as Total", _GetWhereGiaoViec());
            _thuchiencv = ulti.GetColumnValuesTotal("T_Congviec", "COUNT (Ma_Congviec) as Total", _GetWhereThuchienCV());
            this.tabpnlCVCanlam.HeaderText = CommonLib.ReadXML("lblCongviecduocgiao") + " (" + _thuchiencv + ")";
            this.tabCVTheodoi.HeaderText = CommonLib.ReadXML("lblCongviecdagiao") + " (" + _giaoviec + ")";

        }
        protected string StatusComplete(Object hoanthanh)
        {
            string strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (hoanthanh != DBNull.Value)
            {
                if (int.Parse(hoanthanh.ToString()) == 1)
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/mark.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            return strReturn;
        }

        public void BindListGiaoViec()
        {
            string where = _GetWhereGiaoViec();
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
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }

        #endregion

        #region Event Click
        protected void cbo_room_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_nguoinhan.Items.Clear();
            if (cbo_room.SelectedIndex > 0)
            {
                BinddDropDownList(int.Parse(cbo_room.SelectedValue));
            }
            else
            {
                cbo_nguoinhan.DataSource = null;
                cbo_nguoinhan.DataBind();
                cbo_nguoinhan.Items.Insert(0, "---All---");
            }
            this.TabContainer1_ActiveTabChanged(sender, e);

        }
        public void grdListCVCanLam_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                Label lblSTT = (Label)e.Item.FindControl("lblSTT");
                if (lblSTT != null)
                {
                    lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
                }
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null)
                {
                    if (TabContainerListCV.ActiveTabIndex == 0)
                        btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn hủy công việc không?\");");
                    if (TabContainerListCV.ActiveTabIndex == 1)
                        btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn nhận công việc không?\");");
                }
                Label _ngayHT = (Label)e.Item.FindControl("lblNgayHoanthanh");
                Label _status = (Label)e.Item.FindControl("lblStatus");

                if (_ngayHT != null)
                {
                    ImageButton _img = (ImageButton)e.Item.FindControl("imgStatus");
                    if (_status.Text.Trim() == "0")
                    {

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
            string Thaotac = string.Empty;
            string _ID = string.Empty;
            if (TabContainerListCV.ActiveTabIndex == 0)
                _ID = grdListCVCanLam.DataKeys[e.Item.ItemIndex].ToString();
            else if (TabContainerListCV.ActiveTabIndex == 1)
                _ID = grdListTheodoi.DataKeys[e.Item.ItemIndex].ToString();
            int tab = 0;
            tab = TabContainerListCV.ActiveTabIndex;
            TextBox txt_phanhoi = (TextBox)e.Item.FindControl("txt_phanhoi");
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                if (tab != 1)
                    Response.Redirect("~/Congviec/NoidungCV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID + "&Tab=" + tab.ToString());
                else
                    Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID + "&Tab=" + tab.ToString());
            }
            else if (e.CommandArgument.ToString().ToLower() == "nhanviec")
            {
                string sql = "update T_Congviec set Loai=0,Vet=N'" + txt_phanhoi.Text.Trim() + "', NguoiNhan=" + _user.UserID + ", TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + _ID;
                ulti.ExecSql(sql);
                Thaotac = _user.UserFullName + "thực hiện nhận việc " + _DALCV.GetOneFromT_CongviecByID(double.Parse(_ID)).Tencongviec;

            }
            else if (e.CommandArgument.ToString().ToLower() == "finishcv")
            {
                string sql = string.Empty;
                int _status = UltilFunc.GetColumnValuesOne("T_Congviec", "Status", " Ma_Congviec=" + _ID);
                if (_status == 0)
                    sql = "update T_Congviec set Status=1,Vet=N'" + txt_phanhoi.Text.Trim() + "', NguoiNhan=" + _user.UserID + ", TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + _ID;
                else
                    sql = "update T_Congviec set Status=0,Vet=N'" + txt_phanhoi.Text.Trim() + "', NguoiNhan=" + _user.UserID + ", TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + _ID;
                ulti.ExecSql(sql);
                Thaotac = _user.UserFullName + "thực hiện hoàn thành công việc " + _DALCV.GetOneFromT_CongviecByID(double.Parse(_ID)).Tencongviec;

            }
            else if (e.CommandArgument.ToString().ToLower() == "cancelcv")
            {
                string sql = "update T_Congviec set Status=0, Vet=N'" + txt_phanhoi.Text.Trim() + "',NguoiNhan=0, TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + _ID;
                ulti.ExecSql(sql);
                Thaotac = _user.UserFullName + "thực hiện hủy việc " + _DALCV.GetOneFromT_CongviecByID(double.Parse(_ID)).Tencongviec;

            }
            else if (e.CommandArgument.ToString().ToLower() == "phanhoi")
            {
                string sql = "update T_Congviec set Vet=N'" + txt_phanhoi.Text.Trim() + "',TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + _ID;
                ulti.ExecSql(sql);
            }
            else if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                Thaotac = "Thực hiện xóa công viêc " + _DALCV.GetOneFromT_CongviecByID(double.Parse(_ID)).Tencongviec;
                _DALCV.DeleteOneFromT_Congviec(double.Parse(_ID), _user.UserID);     
            }
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            this.TabContainerListCV.ActiveTabIndex = tab;
            this.TabContainer1_ActiveTabChanged(source, e);
        }
        public void grdListTheodoi_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                Label lblSTT = (Label)e.Item.FindControl("lblSTT");
                if (lblSTT != null)
                {
                    lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
                }
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (btnDelete != null)
                {
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa công việc không?\");");
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
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&Tab=" + TabContainerListCV.ActiveTabIndex.ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            GetTotalRecordCV();
            if (TabContainerListCV.ActiveTabIndex == 0)
            {
                this.lblUser.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblNguoigiao");
                this.BindList_CongViec();
            }
            if (TabContainerListCV.ActiveTabIndex == 1)
            {
                this.lblUser.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblNguoinhan");
                this.BindListGiaoViec();
            }
        }

        #endregion

        #region Check Function
        public bool Check_Congviec_Nguoitao(string _ID)
        {
            bool _return;
            CongviecDAL _dal = new CongviecDAL();
            return _return = _dal.Check_Nguoitao_Congviec(Convert.ToDouble(_ID), _user.UserID);
        }

        #endregion
    }
}
