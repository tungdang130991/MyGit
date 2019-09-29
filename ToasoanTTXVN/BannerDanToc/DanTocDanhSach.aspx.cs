using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HPCInfo;
using HPCBusinessLogic.DAL;
using HPCComponents;
using HPCBusinessLogic;
using System.Data;


namespace ToasoanTTXVN.BannerDanToc
{
    public partial class DanTocDanhSach : System.Web.UI.Page
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
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            LoadData();
                            Session["CurrentPage"] = null;
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        protected void ActiverPermission()
        {
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
        }
        #region Methods
        protected void ActivePermission()
        {
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc chắn muốn xóa?','ctl00_MainContent_grdListCate_ctl01_chkAll');");
        }

        public void LoadData()
        {
            string where = " 1=1 AND Possittion = 1";
            if (txt_tieude.Text.Trim().Length > 0)
                where += " AND DisplayType LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
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
            Response.Redirect("~/BannerDanToc/DanTocEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
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
            WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserName, strActionHistory, Request["Menu_ID"].ToString(), "XÓA THÔNG TIN DÂN TỘC", 0, 0);
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
                Response.Redirect("~/BannerDanToc/DanTocEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
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
                    strTemp = Convert.ToInt32(_DAL.BindGridT_Customer_Ads(_obj.Possittion.ToString().Trim()).Tables[0].Rows.Count.ToString());
                }
            }
            return strTemp;
        }
        #endregion
    }
}
