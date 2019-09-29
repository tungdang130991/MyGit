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
    public partial class ButDanh : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        UltilFunc ulti = new UltilFunc();
        private int userID
        {
            get { if (ViewState["userID"] != null) return Convert.ToInt32(ViewState["userID"]); else return 0; }

            set { ViewState["userID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }


        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            string _thaotac = string.Empty;
            if (txt_butdanh.Text.Trim().Length == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa nhập bút danh");
                return;
            }

            T_Nguoidung _obj = SetItem();
            int _return = 0;
            if (userID != 0)
                _return = userID;
            if (_return != 0)
            {
                if (!isExist(txt_butdanh.Text.Trim(), userID))
                {
                    _return = _NguoidungDAL.InsertT_Nguoidung(_obj);
                    _thaotac = "[Sửa bút danh]-->[ID:" + _return.ToString() + " ]";
                    
                }
                else
                {
                    FuncAlert.AlertJS(this, "Bút danh đã tồn tại!");
                    return;
                }
            }
            else
            {
                if (!Page.IsValid) return;
                if (!isExist(txt_butdanh.Text.Trim()))
                {

                    _return = _NguoidungDAL.InsertT_Nguoidung(_obj);
                    _thaotac = "[Thêm mới bút danh]-->[ID:" + _return.ToString() + " ]";                    

                }
                else
                {

                    FuncAlert.AlertJS(this, "Bút danh đã tồn tại!");
                    return;

                }
            }
            txt_butdanh.Text = "";
            userID = 0;
            btnAddMenu.Text = "Thêm mới";
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), _thaotac);
            LoadData();
        }

        private bool isExist(string CustomerName, double ID)
        {
            DataTable d = _NguoidungDAL.GetTenNguoiDungDynamic(" Ten_Dangnhap=N'" + CustomerName + "' and nguoitao=" + _user.UserID.ToString() + " and Ma_Nguoidung<>" + ID.ToString(), null).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExist(string CustomerName)
        {
            DataTable d = _NguoidungDAL.GetTenNguoiDungDynamic(" Ten_Dangnhap=N'" + CustomerName + "' and nguoitao=" + _user.UserID.ToString(), null).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public string GetCMTND(int _userid, string UserName)
        {
            string CMTND = string.Empty;
            DataTable _dt = new DataTable();
            string _sql = "select top 1 CMTND from T_Nguoidung where Ten_Dangnhap=N'" + UserName + "' and Loai=0 and Nguoitao=" + _userid;
            _dt = ulti.ExecSqlDataSet(_sql).Tables[0];
            if (_dt != null && _dt.Rows.Count > 0)
                CMTND = _dt.Rows[0]["CMTND"].ToString();
            else
                CMTND = "";
            return CMTND;
        }
        public T_Nguoidung SetItem()
        {
            T_Users objuser = new T_Users();
            objuser = _NguoidungDAL.GetUserByUserName_ID(_user.UserID);
            T_Nguoidung _obj = new T_Nguoidung();
            if (userID != 0)
                _obj.Ma_Nguoidung = userID;
            else
                _obj.Ma_Nguoidung = 0;
            _obj.Ten_Dangnhap = UltilFunc.SqlFormatText(txt_butdanh.Text.Trim());
            if (objuser.UserBirthday != null)
                _obj.Ngaysinh = objuser.UserBirthday;
            _obj.Diachi = objuser.UserAddress;
            _obj.TenDaydu = objuser.UserFullName;
            if (GetCMTND(objuser.UserID, objuser.UserName).Length > 0)
                _obj.CMTND = GetCMTND(objuser.UserID, objuser.UserName);
            else
                _obj.CMTND = "";
            _obj.Mobile = objuser.UserMobile;
            _obj.Email = objuser.UserEmail;
            _obj.Hoatdong = false;
            _obj.NgayTao = DateTime.Now;
            _obj.NguoiTao = _user.UserID;
            _obj.Loai = 0;
            _obj.Ma_Vung = int.Parse(objuser.RegionID.ToString());

            return _obj;
        }
        public void LoadData()
        {
            string where = " Loai=0 and (Trangthai_Xoa=0 or Trangthai_Xoa is null) and NguoiTao=" + _user.UserID;
            if (!String.IsNullOrEmpty(txtSearch_UserName.Text.Trim()))
                where += "AND " + string.Format(" Ten_Dangnhap like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_UserName.Text.Trim()));
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new HPCBusinessLogic.NguoidungDAL();
            DataSet _ds;
            _ds = _NguoidungDAL.BindGridT_Nguoidung(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _NguoidungDAL.BindGridT_Nguoidung(pages.PageIndex - 1, pages.PageSize, where);
            DataGridButDanh.DataSource = _ds;
            DataGridButDanh.DataBind();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }

        #region Tim KIEM
        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion

        public void grdListUser_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListUser_EditCommand(object source, DataGridCommandEventArgs e)
        {
            userID = int.Parse(this.DataGridButDanh.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "editusers")
            {               
                T_Nguoidung obj = new T_Nguoidung();
                obj = _NguoidungDAL.GetOneFromT_NguoidungByID(userID);
                txt_butdanh.Text = obj.Ten_Dangnhap;
                btnAddMenu.Text = "Lưu giữ";
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {               
                if (_NguoidungDAL.CheckDelete_users(userID))
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn không được xóa bút danh đã được chọn tác giả cho tin bài xuất bản.!');", true);
                    return;
                }
                else
                {
                    _NguoidungDAL.DeleteFromT_Nguoidung(userID);
                    this.LoadData();
                }
            }

        }
    }
}
