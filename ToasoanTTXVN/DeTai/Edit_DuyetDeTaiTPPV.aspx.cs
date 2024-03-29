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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.DeTai
{
    public partial class Edit_DuyetDeTaiTPPV : System.Web.UI.Page
    {

        #region Variable Member
        int tab = 0;
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
        UltilFunc ulti = new UltilFunc();
        private int ChildID
        {
            get
            {
                if (ViewState["ChildID"] != null) return Convert.ToInt32(ViewState["ChildID"]);
                else return 0;
            }
            set { ViewState["ChildID"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                        if (Request["Tab"] != null)
                            tab = int.Parse(Request["Tab"].ToString());
                        if (tab == 0)
                            linkbtn_SendTPBT.Visible = false;
                        else
                            linkbtn_SendTPBT.Visible = true;
                    }
                }
            }
        }

        #region Method
        private void LoadComboBox()
        {
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            T_Idiea obj = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
            int _curentID = dal.GetOneFromT_IdieaByID(id).User_Created;

            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", " Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " and ID in (select Ma_Ngonngu from T_Nguoidung_NgonNgu where Ma_Nguoidung=" + _user.UserID + ")", "---Tất cả---");

            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and  Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
            UltilFunc.BindCombox(ddlGroup, "Ma_nhom", "Ten_nhom", "T_Nhom", " 1=1  Order by Ten_Nhom", "");
            BindComboxNguoiNhan(0);
        }
        private void BindComboxNguoiNhan(int _MaNhom)
        {
            DataTable _dt = new DataTable();
            DataTable _dtmanhom = new DataTable();
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            string _sqlselect = string.Empty;
            string _where = string.Empty;
            if (_MaNhom > 0)
            {
                _sqlselect = "select Ma_Nguoidung from T_Nguoidung_Nhom where Ma_Nhom=" + _MaNhom;
                _dtmanhom = ulti.ExecSqlDataSet(_sqlselect).Tables[0];
                string _ma_nguoidung = string.Empty;
                for (int i = 0; i < _dtmanhom.Rows.Count; i++)
                {
                    if (i == 0)
                        _ma_nguoidung = _dtmanhom.Rows[i][0].ToString();
                    else
                        _ma_nguoidung += "," + _dtmanhom.Rows[i][0].ToString();
                }

                _where = " IsDeleted = 0 and UserID in (" + _ma_nguoidung + ")";
                _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];
                cbo_NguoiNhan.Items.Add(new ListItem("--Chọn người nhận-- ", "0", true));
                cbo_NguoiNhan.DataSource = _dt;
                cbo_NguoiNhan.DataTextField = "UserFullName";
                cbo_NguoiNhan.DataValueField = "UserID";
                cbo_NguoiNhan.DataBind();


            }
            else
            {


                _where = " IsDeleted = 0 ";
                _dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];
                cbo_NguoiNhan.Items.Add(new ListItem("--Chọn người nhận-- ", "0", true));
                cbo_NguoiNhan.DataSource = _dt;
                cbo_NguoiNhan.DataTextField = "UserFullName";
                cbo_NguoiNhan.DataValueField = "UserID";
                cbo_NguoiNhan.DataBind();



            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_NguoiNhan.Items.Clear();
            if (ddlGroup.SelectedIndex > 0)
            {
                BindComboxNguoiNhan(Convert.ToInt32(this.ddlGroup.SelectedValue.ToString()));

            }
            else
            {
                BindComboxNguoiNhan(0);

            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {

                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    ChildID = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(ChildID);
                }
            }
            else
            {

            }
        }
        private void PopulateItem(int _ID)
        {
            T_Idiea obj_Idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL obj_DAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            obj_Idiea = obj_DAL.GetOneFromT_IdieaByID(_ID);
            Txt_tieude.Text = obj_Idiea.Title;
            txt_noidung.Text = obj_Idiea.Comment;
            ddlLang.SelectedValue = obj_Idiea.Lang_ID.ToString();
            cbo_chuyenmuc.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "ten_chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 and Ma_Anpham= " + this.ddlLang.SelectedValue + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_Idiea.Cat_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();

            }
            if (obj_Idiea.Diea_Stype == 1)
            {
                txt_noidungbaiviet.Text = obj_Idiea.Diea_Articles;
                cbb_Loai.SelectedValue = obj_Idiea.Diea_Stype.ToString();
                this.pnlEdit_Editor1.Visible = false;
            }
            else
            {
                if (Page.Request["Tab"].ToString() == "1")
                {
                    this.pnlEdit_Editor1.Visible = true;
                    lb_noidungbaiviet.Visible = true;
                    txt_noidungbaiviet.Visible = true;
                    cbb_Loai.SelectedValue = "2";
                    txt_noidungbaiviet.Text = obj_Idiea.Diea_Articles;
                    txt_FromDate.Text = obj_Idiea.Date_From.ToString();
                    txt_ToDate.Text = obj_Idiea.Date_To.ToString();
                    BindComboxNguoiNhan(0);
                    cbo_NguoiNhan.SelectedValue = obj_Idiea.User_NguoiNhan.ToString();
                    ddlGroup.Enabled = false;
                    cbo_NguoiNhan.Enabled = false;
                    txt_FromDate.Enabled = false;
                    txt_ToDate.Enabled = false;
                    cbb_LoaiBaiviet.Enabled = false;
                }
                else
                {
                    this.pnlEdit_Editor1.Visible = true;
                    lb_noidungbaiviet.Visible = false;
                    txt_noidungbaiviet.Visible = false;
                    cbb_Loai.SelectedValue = "2";

                }
            }
        }
        private void Gui_DuyetBT()
        {

            double DieaID = double.Parse(Page.Request["id"].ToString());
            T_Idiea _t_idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            HPCBusinessLogic.DAL.T_AllotmentDAL _objT_AllDAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
            T_Allotments _obj = new T_Allotments();
            T_Idiea _objIdiea = _T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString()));

            int id = 0;
            if (_objIdiea.Diea_Stype == 1)
            {
                _t_idiea = SetItem();
                id = _T_dieaDAL.InsertT_Idiea(_t_idiea);


                if (_T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString())).Diea_Lock == true && _T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString())).User_Edit != _user.UserID)
                {
                    FuncAlert.AlertJS(this, "Bài đang có người làm việc.!");
                    return;
                }
                _T_dieaDAL.IsLock(DieaID, 0, _user.UserID, DateTime.Now);
                _T_dieaDAL.Update_Status_tintuc(DieaID, 52, _user.UserID, DateTime.Now, 0);
                _T_dieaDAL.Insert_Version_From_T_idiea_WithUserModify(DieaID, 2, 52, _user.UserID, DateTime.Now);

                ActionsCode = "[Gửi Duyệt(TBT)][Diea_ID:" + DieaID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);

            }
            else
            {
                if (DateTime.Parse(txt_FromDate.Text.Trim(), new System.Globalization.CultureInfo("fr-FR")) > DateTime.Parse(txt_ToDate.Text.Trim(), new System.Globalization.CultureInfo("fr-FR")) || DateTime.Parse(txt_FromDate.Text.Trim() + " 23:59:59", new System.Globalization.CultureInfo("fr-FR")) < DateTime.Now)
                {
                    FuncAlert.AlertJS(this, "Ngày bắt đầu phải lớn hơn hoặc bằng ngày hiện tại và nhỏ hơn hoặc bằng ngày kết thúc!");
                    return;
                }

                if (cbo_NguoiNhan.SelectedIndex == 0)
                {
                    FuncAlert.AlertJS(this, "Trước khi lưu bạn phải chọn người nhận việc ");
                    return;
                }
                _obj = SetItem1();
                id = _objT_AllDAL.InsertT_Allotment(_obj);

                _t_idiea = SetItem();
                id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                if (_T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString())).Diea_Lock == true && _T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString())).User_Edit != _user.UserID)
                {
                    FuncAlert.AlertJS(this, "Bài đang có người làm việc.!");
                    return;
                }
                _T_dieaDAL.IsLock(DieaID, 0, _user.UserID, DateTime.Now);
                _T_dieaDAL.Update_Status_tintuc(DieaID, 52, _user.UserID, DateTime.Now, 0);
                _T_dieaDAL.Insert_Version_From_T_idiea_WithUserModify(DieaID, 2, 52, _user.UserID, DateTime.Now);

                ActionsCode = "[Gửi Duyệt(TBT)][Diea_ID:" + DieaID + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);

            }
            _T_dieaDAL.IsLock(DieaID, 0, _user.UserID, DateTime.Now);
            if (int.Parse(Page.Request["Tab"].ToString()) != 0)
                Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString());



        }
        public T_Idiea SetItem()
        {
            T_Idiea obj_Idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL _objDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            if (Page.Request.Params["ID"] != null && cbb_Loai.SelectedValue == "1")
            {
                obj_Idiea = _objDAL.GetOneFromT_IdieaByID(int.Parse(Page.Request["id"].ToString()));
                obj_Idiea.Diea_Stype = 1;
                obj_Idiea.Diea_Articles = txt_noidungbaiviet.Text;

            }
            else
            {
                obj_Idiea = _objDAL.GetOneFromT_IdieaByID(int.Parse(Page.Request["id"].ToString()));
                if (Page.Request["Tab"] != null)
                {
                    int _tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                    if (_tab == 2)
                        obj_Idiea.Status = 26;
                    else
                        obj_Idiea.Status = 23;
                }
                obj_Idiea.Diea_Stype = 2;
                obj_Idiea.Date_From = txt_FromDate.Text;
                obj_Idiea.Date_To = txt_ToDate.Text;
                obj_Idiea.User_NguoiNhan = int.Parse(cbo_NguoiNhan.SelectedValue);
            }
            if (Txt_tieude.Text != "")
            {
                obj_Idiea.Title = UltilFunc.CleanFormatTags(Txt_tieude.Text);
            }
            if (txt_noidung.Text.Length > 0)
            {
                obj_Idiea.Comment = txt_noidung.Text;
            }
            if (txt_noidungbaiviet.Text.Length > 0)
            {
                obj_Idiea.Diea_Articles = txt_noidungbaiviet.Text;
            }
            if (int.Parse(cbo_chuyenmuc.SelectedIndex.ToString()) > 0)
            {
                obj_Idiea.Cat_ID = int.Parse(cbo_chuyenmuc.SelectedValue.ToString());
            }
            obj_Idiea.User_Created = _user.UserID;
            obj_Idiea.User_Duyet = _user.UserID;
            obj_Idiea.Date_Created = DateTime.Now;
            obj_Idiea.Date_Duyet = DateTime.Now;
            obj_Idiea.Lang_ID = Convert.ToInt32(this.ddlLang.SelectedValue.ToString());
            obj_Idiea.Date_Edit = DateTime.Now;
            obj_Idiea.User_Edit = _user.UserID;


            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }

            return obj_Idiea;
        }
        public T_Allotments SetItem1()
        {
            int id = Convert.ToInt32(Page.Request["id"].ToString());
            T_Idiea _objIdiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL _idieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            _objIdiea = _idieaDAL.GetOneFromT_IdieaByID(id);
            T_Allotments _obj_Allotment = new T_Allotments();
            _obj_Allotment.Idiea_ID = id;
            _obj_Allotment.Lang_ID = Convert.ToInt32(this.ddlLang.SelectedValue.ToString());
            _obj_Allotment.User_Created = _user.UserID;
            _obj_Allotment.User_Duyet = _user.UserID;
            _obj_Allotment.Date_Created = DateTime.Now;

            _obj_Allotment.Type = Convert.ToInt32(cbb_LoaiBaiviet.SelectedValue);
            _obj_Allotment.Request = txt_noidung.Text;

            _obj_Allotment.Date_start = txt_FromDate.Text;
            _obj_Allotment.Date_End = txt_ToDate.Text;
            _obj_Allotment.Lock = false;
            _obj_Allotment.User_NguoiNhan = int.Parse(cbo_NguoiNhan.SelectedValue);
            _obj_Allotment.Cat_ID = _objIdiea.Cat_ID;
            _obj_Allotment.Title = Txt_tieude.Text.ToString();
            if (Page.Request["Tab"] != null)
            {
                int _tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                if (_tab == 0)
                    _obj_Allotment.Status = 32;
                else
                    _obj_Allotment.Status = 23;
            }

            return _obj_Allotment;
        }
        private bool CheckForm()
        {
            if (ddlLang.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ!");
                return false;
            }
            if (Txt_tieude.Text == "" || txt_noidung.Text == "" || cbo_chuyenmuc.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (txt_noidungbaiviet.Visible == true && txt_noidungbaiviet.Text == "")
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ!");
                return false;
            }
            if (cbb_Loai.SelectedValue == "2")
            {
                if (txt_FromDate.Text.ToString() == "" || txt_ToDate.Text.ToString() == "")
                {
                    FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Event click
        protected void linkExit_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
            Dal.IsLock(double.Parse(Page.Request["id"].ToString()), 0, _user.UserID, DateTime.Now);
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                double DieaID = double.Parse(Page.Request["id"].ToString());
                T_Idiea _t_idiea = new T_Idiea();
                HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                HPCBusinessLogic.DAL.T_AllotmentDAL _objT_AllDAL = new HPCBusinessLogic.DAL.T_AllotmentDAL();
                T_Allotments _obj = new T_Allotments();
                T_Idiea _objIdiea = _T_dieaDAL.GetOneFromT_IdieaByID(int.Parse(DieaID.ToString()));

                int id = 0;
                if (_objIdiea.Diea_Stype == 1)
                {
                    _t_idiea = SetItem();
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);

                    _T_dieaDAL.Insert_Version_From_T_idiea_WithUserModify(DieaID, 2, 23, _user.UserID, DateTime.Now);


                    ActionsCode = "[TPPV Sửa đề tài:]-->[ Sửa][Đề tài_ID=" + id.ToString() + " ]";
                    UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);

                }
                else
                {
                    if (DateTime.Parse(txt_FromDate.Text.Trim(), new System.Globalization.CultureInfo("fr-FR")) > DateTime.Parse(txt_ToDate.Text.Trim(), new System.Globalization.CultureInfo("fr-FR")) || DateTime.Parse(txt_FromDate.Text.Trim() + " 23:59:59", new System.Globalization.CultureInfo("fr-FR")) < DateTime.Now)
                    {
                        FuncAlert.AlertJS(this, "Ngày bắt đầu phải lớn hơn hoặc bằng ngày hiện tại và nhỏ hơn hoặc bằng ngày kết thúc!");
                        return;
                    }

                    if (cbo_NguoiNhan.SelectedValue == "0" || cbo_NguoiNhan.SelectedValue == "-1")
                    {
                        FuncAlert.AlertJS(this, "Trước khi lưu bạn phải chọn người nhận việc !");
                        return;
                    }
                    _obj = SetItem1();
                    id = _objT_AllDAL.InsertT_Allotment(_obj);
                    if (Page.Request["Tab"] != null)
                    {
                        int _tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                        if (_tab == 0)
                            _T_dieaDAL.Update_Status_tintuc(DieaID, 32, _user.UserID, DateTime.Now, 0);
                        else
                        {
                            _t_idiea = SetItem();
                            id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                            if (_tab == 2)
                                _T_dieaDAL.Update_Status_tintuc(DieaID, 26, _user.UserID, DateTime.Now, 0);
                            else
                                _T_dieaDAL.Update_Status_tintuc(DieaID, 23, _user.UserID, DateTime.Now, 0);


                        }
                    }

                    ActionsCode = "[TPPV Phân công công việc:]-->[PCCV][T_Allotment_ID=" + id + "]";
                    UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                }
                _T_dieaDAL.IsLock(DieaID, 0, _user.UserID, DateTime.Now);
                if (int.Parse(Page.Request["Tab"].ToString()) != 0)
                    Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else
                    Response.Redirect("List_DuyetDeTaiTPPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
            }
        }
        protected void linkbtn_SendTPBT_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                Gui_DuyetBT();
            }
        }
        protected void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Loai.SelectedIndex == 1)
            {
                this.pnlEdit_Editor1.Visible = true;


                this.lb_noidungbaiviet.Visible = false;
                this.txt_noidungbaiviet.Visible = false;
            }
            else
            {
                this.lb_noidungbaiviet.Visible = true;
                this.txt_noidungbaiviet.Visible = true;

                this.pnlEdit_Editor1.Visible = false;

            }
        }
        #endregion

    }
}
