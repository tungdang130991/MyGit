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
    public partial class Edit_DuyetDeTaiTBT : System.Web.UI.Page
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
            UltilFunc.BindCombox(ddlLang, "id", "TenNgonNgu", "T_NgonNgu", string.Format(" Hoatdong=1 and ID=" + HPCComponents.Global.DefaultCombobox + " AND ID IN ({0}) Order by TenNgonNGu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");
            ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, HPCComponents.Global.DefaultCombobox);
            if (ddlLang.SelectedIndex > 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Hoatdong=1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Chọn chuyên mục---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
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
        void PopulateItem(int _ID)
        {
            T_Idiea obj_Idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL obj_DAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
            obj_Idiea = obj_DAL.GetOneFromT_IdieaByID(_ID);
            Txt_tieude.Text = obj_Idiea.Title;
            ddlLang.SelectedValue = obj_Idiea.Lang_ID.ToString();

            if (ddlLang.SelectedIndex > 0)
            {

                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_Idiea.Cat_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
            }

            txt_noidung.Text = obj_Idiea.Comment;
            if (obj_Idiea.Diea_Stype == 1)
            {
                txt_noidungbaiviet.Text = obj_Idiea.Diea_Articles;
                cbb_Loai.SelectedValue = obj_Idiea.Diea_Stype.ToString();
            }
            else
            {
                lb_noidungbaiviet.Visible = false;
                txt_noidungbaiviet.Visible = false;
                cbb_Loai.SelectedValue = "2";
            }
        }
        public T_Idiea SetItem()
        {
            T_Idiea obj_Idiea = new T_Idiea();
            HPCBusinessLogic.DAL.T_IdieaDAL Dal = new HPCBusinessLogic.DAL.T_IdieaDAL();
            if (Page.Request.Params["id"] != null)
            {
                obj_Idiea = Dal.GetOneFromT_IdieaByID(int.Parse(Page.Request["id"].ToString()));

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
            obj_Idiea.Diea_Lock = true;
            obj_Idiea.Diea_Stype = int.Parse(cbb_Loai.SelectedValue.ToString());
            if (cbb_Loai.Visible == true)
            {
                obj_Idiea.Diea_Articles = txt_noidungbaiviet.Text;
            }
            if (Page.Request["Tab"] != null)
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            }
            if (tab == 0)
                obj_Idiea.Status = 62;
            else if (tab == -1)
                obj_Idiea.Status = 62;
            else if (tab == 1)
                obj_Idiea.Status = 63;

            return obj_Idiea;
        }
        private bool CheckForm()
        {
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
            if (ddlLang.SelectedIndex == 0)
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

            Dal.IsLock(double.Parse(Page.Request["id"].ToString()), 0, _user.UserID, DateTime.Now);
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                T_Idiea _t_idiea = SetItem();
                HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                int id = 0;
                if (ChildID == 0)
                {
                    // Insert
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);

                    ActionsCode = "[TBT Nhập mới Đề Tài:]-->[Thêm Mới][Tintuc_id:" + id.ToString() + " ]";

                }
                else
                {
                    // update
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Tin đang xử lý. TBT:]-->[Sửa ][Tintuc_id:" + _t_idiea.Diea_ID.ToString() + " ]";

                }
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                _T_dieaDAL.IsLock(_t_idiea.Diea_ID, 0, _user.UserID, DateTime.Now);//Unlock;
                if (int.Parse(Page.Request["Tab"].ToString()) == 1)
                    Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else
                    Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
            }
        }
        protected void linkDuyet_Click(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                T_Idiea _t_idiea = SetItem();
                HPCBusinessLogic.DAL.T_IdieaDAL _T_dieaDAL = new HPCBusinessLogic.DAL.T_IdieaDAL();
                int id = 0;
                if (ChildID == 0)
                {
                    // Insert
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);

                    ActionsCode = "[TBT Nhập mới Đề Tài:]-->[Thêm Mới][Tintuc_id:" + id.ToString() + " ]";

                }
                else
                {
                    // update
                    id = _T_dieaDAL.InsertT_Idiea(_t_idiea);
                    ActionsCode = "[Tin đang xử lý. TBT:]-->[Sửa ][Tintuc_id:" + _t_idiea.Diea_ID.ToString() + " ]";

                }
                _T_dieaDAL.Update_Status_tintuc(id, 22, _user.UserID, DateTime.Now, 0);
                _T_dieaDAL.Insert_Version_From_T_idiea_WithUserModify(id, 6, 22, _user.UserID, DateTime.Now);
                ActionsCode = "[Danh sách Đề tài đang chờ xử lý TBT:]-->[Gửi TPPV][Diea_ID:" + id + "]";
                UltilFunc.Log_Action(_user.UserID, _user.UserName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), ActionsCode);
                _T_dieaDAL.IsLock(_t_idiea.Diea_ID, 0, _user.UserID, DateTime.Now);//Unlock;
                if (int.Parse(Page.Request["Tab"].ToString()) == 1)
                    Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
                else
                    Response.Redirect("List_DuyetDeTaiTBT.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
            }
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
