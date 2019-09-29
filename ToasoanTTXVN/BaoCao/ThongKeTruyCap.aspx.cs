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
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic.DAL;
using Excel1 = Microsoft.Office.Interop.Excel;
namespace ToasoanTTXVN.BaoCao
{
    public partial class ThongKeTruyCap : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        Excel1.Application oExcel;
        Excel1.Workbook oWorkBook;
        Excel1.Workbooks oWorkBooks;
        Excel1.Worksheet oSheet;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadCombox();
                    }
                }
            }
        }

        public void LoadData()
        {
            if ((!string.IsNullOrEmpty(txt_FromDate.Text.Trim())) && (!string.IsNullOrEmpty(txt_ToDate.Text.Trim())))
            {
                T_BaoCao _DAL = new T_BaoCao();
                DataSet _ds;
                _ds = GetDataByCallSP("CMS_List_HitCounter", txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Int32.Parse(ddlCate.SelectedValue));
                grdListHistory.DataSource = _ds;
                grdListHistory.DataBind();
            }
            else
            {
                grdListHistory.DataSource = null;
                grdListHistory.DataBind();
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Hãy nhập khoảng thời gian tìm kiếm!');", true);
            }


        }

        protected void grdListHistory_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }

        #region Report
        private void LoadCombox()
        {
            UltilFunc.BindCombox(ddlCate, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }

        public static DataSet GetDataByCallSP(string StoreName, string fd, string td, int cate)
        {
            SqlService _sqlservice = new SqlService();
            DataSet _ds = null;
            if (fd != "")
            {
                _sqlservice.AddParameter("@FROM_DATE", SqlDbType.NVarChar, fd + " 00:00:01", 25);
            }
            if (td != "")
            {
                _sqlservice.AddParameter("@TO_DATE", SqlDbType.NVarChar, td + " 23:59:59", 25);
            }

            _sqlservice.AddParameter("@cate", SqlDbType.Int, cate, 25);
            _ds = _sqlservice.ExecuteSPDataSet(StoreName, StoreName);
            _sqlservice.CloseConnect(); _sqlservice.Disconnect();
            return _ds;
        }



        #endregion

        #region click
        protected void ViewReport_OnClick(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void linkExport_OnClick(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txt_FromDate.Text.Trim())) && (!string.IsNullOrEmpty(txt_ToDate.Text.Trim())))
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
                string str_Thongbao = null;                
                string strfilename = "/TruycapWebsite_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"; //+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlt";
                object template = Server.MapPath("../Template") + "/TruycapWebsite.XLT";
                string dir_filename = Server.MapPath("../DataExport") + strfilename;
                object Missing = System.Reflection.Missing.Value;
                
                //Excel.Range oRange;
                //Start Excel and get Application object.
                oExcel = new Excel1.Application();
                oWorkBooks = oExcel.Workbooks;
                //oWorkBook = oWorkBooks.Add(Missing);
                //Excel.XlWBATemplate.xlWBATWorksheet;
                oWorkBook = oWorkBooks.Add(template);

                oSheet = (Excel1.Worksheet)oWorkBook.ActiveSheet;
                oExcel.Visible = false;
                oExcel.UserControl = true;
                try
                {
                    if ((txt_FromDate.Text != "") && (txt_ToDate.Text != ""))
                    {
                        oSheet.get_Range("A2", "A2").Value2 = "Từ ngày " + txt_FromDate.Text.Trim() + " đến ngày " + txt_ToDate.Text.Trim();
                    }
                    else
                    {
                        oSheet.get_Range("A2", "A2").Value2 = "";
                    }
                    //if (ddlLang.SelectedIndex > 0)
                    //    oSheet.get_Range("B8", "B8").Value2 = "Kênh PS: " + ddlLang.SelectedItem.Text;
                    //else
                    //    oSheet.get_Range("B8", "B8").Value2 = " ";
                    //string where = GetWhere();
                    // CategoryDAL _cateDAL = new CategoryDAL();
                    DataSet _ds;
                    _ds = GetDataByCallSP("CMS_List_HitCounter", txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Int32.Parse(ddlCate.SelectedValue));
                    //if (Convert.ToInt32(ddlCate.SelectedValue.ToString()) > 0)
                    //    _ds = _ultil.GetStoreDataSet("[CMS_List_HitCounter]", new string[] { "@FROM_DATE", "@TO_DATE", "@cate" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Convert.ToInt32(ddlCate.SelectedValue.ToString()) });
                    //else
                    //    _ds = _ultil.GetStoreDataSet("[CMS_List_HitCounter]", new string[] { "@FROM_DATE", "@TO_DATE", "@cate" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), 0 });
                    int _row = 4;
                    for (int i = 0; i <= _ds.Tables[0].Rows.Count - 1; i++)
                    {
                        //try
                        //{
                        //    Total = Total + int.Parse(_ds.Tables[0].Rows[i]["sl"].ToString());
                        //}
                        //catch { ;}
                        oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (i + 1).ToString();
                        oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["Ten_ChuyenMuc"].ToString();
                        oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["sl"].ToString();
                        //oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["SL"].ToString();
                        _row++;
                    }
                    // _row--;

                    //oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = "Tổng số: " + Total.ToString();
                    oSheet.get_Range("A3", "C" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;
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
                        //object filename = @dir_filename;
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

                    //if (oWorkBooks != null)
                    //{
                    //    oWorkBooks.Close();
                    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBooks);
                    //    oWorkBooks = null;
                    //}
                    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL.exe"))
                    {
                        proc.Kill();
                    }
                    Response.Redirect("~/DataExport/" + strfilename);
                }
            }
            else
            {

                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Hãy nhập khoảng thời gian tìm kiếm!');", true);
            }
        }
        #endregion
    }
}
