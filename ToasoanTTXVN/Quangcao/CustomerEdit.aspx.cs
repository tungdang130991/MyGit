using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCComponents;
using SSOLib.ServiceAgent;
using HPCInfo;

namespace ToasoanTTXVN.Quangcao
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected T_Users _user = null;
        protected T_RolePermission _Role = null;
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
                        DataBind();
                }
            }
        }
        #region User-methods
        private HPCInfo.T_Customers GetObject()
        {
            HPCInfo.T_Customers _objCate = new HPCInfo.T_Customers();
            if (Page.Request.Params["id"] != null)
                _objCate.ID = int.Parse(Page.Request["id"].ToString());
            else _objCate.ID = 0;
            _objCate.Name = UltilFunc.CleanFormatTags(txt_Name.Text);
            _objCate.Address = UltilFunc.CleanFormatTags(Txt_Address.Text);
            _objCate.Phone = UltilFunc.CleanFormatTags(txt_Phone.Text);
            _objCate.Fax = UltilFunc.CleanFormatTags(txt_Fax.Text);
            _objCate.Email = UltilFunc.CleanFormatTags(txt_Email.Text);
            _objCate.Date_Created = DateTime.Now;
            _objCate.Date_Modify = DateTime.Now;
            _objCate.User_Created = _user.UserID;
            _objCate.User_Modify = _user.UserID;
            return _objCate;
        }

        private void PopulateItem(int _id)
        {
            HPCInfo.T_Customers _cateObj = new HPCInfo.T_Customers();
            HPCBusinessLogic.DAL.T_CustomersDAL _cateDAL = new HPCBusinessLogic.DAL.T_CustomersDAL();
            _cateObj = _cateDAL.load_T_Customers(_id);
            if (_cateObj != null)
            {
                txt_Name.Text = _cateObj.Name.ToString();
                Txt_Address.Text = _cateObj.Address.ToString();
                txt_Phone.Text = _cateObj.Phone.ToString();
                txt_Email.Text = _cateObj.Email.ToString();
                txt_Fax.Text = _cateObj.Fax.ToString();
            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    HPCBusinessLogic.DAL.T_CustomersDAL dal = new HPCBusinessLogic.DAL.T_CustomersDAL();
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    PopulateItem(_id);
                }
            }
        }

        private void ResetForm()
        {
            Txt_Address.Text = "";
            txt_Email.Text = "";
            txt_Phone.Text = "";
            txt_Name.Text = "";
        }
        #endregion

        #region Event methods
        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txt_Name.Text.Length > 125)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Bạn nhập quá độ dài cho phép!');", true);
                    return;
                }
                HPCBusinessLogic.DAL.T_CustomersDAL _cateDAL = new HPCBusinessLogic.DAL.T_CustomersDAL();
                HPCInfo.T_Customers _catObj = GetObject();
                int _return = _cateDAL.InsertT_Customers(_catObj);
                if (_catObj.ID == 0)
                {

                    string strLog = "[Thêm mới]-->[Thao tác thêm mới khách hàng]ID:" + _return + " ]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Add News]", Request["Menu_ID"].ToString(), strLog, 0, ConstAction.TSAnh);
                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                }
                else
                {
                    string strLog = "[Sửa danh sách KH]-->[Thao tác sửa][ID:" + Page.Request["id"].ToString() + " ]";
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "[Update]", Request["Menu_ID"].ToString(), strLog, 0, ConstAction.TSAnh);
                    //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + Global.RM.GetString("UpdateSuccessfully") + "');", true);
                }
                Page.Response.Redirect("~/Quangcao/CustomerList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quangcao/CustomerList.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());
        }
        #endregion

        //#region SYNC----
        //private void SynData_InsertT_CustomersAny(int ID, ArrayList _arr)
        //{
        //    if (_arr.Count > 0)
        //    {
        //        for (int i = 0; i < _arr.Count; i++)
        //        {
        //            SynData_InsertT_Customers(ID, _arr[i].ToString());
        //        }
        //    }
        //}
        //private void SynData_InsertT_Customers(int ID, string urlService)
        //{
        //    ServicesInfor.Customers obj = SetItemSyn(ID);
        //    ServicesPutDataBusines.CustomersDAL _PutDataDAL = new ServicesPutDataBusines.CustomersDAL(urlService);
        //    try
        //    {
        //        _PutDataDAL.Insert_Customers(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private ServicesInfor.Customers SetItemSyn(int _ID)
        //{
        //    ServicesInfor.Customers obj = new ServicesInfor.Customers();
        //    HPCInfo.T_Customers _objGet = new T_Customers();
        //    HPCBusinessLogic.DAL.T_CustomersDAL _untilDAL = new HPCBusinessLogic.DAL.T_CustomersDAL();
        //    _objGet = _untilDAL.load_T_Customers(_ID);
        //    if (Page.Request.Params["ID"] != null)
        //        obj.ID = _objGet.ID;
        //    else
        //        obj.ID = _ID;
        //    obj.Name = _objGet.Name;
        //    obj.Address = _objGet.Address;
        //    obj.Phone = _objGet.Phone;
        //    obj.Email = _objGet.Email;
        //    obj.User_Created = _objGet.User_Created;
        //    obj.User_Modify = _objGet.User_Modify;
        //    if (_objGet.Date_Created != null && _objGet.Date_Modify != null)
        //    {
        //        obj.Date_Created = Convert.ToDateTime(_objGet.Date_Created);
        //        obj.Date_Modify = Convert.ToDateTime(_objGet.Date_Modify);
        //    }
        //    return obj;
        //}

        //#endregion
    }
}
