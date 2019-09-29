using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCComponents;
using HPCBusinessLogic;
using HPCInfo;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.BannerDanToc
{
    public partial class DanTocEdit : System.Web.UI.Page
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
                        DataBind();
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
                    //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Khách hàng :" + cbo_Khachhang.SelectedItem.Text,
                   //Request["Menu_ID"].ToString(), "[CẬP NHẬT THÔNG LOGO QUẢNG CÁO]-->[Thêm mới thông tin quảng cáo]ID:" + _catObj.ID.ToString() + " ]", 0);
                }
                else
                {
                    _cateDAL.InsertT_Customer_Ads(_catObj);
                    //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Khách hàng :" + cbo_Khachhang.SelectedItem.Text,
                   //Request["Menu_ID"].ToString(), "[CẬP NHẬT THÔNG LOGO QUẢNG CÁO]-->[Sửa thông tin quảng cáo]ID:" + _catObj.ID.ToString() + " ]", 0);
                }
                Page.Response.Redirect("~/BannerDanToc/DanTocDanhSach.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/BannerDanToc/DanTocDanhSach.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
        }

        #endregion
        private HPCInfo.T_Customer_Ads GetObject()
        {
            HPCInfo.T_Customer_Ads _objCate = new HPCInfo.T_Customer_Ads();
            HPCBusinessLogic.DAL.T_Customer_AdsDAL _CateDAL = new HPCBusinessLogic.DAL.T_Customer_AdsDAL();
            if (Page.Request["id"] != null)
            {
                _objCate.ID = int.Parse(Page.Request["id"].ToString());
                _objCate = _CateDAL.GetOneFromT_Customer_AdsByID(int.Parse(Page.Request["id"].ToString()));
            }
            else
            {
                _objCate.ID = 0;
                _objCate.DateCreated = DateTime.Now;
                _objCate.UserCreated = _user.UserID;
            }
            _objCate.Ads_ImgVideo = txtImageVideo.Text;
            //_objCate.Ads_Images = txtThumbnail.Text;
            //_objCate.AdvType = AdsPosDAL.getAdvType(txtThumbnail.Text);
            _objCate.Possittion = 1;
            if (txtOrder.Text.Length > 0)
                _objCate.Order_Number = int.Parse(txtOrder.Text);
            _objCate.URL = Txt_DiachiQC.Text;
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
                txtOrder.Text = _cateObj.Order_Number.ToString();
                txtImageVideo.Text = _cateObj.Ads_ImgVideo.ToString();
                //txtThumbnail.Text = _cateObj.Ads_Images.ToString();
                txtMota.Text = _cateObj.DisplayType.ToString();
                if (_cateObj.Status == 1)
                    chkDisplay.Checked = true;
                else
                    chkDisplay.Checked = false;
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
    }
}
