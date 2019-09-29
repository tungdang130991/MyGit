using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class BindText : System.Web.UI.Page
    {
        private int _ID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request["IDlo"] != null)
                        _ID = Convert.ToInt32(Request["IDlo"].ToString());
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
                _ds = _objVitritb.T_Tinbai_GetById(id);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rptbindData.DataSource = _ds.Tables[0];
                    rptbindData.DataBind();
                }
                _ds.Clear();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
