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
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Ionic.Zip;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace ToasoanTTXVN.Quytrinh
{
    public partial class Dantrang : BasePage
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
                    }
                    else
                    {
                        string EventName = Request.Form["__EVENTTARGET"].ToString();
                        if (EventName == "UploadImageSuccess")
                        {
                            LoadData_FilePDF();
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
            UltilFunc.BindCombox(cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " Ma_QT in (select Ma_QTBT from T_NguoidungQTBT where Ma_Nguoidung=" + _user.UserID + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Hoatdong=1 and Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ")", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonchuyenmuc"), "Ma_Chuyenmuc_Cha");
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
        string BuildSQL_Phienban(string sOrder)
        {
            string _where = " Ma_Nguoitao =" + _user.UserID;
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
            GetTotalRecordTinBai();
        }
        private void LoadData_Baidaxuly()
        {
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Bind_PhienBanDynamic(pages.PageIndex, pages.PageSize, BuildSQL_Phienban(sOrder));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.Bind_PhienBanDynamic(pages.PageIndex - 1, pages.PageSize, BuildSQL_Phienban(sOrder));
            DataGrid_Daxuly.DataSource = _ds;
            DataGrid_Daxuly.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
            GetTotalRecordTinBai();

        }
        public void GetTotalRecordTinBai()
        {
            string _tinmoi = "0", _tindaxuly = "0";

            _tinmoi = ulti.GetColumnValuesTotal("T_TinBai", "COUNT (Ma_Tinbai) as Total", BuildSQL(1, ""));

            _tindaxuly = ulti.GetColumnValuesTotal("T_PhienBan", "COUNT (Ma_Phienban) as Total", BuildSQL_Phienban(""));

            this.tabpnltinChoXuly.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTinmoi") + " (" + _tinmoi + ")";

            this.TabPanelTinDaXuLy.HeaderText = (string)HttpContext.GetGlobalResourceObject("cms.language", "lblTindagui") + " (" + _tindaxuly + ")";

        }
        private void SendBackTinbai()
        {
            string Thaotac = "";

            ArrayList ar = new ArrayList();
            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (DataGridItem m_Item in DataGrid_DanTrang.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_DanTrang.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                }
            }

            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_ChoXuly();
            HPCBusinessLogic.DAL.TinBaiDAL News = new HPCBusinessLogic.DAL.TinBaiDAL();
            if (!_isRefresh)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    double News_ID = double.Parse(ar[i].ToString());
                    string _trace = News.GetTrace(News_ID);
                    if (_trace.Length > 0)
                    {
                        string[] _tmp = _trace.Split(';');
                        if (_tmp.Length > 0)
                        {
                            string _pos = _tmp[_tmp.Length - 2];
                            _trace = _trace.Substring(0, _trace.Length - _pos.Length - 1);
                            string _u = _pos.Substring(_pos.IndexOf("_") + 1);
                            _pos = _pos.Substring(0, _pos.Length - _u.Length - 1);
                            News.Update_Status_tintuc(News_ID, 3, int.Parse(_u), DateTime.Now, _pos, _trace);
                            News.Insert_Phienban_From_T_Tinbai(News_ID, _user.UserID, DateTime.Now, Request["MaDoiTuong"].ToString());
                            Thaotac = CommonLib.GetTenDoiTuong(Request["MaDoiTuong"]) + " Gửi trả lại tin bài cho " + CommonLib.GetTenDoiTuong(_pos) + " Tiêu đề:" + News.load_T_news(News_ID).Tieude + " ]";
                            UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, Thaotac, News_ID);
                        }
                    }
                    else
                    {
                        FuncAlert.AlertJS(this, "Không thể trả lại tin bài, vì không tìm được người gửi tin! ");
                        return;
                    }
                }
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), Thaotac);
            }
            if (TabContainer1.ActiveTabIndex == 0)
                LoadData_ChoXuly();

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
            if (cboPage.SelectedIndex != 0)
                return cboPage.SelectedValue;
            else
                return "0";
        }
        public string GetSoBao()
        {
            if (cboSoBao.SelectedIndex != 0)
                return cboSoBao.SelectedValue;
            else
                return "0";
        }

        #endregion

        #region Event Click
        protected void cboSoBao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoBao.SelectedIndex > 0)
                LoadData_FilePDF();
            this.TabContainer1_ActiveTabChanged(sender, e);
        }
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblbanchuachonanpham"));
                return;
            }
            if (cboSoBao.SelectedIndex > 0)
            {
                LoadData_FilePDF();
                if (CheckSendPDF(cboPage.SelectedValue, cboSoBao.SelectedValue))
                    lblMessage.Visible = true;
                else
                    lblMessage.Visible = false;
                this.TabContainer1_ActiveTabChanged(sender, e);
            }
            else
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblbanchuachonsobao"));
                return;
            }
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                if (cbo_Anpham.SelectedIndex == 0)
                {
                    FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblbanchuachonanpham"));
                    return;
                }
                if (cboSoBao.SelectedIndex > 0 || txt_tungay.Text.Trim() != "" || txt_denngay.Text.Trim() != "")
                {
                    if (cboPage.SelectedIndex > 0)
                    {
                        LoadData_FilePDF();
                    }
                    this.LoadData_ChoXuly();
                }
                else
                {
                    FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblbanchuachonsobao"));
                    return;
                }
            }
            if (TabContainer1.ActiveTabIndex == 1)
                this.LoadData_Baidaxuly();

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, (string)HttpContext.GetGlobalResourceObject("cms.language", "lblbanchuachonanpham"));
                return;
            }
            if (CheckSendPDF(cboPage.SelectedValue, cboSoBao.SelectedValue))
                lblMessage.Visible = true;
            else
                lblMessage.Visible = false;
            this.TabContainer1_ActiveTabChanged(sender, e);
        }
        public void pages_IndexChanged_baichoxuly(object sender, EventArgs e)
        {
            LoadData_ChoXuly();
        }
        public void pages_IndexChanged_baidaxuly(object sender, EventArgs e)
        {
            LoadData_Baidaxuly();
        }
        protected void ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Quytrinh/Edit_PV.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&MaDoiTuong=" + Request["MaDoiTuong"].ToString() + "&Tab=" + -1);
        }
        protected void SendBack_Click(object sender, EventArgs e)
        {
            SendBackTinbai();
        }
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
        protected void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            double _ID = double.Parse(DataGrid_DanTrang.DataKeys[e.Item.ItemIndex].ToString());

            string _thaotac = string.Empty;

            if (e.CommandArgument.ToString().ToLower() == "back")
            {
                string _trace = Daltinbai.GetTrace(_ID);
                if (_trace.Length > 0)
                {
                    string[] _tmp = _trace.Split(';');
                    if (_tmp.Length > 0)
                    {
                        string _pos = _tmp[_tmp.Length - 2];
                        _trace = _trace.Substring(0, _trace.Length - _pos.Length - 1);
                        string _u = _pos.Substring(_pos.IndexOf("_") + 1);
                        _pos = _pos.Substring(0, _pos.Length - _u.Length - 1);
                        Daltinbai.Update_Status_tintuc(_ID, 3, int.Parse(_u), DateTime.Now, _pos, _trace);
                        Daltinbai.Insert_Phienban_From_T_Tinbai(_ID, _user.UserID, DateTime.Now, Request["MaDoiTuong"].ToString());
                        _thaotac = "[" + Request["MaDoiTuong"] + " Gửi trả lại tin bài cho " + _pos + " Tiêu đề:" + Daltinbai.load_T_news(_ID).Tieude + " ]";
                        UltilFunc.Log_Thaotactinbai(_user.UserID, _user.UserFullName, DateTime.Now, _thaotac, _ID);
                    }
                }
                else
                {
                    FuncAlert.AlertJS(this, "Không thể trả lại, vì không tìm được người gửi tin! ");
                    return;
                }
                this.TabContainer1_ActiveTabChanged(null, null);
            }
            LoadData_ChoXuly();
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
                        btnBack.Attributes.Add("onclick", "return confirm('Bạn có muốn trả lại?');");
                if (!_Role.R_Write)
                    linkTittle.Enabled = _Role.R_Write;
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #endregion

        #region Attach File PDF
        public void LoadData_FilePDF()
        {
            string _where = "(Status=2 or Status=3)";
            if (cboPage.SelectedIndex > 0)
                _where += " and Page_Number=" + cboPage.SelectedValue;
            if (cboSoBao.SelectedIndex > 0)
                _where += " and Publish_Number_ID=" + cboSoBao.SelectedValue;
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Publish_Number_ID in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            string _sql = "select * from T_Publish_Pdf where " + _where;
            DataSet _ds = Daltinbai.Sp_SelectT_Publish_PdfDynamic(_where, " ID DESC");
            this.DataGrid_FilePDF.DataSource = _ds;
            this.DataGrid_FilePDF.DataBind();
        }
        public string Get_Status(Object _ID)
        {
            string _status = string.Empty;
            if (_ID != DBNull.Value)
                if (int.Parse(_ID.ToString()) == 2)
                    return _status = "<span class=\"linkGridForm\" style=\"color: Red;font-weight:bold\"> Chờ duyệt</span>";
                else
                    return _status = "<span class=\"linkGridForm\" style=\"color: Green;font-weight:bold\">Đã duyệt</span>";
            return _status;
        }
        public bool CheckSendPDF(string _trang, string _sobao)
        {
            string str = string.Empty;
            str = UltilFunc.GetColumnValuesOne("T_Publish_Pdf", "ID", "Page_Number=" + _trang + " and Publish_Number_ID=" + _sobao + " and Status=2").ToString();
            if (str != "0")
                return true;
            else
                return false;
        }
        protected void DataGrid_FilePDF_EditCommand(object source, DataListCommandEventArgs e)
        {
            int _ID = Convert.ToInt32(DataGrid_FilePDF.DataKeys[e.Item.ItemIndex].ToString());
            TextBox txt_chuthich = e.Item.FindControl("txt_chuthich") as TextBox;
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {

                Label lblUrl = (Label)e.Item.FindControl("lbFileAttach");
                string filePath = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lblUrl.Text);
                System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                if (File.Exists(filePath))
                {
                    fi.Delete();
                }

                Daltinbai.Sp_DeleteT_Publish_Pdf(_ID);
                LoadData_FilePDF();
            }
            if (e.CommandArgument.ToString().ToLower() == "reply")
            {
                string _sqlupdate = "update T_Publish_Pdf set Comments=N'" + txt_chuthich.Text.Trim() + "' where ID=" + _ID;
                ulti.ExecSql(_sqlupdate);
            }

        }
        protected void btnSendPublish_Click(object sender, EventArgs e)
        {
            if (DataGrid_FilePDF.Items.Count > 0)
            {
                foreach (DataGridItem m_Item in DataGrid_FilePDF.Items)
                {
                    TextBox txt_chuthich = (TextBox)m_Item.FindControl("txt_chuthich");
                    string _sqldelete = "delete from T_Publish_Pdf  where Status=4 and Page_Number=" + cboPage.SelectedValue + " and Publish_Number_ID=" + cboSoBao.SelectedValue;
                    ulti.ExecSql(_sqldelete);
                    string _sqlupdate = "update T_Publish_Pdf set Status=2,Comments=N'" + txt_chuthich.Text.Trim() + "' where Page_Number=" + cboPage.SelectedValue + " and Publish_Number_ID=" + cboSoBao.SelectedValue;
                    ulti.ExecSql(_sqlupdate);
                }
                LoadData_FilePDF();
            }
            else
            {
                FuncAlert.AlertJS(this, "Bạn chưa chon file PDF để gửi!");
                return;
            }
        }
        protected void btnXoaFile_Click(object sender, EventArgs e)
        {
            ArrayList ar = new ArrayList();
            if (!_isRefresh)
            {
                if (DataGrid_FilePDF.Items.Count > 0)
                {
                    foreach (DataGridItem m_Item in DataGrid_FilePDF.Items)
                    {
                        Label lbFileAttach = m_Item.FindControl("lbFileAttach") as Label;

                        int _ID = int.Parse(DataGrid_FilePDF.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        Daltinbai.Sp_DeleteT_Publish_Pdf(_ID);
                        string path = HttpContext.Current.Server.MapPath("~" + lbFileAttach.Text);
                        System.IO.FileInfo fi = new System.IO.FileInfo(path);
                        try
                        {
                            if (File.Exists(path))
                                fi.Delete();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    LoadData_FilePDF();
                }
                else
                {
                    FuncAlert.AlertJS(this, "Không có bản ghi nào được xóa!");
                    return;
                }
            }

        }
        #endregion

        #region Download All file
        protected void Linkdownloadfile_Click(object sender, EventArgs e)
        {
            DownloadAll(cbo_Anpham.SelectedItem.Text);
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
        private void GenerateRTFFile(string _Noidung, string _tieude, double id)
        {
            string strFileName;
            string strHTML = "";
            strHTML += "<html>";
            strHTML += "<BODY>";
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
            if (cbo_Anpham.SelectedIndex > 0)
                _pathfolder += cbo_Anpham.SelectedItem.Text;
            if (cboSoBao.SelectedIndex > 0)
                _pathfolder += "/" + cboSoBao.SelectedItem.Text.Replace("/", "-");
            if (cboPage.SelectedIndex > 0)
                _pathfolder += "/" + cboPage.SelectedItem.Text;
            string _zipFile = _ZipFileName + ".zip";
            try
            {
                DataTable _dt = new DataTable();
                string chuthichanh = string.Empty;

                foreach (DataGridItem _item in DataGrid_DanTrang.Items)
                {
                    string _sqlupdate = string.Empty;
                    double _ID = double.Parse(DataGrid_DanTrang.DataKeys[_item.ItemIndex].ToString());
                    _sqlupdate = "update t_tinbai set updatecontents=0 where ma_tinbai=" + _ID;
                    ulti.ExecSql(_sqlupdate);
                    
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
