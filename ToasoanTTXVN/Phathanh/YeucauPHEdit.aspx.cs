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
    public partial class YeucauPHEdit : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        BindCombo();
                        BindData();
                    }
                }
            }
        }

        #region Method
        private void Clear()
        {
            this.ddl_TenKH.Items.Clear();
            this.txt_Tieude.Text = "";
            this.ckeNoidung.Text = "";
        }
        private void GetItem(double _id)
        {
            T_Yeucau _objYeucau = new T_Yeucau();
            HPCBusinessLogic.DAL.YeucauDAL _yeucauDAL = new HPCBusinessLogic.DAL.YeucauDAL();
            _objYeucau = _yeucauDAL.GetOneFromT_YeuCauByID(_id);
            if (_id > 0)
            {
                this.ddl_TenKH.SelectedIndex = UltilFunc.GetIndexControl(ddl_TenKH, _objYeucau.Ma_Khachhang.ToString());
                this.ddl_TenKH.Enabled = false;
            }
            this.txt_Tieude.Text = _objYeucau.TenQuangCao;
            this.ckeNoidung.Text = _objYeucau.NoidungQC;
        }
        private T_Yeucau SetItem()
        {
            T_Yeucau _obj = new T_Yeucau();
            if (Page.Request.Params["id"] != null)
            {
                _obj.ID = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.ID = 0;
            _obj.TenQuangCao = UltilFunc.SqlFormatText(this.txt_Tieude.Text.Trim());
            _obj.NoidungQC = UltilFunc.SqlFormatText(this.ckeNoidung.Text.Trim());
            _obj.Ma_Khachhang = Convert.ToInt32(this.ddl_TenKH.SelectedValue.ToString());
            _obj.Ngaytao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;           
            _obj.Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;
            _obj.Loai = 2;
            _obj.Trangthai = 0;
            return _obj;
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
        private void BindCombo()
        {
            UltilFunc.BindCombox(ddl_TenKH, "Ma_KhachHang", "Ten_khachhang", "T_Khachhang", " Loai_KH = 2 ", "---Chọn---");
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
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Session["CurrentPage"] = Session["PageIndex"];
            Page.Response.Redirect("~/Phathanh/YeucauPHList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
            HPCBusinessLogic.DAL.YeucauDAL _cvDAL = new HPCBusinessLogic.DAL.YeucauDAL();
            T_Yeucau _yc = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            double _return = _cvDAL.InsertT_YecauQuangCao(_yc);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới yêu cầu phát hành]-->[mã yêu cầu:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa nội dung yêu cầu phát hành]-->[Mã yêu cầu:" + Page.Request["id"].ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
        }
        #endregion
    }
}
