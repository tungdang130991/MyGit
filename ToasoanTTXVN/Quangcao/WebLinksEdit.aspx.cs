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
using ToasoanTTXVN.BaoDienTu;

namespace ToasoanTTXVN.Quangcao
{
    public partial class WebLinksEdit : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
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
                        LoadComboBox();
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
                T_WebLinksDAL _cateDAL = new T_WebLinksDAL();
                T_WebLinks _catObj = GetObject();
                int _return = _cateDAL.InsertT_WebLinks(_catObj);
                // DONG BO FILE
                _catObj = _cateDAL.load_T_WebLinks(_return);
                try
                {
                    SynFiles _syncfile = new SynFiles();
                    if (_catObj.Logo.Length > 0)
                    {
                        _syncfile.SynData_UploadImgOne(_catObj.Logo, HPCComponents.Global.ImagesService);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                //END

                //#region SYNC
                //if (UltilFunc.Check_SyncNewsDatabase(Request["Menu_ID"]))
                //    SynData_InsertT_WebLinksOne(_return, Global.Path_Service);
                //if (UltilFunc.Check_SyncImageDatabase(Request["Menu_ID"]))
                //{
                //    if (!txtThumbnail.Text.StartsWith("http"))
                //    {
                //        if (_catObj.Logo != null && _catObj.Logo.Length > 0)
                //            HPCSYNCUploadFiles.SyncUpload.SynData_UploadImgOne(_catObj.Logo, Global.ImagesService);
                //    }
                //}
                //#endregion
                if (_catObj.ID == 0)
                {
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Thêm mới]", Request["Menu_ID"].ToString(), "[Thêm mới]-->[Thao tác thêm mới T_WebLinks]", 0, 0);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                }
                else
                {
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Cập nhật]", Request["Menu_ID"].ToString(), "[Sửa danh sách T_WebLinks]-->[Thao tác sửa]", 0, 0);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                }
                Page.Response.Redirect("~/Quangcao/WeblinksList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            if (Page.Request["Menu_ID"] != null)
                Page.Response.Redirect("~/Quangcao/WeblinksList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
            else return;
        }
        #endregion

        #region User-methods
        private void LoadComboBox()
        {
            cbo_lanquage.Items.Clear();
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1", CommonLib.ReadXML("lblTatca"));
            if (cbo_lanquage.Items.Count >= 3)
                cbo_lanquage.SelectedIndex = Global.DefaultLangID;
            else cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(cbo_lanquage, Global.DefaultCombobox);

        }
        private HPCInfo.T_WebLinks GetObject()
        {
            HPCInfo.T_WebLinks _objCate = new HPCInfo.T_WebLinks();
            if (Page.Request.Params["id"] != null)
                _objCate.ID = int.Parse(Page.Request["id"].ToString());
            else _objCate.ID = 0;
            _objCate.URL = Txt_URL.Text;
            _objCate.Description = Txt_Address.Text;
            if (cbo_lanquage.SelectedIndex > 0)
                _objCate.Lang_ID = int.Parse(cbo_lanquage.SelectedValue.ToString());
            _objCate.Logo = txtThumbnail.Text;
            _objCate.Tittle = txtTieude.Text;
            _objCate.IsType = int.Parse(ddlType.SelectedValue);
            if (txtOrder.Text.Length > 0)
                if (UltilFunc.IsNumeric(this.txtOrder.Text.Trim()))
                    _objCate.OrderLinks = Convert.ToInt32(this.txtOrder.Text.Trim());
            return _objCate;
        }
        private void PopulateItem(int _id)
        {
            HPCInfo.T_WebLinks _cateObj = new HPCInfo.T_WebLinks();
            HPCBusinessLogic.DAL.T_WebLinksDAL _cateDAL = new HPCBusinessLogic.DAL.T_WebLinksDAL();
            _cateObj = _cateDAL.load_T_WebLinks(_id);
            if (_cateObj != null)
            {
                txtTieude.Text = _cateObj.Tittle.ToString();
                Txt_URL.Text = _cateObj.URL.ToString();
                Txt_Address.Text = _cateObj.Description.ToString();
                txtOrder.Text = _cateObj.OrderLinks.ToString();
                txtThumbnail.Text = _cateObj.Logo.ToString();
                if (_cateObj.Logo.Length > 0)
                    this.ImgTemp.Src = HPCComponents.Global.TinPathBDT + "/" + _cateObj.Logo;
                else this.ImgTemp.Attributes.CssStyle.Add("display", "none");
                cbo_lanquage.SelectedValue = _cateObj.Lang_ID.ToString();
                ddlType.SelectedValue = _cateObj.IsType.ToString();
            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
            else
                this.ImgTemp.Attributes.CssStyle.Add("display", "none");
        }


        #endregion

        #region Đồng bộ dữ liệu
        private ServicesInfor.WebLinks SetItemSyn(double News_ID)
        {
            HPCBusinessLogic.DAL.T_WebLinksDAL _untilDAL = new HPCBusinessLogic.DAL.T_WebLinksDAL();
            ServicesInfor.WebLinks obj = new ServicesInfor.WebLinks();
            HPCInfo.T_WebLinks objGet = new HPCInfo.T_WebLinks();
            objGet = _untilDAL.load_T_WebLinks(int.Parse(News_ID.ToString()));
            obj.ID = objGet.ID;
            obj.Lang_ID = objGet.Lang_ID;
            obj.URL = objGet.URL;
            obj.Description = objGet.Description;
            obj.Logo = objGet.Logo;
            obj.OrderLinks = objGet.OrderLinks;
            return obj;
        }
        private void SynData_InsertT_WebLinks(double Alb_Photo_ID, string urlService)
        {
            if (urlService.Length > 0)
            {
                ServicesInfor.WebLinks obj = SetItemSyn(Alb_Photo_ID);
                ServicesPutDataBusines.WebLinksDAL _PutDataDAL = new ServicesPutDataBusines.WebLinksDAL(urlService);
                try
                {
                    _PutDataDAL.InsertWebLinks(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void SynData_InsertT_WebLinksOne(double News_ID, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    SynData_InsertT_WebLinks(News_ID, _arr[i].ToString());
                }
            }
        }
        #endregion
    }
}
