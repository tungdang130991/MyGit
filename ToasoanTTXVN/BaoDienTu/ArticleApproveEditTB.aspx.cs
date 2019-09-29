using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Word;
using Microsoft.Office.Core;
using System.Text.RegularExpressions;
using HPCBusinessLogic.DAL;
using HPCApplication;
using System.Globalization;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ArticleApproveEditTB : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #region Variable Member
        private DataTable _dtKeywords
        {
            get { return (DataTable)Session["_dtKeywords"]; }
            set { Session["_dtKeywords"] = value; }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    this.btnSend.Attributes.Add("onclick", "return  CheckConfirmAll('Bạn có muốn gửi duyệt không?');");
                    this.LinkBack.Attributes.Add("onclick", "return  CheckConfirmAll('Bạn có muốn trả lại không?');");
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                    }
                }
            }
        }

        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cbo_lanquage.Items.Count >= 3)
                cbo_lanquage.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(this.cbo_lanquage, HPCComponents.Global.DefaultCombobox);
            if (cbo_lanquage.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                lblTitleCaption.Text = CommonLib.ReadXML("lblCapnhatbaiviet");
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int ChildID = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(ChildID);
                }
            }
            else
            {
                lblTitleCaption.Text = CommonLib.ReadXML("lblSuabaiviet");
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                this.cbHienthiAnh.Checked = false;
                //if (cbo_lanquage.SelectedIndex > 0)
                //{
                //    cbo_chuyenmuc.Items.Clear();
                //    if (cbo_lanquage.SelectedIndex >= 0)
                //    {
                        UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                        cbo_chuyenmuc.UpdateAfterCallBack = true;
                //    }
                //    else
                //    {
                //        this.cbo_chuyenmuc.DataSource = null;
                //        this.cbo_chuyenmuc.DataBind();
                //        this.cbo_chuyenmuc.UpdateAfterCallBack = true;
                //    }
                //}
            }
        }
        private void PopulateItem(int _id)
        {
            //Lấy ID trong T_AutoSave
            AutoSavesDAL _dal = new AutoSavesDAL();
            int id_autoSave = _dal.Get_ID_AutoSave(_id, _user.UserID);
            txtID.Text = id_autoSave.ToString();
            //end
            HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            HPCInfo.T_News _obj = new T_News();
            _obj = _untilDAL.load_T_news(_id);
            if (_obj != null)
            {

                this.txt_Author_name.Text = _obj.News_AuthorName;
                this.Txt_tieude.Text = _obj.News_Tittle;
                this.txt_TieuDePhu.Text = _obj.News_Sub_Title;
                this.txt_noidung.Text = _obj.News_Body;
                //this.ddlNews_Priority.SelectedValue = _obj.News_Priority.ToString();
                this.chk_IsCategorys.Checked = _obj.News_IsCategorys;
                this.chk_IsHomePages.Checked = _obj.News_IsHomePages;
                this.chk_IsCategoryParrent.Checked = _obj.News_IsCategoryParrent;
                this.chkHistorys.Checked = _obj.News_IsHistory;
                this.chkImages.Checked = _obj.News_IsImages;
                this.chkVideo.Checked = _obj.News_IsVideo;
                if (_obj.News_TienNB > 0.0)
                    this.txtTienNhuanBut.Text = string.Format("{0:#,#}", _obj.News_TienNB).Replace(".", ",");
                //this.ddlNews_IsType.SelectedValue = _obj.News_IsType.ToString();
                this.chkNewsIsFocus.Checked = _obj.News_IsFocus;
                this.chkNewsIsHot.Checked = _obj.News_IsHot;
                if (_obj.Images_Summary.Length > 0)
                    this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _obj.Images_Summary;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                this.Txt_Comments.Text = _obj.News_Comment;
                this.txt_tomtat.Text = _obj.News_Summary;
                this.txtThumbnail.Text = _obj.Images_Summary;
                this.txtTukhoa.Text = _untilDAL.GetKeywordsByNewsID(_id);

                this.cbo_lanquage.SelectedValue = _obj.Lang_ID.ToString();
                this.cbo_chuyenmuc.Items.Clear();
                if (cbo_lanquage.SelectedIndex > 0)
                {
                    UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by Ten_ChuyenMuc ASC");
                    cbo_chuyenmuc.UpdateAfterCallBack = true;
                    cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, _obj.CAT_ID.ToString());
                }
                else
                {
                    this.cbo_chuyenmuc.DataSource = null;
                    this.cbo_chuyenmuc.DataBind();
                    this.cbo_chuyenmuc.UpdateAfterCallBack = true;
                }
                //bind bai viet lien quan
                if (_obj.News_Realate.ToString().Trim() != "")
                    txtListID.Text = _obj.News_Realate.ToString().Trim().Replace(",0", "");
                LoadNewRealation();
                txtVideoPath.Text = _obj.News_PhotoAtt;
                txtChuthichanh.Text = _obj.News_DescImages;
                this.cbHienthiAnh.Checked = _obj.Image_Hot;
                this.txtNguon.Text = _obj.News_Nguon;
                this.cbDisplayMobile.Checked = _obj.News_DisplayMobile;
                this.cbMoreViews.Checked = _obj.News_Delete;
                //if (_obj.Image_Hot)
                //    this.cbHienthiAnh.Text = "Hiển thị trong tin chi tiết";
                //else
                //    this.cbHienthiAnh.Text = "Không hiển thị trong tin chi tiết";
                if (_untilDAL.Get_NewsVersion(_untilDAL.load_T_news(Convert.ToInt32(_id)).News_CopyFrom, 7, 92)
                    || _untilDAL.Get_NewsVersion(_untilDAL.load_T_news(Convert.ToInt32(_id)).News_CopyFrom, 7, 82))
                {
                   btn_Layout.Visible = true;
                }
                else
                {
                   btn_Layout.Visible = false;
                }

            }
        }
        private T_News SetItem(int _status)
        {
            T_NewsDAL Dal = new T_NewsDAL();
            T_News _obj = new T_News();
            if (Request["id"] != null)
                _obj.News_ID = int.Parse(Request["id"]);
            else _obj.News_ID = 0;
            _obj = Dal.load_T_news(int.Parse(Request["id"]));
            //if (Dal.Get_NewsVersion(int.Parse(Page.Request["id"].ToString()), 9, 73) == true)
            //    _obj.News_DatePublished = _obj.News_DatePublished;
            //else
            //    _obj.News_DatePublished = DateTime.Now;
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(txt_Author_name.Text.Trim()))
            {
                obj_BD.BD_ID = 0;
                obj_BD.UserID = _user.UserID;
                obj_BD.BD_Name = txt_Author_name.Text.Trim();
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _obj.News_TacgiaID = butdanhID;
            _obj.Lang_ID = Convert.ToInt32(this.cbo_lanquage.SelectedValue.ToString());
            _obj.CAT_ID = Convert.ToInt32(this.cbo_chuyenmuc.SelectedValue.ToString());
            _obj.News_Tittle = this.Txt_tieude.Text.Trim();
            _obj.Images_Summary = this.txtThumbnail.Text.Trim();
            _obj.News_Sub_Title = this.txt_TieuDePhu.Text.Trim();
            _obj.News_Summary = this.txt_tomtat.Text;
            _obj.News_Body = this.txt_noidung.Text.Trim();
            _obj.News_EditorID = _user.UserID;
            if (_status != 55)
                _obj.News_DateEdit = _obj.News_DateEdit;
            else
                _obj.News_DateEdit = DateTime.Now;
            if (_status == 6)
            {
                _obj.News_PublishNumber = DateTime.Now.Month;
                _obj.News_PublishYear = DateTime.Now.Year;
                _obj.News_PublishedID = _user.UserID;

                _obj.News_DatePublished = DateTime.Now;
                _obj.News_DateApproved = DateTime.Now;
                _obj.News_AprovedID = _user.UserID;
            }
            _obj.News_Comment = this.Txt_Comments.Text.Trim();
            _obj.News_AuthorName = this.txt_Author_name.Text.Trim();
            //if (ddlNews_Priority.Items.Count > 0)
            //    _obj.News_Priority = int.Parse(ddlNews_Priority.SelectedValue.ToString());
            _obj.News_IsCategorys = this.chk_IsCategorys.Checked;
            _obj.News_IsHomePages = this.chk_IsHomePages.Checked;
            _obj.News_IsCategoryParrent = this.chk_IsCategoryParrent.Checked;
            int tab = 0;
            if (Page.Request["Tab"] != null)
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            if (tab == 3)
                _obj.News_Status = ConstNews.NewsDelete;
            else if (tab == 0)
                _obj.News_Status = ConstNews.NewsApproving_tb;
            else if (tab == 1)
                _obj.News_Status = ConstNews.NewsReturn_tb;
            //_obj.Keywords = this.txtTukhoa.Text;
            _obj.News_IsHot = this.chkNewsIsHot.Checked;
            _obj.News_IsFocus = this.chkNewsIsFocus.Checked;
            _obj.News_Realate = "";
            _obj.News_IsImages = this.chkImages.Checked;
            _obj.News_IsVideo = this.chkVideo.Checked;
            _obj.News_IsHistory = this.chkHistorys.Checked;
            //obj_news.News_TienNB = 0;
            int tien = 0;
            if (this.txtTienNhuanBut.Text.Trim().Length > 0)
            {
                tien = int.Parse(txtTienNhuanBut.Text.Replace(",", ""));
                if (tien > 0)
                {
                    _obj.News_TienNB = tien;
                    _obj.News_Ngaycham = DateTime.Now;
                    _obj.News_NguoichamNBID = _user.UserID;
                }
                else
                {
                    _obj.News_TienNB = 0;
                    _obj.News_NguoichamNBID = 0;
                }
            }
            //Add By nvthai
            _obj.News_CopyFrom = 0;
            _obj.News_Realate = ReturnFilterListRelation();
            _obj.News_PhotoAtt = txtVideoPath.Text;
            _obj.News_DescImages = txtChuthichanh.Text;
            _obj.Image_Hot = cbHienthiAnh.Checked;
            _obj.News_Nguon = txtNguon.Text;
            _obj.News_DisplayMobile = cbDisplayMobile.Checked;
            _obj.News_Delete = cbMoreViews.Checked;
            return _obj;
        }

        private double Insert(Boolean _send)
        {
            T_News obj;
            HPCBusinessLogic.DAL.T_NewsDAL tt_DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            if (_send)
                obj = SetItem(ConstNews.NewsApproving_tbt);
            else
                obj = SetItem(ConstNews.NewsReturn_tk);
            if (obj.News_ID > 0)
            {
                obj.News_ID = tt_DAL.InsertT_news(obj);
                UltilFunc.Insert_News_Image(this.txt_noidung.Text.Trim(), obj.News_ID);
                if (_send)
                {
                    SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsApproving_tbt);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
                       Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin bài đang biên tập] [Gửi Duyệt tin bài]", obj.News_ID, ConstAction.BaoDT);
                }
                else
                {
                    SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsReturn_tk);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
                        Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin bài đang biên tập] [Trả lại Trình bày tin bài]", obj.News_ID, ConstAction.BaoDT);
                }
                return double.Parse(obj.News_ID.ToString());
            }
            else
                return 0;
        }
        private void SendPub(double id, int _status)
        {
            T_News obj_T_News = new T_News();
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            if (_status == ConstNews.NewsApproving_tbt)
                tt.Update_Status_tintuc(double.Parse(id.ToString()), _status, _user.UserID, DateTime.Now);
            else
                //Hungviet add
                tt.UpdateStatus_T_News_ex_New_HV(double.Parse(id.ToString()), _status, 0, DateTime.Now);
            tt.Insert_Version_From_T_News_WithUserModify(double.Parse(id.ToString()), ConstNews.NewsAppro_tb, _status, _user.UserID);

        }
        //insert keyword
        private void InsertKeyword(double News_ID, int User_ID)
        {
            if (txtTukhoa.Text.Length > 0)
            {
                HPCBusinessLogic.DAL.T_NewsDAL tt_DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                tt_DAL.InsertT_Keywords(txtTukhoa.Text, News_ID, User_ID);
            }
        }
        //end

        #endregion

        #region Event click
        protected void btnLayout_Click(object sender, EventArgs e)
        {
            int ChildID = Convert.ToInt32(Request["ID"]);
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _objNewsCurr = new T_NewsDAL().load_T_news(ChildID);
            int Get_T_News_ID = int.Parse(_objNewsCurr.News_CopyFrom.ToString());
            T_News _objNews = new T_News();
            if (tt.Get_NewsVersion(Get_T_News_ID, 7, 92) || tt.Get_NewsVersion(Get_T_News_ID, 7, 82))
            {
                _objNews = tt.load_T_news(Get_T_News_ID);
                this.txt_tomtat.Text = txt_tomtat.Text + "<br />" + _objNews.News_Summary;
                this.txt_noidung.Text = txt_noidung.Text + "<br />" + _objNews.News_Body;
            }
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            //dung them vao de kiem tra dieu dau vao và để busybox khong bi dung trong IE
            if (Txt_tieude.Text.Length <= 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (cbo_lanquage.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }

            if (cbo_chuyenmuc.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            {
                try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
                catch
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                    return;
                }
            }
            T_News _t_news = SetItem(ConstNews.NewsApproving_tb);
            int id = 0;
            T_NewsDAL _T_newsDAL = new T_NewsDAL();
            //T_News _objSet = SetItem(ConstNews.NewsApproving_tb);
            // Insert
            id = _T_newsDAL.InsertT_news(_t_news);
            // Insert keyword
            InsertKeyword(id, _user.UserID);
            UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
            int tab = 0;
            if (Request["ID"] == null)
            {
                if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
                {
                    tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                    if (tab == 10)
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Tin tức đang đăng] [Thao tác cập nhật]", id, ConstAction.BaoDT);
                    else
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin bài đang biên tập] [Thao tác cập nhật]", id, ConstAction.BaoDT);
                }
                else
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin tức đang chờ xuất bản:] [Thao tác cập nhật]", id, ConstAction.BaoDT);
            }
            else
            {
                if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
                {
                    tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                    if (tab == 10)
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Tin tức đang đăng] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
                    else
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin bài đang biên tập] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
                }
                else
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Biên tập tin bài] [Tin tức đang chờ xuất bản] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
            }
            if (_t_news.Images_Summary.Length > 0)
                this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + "/" + _t_news.Images_Summary;
            _T_newsDAL.IsLock(double.Parse(_t_news.News_ID.ToString()), 1, _user.UserID);
            if (Request["Tab"] != null && Request["Tab"].ToString() != "" && Request["Tab"].ToString() != String.Empty)
            {
                Response.Redirect("ArticleApproveEditTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString() + "&ID=" + id.ToString());
            }
            else
                Response.Redirect("ArticleApproveEditTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + id.ToString());
        }

        protected void LinkBack_Click(object sender, EventArgs e)
        {
            //dung them vao de kiem tra dieu dau vao và để busybox khong bi dung trong IE
            if (Txt_tieude.Text.Length <= 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (cbo_lanquage.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }

            if (cbo_chuyenmuc.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            {
                try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
                catch
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                    return;
                }
            }
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
            Insert(false);
            HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            Dal.IsLock(double.Parse(ChildID.ToString()), 0, 0);
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void btnGuiDuyet_Click(object sender, EventArgs e)
        {
            if (Txt_tieude.Text.Length <= 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (cbo_lanquage.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }

            if (cbo_chuyenmuc.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            {
                try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
                catch
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                    return;
                }
            }
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
            Insert(true);
            HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            Dal.IsLock(double.Parse(ChildID.ToString()), 0, 0);
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            HPCBusinessLogic.DAL.T_NewsDAL _DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            int tab = 0;
            if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                if (tab != 3)
                    _DAL.IsLock(double.Parse(ChildID.ToString()), 0, 0);
                if (tab == 10)
                    Response.Redirect("PublishedList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
                else
                    Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            }
            else
                Response.Redirect("ArticleApproveListTB.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        #endregion

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cbo_lanquage.SelectedIndex >= 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                this.cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
        private void clearForm()
        {
            this.txt_Author_name.Text = string.Empty;
            Txt_Comments.Text = string.Empty;
            txt_noidung.Text = string.Empty;
            Txt_tieude.Text = string.Empty;
            txt_TieuDePhu.Text = string.Empty;
            txt_tomtat.Text = string.Empty;
            txtTukhoa.Text = string.Empty;
            txtThumbnail.Text = string.Empty;
        }

        #region PHAN BAI LIEN QUAN
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadNewRealation();
        }
        public void LoadNewRealation()
        {
            if (txtListID.Text != "0")
            {
                try
                {
                    string _listID = txtListID.Text.Trim();
                    DataTable _dt = UltilFunc.GetAllNewsRelation(_listID);
                    dgListNewRelation.DataSource = _dt.DefaultView;
                    dgListNewRelation.DataBind();
                }
                catch { };

            }
            else
            {
                dgListNewRelation.DataSource = null;
                dgListNewRelation.DataBind();
            }
        }
        public void dgListNewRelation_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void dgListNewRelation_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "deletenewrelation")
            {
                string _list = null;
                string _ID = this.dgListNewRelation.DataKeys[e.Item.ItemIndex].ToString();
                string[] sArrProdID = null;
                char[] sep = { ',' };
                sArrProdID = txtListID.Text.Trim().Split(sep);
                for (int i = 0; i < sArrProdID.Length; i++)
                {
                    if (sArrProdID[i].ToString().Trim() != _ID.Trim())
                    {
                        if (_list == null)
                            _list += sArrProdID[i].ToString();
                        else _list += "," + sArrProdID[i].ToString();
                    }
                }
                if (_list != null)
                    txtListID.Text = _list.ToString();
                else
                    txtListID.Text = "0";
                LoadNewRealation();
            }
        }
        public string ReturnFilterListRelation()
        {
            string _return = null;
            foreach (DataGridItem m_Item in dgListNewRelation.Items)
            {
                if (_return == null)
                    _return += dgListNewRelation.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString();
                else _return += "," + dgListNewRelation.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString();
            }
            if (_return == null)
                _return = "0";
            return _return;
        }
        #endregion PHAN BAI LIEN QUAN
    }
}
