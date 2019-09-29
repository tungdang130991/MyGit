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
    public partial class PublishedEdit : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
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
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham"," 1=1 ", CommonLib.ReadXML("lblTatca"));
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
                lblTitleCaption.Text = "CẬP NHẬT BÀI VIẾT";
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int ChildID = Convert.ToInt32(Request["ID"]);
                    PopulateItem(ChildID);
                }
            }
            else
            {
                lblTitleCaption.Text = "SỬA ĐỔI BÀI VIẾT";
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                this.cbHienthiAnh.Checked = false;
                if (cbo_lanquage.SelectedIndex > 0)
                {
                    cbo_chuyenmuc.Items.Clear();
                    if (cbo_lanquage.SelectedIndex >= 0)
                    {
                        UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                        cbo_chuyenmuc.UpdateAfterCallBack = true;
                    }
                    else
                    {
                        this.cbo_chuyenmuc.DataSource = null;
                        this.cbo_chuyenmuc.DataBind();
                        this.cbo_chuyenmuc.UpdateAfterCallBack = true;
                    }
                }
            }
        }

        protected void PopulateItem(int _id)
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
                cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(cbo_lanquage, _obj.Lang_ID.ToString());
                cbo_chuyenmuc.Items.Clear();
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
                //if (_obj.News_TienNB > 0.0)
                //    this.txtTienNhuanBut.Text = _obj.News_TienNB.ToString();
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "Comma('" + txtTienNhuanBut.ClientID + "');", true);
                if (_obj.News_TienNB > 0.0)
                    this.txtTienNhuanBut.Text = string.Format("{0:#,#}", _obj.News_TienNB).Replace(".", ",");
                //this.ddlNews_IsType.SelectedValue = _obj.News_IsType.ToString();
                this.chkNewsIsFocus.Checked = _obj.News_IsFocus;
                this.chkNewsIsHot.Checked = _obj.News_IsHot;
                if (_obj.Images_Summary.Length > 0)
                    this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + "/" + _obj.Images_Summary;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                this.Txt_Comments.Text = _obj.News_Comment;
                this.txt_tomtat.Text = _obj.News_Summary;
                this.txtThumbnail.Text = _obj.Images_Summary;
                this.txtTukhoa.Text = _untilDAL.GetKeywordsByNewsID(_id);
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
            }
        }

        //private double Insert(Boolean _send)
        //{
        //    T_News obj;
        //    HPCBusinessLogic.DAL.T_NewsDAL tt_DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
        //    if (_send)
        //        obj = SetItem(ConstNews.NewsApproving_tb);
        //    else
        //        obj = SetItem(ConstNews.NewsReturn);
        //    if (obj.News_ID > 0)
        //    {
        //        obj.News_ID = tt_DAL.InsertT_news(obj);
        //        if (_send)
        //        {
        //            SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsApproving_tb);
        //            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
        //           Request["Menu_ID"].ToString(), "[Bài đang xuất bản] [Tin bài đang chờ biên tập] [Thao tác gửi trưởng phòng duyệt]", obj.News_ID, ConstAction.BaoDT);

        //        }
        //        else
        //        {
        //            SendPub(double.Parse(obj.News_ID.ToString()), ConstNews.NewsReturn_tb);
        //            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, obj.News_Tittle,
        //                Request["Menu_ID"].ToString(), "[Bài đang xuất bản] [Tin bài đang xuất bản] [Thao tác ngừng đăng]", obj.News_ID, ConstAction.BaoDT);
        //        }
        //        return double.Parse(obj.News_ID.ToString());
        //    }
        //    else
        //        return 0;
        //}
        //private void SendPub(double id, int _status)
        //{
        //    T_News obj_T_News = new T_News();
        //    HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
        //    if (_status == ConstNews.NewsApproving_tb)
        //        tt.Update_Status_tintuc(double.Parse(id.ToString()), _status, _user.UserID, DateTime.Now);
        //    else
        //        //Hungviet add
        //        tt.UpdateStatus_T_News_ex_New_HV(double.Parse(id.ToString()), _status, 0, DateTime.Now);
        //    tt.Insert_Version_From_T_News_WithUserModify(double.Parse(id.ToString()), ConstNews.NewsAppro_tk, _status, _user.UserID);

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
        protected void linkSave_Click(object sender, EventArgs e)
        {
            //dung them vao de kiem tra dieu dau vao và để busybox khong bi dung trong IE
            if (Txt_tieude.Text.Length == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
                return;
            }
            //if (cbo_lanquage.SelectedIndex == 0)
            //{
            //    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanLuu") + "');", true);
            //    return;
            //}

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
            if (Page.IsValid)
            {
                HPCBusinessLogic.DAL.T_NewsDAL _untilDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                T_News _objSet = null;
                int tab = 0;
                if (Request["Tab"] != null)
                    tab = Convert.ToInt32(Request["Tab"].ToString());
                if (tab == 0)
                    _objSet = SetItem(ConstNews.NewsPublishing);
                else _objSet = SetItem(ConstNews.NewsUnPublishing);
                UltilFunc.Insert_News_Image(txt_noidung.Text.Trim(), Convert.ToDouble(Page.Request["id"]));
                int _return = _untilDAL.InsertT_newsXb(_objSet);
                //key words
                InsertKeyword(_return, _user.UserID);
                if (_objSet.Images_Summary.ToString().Length > 0)
                    this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + "/" + _objSet.Images_Summary;
                _untilDAL.IsLock(_return, 1, _user.UserID);//dung them vao de giu trang thai lock khi dang lam viec voi tin bai
                if (_objSet.News_Status != ConstNews.NewsPublishing)
                {
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _objSet.News_Tittle,
                            Request["Menu_ID"].ToString(), "[Bài ngừng đăng] [Sửa bài ngừng xuất bản]", _objSet.News_ID, ConstAction.BaoDT);
                    
                }
                else
                {
                    //Tao cache
                    //UltilFunc.GenCacheHTML();
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _objSet.News_Tittle,
                             Request["Menu_ID"].ToString(), "[Bài đang xuất bản] [Sửa bài đang xuất bản]", _objSet.News_ID, ConstAction.BaoDT);
                    
                }
                if (Request["Tab"].ToString() == "0")
                {
                    #region Sync
                    // DONG BO FILE
                    SynFiles _syncfile = new SynFiles();
                    if (_objSet.Images_Summary.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_objSet.Images_Summary, HPCComponents.Global.ImagesService);
                    }

                    //Cap nhat anh trong bai viet - vao may dong bo
                    if (_objSet.News_Body.Length > 5)
                    {
                        _syncfile.SearchImgTag(_objSet.News_Body);
                        _syncfile.SearchTagSwf(_objSet.News_Body);
                        _syncfile.SearchTagFLV(_objSet.News_Body);
                    }
                    //END
                    #endregion
                }
                if (Request["Tab"] != null && Request["Tab"].ToString() != "" && Request["Tab"].ToString() != String.Empty)
                {
                    Response.Redirect("PublishedEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString() + "&ID=" + _return.ToString());
                }
                else
                    Response.Redirect("PublishedEdit.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + _return.ToString());
            }
        }
        protected void linkExit_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.T_NewsDAL _DAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            double ChildID = 0;
            double.TryParse(Request["ID"] == null ? "0" : Request["ID"], out ChildID);
            _DAL.IsLock(double.Parse(ChildID.ToString()), 0, 0);
            int tab = 0;
            if (Page.Request["Tab"] != null && Page.Request["Tab"].ToString() != "-1")
            {
                tab = Convert.ToInt32(Page.Request["Tab"].ToString());
                if (tab == 10)
                    Response.Redirect("PublishedList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
                else
                    Response.Redirect("PublishedList.aspx?Menu_ID=" + Request["Menu_ID"].ToString() + "&Tab=" + Page.Request["Tab"].ToString());
            }
            else
                Response.Redirect("PublishedList.aspx?Menu_ID=" + Request["Menu_ID"].ToString());
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
        protected T_News SetItem(int _status)
        {
            T_NewsDAL Dal = new T_NewsDAL();
            T_News _obj = new T_News();
            if (Page.Request.Params["id"] != null)
                _obj.News_ID = int.Parse(Page.Request["id"].ToString());
            else _obj.News_ID = 0;
            _obj = Dal.load_T_news(int.Parse(Page.Request["id"].ToString()));
            _obj.Lang_ID = Convert.ToInt32(this.cbo_lanquage.SelectedValue);
            _obj.CAT_ID = Convert.ToInt32(this.cbo_chuyenmuc.SelectedValue.ToString());
            _obj.News_Tittle = this.Txt_tieude.Text.Trim();
            _obj.Images_Summary = this.txtThumbnail.Text.Trim();
            if (_obj.Images_Summary.ToString().Length > 0)
                this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _obj.Images_Summary;
            else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
            _obj.News_Sub_Title = this.txt_TieuDePhu.Text.Trim();
            _obj.News_Summary = this.txt_tomtat.Text;
            _obj.News_Body = this.txt_noidung.Text.Trim();
            _obj.News_EditorID = _user.UserID;
            if (_obj.News_Status != 55)
                _obj.News_DateEdit = DateTime.Now;
            else
                _obj.News_DateEdit = _obj.News_DateEdit;
            if (_status == 6)
            {
                _obj.News_PublishNumber = DateTime.Now.Month;
                _obj.News_PublishYear = DateTime.Now.Year;
                _obj.News_PublishedID = _user.UserID;

                _obj.News_DatePublished = DateTime.Now;
                _obj.News_DateApproved = DateTime.Now;
                _obj.News_AprovedID = _user.UserID;
            }
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
            _obj.News_Comment = this.Txt_Comments.Text.Trim();
            _obj.News_AuthorName = this.txt_Author_name.Text.Trim();
            //if (ddlNews_Priority.Items.Count > 0)
            //    _obj.News_Priority = int.Parse(ddlNews_Priority.SelectedValue.ToString());
            _obj.News_IsCategorys = this.chk_IsCategorys.Checked;
            _obj.News_IsHomePages = this.chk_IsHomePages.Checked;
            _obj.News_IsCategoryParrent = this.chk_IsCategoryParrent.Checked;
            _obj.News_Status = _status;
            _obj.Keywords = this.txtTukhoa.Text;
            _obj.News_IsHot = this.chkNewsIsHot.Checked;
            _obj.News_IsFocus = this.chkNewsIsFocus.Checked;
            _obj.News_Realate = "";

            _obj.News_IsImages = this.chkImages.Checked;
            _obj.News_IsVideo = this.chkVideo.Checked;
            _obj.News_IsHistory = this.chkHistorys.Checked;
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
