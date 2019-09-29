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
using System.Collections.Generic;
using HPCShareDLL;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class Approved_List : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                Response.Redirect("~/Errors/AccessDenied.aspx");
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
           // this.link_HuyXB.Visible = _Role.R_Pub; 
            this.link_HuyXB.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_DataGrid_DaXB_ctl01_chkAll');");
            //this.LinkButton1.Visible = _Role.R_Pub; 
            this.LinkButton1.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_DataGrid_DaXB_ctl01_chkAll');");
            if (!IsPostBack)
            {
                string back = "";
                string text_search = "";
                try { back = Request["Back"].ToString(); }
                catch { ;}
                int pageindex = 0, langid = 0;
                LoadComboBox();
                if (!string.IsNullOrEmpty(back))
                {
                    try { text_search = Session["PSDaXB_TenPS"].ToString(); }
                    catch { ;}
                    try { langid = int.Parse(Session["PSDaXB_Langid"].ToString()); }
                    catch { ;}
                    try { pageindex = int.Parse(Session["PSDaXB_pagesindex"].ToString()); }
                    catch { ;}
                    txtSearch_Cate.Text = text_search;
                    cboNgonNgu.SelectedValue = langid.ToString();
                    Pager_DaXB.PageIndex = pageindex;
                }
                LoadPSXB();
            }
        }

        private void LoadComboBox()
        {
            cboNgonNgu.Items.Clear();
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
                cboNgonNgu.SelectedIndex = Global.DefaultLangID;
            else cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
        }

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            Pager_DaXB.PageIndex = 0; LoadPSXB();
        }

        protected void link_HuyXB_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL T_Album = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            T_Album_Categories _obj = new T_Album_Categories();
            foreach (DataGridItem item in DataGrid_DaXB.Items)
            {
                Label lblcatid = (Label)item.FindControl("lblcatid");
                int _id = Convert.ToInt32(lblcatid.Text);
                TextBox txtGhichu = (TextBox)item.FindControl("txtGhichu");
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                if (check.Checked)
                {
                    string _sql = "Update T_Album_Categories set Comment='" + txtGhichu.Text + "' where Cat_Album_ID =" + _id;
                    HPCDataProvider.Instance().ExecSql(_sql);
                    T_Album.Update_Status(_id, 5, _user.UserID);
                }
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Hủy xuất bản", Request["Menu_ID"].ToString(), "[Xuất bản] [Hủy xuất bản Phóng sự ảnh: " + T_Album.load_T_Album_Categories(Convert.ToInt32(lblcatid.Text)).Cat_Album_Name + "]", _id, ConstAction.GocAnh);
            }
            LoadPSXB();
        }
        public void LoadPSXB()
        {
            string where = " 1=1 and Cat_Album_Status_Approve = 4 AND Lang_ID IN (SELECT  DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
            if (!String.IsNullOrEmpty(this.txtSearch_Cate.Text.Trim()))
                where += " AND " + string.Format(" Cat_Album_Name like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch_Cate.Text.Trim()));
            if (cboNgonNgu.SelectedIndex > 0)
                where += " AND Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            where += " Order by Cat_Album_ID DESC";
            Pager_DaXB.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_DaXB.PageIndex, Pager_DaXB.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_CategoriesDynamic(Pager_DaXB.PageIndex - 1, Pager_DaXB.PageSize, where);
            DataGrid_DaXB.DataSource = _ds;
            DataGrid_DaXB.DataBind(); _ds.Clear();
            Pager_DaXB.TotalRecords = curentPages_DaXB.TotalRecords = TotalRecords;
            curentPages_DaXB.TotalPages = Pager_DaXB.CalculateTotalPages();
            curentPages_DaXB.PageIndex = Pager_DaXB.PageIndex;
            foreach (DataGridItem item in DataGrid_DaXB.Items)
            {
                ImageButton btnview = (ImageButton)item.FindControl("btnViewPhoto");
                Label lblcatid = (Label)item.FindControl("lblcatid");
                btnview.Attributes.Add("onclick", "PopupWindow('T_Album_Categories_View.aspx?catps=" + lblcatid.Text + "')");
                item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        protected void Pager_DaXB_IndexChanged(object sender, EventArgs e)
        {
            LoadPSXB();
        }

        public void DataGrid_DaXB_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.DataGrid_DaXB.DataKeys[e.Item.ItemIndex].ToString());
                Session["PSDaXB_pagesindex"] = Pager_DaXB.PageIndex;
                Session["PSDaXB_Langid"] = cboNgonNgu.SelectedValue;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["PSDaXB_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 3;
                Response.Redirect("~/PhongSuAnh/Album_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }

            if (e.CommandArgument.ToString().ToLower() == "editdisplay")
            {
                int catID = Convert.ToInt32(this.DataGrid_DaXB.DataKeys[e.Item.ItemIndex].ToString());
                HPCBusinessLogic.DAL.T_Album_CategoriesDAL obj_Cate = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                try
                {
                    bool check = obj_Cate.load_T_Album_Categories(catID).Status;
                    if (check)
                    {
                        obj_Cate.UpdateFromT_T_Album_CategoriesDynamic(" Status = 0 Where Cat_Album_ID = " + catID);
                    }
                    else
                    {
                        obj_Cate.UpdateFromT_T_Album_CategoriesDynamic(" Status = 1 Where Cat_Album_ID = " + catID);

                    }
                    this.LoadPSXB();
                }
                catch (Exception ex)
                { }
                finally
                {
                    obj_Cate = null;
                }
            }
            if (e.CommandArgument.ToString().ToLower() == "inputphoto")
            {
                int catID = Convert.ToInt32(this.DataGrid_DaXB.DataKeys[e.Item.ItemIndex].ToString());
                Session["PSDaXB_pagesindex"] = Pager_DaXB.PageIndex;
                Session["PSDaXB_Langid"] = cboNgonNgu.SelectedValue;
                if (!string.IsNullOrEmpty(txtSearch_Cate.Text))
                    Session["PSDaXB_TenPS"] = txtSearch_Cate.Text;
                Session["PageFromID"] = 3;
                Response.Redirect("~/PhongSuAnh/PhotoAlbum_EditMullti.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
        }

        public string LoadImage(object Url)
        {
            string _Url = "";
            try { _Url = Url.ToString(); }
            catch { ;}
            if (!string.IsNullOrEmpty(_Url))
                return CommonLib.tinpathBDT(_Url);
            else
                return "~/Dungchung/Images/no_images.jpg";
        }

        protected string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            return strReturn;
        }
    }
}
