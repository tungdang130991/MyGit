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
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using Microsoft.Office.Core;
using Word;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Collections.Generic;
using Ionic.Zip;
namespace ToasoanTTXVN.Quytrinh
{
    public partial class ViewTinbaiDantrang : System.Web.UI.Page
    {
        public string Tieude;
        public string Noidung;
        public string Chuyenmuc;
        List<string> listPathFile = new List<string>();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        double _ID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    //btncopycontent1.Attributes.Add("onclick", "javascript:getvalue_exports()");
                    //btncopycontent2.Attributes.Add("onclick", "javascript:getvalue_exports()");
                    _ID = int.Parse(Page.Request.QueryString["ID"].ToString());


                    if (!IsPostBack)
                    {
                        if (_user == null)
                        {
                            Page.Response.Redirect("~/login.aspx", true);
                        }
                        else
                        {
                            T_TinBai obj = new T_TinBai();
                            obj = Daltinbai.load_T_news(_ID);
                            Chuyenmuc = dalcm.GetOneFromT_ChuyenmucByID(obj.Ma_Chuyenmuc).Ten_ChuyenMuc;
                            Tieude = obj.Tieude.ToString();
                            Noidung = obj.Noidung.ToString();
                            BindGridPhotosByMatin();
                            if (DataListAnh.Items.Count != 0)
                                btn_downloadfile.Visible = true;
                            else
                                btn_downloadfile.Visible = false;
                        }
                    }
                }
            }
        }

        #region Method
        public void BindGridPhotosByMatin()
        {
            TinBaiAnhDAL _DAL = new TinBaiAnhDAL();
            DataSet _ds = _DAL.ListPhotoByMatinbai(" Ma_TinBai =" + Request["ID"].ToString());
            DataView _dv = BindGridPhotoOfNews(_ds.Tables[0], int.Parse(Request["ID"].ToString()));
            if (_ds != null)
            {
                this.DataListAnh.DataSource = _dv;
                this.DataListAnh.DataBind();
            }
        }
        public DataView BindGridPhotoOfNews(DataTable _dt, int _ma_tin)
        {

            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("Ma_Tin", typeof(string)));
                dt.Columns.Add(new DataColumn("Ma_Anh", typeof(string)));
                dt.Columns.Add(new DataColumn("TieuDe", typeof(string)));
                dt.Columns.Add(new DataColumn("Chuthich", typeof(string)));
                dt.Columns.Add(new DataColumn("Duongdan_Anh", typeof(string)));
                dt.Columns.Add(new DataColumn("Sotien", typeof(string)));
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = _ma_tin.ToString().Trim();
                        dr[1] = _dt.Rows[i]["Ma_Anh"].ToString();
                        if (_ma_tin != 0)
                        {
                            dr[2] = UltilFunc.GetTieude_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()));
                            if (UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 1) != "")
                                dr[3] = UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 1);
                            else
                                dr[3] = UltilFunc.GetChuthich_Anh(_ma_tin, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 2);
                            dr[4] = UltilFunc.GetPathPhoto_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()),1);
                        }
                        else
                        {
                            dr[2] = _dt.Rows[i]["TieuDe"].ToString();
                            dr[3] = _dt.Rows[i]["Chuthich"].ToString();
                            dr[4] = _dt.Rows[i]["Duongdan_Anh"].ToString();
                        }
                        dr[5] = UltilFunc.GetNhuanbut_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), _ma_tin);

                        dt.Rows.Add(dr);
                    }
                }
                DataView dv = new DataView(dt);
                return dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btncopycontent_Click(object sender, EventArgs e)
        {
            Backup_File_Doc(_ID);
        }
        #endregion

        #region Event Click Img
        private void CreateFolderByUserName(string FolderName)
        {
            string strRootPath = "";
            strRootPath = FolderName;
            if (Directory.Exists(strRootPath) == false)
                Directory.CreateDirectory(strRootPath);
        }
        public string GetDateTimeStringUnique()
        {
            string dateString = DateTime.Now.Millisecond.ToString();
            return dateString + DateTime.Now.ToLongTimeString().Replace("-", "").Replace(" ", "").Replace(":", "");
        }
        protected void btn_downloadimg_click(object sender, EventArgs e)
        {
            string _zipFile = "VietNamNews.zip";
            BindGridPhotosByMatin();
            if (DataListAnh.Items.Count != 0)
            {
                foreach (DataListItem m_Item in DataListAnh.Items)
                {
                    Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;
                    /******************* ZIP FILE ******************************/
                    string sourceFile = lbFileAttach.Text;
                    string _linkImage = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + sourceFile);
                    if (File.Exists(_linkImage))
                    {
                        listPathFile.Add(_linkImage);
                    }
                    
                }
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + _zipFile);
                using (ZipFile _zip = new ZipFile())
                {
                    foreach (string _itemNews in listPathFile) // Loop with for.
                    {
                        _zip.AddFile(_itemNews, "VietNamNews");
                    }

                    _zip.Save(Response.OutputStream);
                }
                Response.End();
            }



        }
        public void DataListAnh_EditCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "download")
            {
                Label lbFileAttach = (Label)e.Item.FindControl("lbFileAttach");
                string filePath = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lbFileAttach.Text);
                if (File.Exists(filePath))
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(lbFileAttach.Text));
                    Response.WriteFile(filePath);
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
        #endregion

        #region Backup_FileDoc

        private void Backup_File_Doc(double News_ID)
        {
            string _noidung = "";
            string _tieude = string.Empty;
            _tieude = Daltinbai.load_T_news(News_ID).Tieude;
            _noidung += txtOriginal.Value.ToString();
            if (_noidung.Length > 0)
                Backup_fileRTF(_noidung, _tieude, News_ID);
        }
        private void Backup_fileRTF(string _arr_IN, string _tieude, double id)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += "<br>";
            strHTML += "<br>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            int _lb = 0;
            HPCBusinessLogic.AnPhamDAL dalanpham = new AnPhamDAL();
            HPCBusinessLogic.SobaoDAL dalsb = new SobaoDAL();
            string _loaibao = string.Empty;
            string _sobao = string.Empty;
            string _Trang = string.Empty;

            DataTable dttrangsb = UltilFunc.GetTrangSoBaoFrom_T_VitriTiBai(int.Parse(id.ToString()));
            _lb = Daltinbai.load_T_news(id).Ma_Anpham;
            _loaibao = dalanpham.GetOneFromT_AnPhamByID(_lb).Ten_AnPham.ToString();
            if (dttrangsb.Rows.Count > 0)
            {
                _sobao += dalsb.GetOneFromT_SobaoByID(int.Parse(dttrangsb.Rows[0]["Ma_Sobao"].ToString())).Ten_Sobao;
                _Trang += dttrangsb.Rows[0]["Trang"].ToString();
            }
            string Pathfolder = System.Configuration.ConfigurationManager.AppSettings["DanTrang"].ToString() + _loaibao + "/" + _sobao + "/" + _Trang;
            string Pathphysical = Context.Server.MapPath("/" + Pathfolder);
            if (!File.Exists(Pathphysical))
                Directory.CreateDirectory(Pathphysical);
            strFileName = _loaibao + "_" + _sobao + "_" + _Trang + "_" + CommonLib.ReplaceCharsRewrite(_tieude) + ".rtf";
            string savepath = Pathphysical + "/" + strFileName;

            try
            {
                StreamWriter wr = new StreamWriter(savepath, false, System.Text.Encoding.Unicode);
                wr.Write(strHTML);
                wr.Close();
                CreateWordFile(savepath);
                Page.Response.Redirect(Pathfolder + "/" + strFileName);

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

        #endregion
    }
}
