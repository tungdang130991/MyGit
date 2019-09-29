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
    public partial class Edit_CTV : BasePage
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


        public T_Nguoidung SetItem()
        {
            T_Nguoidung _obj = new T_Nguoidung();
            if (Page.Request.Params["id"] != null)
                _obj.Ma_Nguoidung = int.Parse(Page.Request["id"].ToString());
            else
                _obj.Ma_Nguoidung = 0;
            _obj.Ten_Dangnhap = UltilFunc.SqlFormatText(this.txtUserName.Text.Trim());
            if (this.txtBirth.Text.Length > 0)
                _obj.Ngaysinh = UltilFunc.ToDate(this.txtBirth.Text, "dd/MM/yyyy");
            _obj.Diachi = this.txtAddress.Text.Trim();
            _obj.TenDaydu = this.txtFullName.Text.Trim();
            _obj.CMTND = txt_CMTND.Text.Trim();
            _obj.Mobile = this.txtPhoneNumber.Text.Trim();
            _obj.Email = this.txtEmail.Text.Trim();
            _obj.Hoatdong = false;
            _obj.NgayTao = DateTime.Now;
            _obj.NguoiTao = _user.UserID;
            _obj.Loai = 1;
            _obj.Ma_Vung = int.Parse(cbo_vungmien.SelectedValue.ToString());
            _obj.Image = txt_image.Text.Trim();
            return _obj;
        }
        private void PopulateItem(int _id)
        {
            T_Nguoidung obj = new T_Nguoidung();
            obj = _userDAL.GetOneFromT_NguoidungByID(_id);
            this.txtUserName.Text = obj.Ten_Dangnhap;
            this.txtEmail.Text = obj.Email;
            this.txtFullName.Text = obj.TenDaydu;
            this.txtPhoneNumber.Text = obj.Mobile;
            this.txtAddress.Text = obj.Diachi;
            this.txt_CMTND.Text = obj.CMTND;
            if (obj.Ngaysinh != null && obj.Ngaysinh != DateTime.MaxValue && obj.Ngaysinh != DateTime.MinValue)
                this.txtBirth.Text = obj.Ngaysinh.ToString("dd/MM/yyyy");
            cbo_vungmien.SelectedIndex = CommonLib.GetIndexControl(cbo_vungmien, obj.Ma_Vung.ToString());
            txt_image.Text = obj.Image;
            if (obj.Image != "" && obj.Image != null)
                ImgCTV.ImageUrl = Global.ApplicationPath + "/" + obj.Image;
            else
                ImgCTV.ImageUrl = Global.ApplicationPath + "/Dungchung/Images/no_images.jpeg";
        }

        private bool isExist(string CustomerName, double ID)
        {
            DataTable d = _userDAL.GetTenNguoiDungDynamic(" Ten_Dangnhap=N'" + CustomerName + "' and nguoitao=" + _user.UserID.ToString() + " and Ma_Nguoidung<>" + ID.ToString(), null).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExist(string CustomerName)
        {
            DataTable d = _userDAL.GetTenNguoiDungDynamic(" Ten_Dangnhap=N'" + CustomerName + "' and nguoitao=" + _user.UserID.ToString(), null).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        private bool isExistCMTND(string where)
        {
            DataTable d = _userDAL.GetTenNguoiDungDynamic(where, null).Tables[0];
            if (d.Rows.Count == 0)
                return false;
            else
                return true;
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string _thaotac = string.Empty;
            if (txtUserName.Text.Trim().Length == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa nhập bút danh CTV");
                return;
            }
            if (txtFullName.Text.Trim().Length == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa nhập họ tên đầy đủ CTV");
                return;
            }
            if (cbo_vungmien.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa nhập vùng miền CTV");
                return;
            }
            T_Nguoidung _obj = SetItem();
            int _return = 0;
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
                _return = int.Parse(Request["ID"].ToString());
            if (_return != 0)
            {
                if (!isExist(txtUserName.Text.Trim(), double.Parse(Page.Request["ID"].ToString())))
                {
                    string checkexitsCMTND = " CMTND=N'" + txt_CMTND.Text.Trim() + "' and Nguoitao=" + _user.UserID.ToString() + " and Loai=1 and Ma_Nguoidung<>" + Page.Request["id"].ToString();
                    if (!isExistCMTND(checkexitsCMTND))
                    {
                        _return = _userDAL.InsertT_Nguoidung(_obj);
                        _thaotac = "[Sửa CTV]-->[Thao tác sửa][CTVID:" + _return.ToString() + " ]";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);

                    }
                    else
                    {
                        FuncAlert.AlertJS(this, "CMTND đã tồn tại!");
                        return;
                    }
                }
                else
                {
                    FuncAlert.AlertJS(this, "Bút danh CTV này đã tồn tại!");
                    return;
                }
            }
            else
            {
                if (!Page.IsValid) return;
                if (!isExist(txtUserName.Text.Trim()))
                {
                    string checkexitsCMTND = " CMTND=N'" + txt_CMTND.Text.Trim() + "' and Loai=1 and Nguoitao=" + _user.UserID.ToString();
                    if (!isExistCMTND(checkexitsCMTND))
                    {
                        _return = _userDAL.InsertT_Nguoidung(_obj);
                        _thaotac = "[Thêm mới CTV]-->[Thao tác Thêm][CTVID:" + _return.ToString() + " ]";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("VALIDATE_ADDNEWS") + "');", true);

                    }
                    else
                    {
                        FuncAlert.AlertJS(this, "CMTND đã tồn tại!");
                        return;
                    }
                }
                else
                {

                    FuncAlert.AlertJS(this, "Bút danh CTV này đã tồn tại!");
                    return;

                }
            }            
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), _thaotac);
            FuncAlert.AlertJS(this, "Thêm mới CTV thành công");
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Hethong/List_CTV.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
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
    }
}
