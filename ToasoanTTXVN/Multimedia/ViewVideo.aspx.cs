using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using HPCBusinessLogic.DAL;
namespace ToasoanTTXVN.Multimedia
{
    public partial class ViewVideo : System.Web.UI.Page
    {
        private int VodID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["ID"]))
                    return Convert.ToInt32(Request["ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "clearBuffer()", true);
        }
        public string videoImage()
        {
            string _return = "";
            T_MultimediaDAL _DAl = new T_MultimediaDAL();
            if (VodID != 0)
            {
                string _fileName = _DAl.GetOneFromT_MultimediaByID(VodID).URL_Images;
                _return = CommonLib._returnimg(_fileName);
            }
            return _return;
        }
        public string videoPath()
        {
            string _return = "";
            T_MultimediaDAL _DAl = new T_MultimediaDAL();
            if (VodID != 0)
            {
                string _pathfile = _DAl.GetOneFromT_MultimediaByID(VodID).URLPath.ToString();
                _return = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"] + _pathfile;
            }
            return _return;
        }
    }
}
