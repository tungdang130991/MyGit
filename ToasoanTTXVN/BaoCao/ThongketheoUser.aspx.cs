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
using System.Globalization;
using System.Reflection;

namespace ToasoanTTXVN.BaoCao
{
    public partial class ThongketheoUser : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
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

        #region Method
        private void LoadCombox()
        {
            NguoidungDAL _Obj = new NguoidungDAL();
            UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format("  HoatDong = 1 and HienThi_BDT = 1 "), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            drop_User.Items.Clear();
            DataTable dt = _Obj.GetAllUser_By_CatID(0);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    drop_User.Items.Add(new ListItem(CommonLib.ReadXML("lblTatca"), "0", true));
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.drop_User.Items.Add(new ListItem(dt.Rows[i]["Fullname"].ToString(), dt.Rows[i]["Ma_Nguoidung"].ToString()));
                    }
                }
            }
        }

        public static DataSet CallSP(string StoreName, string fd, string td, int cate)
        {
            DataSet _ds = null;
            SqlService _sqlservice = new SqlService();
            if (fd != "")
                _sqlservice.AddParameter("@fromdate", SqlDbType.NVarChar, fd + " 00:00:01", 25);
            if (td != "")
                _sqlservice.AddParameter("@todate", SqlDbType.NVarChar, td + " 23:59:59", 25);
            _sqlservice.AddParameter("@Cate", SqlDbType.Int, cate, 25);
            _ds = _sqlservice.ExecuteSPDataSet(StoreName, StoreName);
            _sqlservice.CloseConnect(); _sqlservice.Disconnect();
            return _ds;
        }
        public bool checkDate()
        {
            bool success = true;
            CultureInfo cultureInfo = new CultureInfo("fr-FR");
            if (!string.IsNullOrEmpty(txt_FromDate.Text.Trim()))
            {
                try
                {
                    DateTime.Parse(txt_FromDate.Text.Trim(), cultureInfo);
                }
                catch
                {
                    success = false;

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày phải nhập theo định dạng dd/MM/yyyy');", true);
                }
            }
            if (!string.IsNullOrEmpty(txt_ToDate.Text.Trim()))
            {
                try
                {
                    DateTime.Parse(txt_ToDate.Text.Trim(), cultureInfo);

                }
                catch
                {
                    success = false;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày phải nhập theo định dạng dd/MM/yyyy');", true);
                }
            }
            return success;
        }
        #endregion


        #region Click new
        //protected void linkExport_OnClick(object sender, EventArgs e)
        //{
        //    if (checkDate())
        //    {
        //        HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
        //        System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
        //        string str_Thongbao = null;
        //        string strfilename = "/Baivietuser_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"; //+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlt";
        //        object template = Server.MapPath("../Template") + "/Baivietuser.xlt";
        //        string dir_filename = Server.MapPath("../Data") + strfilename;
        //        object Missing = System.Reflection.Missing.Value;
        //        Excel1.Application oExcel;
        //        Excel1.Workbook oWorkBook;
        //        Excel1.Workbooks oWorkBooks;
        //        Excel1.Worksheet oSheet;
        //        oExcel = new Excel1.Application();
        //        CultureInfo cultureInfo = new CultureInfo("en-US");
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //        oWorkBooks = oExcel.Workbooks;
        //        oWorkBook = oWorkBooks.Add(template);
        //        System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

        //        oSheet = (Excel1.Worksheet)oWorkBook.ActiveSheet;
        //        oExcel.Visible = false;
        //        oExcel.UserControl = true;
        //        try
        //        {
        //            string _date = "";
        //            if (drop_User.SelectedValue.Trim() != "0")
        //            {
        //                oSheet.get_Range("A2", "A2").Value2 = "Tác giả :" + drop_User.SelectedItem.Text;
        //            }
        //            else
        //            {
        //                oSheet.get_Range("A2", "A2").Value2 = "";
        //            }
        //            if ((txt_FromDate.Text != ""))
        //            {
        //                _date = _date + "Từ ngày " + txt_FromDate.Text.Trim();
        //            }
        //            if (!string.IsNullOrEmpty(txt_ToDate.Text))
        //                _date = _date + " Đến ngày " + txt_ToDate.Text.Trim();
        //            oSheet.get_Range("A4", "A4").Value2 = _date;
        //            if (cbo_chuyenmuc.SelectedValue.Trim() != "0")
        //            {
        //                oSheet.get_Range("A3", "A3").Value2 = "Chuyên mục :" + cbo_chuyenmuc.SelectedItem.Text;
        //            }
        //            else
        //            {
        //                oSheet.get_Range("A3", "A3").Value2 = "";
        //            }


        //            DataSet _ds;
        //            DataSet _ds1;
        //            if (drop_User.SelectedValue.Trim() != "0")
        //            {
        //                if (cbo_chuyenmuc.SelectedIndex > 0)
        //                    _ds = _ultil.GetStoreDataSet("[CMS_SelectCountNews_ByID]", new string[] { "@fromdate", "@todate", "@UserID", "@CatID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Convert.ToInt32(drop_User.SelectedValue.ToString()), Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString()) });
        //                else
        //                    _ds = _ultil.GetStoreDataSet("[CMS_SelectCountNews_ByID]", new string[] { "@fromdate", "@todate", "@UserID", "@CatID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Convert.ToInt32(drop_User.SelectedValue.ToString()), 0 });
        //            }
        //            else
        //            {
        //                if (cbo_chuyenmuc.SelectedIndex > 0)
        //                    _ds = _ultil.GetStoreDataSet("[CMS_SelectCountNews_ByID]", new string[] { "@fromdate", "@todate", "@UserID", "@CatID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), 0, Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString()) });
        //                else
        //                    _ds = _ultil.GetStoreDataSet("[CMS_SelectCountNews_ByID]", new string[] { "@fromdate", "@todate", "@UserID", "@CatID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), 0, 0 });
        //            }


        //            int _row = 6;
        //            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        //            {
        //                if (cbo_chuyenmuc.SelectedValue.Trim() != "0")
        //                {
        //                    _ds1 = _ultil.GetStoreDataSet("[CMS_SelectAll_News_ByUserID]", new string[] { "@fromdate", "@todate", "@userID", "@Cate" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Convert.ToInt32(_ds.Tables[0].Rows[i]["UserID"].ToString()), int.Parse(cbo_chuyenmuc.SelectedValue.Trim()) });
        //                }
        //                else
        //                    _ds1 = _ultil.GetStoreDataSet("[CMS_SelectAll_News_ByUserID]", new string[] { "@fromdate", "@todate", "@userID", "@Cate" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), Convert.ToInt32(_ds.Tables[0].Rows[i]["UserID"].ToString()), 0 });
        //                oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["UserFullName"].ToString();
        //                oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Value2 = _ds.Tables[0].Rows[i]["sl"].ToString();
        //                _row += 1;
        //                for (int j = 0; j < _ds1.Tables[0].Rows.Count; j++)
        //                {
        //                    oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (j + 1).ToString();
        //                    oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["News_Tittle"].ToString();
        //                    oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["Category_Name"].ToString();
        //                    try
        //                    {
        //                        oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = DateTime.Parse(_ds1.Tables[0].Rows[j]["News_DatePublished"].ToString()).ToString("dd/MM/yyyy HH:mm");
        //                    }
        //                    catch { ;}
        //                    _row++;
        //                }
        //                _row++;
        //            }
        //            _row--;
        //            oSheet.get_Range("A5", "F" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;
        //        }
        //        catch (Exception ex)
        //        {
        //            str_Thongbao = ex.ToString();
        //        }
        //        finally
        //        {
        //            object filename = @dir_filename;
        //            oWorkBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing, Missing, Missing, Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing, Missing, Missing, Missing, Missing);

        //            if (oSheet != null)
        //            {
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
        //                oSheet = null;
        //            }
        //            if (oWorkBook != null)
        //            {
        //                oWorkBook.Close(Missing, @dir_filename, Missing);
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBook);
        //                oWorkBook = null;
        //            }

        //            if (oExcel != null)
        //            {
        //                oExcel.Quit();
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
        //                oExcel = null;
        //            }
        //            GC.Collect();
        //            GC.WaitForPendingFinalizers();

        //            foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL.exe"))
        //            {
        //                proc.Kill();
        //            }
        //            Response.Redirect("~/Data/" + strfilename);
        //        }
        //    }
        //}
        #endregion

        #region Print

        protected void linkExport_OnClick(object sender, EventArgs e)
        {
            if (checkDate())
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                DataSet _ds;
                int intChuyenMuc = 0;
                int intUser = 0;
                if (cbo_chuyenmuc.SelectedIndex > 0) intChuyenMuc = Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString());
                if (drop_User.SelectedIndex > 0) intUser = Convert.ToInt32(drop_User.SelectedValue.ToString());
                _ds = _ultil.GetStoreDataSet("[CMS_SelectAll_News_ByUserID]", new string[] { "@Fromdate", "@Todate", "@cate", "@UserID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), intChuyenMuc, intUser });
                if (_ds.Tables[0].Rows.Count > 0 && _ds != null)
                {
                    gvList.DataSource = _ds;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
            }
        }
        protected void Print_OnClick(object sender, EventArgs e)
        {
            if (checkDate())
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                DataSet _ds;
                int intChuyenMuc = 0;
                int intUser = 0;
                if (cbo_chuyenmuc.SelectedIndex > 0) intChuyenMuc = Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString());
                if (drop_User.SelectedIndex > 0) intUser = Convert.ToInt32(drop_User.SelectedValue.ToString());
                _ds = _ultil.GetStoreDataSet("[CMS_SelectAll_News_ByUserID]", new string[] { "@Fromdate", "@Todate", "@cate", "@UserID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), intChuyenMuc, intUser });
                if (_ds.Tables[0].Rows.Count > 0 && _ds != null)
                {
                    gvList.DataSource = _ds.Tables[0];
                    gvList.DataBind();
                    GridViewToExcel.Export("Ket_Qua_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".xls", gvList, "THỐNG KÊ BÀI VIẾT THEO TÁC GIẢ", "Từ ngày " + txt_FromDate.Text.Trim() + " đến ngày " + txt_ToDate.Text.Trim(), "");
                    this.gvList.AllowPaging = true;
                    this.gvList.DataBind();
                    _ds.Dispose();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Không có dữ liệu!');", true);
                    return;
                }
            }
        }

        #endregion
    }
}
