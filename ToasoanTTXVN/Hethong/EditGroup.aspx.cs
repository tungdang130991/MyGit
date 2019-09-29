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
    public partial class EditGroup : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        DataBind();
                    }
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
        protected void linkSave_Click(object sender, EventArgs e)
        {
            #region GhiLog
            
            HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            this.Page.Validate(vs_Themmoi.ValidationGroup);
            if (!Page.IsValid) return;
            HPCBusinessLogic.DAL.NhomDAL _nhomnguoidungDAL = new HPCBusinessLogic.DAL.NhomDAL();
            T_Nhom _nhomnguoidung = SetItem();
            T_Nhom _isnhomnguoidung;
            int groupID = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                groupID = int.Parse(Request["ID"].ToString());

            _isnhomnguoidung = _nhomnguoidungDAL.GetGroupName(_nhomnguoidung.Ten_Nhom, groupID);
            if (_isnhomnguoidung != null)
            {
                FuncAlert.AlertJS(this,CommonLib.ReadXML("lblNhomdatontai"));
                return;
            }
            int _return = _nhomnguoidungDAL.Insert_T_Group(_nhomnguoidung);
            if (Page.Request.Params["id"] == null)
            {
                action.Thaotac = "[Thêm mới Group]-->[Thao tác Thêm][ID:" + _return.ToString() + " ]";
            }
            else
            {
                action.Thaotac = "[Sửa Group]-->[Thao tác Sửa][ID:" + Page.Request.Params["id"].ToString() + " ]";
            }
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
        }
        public void Clear()
        {
            this.txtName.Text = "";
            this.txtDesc.Text = "";
        }
        private T_Nhom SetItem()
        {
            T_Nhom _obj = new T_Nhom();
            if (Page.Request.Params["id"] != null)
            {
                _obj.Ma_Nhom = int.Parse(Page.Request["id"].ToString());
            }
            else _obj.Ma_Nhom = 0;
            _obj.Ten_Nhom = this.txtName.Text.Trim();
            _obj.MoTa = this.txtDesc.Text.Trim();
            _obj.NgayTao = DateTime.Now;
           
            return _obj;
        }
        private void PopulateItem(int _id)
        {
            T_Nhom _objSet = new T_Nhom();
            HPCBusinessLogic.DAL.NhomDAL _groupDAL = new HPCBusinessLogic.DAL.NhomDAL();
            _objSet = _groupDAL.GetOneFromT_NhomByID(_id);
            this.txtName.Text = _objSet.Ten_Nhom;
            this.txtDesc.Text = _objSet.MoTa;
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {

                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }

        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Hethong/ListGroup.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
    }
}
