using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using HPCComponents;
using HPCServerDataAccess;
using System.Text;
using System.Data.SqlClient;
//using WDF.Component;
using WDF.UI.WebControls;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class T_Album_Categories_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }
        protected void LoadData()
        {
            SqlService _service = new SqlService();
            int Catid = 0;
            if (Request["catps"] != null)
                Catid = Convert.ToInt32(Request["catps"]);
            DataSet _ds = null;
            //_service.AddParameter("@Cat_Album_ID", SqlDbType.Int, Catid);
            try
            {
                _ds = _service.ExecuteSqlDataSet("select * from T_Album_Photo where Cat_Album_ID= " + Catid.ToString());
                this.rptAlbumActive.DataSource = _ds.Tables[0].DefaultView;
                this.rptAlbumActive.DataBind();
            }
            catch
            {
                _ds.Clear(); _service.CloseConnect();
            }
            finally { _ds.Clear(); _service.CloseConnect(); _service.Disconnect(); }
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public string GetURLImage(object url)
        {
            string str = "";
            str = ConfigurationManager.AppSettings["tinpathbdt"].ToString() + url.ToString();
            return str;
        }

        public static string tinpath(Object strFileName, int _with, int _height, bool isCroped)
        {
            string _path = "";

            if (!string.IsNullOrEmpty(strFileName.ToString()))
            {
                _path = System.Configuration.ConfigurationSettings.AppSettings["tinpathbdt"];
                _path = _path + getFileNameFull(Convert.ToString(strFileName), _with, _height, isCroped);
            }

            return _path;

        }
        private static string getFileNameFull(string fullPath, int _with, int _height, bool isCroped)
        {
            char[] ch = { '/' };
            string[] arr = fullPath.Split(ch);
            string fileName = arr[arr.Length - 1];
            string[] _fileNameNonExt = fileName.Split('.');
            string strTemPath = "";
            if (_fileNameNonExt.Length > 1)
            {
                string _fileEtx = _fileNameNonExt[_fileNameNonExt.Length - 1].ToString();
                string _fileReplate = "";
                for (int i = 0; i < _fileNameNonExt.Length - 1; i++)
                {
                    if (_fileReplate == "") { _fileReplate += _fileNameNonExt[i].ToString(); }
                    else { _fileReplate += "." + _fileNameNonExt[i].ToString(); }
                }
                if (isCroped)
                    strTemPath = fullPath.Replace(fileName, _fileReplate + "_crop_wh" + _with + "_" + _height + "." + _fileEtx);
                else
                    strTemPath = fullPath.Replace(fileName, _fileReplate + "_wh" + _with + "_" + _height + "." + _fileEtx);
            }
            else
            {
                string _fileEtx = _fileNameNonExt[1].ToString();
                if (isCroped)
                    strTemPath = fullPath.Replace(fileName, _fileNameNonExt[0].ToString() + "_crop_wh" + _with + "_" + _height + "." + _fileEtx);
                else
                    strTemPath = fullPath.Replace(fileName, _fileNameNonExt[0].ToString() + "_wh" + _with + "_" + _height + "." + _fileEtx);
            }
            return strTemPath;
        }

        private static string getFileNameFull(string fullPath, int _with, int _height)
        {
            char[] ch = { '/' };
            string[] arr = fullPath.Split(ch);
            string fileName = arr[arr.Length - 1];
            string[] _fileNameNonExt = fileName.Split('.');
            if (_fileNameNonExt.Length > 1)
            {
                string _fileEtx = _fileNameNonExt[_fileNameNonExt.Length - 1].ToString();
                string _fileReplate = "";
                for (int i = 0; i < _fileNameNonExt.Length - 1; i++)
                {
                    if (_fileReplate == "") { _fileReplate += _fileNameNonExt[i].ToString(); }
                    else { _fileReplate += "." + _fileNameNonExt[i].ToString(); }
                }
                return fullPath.Replace(fileName, _fileReplate + "_wh" + _with + "_" + _height + "." + _fileEtx);
            }
            else
            {
                string _fileEtx = _fileNameNonExt[1].ToString();
                return fullPath.Replace(fileName, _fileNameNonExt[0].ToString() + "_wh" + _with + "_" + _height + "." + _fileEtx);
            }
        }
    }
}
