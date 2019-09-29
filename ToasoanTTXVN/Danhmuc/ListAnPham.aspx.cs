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
    public partial class ListAnPham : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc _ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    btnAddMenu.Visible = _Role.R_Read;
                    if (!IsPostBack)
                    {

                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            Session["CurrentPage"] = null;
                            Danhsach_AnPham();
                        }
                        else
                            Danhsach_AnPham();
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
        public void Danhsach_AnPham()
        {
            string where = "";
            if (!String.IsNullOrEmpty(this.txt_TenAnPham.Text.Trim()))
                where += " " + string.Format(" Ten_AnPham like N'%{0}%'", UltilFunc.SqlFormatText(this.txt_TenAnPham.Text.Trim()));


            pages.PageSize = Global.MembersPerPage;
            AnPhamDAL _anphamDAL = new AnPhamDAL();
            DataSet _ds;
            _ds = _anphamDAL.BindGridT_AnPham(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _anphamDAL.BindGridT_AnPham(pages.PageIndex - 1, pages.PageSize, where);

            grdList.DataSource = _ds.Tables[0];
            grdList.DataBind();
            _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
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
        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Danhmuc/EditAnPham.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Danhsach_AnPham();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_AnPham();
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            DropDownList cbo_quytrinh = (DropDownList)e.Item.FindControl("cbo_quytrinh");
            if (cbo_quytrinh != null)
            {
                cbo_quytrinh.Items.Clear();
                UltilFunc.BindCombox(cbo_quytrinh, "ID", "Ten_QuyTrinh", "T_Ten_QuyTrinh", " 1=1 ", "---");
            }
            HiddenField HiddenField_MaQT = (HiddenField)e.Item.FindControl("HiddenField_MaQT");
            if (HiddenField_MaQT != null)
                if (HiddenField_MaQT.Value != "")
                    cbo_quytrinh.SelectedValue = HiddenField_MaQT.Value;
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
                    btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
                AnPhamDAL _dtDAL = new AnPhamDAL();
                int _id = Convert.ToInt32(grdList.DataKeys[e.Item.ItemIndex].ToString());
                if (_dtDAL.CheckExists_AnPham(_id) == 1)
                    btnDelete.Attributes.Add("onclick", "return alert(\"Ấn phẩm này đang được sử dụng\");");
                else
                    btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            AnPhamDAL obj_AnPham = new AnPhamDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Danhmuc/EditAnPham.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                int catID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                if (obj_AnPham.CheckExists_AnPham(catID) == 0)
                {
                    obj_AnPham.DeleteFromT_AnPhamByID(catID);
                    action.Thaotac = "[Xóa T_AnPham]-->[Thao tác Xóa Trong Bảng T_AnPham][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                }
                else
                {
                    return;
                }
                this.Danhsach_AnPham();
            }
        }
        #endregion

        protected void btn_updateqt_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdList.Items)
            {
                DropDownList cbo_quytrinh = (DropDownList)m_Item.FindControl("cbo_quytrinh");
                double _ID = double.Parse(grdList.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                string _sql = "update T_AnPham set Ma_QT=" + cbo_quytrinh.SelectedValue + " where Ma_AnPham=" + _ID;
                _ulti.ExecSql(_sql);

            }
            Danhsach_AnPham();
        }
    }
}
