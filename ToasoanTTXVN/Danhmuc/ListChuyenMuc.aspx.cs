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
    public partial class ListChuyenMuc : BasePage
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
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);

                    if (!IsPostBack)
                    {
                        Session["where_chuyenmuc"] = null;
                        cbo_Anpham.Items.Clear();
                        UltilFunc.BindCombox(cbo_Anpham, "Ma_AnPham", "Ten_AnPham", "T_Anpham", " 1=1 ", "---Chọn ấn phẩm---");
                        cbo_Anpham.SelectedIndex = UltilFunc.GetIndexControl(cbo_Anpham, HPCComponents.Global.DefaultCombobox);
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            Danhsach_ChuyenMuc();
                        }
                        else
                            Danhsach_ChuyenMuc();
                    }


                }
            }
        }

        #region Methods
        public void Danhsach_ChuyenMuc()
        {
            string where = " Ma_Chuyenmuc_Cha=0";
            if (cbo_Anpham.SelectedIndex > 0)
                where += " and Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString();
            if (!String.IsNullOrEmpty(this.txtSearch_ChuyenMuc.Text.Trim()))
                where += " and " + string.Format(" Ten_ChuyenMuc like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_ChuyenMuc.Text.Trim()))
                    + " or Ma_ChuyenMuc in( select Ma_Chuyenmuc_Cha from T_ChuyenMuc where Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where " + string.Format(" Ten_ChuyenMuc like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_ChuyenMuc.Text.Trim())) + "))";
            if (chk_Hoatdong.Checked)
                where += " and Hoatdong=1";
            if (!chk_Hoatdong.Checked)
                where += " and Hoatdong=0";
            if (CheckBoxBaoDT.Checked)
                where += " and HienThi_BDT=1";
            if (CheckBoxBaoIn.Checked)
                where += " and HienThi_BaoIn=1";

            pages.PageSize = Global.MembersPerPage;
            ChuyenmucDAL _chuyenmucDAL = new ChuyenmucDAL();
            DataSet _ds;
            if (Session["where_chuyenmuc"] != null)
                _ds = _chuyenmucDAL.BindGridT_Cagegorys(pages.PageIndex, pages.PageSize, Session["where_chuyenmuc"].ToString());
            else
                _ds = _chuyenmucDAL.BindGridT_Cagegorys(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                if (Session["where_chuyenmuc"] != null)
                    _ds = _chuyenmucDAL.BindGridT_Cagegorys(pages.PageIndex - 1, pages.PageSize, Session["where_chuyenmuc"].ToString());
                else
                    _ds = _chuyenmucDAL.BindGridT_Cagegorys(pages.PageIndex - 1, pages.PageSize, where);
            DataTable dt = _chuyenmucDAL.BindGridCategory(_ds.Tables[0]);
            grdListCate.DataSource = dt;
            grdListCate.DataBind();
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
        protected string Hoatdong(Object Hoatdong)
        {
            string strReturn = "";
            if (Hoatdong != DBNull.Value)
            {
                if (bool.Parse(Hoatdong.ToString()))
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            return strReturn;
        }
        #endregion

        #region Click
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Danhmuc/EditChuyenMuc.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            Session["where_chuyenmuc"] = null;
            string where = " Ma_Chuyenmuc_Cha=0";
            if (cbo_Anpham.SelectedIndex > 0)
                where += " and Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString();
            if (!String.IsNullOrEmpty(this.txtSearch_ChuyenMuc.Text.Trim()))

                where += " and " + string.Format(" Ten_ChuyenMuc like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_ChuyenMuc.Text.Trim()))
                    + " or Ma_ChuyenMuc in( select Ma_Chuyenmuc_Cha from T_ChuyenMuc where Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where " + string.Format(" Ten_ChuyenMuc like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_ChuyenMuc.Text.Trim())) + "))";
            if (chk_Hoatdong.Checked)
                where += " and Hoatdong=1";
            if (!chk_Hoatdong.Checked)
                where += " and Hoatdong=0";
            if (CheckBoxBaoDT.Checked)
                where += " and HienThi_BDT=1";
            if (CheckBoxBaoIn.Checked)
                where += " and HienThi_BaoIn=1";
            Session["where_chuyenmuc"] = where;
            pages.PageIndex = 0;
            Danhsach_ChuyenMuc();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_ChuyenMuc();
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                ChuyenmucDAL _dal = new ChuyenmucDAL();
                int _id = Convert.ToInt32(grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                if (_dal.CheckExists_ChuyenMuc(_id) == 1)
                    btnDelete.Attributes.Add("onclick", "return alert(\"Chuyên mục này đang được sử dụng\");");
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
            ChuyenmucDAL obj_Cate = new ChuyenmucDAL();
            int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {

                Response.Redirect("~/Danhmuc/EditChuyenMuc.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
            if (e.CommandArgument.ToString().ToLower() == "editdisplay")
            {
                bool check = obj_Cate.GetOneFromT_ChuyenmucByID(catID).HoatDong;
                if (check)
                {
                    obj_Cate.UpdateFromT_ChuyenMucDynamic(" Hoatdong = 0, Ngaysua = Getdate(), Nguoisua=" + _user.UserID.ToString() + " Where Ma_ChuyenMuc = " + catID);
                    action.Thaotac = "[Update T_ChuyenMuc]-->[Hoatdong = 0]";
                }
                else
                {
                    obj_Cate.UpdateFromT_ChuyenMucDynamic(" Hoatdong = 1, Ngaysua = Getdate(), Nguoisua=" + _user.UserID.ToString() + " Where Ma_ChuyenMuc = " + catID);
                    action.Thaotac = "[Update T_ChuyenMuc]-->[Hoatdong = 1]";
                }

            }
            if (e.CommandArgument.ToString().ToLower() == "active_bdt")
            {

                bool check = obj_Cate.GetOneFromT_ChuyenmucByID(catID).HienThi_BDT;
                if (check)
                {
                    obj_Cate.UpdateFromT_ChuyenMucDynamic(" HienThi_BDT = 0, Ngaysua = Getdate(), Nguoisua=" + _user.UserID.ToString() + " Where Ma_ChuyenMuc = " + catID);
                    action.Thaotac = "[Update T_ChuyenMuc]-->[HienThi_BDT = 0]";
                }
                else
                {
                    obj_Cate.UpdateFromT_ChuyenMucDynamic(" HienThi_BDT = 1, Ngaysua = Getdate(), Nguoisua=" + _user.UserID.ToString() + " Where Ma_ChuyenMuc = " + catID);
                    action.Thaotac = "[Update T_ChuyenMuc]-->[HienThi_BDT = 1]";
                }

            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {

                if (obj_Cate.CheckExists_ChuyenMuc(catID) == 0)
                {
                    obj_Cate.DeleteFromT_ChuyenmucByID(catID);
                    action.Thaotac = "[Xóa T_ChuyenMuc]-->[Thao tác Xóa Trong Bảng T_ChuyenMuc][ID:" + this.grdListCate.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                }
                else
                {
                    FuncAlert.AlertJS(this, "chuyên mục đã được sử dung biên tập tin bài!");
                    return;
                }
            }
            Danhsach_ChuyenMuc();
        }
        #endregion
    }
}
