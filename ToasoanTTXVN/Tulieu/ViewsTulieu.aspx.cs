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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Tulieu
{
    public partial class ViewsTulieu : System.Web.UI.Page
    {
       
        public string Noidung;
        public string Chuyenmuc;
        public string Sotu;   
        public string Tacgia;
     
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Page.Request.QueryString["ID"].ToString());
            if (!IsPostBack)
            {
                

                if (Page.Request.QueryString["Menu_ID"] != null)
                {
                    T_Tulieu obj = new T_Tulieu();
                    obj = Daltinbai.load_T_Tulieu(id);

                    if (obj.Sotu != 0)
                        Sotu = obj.Sotu.ToString();
                    else
                        Sotu = "0";
                    

                    Chuyenmuc = dalcm.GetOneFromT_ChuyenmucByID(obj.Ma_Chuyenmuc).Ten_ChuyenMuc;
                    
                    Noidung = obj.Noidung.ToString();
                    Tacgia = obj.TacGia;                   


                }
            }
        }
    }
}
