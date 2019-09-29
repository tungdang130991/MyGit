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
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ArticleSearch : BasePage
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user = null;
        protected T_RolePermission _Role = null;
        #endregion

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
                        LoadCombox();
                        LoadTacgia();
                    }
                }
            }
        }

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cboNgonNgu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }

        protected void cmd_Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Search();
        }
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            T_NewsVersion _obj_T_NewsVecion = new T_NewsVersion();
            if (e.CommandArgument.ToString().ToLower() == "downloadalias")
            {
                int _ID = Convert.ToInt32(this.dgr_tintuc.DataKeys[e.Item.ItemIndex].ToString());
                LoadFileDoc(_ID);
            }
            Search();
        }

        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void pages_IndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        public void LoadCombox()
        {
            cboNgonNgu.Items.Clear();
            cbo_chuyenmuc.Items.Clear();
            UltilFunc.BindCombox(cboNgonNgu, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cboNgonNgu.Items.Count >= 3)
            {
                cboNgonNgu.SelectedIndex = Global.DefaultLangID;
            }
            else
                cboNgonNgu.SelectedIndex = UltilFunc.GetIndexControl(cboNgonNgu, Global.DefaultCombobox);
            if (cboNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            cbo_chuyenmuc.UpdateAfterCallBack = true;
            cboNgonNgu.UpdateAfterCallBack = true;
        }
        public void Search1()
        {
            string Str_Search = GetSQLSearch();
            if (!string.IsNullOrEmpty(Str_Search))
            {
                pages.PageSize = Global.MembersPerPage;
                HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                DataSet _ds;
                _ds = _T_newsDAL.Search_All_News(pages.PageIndex, pages.PageSize, Str_Search);
                int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
                if (TotalRecord == 0)
                    _ds = _T_newsDAL.Search_All_News(pages.PageIndex - 1, pages.PageSize, Str_Search);
                dgr_tintuc.DataSource = _ds;
                dgr_tintuc.DataBind();
                pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
                CurrentPage2.TotalPages = pages.CalculateTotalPages();
                CurrentPage2.PageIndex = pages.PageIndex;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Trường ngày tháng phải nhập đúng theo định dang dd/MM/yyyy!');", true);
            }

        }

        public void Search()
        {
            string Str_Search = GetSQLSearch();
            if (!string.IsNullOrEmpty(Str_Search))
            {
                pages.PageSize = Global.MembersPerPage;
                HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                DataSet _ds;
                _ds = _T_newsDAL.BindGridT_NewsSearchEditor(pages.PageIndex, pages.PageSize, Str_Search);
                if (_ds != null)
                {
                    try
                    {
                        int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
                        int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
                        if (TotalRecord == 0)
                            _ds = _T_newsDAL.BindGridT_NewsSearchEditor(pages.PageIndex - 1, pages.PageSize, Str_Search);

                        if (TotalRecord > 0)
                        {
                            dgr_tintuc.DataSource = _ds;
                            dgr_tintuc.DataBind();
                            pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
                            CurrentPage2.TotalPages = pages.CalculateTotalPages();
                            CurrentPage2.PageIndex = pages.PageIndex;
                            Panel_DS_Ketqua.Visible = true;
                        }
                        else
                        {
                            dgr_tintuc.DataSource = null;
                            dgr_tintuc.DataBind();
                            pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                            CurrentPage2.TotalPages = 1;
                            CurrentPage2.PageIndex = 1;
                            Panel_DS_Ketqua.Visible = false;
                        }
                    }
                    catch
                    {
                        dgr_tintuc.DataSource = null;
                        dgr_tintuc.DataBind();
                        pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                        CurrentPage2.TotalPages = 1;
                        CurrentPage2.PageIndex = 1;
                    }
                }
                else
                {
                    dgr_tintuc.DataSource = null;
                    dgr_tintuc.DataBind();
                    pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                    CurrentPage2.TotalPages = 1;
                    CurrentPage2.PageIndex = 1;
                    Panel_DS_Ketqua.Visible = false;
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("msgXacnhanngaythang") + "');", true);
            }

        }

        public string GetSQLSearch()
        {
            string sql = "";
            string sClause = " 1=1 and CAT_ID in (select DISTINCT(T_Nguoidung_Chuyenmuc.Ma_chuyenmuc) from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";
            string sWhere = "";


            //if (cboNgonNgu.SelectedIndex > 0)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  Lang_ID=" + cboNgonNgu.SelectedValue.ToString();
            //}
            if (cbo_chuyenmuc.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "" + string.Format(" CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.cbo_chuyenmuc.SelectedValue);

            }
            if (Drop_Status_Public.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  News_Status=" + Drop_Status_Public.SelectedValue.ToString();
            }
            if (Drop_Tacgia.SelectedIndex > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  News_AuthorID=" + Drop_Tacgia.SelectedValue.ToString();
            }
            //if (chkNewsIsBaidinh.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsHomePages=1 ";
            //}
            //if (chkNewsIsFocus.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsHot=1 ";
            //}

            //if (chkNewTieudiem.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsFocus=1 ";
            //}
            //if (chkNewFocusParent.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsCategoryParrent=1 ";
            //}
            if (chkNewFocusChild.Checked)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  News_IsCategorys=1 ";
            }

            //if (chkImageIsFocus.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsImages=1 ";
            //}
            //if (chkVideoIsFocus.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsVideo=1 ";
            //}
            //if (chkHosoIsFocus.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_IsHistory=1 ";
            //}
            //if (cbDisplayMobi.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_DisplayMobile=1 ";
            //}
            //if (cbMoreViews.Checked)
            //{
            //    if (sWhere.Trim() != "") sWhere += " AND ";
            //    sWhere += "  News_Delete=1 ";
            //}
            if (!string.IsNullOrEmpty(txt_tieude.Text.Trim()))
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += " News_Tittle LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            }
            if (txt_tungay.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  News_DatePublished >= convert(datetime,'" + txt_tungay.Text.Trim() + " 00:00:00',103) ";
            }
            if (txt_denngay.Text.Length > 0)
            {
                if (sWhere.Trim() != "") sWhere += " AND ";
                sWhere += "  News_DatePublished <= convert(datetime,'" + txt_denngay.Text.Trim() + " 23:59:59',103) ";
            }
            sql += sClause;
            if (sWhere.Trim().Length > 0)
                sql += " AND" + sWhere;
            return sql + " Order by News_DatePublished DESC ";

        }

        public void LoadFileDoc(int _ID)
        {
            string strHTML = "";
            HPCBusinessLogic.DAL.T_NewsDAL dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News obj = dal.load_T_news(_ID);
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b>" + obj.News_Tittle + "<o:p></o:p></b></p>";
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b><br>" + obj.News_Summary + "<u1:p></u1:p></b></p>";
            strHTML += "<p style='text-align:justify'>" + obj.News_Body + "<o:p></o:p></p>";
            if (strHTML.Length > 0)
                SaveAsText(strHTML);
        }
        private void SaveAsText(string _arr_IN)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            DirectoryInfo r = new DirectoryInfo(HttpContext.Current.Server.MapPath(HPCComponents.Global.GetAppPath(Request)));
            FileInfo[] file;
            file = r.GetFiles("*.doc");
            foreach (FileInfo i in file)
            {
                File.Delete(r.FullName + "\\" + i.Name);
            }
            strFileName = _user.UserName + "_HoaSiTrinhBay_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            Page.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }
        public void LoadTacgia()
        {
            this.Drop_Tacgia.Items.Clear();
            UltilFunc.BindCombox(Drop_Tacgia, "Ma_Nguoidung", "TenDaydu", "T_Nguoidung", "1=1 Order by TenDaydu ASC", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTatca"));
        }
        public string GetstatusXB(object status)
        {
            string _statusOutput = "";
            try
            {
                int _status = Convert.ToInt32(status);
                switch (_status)
                {
                    case 1: _statusOutput = CommonLib.ReadXML("lblNhaptinbai"); break;
                    case 7: _statusOutput = CommonLib.ReadXML("lblTrinhbay"); break;
                    case 8: _statusOutput = CommonLib.ReadXML("lblBientap"); break;
                    case 9: _statusOutput = CommonLib.ReadXML("lblDuyettinbai"); break;
                    case 12: _statusOutput = CommonLib.ReadXML("lblTinmoi"); break;
                    case 13: _statusOutput = CommonLib.ReadXML("lblTralainguoinhaptin"); break;
                    case 72: _statusOutput = CommonLib.ReadXML("lblChotrinhbay"); break;
                    case 73: _statusOutput = CommonLib.ReadXML("lblTralaitrinhbay"); break;
                    case 82: _statusOutput = CommonLib.ReadXML("lblChobientap"); break;
                    case 83: _statusOutput = CommonLib.ReadXML("lblTralaibientap"); break;
                    case 92: _statusOutput = CommonLib.ReadXML("lblBaichoduyet"); break;
                    case 4: _statusOutput = CommonLib.ReadXML("lblTinngungdang"); break;
                    case 6: _statusOutput = CommonLib.ReadXML("lblTinxuatban"); break;
                    case 55: _statusOutput = CommonLib.ReadXML("lblTindaxoa"); break;
        
                }
            }
            catch { ;}
            return _statusOutput;
        }

        public string GetstatusNoibat(object status, object Focus, object ishot)
        {
            string _status = "";
            int i = 0;
            try
            {
                _status = status.ToString();
                if (_status == "1")
                {
                    _status = CommonLib.ReadXML("lblBinhthuong"); i++;
                }
                else if (_status == "2")
                {
                    _status = CommonLib.ReadXML("lblNoibatchuyenmuc"); i++;
                }
                else if (_status == "3")
                {
                    _status = CommonLib.ReadXML("lblNoibattrangchu"); i++;
                }
            }
            catch { ;}
            string str_focus = "";
            string str_ishost = "";
            try
            {
                str_focus = Focus.ToString();
                if (str_focus.Trim() == "1" || str_focus.Trim().ToLower() == "true")
                {
                    if (i > 0)
                    {
                        _status = status + "<br />" + CommonLib.ReadXML("lblTieudiem"); i++;
                    }
                    else { _status = status + CommonLib.ReadXML("lblTieudiem"); i++; }
                }
            }
            catch { ;}
            try
            {
                str_ishost = ishot.ToString();
                if (str_ishost.Trim() == "1" || str_focus.Trim().ToLower() == "true")
                {
                    if (i > 0)
                    {
                        _status = status + "<br />" + CommonLib.ReadXML("lblTinnong"); i++;
                    }
                    else { _status = status + CommonLib.ReadXML("lblTinnong"); i++; }
                }
            }
            catch { ;}
            return _status;
        }
    }
}
