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
using Excel1 = Microsoft.Office.Interop.Excel;
namespace ToasoanTTXVN.Nhuanbut
{
    public partial class Tracuunhuanbut : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        ArrayList ar;
        Excel1.Application oExcel;
        Excel1.Workbook oWorkBook;
        Excel1.Workbooks oWorkBooks;
        Excel1.Worksheet oSheet;
        UltilFunc ulti = new UltilFunc();

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

                        }
                        else
                            Page.Response.Redirect("~/login.aspx", true);
                    }
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboAnPham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", " 1=1 ", (string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanpham"));
        }
        string GetWhere()
        {
            string _where = " Trangthai_Xoa=0 and Doituong_DangXuly=N'" + Global.MaXuatBan + "'";

            if (txt_tieude.Text.Length > 0 && txt_tieude.Text.Trim() != "")
                _where += " AND Tieude LIKE " + string.Format("N'%{0}%'", UltilFunc.SqlFormatText(txt_tieude.Text.Trim()));
            if (cboAnPham.SelectedIndex > 0)
                _where += " AND Ma_AnPham=" + cboAnPham.SelectedValue;
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " AND Ma_Sobao in (select Ma_Sobao from T_Sobao where Ngay_Xuatban>='" + txt_tungay.Text.Trim() + " 00:00:00' and Ngay_Xuatban<='" + txt_denngay.Text.Trim() + " 23:59:59')";
            if (txt_cmtnd.Text.Trim() != "" && txt_cmtnd.Text.Trim() != "")
                _where += " AND Ma_TacGia in (select Ma_Nguoidung from T_Nguoidung where CMTND='" + txt_cmtnd.Text.Trim() + "')";
            if (txt_PVCTV.Text.Trim() == "")
                HiddenFieldTacgiatin.Value = "";
            if (HiddenFieldTacgiatin.Value != "")
                _where += " AND Ma_Tinbai in (select Ma_Tinbai from T_Nhuanbut where Ma_tacgia=" + HiddenFieldTacgiatin.Value + ")";
            
            return _where;
        }
        private void LoadData()
        {
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Sp_ListTraCuuNhuanBut(GetWhere());
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();
        }
        public void ExportReportExcell(string MaTacGia)
        {
            System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
            string str_Thongbao = null;
            string strfilename = "/Bienlaithanhtoan" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            object template = HttpContext.Current.Server.MapPath("~/Template") + "/BIENLAITHANHTOAN.xlt";
            string dir_filename = HttpContext.Current.Server.MapPath("~/DataExport") + strfilename;
            object Missing = System.Reflection.Missing.Value;


            oExcel = new Excel1.Application();
            oWorkBooks = oExcel.Workbooks;

            oWorkBook = oWorkBooks.Add(template);

            oSheet = (Excel1.Worksheet)oWorkBook.ActiveSheet;
            oExcel.Visible = false;
            oExcel.UserControl = true;
            try
            {
                DataTable _dtmatacgia = new DataTable();
                DataSet _ds = new DataSet();
                int lb = int.Parse(cboAnPham.SelectedValue.ToString());
                if (txt_cmtnd.Text.Trim() != "")
                    _ds = Daltinbai.Sp_InbienlaithanhtoannhuanbutALL(lb, txt_tungay.Text.Trim(), txt_denngay.Text.Trim(), 0, Global.MaXuatBan, txt_cmtnd.Text.Trim());

                else
                    _ds = Daltinbai.Sp_InbienlaithanhtoannhuanbutALL(lb, txt_tungay.Text.Trim(), txt_denngay.Text.Trim(), int.Parse(HiddenFieldTacgiatin.Value), Global.MaXuatBan, "");

                if (_ds.Tables[0].Rows.Count > 0)
                {

                    oSheet.get_Range("A4", "A4").Value2 = "Họ tên: " + UltilFunc.GetTenTacGiaTinBai(MaTacGia).ToUpper();

                    string diachi = UltilFunc.GetDiaChiTacGia(MaTacGia, 1);
                    if (diachi != "")
                    {
                        oSheet.get_Range("A5", "A5").Value2 = "Địa chỉ: " + diachi.ToUpper();
                        oSheet.get_Range("A5", "A5").WrapText = true;
                    }

                    int _row = 8;

                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {

                        oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (i + 1).ToString();
                        if (_ds.Tables[0].Rows[i]["Publish_Date"].ToString() != "")
                            oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = Convert.ToDateTime(_ds.Tables[0].Rows[i]["Publish_Date"]).ToString("dd/MM/yyyy");
                        else
                            oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = "";
                        oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["Tieude"].ToString();
                        oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["DiemTin"].ToString();

                        _row++;

                    }
                    oSheet.get_Range("C8", "C" + _row.ToString().Trim()).WrapText = true;

                    oSheet.get_Range("A8", "A" + _row.ToString().Trim()).Cells.RowHeight = 25;
                    oSheet.get_Range("A7", "D" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;

                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = "=SUM(D8:E" + (_row - 1) + ")";
                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Font.Bold = true;
                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                    oSheet.get_Range("A" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = "TỔNG TIỀN:";
                    oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                    oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Value2 = "Số tiền bằng chữ:";
                    oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Font.Name = "Times New Roman";
                    oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Font.Size = 13;
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "D" + (_row + 1).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).Value2 = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).Font.Size = 11;
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Value2 = "NGƯỜI NHẬN TIỀN                                         NGƯỜI TRẢ TIỀN";
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Font.Bold = true;
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Font.Size = 13;


                }

            }
            catch (Exception ex)
            {
                str_Thongbao = ex.ToString();
            }
            finally
            {
                object filename = @dir_filename;
                oWorkBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing, Missing, Missing, Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing, Missing, Missing, Missing, Missing);

                if (oSheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                    oSheet = null;
                }
                if (oWorkBook != null)
                {
                    oWorkBook.Close(Missing, @dir_filename, Missing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBook);
                    oWorkBook = null;
                }

                if (oExcel != null)
                {
                    oExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                    oExcel = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();


                foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL.exe"))
                {
                    proc.Kill();
                }
                Page.Response.Redirect("~/DataExport/" + strfilename);
            }
        }
        public void ExportReportExcellCheckBox(string MaTacGia)
        {
            System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
            string str_Thongbao = null;
            string strfilename = "/Bienlaithanhtoan" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            object template = HttpContext.Current.Server.MapPath("~/Template") + "/BIENLAITHANHTOAN.xlt";
            string dir_filename = HttpContext.Current.Server.MapPath("~/DataExport") + strfilename;
            object Missing = System.Reflection.Missing.Value;


            oExcel = new Excel1.Application();
            oWorkBooks = oExcel.Workbooks;
            oWorkBook = oWorkBooks.Add(template);
            oSheet = (Excel1.Worksheet)oWorkBook.ActiveSheet;
            oExcel.Visible = false;
            oExcel.UserControl = true;
            try
            {
                int _row = 8;
                int lb = int.Parse(cboAnPham.SelectedValue.ToString());

                for (int k = 0; k < ar.Count; k++)
                {
                    double ID = double.Parse(ar[k].ToString());
                    DataSet _ds = Daltinbai.Sp_Inbienlaithanhtoannhuanbut(ID, MaTacGia);

                    if (_ds.Tables[0].Rows.Count > 0)
                    {

                        oSheet.get_Range("A4", "A4").Value2 = "Họ tên: " + UltilFunc.GetTenTacGiaTinBai(MaTacGia).ToUpper();

                        string diachi = UltilFunc.GetDiaChiTacGia(MaTacGia, 1);
                        if (diachi != "")
                        {
                            oSheet.get_Range("A5", "A5").Value2 = "Địa chỉ: " + diachi.ToUpper();
                            oSheet.get_Range("A5", "A5").WrapText = true;
                        }


                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {

                            oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (i + 1).ToString();
                            if (_ds.Tables[0].Rows[i]["Publish_Date"].ToString() != "")
                                oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = Convert.ToDateTime(_ds.Tables[0].Rows[i]["Publish_Date"]).ToString("dd/MM/yyyy");
                            else
                                oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = "";
                            oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["Tieude"].ToString();
                            oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["DiemTin"].ToString();

                            _row++;

                        }

                    }

                }
                oSheet.get_Range("C8", "C" + _row.ToString().Trim()).WrapText = true;

                oSheet.get_Range("A8", "A" + _row.ToString().Trim()).Cells.RowHeight = 25;
                oSheet.get_Range("A7", "D" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;

                oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = "=SUM(D8:E" + (_row - 1) + ")";
                oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Font.Bold = true;
                oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                oSheet.get_Range("A" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).MergeCells = true;
                oSheet.get_Range("A" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = "TỔNG TIỀN:";
                oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Value2 = "Số tiền bằng chữ:";
                oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Font.Name = "Times New Roman";
                oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "C" + (_row + 1).ToString().Trim()).Font.Size = 13;
                oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "D" + (_row + 1).ToString().Trim()).MergeCells = true;
                oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).MergeCells = true;
                oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).Value2 = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "D" + (_row + 2).ToString().Trim()).Font.Size = 11;
                oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).MergeCells = true;
                oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Value2 = "NGƯỜI NHẬN TIỀN                                         NGƯỜI TRẢ TIỀN";
                oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Font.Bold = true;
                oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "D" + (_row + 3).ToString().Trim()).Font.Size = 13;
            }


            catch (Exception ex)
            {
                str_Thongbao = ex.ToString();
            }
            finally
            {
                object filename = @dir_filename;
                oWorkBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing, Missing, Missing, Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing, Missing, Missing, Missing, Missing);

                if (oSheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                    oSheet = null;
                }
                if (oWorkBook != null)
                {
                    oWorkBook.Close(Missing, @dir_filename, Missing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBook);
                    oWorkBook = null;
                }

                if (oExcel != null)
                {
                    oExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                    oExcel = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();


                foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL.exe"))
                {
                    proc.Kill();
                }
                Page.Response.Redirect("~/DataExport/" + strfilename);
            }

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboAnPham.SelectedIndex > 0)
            {
                LoadData();
            }
            else
            {
                FuncAlert.AlertJS(this, "Bạn chưa chọn loại báo!");
                return;
            }
        }
        protected void btn_inbienlai_click(object sender, EventArgs e)
        {
            if (cboAnPham.SelectedIndex == 0)
            {
                FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanchuachonanpham"));
                return;
            }
            if (HiddenFieldTacgiatin.Value != "" || txt_cmtnd.Text.Trim() != "")
            {
                ar = new ArrayList();
                foreach (DataGridItem m_Item in DataGrid_tinbai.Items)
                {
                    CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                    Label lblMaTacGia = (Label)m_Item.FindControl("lblMaTacGia");
                    if (chk_Select != null && chk_Select.Checked)
                    {
                        ar.Add(double.Parse(DataGrid_tinbai.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                    }
                    if (ar.Count > 0)
                        ExportReportExcellCheckBox(lblMaTacGia.Text);
                    else
                        ExportReportExcell(lblMaTacGia.Text);
                }
                LoadData();

            }
            else
            {
                FuncAlert.AlertJS(this, CommonLib.ReadXML("lblBanphaichontacgiahoacnhapsocmtnd"));
                return;
            }
        }
        public void pages_IndexChanged_Trang(object sender, EventArgs e)
        {
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }


    }
}
