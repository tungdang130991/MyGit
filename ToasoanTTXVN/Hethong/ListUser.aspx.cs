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
    public partial class ListUser : BasePage
    {
        private bool _refreshState;
        private bool _isRefresh;
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] AllStates = (object[])savedState;
                base.LoadViewState(AllStates[0]);
                _refreshState = bool.Parse(AllStates[1].ToString());
                _isRefresh = _refreshState ==
                bool.Parse(Session["__ISREFRESH"].ToString());
            }
            catch
            { }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        private int userID
        {
            get { if (ViewState["userID"] != null) return Convert.ToInt32(ViewState["userID"]); else return 0; }

            set { ViewState["userID"] = value; }
        }
        private string _lang = "vi";
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
                    linkRoleCateSaves.Attributes.Add("onclick", "getIDSelect()");
                    btnAddMenu.Visible = _Role.R_Read;
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["culture"])))
                        _lang = Convert.ToString(Session["culture"]);
                    if (!IsPostBack)
                    {
                        Session["where_users"] = null;
                        UltilFunc.BindCombox(cbo_anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", CommonLib.ReadXML("lblChonanpham"));
                        UltilFunc.BindCombox(cbo_phongban, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1  Order by Ten_Phongban", CommonLib.ReadXML("lblChonphongban"));

                        cbo_anpham.SelectedValue = Global.DefaultCombobox;
                        LoadData(0);

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

        protected void cbo_anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAddEdit(false, false, false, true, false, false);

            this.roleChuyenMuc.Text = "Quyền chuyên mục của người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
            this.dgListCategorys.DataSource = _NguoidungDAL.BindGridT_UserCategoryByUser(userID, int.Parse(cbo_anpham.SelectedValue)).DefaultView;
            this.dgListCategorys.DataBind();

        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData(0);
        }
        public void Search_OnClick(object sender, EventArgs e)
        {
            LoadData(0);
        }
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hethong/EditUser.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void btn_ConvertPassword_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdListUser.Items)
            {
                int _ID = int.Parse(grdListUser.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                string Password = _NguoidungDAL.GetUserByUserName_ID(_ID).UserPass;
                if (_user.UserID != _NguoidungDAL.GetUserByUserName_ID(_ID).UserID)
                    _NguoidungDAL.ResetPass(FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Trim(), "sha1"), _ID);
            }
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
        public T_Users SetItem(int _userid)
        {
            T_Users _objGet = new T_Users();
            _objGet = _NguoidungDAL.GetUserByUserName_ID(_userid);
            T_Users _obj = new T_Users();
            _obj.UserID = _userid;
            _obj.UserName = _objGet.UserName;
            _obj.UserPass = _objGet.UserPass;
            _obj.UserEmail = _objGet.UserEmail;

            _obj.UserBirthday = _objGet.UserBirthday;
            _obj.UserAddress = _objGet.UserAddress;
            _obj.UserFullName = _objGet.UserFullName;
            _obj.UserMobile = _objGet.UserMobile;
            _obj.DateCreated = _objGet.DateCreated;
            _obj.DateModify = _objGet.DateModify;
            _obj.UserCreate = 0;
            _obj.IsLoai = 0;
            if (cbo_phongban.SelectedIndex > 0)
                _obj.ProvinceID = int.Parse(cbo_phongban.SelectedValue);
            else
                _obj.ProvinceID = 0;
            _obj.UserActive = _objGet.UserActive;
            _obj.RegionID = _objGet.RegionID;
            return _obj;
        }
        public void grdListUser_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                ImageButton btnqtbt = (ImageButton)e.Item.FindControl("btnqtbt");
                ImageButton btnUpdateRoom = (ImageButton)e.Item.FindControl("btnUpdateRoom");
                ImageButton btnGroup = (ImageButton)e.Item.FindControl("btnGroup");
                ImageButton btnRole = (ImageButton)e.Item.FindControl("btnRole");
                ImageButton btnRoleCategory = (ImageButton)e.Item.FindControl("btnRoleCategory");
                ImageButton btnIsReporter = (ImageButton)e.Item.FindControl("btnIsReporter");
                ImageButton btnResetPass = (ImageButton)e.Item.FindControl("btnResetPass");
                Label lblUserId = (Label)e.Item.FindControl("lblUserId");
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                if (lblUserId != null)
                {
                    if (int.Parse(lblUserId.Text) == 0)
                    {

                        btnqtbt.Visible = false;
                        btnUpdateRoom.Visible = false;
                        btnDelete.Visible = false;
                        btnGroup.Visible = false;
                        btnRole.Visible = false;
                        btnRoleCategory.Visible = false;
                        btnIsReporter.Visible = false;
                        btnResetPass.Visible = false;
                    }
                    else
                    {
                        btnqtbt.Visible = true;
                        btnUpdateRoom.Visible = true;
                        btnDelete.Visible = true;
                        btnGroup.Visible = true;
                        btnRole.Visible = true;
                        btnRoleCategory.Visible = true;
                        btnIsReporter.Visible = true;
                        btnResetPass.Visible = true;
                    }
                }
                if (btnEdit != null)
                    if (!_Role.R_Write)
                        btnEdit.Enabled = _Role.R_Write;
                if (btnDelete != null)
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Bạn có chắc chắn muốn xóa người dùng?');");

            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListUser_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;

            #endregion
            userID = Convert.ToInt32(grdListUser.DataKeys[e.Item.ItemIndex]);
            T_Users _obj = new T_Users();
            string _sql_update = string.Empty;
            Label lblmaphongban = (Label)e.Item.FindControl("lblmaphongban");
            int ma_phongban = 0;
            ma_phongban = int.Parse(lblmaphongban.Text);
            switch (e.CommandArgument.ToString().ToLower())
            {
                case "role":
                    SetAddEdit(false, true, false, false, false, false);

                    this.roleChucNang.Text = CommonLib.ReadXML("lblPhanquyenMenu") + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    this.gdListMenu.DataSource = _NguoidungDAL.BindGridMenuByUser(userID, _lang).DefaultView;
                    this.gdListMenu.DataBind();
                    action.Thaotac = "Phân quyền chức năng cho người dùng:" + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    break;
                case "updateroom":
                    if (ma_phongban == 0)
                    {
                        if (cbo_phongban.SelectedIndex == 0)
                        {
                            FuncAlert.AlertJS(this, "Bạn chưa chọn phòng ban!");
                            return;
                        }
                        else
                        {
                            _obj = SetItem(userID);
                            _NguoidungDAL.InsertT_Users(_obj);
                            _sql_update = "update T_Nguoidung set Ma_PhongBan=" + cbo_phongban.SelectedValue +
                                          " where Loai=0 and NguoiTao=" + userID;
                            ulti.ExecSql(_sql_update);
                            LoadData(1);
                        }
                    }
                    else
                    {

                        _obj = SetItem(userID);
                        _NguoidungDAL.InsertT_Users(_obj);
                        if (cbo_phongban.SelectedIndex > 0)
                        {
                            _sql_update = "update T_Nguoidung set Ma_PhongBan=" + cbo_phongban.SelectedValue + " where Loai=0 and NguoiTao=" + userID;
                        }
                        else
                        {
                            _sql_update = "update T_Nguoidung set Ma_PhongBan=0 where Loai=0 and NguoiTao=" + userID;
                        }
                        ulti.ExecSql(_sql_update);
                        LoadData(1);

                    }
                    break;
                case "editusers":
                    Response.Redirect("~/Hethong/EditUser.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + this.grdListUser.DataKeys[e.Item.ItemIndex].ToString());
                    break;
                case "qtbt":
                    action.Thaotac = "Phân quyền QTBT cho người dùng:" + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    SetAddEdit(false, false, false, false, false, true);
                    LabelQTBT.Text = CommonLib.ReadXML("lblPhanquyenQTBT") + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    this.DataGridQTBT.DataSource = _NguoidungDAL.BindGridT_NguoiDungQTBT(userID);
                    this.DataGridQTBT.DataBind();
                    break;
                case "group":
                    SetAddEdit(false, false, true, false, false, false);
                    this.lblThuocNhom.Text = CommonLib.ReadXML("lblPhanquyenThuocnhom") + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    DataBindGroup(userID);
                    break;
                case "rolengonngu":
                    action.Thaotac = "Phân quyền ngữ cho người dùng:" + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    SetAddEdit(false, false, false, false, true, false);
                    LabelRoleNgonngu.Text = CommonLib.ReadXML("lblPhanquyenNgonngu") + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    this.DataGridNgonNgu.DataSource = _NguoidungDAL.BindGridT_NguoiDungNgonNgu(userID);
                    this.DataGridNgonNgu.DataBind();
                    break;
                case "rolecategory":
                    SetAddEdit(false, false, false, true, false, false);
                    int MaAnPham = 0;
                    MaAnPham = int.Parse(cbo_anpham.SelectedValue.ToString());

                    this.roleChuyenMuc.Text = CommonLib.ReadXML("lblPhanquyenChuyenmuc") + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    this.dgListCategorys.DataSource = _NguoidungDAL.BindGridT_UserCategoryByUser(userID, MaAnPham).DefaultView;
                    this.dgListCategorys.DataBind();
                    action.Thaotac = "Phân quyền chuyên mục cho người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                    break;

                case "resetpass":

                    string pass = "123456";
                    _NguoidungDAL.ResetPass(FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Trim(), "sha1"), userID);
                    action.Thaotac = "[RESET PASS]-->[RESET PASS: Defaul: 123456] cho người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName + "[UserID:" + userID.ToString() + "]";
                    FuncAlert.AlertJS(this, CommonLib.ReadXML("lblResetmatkhauthanhcong"));
                    this.LoadData(0);
                    break;
                case "delete":

                    if (_NguoidungDAL.CheckDelete_users(userID))
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn không được xóa User đã được chọn tác giả cho tin bài xuất bản.!');", true);
                        return;
                    }
                    else
                    {
                        _NguoidungDAL.DeleteFromT_UserByID(userID);
                        action.Thaotac = "Thao tác xóa người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName + " [UserID:" + userID.ToString() + " ]";
                        SetAddEdit(true, false, false, false, false, false);
                        this.LoadData(0);
                    }
                    break;
                case "isreporter":
                    int _ID = Convert.ToInt32(this.grdListUser.DataKeys[e.Item.ItemIndex].ToString());
                    double _userCurrentID = _user.UserID;
                    if (_userCurrentID != _ID)
                    {
                        bool check = _NguoidungDAL.GetUserByUserName_ID(_ID).UserActive;
                        if (check)
                        {
                            _NguoidungDAL.ActiveUser(false, _ID);
                            action.Thaotac = "[Update UserActive T_Users]-->[T_Users:][UserActive:0] cho người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                        }
                        else
                        {
                            _NguoidungDAL.ActiveUser(true, _ID);
                            action.Thaotac = "[Update UserActive T_Users]-->[T_Users:][UserActive: 1]cho người dùng: " + _NguoidungDAL.GetUserByUserName_ID(userID).UserFullName;
                        }
                        SetAddEdit(true, false, false, false, false, false);
                        this.LoadData(0);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn không thể thao tác khi bạn đang đăng nhập!');", true);
                        return;
                    }
                    break;
                default:
                    Response.Redirect("~/Hethong/EditUser.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + this.grdListUser.DataKeys[e.Item.ItemIndex].ToString());
                    break;
            }
            action.Ma_Chucnang = int.Parse(Page.Request["Menu_ID"].ToString());
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

        }
        protected string IsStatusGet(string str)
        {
            string strReturn = "";
            if (str == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            if (str == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            return strReturn;
        }
        protected void DataBindGroup(int UserID)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            this.leftPane.DataSource = _usermenuDAL.GetAllFrom_T_GroupNotInUser("LeftPane", UserID);
            this.leftPane.DataBind();
            this.rightPane.DataSource = _usermenuDAL.GetAllFrom_T_GroupInUser("RightPane", UserID);
            this.rightPane.DataBind();
        }
        private void SetAddEdit(bool isList, bool isRole, bool isgroup, bool isRoleCategorys, bool isRoleLanguages, bool isRoleQTBT)
        {
            pnlList.Visible = isList;
            plRole.Visible = isRole;
            plGroup.Visible = isgroup;
            pnlRoleCategorys.Visible = isRoleCategorys;
            PnlNgonNgu.Visible = isRoleLanguages;
            PanelQTBT.Visible = isRoleQTBT;
        }
        public string SetNameMenu(object _menuName, object _menuID)
        {
            if (CommonLib.isParrentMenu(Convert.ToInt32(_menuID)))
            {
                return string.Format("<b>{0}", _menuName.ToString());
            }
            else
                return _menuName.ToString();
        }
        protected string StatusRoom(Object maphongban)
        {
            string strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (maphongban != DBNull.Value)
            {
                if (int.Parse(maphongban.ToString()) != 0)
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/mark.png";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            return strReturn;
        }
        private string Getphongban(int _status)
        {
            string _maphongban = "";
            DataTable _dt = new DataTable();
            string _where = " IsDeleted = 0 and UserActive=1 and ProvinceID<>0 and ProvinceID is not null";
            if (_status == 0)
            {
                if (cbo_phongban.SelectedIndex > 0)
                    _where += " and ProvinceID=" + cbo_phongban.SelectedValue;
            }
            _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    if (i == 0)
                        _maphongban = _dt.Rows[i]["ProvinceID"].ToString();
                    else
                        _maphongban += "," + _dt.Rows[i]["ProvinceID"].ToString();

                }
            }
            return _maphongban;
        }
        public void LoadData(int _status)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Ten_phongban", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("UserFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("UserName", typeof(string)));
            dt.Columns.Add(new DataColumn("UserActive", typeof(bool)));
            dt.Columns.Add(new DataColumn("Maphongban", typeof(string)));

            HPCBusinessLogic.DAL.PhongBan_DAL _dalPB = new HPCBusinessLogic.DAL.PhongBan_DAL();

            int TotalRecords = 0;


            string where = " IsDeleted = 0 ";
            if (checkactive.Checked)
                where += " and UserActive=1";
            if (!checkactive.Checked)
                where += " and UserActive=0";
            if (!String.IsNullOrEmpty(txtSearch_UserName.Text.Trim()))
                where += " AND " + string.Format(" UserName like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_UserName.Text.Trim()));
            if (!String.IsNullOrEmpty(txt_userfullname.Text.Trim()))
                where += " AND " + string.Format(" UserFullName like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_userfullname.Text.Trim()));

            SSOLibDAL _DalSSO = new SSOLibDAL();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable _dtphongban = new DataTable();


            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new HPCBusinessLogic.NguoidungDAL();
            DataSet _ds = new DataSet();
            DataTable _dt = new DataTable();



            string wherePB = "";
            if (Getphongban(_status).Length > 0)
                wherePB += " Ma_Phongban in (" + Getphongban(_status) + ")";
            else
                wherePB += " 1=0 ";

            string sqlphongban = "select * from t_phongban where" + wherePB + " order by Ten_Phongban";
            _dtphongban = ulti.ExecSqlDataSet(sqlphongban).Tables[0];
            if (_dtphongban != null && _dtphongban.Rows.Count > 0)
            {
                ViewState["pageindex"] = null;
                for (int k = 0; k < _dtphongban.Rows.Count; k++)
                {

                    if (Session["where_users"] != null)
                        _dt = _NguoidungDAL.GetT_User_Dynamic(Session["where_users"].ToString() + " and (ProvinceID=" + _dtphongban.Rows[k]["Ma_Phongban"].ToString() + ")").Tables[0];
                    else
                        _dt = _NguoidungDAL.GetT_User_Dynamic(where + " and (ProvinceID=" + _dtphongban.Rows[k]["Ma_Phongban"].ToString() + ")").Tables[0];

                    TotalRecords = TotalRecords + _dt.Rows.Count;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {

                        dr = dt.NewRow();

                        dr[0] = _dtphongban.Rows[k]["Ten_phongban"].ToString();
                        dr[1] = "0";
                        dr[4] = false;
                        dr[5] = "0";
                        dt.Rows.Add(dr);
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            dr = dt.NewRow();

                            dr[1] = _dt.Rows[j]["UserID"].ToString();
                            dr[2] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dt.Rows[j]["UserFullName"].ToString();
                            dr[3] = _dt.Rows[j]["UserName"].ToString();
                            dr[4] = bool.Parse(_dt.Rows[j]["UserActive"].ToString());
                            dr[5] = _dt.Rows[j]["ProvinceID"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }

                }
            }

            if (Session["where_users"] != null)
                _dt = _NguoidungDAL.GetT_User_Dynamic(Session["where_users"].ToString() + " and (ProvinceID=0 or ProvinceID is null)").Tables[0];
            else
                _dt = _NguoidungDAL.GetT_User_Dynamic(where + " and (ProvinceID=0 or ProvinceID is null)").Tables[0];

            TotalRecords = TotalRecords + _dt.Rows.Count;
            int totaltrang = TotalRecords / Global.MembersPerPage;
            if (TotalRecords % Global.MembersPerPage > 0)
                totaltrang++;
            if (_dt != null && _dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[1] = _dt.Rows[i]["UserID"].ToString();
                    dr[2] = _dt.Rows[i]["UserFullName"].ToString();
                    dr[3] = _dt.Rows[i]["UserName"].ToString();
                    dr[4] = bool.Parse(_dt.Rows[i]["UserActive"].ToString());
                    dr[5] = _dt.Rows[i]["ProvinceID"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            grdListUser.DataSource = dt;
            grdListUser.DataBind();
            lbltotalrecord.Text = "Bản ghi: " + TotalRecords + "/Trang: " + totaltrang;

        }
        public void LoadDataUserWithPhongban(int _status)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Ten_phongban", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("UserFullName", typeof(string)));
            dt.Columns.Add(new DataColumn("UserName", typeof(string)));
            dt.Columns.Add(new DataColumn("UserActive", typeof(bool)));
            dt.Columns.Add(new DataColumn("Maphongban", typeof(string)));

            HPCBusinessLogic.DAL.PhongBan_DAL _dalPB = new HPCBusinessLogic.DAL.PhongBan_DAL();

            int TotalRecords = 0;


            string where = " IsDeleted = 0 ";
            if (checkactive.Checked)
                where += " and UserActive=1";
            if (!checkactive.Checked)
                where += " and UserActive=0";
            if (!String.IsNullOrEmpty(txtSearch_UserName.Text.Trim()))
                where += " AND " + string.Format(" UserName like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_UserName.Text.Trim()));
            if (!String.IsNullOrEmpty(txt_userfullname.Text.Trim()))
                where += " AND " + string.Format(" UserFullName like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_userfullname.Text.Trim()));

            SSOLibDAL _DalSSO = new SSOLibDAL();
            UltilFunc _untilDAL = new UltilFunc();
            DataTable _dtphongban = new DataTable();


            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new HPCBusinessLogic.NguoidungDAL();
            DataSet _ds = new DataSet();
            DataTable _dt = new DataTable();



            string wherePB = "";
            if (Getphongban(_status).Length > 0)
                wherePB += " Ma_Phongban in (" + Getphongban(_status) + ")";
            else
                wherePB += " 1=0 ";

            string sqlphongban = "select * from t_phongban where" + wherePB + " order by Ten_Phongban";
            _dtphongban = ulti.ExecSqlDataSet(sqlphongban).Tables[0];
            if (_dtphongban != null && _dtphongban.Rows.Count > 0)
            {
                ViewState["pageindex"] = null;
                for (int k = 0; k < _dtphongban.Rows.Count; k++)
                {

                    if (Session["where_users"] != null)
                        _dt = _NguoidungDAL.GetT_User_Dynamic(Session["where_users"].ToString() + " and (ProvinceID=" + _dtphongban.Rows[k]["Ma_Phongban"].ToString() + ")").Tables[0];
                    else
                        _dt = _NguoidungDAL.GetT_User_Dynamic(where + " and (ProvinceID=" + _dtphongban.Rows[k]["Ma_Phongban"].ToString() + ")").Tables[0];

                    TotalRecords = TotalRecords + _dt.Rows.Count;
                    if (_dt != null && _dt.Rows.Count > 0)
                    {

                        dr = dt.NewRow();

                        dr[0] = _dtphongban.Rows[k]["Ten_phongban"].ToString();
                        dr[1] = "0";
                        dr[4] = false;
                        dr[5] = "0";
                        dt.Rows.Add(dr);
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            dr = dt.NewRow();

                            dr[1] = _dt.Rows[j]["UserID"].ToString();
                            dr[2] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _dt.Rows[j]["UserFullName"].ToString();
                            dr[3] = _dt.Rows[j]["UserName"].ToString();
                            dr[4] = bool.Parse(_dt.Rows[j]["UserActive"].ToString());
                            dr[5] = _dt.Rows[j]["ProvinceID"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }


                }
            }
            int totaltrang = TotalRecords / Global.MembersPerPage;
            if (TotalRecords % Global.MembersPerPage > 0)
                totaltrang++;
            grdListUser.DataSource = dt;
            grdListUser.DataBind();
            lbltotalrecord.Text = "Bản ghi: " + TotalRecords + "/Trang: " + totaltrang;

        }

        #region Tim KIEM
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            if (cbo_phongban.SelectedIndex == 0)
            {
                string where = " IsDeleted = 0 ";
                if (checkactive.Checked)
                    where += " and UserActive=1";
                if (!checkactive.Checked)
                    where += " and UserActive=0";
                if (!String.IsNullOrEmpty(txtSearch_UserName.Text.Trim()))
                    where += " AND " +
                             string.Format(" UserName like N'%{0}%'",
                                           UltilFunc.SqlFormatText(this.txtSearch_UserName.Text.Trim()));
                if (!String.IsNullOrEmpty(txt_userfullname.Text.Trim()))
                    where += " AND " +
                             string.Format(" UserFullName like N'%{0}%'",
                                           UltilFunc.SqlFormatText(this.txt_userfullname.Text.Trim()));

                Session["where_users"] = where;

                LoadData(0);
            }
            else
            {
                LoadDataUserWithPhongban(0);
            }

        }
        #endregion

        #region Roll QTBT
        protected void LinkButtonApplyQTBT_Click(object sender, EventArgs e)
        {
            _NguoidungDAL.DeleteFromT_Nguoidung_QTBTDynamic(" Ma_Nguoidung=" + userID + " AND (Ma_Nhom =0 or Ma_Nhom is null)");
            foreach (DataGridItem m_Item in DataGridQTBT.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");

                if (chk_Select != null && chk_Select.Checked && chk_Select.Enabled == true)
                {
                    int QTBT_ID = Convert.ToInt32(this.DataGridQTBT.DataKeys[m_Item.ItemIndex].ToString());
                    _NguoidungDAL.SP_InsertT_Nguoidung_QTBT(userID, QTBT_ID, 0);
                }
            }
            this.DataGridQTBT.DataSource = _NguoidungDAL.BindGridT_NguoiDungQTBT(userID);
            this.DataGridQTBT.DataBind();

        }
        #endregion

        #region Phan Quyen Nhom
        ArrayList lasset = new ArrayList();
        ArrayList lsubordinate = new ArrayList();
        protected void btAddAll_Click(object sender, EventArgs e)
        {
            if (userID > 0)
            {
                while (leftPane.Items.Count != 0)
                {
                    for (int i = 0; i < leftPane.Items.Count; i++)
                    {

                        if (!lasset.Contains(leftPane.Items[i]))
                        {
                            lasset.Add(leftPane.Items[i]);
                        }
                    }

                    for (int i = 0; i < lasset.Count; i++)
                    {
                        if (!rightPane.Items.Contains(((ListItem)lasset[i])))
                        {
                            rightPane.Items.Add(((ListItem)lasset[i]));
                        }
                        leftPane.Items.Remove(((ListItem)lasset[i]));
                    }
                }
            }
        }
        protected void btRemoveAll_Click(object sender, EventArgs e)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            if (userID > 0)
            {
                while (rightPane.Items.Count != 0)
                {
                    for (int i = 0; i < rightPane.Items.Count; i++)
                    {
                        if (!lsubordinate.Contains(rightPane.Items[i]))
                        {
                            lsubordinate.Add(rightPane.Items[i]);
                        }
                    }
                    for (int i = 0; i < lsubordinate.Count; i++)
                    {
                        if (!leftPane.Items.Contains(((ListItem)lsubordinate[i])))
                        {
                            leftPane.Items.Add(((ListItem)lsubordinate[i]));
                        }
                        rightPane.Items.Remove(((ListItem)lsubordinate[i]));
                    }
                }
                _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");

                _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");

            }
        }
        protected void btAddOne_Click(object sender, EventArgs e)
        {
            if (userID > 0)
            {
                if (leftPane.SelectedIndex != -1)
                {
                    ListItem selectedItem = leftPane.SelectedItem;
                    selectedItem.Selected = false;
                    leftPane.Items.Remove(selectedItem);
                    rightPane.Items.Add(selectedItem);
                }
            }

        }
        protected void btRemoveOne_Click(object sender, EventArgs e)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL(); ;
            if (userID > 0)
            {
                ArrayList modules = _usermenuDAL.GetAllFrom_T_GroupInUser("RightPane", userID);
                if (rightPane.SelectedIndex != -1)
                {
                    ListItem selectedItem = rightPane.SelectedItem;

                    _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom=" + int.Parse(rightPane.SelectedValue));

                    _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom=" + int.Parse(rightPane.SelectedValue));

                    selectedItem.Selected = false;
                    rightPane.Items.Remove(selectedItem);
                    leftPane.Items.Add(selectedItem);
                }
            }

        }
        protected void linkGroupExit_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false, false, false, false, false);

        }
        protected void linkGroupApplly_Click(object sender, EventArgs e)
        {
            if (userID > 0)
            {
                if (rightPane.Items.Count > 0)
                {
                    for (int i = 0; i < rightPane.Items.Count; i++)
                    {
                        if (!lsubordinate.Contains(rightPane.Items[i]))
                        {
                            lsubordinate.Add(rightPane.Items[i]);
                        }

                    }
                    UpdateUser_Group(lsubordinate);
                }
                else
                    UpdateUser_Group(lsubordinate);

            }
            SetAddEdit(true, false, false, false, false, false);

        }
        protected void SaveGroupByMenuID(Int32 User_ID, Int32 Group_ID)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL(); ;
            DataTable _dt = new DataTable();
            DataTable _dtCate = new DataTable();
            DataTable _dtLang = new DataTable();
            DataTable _dtqtbt = new DataTable();
            try
            {
                _dt = _usermenuDAL.GetT_GroupMenuDynamic(Group_ID);
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        bool _R_Edit = false;
                        bool _R_Del = false;
                        bool _R_Add = false;

                        if (_dt.Rows[i]["Doc"] != DBNull.Value)
                            _R_Edit = Convert.ToBoolean(_dt.Rows[i]["Doc"]);
                        if (_dt.Rows[i]["Ghi"] != DBNull.Value)
                            _R_Del = Convert.ToBoolean(_dt.Rows[i]["Ghi"]);
                        if (_dt.Rows[i]["Xoa"] != DBNull.Value)
                            _R_Add = Convert.ToBoolean(_dt.Rows[i]["Xoa"]);

                        _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_ChucNang=" + Convert.ToInt32(_dt.Rows[i]["Ma_ChucNang"]) + " and Ma_Nhom = " + Group_ID);
                        _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_ChucNang=" + Convert.ToInt32(_dt.Rows[i]["Ma_ChucNang"]) + " and Ma_Nhom <>0");
                        _usermenuDAL.InsertT_UserMenu(User_ID, Convert.ToInt32(_dt.Rows[i]["Ma_ChucNang"]), _R_Edit, _R_Del, _R_Add, Group_ID);
                    }
                }

                _dtCate = _usermenuDAL.GetT_GroupCategoryDynamic(Group_ID);
                if (_dtCate.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtCate.Rows.Count; i++)
                    {
                        _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_chuyenmuc=" + Convert.ToInt32(_dtCate.Rows[i]["Ma_chuyenmuc"]) + " AND Ma_Nhom = " + Group_ID);
                        _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_chuyenmuc=" + Convert.ToInt32(_dtCate.Rows[i]["Ma_chuyenmuc"]) + " AND Ma_Nhom <>0 ");
                        _usermenuDAL.InsertT_UserCategory(User_ID, Convert.ToInt32(_dtCate.Rows[i]["Ma_chuyenmuc"]), Group_ID);
                    }
                }

                _dtLang = _usermenuDAL.GetT_GroupLanguagesDynamic(Group_ID);
                if (_dtLang.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtLang.Rows.Count; i++)
                    {
                        _usermenuDAL.DeleteFromT_UserLanguageDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_Ngonngu=" + Convert.ToInt32(_dtLang.Rows[i]["Ma_Ngonngu"]) + " AND Ma_Nhom = " + Group_ID);
                        _usermenuDAL.DeleteFromT_UserLanguageDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_Ngonngu=" + Convert.ToInt32(_dtLang.Rows[i]["Ma_Ngonngu"]) + " AND Ma_Nhom <>0 ");
                        _usermenuDAL.InsertT_UserLanguages(User_ID, Convert.ToInt32(_dtLang.Rows[i]["Ma_Ngonngu"]), Group_ID);
                    }
                }
                _dtqtbt = _NguoidungDAL.GetT_Nhom_QTBTDynamic(Group_ID);
                if (_dtqtbt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtqtbt.Rows.Count; i++)
                    {
                        _usermenuDAL.DeleteFromT_Nguoidung_QTBTDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_QTBT=" + Convert.ToInt32(_dtqtbt.Rows[i]["Ma_QTBT"]) + " AND Ma_Nhom = " + Group_ID);
                        _usermenuDAL.DeleteFromT_Nguoidung_QTBTDynamic(" Ma_Nguoidung = " + User_ID + " AND Ma_QTBT=" + Convert.ToInt32(_dtqtbt.Rows[i]["Ma_QTBT"]) + " AND Ma_Nhom <>0 ");
                        _usermenuDAL.SP_InsertT_Nguoidung_QTBT(User_ID, Convert.ToInt32(_dtqtbt.Rows[i]["Ma_QTBT"]), Group_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateUser_Group(ArrayList _item)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            _usermenuDAL.DeleteT_UserGroups(userID);
            if (lsubordinate.Count > 0)
            {
                _NguoidungDAL.DeleteFromT_Nguoidung_QTBTDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <>0 ");
                _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom<>0");
                _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");
                _usermenuDAL.DeleteFromT_UserLanguageDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");
                for (int i = 0; i < _item.Count; i++)
                {
                    ListItem _it = (ListItem)_item[i];

                    _usermenuDAL.InsertT_UserGroups(userID, int.Parse(_it.Value));
                    SaveGroupByMenuID(userID, Convert.ToInt32(_it.Value));
                }
            }
            else
            {
                _NguoidungDAL.DeleteFromT_Nguoidung_QTBTDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <>0 ");
                _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");
                _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");
                _usermenuDAL.DeleteFromT_UserLanguageDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom <> 0");
            }
        }

        #endregion

        #region Roll Language
        protected void btnApplyRoleLanguage_Click(object sender, EventArgs e)
        {
            _NguoidungDAL.DeleteFromT_UserLanguageDynamic(" Ma_Nguoidung=" + userID + " AND Ma_Nhom =0");
            foreach (DataGridItem m_Item in DataGridNgonNgu.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");

                if (chk_Select != null && chk_Select.Checked && chk_Select.Enabled == true)
                {
                    int LangID = Convert.ToInt32(this.DataGridNgonNgu.DataKeys[m_Item.ItemIndex].ToString());
                    _NguoidungDAL.InsertT_UserLanguages(userID, LangID, 0);
                }
            }
            SetAddEdit(true, false, false, false, false, false);

        }
        #endregion

        #region Phan quyen chuyen muc

        protected void linkRoleCateExit_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false, false, false, false, false);
        }
        protected void linkRoleCateSaves_Click(object sender, EventArgs e)
        {
            Save_UserCategory(userID);
            SetAddEdit(true, false, false, false, false, false);

        }
        private bool checkexits_usercate(int userid, int Ma_cate)
        {
            DataTable _dt = new DataTable();
            string _sql = "select Ma_chuyenmuc,Ma_Nguoidung from T_Nguoidung_Chuyenmuc where Ma_Nguoidung=" + userid + " and Ma_chuyenmuc=" + Ma_cate;
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        private void Save_UserCategory(int User_ID)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            ChuyenmucDAL _ChuyenmucDAL = new ChuyenmucDAL();
            T_ChuyenMuc _objcm = new T_ChuyenMuc();            

            foreach (DataGridItem _item in dgListCategorys.Items)
            {
                HtmlInputCheckBox optSelect = (HtmlInputCheckBox)_item.FindControl("inputcheckbox");
                int _CateID = int.Parse(dgListCategorys.DataKeys[_item.ItemIndex].ToString());
                if (optSelect != null && optSelect.Checked)
                {
                    _usermenuDAL.DeleteFromT_UserCategoryDynamic("Ma_Nguoidung=" + User_ID + " and Ma_chuyenmuc=" + _CateID + " AND Ma_Nhom=0");
                    if (!checkexits_usercate(User_ID, _CateID))
                    {
                        if (_ChuyenmucDAL.GetOneFromT_ChuyenmucByID(_CateID) != null)
                            _objcm = _ChuyenmucDAL.GetOneFromT_ChuyenmucByID(_CateID);
                        else
                            _objcm = null;
                        if (_objcm.Ma_Chuyenmuc_Cha > 0)
                        {
                            // insert chuyen muc con
                            _usermenuDAL.InsertT_UserCategory(User_ID, _CateID, 0);
                            // insert chuyen muc cha
                            _usermenuDAL.DeleteFromT_UserCategoryDynamic("Ma_Nguoidung=" + User_ID + " and Ma_chuyenmuc=" + _objcm.Ma_Chuyenmuc_Cha + " AND Ma_Nhom=0");
                            _usermenuDAL.InsertT_UserCategory(User_ID, _objcm.Ma_Chuyenmuc_Cha, 0);

                        }
                        else
                        {  
                            _usermenuDAL.InsertT_UserCategory(User_ID, _CateID, 0);
                        }
                    }
                }
                else
                    _usermenuDAL.DeleteFromT_UserCategoryDynamic("Ma_Nguoidung=" + User_ID + " and Ma_chuyenmuc=" + _CateID + " AND Ma_Nhom=0");
                
            }
        }

        #endregion

        #region PHAN QUYEN CHUC NANG NGUOI DUNG
        public void gdListMenu_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            CheckBox chk_Select = (CheckBox)e.Item.FindControl("optSelect");
            if (chk_Select != null)
            {
                int temp = e.Item.ItemIndex + 2;
                chk_Select.InputAttributes.Add("value", "" + temp + "");
                chk_Select.Attributes.Add("onclick", "return ChkParrent(this,this.value);");
            }
        }
        protected void btnApplyRole_Click(object sender, EventArgs e)
        {
            Save_UserMenu(userID);
            SetAddEdit(true, false, false, false, false, false);
        }
        private bool checkexits_UserMenu(int userid, int Ma_Menu)
        {
            DataTable _dt = new DataTable();
            string _sql = "select Ma_Nhom from T_Nguoidung_Chucnang where Ma_Nguoidung=" + userid + " and Ma_ChucNang=" + Ma_Menu;
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        private void Save_UserMenu(int User_ID)
        {
            bool Doc = false, Ghi = false, Xoa = false;
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            T_Chucnang _objchucnang = new T_Chucnang();
            ChucnangDAL _dalchucnang = new ChucnangDAL();
            _usermenuDAL.DeleteFromT_UserMenuDynamic("Ma_Nguoidung=" + User_ID + " AND Ma_Nhom=0");

            foreach (DataGridItem m_Item in gdListMenu.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                CheckBox _Doc = (CheckBox)m_Item.FindControl("chkR_Add");
                CheckBox _Ghi = (CheckBox)m_Item.FindControl("chkR_Edit");
                CheckBox _Xoa = (CheckBox)m_Item.FindControl("chkR_Del");

                if (chk_Select != null && chk_Select.Checked && chk_Select.Enabled == true)
                {
                    int Menu_ID = Convert.ToInt32(this.gdListMenu.DataKeys[m_Item.ItemIndex].ToString());
                    if (!checkexits_UserMenu(User_ID, Menu_ID))
                    {
                        if (_dalchucnang.GetOneFromT_ChucnangByID(Menu_ID) != null)
                            _objchucnang = _dalchucnang.GetOneFromT_ChucnangByID(Menu_ID);
                        else
                            _objchucnang = null;
                        if (_Doc != null && _Doc.Checked) Doc = true;
                        else Doc = false;
                        if (_Ghi != null && _Ghi.Checked) Ghi = true;
                        else Ghi = false;
                        if (_Xoa != null && _Xoa.Checked) Xoa = true;
                        else Xoa = false;
                        if (_objchucnang.Ma_Chucnang_Cha > 0)
                        {
                            _usermenuDAL.InsertT_UserMenu(userID, Menu_ID, Doc, Ghi, Xoa, 0);
                            if (_objchucnang != null && _objchucnang.Ma_Chucnang_Cha != 0)
                            {
                                _usermenuDAL.DeleteFromT_UserMenuDynamic("Ma_Nguoidung=" + User_ID + " and Ma_ChucNang=" + _objchucnang.Ma_Chucnang_Cha + " AND Ma_Nhom=0");
                                _usermenuDAL.InsertT_UserMenu(userID, _objchucnang.Ma_Chucnang_Cha, Doc, Ghi, Xoa, 0);
                            }
                            else
                            {
                                FuncAlert.AlertJS(this, "chức năng không tồn tại!");
                                return;
                            }
                        }
                        else
                            _usermenuDAL.InsertT_UserMenu(userID, Menu_ID, Doc, Ghi, Xoa, 0);
                    }
                }
            }
        }
        protected void btnCancelRole_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false, false, false, false, false);
        }
        #endregion


    }
}
