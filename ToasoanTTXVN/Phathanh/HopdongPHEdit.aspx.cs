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
    public partial class HopdongPHEdit : System.Web.UI.Page
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
            ddl_Yeucau.Items.Clear();
            UltilFunc.BindCombox(ddl_TenKH, "Ma_KhachHang", "Ten_KhachHang", "T_KhachHang"," Loai_KH = 2");
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
            this.ddl_Yeucau.Items.Clear();
            this.txt_SoHD.Text = "";
            this.txt_Mota.Text = "";
            this.txt_Sotien.Text = "";
            this.lblFilePath.Text = "";
            this.txt_NgayKy.Text = "";
            this.txt_NgayHetHan.Text = "";
        }
        private void GetItem(int _id)
        {
            T_Hopdong _obj = new T_Hopdong();
            HopdongDAL _objDAL = new HopdongDAL();
            _obj = _objDAL.GetOneFromT_HopdongByID(_id);
            if (_id > 0)
            {
                this.ddl_TenKH.SelectedIndex = UltilFunc.GetIndexControl(ddl_TenKH, _obj.Ma_KhachHang.ToString());
                this.ddl_TenKH.Enabled = false;
                ddl_Yeucau.Items.Clear();
                UltilFunc.BindCombox(ddl_Yeucau, "ID", "TenQuangCao", "T_Yeucau", " Ma_Khachhang = " + _obj.Ma_KhachHang.ToString());
                this.ddl_Yeucau.SelectedIndex = UltilFunc.GetIndexControl(ddl_Yeucau, _obj.Ma_Yeucau.ToString());
                ddl_Yeucau.Enabled = false;
            }
            //else
            //{
            //    this.ddl_TenKH.SelectedIndex = UltilFunc.GetIndexControl(ddl_TenKH, _obj.Ma_KhachHang.ToString());
            //    this.ddl_Yeucau.SelectedIndex = UltilFunc.GetIndexControl(ddl_Yeucau, _obj.Ma_Yeucau.ToString());
            //}
            this.txt_SoHD.Text = _obj.hopdongso;
            this.txt_Mota.Text = _obj.Tomtatnoidung;
            this.txt_Sotien.Text = _obj.Sotien.ToString();
            this.lblFilePath.Text = _obj.duongdan_file;
            this.txt_NgayKy.Text = _obj.ngayky.ToString("dd/MM/yyyy");
            this.txt_NgayHetHan.Text = _obj.Ngayketthuc.ToString("dd/MM/yyyy");
        }
        private T_Hopdong SetItem()
        {
            T_Hopdong _obj = new T_Hopdong();
            if (Page.Request.Params["id"] != null)
            {
                _obj.ID = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.ID = 0;
            _obj.Ma_KhachHang = Convert.ToInt32(this.ddl_TenKH.SelectedValue.ToString());
            _obj.Ma_Yeucau = Convert.ToInt32(this.ddl_Yeucau.SelectedValue.ToString());
            _obj.hopdongso = this.txt_SoHD.Text.Trim();
            _obj.Tomtatnoidung = this.txt_Mota.Text.Trim();
            if(this.txt_Sotien.Text.Trim().Length > 0)
                _obj.Sotien = Convert.ToDouble(this.txt_Sotien.Text.Trim());
            if (this.txt_NgayKy.Text.Length > 0)
                _obj.ngayky = UltilFunc.ToDate(this.txt_NgayKy.Text, "dd/MM/yyyy");
            if (this.txt_NgayHetHan.Text.Length > 0)
                _obj.Ngayketthuc = UltilFunc.ToDate(this.txt_NgayHetHan.Text, "dd/MM/yyyy");
            if (fileUpload.HasFile)
            {
                string _fileName = fileUpload.FileName;
                string _filePath = System.Configuration.ConfigurationManager.AppSettings["PathQuangCao"];
                fileUpload.SaveAs(Server.MapPath(_filePath + @"\" + _fileName));
                _obj.duongdan_file = _filePath +  _fileName;
            }
            _obj.Ngaytao = DateTime.Now;
            _obj.Nguoitao = _user.UserID;
            _obj.loai = 2;

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
        #endregion

        #region Click
        protected void ddl_TenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_Yeucau.Items.Clear();

            if (ddl_TenKH.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(ddl_Yeucau, "ID", "TenQuangCao", "T_Yeucau", " Ma_Khachhang = " + ddl_TenKH.SelectedValue.ToString() + " AND Trangthai = 1");
                ddl_Yeucau.UpdateAfterCallBack = true;
                ddl_Yeucau.AutoUpdateAfterCallBack = true;
            }
            else
            {
                ddl_Yeucau.DataSource = null;
                ddl_Yeucau.DataBind();
                ddl_Yeucau.UpdateAfterCallBack = true;                
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
            HopdongDAL _hopdongDAL = new HopdongDAL();
            T_Hopdong _hopdong = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _hopdongDAL.InsertT_Hopdong(_hopdong);
            if (Page.Request.Params["id"] == null)
            {
                #region Update T_Yeucau khi da co hop dong --> Trangthai = 2

                HPCBusinessLogic.DAL.YeucauDAL _yeucauDAL = new HPCBusinessLogic.DAL.YeucauDAL();
                _yeucauDAL.UpdateinfoT_Yeucau(" [Trangthai] = 2 where ID = " + ddl_Yeucau.SelectedValue.ToString());

                #endregion
                action.Thaotac = "[Thêm mới hợp đồng]-->[mã hợp đồng:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa hợp đồng]-->[Mã hợp đồng:" + Page.Request["id"].ToString() + " ]";
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
            Page.Response.Redirect("~/Phathanh/HopdongPHList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        #endregion
    }
}
