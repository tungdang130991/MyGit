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

namespace ToasoanTTXVN.Menu
{
    public partial class EditMenu : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        public string urlimages = "";
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
                        ddl_Madoituong.Enabled = false;
                        BindCombo();
                        BindData();
                        if (chk_Quytrinh.Checked)
                            ddl_Madoituong.Enabled = true;
                        else
                            ddl_Madoituong.Enabled = false;
                    }
                }
            }
        }
        #region Method
        private void Clear()
        {
            this.txt_Tenchucnang.Text = "";
            this.ddl_Chucnangcha.Items.Clear();
            this.txt_URL.Text = "";
            this.txt_Mota.Text = "";
            this.ddl_Madoituong.Items.Clear();
            this.txt_STT.Text = "";
            this.chk_Hoatdong.Checked = false;
            this.chk_Quytrinh.Checked = false;
        }
        private void GetItem(int _id)
        {
            T_Chucnang _objChucnang = new T_Chucnang();
            ChucnangDAL _chucnangDAL = new ChucnangDAL();
            _objChucnang = _chucnangDAL.GetOneFromT_ChucnangByID(_id);
            this.txt_Tenchucnang.Text = _objChucnang.Ten_chucnang;
            ddl_Chucnangcha.SelectedIndex = UltilFunc.GetIndexControl(ddl_Chucnangcha, _objChucnang.Ma_Chucnang_Cha.ToString());
            this.txt_URL.Text = _objChucnang.URL_Chucnang;
            this.txt_Mota.Text = _objChucnang.Mota;
            this.txt_STT.Text = _objChucnang.STT.ToString();
            this.chk_Hoatdong.Checked = _objChucnang.HoatDong;
            if (_objChucnang.Quytrinh)
                this.chk_Quytrinh.Checked = _objChucnang.Quytrinh;
            else
                chk_Quytrinh.Enabled = false;
            if (_objChucnang.Ma_Doituong.Length > 0)
                ddl_Madoituong.SelectedValue = _objChucnang.Ma_Doituong.ToString();
            else
                ddl_Madoituong.Enabled = false;
            txt_lang.Text = _objChucnang.MenuEnglish;
        }
        private T_Chucnang SetItem()
        {
            T_Chucnang _obj = new T_Chucnang();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Ma_Chucnang = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Ma_Chucnang = 0;
            _obj.Ten_chucnang = UltilFunc.SqlFormatText(this.txt_Tenchucnang.Text.Trim());
            _obj.URL_Chucnang = this.txt_URL.Text.Trim();
            _obj.Mota = this.txt_Mota.Text.Trim();
            _obj.Icon = "";
            if (txt_STT.Text.Length > 0)
                if (UltilFunc.IsNumeric(this.txt_STT.Text.Trim()))
                    _obj.STT = Convert.ToInt32(this.txt_STT.Text.Trim());
            _obj.NgayTao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;
            _obj.HoatDong = this.chk_Hoatdong.Checked;
            _obj.Quytrinh = this.chk_Quytrinh.Checked;
            _obj.Ma_Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;
            _obj.Ma_Chucnang_Cha = Convert.ToInt32(this.ddl_Chucnangcha.SelectedValue.ToString());
            if (ddl_Madoituong.SelectedValue != "")
                _obj.Ma_Doituong = this.ddl_Madoituong.SelectedValue.ToString();
            if (txt_lang.Text.Trim() != "")
                _obj.MenuEnglish = txt_lang.Text.Trim();
            else
                _obj.MenuEnglish = "";
            return _obj;
        }
        private void BindCombo()
        {
            ddl_Chucnangcha.Items.Clear();
            UltilFunc.BindCombox(this.ddl_Chucnangcha, "Ma_Chucnang", "Ten_chucnang", "T_Chucnang", " 1=1 ", "---", "Ma_Chucnang_Cha");
            ddl_Madoituong.Items.Clear();
            UltilFunc.BindCombox(this.ddl_Madoituong, "Ma_Doituong", "Ma_Doituong", "T_Doituong", " 1=1 ", "---");
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
            Page.Response.Redirect("~/Menu/ListMenu.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
            ChucnangDAL _menuDAL = new ChucnangDAL();
            T_Chucnang _menu = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _menuDAL.Insert_T_Chucnang(_menu);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới Menu]-->[Thao tác Thêm][Menu_ID:" + _return.ToString() + " ]";

                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindCombo();
                Clear();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa Menu]-->[Thao tác sửa][Menu_ID:" + Page.Request["id"].ToString() + " ]";

                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                BindCombo();
                Clear();
                return;
            }

        }
        #endregion

        protected void chk_Quytrinh_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chk_Quytrinh.Checked)
                ddl_Madoituong.Enabled = true;
            else
                ddl_Madoituong.Enabled = false;
            ddl_Madoituong.AutoUpdateAfterCallBack = true;
        }
    }
}
