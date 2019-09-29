using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using HPCBusinessLogic;
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCShareDLL;
namespace HPCComponents
{
    public class CommonLib
    {
        #region Conversation functions
        public static bool CheckNullBool(object Value)
        {
            if ((Value == DBNull.Value) || (Value == null) || (Value.ToString().Trim() == "")) return false;
            else return Convert.ToBoolean(Value);
        }

        public static byte CheckNullByte(object Value)
        {
            if (Value == DBNull.Value) return 0;
            else return Convert.ToByte(Value);
        }

        public static int CheckNullInt(object Value)
        {
            if (Value == DBNull.Value) return 0;
            else return Convert.ToInt32(Value);
        }

        public static int CheckNullInt(string Value)
        {
            if (Value == "") return 0;
            else return Convert.ToInt32(Value);
        }

        public static double CheckNullDbl(object Value)
        {
            if (Value == DBNull.Value) return 0;
            else return Convert.ToDouble(Value);
        }

        public static float CheckNullFloat(object Value)
        {
            if (Value == DBNull.Value) return 0;
            else return Convert.ToSingle(Value);
        }

        public static long CheckNullLong(object Value)
        {
            if (Value == DBNull.Value) return 0;
            else return Convert.ToInt64(Value);
        }

        public static string CheckNullStr(object Value)
        {
            if (Value == DBNull.Value) return "";
            else return Value.ToString();
        }

        public static DateTime CheckNullDate(object Value)
        {
            if (Value == DBNull.Value) return DateTime.MinValue;
            else return Convert.ToDateTime(Value);
        }

        public static object CheckDBNullDate(object Value)
        {
            if (Convert.ToDateTime(Value) == DateTime.MinValue) return DBNull.Value;
            else return Convert.ToDateTime(Value);
        }

        public static string ShowNullDate(DateTime Value)
        {
            if (Value == DateTime.MinValue) return "";
            else return Value.ToString("dd/MM/yyyy");
        }

        public static DateTime GetNullDate(string Value)
        {
            if (Value.Trim() == "") return DateTime.MinValue;
            else return ToDate(Value, "dd/MM/yyyy");
        }

        public static DateTime ToDate(string x, string kieu)
        {
            try
            {
                int sp1 = x.IndexOf('/'), sp2 = x.LastIndexOf('/');
                int day, month, year;

                if (kieu.Equals("MM/dd/yyyy")) // MM/dd/yyyy hoac M/d/yyyy
                {
                    day = int.Parse(x.Substring(sp1 + 1, sp2 - sp1 - 1));
                    month = int.Parse(x.Substring(0, sp1));
                }
                else //'dd/MM/yyyy' hoac d/M/yyyy
                {
                    day = int.Parse(x.Substring(0, sp1));
                    month = int.Parse(x.Substring(sp1 + 1, sp2 - sp1 - 1));
                }
                year = int.Parse(x.Substring(sp2 + 1, 4));
                return new DateTime(year, month, day);
            }
            catch
            {
                throw new Exception("Sai kieu ngay thang");
            }
        }

