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
    public partial class ListGroup : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        HPCBusinessLogic.DAL.NhomDAL _nhomnguoidungDAL = new HPCBusinessLogic.DAL.NhomDAL();
        private int manhom
        {
            get { if (ViewState["manhom"] != null) return Convert.ToInt32(ViewState["manhom"]); else return 0; }

            set { ViewState["manhom"] = value; }
        }
        public string _lang = "vi";
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
                    linkAddNews.Visible = _Role.R_Read;
                    linkRoleCateSaves.Attributes.Add("onclick", "getIDSelect()");
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["culture"])))
                        _lang = Convert.ToString(Session["culture"]);
                    if (!IsPostBack)
                    {
                        UltilFunc.BindCombox(cbo_anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", "Chọn ấn phẩm");

                        cbo_anpham.SelectedValue = Global.DefaultCombobox;
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
        protected bool StatusDisplay(Object _manguoidung)
        {
            if (int.Parse(_manguoidung.ToString()) == 0)
                return true;
            else
                return false;
        }
        protected void BindData()
        {
            string where = "1=1";
            if (!String.IsNullOrEmpty(txtTenNhom.Text.Trim()))
                where += "AND " + string.Format(" Ten_Nhom like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTenNhom.Text.Trim()));

            pages.PageSize = Global.MembersPerPage;
            DataSet _ds;
            _ds = _nhomnguoidungDAL.BindGridT_Nhom(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _nhomnguoidungDAL.BindGridT_Nhom(pages.PageIndex - 1, pages.PageSize, where);
            DataTable _dtgroup = _ds.Tables[0];
            _dtgroup = LoadDataUserWithGroup(_dtgroup);
            grdListNhom.DataSource = _dtgroup;// _ds.Tables[0].DefaultView;
            grdListNhom.DataBind();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        public DataTable LoadDataUserWithGroup(DataTable _dtgroup)
        {
            UltilFunc _ulti = new UltilFunc();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Ma_nhom", typeof(int)));
            dt.Columns.Add(new DataColumn("Ten_nhom", typeof(string)));
            dt.Columns.Add(new DataColumn("Userfullname", typeof(string)));
            dt.Columns.Add(new DataColumn("Username", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("Dem", typeof(int)));
            DataTable _dt = new DataTable();
            string sqlselect = "";
           
            if (_dtgroup != null && _dtgroup.Rows.Count > 0)
            {
                for (int i = 0; i < _dtgroup.Rows.Count; i++)
                {
                    sqlselect = "select * from T_Nguoidung_Nhom where Ma_Nhom=" + _dtgroup.Rows[i]["Ma_nhom"].ToString();
                    _dt = _ulti.ExecSqlDataSet(sqlselect).Tables[0];
                    dr = dt.NewRow();
                    dr[0] = int.Parse(_dtgroup.Rows[i]["Ma_nhom"].ToString());
                    dr[1] = _dtgroup.Rows[i]["Ten_nhom"].ToString();
                    dr[4] = "0";
                    dr[5] = _dt.Rows.Count;
                    dt.Rows.Add(dr);

                    
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            dr = dt.NewRow();
                            dr[0] = int.Parse(_dt.Rows[j]["Ma_Nhom"].ToString());
                            dr[2] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + HPCBusinessLogic.UltilFunc.GetUserFullName(_dt.Rows[j]["Ma_Nguoidung"].ToString());
                            dr[3] = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + HPCBusinessLogic.UltilFunc.GetUserByUserName_ID(int.Parse(_dt.Rows[j]["Ma_Nguoidung"].ToString()));
                            dr[4] = _dt.Rows[j]["Ma_Nguoidung"].ToString();                            
                            dt.Rows.Add(dr);
                        }

                    }


                }
            }

            return dt;
        }
        protected void cbo_anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAddEdit(false, false, true, false, false);

            lblRoleChuyenMuc.Text = "Quyền chuyên mục của nhóm người dùng: " + _nhomnguoidungDAL.GetOneFromT_NhomByID(manhom).Ten_Nhom;
            this.dgCategory.DataSource = _nhomnguoidungDAL.BindGridCategoryByGroup(manhom, int.Parse(cbo_anpham.SelectedValue)).DefaultView;
            dgCategory.DataBind();

        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void linkSearch_OnClick(object sender, EventArgs e)
        {
            BindData();
        }
        public void linkAddNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hethong/EditGroup.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
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
        private void SetAddEdit(bool isList, bool isRole, bool isRoleCate, bool isRoleLang, bool isQTBT)
        {
            pnlList.Visible = isList;
            plRole.Visible = isRole;
            pnlRoleCategorys.Visible = isRoleCate;
            PnlNgonNgu.Visible = isRoleLang;
            PanelGroupQTBT.Visible = isQTBT;
        }
        public void grdListNhom_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            manhom = Convert.ToInt32(this.grdListNhom.DataKeys[e.Item.ItemIndex]);
            if (e.CommandArgument.ToString().ToLower() == "role")
            {
                SetAddEdit(false, true, false, false, false);

                this.roleChucNang.Text = CommonLib.ReadXML("lblPhanquyenchonhomnguoidung") + _nhomnguoidungDAL.GetOneFromT_NhomByID(manhom).Ten_Nhom;
                gdListMenu.DataSource = _nhomnguoidungDAL.BindGridMenuByGroup(manhom, _lang).DefaultView;
                gdListMenu.DataBind();
            }
            if (e.CommandArgument.ToString().ToLower() == "roleqtbt")
            {
                SetAddEdit(false, false, false, false, true);

                this.LabelRoleQTBT.Text = CommonLib.ReadXML("lblPhanquyenchonhomnguoidung") + _nhomnguoidungDAL.GetOneFromT_NhomByID(manhom).Ten_Nhom;
                DataGridGroupQTBT.DataSource = _nhomnguoidungDAL.BindGridGroupQTBT(manhom).DefaultView;
                DataGridGroupQTBT.DataBind();
            }
            if (e.CommandArgument.ToString().ToLower() == "rolelanguage")
            {
                SetAddEdit(false, false, false, true, false);

                this.LabelRoleNgonngu.Text = CommonLib.ReadXML("lblPhanquyenchonhomnguoidung") + _nhomnguoidungDAL.GetOneFromT_NhomByID(manhom).Ten_Nhom;
                DataGridNgonNgu.DataSource = _nhomnguoidungDAL.BindGridGroupLanguages(manhom).DefaultView;
                DataGridNgonNgu.DataBind();
            }
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                Response.Redirect("~/Hethong/EditGroup.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + manhom.ToString());
            }
            if (e.CommandArgument.ToString().ToLower() == "btnrolecate")
            {
                SetAddEdit(false, false, true, false, false);

                lblRoleChuyenMuc.Text = CommonLib.ReadXML("lblPhanquyenchonhomnguoidung") + _nhomnguoidungDAL.GetOneFromT_NhomByID(manhom).Ten_Nhom;
                this.dgCategory.DataSource = _nhomnguoidungDAL.BindGridCategoryByGroup(manhom, int.Parse(cbo_anpham.SelectedValue)).DefaultView;
                dgCategory.DataBind();
            }

            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                UltilFunc ulti = new UltilFunc();
                _nhomnguoidungDAL.DeleteFromT_NhomByID(manhom);
                string sql_T_Nguoidung_Nhom = "delete from T_Nguoidung_Nhom where Ma_Nhom=" + manhom;
                ulti.ExecSql(sql_T_Nguoidung_Nhom);
                string T_Nguoidung_Chucnang = "delete from T_Nguoidung_Chucnang  where Ma_Nhom=" + manhom;
                ulti.ExecSql(T_Nguoidung_Chucnang);
                string sql_T_Nguoidung_Chuyenmuc = "delete from T_Nguoidung_Chuyenmuc  where Ma_Nhom=" + manhom;
                ulti.ExecSql(sql_T_Nguoidung_Chuyenmuc);
                string sql_T_Nhom_Chucnang = "delete from T_Nhom_Chucnang where Ma_Nhom=" + manhom;
                ulti.ExecSql(sql_T_Nhom_Chucnang);
                string sql_T_Nhom_Chuyenmuc = "delete from T_Nhom_Chuyenmuc where Ma_Nhom=" + manhom;
                ulti.ExecSql(sql_T_Nhom_Chuyenmuc);
                action.Thaotac = "[Xóa GroupID]-->[Thao tác Xóa Trong Bảng T_GROUP][GroupID:" + manhom.ToString() + " ]";
                SetAddEdit(true, false, false, false, false);
                BindData();
            }
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
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
        public void grdListNhom_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                int ma_nhom = 0;
                Label lbluserid = (Label)e.Item.FindControl("lbluserid");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                ImageButton btnDeleteAll = (ImageButton)e.Item.FindControl("btnDelete");
                ImageButton btnqtbt = (ImageButton)e.Item.FindControl("btnqtbt");
                ImageButton btnRole = (ImageButton)e.Item.FindControl("btnRole");
                ImageButton btnRoleCate = (ImageButton)e.Item.FindControl("btnRoleCate");
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");

                if (lbluserid != null)
                {
                    ma_nhom = int.Parse(lbluserid.Text);
                    if (ma_nhom != 0)
                    {
                        btnEdit.Visible = false;
                        btnqtbt.Visible = false;
                        btnRole.Visible = false;
                        btnRoleCate.Visible = false;
                        btnDelete.Visible = false;
                    }
                    else
                    {
                        btnEdit.Visible = true;
                        btnqtbt.Visible = true;
                        btnRole.Visible = true;
                        btnRoleCate.Visible = true;
                        btnDelete.Visible = true;

                    }
                }
                if (btnEdit != null)
                    if (!_Role.R_Write)
                        btnEdit.Enabled = _Role.R_Write;
                if (btnDeleteAll != null)
                    if (!_Role.R_Delete)
                        btnDeleteAll.Enabled = _Role.R_Delete;
                    else
                        btnDeleteAll.Attributes.Add("onclick", "return confirm('Bạn có chắc chắn muốn xóa nhóm người dùng?');");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void gdListMenu_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void linkGroupSave_Click(object sender, EventArgs e)
        {
            Save_GroupMenu(manhom);
            SetAddEdit(true, false, false, false, false);
        }
        public void LinkGroupExit_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false, false, false, false);
        }
        private void Save_GroupMenu(int Group_ID)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            UltilFunc Ulti = new UltilFunc();
            bool R_Edit = false, R_Del = false, R_Add = false;
            T_Chucnang _objchucnang = new T_Chucnang();
            ChucnangDAL _dalchucnang = new ChucnangDAL();

            _nhomnguoidungDAL.XoaChucnangNhomNguoidung(Group_ID);
            _usermenuDAL.DeleteFromT_UserMenuDynamic(" Ma_Nhom=" + Group_ID);
            foreach (DataGridItem m_Item in gdListMenu.Items)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk_Select = (HtmlInputCheckBox)m_Item.FindControl("optSelect");

                HtmlInputCheckBox chkR_Add = (HtmlInputCheckBox)m_Item.FindControl("chkR_Add");
                HtmlInputCheckBox chkR_Edit = (HtmlInputCheckBox)m_Item.FindControl("chkR_Edit");
                HtmlInputCheckBox chkR_Del = (HtmlInputCheckBox)m_Item.FindControl("chkR_Del");
                HtmlInputCheckBox chkR_Pub = (HtmlInputCheckBox)m_Item.FindControl("chkR_Pub");
                if (chk_Select != null && chk_Select.Checked)
                {
                    int Menu_ID = Convert.ToInt32(this.gdListMenu.DataKeys[m_Item.ItemIndex].ToString());
                    _objchucnang = _dalchucnang.GetOneFromT_ChucnangByID(Menu_ID);
                    if (chkR_Add != null && chkR_Add.Checked) R_Add = true;
                    else R_Add = false;
                    if (chkR_Edit != null && chkR_Edit.Checked) R_Edit = true;
                    else R_Edit = false;
                    if (chkR_Del != null && chkR_Del.Checked) R_Del = true;
                    else R_Del = false;
                    if (_objchucnang.Ma_Chucnang_Cha > 0)
                    {
                        _nhomnguoidungDAL.InsertT_GroupMenu(Menu_ID, Group_ID, R_Edit, R_Del, R_Add);
                        string _sqldelete = "delete from T_Nhom_Chucnang where Ma_Nhom=" + Group_ID + " and Ma_ChucNang=" + _objchucnang.Ma_Chucnang_Cha;
                        Ulti.ExecSql(_sqldelete);
                        _nhomnguoidungDAL.InsertT_GroupMenu(_objchucnang.Ma_Chucnang_Cha, Group_ID, R_Edit, R_Del, R_Add);
                    }
                    else
                        _nhomnguoidungDAL.InsertT_GroupMenu(Menu_ID, Group_ID, R_Edit, R_Del, R_Add);
                }
            }
        }
        protected void linkRoleCateExit_Click(object sender, EventArgs e)
        {
            SetAddEdit(true, false, false, false, false);

        }
        protected void linkRoleCateSaves_Click(object sender, EventArgs e)
        {
            SetAddEdit(false, false, true, false, false);
            Save_GroupCategorys(manhom);
            SetAddEdit(true, false, false, false, false);

        }
        private void Save_GroupCategorys(int Group_ID)
        {
            NguoidungDAL _usermenuDAL = new NguoidungDAL();
            ChuyenmucDAL _ChuyenmucDAL = new ChuyenmucDAL();
            T_ChuyenMuc _objcm = new T_ChuyenMuc();
            UltilFunc ulti = new UltilFunc();
            int Cate_ID = 0;
            string[] arrCate;
            char[] sepparator;
            sepparator = ";".ToCharArray();
            if (txtCateAccess.Value.Trim() != "")
            {
                arrCate = txtCateAccess.Value.Split(sepparator);
                string txt = "";
                if (txtCateAccess.Value.StartsWith("on;"))
                {
                    txt = txtCateAccess.Value.Remove(0, 3);
                    arrCate = txt.Split(sepparator);
                }
                _nhomnguoidungDAL.DeleteFromT_GroupCategory(Group_ID);
                _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nhom=" + Group_ID);
                string _sqldelete = string.Empty;
                for (int x = 0; x < arrCate.Length; x++)
                {
                    Cate_ID = Convert.ToInt32(arrCate[x].ToString());
                    _objcm = _ChuyenmucDAL.GetOneFromT_ChuyenmucByID(Cate_ID);
                    if (_objcm.Ma_Chuyenmuc_Cha > 0)
                    {
                        _nhomnguoidungDAL.InsertT_GroupCategory(Cate_ID, Group_ID);
                        _sqldelete = "delete from T_Nhom_Chuyenmuc where Ma_Nhom=" + Group_ID + " and Ma_Chuyenmuc=" + _objcm.Ma_Chuyenmuc_Cha;
                        ulti.ExecSql(_sqldelete);
                        _nhomnguoidungDAL.InsertT_GroupCategory(_objcm.Ma_Chuyenmuc_Cha, Group_ID);
                    }
                    else
                    {
                        _nhomnguoidungDAL.InsertT_GroupCategory(Cate_ID, Group_ID);
                    }
                }
            }
            else
            {
                _usermenuDAL.DeleteFromT_UserCategoryDynamic(" Ma_Nhom=" + Group_ID);
                _nhomnguoidungDAL.DeleteFromT_GroupCategory(Group_ID);
            }
        }

        #region Roll QTBT
        protected void btnApplyRoleQTBT_Click(object sender, EventArgs e)
        {
            _nhomnguoidungDAL.DeleteFromT_GroupQTBT(" Ma_Nhom=" + manhom);
            foreach (DataGridItem m_Item in DataGridGroupQTBT.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");

                if (chk_Select != null && chk_Select.Checked && chk_Select.Enabled == true)
                {
                    int LangID = Convert.ToInt32(this.DataGridGroupQTBT.DataKeys[m_Item.ItemIndex].ToString());
                    _nhomnguoidungDAL.InsertT_GroupQTBT(manhom, LangID);
                }
            }
            SetAddEdit(true, false, false, false, false);

        }
        #endregion

        #region Roll Language
        protected void btnApplyRoleLanguage_Click(object sender, EventArgs e)
        {
            _nhomnguoidungDAL.DeleteFromT_GroupLanguages(" Ma_Nhom=" + manhom);
            foreach (DataGridItem m_Item in DataGridNgonNgu.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");

                if (chk_Select != null && chk_Select.Checked && chk_Select.Enabled == true)
                {
                    int LangID = Convert.ToInt32(this.DataGridNgonNgu.DataKeys[m_Item.ItemIndex].ToString());
                    _nhomnguoidungDAL.InsertT_GroupLanguages(manhom, LangID, DateTime.Now);
                }
            }
            SetAddEdit(true, false, false, false, false);

        }
        #endregion
    }
}
