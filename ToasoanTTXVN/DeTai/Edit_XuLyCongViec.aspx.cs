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
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Word;
using Microsoft.Office.Core;
using System.Text.RegularExpressions;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.DeTai
{
    public partial class Edit_XuLyCongViec : System.Web.UI.Page
    {
        
        #region Variable Member
        int tab = 0;
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        string ActionsCode = string.Empty;
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
                    }
                }
            }
        }

        #region Method
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" Hoatdong=1 AND ID IN ({0}) Order by TenNgonNgu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Chọn ngôn ngữ---");
            if (ddlLang.Items.Count == 2)
                ddlLang.SelectedIndex = 1;
            else
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 AND Ma_Anpham= " + this.ddlLang.SelectedValue + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Chọn chuyên mục---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
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
                
                ChildID = Convert.ToInt32(Request["DT_id"].ToString());
                PopulateItem1(ChildID);

            }
        }
        void PopulateItem1(int _ID)
        {
            T_Allotments _obj = new T_Allotments();
            T_AllotmentDAL _objDAL = new T_AllotmentDAL();
            T_Idiea obj_Idiea = new T_Idiea();
            T_IdieaDAL obj_DAL = new T_IdieaDAL();

            _obj = _objDAL.GetOneFromT_AllotmentByID(_ID);
            obj_Idiea = obj_DAL.GetOneFromT_IdieaByID(_obj.Idiea_ID);


            ddlLang.SelectedValue = obj_Idiea.Lang_ID.ToString();

            cbo_chuyenmuc.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Ma_Anpham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Chọn chuyên mục---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

                
                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_Idiea.Cat_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                
            }

            txt_noidung.Text = _obj.Request;
            txt_noidungbaiviet.Text = obj_Idiea.Diea_Articles;
            cbb_Loai.SelectedValue = "1";



        }
        void PopulateItem(int _ID)
        {

            T_Idiea obj_Idiea = new T_Idiea();
            T_IdieaDAL obj_DAL = new T_IdieaDAL();


            obj_Idiea = obj_DAL.GetOneFromT_IdieaByID(_ID);


            Txt_tieude.Text = obj_Idiea.Title;

            ddlLang.SelectedValue = obj_Idiea.Lang_ID.ToString();

            cbo_chuyenmuc.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Ma_Anpham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Chọn chuyên mục---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                
                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_Idiea.Cat_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                
            }

            txt_noidung.Text = obj_Idiea.Comment;
            txt_noidungbaiviet.Text = obj_Idiea.Diea_Articles;
            cbb_Loai.SelectedValue = "1";


        }
        public T_Idiea SetItem()
        {
            T_IdieaDAL _objDAL = new T_IdieaDAL();
            T_Idiea obj_Idiea = new T_Idiea();
            T_Allotments obj_All = new T_Allotments();
            T_AllotmentDAL _DAL = new T_AllotmentDAL();
            if (Page.Request.Params["id"] != null)
            {
                int Diea_ID = Convert.ToInt32(Page.Request["id"].ToString());
                obj_Idiea = _objDAL.GetOneFromT_IdieaByID(Diea_ID);

            }
            obj_All = _DAL.GetOneFromT_AllotmentByIdieaID(int.Parse(Page.Request["DT_id"].ToString()));
            obj_Idiea.Lang_ID = Convert.ToInt32(this.ddlLang.SelectedValue.ToString());
            if (Txt_tieude.Text != "")
            {
                obj_Idiea.Title = UltilFunc.CleanFormatTags(Txt_tieude.Text);
            }
            if (txt_noidung.Text.Length > 0)
            {
                obj_Idiea.Comment = txt_noidung.Text;
            }
            if (int.Parse(cbo_chuyenmuc.SelectedIndex.ToString()) > 0)
            {
                obj_Idiea.Cat_ID = int.Parse(cbo_chuyenmuc.SelectedValue.ToString());
            }
            obj_Idiea.User_Created = _user.UserID;
            obj_Idiea.User_Duyet = _user.UserID;
            obj_Idiea.Date_Created = DateTime.Now;
            obj_Idiea.Date_Duyet = DateTime.Now;
            obj_Idiea.Diea_Stype = 2;
            obj_Idiea.Date_Edit = DateTime.Now;
            obj_Idiea.User_Edit = _user.UserID;
            obj_Idiea.Diea_Articles = txt_noidungbaiviet.Text;
            //Add by nvthai
            if (obj_All != null)
            {
                obj_Idiea.Date_From = obj_All.Date_start;
                obj_Idiea.Date_To = obj_All.Date_End;
                obj_Idiea.User_NguoiNhan = obj_All.User_NguoiNhan;
            }
            //end
            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }
            if (int.Parse(Page.Request["Tab"].ToString()) == 1)
            {
                obj_Idiea.Status = 33;
            }
            else
            {
                obj_Idiea.Status = 32;
            }

            obj_Idiea.CV_id = int.Parse(Page.Request["DT_id"].ToString());

            return obj_Idiea;
        }
        private bool CheckForm()
        {
            if (ddlLang.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (Txt_tieude.Text == "")
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (cbo_chuyenmuc.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (txt_noidung.Text == "")
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (txt_noidungbaiviet.Visible == true && txt_noidungbaiviet.Text == "")
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            return true;
        }
        #endregion

        #region Event click
        
        protected void linkExit_Click(object sender, EventArgs e)
        {
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("List_VietBaiPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + Request["DT_id"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("List_VietBaiPV.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + Request["DT_id"].ToString());
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            
            
            T_Idiea _t_idiea = SetItem();
            HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            int id = 0;
            if (_t_idiea.Diea_ID == 0)
            {
                // Insert
                id = _T_dieaDAL.InsertT_Idiea(_t_idiea);

                
                ActionsCode = "[Nhập mới Đề Tài PV:]-->[Thêm Mới][Tintuc_id:" + id.ToString() + " ]";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
            }
            else
            {
                // update
                id = _T_dieaDAL.InsertT_Idiea(_t_idiea);               
                ActionsCode = "[Tin đang xử lý PV:]-->[Sửa ][Tintuc_id:" + _t_idiea.Diea_ID.ToString() + " ]";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
            }
            Response.Redirect("Edit_XuLyCongViec.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&DT_id=" + Page.Request["DT_id"].ToString() + "&id=" + id.ToString() + "&Tab=" + Page.Request["Tab"].ToString());
        }
        protected void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Loai.SelectedIndex == 1)
            {
                this.lb_noidungbaiviet.Visible = false;
                this.txt_noidungbaiviet.Visible = false;
            }
            else
            {
                this.lb_noidungbaiviet.Visible = true;
                this.txt_noidungbaiviet.Visible = true;

            }
        }
        #endregion
        
    }
}
