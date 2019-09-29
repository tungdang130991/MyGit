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
    public partial class ListDoiTuong : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        T_Doituong _dt = null;
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
                    if (!IsPostBack)
                    {
                        lblMessError.Text = "";
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            BindList_Doituong();
                        }
                        else
                        {
                            BindList_Doituong();
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

        #region Method
        public void BindList_Doituong()
        {
            string where = " 1=1 ";

            DoituongDAL _DAL = new DoituongDAL();
            DataSet _ds;
            pages.PageSize = 10;//Global.MembersPerPage;
            _ds = _DAL.BindGridT_Doituong(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Doituong(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataView _dv = _ds.Tables[0].DefaultView;
                if (SortExtension.Length > 1)
                    _dv.Sort = SortExtension + " " + SortOrder;
                GVDoituong.DataSource = _dv;
                GVDoituong.DataBind();
                GVDoituong.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVDoituong.DataSource = _ds;
                GVDoituong.DataBind();
                int columncount = GVDoituong.Rows[0].Cells.Count;
                GVDoituong.Rows[0].Cells.Clear();
                GVDoituong.Rows[0].Cells.Add(new TableCell());
                GVDoituong.Rows[0].Cells[0].ColumnSpan = columncount;
                GVDoituong.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVDoituong.ShowFooter = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
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
            BindList_Doituong();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            GVDoituong.ShowFooter = true;
            BindList_Doituong();
        }
        #endregion

        #region Gridview
        protected void GVDoituong_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                ImageButton btnAdd = (ImageButton)e.Row.FindControl("btnAdd");
                if (btnAdd != null)
                    if (!_Role.R_Write)
                        btnAdd.Enabled = _Role.R_Write;
                if (btnDelete != null)
                {
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Bạn có chắc chắn muốn xóa?');");
                    DoituongDAL _dtDAL = new DoituongDAL();
                    int _id = Convert.ToInt32(GVDoituong.DataKeys[e.Row.RowIndex].Value);
                    if (_dtDAL.CheckExists_Madoituong(_id, 1) == 1)
                        btnDelete.Attributes.Add("onclick", "return alert(\"Đối tượng này đang được sử dụng\");");
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
                }
                Label lblSTT = (Label)e.Row.FindControl("lblSTT");
                if (lblSTT != null)
                {
                    lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Row.RowIndex + 1).ToString();
                }

                e.Row.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void AddNewRecord(object sender, EventArgs e)
        {
            GVDoituong.ShowFooter = true;
            BindList_Doituong();

        }
        protected void GVDoituong_OnRowCommand1(object sender, GridViewCommandEventArgs e)
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
                TextBox _ma = (TextBox)GVDoituong.FooterRow.FindControl("txt_MaDoituong");
                TextBox _ten = (TextBox)GVDoituong.FooterRow.FindControl("txt_Tendoituong");
                TextBox _englishname = (TextBox)GVDoituong.FooterRow.FindControl("txt_EnglishName");
                TextBox _stt = (TextBox)GVDoituong.FooterRow.FindControl("txt_STT");
                //Label _MaDT = (Label)GVDoituong.FooterRow.FindControl("lblMaDT_Error");
                //Label _STT = (Label)GVDoituong.FooterRow.FindControl("lblSTT_Error");
                int _return;
                DoituongDAL _doituongDAL = new DoituongDAL();
                if (_doituongDAL.Check_Madoituong(0, _ma.Text.Trim()) > 0)
                {
                    lblMessError.Text = "Mã đối tượng đã tồn tại";
                    GVDoituong.ShowFooter = true;
                    BindList_Doituong();
                }
                else if (_doituongDAL.Check_STT(0, Convert.ToInt32(_stt.Text.Trim())) > 0)
                {
                    lblMessError.Text = "Số thứ tự đã tồn tại";
                    GVDoituong.ShowFooter = true;
                    BindList_Doituong();
                }
                else
                {
                    _dt = new T_Doituong();
                    _dt.ID = 0;
                    _dt.Ma_Doituong = _ma.Text.Trim();
                    _dt.Ten_Doituong = _ten.Text.Trim();
                    _dt.EnglishName = _englishname.Text.Trim();
                    if (!String.IsNullOrEmpty(_stt.Text.Trim()))
                        _dt.STT = Convert.ToInt32(_stt.Text.Trim());

                    _dt.Ngaysua = DateTime.Now;
                    _dt.Nguoitao = _user.UserID;
                    _dt.Ngaytao = DateTime.Now;
                    _dt.Nguoisua = _user.UserID;
                    _return = _doituongDAL.InsertT_Doituong(_dt);
                    action.Thaotac = "[Thêm mới đối tượng]-->[Mã mã đối tượng:" + _return.ToString() + " ]";
                    actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                    BindList_Doituong();
                }
            }

        }
        protected void GVDoituong_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDoituong.PageIndex = e.NewPageIndex;

        }
        protected void GVDoituong_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            int _id = Convert.ToInt32(GVDoituong.DataKeys[e.RowIndex].Values["ID"].ToString());
            DoituongDAL _dtDAL = new DoituongDAL();
            if (_dtDAL.CheckExists_Madoituong(_id, 1) == 1)
            {
                return;
            }
            else
            {
                _dtDAL.DeleteOneFromT_Doituong(_id);
                action.Thaotac = "[Xóa đối tượng]-->[Mã đối tượng:" + _id.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
            }
            BindList_Doituong();
        }
        protected void GVDoituong_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVDoituong.EditIndex = e.NewEditIndex;
            BindList_Doituong();

        }
        protected void GVDoituong_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
            int _id = Convert.ToInt32(GVDoituong.DataKeys[e.RowIndex].Value.ToString());

            TextBox _ma = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_MaDoituong");
            TextBox _ten = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_Tendoituong");
            TextBox _englishname = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_EnglishName");
            TextBox _stt = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_STT");
            Label _MaDT = (Label)GVDoituong.Rows[e.RowIndex].FindControl("lblMaDT_Error");
            Label _STT = (Label)GVDoituong.Rows[e.RowIndex].FindControl("lblSTT_Error");
            DoituongDAL _dtDAL = new DoituongDAL();
            if (_dtDAL.Check_Madoituong(_id, _ma.Text.Trim()) > 0)
            {
                _MaDT.Text = "Mã đối tượng đã tồn tại";
                return;
            }
            else if (_dtDAL.Check_STT(_id, Convert.ToInt32(_stt.Text.Trim())) > 0)
            {
                _STT.Text = "Số thứ tự đã tồn tại";
                return;
            }
            else
            {
                _dt = new T_Doituong();
                _dt.ID = _id;
                _dt.Ma_Doituong = _ma.Text.Trim();
                _dt.Ten_Doituong = _ten.Text.Trim();
                _dt.EnglishName = _englishname.Text.Trim();
                if (!String.IsNullOrEmpty(_stt.Text.Trim()))
                    _dt.STT = Convert.ToInt32(_stt.Text.Trim());

                _dt.Ngaysua = DateTime.Now;
                _dt.Nguoitao = _user.UserID;
                _dt.Ngaytao = DateTime.Now;
                _dt.Nguoisua = _user.UserID;
                _return = _dtDAL.InsertT_Doituong(_dt);

                action.Thaotac = "[Sửa đối tượng]-->[Mã đối tượng:" + _return.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

                GVDoituong.EditIndex = -1;
                BindList_Doituong();
            }
        }
        protected void GVDoituong_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVDoituong.EditIndex = -1;
            BindList_Doituong();
            lblMessError.Text = "";
        }
        #endregion

        #region EVENT SORTING
        protected void GVDoituong_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (tc.HasControls())
                    {
                        // search for the header link
                        LinkButton lnk = (LinkButton)tc.Controls[0];
                        if (lnk != null)
                        {

                            // inizialize a new image
                            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                            // setting the dynamically URL of the image
                            //img.ImageUrl = "~/Images/icon_" + (GridViewAuthors.SortDirection == SortDirection.Ascending ? "asc" : "desc") + ".png";
                            img.ImageUrl = "~/Images/icon_" + SortOrder + ".gif";
                            // checking if the header link is the user's choice
                            if (SortExtension == lnk.CommandArgument)
                            {
                                // adding a space and the image to the header link
                                tc.Controls.Add(new LiteralControl(" "));
                                tc.Controls.Add(img);
                            }
                        }
                    }
                }
            }
        }
        protected string SortOrder
        {
            get
            {
                if (ViewState["_SortOrder"] == null)
                    return "asc";
                else
                    return ViewState["_SortOrder"].ToString();
            }
            set { ViewState["_SortOrder"] = "" + value; }
        }
        protected string SortExtension
        {
            get
            {
                if (ViewState["_SortExtension"] == null)
                    return "";
                else
                    return ViewState["_SortExtension"].ToString();
            }
            set { ViewState["_SortExtension"] = "" + value; }
        }
        protected void GVDoituong_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortOrder = SortOrder.ToString().Equals("asc") ? "desc" : "asc";
            SortExtension = e.SortExpression;
            BindList_Doituong();
        }



        #endregion

    }
}
