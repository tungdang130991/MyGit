using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCBusinessLogic;
using HPCComponents;
using HPCBusinessLogic.DAL;
using HPCShareDLL;

namespace ToasoanTTXVN.Multimedia
{
    public partial class Multimedia_Published : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));

                    ActiverPermission();
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        Load_XB();

                    }
                }
            }
        }

        #region Load Methods
        protected void ActiverPermission()
        {
            this.btnHuyDang.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_DataGrid_Daxuatban_ctl01_chkAll');");
            this.btnHuyDangBottom.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lblBanmuonhuyXB") + "','ctl00_MainContent_DataGrid_Daxuatban_ctl01_chkAll');");
        }
        public void Load_XB()
        {
            //string _where = " 1 = 1 AND Languages_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") and Status = 3 ";
            string _where = " 1=1 and Status = 3 ";
            if (txtSearch.Text.Length > 0)
                _where += " AND " + string.Format(" Tittle like N'%{0}%'", UltilFunc.SqlFormatText(this.txtSearch.Text.Trim()));
            if (ddlLang.SelectedIndex > 0)
                _where += " AND " + string.Format(" Languages_ID = {0}", ddlLang.SelectedValue);
            if (this.ddlCategorys.SelectedIndex > 0)
                _where += " AND" + string.Format(" Category IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.ddlCategorys.SelectedValue);

            _where += " Order by T_Multimedia.DatePublish DESC";
            Pager_Xuatban.PageSize = 5;
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            DataSet _ds;
            _ds = _untilDAL.BindGridT_Multimedia(Pager_Xuatban.PageIndex, Pager_Xuatban.PageSize, _where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _untilDAL.BindGridT_Multimedia(Pager_Xuatban.PageIndex - 1, Pager_Xuatban.PageSize, _where);
            DataGrid_Daxuatban.DataSource = _ds.Tables[0];
            DataGrid_Daxuatban.DataBind();
            Pager_Xuatban.TotalRecords = CurrentPage_Xuatban.TotalRecords = TotalRecords;
            CurrentPage_Xuatban.TotalPages = Pager_Xuatban.CalculateTotalPages();
            CurrentPage_Xuatban.PageIndex = Pager_Xuatban.PageIndex;
        }
        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategorys.Items.Clear();
            if (ddlLang.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                //ddlCategorys.UpdateAfterCallBack = true;
            }
            else
            {
                ddlCategorys.DataSource = null;
                ddlCategorys.DataBind();
                //ddlCategorys.UpdateAfterCallBack = true;
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(ddlLang, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (ddlLang.Items.Count >= 3)
            {
                ddlLang.SelectedIndex = Global.DefaultLangID;
            }
            else
                ddlLang.SelectedIndex = UltilFunc.GetIndexControl(ddlLang, Global.DefaultCombobox);
            if (ddlLang.SelectedIndex != 0)
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(ddlCategorys, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham=" + this.ddlLang.SelectedValue.ToString() + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");

        }
        protected string ReturnObjectDisplay(object objPath, object objimg, object _ID)
        {
            string _extend = "";
            string _return = "";
            if (objPath.ToString().StartsWith("<iframe"))
            {
                try
                {
                    string _youReziWith = UltilFunc.ReplapceYoutoubeWidth(objPath.ToString(), "200");
                    string _youReziHeigh = UltilFunc.ReplapceYoutoubeHight(_youReziWith.ToString(), "180");
                    _return = _youReziHeigh.ToString();
                }
                catch
                { _return = ""; }
            }
            else
            {
                _extend = System.IO.Path.GetExtension(objPath.ToString().Split('/').GetValue(objPath.ToString().Split('/').Length - 1).ToString().Trim());
                if (_extend.ToLower() == ".jpg" || _extend.ToLower() == ".png" || _extend.ToLower() == ".gif" || _extend.ToLower() == ".jpeg" || _extend.ToLower() == ".bmp")
                {
                    _return = "<img style=\"max-width:180px;max-height:180px;\"  src=\"" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "\" alt=\"View\" onclick=\"openNewImage(this,'Close');\" />";
                }
                else if (_extend.ToLower() == ".flv" || _extend.ToLower() == ".wmv" || _extend.ToLower() == ".mp4")
                {
                    //_return = "<img style=\"max-width:180px;\" src=\"" + HPCComponents.Global.TinPathBDT + "/upload/Ads/admin/Icons/ico_video.jpg" + "\" alt=\"View\" onclick=\"xemquangcao('"+HPCComponents.Global.TinPathBDT + objPath.ToString() +"','');\" />";
                    string _viewFile = "";
                    _viewFile += "<div id=\"MediaPlayer\">";
                    _viewFile += "<div id=\"liveTV" + _ID + "\"></div>";
                    _viewFile += "<script type=\"text/javascript\">";
                    _viewFile += "jwplayer(\"liveTV" + _ID + "\").setup({";
                    _viewFile += " image: '" + HPCComponents.Global.TinPathBDT + objimg.ToString() + "',";
                    _viewFile += " file: '" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "',";
                    _viewFile += " width:200, height:180,primary: \"flash\"";
                    _viewFile += " });";
                    _viewFile += " </script>";
                    _return = _viewFile.ToString();
                }
                else if (_extend.ToLower() == ".swf")
                {
                    string _viewFile = "";
                    _viewFile += "<style>a:visited{color:blue;text-decoration:none}</style>";
                    _viewFile += "<body topmargin=\"0\" leftmargin=\"0\" marginheight=\"0\" marginwidth=\"0\"><center>";
                    _viewFile += "<div style=\"width:100%;height:100%;overflow:auto;\">";
                    _viewFile += "<embed width=\"200px\" height=\"45px\" type=\"application/x-shockwave-flash\"";
                    _viewFile += "src=\"" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "\" style=\"undefined\" id=\"Advertisement\" name=\"Advertisement\"";
                    _viewFile += "quality=\"high\" wmode=\"transparent\" allowscriptaccess=\"always\" flashvars=\"clickTARGET=_self&amp;clickTAG=#\"></embed></div>";

                    _return = _viewFile.ToString();
                }
                else
                {
                    _return = "<img  src=\"\" alt=\"No Image\" />";
                }

            }
            return _return;

        }
        #endregion

        #region Event Handle

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Multimedia/Edit_Multimedia.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());//+ "&ID=" +0
        }

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            Pager_Xuatban.PageIndex = 0;
            Load_XB();
        }
        protected void lbt_HuyXuatban_Click(object sender, EventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            foreach (DataGridItem item in DataGrid_Daxuatban.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("optSelect");
                TextBox txtGhichu = (TextBox)item.FindControl("txtGhichu");
                Label lblLogTitle = (Label)item.FindControl("lblLogTitle");
                if (check != null && check.Checked)
                {
                    int _ID = Convert.ToInt32(this.DataGrid_Daxuatban.DataKeys[item.ItemIndex].ToString());
                    string _sql = "Update T_Multimedia set Comment='" + txtGhichu.Text + "' where ID =" + _ID;
                    HPCDataProvider.Instance().ExecSql(_sql);
                    _untilDAL.UpdateStatusMultimedia(_ID, 2, _user.UserID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblLogTitle.Text, Request["Menu_ID"].ToString(), "HỦY XUẤT BẢN MULTIMEDIA", _ID, ConstAction.AmThanhHinhAnh);
                }
            }
            Load_XB();
        }

        #endregion

        #region Datagrid Handle

        public void DataGrid_Daxuatban_EditCommandEditor(object source, DataGridCommandEventArgs e)
        {
            T_MultimediaDAL _untilDAL = new T_MultimediaDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int _ID = Convert.ToInt32(this.DataGrid_Daxuatban.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Multimedia/Multimedia_Published_Edit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString());
            }
        }

        #endregion

        #region PageIndexChange

        protected void Pager_Xuatban_IndexChanged_Editor(object sender, EventArgs e)
        {
            Load_XB();
        }
        #endregion
    }
}
