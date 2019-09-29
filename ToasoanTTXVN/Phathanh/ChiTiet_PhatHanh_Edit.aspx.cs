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
    public partial class ChiTiet_PhatHanh_Edit : System.Web.UI.Page
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
            ddl_AnPham.Items.Clear();
            UltilFunc.BindCombox(ddl_AnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ");
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
            this.ddl_AnPham.Items.Clear();
            this.ddl_Sobao.Items.Clear();
            this.txt_NgayPhatHanh.Text = "";
            this.txt_SoluongPH.Text = "";
            this.txt_SoluongTon.Text = "";
            this.txt_Ghichu.Text = "";
        }
        private void GetItem(int _id)
        {
            T_Phathanh _obj = new T_Phathanh();
            PhathanhDAL _objDAL = new PhathanhDAL();
            _obj = _objDAL.GetOneFromT_PhathanhByID(_id);
            this.ddl_AnPham.SelectedIndex = UltilFunc.GetIndexControl(ddl_AnPham, _obj.Ma_Anpham.ToString());
          
            if (_id > 0)
            {
                this.ddl_Sobao.Items.Clear();
                UltilFunc.BindCombox(ddl_Sobao, "Ma_Sobao", "Ten_Sobao", "T_Sobao", " Ma_AnPham = " + _obj.Ma_Anpham.ToString());
                this.ddl_Sobao.SelectedIndex = UltilFunc.GetIndexControl(ddl_Sobao, _obj.Ma_Sobao.ToString());
                this.txt_NgayPhatHanh.Text = _obj.Ngay_Phathanh.ToString("dd/MM/yyyy");
            }             
            this.txt_SoluongPH.Text = _obj.Soluong_PH.ToString();
            this.txt_SoluongTon.Text= _obj.Soluong_ton.ToString();          
            this.txt_Ghichu.Text = _obj.Ghichu;
        }
        private T_Phathanh SetItem()
        {
            T_Phathanh _obj = new T_Phathanh();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Id = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Id = 0;

            _obj.Ma_Anpham = Convert.ToInt32(this.ddl_AnPham.SelectedValue.ToString());
            _obj.Ma_Sobao = Convert.ToInt32(this.ddl_Sobao.SelectedValue.ToString());
            if (this.txt_NgayPhatHanh.Text.Length > 0)
                _obj.Ngay_Phathanh = UltilFunc.ToDate(this.txt_NgayPhatHanh.Text, "dd/MM/yyyy");    
            if(txt_SoluongPH.Text.Length > 0)
                _obj.Soluong_PH = Convert.ToInt32(this.txt_SoluongPH.Text.Trim());
            if(txt_SoluongTon.Text.Length >0 )
                _obj.Soluong_ton = Convert.ToInt32(this.txt_SoluongTon.Text.Trim());
            _obj.Ghichu = this.txt_Ghichu.Text.Trim();
            
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
        protected void ddl_AnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_AnPham.SelectedIndex > 0)
            {
                ddl_Sobao.Items.Clear();
                UltilFunc.BindCombox(ddl_Sobao, "Ma_Sobao", "Ten_Sobao", "T_Sobao", " Ma_AnPham = " + ddl_AnPham.SelectedValue.ToString() + " AND Ma_Sobao not in (select Ma_Sobao from T_Phathanh where Ma_AnPham = " + ddl_AnPham.SelectedValue.ToString() + ")");
                ddl_Sobao.UpdateAfterCallBack = true;
            }
        }
        protected void ddl_Sobao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Sobao.SelectedIndex > 0)
            {
                T_Sobao _sobao = new T_Sobao();
                SobaoDAL _sobaoDAL = new SobaoDAL();
                _sobao = _sobaoDAL.GetOneFromT_SobaoByID(Convert.ToInt32(ddl_Sobao.SelectedValue.ToString()));
                this.txt_NgayPhatHanh.Text = _sobao.Ngay_Xuatban.ToString("dd/MM/yyyy");                
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
            PhathanhDAL _phathanhDAL = new PhathanhDAL();
            T_Phathanh _phathanh = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _phathanhDAL.InsertT_Phathanh(_phathanh);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới T_PhatHanh]-->[Mã:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa T_PhatHanh]-->[Mã :" + Page.Request["id"].ToString() + " ]";
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
            Page.Response.Redirect("~/Phathanh/ChiTiet_PhatHanh_List.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
       
     
        #endregion
    }
}