        public static bool IsInt(string Value)
        {
            try
            {
                Convert.ToInt32(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ReadXML
        public static string ReadXML(string ItemName)
        {
            string strReturn = String.Empty;
            try
            {
                strReturn = (String)HttpContext.GetGlobalResourceObject("cms.language", ItemName);// Global.RM.GetString(ItemName);
            }
            catch (Exception ex)
            {
                strReturn = String.Empty;
            }
            return strReturn;
        }
        #endregion

        #region Other
        public static bool IsNumeric(string str)
        {
            bool temp = true;
            try
            {
                str = str.Trim();
                int foo = int.Parse(str);
            }
            catch
            {
                temp = false;
            }
            return temp;
        }
        public static int GetIndexControl(System.Web.UI.WebControls.DropDownList sControl, string iValue)
        {
            int iCount;
            int retVal = 0;
            iCount = sControl.Items.Count;
            for (int i = 0; i <= iCount - 1; i++)
            {
                if (sControl.Items[i].Value == iValue)
                {
                    return i;
                }
            }
            return retVal;
        }
        public static int GetLatestID(string TableName, string IDFieldName)
        {
            HPCBusinessLogic.UltilFunc _untilFuncDAL = new HPCBusinessLogic.UltilFunc();
            try
            {
                DataSet ds = _untilFuncDAL.GetStoreDataSet("SP_GetLatestID", new string[] { "@TableName", "@IDFieldName" }, new object[] { TableName, IDFieldName });
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
            }
            catch
            {
                return 0;
            }
        }
        public static int GetLatestID(string TableName, string IDFieldName, string Condition)
        {

            HPCBusinessLogic.UltilFunc _untilFuncDAL = new HPCBusinessLogic.UltilFunc();
            try
            {
                DataSet ds = _untilFuncDAL.GetStoreDataSet("SP_GetLatestID", new string[] { "@TableName", "@IDFieldName", "@Condition" }, new object[] { TableName, IDFieldName, Condition });
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
            }
            catch
            {
                return 0;
            }
        }
        #endregion
        public static string CheckImgView(Object imgPath)
        {
            string _isDisplay = "";
            if ((Convert.ToString(imgPath) != "") && (imgPath != DBNull.Value) && (Convert.ToString(imgPath).Length > 0))
            {
                return _isDisplay = "Display:yes;";
            }
            else
            {
                return _isDisplay = "Display:none;";
            }
            return _isDisplay;
        }
        public static string tinpathBDT(Object strFileName)
        {

            if (Convert.ToString(strFileName) != "")
                return System.Configuration.ConfigurationManager.AppSettings["tinpathbdt"] + "/" + CommonLib.CleanHTML(Convert.ToString(strFileName));
            else
                return "";
        }
        public static string tinpath(Object strFileName)
        {

            if (Convert.ToString(strFileName) != "")
                return System.Configuration.ConfigurationManager.AppSettings["tinpath"] + "/" + CommonLib.CleanHTML(Convert.ToString(strFileName));
            else
                return "";
        }
        public static string HPCOnmouseoverGrid()
        {
            return System.Configuration.ConfigurationManager.AppSettings["HPCOnmouseoverGrid"].ToString();
        }
        public static string HPCOnmouseoverOut()
        {
            return System.Configuration.ConfigurationManager.AppSettings["HPCOnmouseoverOut"].ToString();
        }
        public static string CleanWordHtml(string html)
        {
            StringCollection sc = new StringCollection();
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");
            // Get rid of classes and styles
            //			sc.Add(@"\s?class=\w+");
            sc.Add(@"\s+style='[^']+'");
            // Get rid of unnecessary tags
            sc.Add(@"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>");
            // Get rid of empty paragraph tags
            sc.Add(@"(<[^>]+>)+&nbsp;(</\w+>)+");
            // remove bizarre v: element attached to <img> tag
            sc.Add(@"\s+v:\w+=""[^""]+""");
            // remove extra lines
            sc.Add(@"(\n\r){2,}");
            foreach (string s in sc)
            {
                html = Regex.Replace(html, s, "", RegexOptions.IgnoreCase);
            }
            return html;
        }
        public static string CleanHTML(string Contents)
        {
            Contents = Regex.Replace(Contents, "<(select|option|script|style|title)(.*?)>((.|\n)*?)</(select|option|script|style|title)>", " ", RegexOptions.IgnoreCase);
            Contents = Regex.Replace(Contents, "&(nbsp|quot|copy);", "");
            Contents = Regex.Replace(Contents, "'", "''");
            Contents = Regex.Replace(Contents, "(;|--|create|drop|select|insert|delete|update|union|sp_|xp_)", "");
            Contents = Regex.Replace(Contents, "<([\\s\\S])+?>", " ", RegexOptions.IgnoreCase).Replace("  ", " ");
            return (Contents);
        }
        public static bool isParrentMenu(int Menu_ID)
        {
            bool _return = true;
            HPCBusinessLogic.UltilFunc _lib = new HPCBusinessLogic.UltilFunc();

            DataSet ds = _lib.GetStoreDataSet("SP_isParrentMenu", new string[] { "@Menu_ID" }, new object[] { Menu_ID });
            if (Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0)) > 0)
                _return = false;
            return _return;
        }
        public static string GetStatusUpdateContents(Object ID)
        {
            HPCInfo.T_TinBai _obj = new T_TinBai();
            HPCBusinessLogic.DAL.TinBaiDAL _daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value)
                {
                    _obj = _daltinbai.load_T_news(double.Parse(ID.ToString()));
                    if (_obj.UpdateContents == 0)
                        str = _obj.Tieude;
                    else
                        str = "<span style=\"font-family: arial; font-size: 14px; color: red; font-weight: bold;font-style: italic\">Tin mới</span></br>" + _obj.Tieude;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetTieuDeBaiGac(Object ID)
        {
            UltilFunc ulti = new UltilFunc();
            string _Trangthai = string.Empty;
            string _tieudetin = string.Empty;
            HPCBusinessLogic.DAL.TinBaiDAL _daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
            string str = "";
            try
            {
                if (ID != DBNull.Value)
                {
                    _tieudetin = _daltinbai.load_T_news(double.Parse(ID.ToString())).Tieude;
                    _Trangthai = UltilFunc.GetColumnValuesOne("T_PhienBan", "Trangthai", "Trangthai=5 and Ma_Tinbai=" + ID.ToString()).ToString();
                    if (_Trangthai == "0")
                        str = _tieudetin;
                    else
                        str = "<img  border=0 height='12px' src='" + Global.ApplicationPath + "/Dungchung/Images/baigac.gif' alt='Bài gác' title=' Bài gác'> <br/>" + _tieudetin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static string GetPathImgWordPDF(Object ID)
        {
            string str = "";
            if (ID == DBNull.Value)
                str = "";
            else
            {
                try
                {
                    string _ID = "";
                    _ID = System.IO.Path.GetExtension(ID.ToString());
                    if (_ID.ToString() == ".doc" || _ID.ToString() == ".docx" || _ID.ToString() == ".txt" || _ID.ToString() == ".rtf")
                        str = "../Dungchung/Images/word.jpg";
                    else
                    {
                        if (_ID.ToString() == ".pdf")
                            str = "../Dungchung/Images/pdf.png";
                        else if (_ID.ToString() == ".rar" || _ID.ToString() == ".zip")
                            str = "../Dungchung/Images/IconRar.png";
                        else
                            str = Global.PathImageFTP + ID.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return str;
        }
        public static string GetTenDoiTuong(string madoituong)
        {
            UltilFunc _ulti = new UltilFunc();
            string tendoituong = string.Empty;
            tendoituong = _ulti.GetColumnValuesDynamic("t_doituong", "ten_doituong", "ten_doituong", "ma_doituong='" + madoituong + "'");
            return tendoituong;

        }
        #region Backup_FileDoc

        public static void Backup_fileDoc(double NewsID, string _UserName, string _Chucnang)
        {
            string _noidung = string.Empty;
            string _tieude = string.Empty;
            string _loaibao = string.Empty;
            string _sobao = string.Empty;
            int _lb = 0;
            int _sb = 0;
            string strFileName = string.Empty;
            string strHTML = string.Empty;
            AnPhamDAL _dalanpham = new AnPhamDAL();
            ChuyenmucDAL dalcm = new ChuyenmucDAL();
            SobaoDAL dalsb = new SobaoDAL();
            HPCBusinessLogic.DAL.TinBaiDAL dalnews = new HPCBusinessLogic.DAL.TinBaiDAL();
            _tieude += dalnews.load_T_news(NewsID).Tieude;
            _tieude += System.Environment.NewLine;
            _noidung += dalnews.load_T_news(NewsID).Noidung;
            _noidung += System.Environment.NewLine;
            if (dalnews.load_T_news(NewsID).Ma_NgonNgu != 0)
            {
                _lb = dalnews.load_T_news(NewsID).Ma_Anpham;
                _loaibao += _dalanpham.GetOneFromT_AnPhamByID(_lb).Ten_AnPham;
            }
            if (dalnews.load_T_news(NewsID).Ma_Chuyenmuc != 0)
            {
                _lb = dalnews.load_T_news(NewsID).Ma_Chuyenmuc;
                _loaibao += _dalanpham.GetOneFromT_AnPhamByID(dalcm.GetOneFromT_ChuyenmucByID(_lb).Ma_AnPham).Ten_AnPham;
            }
            if (dalnews.load_T_news(NewsID).Ma_Sobao != 0)
            {
                _sb = dalnews.load_T_news(NewsID).Ma_Sobao;
                _sobao += dalsb.GetOneFromT_SobaoByID(_sb).Ten_Sobao;
            }

            if (_noidung.Length > 0)
            {

                strHTML += "<html><BODY>";
                strHTML += _tieude;
                strHTML += "<br />";
                strHTML += "<br />";
                strHTML += _noidung;
                strHTML += "</BODY></html>";
                DirectoryInfo r = new DirectoryInfo(HttpContext.Current.Server.MapPath("~" + System.Configuration.ConfigurationManager.AppSettings["BackupDoc"].ToString()));
                if (File.Exists(r.ToString()))
                {
                    FileInfo[] file;

                    file = r.GetFiles("*.doc");

                    foreach (FileInfo i in file)
                    {
                        File.Delete(r.FullName + "\\" + i.Name);
                    }
                }
                strFileName = _UserName + _Chucnang + "_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
                string pathStore = HttpContext.Current.Server.MapPath("~" + System.Configuration.ConfigurationManager.AppSettings["BackupDoc"].ToString() + _Chucnang + "/" + _loaibao + "/" + _sobao + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day);
                string path = HttpContext.Current.Server.MapPath("~" + System.Configuration.ConfigurationManager.AppSettings["BackupDoc"].ToString() + _Chucnang + "/" + _loaibao + "/" + _sobao + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + strFileName + ".doc");
                if (!File.Exists(pathStore))
                    Directory.CreateDirectory(pathStore);
                try
                {
                    StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
                    wr.Write(strHTML);
                    wr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
        public static string _returnimg(object _url)
        {
            string _root = string.Empty;
            string _fullPath = string.Empty;
            string tempath = _url.ToString();
            try
            {
                if (tempath.Length > 7)
                {
                    if (tempath.Contains("Video"))
                        _root = HPCComponents.Global.TinPathBDT;
                    else
                        _root = System.Configuration.ConfigurationManager.AppSettings["UploadPathBDT"];
                    _fullPath = _root + _url.ToString();

                }
                else
                    _fullPath = HPCComponents.Global.ApplicationPath + "/Dungchung/Images/Video-icon.png";
            }
            catch { }
            return _fullPath;
        }
        public static string ReplaceCharsRewrite(string input)
        {
            string str = "", StrTemp = RemoveHTMLTag(Convert.ToString(input));
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = StrTemp.Normalize(System.Text.NormalizationForm.FormD);
            str = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex objRegEx = new Regex("<[^>]*>");
            str = str.Replace(" ", "-");
            str = str.Replace("/", "");
            str = str.Replace(",", "");
            str = str.Replace(".", "");
            str = str.Replace(";", "");
            str = str.Replace(":", "");
            str = str.Replace("?", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("!", "");
            str = str.Replace("@", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("+", "");
            str = str.Replace("=", "");
            str = str.Replace("\\", "");
            str = str.Replace("|", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("”", "");
            str = str.Replace("“", "");
            str = str.Replace("-»", "");
            str = str.Replace("«-", "");
            str = str.Replace("»", "");
            str = str.Replace("»", "");
            str = str.Replace("«", "");
            str = str.Replace("’", "");
            str = str.Replace("--", "-");
            str = str.Replace("---", "-");
            str = str.Replace("----", "-");
            str = str.Replace("-----", "-");
            str = str.Replace(" ", "-");
            str = str.Replace("Ã°", "");
            str = str.Replace("â€", "");
            str = str.Replace("a€", "");
            str = str.Replace("a°", "");
            str = str.Replace("°", "");
            str = str.Replace("€", "");
            str = str.Replace("ß", "");
            str = str.Replace("¦", "");
            str = str.Replace("ßƒ", "");
            str = str.Replace("ƒ", "");
            str = str.Replace("v¿", "");
            str = str.Replace("¿", "");
            str = str.Replace("ð", "");
            str = str.Replace("¥", "");
            str = str.Replace("æ", "");
            str = str.Replace("¼", "");
            str = str.Replace("½", "");
            str = str.Replace("¬", "");
            str = str.Replace("cº", "");
            str = str.Replace("phº", "");
            str = str.Replace("ç", "");
            str = str.Replace("ßº", "");
            str = str.Replace("¢", "");
            str = str.Replace("Â¢", "");
            return str.ToLower();
        }
        public static string RemoveHTMLTag(string HTML)
        {
            // Xóa các thẻ html
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            return objRegEx.Replace(HTML, "");

        }
    }
}
