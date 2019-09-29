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


namespace ToasoanTTXVN.Phathanh
{
    public partial class DatbaoEdit : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, MenuID);
                    this.linkSave.Visible = _Role.R_Write;
                    if (!IsPostBack)
                    {
                        BindCombo();
                        BindData();
                    }
                }
            }
        }

        #region Method
        public void BindCombo()
        {
            ddl_TenKH.Items.Clear();           
            UltilFunc.BindCombox(ddl_TenKH, "Ma_KhachHang", "Ten_KhachHang", "T_KhachHang", " Loai_KH = 2");
            ddl_Anpham.Items.Clear();
            UltilFunc.BindCombox(ddl_Anpham, "Ma_AnPham", "Ten_AnPham", "T_AnPham"," 1=1 ");
        }
        public void BindData()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" &&
                Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    GetItem(_id);
                }
            }
        }
        private void Clear()
        {
            this.ddl_TenKH.Items.Clear();
            this.ddl_Anpham.Items.Clear();
           // this.txt_SoHD.Text = "";
            this.txt_Soluong.Text = "";
            this.txt_Sotien.Text = "";
            this.txt_NgayBatDau.Text = "";
            this.txt_NgayKetThuc.Text = "";
            this.txt_Ghichu.Text = "";            
        }
        private void GetItem(int _id)
        {
            T_Datbao _obj = new T_Datbao();
            DatbaoDAL _objDAL = new DatbaoDAL();
            _obj = _objDAL.GetOneFromT_DatbaoByID(_id);
            this.ddl_TenKH.SelectedIndex = UltilFunc.GetIndexControl(ddl_TenKH, _obj.Ma_khachhang.ToString());
           // this.txt_SoHD.Text = _obj.Hopdong_so;
            UltilFunc.BindCombox(ddl_SoHD, "ID", "hopdongso", "T_Hopdong", " Ma_KhachHang = " + _obj.Ma_khachhang.ToString());
            this.ddl_SoHD.SelectedIndex = UltilFunc.GetIndexControl(ddl_SoHD, _obj.Hopdong_so);
            this.ddl_Anpham.SelectedIndex = UltilFunc.GetIndexControl(ddl_Anpham, _obj.Ma_anpham.ToString());         
            
            this.txt_Sotien.Text = _obj.Sotien.ToString();
            this.txt_Soluong.Text = _obj.Soluong.ToString();           
            this.txt_NgayBatDau.Text = _obj.NGAY_BATDAU.ToString("dd/MM/yyyy");
            this.txt_NgayKetThuc.Text = _obj.NGAY_KETTHUC.ToString("dd/MM/yyyy");
            this.txt_Ghichu.Text = _obj.Ghichu;
        }
        private T_Datbao SetItem()
        {
            T_Datbao _obj = new T_Datbao();
            if (Page.Request.Params["id"] != null)
            {
                _obj.ID = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.ID = 0;
            _obj.Ma_khachhang = Convert.ToInt32(this.ddl_TenKH.SelectedValue.ToString());
            _obj.Ma_anpham = Convert.ToInt32(this.ddl_Anpham.SelectedValue.ToString());
            _obj.Hopdong_so = ddl_SoHD.SelectedValue.ToString(); //this.txt_SoHD.Text.Trim();
            _obj.Ghichu = this.txt_Ghichu.Text.Trim();
            _obj.Sotien = Convert.ToInt32(this.txt_Sotien.Text.Trim());
            _obj.Soluong = Convert.ToInt32(this.txt_Soluong.Text.Trim());
            if (this.txt_NgayBatDau.Text.Length > 0)
                _obj.NGAY_BATDAU = UltilFunc.ToDate(this.txt_NgayBatDau.Text, "dd/MM/yyyy");
            if (this.txt_NgayKetThuc.Text.Length > 0)
                _obj.NGAY_KETTHUC = UltilFunc.ToDate(this.txt_NgayKetThuc.Text, "dd/MM/yyyy");       
            

            return _obj;
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
        //protected string So_Hop_Dong(int _maKhachhang)
        //{
        //    string strReturn = "";
        //    T_Hopdong\
        //    _anpham;
        //    AnPhamDAL _anphamDAL = new AnPhamDAL();
        //    _anpham = _anphamDAL.GetOneFromT_AnPhamByID(Convert.ToInt32(_maAnpham));
        //    if (_anpham != null)
        //        strReturn = _anpham.Ten_AnPham;
        //    return strReturn;
        //}
       
        #endregion

        #region Click
        protected void ddl_TenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_TenKH.SelectedIndex > 0)
            {
                ddl_SoHD.Items.Clear();
                UltilFunc.BindCombox(ddl_SoHD, "ID", "hopdongso", "T_Hopdong", " Ma_KhachHang = " + ddl_TenKH.SelectedValue.ToString());
                ddl_SoHD.UpdateAfterCallBack = true;
            }          

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            this.Page.Validate(vs_Themmoi.ValidationGroup);
            if (!Page.IsValid) return;
            DatbaoDAL _datbaoDAL = new DatbaoDAL();
            T_Datbao _datbao = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _datbaoDAL.InsertT_Datbao(_datbao);
            if (Page.Request.Params["id"] == null)
            {
               
                action.Thaotac = "[Thêm mới T_Datbao]-->[Mã:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa T_Datbao]-->[Mã :" + Page.Request["id"].ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }

        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Session["CurrentPage"] = Session["PageIndex"];
            Page.Response.Redirect("~/Phathanh/DatbaoList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        #endregion
    }
}
