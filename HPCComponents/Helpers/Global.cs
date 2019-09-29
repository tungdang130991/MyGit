using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Web;
using System.Configuration;
using System.Globalization;
using System.Collections;
namespace HPCComponents
{
    public class Global : System.Web.HttpApplication
    {
        public static string GetAppPath(HttpRequest request)
        {
            string path = string.Empty;
            try
            {
                if (request.ApplicationPath != "/")
                {
                    path = request.ApplicationPath + "/";
                }
                else
                {
                    path = request.ApplicationPath;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return path;
        }
        public static int DefaultLangID = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultLangID"]);
        public static string getImpersonat_User()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["Impersonat_User"]);
        }
        public static string getImpersonat_Pass()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["Impersonat_Pass"]);
        }
        public static string getImpersonat_Domain()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["Impersonat_Domain"]);
        }
        public static string ApplicationPath = ConfigurationManager.AppSettings["ApplicationPath"];
        public static string MultimediaPath = ConfigurationManager.AppSettings["MultimediaPath"];
        public static string VNPResizeImages = ConfigurationManager.AppSettings["VNPResizeImages"];
        public static string VNPResizeImagesPhoto = ConfigurationManager.AppSettings["VNPResizeImagesPhoto"];
        public static string VNPResizeImagesContent = ConfigurationManager.AppSettings["VNPResizeImagesContent"];
        public static string UploadPath = ConfigurationManager.AppSettings["UploadPath"];
        public static string UploadPathBDT = ConfigurationManager.AppSettings["UploadPathBDT"];
        public static string UploadPhotoAlbum = ConfigurationManager.AppSettings["UploadPhotoAlbum"];
        public static string DefaultCombobox = ConfigurationManager.AppSettings["DefaultCombobox"];
        public static string UploadPhotoEvent = ConfigurationManager.AppSettings["UploadPhotoEvent"];
        public static string TinPath = ConfigurationManager.AppSettings["tinpath"];
        public static string TinPathBDT = ConfigurationManager.AppSettings["tinpathbdt"];
        public static string FileQuangCao = ConfigurationManager.AppSettings["PathPhysicalFileQuangCao"];
        public static string PathQuangCao = ConfigurationManager.AppSettings["FolderQuangCao"];
        public static string MaXuatBan = ConfigurationManager.AppSettings["MaXuatBan"];
        public static string ImagesServiceSyn = ConfigurationManager.AppSettings["ImagesService"];
        public static string Pathfiledoc = ConfigurationManager.AppSettings["Pathfiledoc"];
        public static string PathImageFTP = ConfigurationManager.AppSettings["PathImageFTP"];

        #region TIN VAN
        public static int VNPTINVANTIENGVIET = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGVIET"]);
        public static int VNPTINVANTIENGANH = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGANH"]);
        public static int VNPTINVANTIENGPHAP = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGPHAP"]);
        public static int VNPTINVANTIENGTBN = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGTBN"]);
        public static int VNPTINVANTIENGNHAT = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGNHAT"]);
        public static int VNPTINVANTIENGTRUNG = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGTRUNG"]);
        public static int VNPTINVANTIENGNGA = Convert.ToInt32(ConfigurationManager.AppSettings["VNPTINVANTIENGNGA"]);
        public static string VNPTINVANALL = System.Configuration.ConfigurationSettings.AppSettings["VNPTINVANALL"];

        #endregion END TIN VAN

        #region BEGIN SYNC
        public static ArrayList Path_Service
        {
            get
            {
                ArrayList _arr = new ArrayList();

                string _str = System.Configuration.ConfigurationSettings.AppSettings["PuDataService"];
                if (_str.Length > 6)
                {
                    char[] ch = { ';' };
                    string[] arr = _str.Split(ch);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        _arr.Add(arr[i]);
                    }
                }
                return _arr;
            }
        }
        public static ArrayList ImagesService
        {
            get
            {
                ArrayList _arr = new ArrayList();
                string _str = System.Configuration.ConfigurationSettings.AppSettings["ImagesService"];
                if (_str.Length > 6)
                {
                    char[] ch = { ';' };
                    string[] arr = _str.Split(ch);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        _arr.Add(arr[i]);
                    }
                }
                return _arr;
            }
        }
        public static ArrayList Path_Replate
        {
            get
            {
                ArrayList _arr = new ArrayList();

                string _str = System.Configuration.ConfigurationSettings.AppSettings["Path_Replate"];
                char[] ch = { ';' };
                string[] arr = _str.Split(ch);
                for (int i = 0; i < arr.Length; i++)
                {
                    _arr.Add(arr[i]);
                }
                return _arr;

            }
        }
        #endregion END

        public static ResourceManager RM = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public static int MembersPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["MembersPerPage"]);
        public static int PhotosPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PhotosPerPage"]);
        //VietnamNews Online status
        public static string GetStatusT_NewsFrom_T_version(Object ID)
        {
            string str = "";
            int status = 0;
            HPCBusinessLogic.DAL.T_NewsDAL Dal = new HPCBusinessLogic.DAL.T_NewsDAL();
            if (Dal.load_T_news(Convert.ToInt32(ID)) == null)
                status = 0;
            else
                status = (int)Dal.load_T_news(Convert.ToInt32(ID)).News_Status;
            switch (status)
            {
                case 1: str = CommonLib.ReadXML("lblNhaptinbai"); break;
                case 7: str = CommonLib.ReadXML("lblTrinhbay"); break;
                case 8: str = CommonLib.ReadXML("lblBientap"); break;
                case 9: str = CommonLib.ReadXML("lblDuyettinbai"); break;
                case 12: str = CommonLib.ReadXML("lblTinmoi"); break;
                case 13: str = CommonLib.ReadXML("lblTralainguoinhaptin"); break;
                case 72: str = CommonLib.ReadXML("lblChotrinhbay"); break;
                case 73: str = CommonLib.ReadXML("lblTralaitrinhbay"); break;
                case 82: str = CommonLib.ReadXML("lblChobientap"); break;
                case 83: str = CommonLib.ReadXML("lblTralaibientap"); break;
                case 92: str = CommonLib.ReadXML("lblBaichoduyet"); break;
                case 4: str = CommonLib.ReadXML("lblTinngungdang"); break;
                case 6: str = CommonLib.ReadXML("lblTinxuatban"); break;
                case 55: str = CommonLib.ReadXML("lblTindaxoa"); break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        //end
        //VietnamNews newspapers
        public static string GetTrangThaiFrom_T_version(Object ID)
        {
            string str = "";
            string Doituongdangxuly = "";
            double Trangthai = 0;
            HPCBusinessLogic.DAL.TinBaiDAL Dal = new HPCBusinessLogic.DAL.TinBaiDAL();
            if (Dal.load_T_news(Convert.ToInt32(ID)) != null)
            {
                Doituongdangxuly = Dal.load_T_news(Convert.ToInt32(ID)).Doituong_DangXuly;
                Trangthai = Dal.load_T_news(Convert.ToInt32(ID)).Trangthai;
            }
            switch (Doituongdangxuly)
            {
                case "PVHCM":
                    if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusReporterHCMBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #DAA520\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusReporterHCMTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #DAA520\"></div>";
                    break;
                case "PVHN":
                    if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusReporterHNBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FFD700\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusReporterHNTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FFD700\"></div>";
                    break;
                case "MOREINFO":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusMoreInfoTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #20B2AA\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusMoreInBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #20B2AA\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusMoreInTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #20B2AA\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    break;
                case "EXPATHN":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusExpatHNTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #CD853F\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusExpatHNBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #CD853F\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusExpatHNTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #CD853F\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    break;
                case "EXPATHCM":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusExpatHCMTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #F4A460\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusExpatHCMBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #F4A460\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusExpatHCMTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #F4A460\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    break;
                case "DESKHN":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusDeskHNTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #0000FF\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusDeskHNBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #0000FF\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusDeskHNTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #0000FF\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    else if (Trangthai == 5)
                        str = CommonLib.ReadXML("lblSatusDeskHNBaigac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FFCC99\"></div>";
                    break;
                case "DESKHCM":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusDeskHCMTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #00008B\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusDeskHCMBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #00008B\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusDeskHCMTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #00008B\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    else if (Trangthai == 5)
                        str = CommonLib.ReadXML("lblSatusDeskHCMBaigac");
                    break;


                case "CHIEF":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblSatusChiefTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #8B4513\"></div>";
                    else if (Trangthai == 2)
                        str = CommonLib.ReadXML("lblSatusChiefBientap") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #8B4513\"></div>";
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblSatusChiefTralai") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #8B4513\"></div>";
                    else if (Trangthai == 4)
                        str = CommonLib.ReadXML("lblThungrac") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #FF3030\"></div>";
                    break;


                case "DT":
                    str = CommonLib.ReadXML("lblSatusDesignerTinmoi") + "</br><div style=\"float: left; width: 100%; height: 20px; background-color: #32CD32\"></div>";
                    break;

            }
            return str;
        }
        public static string GetActionName(Object ID)
        {
            HPCBusinessLogic.DAL.TinBaiDAL Dal = new HPCBusinessLogic.DAL.TinBaiDAL();
            string str = "";
            string doituongdaxuly = "";
            double Trangthai = 0;
            if (ID != DBNull.Value)
            {
                doituongdaxuly = Dal.Sp_SelectOneFromT_PhienBan(double.Parse(ID.ToString())).Doituong_DangXuly;
                Trangthai = Dal.Sp_SelectOneFromT_PhienBan(double.Parse(ID.ToString())).Trangthai;
            }
            switch (doituongdaxuly.Trim())
            {
                case "PVHCM":
                    if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblActionReturnReporterHCM");
                    break;
                case "PVHN":
                    if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblActionReturnReporterHN");
                    break;
                case "MOREINFO":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendMoreInfo");
                    else
                        str = CommonLib.ReadXML("lblActionReturnMoreInfo");
                    break;
                case "DESKHN":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendDeskHN");
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblActionReturnDeskHN");
                    else if (Trangthai == 5)
                        str = CommonLib.ReadXML("lblActionBaigacDeskHN");
                    break;
                case "DESKHCM":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendDeskHCM");
                    else if (Trangthai == 3)
                        str = CommonLib.ReadXML("lblActionReturnDeskHCM");
                    else if (Trangthai == 5)
                        str = CommonLib.ReadXML("lblActionBaigacDeskHCM");
                    break;

                case "CHIEF":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendChief");
                    else
                        str = CommonLib.ReadXML("lblActionReturnChief");
                    break;

                case "EXPATHN":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendExpatHN");
                    else
                        str = CommonLib.ReadXML("lblActionReturnExpatHN");
                    break;
                case "EXPATHCM":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendExpatHCM");
                    else
                        str = CommonLib.ReadXML("lblActionReturnExpatHCM");
                    break;
                case "DT":
                    if (Trangthai == 1)
                        str = CommonLib.ReadXML("lblActionSendDesigner");
                    break;
            }
            return str;
        }
        public static string GetHoatdong(Object Hoatdong)
        {
            string strReturn = "";
            if (Hoatdong != DBNull.Value)
            {
                if (bool.Parse(Hoatdong.ToString()))
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
                else
                    strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            }
            else
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            return strReturn;
        }
        public static string GetTrangThaiFrom_T_Quangcao(Object Trangthai)
        {
            string str = "";
            switch (Trangthai.ToString())
            {
                case "12":
                    str = CommonLib.ReadXML("lblSatusNVQCPending");
                    break;
                case "13":
                    str = CommonLib.ReadXML("lblSatusNVQCReturn");
                    break;
                case "21":
                    str = CommonLib.ReadXML("lblSatusTPQCPending");
                    break;
                case "22":
                    str = CommonLib.ReadXML("lblSatusTPQCEditor");
                    break;
                case "23":
                    str = CommonLib.ReadXML("lblSatusTPQCReturn");
                    break;

                case "41":
                    str = CommonLib.ReadXML("lblSatusTBTPending");
                    break;
                case "42":
                    str = CommonLib.ReadXML("lblSatusTBTEditor");
                    break;
                case "43":
                    str = CommonLib.ReadXML("lblSatusTBTReturn");
                    break;

                case "51":
                    str = CommonLib.ReadXML("lblSatusTLBTPending");
                    break;
                case "52":
                    str = CommonLib.ReadXML("lblSatusTLBTEditor");
                    break;
                case "53":
                    str = CommonLib.ReadXML("lblSatusTLBTReturn");
                    break;
                case "6":
                    str = CommonLib.ReadXML("lblSatusDTPending");
                    break;
            }
            return str;
        }
        public static string GetTrangThaiFrom_T_PhienbanQuangcao(Object Trangthai)
        {
            string str = "";
            switch (Trangthai.ToString())
            {
                case "12":
                    str = CommonLib.ReadXML("lblActionSendNVQC");
                    break;
                case "13":
                    str = CommonLib.ReadXML("lblActionReturnNVQC");
                    break;
                case "21":
                    str = CommonLib.ReadXML("lblActionSendTPQC");
                    break;
                case "23":
                    str = CommonLib.ReadXML("lblActionRetunTPQC");
                    break;

                case "41":
                    str = CommonLib.ReadXML("lblActionSendTBTQC");
                    break;
                case "43":
                    str = CommonLib.ReadXML("lblActionReturnTBTQC");
                    break;

                case "51":
                    str = CommonLib.ReadXML("lblActionSendTLBTQC");
                    break;
                case "53":
                    str = CommonLib.ReadXML("lblActionReturnTBTQC");
                    break;
                case "6":
                    str = CommonLib.ReadXML("lblActionSendDTQC");
                    break;
            }
            return str;
        }
        //end
    }
}
