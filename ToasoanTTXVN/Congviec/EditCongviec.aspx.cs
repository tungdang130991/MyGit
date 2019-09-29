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
using System.IO;

namespace ToasoanTTXVN.Congviec
{
    public partial class EditCongviec : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user = null;
        string _Pathfolder = string.Empty;
        string _Filename = string.Empty;
        string _FileExt = string.Empty;
        string _Savefile = string.Empty;
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
                        UltilFunc.BindCombox(cbo_room, "Ma_Phongban", "Ten_Phongban", "T_Phongban", " 1=1", "---All---");
                        BinddDropDownList(0);
                        BindData();
                    }
                }
            }
        }

        #region Method

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
            if (_objCongViec.Attachfile != "")
            {
                txt_attachfile.Visible = true;
                txt_attachfile.HRef = System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + _objCongViec.Attachfile;
            }
            else
            {
                txt_attachfile.Visible = false;
            }
        }
        private T_Congviec SetItem()
        {
            T_Congviec _obj = new T_Congviec();
            if (Page.Request.Params["id"] != null)
                _obj.Ma_Congviec = int.Parse(Page.Request["id"].ToString());
            else
                _obj.Ma_Congviec = 0;
            _obj.Tencongviec = txt_tencongviec.Text.Trim();
            _obj.Noidung_Congviec = txt_NoidungCV.Text.Trim();


            _obj.Sotu = 0;
            _obj.NgayTao = DateTime.Now;
            if (this.txt_NgayHT.Text.Length > 0)
                _obj.NgayHoanthanh = UltilFunc.ToDate(this.txt_NgayHT.Text, "dd/MM/yyyy");

            if (cbo_nguoinhan.SelectedIndex != 0)
            {
                _obj.NguoiNhan = Convert.ToDouble(cbo_nguoinhan.SelectedValue);
                _obj.TenNguoiNhan = cbo_nguoinhan.SelectedItem.Text.Trim();
            }
            else
            {
                _obj.NguoiNhan = 0;
                _obj.TenNguoiNhan = "";
            }
            _obj.Vet = txt_phanhoi.Text.Trim();
            _obj.Loai = 0;
            _obj.Status = 0;
            if (cbo_room.SelectedIndex != 0)
                _obj.Phong_ID = int.Parse(cbo_room.SelectedValue);
            else
                _obj.Phong_ID = 0;
            _obj.NguoiTao = _user.UserID;
            _obj.NguoiGiaoViec = _user.UserID;

            _Pathfolder = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
            int startchar = _Pathfolder.Substring(1, _Pathfolder.Length - 1).IndexOf("/");
            startchar += 1;
            if (Filedinhkem.PostedFile.FileName.Length > 0)
            {
                _FileExt = Path.GetExtension(Filedinhkem.PostedFile.FileName);
                _Filename = DateTime.Now.Millisecond.ToString() + Path.GetFileNameWithoutExtension(Filedinhkem.PostedFile.FileName) + _FileExt;
                _Savefile = _Pathfolder.Substring(startchar, _Pathfolder.Length - startchar);
                _obj.Attachfile = _Savefile + _Filename;
            }
            else
            {
                _obj.Attachfile = "";
            }
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
        public void BinddDropDownList(double maphong)
        {
            DataTable _dt = new DataTable();
            string _where = string.Empty;
            if (maphong > 0)
                _where = " IsDeleted = 0 and UserID <>" + _user.UserID + " and ProvinceID=" + maphong;
            else
                _where = " IsDeleted = 0 and UserID <>" + _user.UserID;
            _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];

            cbo_nguoinhan.DataSource = _dt;
            cbo_nguoinhan.DataTextField = "UserFullName";
            cbo_nguoinhan.DataValueField = "UserID";
            cbo_nguoinhan.DataBind();
            cbo_nguoinhan.Items.Insert(0, "---All---");
        }
        protected void ValidateNgayHT(object source, ServerValidateEventArgs args)
        {
            string str = args.Value;
            args.IsValid = false;

            if (UltilFunc.ToDate(this.txt_NgayHT.Text, "dd/MM/yyyy") < DateTime.Now)
            {
                FuncAlert.AlertJS(this, "Ngày hoàn thành phải lớn hơn ngày hiện tại!");
                return;
            }

            args.IsValid = true;
        }
        private void Uploadfile(string _pathfolder, string _filename, string _exfile)
        {
            _pathfolder = HttpContext.Current.Server.MapPath("/" + _pathfolder);
            if (_exfile.ToLower() == ".jpg" || _exfile.ToLower() == ".doc" || _exfile.ToLower() == ".docx" || _exfile.ToLower() == ".xls" || _exfile.ToLower() == ".pdf" || _exfile.ToLower() == ".txt" || _exfile.ToLower() == ".rar")
            {
                if (!Directory.Exists(_pathfolder))
                    Directory.CreateDirectory(_pathfolder);
                Filedinhkem.SaveAs(_pathfolder + @"\" + _filename);
            }
            else
            {
                FuncAlert.AlertJS(this, "Bạn không thể đính kèm file, hệ thống không cho phép!");
                return;
            }
        }
        #endregion

        #region Click
        protected void cbo_room_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_nguoinhan.Items.Clear();
            if (cbo_room.SelectedIndex > 0)
            {
                BinddDropDownList(int.Parse(cbo_room.SelectedValue));

            }
            else
            {
                BinddDropDownList(0);
            }


        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Session["CurrentPage"] = Session["PageIndex"];
            Page.Response.Redirect("~/Congviec/PhanCongCV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Request["Tab"].ToString());
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string Thaotac = string.Empty;
            this.Page.Validate(vs_Themmoi.ValidationGroup);
            if (!Page.IsValid) return;

            if (txt_tencongviec.Text.Trim() == "")
            {
                FuncAlert.AlertJS(this, "bạn chưa nhập tên công việc");
                return;
            }
            if (txt_NoidungCV.Text.Trim() == "")
            {
                FuncAlert.AlertJS(this, "bạn chưa nhập nội dung công việc");
                return;
            }
            if (txt_NgayHT.Text.Trim() == "")
            {
                FuncAlert.AlertJS(this, "bạn chưa nhập ngày hoàn thành công việc");
                return;
            }

            CongviecDAL _cvDAL = new CongviecDAL();
            T_Congviec _cv = SetItem();
            double _return = _cvDAL.InsertT_Congviec(_cv);
            if (_Filename.Length > 0 && _FileExt.Length > 0)
                Uploadfile(_Pathfolder, _Filename, _FileExt);
            if (Page.Request.Params["id"] == null)
            {
                Thaotac = "[Thêm mới công việc]-->[mã CV:" + _return.ToString() + " ]";

            }
            if (Page.Request.Params["id"] != null)
            {
                Thaotac = "[Sửa công việc]-->[Mã CV:" + Page.Request["id"].ToString() + " ]";

            }
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            Page.Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _return + "&Tab=1");
        }
        #endregion
    }
}
