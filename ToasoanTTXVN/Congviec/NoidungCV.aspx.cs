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

namespace ToasoanTTXVN.Congviec
{
    public partial class NoidungCV : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        UltilFunc ulti = new UltilFunc();
        private double nguoinhan = 0;
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
                        UltilFunc.BindCombox(cbo_room, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1", "---Select all---");

                        if (Request["ID"] != null)
                            GetItem(double.Parse(Request["ID"]));
                        if (nguoinhan != 0)
                        {
                            Linkhuycv.Visible = true;
                            linkSave.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblHoanthanh");
                        }
                        else
                        {
                            Linkhuycv.Visible = false;
                            linkSave.Text = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblNhanviec");
                        }
                    }
                }
            }
        }
        public void BinddDropDownList(double maphong)
        {
            DataTable _dt = new DataTable();
            string _where = string.Empty;
            if (maphong > 0)
                _where = " IsDeleted = 0 and UserID =" + _user.UserID + " and ProvinceID=" + maphong;
            else
                _where = " IsDeleted = 0 and UserID =" + _user.UserID;
            _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];

            cbo_nguoinhan.DataSource = _dt;
            cbo_nguoinhan.DataTextField = "UserFullName";
            cbo_nguoinhan.DataValueField = "UserID";
            cbo_nguoinhan.DataBind();
            cbo_nguoinhan.Items.Insert(0, "---Chọn người nhận---");
        }
        private void GetItem(double _id)
        {
            T_Congviec _objCongViec = new T_Congviec();
            CongviecDAL _CongViecDAL = new CongviecDAL();
            _objCongViec = _CongViecDAL.GetOneFromT_CongviecByID(_id);
            txt_tencongviec.Text = _objCongViec.Tencongviec;
            this.txt_NoidungCV.Text = _objCongViec.Noidung_Congviec;

            if (_objCongViec.Phong_ID > 0)
                this.cbo_room.SelectedIndex = CommonLib.GetIndexControl(cbo_room, _objCongViec.Phong_ID.ToString());
            BinddDropDownList(double.Parse(cbo_room.SelectedValue));
            if (_objCongViec.NguoiNhan > 0)
                this.cbo_nguoinhan.SelectedIndex = CommonLib.GetIndexControl(cbo_nguoinhan, _objCongViec.NguoiNhan.ToString());

            this.txt_NgayHT.Text = _objCongViec.NgayHoanthanh.ToString("dd/MM/yyyy");

            txt_phanhoi.Text = _objCongViec.Vet;
            nguoinhan = _objCongViec.NguoiNhan;
            if (_objCongViec.Attachfile != "")
            {
                txt_attachfile.Visible = true;
                txt_attachfile.HRef = System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + _objCongViec.Attachfile;
            }
            else
                txt_attachfile.Visible = false;
        }

        #region Click

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Congviec/PhanCongCV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            string Thaotac = string.Empty;
            if (nguoinhan != 0)
            {
                Thaotac = "Thực hiện hoàn thành công việc: " + txt_tencongviec.Text.Trim();
                sql = "update T_Congviec set Status=1, NguoiNhan=" + _user.UserID + ", TenNguoiNhan=N'" + _user.UserFullName + "', Vet=N'" + txt_phanhoi.Text.Trim() + "' where Ma_Congviec=" + Request["ID"];
            }
            else
            {
                Thaotac = "Thực hiện nhận công việc: " + txt_tencongviec.Text.Trim();
                sql = "update T_Congviec set Vet=N'" + txt_phanhoi.Text.Trim() + "', NguoiNhan=" + _user.UserID + ", TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + Request["ID"];
            }
            ulti.ExecSql(sql);

            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            Page.Response.Redirect("~/Congviec/PhanCongCV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=0");
        }
        protected void Linkhuycv_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            string Thaotac = string.Empty;

            Thaotac = "Thực hiện nhận công việc: " + txt_tencongviec.Text.Trim();
            sql = "update T_Congviec set Status=0,Vet=N'" + txt_phanhoi.Text.Trim() + "',NguoiNhan=0, TenNguoiNhan=N'" + _user.UserFullName + "' where Ma_Congviec=" + Request["ID"];

            ulti.ExecSql(sql);

            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            Page.Response.Redirect("~/Congviec/PhanCongCV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=0");
        }
        #endregion
    }
}
