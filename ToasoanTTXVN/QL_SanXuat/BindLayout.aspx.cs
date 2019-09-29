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
    public partial class BindLayout : System.Web.UI.Page
    {
        private int mabao = 0;
        private int trang = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request["mabao"] != null)
                        mabao = Convert.ToInt32(Request["mabao"].ToString());
                    if (Request["trangbao"] != null)
                        trang = Convert.ToInt32(Request["trangbao"].ToString());
                    BinData(mabao, trang);
                }
                catch (Exception ex) { throw ex; }
            }
        }
        private void BinData(int masobao, int trang)
        {
            try
            {
                HPCBusinessLogic.VitriTinbaiDAL _objVitritb = new HPCBusinessLogic.VitriTinbaiDAL();
                DataSet _ds;
                _ds = _objVitritb.T_Vitri_Tinbai_SelectBySoBaoAndPageNo(masobao, trang);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    rptbindData.DataSource = _ds.Tables[0];
                    rptbindData.DataBind();
                }
                _ds.Clear();
            }
            catch (Exception ex) { throw ex; }
        }
        protected string CheckDisplay(object idCV, object idTB, object Vitri)
        {
            string str = string.Empty;
            if (idTB.ToString() != "")
                str = "<input id=\"txtTB" + Vitri.ToString() + "\" type=\"text\" value=\"" + idTB.ToString() + "\"/>";
            if (idCV.ToString() != "")
                str = "<input id=\"txtCV" + Vitri.ToString() + "\" type=\"text\" value=\"" + idCV.ToString() + "\"/>";
            return str;
        }
        protected string CheckBindTextTitle(object MaCV,object MaTB,object textTB,object textCV)
        {
            string str = "";
            if (MaCV.ToString() != "")
                str = "<span>Công việc:</span> "+textCV.ToString();
            else if (MaTB.ToString() != "")
                str = "<span>Tin bài:</span> " + textTB.ToString();
            return str;
        }
        protected string CheckBindTextTitleAdv(object MaQC, object TenQC)
        {
            string str = "";
            if (MaQC.ToString() != "")
                str = "<span>Quảng cáo: </span> " + TenQC.ToString();
            return str;
        }
        protected string CheckBindText(object MaCV,object MaTB, object textTB, object textCV)
        {
            string str = "";
            if (MaCV.ToString() != "")
                str = "<span>Người nhân:</span> " + textCV.ToString();
            else if(MaTB.ToString()!="")
                str = "<span>Tác giả:</span> " + textTB.ToString();
            return str;
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

        protected string CheckTypeLayout()
        {
            try
            {
                string str = "";
                HPCBusinessLogic.VitriTinbaiDAL _objVitritb = new HPCBusinessLogic.VitriTinbaiDAL();
                DataSet _ds;
                _ds = _objVitritb.GetTypeLayout(mabao, trang);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    DataRow mrow = _ds.Tables[0].Rows[0];
                    if (mrow["Type"].ToString() == "2")
                        str = "<div class=\"separated\"></div>";
                }
                _ds.Clear();
                return str;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
