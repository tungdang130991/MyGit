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
    public partial class Edit_Idiea : System.Web.UI.Page
    {
        #region Variable Member
        int tab = 0;
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
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
            UltilFunc.BindCombox(ddlLang, "ID", "TenNgonNgu", "T_NgonNgu", " Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " and ID in (select Ma_Ngonngu from T_Nguoidung_NgonNgu where Ma_Nguoidung=" + _user.UserID + ")", "---Tất cả---");
            
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and  Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                this.pnlEdit_Editor.GroupingText = "Cập nhật đề tài";
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    ChildID = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(ChildID);
                }
            }
            else
            {
                this.pnlEdit_Editor.GroupingText = "Thêm mới đề tài";
            }
        }
        protected void PopulateItem(int _ID)
        {
            T_Idiea obj_Idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL obj_DAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            obj_Idiea = obj_DAL.GetOneFromT_IdieaByID(_ID);
            Txt_tieude.Text = obj_Idiea.Title;
            ddlLang.SelectedValue = obj_Idiea.Lang_ID.ToString();
            cbo_chuyenmuc.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + ddlLang.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");
                
                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_Idiea.Cat_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
               
            }

            txt_noidung.Text = obj_Idiea.Comment;

        }
        public T_Idiea SetItem()
        {
            T_Idiea obj_Idiea = new T_Idiea();
            if (Page.Request.Params["id"] != null)
            {
                obj_Idiea.Diea_ID = Convert.ToInt32(Page.Request["id"].ToString());
            }
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
            obj_Idiea.Lang_ID = Convert.ToInt32(this.ddlLang.SelectedValue.ToString());
            obj_Idiea.Date_Edit = DateTime.Now;
            obj_Idiea.User_Edit = _user.UserID;
            obj_Idiea.Diea_Stype = 2;

            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }
            if (tab == 0)
                obj_Idiea.Status = 12;
            else if (tab == -1)
                obj_Idiea.Status = 12;
            else if (tab == 1)
                obj_Idiea.Status = 13;

            return obj_Idiea;
        }
        private bool CheckForm()
        {
            if (ddlLang.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, "Trước khi lưu bạn phải nhập thông tin cho các phần (*) mầu đỏ !");
                return false;
            }
            if (Txt_tieude.Text == "" || cbo_chuyenmuc.SelectedIndex == 0 || txt_noidung.Text == "")
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
            HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();

            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("List_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("List_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                T_Idiea _t_idiea = SetItem();
                HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                int id = 0;
                string ActionsCode = string.Empty;
                if (ChildID == 0)
                {
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Nhập mới Đề Tài PV:]-->[Thêm Mới][Tintuc_id:" + id.ToString() + " ]";
                }
                else
                {
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Tin đang xử lý PV:]-->[Sửa ][Tintuc_id:" + _t_idiea.Diea_ID.ToString() + " ]";
                }
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                Response.Redirect("Edit_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString() + "&ID=" + id.ToString());
            }
        }
        protected void linkSend_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                T_Idiea _t_idiea = SetItem();
                HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                int id = 0;
                string ActionsCode = string.Empty;
                if (ChildID == 0)
                {
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Nhập mới Đề Tài PV:]-->[Thêm Mới][Tintuc_id:" + id.ToString() + " ]";
                }
                else
                {
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Tin đang xử lý PV:]-->[Sửa ][Tintuc_id:" + _t_idiea.Diea_ID.ToString() + " ]";
                }
                _T_dieaDAL.Update_Status_tintuc(id, 62, _user.UserID, DateTime.Now, 0);

                _T_dieaDAL.Insert_Version_From_T_idiea_WithUserModify(id, 1, 62, _user.UserID, DateTime.Now);

                ActionsCode = "[Danh sách Đề tài đang chờ xử lý PV:]-->[Gửi Duyêt đề tài (TBT)][Diea_ID:" + id + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                if (Page.Request["Tab"].ToString() != "-1")
                    Response.Redirect("List_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else
                    Response.Redirect("List_Idiea.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
            }
        }
        #endregion


    }
}
