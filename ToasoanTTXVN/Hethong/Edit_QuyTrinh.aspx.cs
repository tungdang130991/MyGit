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
    public partial class Edit_QuyTrinh : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        T_Ten_Quytrinh _obj = new T_Ten_Quytrinh();
        HPCBusinessLogic.DAL.TenQuyTrinh_DAL _dalqt = new HPCBusinessLogic.DAL.TenQuyTrinh_DAL();
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
                        BindData();
                    }
                }
            }

        }
        #region Method
        private void Clear()
        {
            this.txt_TenQT.Text = "";
            this.txtThumbnail.Text = "";
            this.txt_Mota.Text = "";
            this.txtThumbnail.Text = "";
        }
        private void GetItem(int _id)
        {
            _obj = _dalqt.GetOneFromT_TenQuyTrinhByID(_id);
            this.txt_TenQT.Text = _obj.Ten_Quytrinh;
            this.txt_Mota.Text = _obj.Mota;
            this.txtThumbnail.Text = _obj.Url_Img;
        }
        private T_Ten_Quytrinh SetItem()
        {
            if (Page.Request.Params["id"] != null)
                _obj.ID = int.Parse(Page.Request["id"].ToString());
            else
                _obj.ID = 0;
            _obj.Ten_Quytrinh = this.txt_TenQT.Text.Trim();
            _obj.Url_Img = this.txtThumbnail.Text.Trim();
            _obj.Mota = this.txt_Mota.Text.Trim();
            _obj.Active = true;
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
            Page.Response.Redirect("~/Hethong/QuytrinhDynamic.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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

            _obj = SetItem();

            int menuID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                menuID = int.Parse(Request["ID"].ToString());
            int _return = _dalqt.InsertUpdateT_TenQuyTrinh(_obj);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới quy trình]-->[mã QT:" + _return.ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                return;
            }
            if (Page.Request.Params["id"] != null)
            {
                action.Thaotac = "[Sửa quy trình]-->[Mã QT:" + Page.Request["id"].ToString() + " ]";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                Clear();
                return;
            }
        }
        #endregion
    }
}
