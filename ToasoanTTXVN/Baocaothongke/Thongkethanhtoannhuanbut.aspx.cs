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
using System.Security.Principal;
using System.Runtime.InteropServices;
namespace ToasoanTTXVN.Baocaothongke
{
    public partial class Thongkethanhtoannhuanbut : BasePage
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        Excel1.Application oExcel;
        Excel1.Workbook oWorkBook;
        Excel1.Workbooks oWorkBooks;
        Excel1.Worksheet oSheet;
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
                }
            }

        }
        public void LoadCombox()
        {
            UltilFunc.BindCombox(cbo_Anpham, "Ma_Anpham", "Ten_Anpham", "T_Anpham", "1=1", CommonLib.ReadXML("lblTatca"));
        }
        private void LoadData()
        {

            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.TinBaiDAL _T_newsDAL = new HPCBusinessLogic.DAL.TinBaiDAL();
            DataSet _ds;
            int Vungmien = 0;
            if (cboVungmien.SelectedValue != "")
                Vungmien = int.Parse(cboVungmien.SelectedValue);
            _ds = _T_newsDAL.Sp_ListThongKeNhuanBut(pages.PageIndex, pages.PageSize, int.Parse(cbo_Anpham.SelectedValue), int.Parse(cboThanhtoan.SelectedValue), txt_tungay.Text.Trim(), txt_denngay.Text.Trim(), int.Parse(cbo_PVCTV.SelectedValue), Vungmien, Global.MaXuatBan);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _T_newsDAL.Sp_ListThongKeNhuanBut(pages.PageIndex, pages.PageSize, int.Parse(cbo_Anpham.SelectedValue), int.Parse(cboThanhtoan.SelectedValue), txt_tungay.Text.Trim(), txt_denngay.Text.Trim(), int.Parse(cbo_PVCTV.SelectedValue), Vungmien, Global.MaXuatBan);
            DataGrid_tinbai.DataSource = _ds;
            DataGrid_tinbai.DataBind();

            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
        }
        public void ExportReportExcell()
        {
            System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
            string str_Thongbao = null;
            string strfilename = "/Thongkenhuanbut_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            object template = HttpContext.Current.Server.MapPath("~/Template") + "/NBCTV_QDND.xlt";
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

                int lb = int.Parse(cbo_Anpham.SelectedValue);
                if (cbo_Anpham.SelectedIndex == 0)
                {
                    UltilFunc.AlertJS("Bạn chưa chọn ấn phẩm");
                    return;
                }

                int Vungmien = 0;
                if (cboVungmien.SelectedValue != "")
                    Vungmien = int.Parse(cboVungmien.SelectedValue);
                DataSet _ds = Daltinbai.Sp_Thongkethanhtoannhuanbut(lb, int.Parse(cboThanhtoan.SelectedValue), txt_tungay.Text.Trim(), txt_denngay.Text.Trim(), int.Parse(cbo_PVCTV.SelectedValue), Vungmien, Global.MaXuatBan);

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    if (cbo_Anpham.SelectedValue != "")
                        if (cbo_PVCTV.SelectedIndex > 0)
                            oSheet.get_Range("A3", "A3").Value2 = cbo_Anpham.SelectedItem.Text.ToUpper() + "  " + cbo_PVCTV.SelectedItem.Text.ToUpper();
                        else
                            oSheet.get_Range("A3", "A3").Value2 = cbo_Anpham.SelectedItem.Text.ToUpper();

                    oSheet.get_Range("A4", "A4").Value2 = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;


                    int _row = 7;

                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {

                        oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (i + 1).ToString();
                        oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = UltilFunc.GetTenTacGiaTinBai(_ds.Tables[0].Rows[i]["TacgiaTin"].ToString());
                        oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).WrapText = true;
                        oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = UltilFunc.GetDiaChiTacGia(_ds.Tables[0].Rows[i]["TacgiaTin"].ToString(), 1);
                        oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).WrapText = true;
                        oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["Tieude"].ToString();
                        oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).WrapText = true;
                        oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["DiemTin"].ToString();

                        _row++;

                    }
                    oSheet.get_Range("E7", "E" + _row.ToString().Trim()).WrapText = true;
                    oSheet.get_Range("B7", "B" + _row.ToString().Trim()).WrapText = true;

                    oSheet.get_Range("A7", "A" + _row.ToString().Trim()).Cells.RowHeight = 25;
                    oSheet.get_Range("A6", "E" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;

                    oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = "=SUM(E7:E" + (_row - 1) + ")";
                    oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Font.Bold = true;
                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlRight;
                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = "TỔNG TIỀN:";
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "E" + (_row + 1).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "B" + (_row + 1).ToString().Trim()).Value2 = "Số tiền bằng chữ:";
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "B" + (_row + 1).ToString().Trim()).Font.Bold = true;
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "B" + (_row + 1).ToString().Trim()).Font.Name = "Times New Roman";
                    oSheet.get_Range("B" + (_row + 1).ToString().Trim(), "B" + (_row + 1).ToString().Trim()).Font.Size = 13;
                    oSheet.get_Range("C" + (_row + 1).ToString().Trim(), "E" + (_row + 1).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 1).ToString().Trim(), "A" + (_row + 1).ToString().Trim()).Cells.RowHeight = 30;

                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "E" + (_row + 2).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "E" + (_row + 2).ToString().Trim()).Value2 = "THỦ TRƯỞNG                          CQ TÀI CHÍNH                       TRƯỞNG PHÒNG BĐCTV                       NGƯỜI LẬP";
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "A" + (_row + 2).ToString().Trim()).Columns.AutoFit();
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "A" + (_row + 2).ToString().Trim()).HorizontalAlignment = Excel1.Constants.xlCenter;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "A" + (_row + 2).ToString().Trim()).Font.Size = 13;
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "E" + (_row + 2).ToString().Trim()).Font.Name = "Times New Roman";
                    oSheet.get_Range("A" + (_row + 2).ToString().Trim(), "A" + (_row + 2).ToString().Trim()).Cells.RowHeight = 40;
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "E" + (_row + 3).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("A" + (_row + 3).ToString().Trim(), "A" + (_row + 3).ToString().Trim()).Cells.RowHeight = 40;

                    oSheet.get_Range("C" + (_row + 4).ToString().Trim(), "E" + (_row + 4).ToString().Trim()).MergeCells = true;
                    oSheet.get_Range("C" + (_row + 4).ToString().Trim(), "E" + (_row + 4).ToString().Trim()).Value2 = " Đại tá: Lê Quý Hoàng                         Nguyễn Thị Liên";
                    oSheet.get_Range("C" + (_row + 4).ToString().Trim(), "C" + (_row + 4).ToString().Trim()).Font.Size = 13;


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
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbo_Anpham.SelectedIndex == 0)
            { FuncAlert.AlertJS(this, "Bạn chưa chọn ấn phẩm!"); return; }
            pages.PageIndex = 0;
            LoadData();
        }
        protected void btn_xuatbaocao_click(object sender, EventArgs e)
        {
            ExportReportExcell();
        }
        public void pages_IndexChanged_Trang(object sender, EventArgs e)
        {
            LoadData();
        }
        public void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();

        }

        protected void cbo_PVCTV_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            cboVungmien.Items.Clear();
            if (cbo_PVCTV.SelectedValue == "0")
            {

                ListItem item1 = new ListItem("Miền Bắc", "1");
                ListItem item2 = new ListItem("Miền Trung", "2");
                ListItem item3 = new ListItem("Miền Nam", "3");

                cboVungmien.Items.Add(item1);
                cboVungmien.Items.Add(item2);
                cboVungmien.Items.Add(item3);
            }
            else if (cbo_PVCTV.SelectedValue == "1")
            {
                ListItem item11 = new ListItem("Bưu Điện", "1");
                ListItem item12 = new ListItem("Hà nội", "2");
                cboVungmien.Items.Add(item11);
                cboVungmien.Items.Add(item12);
            }
            else if (cbo_PVCTV.SelectedValue == "-1")
            {
                ListItem item111 = new ListItem("----Chọn----", "0");
                cboVungmien.Items.Add(item111);
            }
        }
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }

            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
    }
}
