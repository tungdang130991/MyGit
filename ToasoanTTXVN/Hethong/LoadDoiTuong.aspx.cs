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
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;

namespace ToasoanTTXVN.Hethong
{
    public partial class LoadDoiTuong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UltilFunc Ulti = new UltilFunc();
            HPCBusinessLogic.DAL.QuyTrinhDAL _QTDAL = new HPCBusinessLogic.DAL.QuyTrinhDAL();
            HPCBusinessLogic.DoituongDAL _DTDAL = new HPCBusinessLogic.DoituongDAL();
            if (!IsPostBack)
            {
                int maanpham = int.Parse(Request["maanpham"]);
                DataSet _ds = _DTDAL.BindT_Doituong_AnPham(maanpham);
                rptDoituong.DataSource = _ds;
                rptDoituong.DataBind();
                DataSet _dsDTGui = _QTDAL.Bind_DoituongGui(maanpham);
                string dtGui = _dsDTGui.Tables[0].Rows[0][0].ToString();
                ctl00_MainContent_lblDTGui.Value = dtGui;
                DataSet _dsDTNhan = _QTDAL.Bind_DoituongNhan(maanpham);
                string dtNhan = _dsDTNhan.Tables[0].Rows[0][0].ToString();
                ctl00_MainContent_lblDTNhan.Value = dtNhan;
            }
        }
    }
}
