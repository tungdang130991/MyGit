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
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Hethong
{
    public partial class QuytrinhDynamic : BasePage
    {
        UltilFunc Ulti = new UltilFunc();
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        HPCBusinessLogic.DAL.QuyTrinhDAL _QTDAL = new HPCBusinessLogic.DAL.QuyTrinhDAL();
        HPCBusinessLogic.DAL.TenQuyTrinh_DAL _daltenqt = new HPCBusinessLogic.DAL.TenQuyTrinh_DAL();
        HPCBusinessLogic.DoituongDAL _DTDAL = new HPCBusinessLogic.DoituongDAL();
        T_Doituong _dt = new T_Doituong();
        private int maanpham
        {
            get { if (ViewState["maanpham"] != null) return Convert.ToInt32(ViewState["maanpham"]); else return 0; }

            set { ViewState["maanpham"] = value; }
        }
        private int EID
        {
            get
            {
                if (ViewState["EID"] == null)
                    ViewState["EID"] = -1;
                return (int)ViewState["EID"];
            }
            set
            {
                ViewState["EID"] = value;
            }
        }
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
                    btn_add.Visible = _Role.R_Read;
                    string menuID = Request["Menu_ID"].ToString();
                    lblMenuID.Value = menuID;
                    lblIPAddress.Value = IpAddress();
                    if (!IsPostBack)
                    {
                        BindData();
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
        protected void BindData()
        {
            string where = "1=1";
            if (!String.IsNullOrEmpty(txtTenAnpham.Text.Trim()))
                where += "AND " + string.Format(" Ten_QuyTrinh like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTenAnpham.Text.Trim()));

            pages.PageSize = Global.MembersPerPage;
            DataSet _ds;
            _ds = _daltenqt.BindGridT_TenQuyTrinh(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _daltenqt.BindGridT_TenQuyTrinh(pages.PageIndex - 1, pages.PageSize, where);
            grdListAnpham.DataSource = _ds.Tables[0].DefaultView;
            grdListAnpham.DataBind();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void linkSearch_OnClick(object sender, EventArgs e)
        {
            BindData();
        }
        protected void OnClick_btn_add(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Hethong/Edit_QuyTrinh.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
        private void SetAddEdit(bool isList, bool isRole)
        {
            pnlList.Visible = isList;
            plRole.Visible = isRole;
        }

        private void BinQuytrinhtheoAnPham()
        {

            DataSet _ds = _DTDAL.BindT_Doituong_AnPham(maanpham);
            rptDoituong.DataSource = _ds;
            rptDoituong.DataBind();
            DataSet _dsDTGui = _QTDAL.Bind_DoituongGui(maanpham);
            string dtGui = _dsDTGui.Tables[0].Rows[0][0].ToString();
            lblDTGui.Value = dtGui;
            DataSet _dsDTNhan = _QTDAL.Bind_DoituongNhan(maanpham);
            string dtNhan = _dsDTNhan.Tables[0].Rows[0][0].ToString();
            lblDTNhan.Value = dtNhan;

        }
        public void grdListWorkFlow_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                ImageButton btneditor = (ImageButton)e.Item.FindControl("btneditor");
                ImageButton btnCopyQT = (ImageButton)e.Item.FindControl("btnCopyQT");
                ImageButton btnHoatdong = (ImageButton)e.Item.FindControl("btnHoatdong");
                if (btnEdit != null)
                    if (!_Role.R_Write)
                        btnEdit.Enabled = _Role.R_Write;
                if (btneditor != null)
                    if (!_Role.R_Write)
                        btneditor.Enabled = _Role.R_Write;
                if (btnCopyQT != null)
                    if (!_Role.R_Write)
                        btnCopyQT.Enabled = _Role.R_Write;
                if (btnHoatdong != null)
                    if (!_Role.R_Write)
                        btnHoatdong.Enabled = _Role.R_Write;
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListWorkFlow_EditCommand(object source, DataGridCommandEventArgs e)
        {
            UltilFunc _ulti = new UltilFunc();
            HPCBusinessLogic.DAL.TenQuyTrinh_DAL _dalqt = new HPCBusinessLogic.DAL.TenQuyTrinh_DAL();
            maanpham = Convert.ToInt32(this.grdListAnpham.DataKeys[e.Item.ItemIndex]);
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                Page.Response.Redirect("~/Hethong/Edit_QuyTrinh.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + maanpham);
            }
            if (e.CommandArgument.ToString().ToLower() == "copy")
            {
                _QTDAL.CopyQuyTrinhBienTap(maanpham);
                BindList_Doituong();
            }
            if (e.CommandArgument.ToString().ToLower() == "editdisplay")
            {
                string _sql = string.Empty;
                bool check = _dalqt.GetOneFromT_TenQuyTrinhByID(maanpham).Active;
                if (check)
                    _sql = "update T_Ten_QuyTrinh set Active=0 where ID=" + maanpham;
                else
                    _sql = "update T_Ten_QuyTrinh set Active=1 where ID=" + maanpham;
                _ulti.ExecSql(_sql);
                BindData();
            }
            if (e.CommandArgument.ToString().ToLower() == "role")
            {
                SetAddEdit(false, true);

                lblMaAnPham.Value = this.grdListAnpham.DataKeys[e.Item.ItemIndex].ToString();
                string Tenanpham = UltilFunc.GetTenAnpham_Display(maanpham);
                roleChucNang.Text = CommonLib.ReadXML("lblCauhinhquytrinh")  + Tenanpham;
                BinQuytrinhtheoAnPham();
                BindList_Doituong();
            }

        }
        public void BindList_Doituong()
        {
            string where = " T_Doituong.Ma_Doituong not in (select T_Doituong_Anpham.Ma_Doituong from T_Doituong_Anpham where T_Doituong_Anpham.Ma_AnPham = " + maanpham + ") ";

            DoituongDAL _DAL = new DoituongDAL();
            DataSet _ds;
            pages.PageSize = Global.MembersPerPage;
            _ds = _DAL.BindGridT_Doituong(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Doituong(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataView _dv = _ds.Tables[0].DefaultView;
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
        #endregion

        #region Click
        protected void btnExit_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false);
            Page_Load(sender, e);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BinQuytrinhtheoAnPham();
            BindList_Doituong();

        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            GVDoituong.ShowFooter = true;
            BindList_Doituong();
            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
        }
        protected void ThemDT_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow item in GVDoituong.Rows)
            {
                CheckBox chk_Select = (CheckBox)item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    #region GhiLog
                    Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
                    T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
                    action.Ma_Nguoidung = _user.UserID;
                    action.TenDaydu = _user.UserFullName;
                    action.HostIP = IpAddress();
                    action.NgayThaotac = DateTime.Now;
                    #endregion
                    Label txtMaDT = (Label)item.FindControl("lblMaDT");
                    string MaDT = txtMaDT.Text;
                    _DTDAL.InsertT_Doituong_AnPham(MaDT, maanpham);
                    action.Thaotac = "[Thêm đối tượng vào ấn phẩm]-->[Mã đối tượng:" + MaDT + " ]";
                    actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

                }
            }
            BindList_Doituong();
            BinQuytrinhtheoAnPham();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            EID = -1;
            BinQuytrinhtheoAnPham();
            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
        }
        #endregion

        #region Popup

        protected void GVDoituong_OnRowCommand(object source, GridViewCommandEventArgs e)
        {
            HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox _ma = (TextBox)GVDoituong.FooterRow.FindControl("txt_MaDoituong");
                TextBox _ten = (TextBox)GVDoituong.FooterRow.FindControl("txt_Tendoituong");
                TextBox _stt = (TextBox)GVDoituong.FooterRow.FindControl("txt_STT");
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
                    if (!String.IsNullOrEmpty(_stt.Text.Trim()))
                        _dt.STT = Convert.ToInt32(_stt.Text.Trim());

                    _dt.Ngaysua = DateTime.Now;
                    _dt.Nguoitao = _user.UserID;
                    _dt.Ngaytao = DateTime.Now;
                    _dt.Nguoisua = _user.UserID;
                    _return = _doituongDAL.InsertT_Doituong(_dt);
                    action.Thaotac = "[Thêm mới đối tượng]-->[Mã mã đối tượng:" + _return.ToString() + " ]";
                    actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                    lblMessError.Text = "";
                    BindList_Doituong();
                    BinQuytrinhtheoAnPham();
                }

                UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
            }
        }
        protected void GVDoituong_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVDoituong.EditIndex = e.NewEditIndex;
            BindList_Doituong();
            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
        }
        protected void GVDoituong_RowUpdating(object source, GridViewUpdateEventArgs e)
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

            TextBox txtMaDT = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_MaDoituong");
            TextBox txtTenDT = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_Tendoituong");
            TextBox txtSTT = (TextBox)GVDoituong.Rows[e.RowIndex].FindControl("txt_STT");
            Label _MaDT = (Label)GVDoituong.Rows[e.RowIndex].FindControl("lblMaDT_Error");
            Label _STT = (Label)GVDoituong.Rows[e.RowIndex].FindControl("lblSTT_Error");
            DoituongDAL _dtDAL = new DoituongDAL();
            try
            {
                int.Parse(txtSTT.Text);
            }
            catch (Exception)
            {
                lblMessError.Text = "Nhập lại số thứ tự";
                throw;
            }
            if (_dtDAL.Check_Madoituong(_id, txtMaDT.Text.Trim()) > 0)
            {
                _MaDT.Text = "Mã đối tượng đã tồn tại";
                return;
            }
            else if (_dtDAL.Check_STT(_id, Convert.ToInt32(txtSTT.Text.Trim())) > 0)
            {
                _STT.Text = "Số thứ tự đã tồn tại";
                return;
            }
            else
            {
                _dt = new T_Doituong();
                _dt.ID = _id;
                _dt.Ma_Doituong = txtMaDT.Text.Trim();
                _dt.Ten_Doituong = txtTenDT.Text.Trim();
                if (!String.IsNullOrEmpty(txtSTT.Text.Trim()))
                    _dt.STT = Convert.ToInt32(txtSTT.Text.Trim());

                _dt.Ngaysua = DateTime.Now;
                _dt.Nguoitao = _user.UserID;
                _dt.Ngaytao = DateTime.Now;
                _dt.Nguoisua = _user.UserID;
                _return = _dtDAL.InsertT_Doituong(_dt);

                action.Thaotac = "[Sửa đối tượng]-->[Mã đối tượng:" + _return.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                lblMessError.Text = "";
                GVDoituong.EditIndex = -1;
                BindList_Doituong();
                BinQuytrinhtheoAnPham();
            }

            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
        }
        protected void GVDoituong_RowCancelingEdit(object source, GridViewCancelEditEventArgs e)
        {
            GVDoituong.EditIndex = -1;
            BindList_Doituong();
            BinQuytrinhtheoAnPham();

            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
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
                lblMessError.Text = "";
                action.Thaotac = "[Xóa đối tượng]-->[Mã đối tượng:" + _id.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
            }
            BindList_Doituong();
            BinQuytrinhtheoAnPham();

            UltilFunc.RunJavaScriptCode("$.fx.speeds._default = 10; var dlg = jQuery('#dialog').dialog({draggable: true,  resizable: true, hide: 'scale', modal: true, width: 500 }); dlg.parent().appendTo(jQuery('form:first'));");
        }
        #endregion



    }
}
