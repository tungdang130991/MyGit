using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSOLib.ServiceAgent;
using HPCBusinessLogic;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.Quangcao
{
    public partial class AdsPosEdit : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user = null;
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
                    if (!IsPostBack)
                    {
                        DisplayData();
                    }
                }
            }
        }
        #region "Functions"

        private void PopulateItem(int _id)
        {
            T_AdsPos _cateObj = new T_AdsPos();
            AdsPosDAL _cateDAL = new AdsPosDAL();
            _cateObj = _cateDAL.GetOneFromT_AdsPosByID(_id);
            if (_cateObj != null)
            {
                txt_Ads_Name.Text = _cateObj.Ads_Name.ToString();
                cbo_Ads_DisplayType.SelectedValue = _cateObj.Ads_DisplayType.ToString();
                txt_Ads_Width.Text = _cateObj.Ads_Width.ToString();
                txt_Ads_Height.Text = _cateObj.Ads_Height.ToString();

            }
        }

        private void DisplayData()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
        }

        private T_AdsPos setItemObject()
        {
            HPCInfo.T_AdsPos _objCate = new HPCInfo.T_AdsPos();
            if (Page.Request.Params["id"] != null)
                _objCate.ID = int.Parse(Page.Request["id"].ToString());
            else
                _objCate.ID = 0;
            _objCate.Ads_Name = UltilFunc.CleanFormatTags(txt_Ads_Name.Text);
            _objCate.Ads_DisplayType = int.Parse(cbo_Ads_DisplayType.SelectedValue.ToString());
            _objCate.Ads_Width = this.txt_Ads_Width.Text;
            _objCate.Ads_Height = this.txt_Ads_Height.Text;
            return _objCate;
        }
        #endregion

        #region "Events Click"
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AdsPosDAL _cateDAL = new AdsPosDAL();
                T_AdsPos _catObj = setItemObject();
                _cateDAL.InsertAdsPos(_catObj);
                if (_catObj.ID != 0)
                {
                    //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, txt_Ads_Name.Text,
                    //  Request["Menu_ID"].ToString(), "[Chi tiết vị trí]-->[Thao tác sửa vị trí]ID:" + _catObj.ID.ToString() + " ]", 0);
                    Page.Response.Redirect("~/Quangcao/AdsPostList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                }
                else
                {
                    Page.Response.Redirect("~/Quangcao/AdsPostList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
                }
            }
        }

        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            if (Page.Request["Menu_ID"] != null)
                Page.Response.Redirect("~/Quangcao/AdsPostList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
        }

        #endregion
    }
}
