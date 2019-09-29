using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCComponents;
using HPCInfo;
using System.Configuration;

namespace ToasoanTTXVN.Quanlynhanbut
{
    public partial class ViewImage : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        T_Photo_Event _obj = new T_Photo_Event();
        T_Photo_EventDAL DAL = new T_Photo_EventDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        double _ID = 0;
                        if (Request["id"] != null)
                            _ID = Convert.ToDouble(Request["id"]);
                        if (_ID > 0)
                        {
                            _obj = DAL.GetOneFromT_Photo_EventsByID(_ID);
                            Image1.ImageUrl = ConfigurationManager.AppSettings["tinpathbdt"].ToString() + _obj.Photo_Medium;
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
