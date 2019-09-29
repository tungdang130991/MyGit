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
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Quangcao
{
    public partial class AdsLogoList : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        private int EID
        {
            get
            {
                if (ViewState["EID"] == null)
                    ViewState["EID"] = -1;
                return (int)ViewState["EID"];
            }
            set
            {
                ViewState["EID"] = value;
            }
        }
        string strMenuID = "";
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
                    strMenuID = Request["Menu_ID"].ToString();
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        if (Session["AsdLogolist_LangID"] != null && !string.IsNullOrEmpty(Session["AsdLogolist_LangID"].ToString()))
                        {
                            string langid = "0";
                            langid = Session["AsdLogolist_LangID"].ToString().Trim();
                            cbo_lanquage.SelectedValue = langid;
                        }
                        Session["AsdLogolist_LangID"] = null;
                        Session["AsdLogolist_CATID"] = null;

                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            Session["CurrentPage"] = null;
                        }
                        LoadData();
                    }
                }
            }
        }
        protected void ActiverPermission()
        {
            // Top
            //this.btnGuiduyet.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn gửi duyệt?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            //this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            // Bottom
            //this.btnSend.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn gửi duyệt?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_grdListCate_ctl01_chkAll');");
        }
        protected string ReturnObjectDisplay(object objPath, object objimg,object _ID)
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
                try
                {
                    _extend = System.IO.Path.GetExtension(objPath.ToString().Split('/').GetValue(objPath.ToString().Split('/').Length - 1).ToString().Trim());
                    if (_extend.ToLower() == ".jpg" || _extend.ToLower() == ".png" || _extend.ToLower() == ".gif" || _extend.ToLower() == ".jpeg" || _extend.ToLower() == ".bmp")
                    {
                        _return = "<img style=\"max-width:180px;max-height:180px;\"  src=\"" + HPCComponents.Global.TinPathBDT + objPath.ToString() + "\" alt=\"View\" onclick=\"openNewImage(this,'Close');\" />";
                    }
                    else if (_extend.ToLower() == ".flv" || _extend.ToLower() == ".wmv" || _extend.ToLower() == ".mp4")
                    {
                        //_return = "<img style=\"max-width:180px;\" src=\"" + HPCComponents.Global.TinPath + "/upload/Ads/admin/Icons/ico_video.jpg" + "\" alt=\"View\" onclick=\"xemquangcao('"+HPCComponents.Global.TinPath + objPath.ToString() +"','');\" />";
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
                catch
                {
                    _return = objPath.ToString();
                }
                              
            }
            return _return;

        }
        #region Methods
        protected void ActivePermission()
        {
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            //this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            //this.btnSend.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn gửi duyệt?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
            //this.btnGuiduyet.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn gửi duyệt?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
        }
        protected string CheckAdvType(object strCheck, object strType)
        {
            string strSrc = "";
            if (Convert.ToInt32(strCheck) == 1) strSrc = Global.ApplicationPath + @"/Dungchung/Images/Icons/MediaFileIcon.png";
            if (Convert.ToInt32(strCheck) == 0) strSrc = Global.ApplicationPath + @"/" + strType.ToString();
            return strSrc;
        }

        private void LoadComboBox()
        {
            cbo_lanquage.Items.Clear();

            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cbo_lanquage.Items.Count >= 3)
            {
                cbo_lanquage.SelectedIndex = Global.DefaultLangID;
            }
            else
                cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(cbo_lanquage, Global.DefaultCombobox);

            UltilFunc.BindCombox(cbo_Khachhang, "ID", "Name", "T_Customers", string.Format(" 1=1  Order by Name "), CommonLib.ReadXML("lblTatca"));
           // UltilFunc.BindCombox(cbo_Vitri_hienthi, "ID", "Ads_Name", "T_Ads_Pos", string.Format(" 1=1  Order by ID "), "---Vị trí quảng cáo---");

        }

        public void LoadData()
        {
            //string where = " 1=1 AND Lang_ID IN (SELECT T_UserLanguages.Languages_ID FROM T_UserLanguages WHERE T_UserLanguages.[User_ID] = " + _user.UserID + ")";
            string where = " 1=1 ";
            if (cbo_lanquage.SelectedIndex > 0)
                where += " AND Lang_ID=" + cbo_lanquage.SelectedValue.ToString();
            if (cbo_Khachhang.SelectedIndex > 0)
                where += " AND Cust_ID=" + cbo_Khachhang.SelectedValue.ToString();
            where += " Order by Order_Number ASC";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Customer_AdsDynamic(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Customer_AdsDynamic(pages.PageIndex - 1, pages.PageSize, where);
            // DataView _dv = _cateDAL.BindGridCagegorys(_ds.Tables[0]);
            grdListCate.DataSource = _ds;
            grdListCate.DataBind(); _ds.Clear();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
        }

        protected string getListCategoryName(object AdsID)
        {
            HPCBusinessLogic.DAL.T_Customer_AdsDAL _objCusAdsDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            return _objCusAdsDAL.getListCategoryNameAds(int.Parse(AdsID.ToString()));
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
        protected string IsValidColor(object hienthi)
        {
            string objColor = string.Empty;
            if (hienthi != null && !string.IsNullOrEmpty(hienthi.ToString()))
            {
                if (Convert.ToInt32(hienthi) == 1)
                    objColor = "<span style=\"display:inline-block;background-color:Blue;width:90%;\">&nbsp;</span>";
                else
                    objColor = "<span style=\"display:inline-block;background-color:Red;width:90%;\">&nbsp;</span>";
            }
            return objColor;
        }

      
        protected string UrlAdsImage(object objUrlAdsImage, object AdsType)
        {
            string strUrlImage = "";
            if (AdsType.ToString() == "0")
                strUrlImage = Global.TinPathBDT + objUrlAdsImage.ToString();
            else
                strUrlImage = Global.ApplicationPath + "/Dungchung/images/Video-icon.png";
            return strUrlImage;
        }

        protected string IsGetTrangThai(string str)
        {
            string strReturn = "";
            if (str == "1")
                strReturn = "Banner";
            else
                strReturn = "Bên phải";
            return strReturn;
        }

        protected void DeleteAdsByID(int AdsID)
        {
            HPCBusinessLogic.DAL.T_Customer_AdsDAL objAds_DAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            try
            {
                objAds_DAL.DeleteFrom_T_Customer_Ads(AdsID);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objAds_DAL = null;
            }

        }
        //protected void btnXuatban_Click(object sender, EventArgs e)
        //{
        //    int Ads_ID = 0;
        //    try
        //    {
        //        HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
        //        foreach (DataGridItem m_Item in grdListCate.Items)
        //        {

        //            CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
        //            if (chk_Select != null && chk_Select.Checked)
        //            {
        //                Label lblMota = (Label)m_Item.FindControl("lblMota");
        //                Ads_ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
        //                bool bolUpdate = _cateDAL.Send2Approver(Ads_ID, 3, _user.UserID);
        //                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lblMota.Text, strMenuID, "XUẤT BẢN QUẢNG CÁO", double.Parse(Ads_ID.ToString()));
        //            }
        //        }
        //        LoadData();
        //        //Tao cache
        //        //UltilFunc.GenCacheHTML();
        //    }
        //    catch (Exception ex)
        //    {
        //        HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
        //    }
        //}
        #endregion

        #region Event Click

        
        protected void btnExit_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }

        protected void linkSearch_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }

        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quangcao/AdsLogoEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }

        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {

            string strActionHistory = "";
            int Ads_ID = 0;
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    Label lblMota = (Label)m_Item.FindControl("lblMota");
                    Ads_ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    if (strActionHistory == "")
                        strActionHistory = lblMota.Text;
                    else
                        strActionHistory = strActionHistory + "<br>" + lblMota.Text;
                    DeleteAdsByID(Ads_ID);
                }
            }
            //Ghi log nhat ky xoa vao he thong
            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserName, strActionHistory, Request["Menu_ID"].ToString(), "XÓA THÔNG TIN QUẢNG CÁO",0,0);
            LoadData();
        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    TextBox txtPoss = (TextBox)m_Item.FindControl("txtPoss");
                    int intCategorysID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    if (txtPoss.Text.Trim() != "")
                    {
                        bool bolUpdate = _cateDAL.UpdateOrderOfAdsLogo(intCategorysID, int.Parse(txtPoss.Text));
                    }
                }
                LoadData();
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }
        }

        //protected void linkSend_Click(object sender, EventArgs e)
        //{
        //    string strActionHistory = "";
        //    int Ads_ID = 0;
        //    try
        //    {
        //        HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
        //        foreach (DataGridItem m_Item in grdListCate.Items)
        //        {

        //            CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
        //            if (chk_Select != null && chk_Select.Checked)
        //            {
        //                Label lblMota = (Label)m_Item.FindControl("lblMota");
        //                Ads_ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
        //                if (strActionHistory == "")
        //                    strActionHistory = lblMota.Text;
        //                else
        //                    strActionHistory = strActionHistory + "<br>" + lblMota.Text;
        //                bool bolUpdate = _cateDAL.Send2Approver(Ads_ID, 2, _user.UserID);
        //                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, strActionHistory, Request["Menu_ID"].ToString(), "GỬI DUYỆT QUẢNG CÁO", 0);
        //            }
        //        }
        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
        //    }
        //}

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_Customer_AdsDAL obj_Cate = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                Session["AsdLogolist_LangID"] = cbo_lanquage.SelectedValue;
                Response.Redirect("~/Quangcao/AdsLogoEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }
            if (e.CommandArgument.ToString().ToLower() == "viewads")
            {
                T_Customer_Ads _obj = new T_Customer_Ads();
                T_Customer_AdsDAL _DAL = new T_Customer_AdsDAL();
                DataTable _dtAds = null;
                try
                {
                    EID = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                    _obj = _DAL.GetOneFromT_Customer_AdsByID(EID);
                    if (_obj.Possittion.ToString() != "")
                    {
                        _dtAds = _DAL.BindGridT_Customer_Ads(_obj.Possittion.ToString().Trim()).Tables[0];
                        
                        rptSlideShows.DataSource = _dtAds.DefaultView;
                        rptSlideShows.DataBind();
                    }
                    
                    ModalPopupExtender2.Show();
                }
                catch (Exception ex)
                { }
                
            }
        }
        
        public int GetTotalsAds()
        {
            
            int strTemp = 0;
            if (EID != -1)
            {
                T_Customer_Ads _obj = new T_Customer_Ads();
                T_Customer_AdsDAL _DAL = new T_Customer_AdsDAL();                
                _obj = _DAL.GetOneFromT_Customer_AdsByID(EID);
                if (_obj.Possittion.ToString() != "")
                {
                    strTemp =Convert.ToInt32(_DAL.BindGridT_Customer_Ads(_obj.Possittion.ToString().Trim()).Tables[0].Rows.Count.ToString());
                    
                }
            }
            
            return strTemp;
        }

        #endregion


        #region "Synchronize"

        private void SynData_UpdataStatusT_CustumeradsAny(string strwhere, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    SynData_UpdataStatusT_Custumerads(strwhere, _arr[i].ToString());
                }
            }
        }
        private void SynData_UpdataStatusT_Custumerads(string strwhere, string urlService)
        {
            string _sql = "[Syn_UpdateStatusCustomer_Ads]";
            ServicesPutDataBusines.UltilFunc _untilDAL = new ServicesPutDataBusines.UltilFunc(urlService);
            try
            {

                _untilDAL.ExecStore(_sql, new string[] { "@WhereCondition" }, new object[] { strwhere });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SynData_DeleteT_T_CustumeradsAny(int _ID, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    SynData_DeleteT_Custumerads(_ID, _arr[i].ToString());
                }
            }
        }
        private void SynData_DeleteT_Custumerads(int _ID, string urlService)
        {
            string _sql = "[Syn_DeleteOneFromT_Customer_Ads]";
            ServicesPutDataBusines.UltilFunc _untilDAL = new ServicesPutDataBusines.UltilFunc(urlService);
            try
            {

                _untilDAL.ExecStore(_sql, new string[] { "@ID" }, new object[] { _ID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion end
    }
}
