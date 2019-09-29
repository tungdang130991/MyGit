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


namespace ToasoanTTXVN.Danhmuc
{
    public partial class EditAnPham : BasePage
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
                        //BindCombo();
                        BindData();
                    }
                }
            }
        }

        #region Method
        private void Clear()
        {
            this.txt_TenAnPham.Text = "";
            this.txtThumbnail.Text = "";
            this.txt_Mota.Text = "";
            this.txtThumbnail.Text = "";           
        }
        private void GetItem(int _id)
        {
            T_AnPham _objAnPham = new T_AnPham();
            AnPhamDAL _anphamDAL = new AnPhamDAL();
            _objAnPham = _anphamDAL.GetOneFromT_AnPhamByID(_id);
            this.txt_TenAnPham.Text = _objAnPham.Ten_AnPham;
            this.txt_Mota.Text = _objAnPham.Mota;
            this.txt_Sotrang.Text = _objAnPham.Sotrang.ToString();
            this.txtThumbnail.Text = _objAnPham.Url_Img;
        }
        private T_AnPham SetItem()
        {
            T_AnPham _obj = new T_AnPham();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Ma_AnPham = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Ma_AnPham = 0;
            _obj.Ten_AnPham = UltilFunc.SqlFormatText(this.txt_TenAnPham.Text.Trim());
            _obj.Url_Img = this.txtThumbnail.Text.Trim();
            _obj.Mota = this.txt_Mota.Text.Trim();
            _obj.Sotrang = Convert.ToInt16(this.txt_Sotrang.Text.Trim());
            _obj.Ngaytao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;      
            _obj.Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;         
          
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
            Page.Response.Redirect("~/Danhmuc/ListAnPham.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
             AnPhamDAL _anphamDAL = new AnPhamDAL();
             T_AnPham _anpham = SetItem();

             int menuID = 0;
             if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                 menuID = int.Parse(Request["ID"].ToString());
             int _return = _anphamDAL.InsertT_AnPham(_anpham);
             if (Page.Request.Params["id"] == null)
             {
                 action.Thaotac = "[Thêm mới ấn phẩm]-->[mã ấn phẩm:" + _return.ToString() + " ]";
                 
                 actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                 Clear();               
                 return;
             }
             if (Page.Request.Params["id"] != null)
             {
                 action.Thaotac = "[Sửa ấn phẩm]-->[Mã ấn phẩm:" + Page.Request["id"].ToString() + " ]";
                 
                 actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                 Clear();              
                 return;
             }
         }
        #endregion
    }
}
