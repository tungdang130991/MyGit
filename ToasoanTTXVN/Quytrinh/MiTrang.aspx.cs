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
using HtmlAgilityPack;
using System.Collections.Generic;
using Ionic.Zip;

namespace ToasoanTTXVN.Quytrinh
{
    public partial class MiTrang : BasePage
    {
        private bool _refreshState;
        private bool _isRefresh;
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] AllStates = (object[])savedState;
                base.LoadViewState(AllStates[0]);
                _refreshState = bool.Parse(AllStates[1].ToString());
                _isRefresh = _refreshState ==
                bool.Parse(Session["__ISREFRESH"].ToString());
            }
            catch
            { }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        public string Tieude;
        public string Noidung;
        public string Chuyenmuc;
        UltilFunc ulti = new UltilFunc();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        ChuyenmucDAL dalcm = new ChuyenmucDAL();
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        TinBaiAnhDAL _DalTinAnh = new TinBaiAnhDAL();
        List<ListNewsDownload> listPathFile = new List<ListNewsDownload>();
        List<ListPhotoByNewsID> listPhotoPath = new List<ListPhotoByNewsID>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));


                    if (!IsPostBack)
                    {
                        LoadCombox();
                        cbo_chuyenmuc.Items.Clear();
                        cboSoBao.Items.Clear();
                        if (cboAnPham.SelectedIndex > 0)
                        {
                            UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 0);
                            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, "-Chọn chuyên mục-", "Ma_Chuyenmuc_Cha");

                            bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
                        }
                        else
                        {
                            cbo_chuyenmuc.DataSource = null;
                            cbo_chuyenmuc.DataBind();

                            cboSoBao.DataSource = null;
                            cboSoBao.DataBind();

                        }

                    }

                }
            }
        }

        #region Method
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _Role.R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _Role.R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _Role.R_Read;
        }
        private void bintrang(int _loaibao)
        {
            HPCBusinessLogic.AnPhamDAL dal = new AnPhamDAL();
            cboPage.Items.Clear();
            if (_loaibao > 0)
            {
                int _sotrang = int.Parse(dal.GetOneFromT_AnPhamByID(_loaibao).Sotrang.ToString());
                cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblChontrang"), "0", true));
                for (int j = 1; j < _sotrang + 1; j++)
                {
                    cboPage.Items.Add(new ListItem("Trang " + j.ToString(), j.ToString()));
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " Ma_QT in (select Ma_QTBT from T_NguoidungQTBT where Ma_Nguoidung=" + _user.UserID + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
            UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_Chuyenmuc", "Ten_Chuyenmuc", "T_Chuyenmuc", string.Format(" Ma_Chuyenmuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha", " Order by Ten_ChuyenMuc");
        }
        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                return " Ma_Tinbai DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string _where = " Trangthai_Xoa=0 and Trangthai=" + status + " and Doituong_DangXuly=N'" + Global.MaXuatBan + "' ";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));

            if (cboAnPham.SelectedIndex > 0)
                _where += " AND Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where Ma_AnPham=" + cboAnPham.SelectedValue.ToString() + ")";

            if (cboSoBao.SelectedIndex > 0)
                _where += " AND (Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Ma_Sobao =" + cboSoBao.SelectedValue + ") OR Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Ma_Sobao in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            if (cbo_chuyenmuc.SelectedIndex > 0)
                _where += " AND Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();

            if (cboPage.SelectedIndex > 0)
                _where += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboPage.SelectedValue + ")";


            if (sOrder.Length > 0)
                return _where + sOrder;
            else
                return _where;
        }
        private void LoadData_ChoXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex, pages.PageSize, BuildSQL(1, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(pages.PageIndex - 1, pages.PageSize, BuildSQL(1, sOrder));
            DataGrid_DanTrang.DataSource = _ds;
            DataGrid_DanTrang.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;

        }
        public string IsImageLock(string prmImgStatus)
        {
            string strReturn = "";
            if (prmImgStatus == "False")
                strReturn = Global.ApplicationPath + "/Dungchung/images/document_new.png";
            if (prmImgStatus == "True")
                strReturn = Global.ApplicationPath + "/Dungchung/images/lock.jpg";
            return strReturn;
        }
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        public string GetTrangBao()
        {
            if (cboPage.SelectedIndex > 0)
                return cboPage.SelectedValue;
            else
                return "0";
        }
        public string GetSoBao()
        {
            if (cboSoBao.SelectedIndex > 0)
                return cboSoBao.SelectedValue;
            else
                return "0";
        }

        #endregion

        #region Event Click
        protected void Linkdownloadfile_Click(object sender, EventArgs e)
        {
            DownloadAll(cboAnPham.SelectedItem.Text);
        }
        protected void cboSoBao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex > 0)
                LoadData_ChoXuly();
        }
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAnPham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonanpham"));
                return;
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                LoadData_ChoXuly();
            }
            else
            {
                FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonsobao"));
                return;
            }
        }

        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboAnPham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonanpham"));
                return;
            }
            LoadData_ChoXuly();
        }

        public void pages_IndexChanged_baichoxuly(object sender, EventArgs e)
        {
            LoadData_ChoXuly();
        }

        protected void ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quytrinh/Edit_PV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + -1);
        }

        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();
            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 0);
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");

            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }
            bintrang(int.Parse(cboAnPham.SelectedValue.ToString()));
        }
        protected void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            double _ID = double.Parse(DataGrid_DanTrang.DataKeys[e.Item.ItemIndex].ToString());

            if (e.CommandArgument.ToString().ToLower() == "download")
            {
                Label lbltieude = (Label)e.Item.FindControl("lbltieude");
                Label lblnoidung = (Label)e.Item.FindControl("lblnoidung");

                Backup_File_Doc(_ID, lbltieude.Text, lblnoidung.Text);
            }
        }
        protected void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnBack = (ImageButton)e.Item.FindControl("btnBack");
                LinkButton linkTittle = (LinkButton)e.Item.FindControl("linkTittle");
                if (!_Role.R_Read)
                {
                    if (btnBack != null)
                        btnBack.Enabled = _Role.R_Read;

                }
                else
                    if (btnBack != null)
                        btnBack.Attributes.Add("onclick", "return confirm('Bạn có muốn trả lại tin này không?');");
                if (!_Role.R_Write)
                    linkTittle.Enabled = _Role.R_Write;
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion

        #region Backup_FileDoc

        private void Backup_File_Doc(double News_ID, string _tieude, string _noidung)
        {
            _noidung = ConvertUnicode2Vni(_noidung);
            if (_noidung.Length > 0)
                Backup_fileRTF(_noidung, _tieude, News_ID);
        }
        private void Backup_fileRTF(string _arr_IN, string _tieude, double id)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html>";
            strHTML += "<BODY>";
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


            foreach (Word.Paragraph rPrg in newApp.ActiveDocument.Paragraphs)
            {

                rPrg.Range.Font.Name = "VNI-Times";
                rPrg.Range.Font.Size = 14;
            }
            //newApp.Selection.TypeParagraph();
            //newApp.Selection.Font.Name = "VNI-Times";
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

        private string ConvertUnicode2Vni(string htmlFileNameWithPath)
        {
            string PathAfterDownload = "";

            try
            {
                PathAfterDownload = PathAfterDownload + HPC_ConvertUni2Vni.ConvertUni2Vietnam(htmlFileNameWithPath, HPC_ConvertUni2Vni.FontDest.vni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PathAfterDownload;

        }
        #endregion

        #region Download All file
        private void GenerateRTFFile(string _Noidung, string _tieude, double id)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html>";
            strHTML += "    <BODY>";
            strHTML += ConvertUnicode2Vni(_Noidung);
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
            strFileName = CommonLib.ReplaceCharsRewrite(_tieude) + ".rtf";
            string savepath = Pathphysical + "/" + strFileName;

            try
            {
                StreamWriter wr = new StreamWriter(savepath, false, System.Text.Encoding.Unicode);
                wr.Write(strHTML);
                wr.Close();
                CreateWordFile(savepath);
                getListImage(id, _tieude);
                ListNewsDownload _item = new ListNewsDownload();
                _item.Tieude = CommonLib.ReplaceCharsRewrite(_tieude);
                _item.Duongdananh = savepath;
                listPathFile.Add(_item);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void DownloadAll(string _ZipFileName)
        {
            string _pathfolder = string.Empty;
            if (cboAnPham.SelectedIndex > 0)
                _pathfolder += cboAnPham.SelectedItem.Text;
            if (cboSoBao.SelectedIndex > 0)
                _pathfolder += "/" + cboSoBao.SelectedItem.Text.Replace("/", "-");
            if (cboPage.SelectedIndex > 0)
                _pathfolder += "/" + cboPage.SelectedItem.Text;
            string _zipFile = _ZipFileName + ".zip";
            try
            {
                DataTable _dt = new DataTable();

                foreach (DataGridItem _item in DataGrid_DanTrang.Items)
                {
                    string chuthichanh = string.Empty;
                    double _ID = double.Parse(DataGrid_DanTrang.DataKeys[_item.ItemIndex].ToString());
                    
                    CheckBox _chk = (CheckBox)_item.FindControl("optSelect");
                    Label lbltieude = (Label)_item.FindControl("lbltieude");
                    Label lblnoidung = (Label)_item.FindControl("lblnoidung");
                    if (_chk.Checked == true)
                    {
                        _dt = _DalTinAnh.ListPhotoByMatinbai(" Ma_TinBai =" + _ID.ToString()).Tables[0];
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            chuthichanh = "<p> <b><i>CHÚ THÍCH ẢNH:</i></b></p>";
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                chuthichanh += "<p> + [" + UltilFunc.GetPathPhoto_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 2) + "]: " + UltilFunc.GetChuthich_Anh(0, Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 2) + "</p>";
                            }
                        }
                        GenerateRTFFile(lblnoidung.Text + chuthichanh, lbltieude.Text.Replace("\r\n", string.Empty), _ID);
                    }
                }

                // Begin zip file and Download
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + _zipFile);
                using (ZipFile _zip = new ZipFile())
                {
                    //Zip tin bai file.rtf

                    foreach (ListNewsDownload _itemNews in listPathFile) // Loop with for.
                    {
                        _zip.AddFile(_itemNews.Duongdananh, _pathfolder + "/" + _itemNews.Tieude);
                    }


                    // Begin zip file anh
                    foreach (ListPhotoByNewsID _item in listPhotoPath)
                    {
                        _zip.AddFile(_item.Duongdananh, _pathfolder + "/" + CommonLib.ReplaceCharsRewrite(_item.Tieude));
                    }
                    _zip.Save(Response.OutputStream);
                }
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getListImage(double News_ID, string _Tieude)
        {

            DataTable _dt = _DalTinAnh.ListPhotoByMatinbai(" Ma_TinBai =" + News_ID.ToString()).Tables[0];
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                ListPhotoByNewsID photoitem = new ListPhotoByNewsID();
                string pathimg = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + UltilFunc.GetPathPhoto_Anh(Convert.ToInt32(_dt.Rows[i]["Ma_Anh"].ToString()), 1));
                if (File.Exists(pathimg))
                {
                    photoitem.Duongdananh = pathimg;
                    photoitem.Tieude = _Tieude;
                    listPhotoPath.Add(photoitem);
                }
            }

        }
        public class ListPhotoByNewsID
        {
            public string Tieude { get; set; }
            public string Duongdananh { get; set; }
        }
        public class ListNewsDownload
        {
            public string Tieude { get; set; }
            public string Duongdananh { get; set; }
        }
        #endregion

    }
}
