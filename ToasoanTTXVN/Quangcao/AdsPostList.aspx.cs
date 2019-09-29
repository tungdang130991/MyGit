using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using SSOLib.ServiceAgent;
using HPCInfo;
using HPCComponents;
using HPCBusinessLogic.DAL;
using System.Data;

namespace ToasoanTTXVN.Quangcao
{
    public partial class AdsPostList : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user = null;
        protected T_RolePermission _Role = null;
        #endregion
        protected string strMenu_ID = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    strMenu_ID = Request["Menu_ID"].ToString();
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    ActivePermission();
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }
        protected void ActivePermission()
        {
            this.LinkDelete1.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListAdsPos_ctl01_chkAll');");
            this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListAdsPos_ctl01_chkAll');");
        }
        #region "Event click"
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quangcao/AdsPosEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"]);
        }

        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdListAdsPos.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    LinkButton linkname = (LinkButton)m_Item.FindControl("Linkvitri");
                    int _ID = int.Parse(grdListAdsPos.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    AdsPosDAL objAdsPosDAL = new AdsPosDAL();
                    objAdsPosDAL.DeleteFromT_AdsPosByID(_ID);
                    //WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, linkname.Text,
                  //Request["Menu_ID"].ToString(), "[Danh sách vị trí]-->[Thao tác xóa vị trí]ID:" + _ID.ToString() + " ]", 0);
                }
            }
            LoadData();
        }

        public void grdListAdsPos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa dòng này không ?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListAdsPos_EditCommand(object source, DataGridCommandEventArgs e)
        {
            AdsPosDAL obj_Cate = new AdsPosDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int catID = Convert.ToInt32(this.grdListAdsPos.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Quangcao/AdsPosEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + catID);
            }

            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                obj_Cate.DeleteFromT_AdsPosByID(Convert.ToInt32(this.grdListAdsPos.DataKeys[e.Item.ItemIndex].ToString()));
                this.LoadData();
            }
        }

        #endregion

        #region "Load data"

        protected void LoadData()
        {
            DataSet _ds = null;
            AdsPosDAL objAdsPos = new AdsPosDAL();
            try
            {
                _ds = objAdsPos.BindGridT_AdsPos();
                grdListAdsPos.DataSource = _ds;
                grdListAdsPos.DataBind();
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }
            finally
            {
                _ds.Dispose();
                _ds = null;
                objAdsPos = null;
            }

        }

        protected string DisplayText(string str)
        {
            string strReturn = "";
            if (str == "1")
                strReturn = "Danh sách";
            else if (str == "2")
                strReturn = "Ngẫu nhiên";
            return strReturn;
        }

        #endregion
    }
}
