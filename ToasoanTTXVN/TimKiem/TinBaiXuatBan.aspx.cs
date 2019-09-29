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
using Ionic.Zip;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
namespace ToasoanTTXVN.TimKiem
{
    public partial class TinBaiXuatBan : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();

        List<string> listPathFile = new List<string>();
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
                        if (_user != null)
                        {
                            LoadCombox();
                            cbo_chuyenmuc.Items.Clear();
                            cboSoBao.Items.Clear();
                            if (cboAnPham.SelectedIndex > 0)
                            {
                                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
                                UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") and Ma_AnPham= " + cboAnPham.SelectedValue, CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
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
                        else
                            Page.Response.Redirect("~/login.aspx", true);
                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", CommonLib.ReadXML("lblTatca"));
            UltilFunc.BindCombox_CategoryDequy(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", " WHERE Ma_ChuyenMuc in (select Ma_ChuyenMuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _user.UserID.ToString() + ") ", CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha");
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
                    cboPage.Items.Add(new ListItem((string)HttpContext.GetGlobalResourceObject("cms.language", "lblTrang") + j.ToString(), j.ToString()));
                }
            }

        }
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

        private string GetOrderString()
        {
            if ((ViewState["OrderString"] != null) && (ViewState["OrderString"].ToString() != ""))
            {
                return ViewState["OrderString"].ToString();
            }
            else
            {
                return " Ngaytao DESC";
            }
        }
        string BuildSQL(string sOrder)
        {
            string sWhere = " 1=1 and Doituong_DangXuly='" + Global.MaXuatBan + "'";


            if (cboAnPham.SelectedIndex > 0)
                sWhere += " AND Ma_AnPham=" + cboAnPham.SelectedValue;

            if (cboSoBao.SelectedIndex > 0)
                sWhere += " AND Ma_Sobao=" + cboSoBao.SelectedValue;
            if (cboPage.SelectedIndex > 0)
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Vitri_Tinbai where  Trang =" + cboPage.SelectedValue + ")";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                sWhere += " AND Ma_Sobao in(select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";

            if (cbo_chuyenmuc.SelectedIndex > 0)
                sWhere += " AND Ma_Chuyenmuc=" + cbo_chuyenmuc.SelectedValue.ToString();

            if (txt_PVCTV.Text.Trim() == "")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                sWhere += " AND Ma_Tinbai in (select Ma_Tinbai from T_Nhuanbut where Ma_tacgia=" + HiddenFieldTacgiatin.Value + ")";


            return sWhere + sOrder;
        }
        private void LoadData()
        {
            string FulltextSearch = UltilFunc.ReplaceAll(txt_tieude.Text.Trim(), "'", "’");
            string sOrder = GetOrderString() == "" ? "" : " ORDER BY " + GetOrderString();
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.BindGridT_NewsFullTeztSearch(pages.PageIndex, pages.PageSize, BuildSQL(sOrder), UltilFunc.SplitString(FulltextSearch));
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.BindGridT_NewsFullTeztSearch(pages.PageIndex - 1, pages.PageSize, BuildSQL(sOrder), UltilFunc.SplitString(FulltextSearch));
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;

        }
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
        protected void Linkdownloadfile_Click(object sender, EventArgs e)
        {
            string _pathfolder = string.Empty;
            if (cboAnPham.SelectedIndex > 0)
                _pathfolder += cboAnPham.SelectedItem.Text;
            if (cboSoBao.SelectedIndex > 0)
                _pathfolder += "/" + cboSoBao.SelectedItem.Text.Replace("/", "-");
            if (cboPage.SelectedIndex > 0)
                _pathfolder += "/" + cboPage.SelectedItem.Text;
            string _zipFile = "VietNamNews.zip";

            foreach (DataGridItem m_Item in DataGrid_tinbai.Items)
            {
                Label lbFiledoc = m_Item.FindControl("lbFiledoc") as Label;
                /******************* ZIP FILE ******************************/
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    string sourceFile = lbFiledoc.Text;
                    string _linkImage = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + sourceFile);

                    if (File.Exists(_linkImage))
                    {
                        listPathFile.Add(_linkImage);

                    }
                }

            }

            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=" + _zipFile);
            using (ZipFile _zip = new ZipFile())
            {
                foreach (string _itemNews in listPathFile) // Loop with for.
                {
                    _zip.AddFile(_itemNews, _pathfolder);
                }

                _zip.Save(Response.OutputStream);
            }
            Response.End();
        }
        protected void cboAnPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            cboSoBao.Items.Clear();

            if (cboAnPham.SelectedIndex > 0)
            {
                UltilFunc.BindComboxSoBao(cboSoBao, int.Parse(cboAnPham.SelectedValue.ToString()), 1);
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
        protected void cboPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        protected void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "download")
            {
                Label lbFileAttach = (Label)e.Item.FindControl("lbFiledoc");
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

        protected void SendEmail_Click(object sender, EventArgs e)
        {
            string fileattach = string.Empty;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("to gmail address");
            mail.From = new MailAddress("from gmail address", "Email head", System.Text.Encoding.UTF8);
            mail.Subject = "This mail is send from asp.net application";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "This is Email Body Text";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            if (fileattach.Length > 0)
            {
                Attachment att = new Attachment(Server.MapPath(VirtualPathUtility.ToAbsolute("~/User_Resume/" + fileattach)));
                mail.Attachments.Add(att);
            }
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("from gmail address", "your gmail account password");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {

                client.Send(mail);
                FuncAlert.AlertJS(this, "Successfully Send...");
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                FuncAlert.AlertJS(this, "Sending Failed...");
            }
        }
    }
}
