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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ToasoanTTXVN.Quytrinh
{
    public partial class ChiefList : BasePage
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

        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        NgonNgu_DAL _dalLang = new NgonNgu_DAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        HPCBusinessLogic.DAL.TinBaiDAL _daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        T_TinBai _objtinbai = new T_TinBai();
        public int _Anphamdefault = 0;
        int _Ma_QTBT = 0;
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
                    _Ma_QTBT = UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + _user.UserID);
                    this.LinkAdd.Visible = _Role.R_Read;
                    this.LinkDelete.Visible = _Role.R_Delete;
                    Pager7.PageIndex = 0;
                    if (!IsPostBack)
                    {                       
                        if (_user == null)
                        {
                            Page.Response.Redirect("~/login.aspx", true);
                        }
                        else
                        {
                            LoadCombox();
                            if (Session["LoaibaoE"] != null && Session["SobaoE"] != null)
                            {
                                cbo_Anpham.SelectedValue = Session["LoaibaoE"].ToString();
                                if (cbo_Anpham.SelectedIndex > 0)
                                {
                                    UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);
                                    UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_Anpham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
                                    bintrang(int.Parse(cbo_Anpham.SelectedValue.ToString()));
                                }
                                cboSoBao.SelectedValue = Session["SobaoE"].ToString();
                                if (Session["TrangE"] != null)
                                    cboPage.SelectedValue = Session["TrangE"].ToString();
                            }
                            if (Session["PageIndex"] != null)
                                Pager7.PageIndex = int.Parse(Session["PageIndex"].ToString());

                            int tab_id = 0;
                            if (Request["Tab"] != null)
                                tab_id = Convert.ToInt32(Request["Tab"].ToString());

                            if (tab_id == -1)
                            {
                                this.TabContainer1.ActiveTabIndex = 0;
                                this.TabContainer1_ActiveTabChanged(sender, e);
                            }
                            else
                            {
                                this.TabContainer1.ActiveTabIndex = tab_id;
                                this.TabContainer1_ActiveTabChanged(sender, e);
                            }

                            BindDoiTuongGuiBai();

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
        public void LoadFileDoc(int _ID)
        {
            string strHTML = "";
            HPCBusinessLogic.DAL.TinBaiDAL dal = new HPCBusinessLogic.DAL.TinBaiDAL();
            T_TinBai obj = dal.load_T_news(_ID);

            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b>" + obj.Tieude + "<o:p></o:p></b></p>";
            strHTML += "<p class=MsoNormal style='mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><b><br>" + obj.Tomtat + "<u1:p></u1:p></b></p>";
            strHTML += "<p style='text-align:justify'>" + obj.Noidung + "<o:p></o:p></p>";
            if (strHTML.Length > 0)
                SaveAsText(strHTML);

        }
        private void SaveAsText(string _arr_IN)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html><BODY>";
            strHTML += _arr_IN;
            strHTML += "</BODY></html>";
            System.IO.DirectoryInfo r = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(HPCComponents.Global.GetAppPath(Request)));
            FileInfo[] file;
            file = r.GetFiles("*.doc");
            foreach (FileInfo i in file)
            {
                File.Delete(r.FullName + "\\" + i.Name);
            }
            strFileName = _user.UserName + "_Phongvien_" + string.Format("{0:dd-MM-yyyy_hh-mm-ss}", System.DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~" + HPCShareDLL.Configuration.GetConfig().FilesPath + "/FilePrintView/" + strFileName + ".doc");
            StreamWriter wr = new StreamWriter(path, false, System.Text.Encoding.Unicode);
            wr.Write(strHTML);
            wr.Close();
            Page.Response.Redirect(HPCComponents.Global.ApplicationPath + "/FilePrintView/" + strFileName + ".doc");
        }
        public void LoadCombox()
        {
            _Anphamdefault = UltilFunc.GetColumnValuesOne("T_AnPham", " Ma_AnPham", "Ma_QT=" + _Ma_QTBT);
            UltilFunc.BindCombox(cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " Ma_QT in (select Ma_QTBT from T_NguoidungQTBT where Ma_Nguoidung=" + _user.UserID + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
            if (_Anphamdefault > 0)
                cbo_Anpham.SelectedValue = _Anphamdefault.ToString();
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();
            if (cbo_Anpham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_Anpham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");

            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

                cboSoBao.DataSource = null;
                cboSoBao.DataBind();
            }
            bintrang(int.Parse(cbo_Anpham.SelectedValue));
            
        }
        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                if (TabContainer1.ActiveTabIndex != 2)

                    return " Ngaytao DESC";
                else
                    return " Ma_Tinbai DESC";
            }
        }
        string BuildSQL(int status, string sOrder)
        {
            string _where = " Trangthai_Xoa=0 and Doituong_DangXuly=N'" + Request["MaDoiTuong"].ToString() + "'";

            if (status == 2)
                _where += " and Ma_Nguoitao =" + _user.UserID + " and Trangthai=" + status;
            else
                _where += " and Trangthai=" + status;

            _where += " and  Ma_Chuyenmuc in (select Ma_Chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID + ") ";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));

            if (cbo_Anpham.SelectedIndex > 0)
                _where += " AND Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString() + ")";

            if (cboSoBao.SelectedIndex > 0)
                _where += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
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
        string GetWhere_ThungRac(string sOrder)
        {
            string _where = " Trangthai_Xoa=1 and Doituong_DangXuly= N'" + Request["MaDoiTuong"].ToString() + "'";

            _where += " and  Ma_Nguoitao =" + _user.UserID;


            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " and Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));

            if (cbo_Anpham.SelectedIndex > 0)
                _where += " AND Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString() + ")";

            if (cboSoBao.SelectedIndex > 0)
                _where += " and Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Ma_Sobao in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            if (cbo_chuyenmuc.SelectedIndex > 0)
                _where += " and Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();

            if (cboPage.SelectedIndex > 0)
                _where += " and Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboPage.SelectedValue + ")";


            if (sOrder.Length > 0)
                return _where + sOrder;
            else
                return _where;
        }
        string BuildSQL_Phienban(string sOrder)
        {
            string _where = " Ma_Nguoitao =" + _user.UserID + " and Sender= N'" + Request["MaDoiTuong"].ToString() + "'";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Ngaytao>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngaytao<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            else
                _where += " and Ngaytao>='" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            if (cbo_Anpham.SelectedIndex > 0)
                _where += " AND Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString() + ")";
            if (cboSoBao.SelectedIndex > 0)
                _where += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where Ma_Sobao=" + cboSoBao.SelectedValue.ToString() + ")";

            if (cbo_chuyenmuc.SelectedIndex > 0)
                _where += " AND Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();
            if (sOrder.Length > 0)
                return _where + sOrder;
            else
                return _where;
        }
        string GetWhereTinXuatBan(int status, string sOrder)
        {
            string _where = " Trangthai_Xoa=0 and Trangthai=" + status + " and Doituong_DangXuly='" + Global.MaXuatBan + "' ";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));

            if (cbo_Anpham.SelectedIndex > 0)
                _where += " AND Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_ChuyenMuc where Ma_AnPham=" + cbo_Anpham.SelectedValue.ToString() + ")";

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
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex, Pager7.PageSize, BuildSQL(1, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex - 1, Pager7.PageSize, BuildSQL(1, sOrder));
            DataGrid_TinMoi.DataSource = _ds;
            DataGrid_TinMoi.DataBind();


            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;
            Session["PageIndex"] = Pager7.PageIndex;

        }
        private void LoadData_DangXuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex, Pager7.PageSize, BuildSQL(2, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex - 1, Pager7.PageSize, BuildSQL(2, sOrder));
            DataGrid_TinDangXuLy.DataSource = _ds;
            DataGrid_TinDangXuLy.DataBind();


            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;
            Session["PageIndex"] = Pager7.PageIndex;

        }
        private void LoadData_Bitralai()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex, Pager7.PageSize, BuildSQL(3, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex - 1, Pager7.PageSize, BuildSQL(3, sOrder));
            DataGrid_TinTralai.DataSource = _ds;
            DataGrid_TinTralai.DataBind();


            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;
            Session["PageIndex"] = Pager7.PageIndex;

        }
        private void LoadData_Baidaxuly()
        {
            string sOrder = " ORDER BY Ngaytao DESC";
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Bind_PhienBanDynamic(Pager7.PageIndex, Pager7.PageSize, BuildSQL_Phienban(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.Bind_PhienBanDynamic(Pager7.PageIndex - 1, Pager7.PageSize, BuildSQL_Phienban(sOrder));
            DataGrid_TinDaXuLy.DataSource = _ds;
            DataGrid_TinDaXuLy.DataBind();

            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;

            Session["PageIndex"] = Pager7.PageIndex;


        }
        private void LoadData_ThungRac()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex, Pager7.PageSize, GetWhere_ThungRac(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex - 1, Pager7.PageSize, GetWhere_ThungRac(sOrder));
            DataGrid_Thungrac.DataSource = _ds;
            DataGrid_Thungrac.DataBind();


            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;
            Session["PageIndex"] = Pager7.PageIndex;

        }
        private void LoadData_TinXuatBan()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            Pager7.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex, Pager7.PageSize, GetWhereTinXuatBan(1, sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsEditor(Pager7.PageIndex - 1, Pager7.PageSize, BuildSQL(1, sOrder));
            DataGrid_TinXuatBan.DataSource = _ds;
            DataGrid_TinXuatBan.DataBind();


            Pager7.TotalRecords = CurrentPage7.TotalRecords = TotalRecords;
            CurrentPage7.TotalPages = Pager7.CalculateTotalPages();
            CurrentPage7.PageIndex = Pager7.PageIndex;
            Session["PageIndex"] = Pager7.PageIndex;

        }
        public void GetTotalRecordTinBai()
        {
            string _tinmoi = "0", _tindangxuly = "0", _tintralai = "0", _tindaxuly = "0", _thungrac = "0", _Tinxuatban = "0", _MaketPDF = "0";

            _tinmoi = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(1, ""));
            _tindangxuly = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(2, ""));
            _tintralai = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(3, ""));
            _thungrac = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", GetWhere_ThungRac(""));
            _Tinxuatban = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", GetWhereTinXuatBan(1, ""));
            _tindaxuly = ulti.GetColumnValuesTotal("T_PhienBan", "COUNT (Ma_Phienban) as Total", BuildSQL_Phienban(""));
            _MaketPDF = ulti.GetColumnValuesTotal("T_Publish_Pdf", "COUNT (ID) as Total", GetWhereFilePDF());
            this.tabpnltinChoXuly.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinmoi") + " (" + _tinmoi + ")";
            this.TabPanelDangxuly.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinbientap") + " (" + _tindangxuly + ")";
            this.TabPanelTinTraLai.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTintralai") + " (" + _tintralai + ")";
            this.TabPanelThungrac.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblThungrac") + " (" + _thungrac + ")";
            this.TabPanelTinDaXuLy.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindagui") + " (" + _tindaxuly + ")";
            this.TabPanelTinXuatBan.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinChoDanTrang") + " (" + _Tinxuatban + ")";
            TabPanelMaketPDF.HeaderText = " Layout PDF (" + _MaketPDF + ")";
        }
        private void GuiTinBai(string MaDoiTuong)
        {
            string Thaotac, _trace = string.Empty;
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in DataGrid_TinMoi.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinMoi.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in DataGrid_TinDangXuLy.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinDangXuLy.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in DataGrid_TinTralai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinTralai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 4)
            {
                foreach (DataGridItem m_Item in DataGrid_Thungrac.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_Thungrac.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            if (!_isRefresh)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    double News_ID = double.Parse(ar[i].ToString());
                    if (checksend(News_ID))
                    {

                        _trace = _daltinbai.GetTrace(News_ID) + Request["MaDoiTuong"].ToString() + "_" + _user.UserID + ";";
                        _daltinbai.Update_Status_tintuc(News_ID, 1, _user.UserID, DateTime.Now, MaDoiTuong, _trace);
                        _daltinbai.Insert_Phienban_From_T_Tinbai(News_ID, _user.UserID, DateTime.Now, Request["MaDoiTuong"].ToString());

                        Thaotac = "Thao tác gửi tin bài từ :" + CommonLib.GetTenDoiTuong(Request["MaDoiTuong"].ToString()) + "-->Gửi đến " + CommonLib.GetTenDoiTuong(MaDoiTuong) + " - Tiêu đề:" + _daltinbai.load_T_news(int.Parse(News_ID.ToString())).Tieude;

                        _daltinbai.IsLock(News_ID, 0);
                        UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, Thaotac, News_ID);
                        UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                        int _statusvnonline = UltilFunc.GetColumnValuesOne("T_News", "News_Status", "RefID=" + News_ID);
                        bool _vnnewsonline = _daltinbai.load_T_news(int.Parse(News_ID.ToString())).VietNamNews;
                        if (_vnnewsonline && MaDoiTuong.ToUpper() == Global.MaXuatBan)
                        {
                            // Insert sang bao dien tu
                            if (_statusvnonline == int.Parse(CommonLib.ReadXML("Status_BDT")) || _statusvnonline == 0)
                            {
                                HPCBusinessLogic.DAL.T_NewsDAL _T_NewsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
                                T_News _objT_News = SetItemBaoDienTu(News_ID);
                                int _id_dt = _T_NewsDAL.InsertT_news(_objT_News);
                                _T_NewsDAL.Update_Status_tintuc(_id_dt, int.Parse(CommonLib.ReadXML("Status_BDT")), _user.UserID, DateTime.Now);
                                UltilFunc.Insert_News_Image(_objT_News.News_Body.Trim(), Convert.ToDouble(_id_dt.ToString()));
                            }
                            //end
                        }

                    }
                }
                this.TabContainer1_ActiveTabChanged(null, null);
            }

        }
        private void TraLaiTin()
        {
            string Thaotac = "";

            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in DataGrid_TinMoi.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinMoi.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in DataGrid_TinDangXuLy.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinDangXuLy.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in DataGrid_TinTralai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinTralai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 4)
            {
                foreach (DataGridItem m_Item in DataGrid_Thungrac.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_Thungrac.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }


            if (!_isRefresh)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    double News_ID = double.Parse(ar[i].ToString());
                    string _trace = _daltinbai.GetTrace(News_ID);
                    if (_trace.Length > 0)
                    {
                        string[] _tmp = _trace.Split(';');
                        if (_tmp.Length > 0)
                        {
                            string _pos = _tmp[_tmp.Length - 2];
                            _trace = _trace.Substring(0, _trace.Length - _pos.Length - 1);
                            string _u = _pos.Substring(_pos.IndexOf("_") + 1);
                            _pos = _pos.Substring(0, _pos.Length - _u.Length - 1);
                            _daltinbai.Update_Status_tintuc(News_ID, 3, int.Parse(_u), DateTime.Now, _pos, _trace);
                            _daltinbai.Insert_Phienban_From_T_Tinbai(News_ID, _user.UserID, DateTime.Now, Request["MaDoiTuong"].ToString());
                            Thaotac = CommonLib.GetTenDoiTuong(Request["MaDoiTuong"]) + " Gửi trả lại tin bài cho " + CommonLib.GetTenDoiTuong(_pos) + " Tiêu đề: " + _daltinbai.load_T_news(News_ID).Tieude;
                            UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, Thaotac, News_ID);
                            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                        }
                    }
                    else
                    {
                        FuncAlert.AlertJS(this, "Không thể trả lại tin bài, vì không tìm được người gửi tin! ");
                        return;
                    }
                }

            }

        }
        private void XoaTinbai()
        {
            string Thaotac = "";
            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in DataGrid_TinMoi.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinMoi.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                foreach (DataGridItem m_Item in DataGrid_TinDangXuLy.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinDangXuLy.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                foreach (DataGridItem m_Item in DataGrid_TinTralai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_TinTralai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }
            else if (TabContainer1.ActiveTabIndex == 4)
            {
                foreach (DataGridItem m_Item in DataGrid_Thungrac.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_Thungrac.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            if (!_isRefresh)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    double News_ID = double.Parse(ar[i].ToString());

                    _daltinbai.Sp_DeleteT_Tinbai_WithTrangthai_Xoa(News_ID, _user.UserID);
                    Thaotac = "Thao tác xóa tin bài-->Tiêu đề:" + _daltinbai.load_T_news(int.Parse(News_ID.ToString())).Tieude;
                    _daltinbai.IsLock(News_ID, 0);
                    UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, Thaotac, News_ID);
                    UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
                    this.TabContainer1_ActiveTabChanged(null, null);
                }

            }

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
        public bool checksend(double _ID)
        {
            int _trang = 0;
            int _SoBao = 0;
            if (_ID > 0)
            {
                _objtinbai = _daltinbai.load_T_news(_ID);
                _trang = UltilFunc.GetColumnValuesOne("T_Vitri_Tinbai", "Trang", " Ma_Tinbai=" + _ID.ToString());
                _SoBao = UltilFunc.GetColumnValuesOne("T_Vitri_Tinbai", "Ma_Sobao", " Ma_Tinbai=" + _ID.ToString());
            }
            if (_objtinbai.Ma_Chuyenmuc == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn chuyên mục!");
                return false;
            }
            if (_SoBao == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn số báo!");
                return false;
            }
            if (_trang == 0)
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn trang!");
                return false;
            }
            return true;
        }

        #endregion

        #region Event Click
        protected void cbo_Anpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();

            if (cbo_Anpham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cbo_Anpham.SelectedValue.ToString()), 0);
                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cbo_Anpham.SelectedValue, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
            }
            else
            {
                cbo_chuyenmuc.DataSource = null;
                cbo_chuyenmuc.DataBind();

                cboSoBao.DataSource = null;
                cboSoBao.DataBind();

            }
            bintrang(int.Parse(cbo_Anpham.SelectedValue));
        }

        protected void cboSoBao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedValue != "" && cboSoBao.SelectedValue != "")
            {
                Session["LoaibaoL"] = cbo_Anpham.SelectedValue;
                Session["SobaoL"] = cboSoBao.SelectedValue;
            }
            if (cboPage.SelectedValue != "")
                Session["TrangL"] = cboPage.SelectedValue;
            GetTotalRecordTinBai();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                this.LoadData_ChoXuly();
                pnlbutton.Visible = true;

            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                this.LoadData_DangXuly();
                pnlbutton.Visible = true;

            }
            if (TabContainer1.ActiveTabIndex == 2)
            {
                this.LoadData_Bitralai();
                pnlbutton.Visible = true;

            }
            if (TabContainer1.ActiveTabIndex == 3)
            {
                this.LoadData_Baidaxuly();
                pnlbutton.Visible = false;
            }
            if (TabContainer1.ActiveTabIndex == 4)
            {
                this.LoadData_ThungRac();
                pnlbutton.Visible = true;

            }
            if (TabContainer1.ActiveTabIndex == 5)
            {
                this.LoadData_TinXuatBan();

                pnlbutton.Visible = true;

            }
            if (TabContainer1.ActiveTabIndex == 6)
            {
                this.LoadData_FilePDF();
                pnlbutton.Visible = true;

            }
            if (!IsPostBack)
            {
                Session["LoaibaoE"] = null;
                Session["SobaoE"] = null;
                Session["TrangE"] = null;
            }
        }

        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
            
        }

        public void Pager_IndexChanged(object sender, EventArgs e)
        {
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quytrinh/ChiefEditor.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + -1);
        }

        protected void SendBack_Click(object sender, EventArgs e)
        {
            TraLaiTin();
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            XoaTinbai();
            this.TabContainer1_ActiveTabChanged(sender, e);
        }

        protected void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string Thaotac = "";
            string _ID = string.Empty;
            if (TabContainer1.ActiveTabIndex == 0)
                _ID = DataGrid_TinMoi.DataKeys[e.Item.ItemIndex].ToString();
            else if (TabContainer1.ActiveTabIndex == 1)
                _ID = DataGrid_TinDangXuLy.DataKeys[e.Item.ItemIndex].ToString();
            else if (TabContainer1.ActiveTabIndex == 2)
                _ID = DataGrid_TinTralai.DataKeys[e.Item.ItemIndex].ToString();
            else if (TabContainer1.ActiveTabIndex == 4)
                _ID = DataGrid_Thungrac.DataKeys[e.Item.ItemIndex].ToString();
            else if (TabContainer1.ActiveTabIndex == 5)
                _ID = DataGrid_TinXuatBan.DataKeys[e.Item.ItemIndex].ToString();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int tab = 0;
                tab = TabContainer1.ActiveTabIndex;
                HPCBusinessLogic.DAL.TinBaiDAL Dal = new HPCBusinessLogic.DAL.TinBaiDAL();
                if (tab != 5)
                {
                    if (Dal.load_T_news(int.Parse(_ID)).Doituong_DangXuly == Request["MaDoiTuong"].ToString())
                    {
                        double Nguoikhoa = Dal.load_T_news(int.Parse(_ID)).Nguoi_Khoa;
                        if (Nguoikhoa > 0)
                            if (Nguoikhoa != _user.UserID)
                                Response.Redirect("~/Quytrinh/ChiefList.aspx?Menu_ID=" +
                                                  Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() +
                                                  "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + tab);
                            else
                            {
                                Dal.IsLock(double.Parse(_ID), _user.UserID);
                                Response.Redirect("~/Quytrinh/ChiefEditor.aspx?Menu_ID=" +
                                                  Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() +
                                                  "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + tab);

                            }
                        else
                        {
                            Dal.IsLock(double.Parse(_ID), _user.UserID);
                            Response.Redirect("~/Quytrinh/ChiefEditor.aspx?Menu_ID=" + Request["Menu_ID"].ToString() +
                                              "&ID=" + _ID.ToString() + "&MaDoiTuong=" +
                                              Request["MaDoiTuong"].ToString() + "&Tab=" + tab);

                        }
                    }
                    else
                    {
                        FuncAlert.AlertJS(this,
                                          "Tin bài đã được gửi đi, bạn không thể biên tập lại, vui lòng biên tập tin khác! cảm ơn");
                        return;
                    }
                }
                else
                {

                    double Nguoikhoa = Dal.load_T_news(int.Parse(_ID)).Nguoi_Khoa;
                    if (Nguoikhoa > 0)
                    {
                        if (Nguoikhoa != _user.UserID)
                            Response.Redirect("~/Quytrinh/ChiefList.aspx?Menu_ID=" +
                                              Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() +
                                              "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + tab);
                        else
                        {
                            Dal.IsLock(double.Parse(_ID), _user.UserID);
                            Response.Redirect("~/Quytrinh/ChiefEditor.aspx?Menu_ID=" +
                                              Request["Menu_ID"].ToString() + "&ID=" + _ID.ToString() +
                                              "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + tab);

                        }
                    }
                    else
                    {
                        Dal.IsLock(double.Parse(_ID), _user.UserID);
                        Response.Redirect("~/Quytrinh/ChiefEditor.aspx?Menu_ID=" + Request["Menu_ID"].ToString() +
                                          "&ID=" + _ID.ToString() + "&MaDoiTuong=" +
                                          Request["MaDoiTuong"].ToString() + "&Tab=" + tab);

                    }
                }
            }

            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                HPCBusinessLogic.DAL.TinBaiDAL DalTinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
                DalTinbai.Sp_DeleteT_Tinbai_WithTrangthai_Xoa(double.Parse(_ID), _user.UserID);
                this.TabContainer1_ActiveTabChanged(null, null);
                Thaotac = "Thao tác xoá tin tại: " + Request["MaDoiTuong"].ToString() + " - ID:" + _ID + "-" + DalTinbai.load_T_news(int.Parse(_ID.ToString())).Tieude;
            }
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            if (e.CommandArgument.ToString().ToLower() == "downloadfile")
            {
                LoadFileDoc(int.Parse(_ID));
            }

        }

        protected void DataGrid_OnItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
                LinkButton linkTittle = (LinkButton)e.Item.FindControl("linkTittle");
                if (linkTittle != null)
                    if (!_Role.R_Write)
                        linkTittle.Enabled = _Role.R_Write;

                if (btnDelete != null)
                    if (!_Role.R_Delete)
                        btnDelete.Enabled = _Role.R_Delete;
                    else
                        btnDelete.Attributes.Add("onclick", "return confirm('Bạn có muốn xóa tin này không?');");

            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion

        #region Send Tin Bai Theo Doi Tuong
        protected void DataListDoiTuong_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string MaDoiTuong = e.CommandArgument.ToString();
            GuiTinBai(MaDoiTuong);
            this.TabContainer1_ActiveTabChanged(source, e);
        }

        private DataTable BindDoiTuongGuiBai()
        {
            DataSet _ds = new DataSet();
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            if (Request["MaDoiTuong"] != null && _Ma_QTBT != 0)
            {
                if (Session["culture"] != null)
                    _ds = _T_newsDAL.Bind_DoiTuongGui(Request["MaDoiTuong"].ToString(), _Ma_QTBT, Session["culture"].ToString());
                else
                    _ds = _T_newsDAL.Bind_DoiTuongGui(Request["MaDoiTuong"].ToString(), _Ma_QTBT, "vi");
                DataListDoiTuong.DataSource = _ds;
                DataListDoiTuong.DataBind();
            }
            else
            {
                DataListDoiTuong.DataSource = null;
                DataListDoiTuong.DataBind();

            }
            return _ds.Tables[0];
        }
        #endregion

        #region Methods vnnews online

        protected T_ImageFiles SetItemImgFile(string _tenFile, double _size, string _pathfile, string _extenfile, int _userID, Int16 vType, double Chuyenmuc)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            _obj.ImageFileName = _tenFile.ToString();
            _obj.ImageFileSize = _size;
            _obj.ImageFileExtension = _extenfile.ToString();
            _obj.ImageType = vType;
            _obj.ImgeFilePath = _pathfile.ToString();
            _obj.Status = 0;
            _obj.UserCreated = _userID;
            _obj.DateCreated = DateTime.Now;
            _obj.Categorys_ID = Chuyenmuc;

            return _obj;
        }
        private T_News SetItemBaoDienTu(double _id_newspaper)
        {
            T_ImageFiles _obj = new T_ImageFiles();
            ImageFilesDAL _dalimgfile = new ImageFilesDAL();
            DataTable _dt_img = new DataTable();
            T_News obj_news = new T_News();
            T_TinBai _objbaoin = new T_TinBai();
            _objbaoin = _daltinbai.load_T_news(_id_newspaper);
            double _id_newsonline = UltilFunc.GetColumnValuesOne("T_News", "News_ID", "News_CopyFrom=" + _id_newspaper);
            if (_id_newsonline == 0)
                obj_news.News_ID = 0;
            else
                obj_news.News_ID = _id_newsonline;
            obj_news.News_Tittle = _objbaoin.Tieude;
            obj_news.News_Summary = _objbaoin.Tomtat;
            obj_news.CAT_ID = _objbaoin.Ma_Chuyenmuc;
            obj_news.Lang_ID = _objbaoin.Ma_NgonNgu;

            string _sqlimg = string.Empty;
            _sqlimg = "select TenFile_Hethong,Duongdan_Anh,Chuthich from t_anh where ma_anh in (select Ma_Anh from T_Tinbai_Anh where Ma_TinBai=" + _id_newspaper + ")";
            _dt_img = ulti.ExecSqlDataSet(_sqlimg).Tables[0];
            string DesPath = string.Empty;
            string PathSource = string.Empty;
            string UrlImg = string.Empty;
            if (_dt_img != null && _dt_img.Rows.Count > 0)
            {
                for (int i = 0; i < _dt_img.Rows.Count; i++)
                {
                    if (_dt_img.Rows.Count > 1)
                    {
                        DesPath = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                        DesPath = HttpContext.Current.Server.MapPath("/" + DesPath);
                        if (Directory.Exists(DesPath) == false)
                            Directory.CreateDirectory(DesPath);
                        PathSource = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + _dt_img.Rows[i]["Duongdan_Anh"].ToString());
                        DesPath += Path.GetFileName(PathSource);
                        UrlImg = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + Path.GetFileName(PathSource);
                        ResizeImages(PathSource, Convert.ToInt32(HPCComponents.Global.VNPResizeImagesContent), DesPath);
                        if (i == 0)
                        {
                            string _strremove = "/" + UrlImg.Split('/').GetValue(1).ToString();
                            string _Images_Summary = UrlImg.Replace(_strremove, "");
                            obj_news.Images_Summary = _Images_Summary;
                            obj_news.News_Body = _objbaoin.Noidung;
                        }
                        //insert table T_ImageFiles
                        int _idImgFile = 0;

                        int startchar = UrlImg.Substring(1, UrlImg.Length - 1).IndexOf("/");
                        startchar += 1;
                        string _PathFile = UrlImg.Substring(startchar, UrlImg.Length - startchar);

                        _obj = SetItemImgFile(_dt_img.Rows[i]["TenFile_Hethong"].ToString(), 0, _PathFile, "", _user.UserID, 1, 0);
                        _idImgFile = _dalimgfile.InsertT_ImageFiles(_obj);

                        //end
                        obj_news.News_Body += "<table border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"width: 450px;\"><tbody><tr><td><img border=\"0\" hspace=\"3\" id=\"" + _idImgFile + "\" src=\"" + UrlImg + "\" style=\"cursor-pointer\" vspace=\"3\" /></td></tr>";
                        obj_news.News_Body += "<tr><td>" + _dt_img.Rows[i]["Chuthich"].ToString() + "</tr></td></tbody></table>";
                    }
                    else
                    {
                        DesPath = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                        DesPath = HttpContext.Current.Server.MapPath("/" + DesPath);
                        if (Directory.Exists(DesPath) == false)
                            Directory.CreateDirectory(DesPath);
                        PathSource = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + _dt_img.Rows[i]["Duongdan_Anh"].ToString());
                        DesPath += Path.GetFileName(PathSource);
                        UrlImg = System.Configuration.ConfigurationManager.AppSettings["UrlImageResize"].ToString() + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + Path.GetFileName(PathSource);
                        ResizeImages(PathSource, Convert.ToInt32(HPCComponents.Global.VNPResizeImagesContent), DesPath);
                        int _idImgFile = 0;
                        //insert table T_ImageFiles
                        int startchar = UrlImg.Substring(1, UrlImg.Length - 1).IndexOf("/");
                        startchar += 1;
                        string _PathFile = UrlImg.Substring(startchar, UrlImg.Length - startchar);

                        _obj = SetItemImgFile(_dt_img.Rows[i]["TenFile_Hethong"].ToString(), 0, _PathFile, "", _user.UserID, 1, 0);
                        _idImgFile = _dalimgfile.InsertT_ImageFiles(_obj);

                        //end
                        string _strremove = "/" + UrlImg.Split('/').GetValue(1).ToString();
                        string _Images_Summary = UrlImg.Replace(_strremove, "");
                        obj_news.Images_Summary = _Images_Summary;
                        obj_news.News_Body = "<table border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"width: 450px;\"><tbody><tr><td><img border=\"0\" hspace=\"3\" id=\"" + _idImgFile + "\" src=\"" + UrlImg + "\" style=\"cursor-pointer\" vspace=\"3\" /></td></tr>";
                        obj_news.News_Body += "<tr><td>" + _dt_img.Rows[i]["Chuthich"].ToString() + "</tr></td></tbody></table>";
                        obj_news.News_Body += _objbaoin.Noidung;
                    }
                }
            }
            else
                obj_news.News_Body = _objbaoin.Noidung;
            obj_news.News_PublishNumber = int.Parse(DateTime.Now.Month.ToString());
            obj_news.News_PublishYear = int.Parse(DateTime.Now.Year.ToString());
            obj_news.News_DateCreated = DateTime.Now;
            obj_news.News_DateEdit = DateTime.Now;
            obj_news.News_DatePublished = DateTime.Now;
            obj_news.News_DateApproved = DateTime.Now;
            obj_news.News_AuthorID = int.Parse(_objbaoin.Ma_Nguoitao.ToString());
            obj_news.News_AprovedID = _user.UserID;
            obj_news.News_EditorID = _user.UserID;
            obj_news.News_PublishedID = _user.UserID;
            obj_news.News_CopyFrom = 0;
            obj_news.RefID = int.Parse(_id_newspaper.ToString());
            obj_news.News_Status = int.Parse(CommonLib.ReadXML("Status_BDT"));

            return obj_news;
        }
       
        private bool ResizeImages(string SourcePath, int Width, string Despath)
        {
            bool success;
            string path = SourcePath;
            Bitmap _obj;
            System.Drawing.Image objImage;
            int imgwidth = 0;
            int imgheight = 0;

            decimal lnRatio;
            try
            {
                objImage = System.Drawing.Image.FromFile(path);
                if (objImage.Width > objImage.Height)
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)Width / objImage.Width;
                    imgwidth = Width;
                    decimal lnTemp = objImage.Height * lnRatio;
                    imgheight = (int)lnTemp;

                }
                // Create thumbnail
                _obj = new Bitmap(imgwidth, imgheight);

                Graphics grWatermark = Graphics.FromImage(_obj);
                grWatermark.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grWatermark.SmoothingMode = SmoothingMode.HighQuality;
                grWatermark.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grWatermark.CompositingQuality = CompositingQuality.HighQuality;
                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, imgwidth, imgheight);
                grWatermark.DrawImage(objImage, imageRectangle);
                this.saveJpeg(Despath, _obj, 100L);
                grWatermark.Dispose();
                _obj.Dispose();
                objImage.Dispose();
                success = true;
            }
            catch //(Exception ex)
            {
                success = false;
                //throw ex;
            }
            return success;
        }
        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
            img.Dispose();
        }
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        #endregion

        #region Event File PDF
        public string Get_Status(Object _ID)
        {
            string _status = string.Empty;
            if (_ID != DBNull.Value)
                if (int.Parse(_ID.ToString()) == 2)
                    return _status = "<span class=\"linkGridForm\" style=\"color: Red;font-weight:bold\"> Chờ duyệt</span>";
                else
                    return _status = "<span class=\"linkGridForm\" style=\"color: green;font-weight:bold\">Đã duyệt</span>";
            return _status;
        }
        public bool VisibleImgButton(Object _ID, int _index)
        {
            bool _status = false;
            if (_ID != DBNull.Value)
            {
                if (_index == 1)
                {

                    if (int.Parse(_ID.ToString()) == 2)
                        _status = true;
                    else
                        _status = false;
                }
                else
                {
                    if (int.Parse(_ID.ToString()) == 2)
                        _status = false;
                    else
                        _status = true;

                }
            }

            return _status;
        }
        string GetWhereFilePDF()
        {
            DateTime dtime = DateTime.Now.AddDays(1);
            string _where = "(Status=2 or Status=3)";
            if (cboPage.SelectedIndex > 0)
                _where += " and Page_Number=" + cboPage.SelectedValue;
            if (cboSoBao.SelectedIndex > 0)
                _where += " and Publish_Number_ID=" + cboSoBao.SelectedValue;
            else
                _where += " and Publish_Number_ID in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + dtime.ToString("dd/MM/yyyy") + "')";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Publish_Number_ID in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";

            return _where;
        }
        public void LoadData_FilePDF()
        {
            string _where = GetWhereFilePDF();
            string _sql = "select * from T_Publish_Pdf where " + _where;
            DataSet _ds = _daltinbai.Sp_SelectT_Publish_PdfDynamic(_where, " ID DESC");
            this.DataGrid_FilePDF.DataSource = _ds;
            this.DataGrid_FilePDF.DataBind();
        }
        protected void btnphanhoilayoutPDF_Clik(object sender, EventArgs e)
        {

            if (!_isRefresh)
            {
                if (DataGrid_FilePDF.Items.Count > 0)
                {
                    foreach (DataListItem m_Item in DataGrid_FilePDF.Items)
                    {
                        TextBox txt_chuthich = (TextBox)m_Item.FindControl("txt_chuthich");

                        int _ID = int.Parse(DataGrid_FilePDF.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        string _sqlupdate = "update T_Publish_Pdf set Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
                        ulti.ExecSql(_sqlupdate);

                    }
                    LoadData_FilePDF();
                }
                else
                {
                    FuncAlert.AlertJS(this, "Không có layout!");
                    return;
                }
            }
        }
        protected void btnduyetlayoutPDF_Clik(object sender, EventArgs e)
        {

            if (!_isRefresh)
            {
                if (DataGrid_FilePDF.Items.Count > 0)
                {
                    foreach (DataListItem m_Item in DataGrid_FilePDF.Items)
                    {
                        TextBox txt_chuthich = (TextBox)m_Item.FindControl("txt_chuthich");
                        int _ID = int.Parse(DataGrid_FilePDF.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        string _sqlupdate = "update T_Publish_Pdf set status=3,Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
                        ulti.ExecSql(_sqlupdate);

                    }
                    LoadData_FilePDF();
                }

                else
                {
                    FuncAlert.AlertJS(this, "Không có layout!");
                    return;
                }
                this.TabContainer1_ActiveTabChanged(sender, e);
            }
        }
        public void DataGrid_FilePDF_EditCommand(object source, DataListCommandEventArgs e)
        {
            string _sqlupdate = string.Empty;
            TextBox txt_chuthich = e.Item.FindControl("txt_chuthich") as TextBox;
            int _ID = Convert.ToInt32(DataGrid_FilePDF.DataKeys[e.Item.ItemIndex].ToString());
            if (e.CommandArgument.ToString().ToLower() == "ok")
            {
                _sqlupdate = "update T_Publish_Pdf set status=3,Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
            }
            if (e.CommandArgument.ToString().ToLower() == "undo")
            {
                _sqlupdate = "update T_Publish_Pdf set status=2,Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
            }
            if (e.CommandArgument.ToString().ToLower() == "reply")
            {
                _sqlupdate = "update T_Publish_Pdf set Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;

            }
            ulti.ExecSql(_sqlupdate);
            LoadData_FilePDF();
        }

        #endregion
    }
}
