using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class updateItemLayout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int ID_ = 0;
                if (Request["ItemID"] != null)
                    ID_ = Convert.ToInt32(Request["ItemID"].ToString());
                VitriTinbaiDAL _objVitriTb = new VitriTinbaiDAL();
                _objVitriTb.T_Vitri_Tinbai_UpdateByID(ID_);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
