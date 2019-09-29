using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;
using HPCServerDataAccess;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class BindTextCongviec : System.Web.UI.Page
    {
        private int _ID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request["IDcv"] != null)
                        _ID = Convert.ToInt32(Request["IDcv"].ToString());
                    BinData(_ID);
                }
                catch (Exception ex) { throw ex; }
            }
        }
        private void BinData(int id)
        {
            try
            {
                HPCBusinessLogic.VitriTinbaiDAL _objVitritb = new HPCBusinessLogic.VitriTinbaiDAL();
                DataSet _ds;
                _ds = _objVitritb.T_Congviec_GetById(id);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rptbindData.DataSource = _ds.Tables[0];
                    rptbindData.DataBind();
                }
                _ds.Clear();
            }
            catch (Exception ex) { throw ex; }
        }
        protected string BindUserName(string _Id)
        {
            string strReturn = "";
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
            T_Users _nguoidung = new T_Users();

            if (!String.IsNullOrEmpty(_Id) && Convert.ToInt32(_Id) > 0)
            {
                _nguoidung = _NguoidungDAL.GetUserByUserName_ID(Convert.ToInt32(_Id));
                strReturn = _nguoidung.UserFullName;
            }
            else
                strReturn = "";
            return strReturn;
        }
    }
}
