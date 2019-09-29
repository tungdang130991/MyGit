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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using Rainbow.MergeEngine;
using Word;
using System.Net;
using System.Drawing;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Ionic.Zip;
namespace ToasoanTTXVN.Quangcao
{
    public partial class ViewDTQC : BasePage
    {
        public string Tenquangcao;
        public string Noidung;
        public string Loaibao;
        public string Kichthuoc;
        public string Sotrang;
        public string Ngaydang;
        public string Nguoinhap;
        public string nguoisua;
        public string nguoisuaprev;
        HPCBusinessLogic.DAL.QuangCaoDAL _DalQC = new HPCBusinessLogic.DAL.QuangCaoDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        UltilFunc _ulti = new UltilFunc();
        int _MaPhienbanQC = 0;
        int _MaNguoinhap = 0;
        int id = 0;
        string ftpuser = ConfigurationManager.AppSettings["FTP_Username"].ToString();
        string password = ConfigurationManager.AppSettings["FTP_Password"].ToString();
        string ftpServer = ConfigurationManager.AppSettings["FTP_Server"].ToString();
        List<string> listPathFile = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = int.Parse(Page.Request.QueryString["ID"].ToString());
            btncopycontent1.Attributes.Add("onclick", "javascript:getvalue_exports()");
            btncopycontent2.Attributes.Add("onclick", "javascript:getvalue_exports()");
            if (Page.Request.QueryString["Menu_ID"] != null)
            {
                if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                    Response.Redirect("~/Errors/AccessDenied.aspx");
                _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                if (!IsPostBack)
                {
                    T_QuangCao obj = new T_QuangCao();
                    obj = _DalQC.GetOneFromT_QuangCaoByID(id);

                    Tenquangcao = obj.Ten_QuangCao.ToString();
                    Noidung = obj.Noidung.ToString();
                    Kichthuoc = UltilFunc.GetKichCoQuangCao(obj.Kichthuoc);
                    Loaibao = UltilFunc.GetTenAnpham(obj.Ma_Loaibao);
                    Ngaydang = obj.Ngaydang.ToString("dd/MM/yyyy");

                    if (obj.Trang > 0)
                        Sotrang = " Trang :" + obj.Trang.ToString();
                    else
                        Sotrang = "";

                    _MaNguoinhap = int.Parse(_ulti.GetColumnValuesDynamic("T_PhienBanQuangCao", " top 1 Nguoitao", "Nguoitao", "Ma_Quangcao=" + id + " order by ID ASC"));
                    if (_MaNguoinhap != 0)
                    {
                        if (_NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap) != null)
                            Nguoinhap = _NguoidungDAL.GetUserByUserName_ID(_MaNguoinhap).UserFullName;
                        else
                            Nguoinhap = "";
                    }
                    else
                    {
                        Nguoinhap = "";
                    }
                    if (_NguoidungDAL.GetUserByUserName_ID((int)obj.Nguoitao) != null)
                        nguoisua = _NguoidungDAL.GetUserByUserName_ID((int)obj.Nguoitao).UserFullName;
                    else
                        nguoisua = "User does not exist";

                    _MaPhienbanQC = int.Parse(_ulti.GetColumnValuesDynamic("T_PhienBanQuangCao", " top 1 ID", "ID", "Ma_Quangcao=" + id + " order by ID DESC"));

                    if (_MaPhienbanQC != 0)
                    {

                        ViewState["ver"] = _MaPhienbanQC.ToString();
                    }
                    else
                        ViewState["ver"] = -1;
                    LoadDataImageTinBai();
                }
            }
        }

        public void LoadDataImageTinBai()
        {
            string where = string.Empty;
            where = " Ma_Quangcao=" + Request["ID"].ToString();
            DataSet _ds = _DalQC.Sp_SelectT_FileQuangCaoDynamic(where, " NgayTao DESC ");
            this.DataListAnh.DataSource = _ds;
            this.DataListAnh.DataBind();
        }

        protected void btncopycontent_Click(object sender, EventArgs e)
        {
            Backup_File_Doc(id);
        }

        #region Event Click Img

        protected void btn_downloadimg_click(object sender, EventArgs e)
        {
            UploadFileMulti.FtpClient _Ftp = new ToasoanTTXVN.UploadFileMulti.FtpClient(ftpServer, ftpuser, password, "");

            LoadDataImageTinBai();
            if (DataListAnh.Items.Count != 0)
            {
                List<string> fileSave = new List<string>();
                foreach (DataListItem m_Item in DataListAnh.Items)
                {
                    Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;
                    string _pathfile = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lbFileAttach.Text);
                    byte[] file = _Ftp.DownloadFile(lbFileAttach.Text);
                    File.WriteAllBytes(_pathfile, file);
                    fileSave.Add(_pathfile);
                }
                /******************* ZIP FILE ******************************/


                string _zipFile = "VietNamNews-QC.zip";
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + _zipFile);
                using (ZipFile _zip = new ZipFile())
                {
                    foreach (string filePath in fileSave)
                    {
                        if (File.Exists(filePath))
                        {
                            _zip.AddFile(filePath, "VietNamNews-Quangcao");
                        }

                    }
                    _zip.Save(Response.OutputStream);
                }
                Response.End();

                /*************************************************/
            }

        }
        public void DataListAnh_EditCommand(object source, DataListCommandEventArgs e)
        {
            UploadFileMulti.FtpClient _Ftp = new ToasoanTTXVN.UploadFileMulti.FtpClient(ftpServer, ftpuser, password, "");
            if (e.CommandArgument.ToString().ToLower() == "download")
            {
                Label lbFileAttach = (Label)e.Item.FindControl("lbFileAttach");

                if (_Ftp.IsExistFolder(lbFileAttach.Text))
                {
                    byte[] file = _Ftp.DownloadFile(lbFileAttach.Text);
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(lbFileAttach.Text));
                    Response.Write(file);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    FuncAlert.AlertJS(this, "File không có trong Server");
                    return;
                }
            }
        }
        public Bitmap GetPhoto(string _filename)
        {
            System.Drawing.Bitmap imageresize = null;
            try
            {

                System.Drawing.Image img1;
                string url = ftpServer + _filename;
                try
                {
                    FtpWebRequest reqFTP;
                    string filename = Path.GetFileName(url);
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpuser, password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(ftpStream);
                    img1 = System.Drawing.Image.FromStream(ftpStream);
                    reader.Close();
                    ftpStream.Close();
                    response.Close();
                    imageresize = new Bitmap(img1);

                    img1.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch { ;}

            return imageresize;
        }
        #endregion

        #region Backup_FileDoc

        private void Backup_File_Doc(double QC_ID)
        {

            string _noidung = "";
            string _tieude = string.Empty;
            _tieude = _DalQC.GetOneFromT_QuangCaoByID(QC_ID).Ten_QuangCao;
            _noidung += store_content.Value.ToString();
            if (_noidung.Length > 0)
                Backup_fileRTF(_noidung, _tieude);
        }

        private void Backup_fileRTF(string _noidung, string _tieude)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += "<br>";
            strHTML += "<br>";
            strHTML += _noidung;
            strHTML += "</BODY></html>";
            int _lb = 0;
            HPCBusinessLogic.AnPhamDAL dalanpham = new AnPhamDAL();
            HPCBusinessLogic.SobaoDAL dalsb = new SobaoDAL();
            string _loaibao = string.Empty;
            string _Trang = string.Empty;

            T_QuangCao _obj = new T_QuangCao();
            _obj = _DalQC.GetOneFromT_QuangCaoByID(id);
            _lb = _obj.Ma_Loaibao;
            _loaibao = dalanpham.GetOneFromT_AnPhamByID(_lb).Ten_AnPham.ToString();
            if (_obj.Trang > 0)
                _Trang = _obj.Trang.ToString();

            string pathfolder = System.Configuration.ConfigurationManager.AppSettings["DanTrang"].ToString() + _loaibao + "/" + _Trang;
            string pathphysical = Context.Server.MapPath("/" + pathfolder);
            if (!File.Exists(pathphysical))
                Directory.CreateDirectory(pathphysical);
            strFileName = "Quangcao_" + _loaibao + "_Trang_" + _Trang + "_" + CommonLib.ReplaceCharsRewrite(_tieude) + ".rtf";
            string savepath = pathphysical + "/" + strFileName;
            try
            {
                StreamWriter wr = new StreamWriter(savepath, false, System.Text.Encoding.Unicode);
                wr.Write(strHTML);
                wr.Close();
                CreateWordFile(savepath);
                Page.Response.Redirect(pathfolder + "/" + strFileName);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CreateWordFile(string strFilePath)
        {
            //Creating the instance of Word Application
            Word.Application newApp = new Word.ApplicationClass();
            object Source = strFilePath;
            // specifying the Source & Target file names
            string strNewPath = strFilePath.Replace(".html", ".rtf");

            object Target = strNewPath;

            // Use for the parameter whose type are not known or  
            // say Missing
            object Unknown = Type.Missing;

            Word.Document objDoc;
            // Source document open here
            // Additional Parameters are not known so that are  
            // set as a missing type
            objDoc = newApp.Documents.Open(ref Source, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);

            // Specifying the format in which you want the output file 
            //object format = Word.WdSaveFormat.wdFormatDocument;
            object format = Word.WdSaveFormat.wdFormatRTF;
            objDoc.Activate();


            foreach (Paragraph rPrg in newApp.ActiveDocument.Paragraphs)
            {

                rPrg.Range.Font.Name = "VNI-Times";
                rPrg.Range.Font.Size = 14;
            }
            newApp.Selection.TypeParagraph();
            newApp.Selection.Font.Name = "VNI-Times";
            //Changing the format of the document			
            newApp.ActiveDocument.SaveAs(ref Target, ref format,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown);

            newApp.ActiveDocument.Save();
            // for closing the application
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
        }

        private void f_ReadWordContent(string strPathFile)
        {

            Word.ApplicationClass wordApp = new ApplicationClass();
            object file = @strPathFile;
            object o_true = true;
            object nullobj = System.Reflection.Missing.Value;
            Word.Document doc = wordApp.Documents.Open(ref file, ref nullobj, ref o_true, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);

            string strFile2Html = "";
            strFile2Html = strPathFile.Replace(".html", ".doc");
            object o_newfilename = strFile2Html;
            object o_format = Word.WdSaveFormat.wdFormatDocument;
            object o_encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
            object o_endings = Word.WdLineEndingType.wdCRLF;
            // SaveAs requires lots of parameters, but we can leave most of them empty:
            wordApp.ActiveDocument.SaveAs(ref o_newfilename, ref o_format, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref o_encoding, ref nullobj,
                ref nullobj, ref o_endings, ref nullobj);
            //-------

            doc.Close(ref nullobj, ref nullobj, ref nullobj);
            wordApp.Quit(ref nullobj, ref nullobj, ref nullobj);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);

        }

        public static string UnicodeToKoDau(string s)
        {
            string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

            string KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
            string retVal = String.Empty;
            if (s == null)
                return retVal;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        #endregion

    }
}
