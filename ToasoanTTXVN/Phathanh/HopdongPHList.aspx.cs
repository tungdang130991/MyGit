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
using System.Collections.Generic;

namespace ToasoanTTXVN.Phathanh
{
    public partial class HopdongPHList : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_RolePermission _Role = null;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
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
        private DataTable _DataTableThanhtoan
        {
            get { return (DataTable)Page.Session["_DataTableThanhtoan"]; }
            set { Page.Session["_DataTableThanhtoan"] = value; }
        }
        private double SotienHD
        {
            get
            {
                if (ViewState["SotienHD"] == null)
                    return 0;
                return (double)ViewState["SotienHD"];
            }
            set { ViewState["SotienHD"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UltilFunc.IsNumeric(Request["Menu_ID"]))
            {
                if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                    Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                _Role = _userDAL.GetRole4UserMenu(_user.UserID, MenuID);
                this.btnAdd.Visible = _Role.R_Read;
                Session["sortBy"] = null;
                if (!IsPostBack)
                {
                    if (Session["CurrentPage"] != null)
                    {
                        pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                        Session["CurrentPage"] = null;
                        Danhsach_Hopdong();
                    }
                    else
                    {
                        Danhsach_Hopdong();
                    }
                }
            }
        }

        #region Method
        public void Danhsach_Hopdong()
        {
            string where = " 1=1 and Loai=2 ";
            if (!String.IsNullOrEmpty(this.hdnValue.Value.ToString().Trim()))
                where += " AND Ma_KhachHang = " + string.Format(hdnValue.Value.ToString()) + " ";
            if (!String.IsNullOrEmpty(this.txt_SoHD.Text.Trim()))
                where += " AND hopdongso = '" + this.txt_SoHD.Text.ToString().Trim() + "' ";
            if (!String.IsNullOrEmpty(this.txt_NgayKy.Text.Trim()))
                where += " AND " + string.Format(" ngayky >='{0}'", UltilFunc.ToDate(this.txt_NgayKy.Text.Trim(), "dd/MM/yyyy").ToShortDateString() + " 00:00:00 ");
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.HopdongDAL _hopdongDAL = new HopdongDAL();
            DataSet _ds;
            _ds = _hopdongDAL.BindGridT_Hopdong(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _hopdongDAL.BindGridT_Hopdong(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataView DV = _ds.Tables[0].DefaultView;

                if (Session["sortBy"] != null)
                {
                    DV.Sort = Session["sortBy"].ToString();
                }
                
                GVListHopdong.DataSource = DV;
                GVListHopdong.DataBind();
            }
            else
            {
                GVListHopdong.DataSource = null;
                GVListHopdong.DataBind();
            }
            //grdList.DataSource = _ds.Tables[0];
            //grdList.DataBind();
            
            _ds.Clear();
            hdnValue.Value = "";
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
        }
        protected string TenKhachHang(string _Id)
        {
            string strReturn = "";
            T_KhachHang _kh = new T_KhachHang();
            KhachhangDAL _dal = new KhachhangDAL();
            if (!String.IsNullOrEmpty(_Id) && Convert.ToInt32(_Id) > 0)
            {
                _kh = _dal.GetOneFromT_KhachHangByID(Convert.ToInt32(_Id));
                strReturn = _kh.Ten_KhachHang;
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
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Read;
        }
        #endregion

        #region Click

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Phathanh/HopdongPHEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Danhsach_Hopdong();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            Danhsach_Hopdong();
        }       
        protected void PayClick(object sender, EventArgs e)
        {
            using (GridViewRow _item = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
                popup.Show();
            }
        }
        protected void SaveClick(object sender, EventArgs e)
        { 

        }
        #endregion       

        #region GridView
        protected void GVListHopdong_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
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
        protected void GVListHopdong_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            int _id = Convert.ToInt32(GVListHopdong.DataKeys[e.RowIndex].Values["ID"].ToString());
            HopdongDAL _apDAL = new HopdongDAL();
            _apDAL.DeleteOneFromT_Hopdong(_id);

            action.Thaotac = "[Xóa hợp đồng]-->[Mã hợp đồng:" + _id.ToString() + " ]";
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

            Danhsach_Hopdong();
        }


        protected void GVListHopdong_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                GridViewRow _row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int index = _row.RowIndex;
                int _ID = Convert.ToInt32(this.GVListHopdong.DataKeys[index].Value);
                Response.Redirect("~/Phathanh/HopdongPHEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID);
            }
            if (e.CommandArgument.ToString().ToLower() == "view")
            { 
            }
        }
        protected void GVListHopdong_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GVListHopdong.EditIndex = e.NewEditIndex;
            Danhsach_Hopdong();
        }

        protected void GVListHopdong_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Session["sortBy"] = e.SortExpression;
            Danhsach_Hopdong();
        }
        #endregion

        #region Popup
        public void BindDataLichSuThanhToanPhatHanh()
        {
            string where = "Loai=2 and HOPDONG_SO=" + ViewState["ID"];
            HPCBusinessLogic.DAL.LichsuthanhtoanDAL dal = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            Pager1.PageSize = Global.MembersPerPage;
            DataSet _ds;
            _ds = dal.BindGridT_LichsuThanhtoan(Pager1.PageIndex, Pager1.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = dal.BindGridT_LichsuThanhtoan(Pager1.PageIndex - 1, Pager1.PageSize, where);
           

            if (_ds.Tables[0].Rows.Count > 0)
            {
                _DataTableThanhtoan = _ds.Tables[0];
                GVThanhtoanHD.DataSource = _ds;
                GVThanhtoanHD.DataBind();
                GVThanhtoanHD.ShowFooter = false;
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                GVThanhtoanHD.DataSource = _ds;
                GVThanhtoanHD.DataBind();
                int columncount = GVThanhtoanHD.Rows[0].Cells.Count;
                GVThanhtoanHD.Rows[0].Cells.Clear();
                GVThanhtoanHD.Rows[0].Cells.Add(new TableCell());
                GVThanhtoanHD.Rows[0].Cells[0].ColumnSpan = columncount;
                GVThanhtoanHD.Rows[0].Cells[0].Text = "Không có bản ghi nào";
                GVThanhtoanHD.ShowFooter = false;
                _DataTableThanhtoan = null;
            }          
            Pager1.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = Pager1.CalculateTotalPages();
            CurrentPage1.PageIndex = Pager1.PageIndex;          
        }
        protected void Layout(object sender, EventArgs e)
        {
            using (GridViewRow _row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
                EID = Convert.ToInt32(GVListHopdong.DataKeys[_row.RowIndex].Value);
                ViewState["ID"] = EID.ToString();
                //lblMess.Text = "";
                LinkButton _lblTenKH = (LinkButton)GVListHopdong.Rows[_row.RowIndex].FindControl("btnEdit");
                if (_lblTenKH != null)
                    lbl_TenKH.Text = "Tên khách hàng: " + _lblTenKH.Text.Trim();
                else
                    lbl_TenKH.Text = "";
                Label _lblSHD = (Label)GVListHopdong.Rows[_row.RowIndex].FindControl("lblSohopdong");
                if (!String.IsNullOrEmpty(_lblSHD.Text))
                    lbl_SoHD.Text = "Số hợp đồng: " + _lblSHD.Text;
                else
                    lbl_SoHD.Text = "";

                HopdongDAL daldh = new HopdongDAL();
                SotienHD = daldh.GetOneFromT_HopdongByID(EID).Sotien;

                if (SotienHD != 0)
                    lblTongtien.Text = " Tổng tiền: " + String.Format("{0:00,0}", Convert.ToDecimal(SotienHD));
                else
                    lblTongtien.Text = " Tổng tiền: " +  String.Format("{0:00,0}", "0");
                lblMessError.Text = "";
                BindDataLichSuThanhToanPhatHanh();
                popup.Show();
            }
        }
        protected void btnAddPopUp_Click(object sender, EventArgs e)
        {
            GVThanhtoanHD.ShowFooter = true;
            BindDataLichSuThanhToanPhatHanh();
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            EID = -1;
          //  BindList_AnphamMau();
        }
        public double Total(DataTable dt, string col)
        {
            double tong = 0;
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string temp = dt.Rows[i][col].ToString();
                    tong += int.Parse(temp.Trim());
                }
            }
            return tong;
        }
        public double TongTien(DataTable dt, string col, int id)
        {
            double tong = 0;
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (int.Parse(dt.Rows[i]["ID"].ToString()) != id)
                    {
                        string temp = dt.Rows[i][col].ToString();
                        tong += int.Parse(temp.Trim());
                    }
                }
            }
            return tong;
        }

        protected void GVThanhtoanHD_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GVListHopdong.EditIndex == e.Row.RowIndex)
            {
                TextBox txtsotien = (TextBox)e.Row.FindControl("txtsotien");
                TextBox txtngaythu = (TextBox)e.Row.FindControl("txtngaythu");
                TextBox txtnguoithanhtoan = (TextBox)e.Row.FindControl("txtnguoithanhtoan");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtsotien = (TextBox)e.Row.FindControl("txtsotien");
                TextBox txtngaythu = (TextBox)e.Row.FindControl("txtngaythu");
                TextBox txtnguoithanhtoan = (TextBox)e.Row.FindControl("txtnguoithanhtoan");
                Label lblsohd = (Label)e.Row.FindControl("lblsohd");

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

            e.Row.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void GVThanhtoanHD_OnRowCommand(object source, GridViewCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.LichsuthanhtoanDAL dal = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox txtsotien = (TextBox)GVThanhtoanHD.FooterRow.FindControl("txtsotien");
                TextBox txtngaythu = (TextBox)GVThanhtoanHD.FooterRow.FindControl("txtngaythu");
                TextBox txtnguoithanhtoan = (TextBox)GVThanhtoanHD.FooterRow.FindControl("txtnguoithanhtoan");
                Label lblthongbaoSotien = (Label)GVThanhtoanHD.FooterRow.FindControl("lblthongbaoSotien");

                if (txtsotien.Text.Trim() == "")
                {
                    lblthongbaoSotien.Text = " bạn chưa nhập số tiền";
                    return;
                }
                if (txtngaythu.Text.Trim() == "")
                {
                    lblthongbaoSotien.Text = " bạn chưa nhập ngày thu tiền";
                    return;
                }
                if (txtnguoithanhtoan.Text.Trim() == "")
                {
                    lblthongbaoSotien.Text = " bạn chưa nhập người thanh toán";
                    return;
                }
                double Sotienthanhtoan = 0;

                string Thaotac = "";
                HopdongDAL daldh = new HopdongDAL();
                int makh = daldh.GetOneFromT_HopdongByID(int.Parse(ViewState["ID"].ToString())).Ma_KhachHang;

                T_LichsuThanhtoan obj = new T_LichsuThanhtoan();
                if (ViewState["IDHD"] != null)
                    obj.ID = int.Parse(ViewState["IDHD"].ToString());
                else
                    obj.ID = 0;
                obj.HOPDONG_SO = int.Parse(ViewState["ID"].ToString());
                obj.MA_KHACHHANG = makh;
                if (txtsotien.Text.Trim() != "")
                    obj.SOTIEN = double.Parse(txtsotien.Text.Replace(",", ""));
                else
                    obj.SOTIEN = 0;
                obj.NGUOITHU = _user.UserID;
                obj.TENNGUOINOP = txtnguoithanhtoan.Text;
                obj.Loai = 2;
                if (txtngaythu.Text.Length > 0)
                    obj.NGAYTHU = UltilFunc.ToDate(txtngaythu.Text.Trim(), "dd/MM/yyyy");
                Sotienthanhtoan = Total(_DataTableThanhtoan, "SOTIEN");

                if (obj.SOTIEN + Sotienthanhtoan <= SotienHD)
                {
                    Thaotac = "Thêm mới thông tin lịch sử thanh toán quảng cáo";
                    dal.Sp_InsertT_LichsuThanhtoan(obj);
                    BindDataLichSuThanhToanPhatHanh();
                    if (Thaotac != "")
                        UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), Thaotac);
                }
                else
                {
                    GVThanhtoanHD.ShowFooter = true;
                    lblMessError.Text = "Số tiền thanh toán vượt quá số tiền hợp đồng";
                    BindDataLichSuThanhToanPhatHanh();                    
                }
            }

        }
        protected void GVThanhtoanHD_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVThanhtoanHD.EditIndex = e.NewEditIndex;
            BindDataLichSuThanhToanPhatHanh();
        }
        protected void GVThanhtoanHD_RowUpdating(object source, GridViewUpdateEventArgs e)
        {
            int _ID = Convert.ToInt32(GVThanhtoanHD.DataKeys[e.RowIndex].Value.ToString());

            HPCBusinessLogic.DAL.LichsuthanhtoanDAL _DAL = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            TextBox txtsotien = (TextBox)GVThanhtoanHD.Rows[e.RowIndex].FindControl("txtsotien");
            TextBox txtngaythu = (TextBox)GVThanhtoanHD.Rows[e.RowIndex].FindControl("txtngaythu");
            TextBox txtnguoithanhtoan = (TextBox)GVThanhtoanHD.Rows[e.RowIndex].FindControl("txtnguoithanhtoan");
            Label lblthongbao = (Label)GVThanhtoanHD.Rows[e.RowIndex].FindControl("lblthongbao");
            string _txtsotien = "";
            string _txtngaythu = "";
            string _txtnguoithanhtoan = "";
            string Thaotac = "";
            if (txtsotien != null)
            {
                if (!String.IsNullOrEmpty(txtsotien.Text.Trim()))
                    _txtsotien = txtsotien.Text.Replace(",", "");
            }
            if (txtngaythu != null)
            {
                if (!String.IsNullOrEmpty(txtngaythu.Text.Trim()))
                    _txtngaythu = txtngaythu.Text;
            }
            if (txtnguoithanhtoan != null)
            {
                if (!String.IsNullOrEmpty(txtnguoithanhtoan.Text.Trim()))
                    _txtnguoithanhtoan = txtnguoithanhtoan.Text;
            }
            double Tongtienthanhtoan = TongTien(_DataTableThanhtoan, "SOTIEN", _ID);
            if (double.Parse(_txtsotien) + Tongtienthanhtoan <= SotienHD)
            {
                Thaotac = "Sửa đổi thông tin lịch sử thanh toán quảng cáo";
                _DAL.UpdateT_LichsuThanhtoan(_ID, double.Parse(_txtsotien), UltilFunc.ToDate(_txtngaythu.Trim(), "dd/MM/yyyy"), _user.UserID, _txtnguoithanhtoan);
            }
            else
            {
                lblthongbao.Text = "Số tiền thanh toán vượt quá số tiền hợp đồng";
                return;
            }
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), Thaotac);
            GVThanhtoanHD.EditIndex = -1;
            BindDataLichSuThanhToanPhatHanh();
        }
        protected void GVThanhtoanHD_RowCancelingEdit(object source, GridViewCancelEditEventArgs e)
        {
            GVThanhtoanHD.EditIndex = -1;
            lblMessError.Text = "";
            BindDataLichSuThanhToanPhatHanh();
        }
        protected void GVThanhtoanHD_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _ID = Convert.ToInt32(GVThanhtoanHD.DataKeys[e.RowIndex].Value);
            HPCBusinessLogic.DAL.LichsuthanhtoanDAL dal = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            dal.Sp_DeleteOneFromT_LichsuThanhtoan(_ID);
            BindDataLichSuThanhToanPhatHanh();
        }

        protected void pages1_IndexChanged(object sender, EventArgs e)
        {
            BindDataLichSuThanhToanPhatHanh();
        }
        #endregion
    }
}
