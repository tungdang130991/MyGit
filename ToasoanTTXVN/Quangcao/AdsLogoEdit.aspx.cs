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
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.IO;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Quangcao
{
    public partial class AdsLogoEdit : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
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
                    strMenuID = Request["Menu_ID"].ToString();
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                        LoadCategorys();
                    }
                }
            }
        }
        #region Event methods
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
                HPCInfo.T_Customer_Ads _catObj = GetObject();
                if (_catObj.ID == 0)
                {
                    int _return = _cateDAL.InsertT_Customer_Ads(_catObj);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Khách hàng :" + cbo_Khachhang.SelectedItem.Text,
                   Request["Menu_ID"].ToString(), "[CẬP NHẬT THÔNG LOGO QUẢNG CÁO]-->[Thêm mới thông tin quảng cáo]ID:" + _catObj.ID.ToString() + " ]", 0, 0);
                }
                else
                {
                    _cateDAL.InsertT_Customer_Ads(_catObj);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Khách hàng :" + cbo_Khachhang.SelectedItem.Text,
                   Request["Menu_ID"].ToString(), "[CẬP NHẬT THÔNG LOGO QUẢNG CÁO]-->[Sửa thông tin quảng cáo]ID:" + _catObj.ID.ToString() + " ]", 0, 0);
                }
                Page.Response.Redirect("~/Quangcao/AdsLogoList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/Quangcao/AdsLogoList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
        }

        #endregion

        #region User-methods

        private void getWidthAndHeightPos(int intPosID)
        {
            AdsPosDAL objAdsPosDAL = new AdsPosDAL();
            T_AdsPos objInforAdsPos = new T_AdsPos();
            try
            {
                objInforAdsPos = objAdsPosDAL.GetOneFromT_AdsPosByID(intPosID);
                if (objInforAdsPos != null)
                {
                    this.txtWidth.Text = objInforAdsPos.Ads_Width.ToString();
                    this.txtHeight.Text = objInforAdsPos.Ads_Height.ToString();
                }
            }
            catch { }
            finally
            {
                objAdsPosDAL = null;
                objInforAdsPos = null;
            }
        }
        protected string getListCateAdsDisplay(string strPosAds)
        {
            string strListCategorysID = string.Empty;
            int check = 0;
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    check++;
                    if (check == 1)
                        strListCategorysID = grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString();
                    else
                        strListCategorysID = strListCategorysID + "," + grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString();
                }
            }
            if (strPosAds.ToUpper() == "ALL" || check == 0)
                strListCategorysID = "ALL";
            return strListCategorysID;
        }
        private HPCInfo.T_Customer_Ads GetObject()
        {
            HPCInfo.T_Customer_Ads _objCate = new HPCInfo.T_Customer_Ads();
            HPCBusinessLogic.DAL.T_Customer_AdsDAL _CateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            if (Page.Request["id"] != null)
            {
                _objCate.ID = int.Parse(Page.Request["id"].ToString());
                _objCate = _CateDAL.GetOneFromT_Customer_AdsByID(int.Parse(Page.Request["id"]));
            }
            else
            {
                _objCate.ID = 0;
                _objCate.DateCreated = DateTime.Now;
                _objCate.UserCreated = _user.UserID;
            }
            _objCate.Cat_ID = getListCateAdsDisplay(this.cbo_Category.SelectedValue.ToString());
            if (int.Parse(cbo_Khachhang.SelectedIndex.ToString()) > 0)
                _objCate.Cust_ID = int.Parse(cbo_Khachhang.SelectedValue);
            if (int.Parse(cbo_lanquage.SelectedIndex.ToString()) > 0)
                _objCate.Lang_ID = int.Parse(cbo_lanquage.SelectedValue);
            _objCate.Ads_ImgVideo = txtImageVideo.Text;
            _objCate.Ads_Images = txtThumbnail.Text;
            _objCate.AdvType = AdsPosDAL.getAdvType(txtThumbnail.Text);
            if (!string.IsNullOrEmpty(txt_ngaybatdau.Text))
                _objCate.Start_Date = CommonLib.ToDate(txt_ngaybatdau.Text, "dd/MM/yyyy HH:mm:ss:tt");
            else
                _objCate.Start_Date = DateTime.Now;
            if (!string.IsNullOrEmpty(txt_ngayketthuc.Text))
                _objCate.End_Date = CommonLib.ToDate(txt_ngayketthuc.Text, "dd/MM/yyyy HH:mm:ss:tt");
            else
                _objCate.End_Date = DateTime.Now;
            if (int.Parse(cbo_Vitri_hienthi.SelectedValue.ToString()) > 0)
                _objCate.Possittion = int.Parse(cbo_Vitri_hienthi.SelectedValue.ToString());
            if (txtOrder.Text.Length > 0)
                _objCate.Order_Number = int.Parse(txtOrder.Text);
            if (this.txtHeight.Text.Length > 0)
                _objCate.Height = this.txtHeight.Text.Trim();
            if (this.txtWidth.Text.Length > 0)
                if (UltilFunc.IsNumeric(this.txtWidth.Text.Trim()))
                    _objCate.Width = this.txtWidth.Text.Trim();
            _objCate.URL = Txt_DiachiQC.Text;
            if (int.Parse(cbo_Display.SelectedIndex.ToString()) > 0)
                _objCate.Target = int.Parse(cbo_Display.SelectedValue.ToString());
            _objCate.DisplayType = txtMota.Text;
            if (chkDisplay.Checked == true)
        	    _objCate.Status = 1;	 
            else
                _objCate.Status = 0;
            return _objCate;
        }

        private void PopulateItem(int _id)
        {
            HPCInfo.T_Customer_Ads _cateObj = new HPCInfo.T_Customer_Ads();
            HPCBusinessLogic.DAL.T_Customer_AdsDAL _cateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            _cateObj = _cateDAL.load_T_Customer_Ads(_id);
            if (_cateObj != null)
            {
                Txt_DiachiQC.Text = _cateObj.URL.ToString();
                if (_cateObj.Start_Date.ToString().Length > 0)
                    txt_ngaybatdau.Text = _cateObj.Start_Date.ToString("dd/MM/yyyy");
                else
                    txt_ngaybatdau.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                if (_cateObj.End_Date.ToString().Length > 0)
                    txt_ngayketthuc.Text = _cateObj.End_Date.ToString("dd/MM/yyyy");
                else
                    txt_ngayketthuc.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                txtOrder.Text = _cateObj.Order_Number.ToString();
                this.txtWidth.Text = _cateObj.Width.ToString().Trim();
                this.txtHeight.Text = _cateObj.Height.ToString().Trim();
                txtImageVideo.Text = _cateObj.Ads_ImgVideo.ToString();
                txtThumbnail.Text = _cateObj.Ads_Images.ToString();
                //if (_cateObj.Ads_ImgVideo.Length > 0)
                //    this.ImagesVd.Src = _cateObj.Ads_ImgVideo;
                //if (_cateObj.Ads_Images.Length > 0)
                //    this.ImgTemp.Src =_cateObj.Ads_Images;
                cbo_Khachhang.SelectedValue = _cateObj.Cust_ID.ToString();
                cbo_lanquage.SelectedValue = _cateObj.Lang_ID.ToString();
                cbo_Vitri_hienthi.SelectedValue = _cateObj.Possittion.ToString();
                cbo_Display.SelectedValue = _cateObj.Target.ToString();
                txtMota.Text = _cateObj.DisplayType.ToString();
                if (_cateObj.Status == 1)
                    chkDisplay.Checked = true;
                else
                    chkDisplay.Checked = false;
                if (_cateObj.Cat_ID.ToString().ToLower() == "all")
                {
                    this.cbo_Category.SelectedValue = _cateObj.Cat_ID.ToString();
                    //this.cbo_Category.UpdateAfterCallBack = true;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetDisplay('none');", true);
                }
                else
                {
                    this.cbo_Category.SelectedValue = "CM";
                    //this.cbo_Category.UpdateAfterCallBack = true;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetDisplay('block');", true);
                }
            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    HPCBusinessLogic.DAL.T_Customer_AdsDAL dal = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
        }
        protected void cbo_Vitri_hienthi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Vitri_hienthi.SelectedIndex > 0)
            {
                getWidthAndHeightPos(int.Parse(this.cbo_Vitri_hienthi.SelectedValue));
                this.txtHeight.UpdateAfterCallBack = true;
                this.txtWidth.UpdateAfterCallBack = true;
            }
        }
        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategorys();
        }
        protected void cbo_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbo_Category.SelectedValue.ToString().ToUpper() == "CM")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetDisplay('block');", true);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "javascript", "javascript: SetDisplay('none');", true);
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cbo_lanquage.Items.Count >= 3)
                cbo_lanquage.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(this.cbo_lanquage, HPCComponents.Global.DefaultCombobox);
            UltilFunc.BindCombox(this.cbo_Khachhang, "ID", "Name", "T_Customers", string.Format(" 1=1  Order by Name "), CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox(this.cbo_Vitri_hienthi, "ID", "Ads_Name", "T_Ads_Pos", string.Format(" 1=1  Order by ID "), CommonLib.ReadXML("lblVitriqc"));

        }
        private int getAdvType(string Path2FileAdvs)
        {
            int intAdvType = 0;
            string strExten = "";
            if (Path2FileAdvs.Trim() != "")
            {
                strExten = Path2FileAdvs.Substring(Path2FileAdvs.Length - 3).ToLower();
                if (strExten == "flv" || strExten == "swf" || strExten == "wmv")
                    intAdvType = 1;
            }
            return intAdvType;
        }
        #region User method
        public void LoadCategorys()
        {
            int _id = 0;
            int.TryParse(Request["ID"] == null ? "0" : Request["ID"], out _id);
            HPCBusinessLogic.ChuyenmucDAL _cateDAL = new ChuyenmucDAL();
            DataTable _dt = null;
            _dt = _cateDAL.BindGridT_Cagegorys(int.Parse(cbo_lanquage.SelectedValue.ToString()), _id);
            if (_dt != null)
            {
                this.grdListCate.DataSource = _dt;
                this.grdListCate.DataBind();
            }
            else
            {
                this.grdListCate.DataSource = null;
                this.grdListCate.DataBind();
            }
        }

        #endregion
        #endregion
    }
}
