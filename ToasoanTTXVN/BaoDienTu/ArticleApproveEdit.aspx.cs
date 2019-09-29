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

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ArticleApproveEdit : BasePage
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
                    this.LinkSend.Attributes.Add("onclick", "return  CheckConfirmAll('Bạn có muốn gửi duyệt không?');");
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
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1", CommonLib.ReadXML("lblTatca"));
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
                lblTitleCaption.Text = CommonLib.ReadXML("titCapnhatbaiviet");
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int ChildID = Convert.ToInt32(Request["ID"]);
                    PopulateItem(ChildID);
                }
            }
            else
            {
                lblTitleCaption.Text = CommonLib.ReadXML("titSuabaiviet");
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                this.cbHienthiAnh.Checked = false;
                cbo_chuyenmuc.Items.Clear();
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by Ten_ChuyenMuc ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }

        protected void PopulateItem(int _ID)
        {
            //Lấy ID trong T_AutoSave
            AutoSavesDAL _dal = new AutoSavesDAL();
            int id_autoSave = _dal.Get_ID_AutoSave(_ID, _user.UserID);
            txtID.Text = id_autoSave.ToString();
            //end
            T_News obj_T_news = new T_News();
            T_NewsDAL ObjDAl = new T_NewsDAL();
            obj_T_news = ObjDAl.load_T_news(_ID);
            this.Txt_tieude.Text = obj_T_news.News_Tittle.ToString();
            this.txtTukhoa.Text = ObjDAl.GetKeywordsByNewsID(_ID);
            this.txt_TieuDePhu.Text = obj_T_news.News_Sub_Title.ToString();
            this.txt_tomtat.Text = obj_T_news.News_Summary.ToString();
            this.txt_noidung.Text = obj_T_news.News_Body.ToString();
            this.Txt_Comments.Text = obj_T_news.News_Comment.ToString();
            this.txt_Author_name.Text = obj_T_news.News_AuthorName.ToString();
            this.txtThumbnail.Text = obj_T_news.Images_Summary;
            if (obj_T_news.Images_Summary.Length > 0)
                this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + obj_T_news.Images_Summary;
            else this.ImgTemp.Attributes.CssStyle.Add("display", "none");

            this.cbo_lanquage.SelectedValue = obj_T_news.Lang_ID.ToString();

            this.chk_IsCategorys.Checked = obj_T_news.News_IsCategorys;
            this.chk_IsHomePages.Checked = obj_T_news.News_IsHomePages;
            this.chk_IsCategoryParrent.Checked = obj_T_news.News_IsCategoryParrent;
            this.chkNewsIsFocus.Checked = obj_T_news.News_IsFocus;
            this.chkNewsIsHot.Checked = obj_T_news.News_IsHot;
            this.chkHistorys.Checked = obj_T_news.News_IsHistory;
            this.chkImages.Checked = obj_T_news.News_IsImages;
            this.chkVideo.Checked = obj_T_news.News_IsVideo;
            //if (obj_T_news.News_TienNB > 0.0)
            //    this.txtTienNhuanBut.Text = string.Format("{0:#,#}", obj_T_news.News_TienNB).Replace(".", ",");
            this.cbo_chuyenmuc.Items.Clear();
            if (cbo_lanquage.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format("  HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + "  AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
                cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, obj_T_news.CAT_ID.ToString());
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                this.cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            //bind bai viet lien quan
            if (obj_T_news.News_Realate.ToString().Trim().Length > 0)
                txtListID.Text = obj_T_news.News_Realate.ToString().Trim().Replace(",0", "");
            LoadNewRealation();
            //Add By nvthai
            //obj_T_news.News_Priority == 1;
            txtVideoPath.Text = obj_T_news.News_PhotoAtt;
            txtChuthichanh.Text = obj_T_news.News_DescImages;
            this.cbHienthiAnh.Checked = obj_T_news.Image_Hot;
            this.txtNguon.Text = obj_T_news.News_Nguon;
            this.cbDisplayMobile.Checked = obj_T_news.News_DisplayMobile;
            this.cbMoreViews.Checked = obj_T_news.News_Delete;
            int Copyfrom = obj_T_news.News_CopyFrom;
            if (Copyfrom > 0)
            {
                ChuyenmucDAL _catDAL = new ChuyenmucDAL();
                int catID = ObjDAl.load_T_news(Copyfrom).CAT_ID;
                lblcm.Text = HPCBusinessLogic.UltilFunc.GetCategoryName(catID);
            }
            else
                lblcm.Text = "";
            if (ObjDAl.Get_NewsVersion(ObjDAl.load_T_news(Convert.ToInt32(_ID)).News_CopyFrom, 7, 92)
                || ObjDAl.Get_NewsVersion(ObjDAl.load_T_news(Convert.ToInt32(_ID)).News_CopyFrom, 7, 82))
            {
                btn_Layout.Visible = true;
            }
            else
            {
                btn_Layout.Visible = false;
            }
        }

        private double Insert(Boolean _send)
        {
            T_News obj;
            T_NewsDAL tt_DAL = new T_NewsDAL();
            int news_id = 0;
            if (Page.Request.Params["id"] != null)
                news_id = Convert.ToInt32(Page.Request["id"]);
            if (_send)
            {
                obj = SetItem(ConstNews.NewsApproving_tb);
            }
            else
                obj = SetItem(ConstNews.NewsReturn);
            if (obj.News_ID > 0)
            {
                obj.News_ID = tt_DAL.InsertT_news(obj);
                if (_send)
                {
                    //if (obj.Lang_ID == 1)
                    //{
                    //    obj.News_Status = ConstNews.NewsApproving_tbt;
                    //    SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsApproving_tbt);
                    //    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
                    //        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin bài đang chờ trình bày] [Gửi Duyệt tin bài]", obj.News_ID, ConstAction.BaoDT);
                    //}
                    //else
                    //{
                        obj.News_Status = ConstNews.NewsApproving_tb;
                        SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsApproving_tb);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin bài đang chờ trình bày] [Gửi Biên tập tin bài]", obj.News_ID, ConstAction.BaoDT);
                    //}
                }
                else
                {
                    obj.News_Status = ConstNews.NewsReturn;
                    SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsReturn);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
                        Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin bài đang chờ trình bày] [Trả lại người nhập tin]", obj.News_ID, ConstAction.BaoDT);
                }
                return double.Parse(obj.News_ID.ToString());
            }
            else
                return 0;
            //T_News obj;
            //HPCBusinessLogic.DAL.T_NewsDAL tt_DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            //obj = SetItem();
            //obj.News_ID = tt_DAL.InsertT_news(obj);
            //int tab = 0;
            //if (_send)
            //{
            //    SendPub(double.Parse(obj.News_ID.ToString()));
            //    if (Page.Request["Tab"] != null)
            //        tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            //    if (tab == 1)
            //    {
            //        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
            //                Request["Menu_ID"].ToString(), "[Viết bài mới] [Cập nhật tin bài bị trả lại] [Thao tác gửi thư ký duyệt]", obj.News_ID);
            //    }
            //    else
            //    {
            //        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
            //                Request["Menu_ID"].ToString(), "[Viết bài mới] [Cập nhật tin bài] [Thao tác gửi thư ký duyệt]", obj.News_ID);
            //    }
            //}
            //return double.Parse(obj.News_ID.ToString());
        }
        private void SendPub(double id, int _status)
        {
            T_News obj_T_News = new T_News();
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            tt.Update_Status_tintuc(double.Parse(id.ToString()), _status, _user.UserID, DateTime.Now);
            tt.Insert_Version_From_T_News_WithUserModify(double.Parse(id.ToString()), ConstNews.NewsAppro_tk, _status, _user.UserID);

        }
        //private void SendPub(double id)
        //{
        //    HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
        //    tt.Update_Status_tintuc(double.Parse(id.ToString()), ConstNews.NewsApproving_tb, _user.UserID, DateTime.Now);
        //    tt.Insert_Version_From_T_News_WithUserModify(double.Parse(id.ToString()), 1, ConstNews.NewsApproving_tb, _user.UserID);
        //}
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
            if (Txt_tieude.Text == "")
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
            //if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            //{
            //    try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
            //    catch
            //    {
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Tiền nhận bút phải là kiểu số nguyên!');", true);
            //        return;
            //    }
            //}
            T_News _t_news = SetItem(0);
            int id = 0;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            // Insert
            id = _T_newsDAL.InsertT_news(_t_news);
            // Insert keyword
            InsertKeyword(id, _user.UserID);
            if (Request["ID"] == null)
            {
                int tab = 0;
                if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
                {
                    tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                    if (tab == 10)
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Tin tức đang đăng] [Thao tác cập nhật]", id, ConstAction.BaoDT);
                    else
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin bài đang chờ trình bày:]-->[Thao tác thêm mới]", id, ConstAction.BaoDT);
                }
                else
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin tức đang chờ trình bày:]-->[Thao tác thêm mới]", id, ConstAction.BaoDT);
            }
            else
            {
                int tab = 0;
                if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
                {
                    tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                    if (tab == 10)
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Tin tức đang đăng] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
                    else
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài] [Tin bài đang chờ trình bày] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
                }
                else
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _t_news.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Trình bày tin bài]  [Tin tức đang chờ trình bày] [Thao tác cập nhật]", _t_news.News_ID, ConstAction.BaoDT);
            }
            UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(id.ToString()));
            if (_t_news.Images_Summary.Length > 0)
                this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + "/" + _t_news.Images_Summary;
            _T_newsDAL.IsLock(double.Parse(_t_news.News_ID.ToString()), 1, _user.UserID);
            if (Request["Tab"] != null && Request["Tab"].ToString() != "" && Request["Tab"].ToString() != String.Empty)
            {
                Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString() + "&ID=" + id.ToString());
            }
            else
                Response.Redirect("ArticleApproveEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + id.ToString());
        }
        protected void LinkSend_Click(object sender, EventArgs e)
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
            //if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            //{
            //    try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
            //    catch
            //    {
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Tiền nhận bút phải là kiểu số nguyên!');", true);
            //        return;
            //    }
            //}
            if (Page.IsValid)
            {
                double news_id = 0;
                news_id = Insert(true);
                if (Page.Request["id"] == null || string.IsNullOrEmpty(Page.Request["id"]))
                {
                    UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), news_id);
                }
                else
                {
                    UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
                }
                HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();

                linkExit_Click(sender, e);
            }
        }
        protected void LinkBack_Click(object sender, EventArgs e)
        {
            //dung them vao de kiem tra dieu dau vao và để busybox khong bi dung trong IE
            if (Txt_tieude.Text == "")
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
            //if (!string.IsNullOrEmpty(txtTienNhuanBut.Text))
            //{
            //    try { int.Parse(txtTienNhuanBut.Text.Replace(",", "")); }
            //    catch
            //    {
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Tiền nhận bút phải là kiểu số nguyên!');", true);
            //        return;
            //    }
            //}
            //------------------
            UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
            Insert(false);
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            Dal.IsLock(double.Parse(ChildID.ToString()), 0, 0);
            if (Page.Request["Tab"].ToString() != "-1")
                Response.Redirect("ArticleApproveList.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            else
                Response.Redirect("ArticleApproveList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            int tab = 0;
            if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                if (tab != 3)
                {
                    HPCBusinessLogic.DAL.T_NewsDAL _DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                    _DAL.IsLock(double.Parse(ChildID.ToString()), 0, 0);
                }
                if (tab == 10)
                    Response.Redirect("PublishedList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
                else
                    Response.Redirect("ArticleApproveList.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            }
            else
                Response.Redirect("ArticleApproveList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
        protected T_News SetItem()
        {
            HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News obj_news = new T_News();
            if (Page.Request.Params["id"] != null)
            {
                obj_news.News_ID = Convert.ToInt32(Page.Request["id"].ToString());
                obj_news.News_EditorID = _user.UserID;
                obj_news = Dal.load_T_news(int.Parse(Request["id"]));
                if (obj_news.News_Status != 55)
                    obj_news.News_DateEdit = DateTime.Now;
                else
                    obj_news.News_DateEdit = obj_news.News_DateEdit;
            }
            else
            {
                obj_news.News_ID = 0;
                obj_news.News_DateCreated = DateTime.Now;
                obj_news.News_AuthorID = _user.UserID;
            }
            if (Txt_tieude.Text.Length > 0)
                obj_news.News_Tittle = UltilFunc.CleanFormatTags(Txt_tieude.Text);
            if (txt_TieuDePhu.Text.Length > 0)
                obj_news.News_Sub_Title = UltilFunc.CleanFormatTags(txt_TieuDePhu.Text);
            if (txt_tomtat.Text.Length > 0)
                obj_news.News_Summary = txt_tomtat.Text;
            if (int.Parse(cbo_chuyenmuc.SelectedIndex.ToString()) > 0)
                obj_news.CAT_ID = int.Parse(cbo_chuyenmuc.SelectedValue.ToString());
            if (int.Parse(cbo_lanquage.SelectedIndex.ToString()) > 0)
                obj_news.Lang_ID = int.Parse(cbo_lanquage.SelectedValue.ToString());
            //obj_news.News_Priority = int.Parse(ddlNews_Priority.SelectedValue.ToString());
            obj_news.News_IsImages = this.chkImages.Checked;
            obj_news.News_IsVideo = this.chkVideo.Checked;
            obj_news.News_IsHistory = this.chkHistorys.Checked;
            int tien = 0;
            if (this.txtTienNhuanBut.Text.Trim().Length > 0)
            {
                tien = int.Parse(txtTienNhuanBut.Text.Replace(",", ""));
                if (tien > 0)
                {
                    obj_news.News_TienNB = tien;
                    obj_news.News_Ngaycham = DateTime.Now;
                    obj_news.News_NguoichamNBID = _user.UserID;
                }
                else
                {
                    obj_news.News_TienNB = 0;
                    obj_news.News_NguoichamNBID = 0;
                }
            }
            if (txt_noidung.Text.Length > 0)
                obj_news.News_Body = txt_noidung.Text;
            obj_news.Images_Summary = this.txtThumbnail.Text.Trim();
            obj_news.News_AuthorName = UltilFunc.CleanFormatTags(txt_Author_name.Text);
            int butdanhID = 0;
            //T_Butdanh obj_BD = new T_Butdanh();
            //HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            //if (!string.IsNullOrEmpty(txt_Author_name.Text.Trim()))
            //{
            //    obj_BD.BD_ID = 0;
            //    obj_BD.UserID = _user.UserID;
            //    obj_BD.BD_Name = txt_Author_name.Text.Trim();
            //    butdanhID = obj.Insert_Butdang(obj_BD);
            //}
            obj_news.News_TacgiaID = butdanhID;
            obj_news.News_Comment = UltilFunc.CleanFormatTags(Txt_Comments.Text);
            //obj_news.Keywords = this.txtTukhoa.Text;
            obj_news.News_IsCategorys = this.chk_IsCategorys.Checked;
            obj_news.News_IsHomePages = this.chk_IsHomePages.Checked;
            obj_news.News_IsCategoryParrent = this.chk_IsCategoryParrent.Checked;
            obj_news.News_IsHot = this.chkNewsIsHot.Checked;
            obj_news.News_IsFocus = this.chkNewsIsFocus.Checked;
            int tab = 0;
            if (Page.Request["Tab"] != null)
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            if (tab == 0)
                obj_news.News_Status = ConstNews.AddNew;
            else if (tab == -1)
                obj_news.News_Status = ConstNews.AddNew;
            else if (tab == 1)
                obj_news.News_Status = ConstNews.NewsReturn;
            else if (tab == 3)
                obj_news.News_Status = ConstNews.NewsDelete;
            obj_news.News_Realate = ReturnFilterListRelation();
            //Add By nvthai
            obj_news.News_Priority = 1;

            obj_news.News_PhotoAtt = txtVideoPath.Text;
            obj_news.News_DescImages = txtChuthichanh.Text;
            obj_news.Image_Hot = cbHienthiAnh.Checked;
            obj_news.News_Nguon = txtNguon.Text;
            obj_news.News_DisplayMobile = cbDisplayMobile.Checked;
            obj_news.News_Delete = cbMoreViews.Checked;
            return obj_news;
        }

        protected T_News SetItem(int status)
        {
            T_NewsDAL Dal = new T_NewsDAL();
            T_News obj_news = new T_News();
            if (Page.Request.Params["id"] != null)
                obj_news.News_ID = Convert.ToInt32(Page.Request["id"].ToString());
            else obj_news.News_ID = 0;
            obj_news = Dal.load_T_news(int.Parse(Request["id"]));
            //if (obj_news.News_Status == 73)
            //    obj_news.News_DateEdit = obj_news.News_DateEdit;
            //else
            //    obj_news.News_DateEdit = DateTime.Now;
            if (Txt_tieude.Text != "")
                obj_news.News_Tittle = UltilFunc.CleanFormatTags(Txt_tieude.Text);
            if (txt_TieuDePhu.Text.Length > 0) //added by haolm
                obj_news.News_Sub_Title = UltilFunc.CleanFormatTags(txt_TieuDePhu.Text);
            if (txt_tomtat.Text.Length > 0)
                obj_news.News_Summary = txt_tomtat.Text;
            if (int.Parse(cbo_chuyenmuc.SelectedIndex.ToString()) > 0)
                obj_news.CAT_ID = int.Parse(cbo_chuyenmuc.SelectedValue.ToString());
            if (int.Parse(cbo_lanquage.SelectedIndex.ToString()) > 0)
                obj_news.Lang_ID = int.Parse(cbo_lanquage.SelectedValue.ToString());
            //obj_news.News_Priority = int.Parse(ddlNews_Priority.SelectedValue.ToString());
            obj_news.News_IsCategorys = this.chk_IsCategorys.Checked;
            obj_news.News_IsHomePages = this.chk_IsHomePages.Checked;
            obj_news.News_IsCategoryParrent = this.chk_IsCategoryParrent.Checked;

            obj_news.News_IsImages = this.chkImages.Checked;
            obj_news.News_IsVideo = this.chkVideo.Checked;
            obj_news.News_IsHistory = this.chkHistorys.Checked;
            //obj_news.News_TienNB = 0;
            //int tien = 0;
            //if (this.txtTienNhuanBut.Text.Trim().Length > 0)
            //{
            //    tien = int.Parse(txtTienNhuanBut.Text.Replace(",", ""));
            //    if (tien > 0)
            //    {
            //        obj_news.News_TienNB = tien;
            //        obj_news.News_Ngaycham = DateTime.Now;
            //        obj_news.News_NguoichamNBID = _user.UserID;
            //    }
            //    else
            //    {
            //        obj_news.News_TienNB = 0;
            //        obj_news.News_NguoichamNBID = 0;
            //    }
            //}
            if (txt_noidung.Text.Length > 0)
                obj_news.News_Body = txt_noidung.Text.Trim();
            obj_news.News_PublishNumber = DateTime.Now.Month;
            obj_news.News_PublishYear = DateTime.Now.Year;
            if (txtThumbnail.Text.Length > 0)
                obj_news.Images_Summary = this.txtThumbnail.Text.Trim();
            obj_news.News_AuthorName = UltilFunc.CleanFormatTags(txt_Author_name.Text);
            //obj_news.News_DateCreated = DateTime.Now;
            if (obj_news.News_Status != 55)
                obj_news.News_DateEdit = DateTime.Now;
            else
                obj_news.News_DateEdit = obj_news.News_DateEdit;
            obj_news.News_EditorID = _user.UserID;
            //obj_news.News_DatePublished = DateTime.Now;
            //obj_news.News_AuthorID = _user.UserID;
            //obj_news.News_PublishedID = _user.UserID;
            //obj_news.News_Paper_ID = 0;
            int butdanhID = 0;
            T_Butdanh obj_BD = new T_Butdanh();
            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            if (!string.IsNullOrEmpty(txt_Author_name.Text.Trim()))
            {
                obj_BD.BD_ID = 0;
                obj_BD.BD_Name = txt_Author_name.Text.Trim();
                obj_BD.UserID = _user.UserID;
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            obj_news.News_TacgiaID = butdanhID;
            obj_news.News_Comment = UltilFunc.CleanFormatTags(Txt_Comments.Text);
            obj_news.Keywords = this.txtTukhoa.Text;
            obj_news.News_IsHot = this.chkNewsIsHot.Checked;
            obj_news.News_IsFocus = this.chkNewsIsFocus.Checked;
            int tab = 0;
            if (Page.Request["Tab"] != null)
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
            if (tab == 0)
                obj_news.News_Status = ConstNews.NewsApproving_tk;
            else if (tab == -1)
                obj_news.News_Status = ConstNews.NewsApproving_tk;
            else if (tab == 1)
                obj_news.News_Status = ConstNews.NewsReturn_tk;
            else if (tab == 3)
                obj_news.News_Status = ConstNews.NewsDelete;
            else if (tab == 10)
                obj_news.News_Status = ConstNews.NewsPublishing;
            //Add By nvthai
            obj_news.News_Realate = ReturnFilterListRelation();
            obj_news.News_PhotoAtt = txtVideoPath.Text;
            obj_news.News_DescImages = txtChuthichanh.Text;
            obj_news.Image_Hot = cbHienthiAnh.Checked;
            obj_news.News_Nguon = txtNguon.Text;
            obj_news.News_DisplayMobile = cbDisplayMobile.Checked;
            obj_news.News_Delete = cbMoreViews.Checked;
            return obj_news;
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
