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
    public partial class EditSobao : BasePage
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
            this.txt_Tensobao.Text = "";
            this.txt_NgayXB.Text = "";
            this.txt_Mota.Text = "";
            this.ddl_AnPham.Items.Clear();
        }
        private void GetItem(int _id)
        {
            T_Sobao _Sobao = new T_Sobao();
            SobaoDAL _sobaoDAL = new SobaoDAL();
            _Sobao = _sobaoDAL.GetOneFromT_SobaoByID(_id);
            this.txt_Tensobao.Text = _Sobao.Ten_Sobao;
            this.txt_Mota.Text = _Sobao.Mota;

            this.txt_NgayXB.Text = _Sobao.Ngay_Xuatban.ToString("dd/MM/yyyy");
            this.ddl_AnPham.SelectedIndex = UltilFunc.GetIndexControl(ddl_AnPham, _Sobao.Ma_AnPham.ToString());
            ddl_AnphamMau.Items.Clear();
            //UltilFunc.BindCombox(ddl_AnphamMau, "MA_Mau", "Mota", "T_AnPhamMau", " Ma_Anpham = " + _Sobao.Ma_AnPham.ToString());
        }
        private T_Sobao SetItem()
        {
            T_Sobao _obj = new T_Sobao();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Ma_Sobao = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Ma_Sobao = 0;
            _obj.Ten_Sobao = UltilFunc.SqlFormatText(this.txt_Tensobao.Text.Trim());
            _obj.Mota = this.txt_Mota.Text.Trim();

            if (this.txt_NgayXB.Text.Length > 0)
                _obj.Ngay_Xuatban = UltilFunc.ToDate(this.txt_NgayXB.Text, "dd/MM/yyyy");
            _obj.Ma_AnPham = Convert.ToInt32(this.ddl_AnPham.SelectedValue.ToString());
            _obj.Ngaytao = DateTime.Now;
            _obj.Ngaysua = DateTime.Now;
            _obj.Nguoitao = _user.UserID;
            _obj.Nguoisua = _user.UserID;


            return _obj;
        }
        private void BindCombo()
        {
            ddl_AnPham.Items.Clear();
            UltilFunc.BindCombox(this.ddl_AnPham, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", "---");
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

        protected void ValidateNgayXB(object source, ServerValidateEventArgs args)
        {
            string str = args.Value;
            args.IsValid = false;

            if (UltilFunc.ToDate(this.txt_NgayXB.Text, "dd/MM/yyyy") < DateTime.Now)
            {
                FuncAlert.AlertJS(this,"Ngày xuất bản số báo không được nhỏ hơn ngày hiện tại!");
                return;
            }

            args.IsValid = true;
        }


        #endregion

        #region Click
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Session["CurrentPage"] = Session["PageIndex"];
            Page.Response.Redirect("~/Danhmuc/ListSobao.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
            SobaoDAL _sobaoDAL = new SobaoDAL();
            Layout_SobaoDAL _layoutSobaoDAL = new Layout_SobaoDAL();
            T_Sobao _sobao = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _sobaoDAL.InsertT_Sobao(_sobao);
            if (Page.Request.Params["id"] == null)
            {
                _layoutSobaoDAL.AutoInsertT_Layout_Sobao(_return, Convert.ToInt32(ddl_AnPham.SelectedValue.ToString()));
                action.Thaotac = "[Thêm mới số báo]-->[mã số báo:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa số báo]-->[Mã số báo:" + Page.Request["id"].ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                BindCombo();
                return;
            }

        }
        protected void ddl_Anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddl_AnphamMau.Items.Clear();

            //if (ddl_AnPham.SelectedIndex > 0)
            //{
            //    UltilFunc.BindCombox(ddl_AnphamMau, "MA_Mau", "Mota", "T_AnPhamMau", " Ma_Anpham = " + ddl_AnPham.SelectedValue.ToString());
            //    ddl_AnphamMau.UpdateAfterCallBack = true;
            //    ddl_AnphamMau.AutoUpdateAfterCallBack = true;
            //}
            //else
            //{
            //    ddl_AnphamMau.DataSource = null;
            //    ddl_AnphamMau.DataBind();
            //    ddl_AnphamMau.UpdateAfterCallBack = true;
            //}

        }
        #endregion
    }
}
