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
using System.Collections.Generic;

namespace ToasoanTTXVN.Quanlynhanbut
{
    public partial class ThongKeNhuanBut : System.Web.UI.Page
    {
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        DataSet _dsBind = null;
        protected int type = 0;
        Excel1.Application oExcel;
        Excel1.Workbook oWorkBook;
        Excel1.Workbooks oWorkBooks;
        Excel1.Worksheet oDESCSheet;
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
                        type = int.Parse(cbo_types.SelectedValue);
                    }
                }
            }
        }

        public bool checkDate()
        {
            bool success = false;
            CultureInfo cultureInfo = new CultureInfo("fr-FR");
            if (Drop_Ngonngu.SelectedIndex == 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Phải chọn ngôn ngữ trước !');", true);
                return success;
            }
            if ((!string.IsNullOrEmpty(txt_FromDate.Text.Trim())) && (!string.IsNullOrEmpty(txt_ToDate.Text.Trim())))
            {
                try
                {
                    DateTime.Parse(txt_FromDate.Text.Trim(), cultureInfo);
                    DateTime.Parse(txt_ToDate.Text.Trim(), cultureInfo);
                    success = true;
                }
                catch
                {
                    success = false;
                }

            }
            else
                success = false;

            return success;
        }

        private void LoadCombox()
        {
            Drop_Ngonngu.Items.Clear();
            DropCM.Items.Clear();
            UltilFunc.BindCombox(Drop_Ngonngu, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" hoatdong=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");
            if (Drop_Ngonngu.Items.Count >= 3)
                Drop_Ngonngu.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                Drop_Ngonngu.SelectedIndex = UltilFunc.GetIndexControl(this.Drop_Ngonngu, HPCComponents.Global.DefaultCombobox);
            if (Drop_Ngonngu.SelectedIndex != 0)
                UltilFunc.BindCombox(DropCM, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.Drop_Ngonngu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(DropCM, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 AND Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }

        protected void Drop_Ngonngu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropCM.Items.Clear();
            if (Drop_Ngonngu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(DropCM, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.Drop_Ngonngu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            }
            else
            {
                this.DropCM.DataSource = null;
                this.DropCM.DataBind();
            }
        }



        protected void linkExport_OnClick(object sender, EventArgs e)
        {
            ExportData();
        }
        #region LAY THONG TIN LEN LUOI

        public void LoadData()
        {
            if (checkDate() == true)
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                string loaitinbaiID = "", loaianhID = "", loaivideoID = "", loaianhTsID = "";
                loaitinbaiID = ConfigurationManager.AppSettings["NewsType"].ToString();
                loaianhID = ConfigurationManager.AppSettings["ImageType"].ToString();
                loaivideoID = ConfigurationManager.AppSettings["VideoType"].ToString();
                loaianhTsID = ConfigurationManager.AppSettings["AnhTSType"].ToString();
                int LTBID = int.Parse(loaitinbaiID);
                int LAID = int.Parse(loaianhID);
                int LVID = int.Parse(loaivideoID);
                int LATS = int.Parse(loaianhTsID);
                int lang = 0;
                try { lang = int.Parse(Drop_Ngonngu.SelectedValue); }
                catch { ;}

                //for (int tempt = 0; tempt < 3; tempt++)
                //{
                int loai = 1;
                type = int.Parse(cbo_types.SelectedValue);
                if (type == 1)
                {
                    loai = LTBID;
                }
                else if (type == 2)
                {
                    loai = LAID;
                }
                else if (type == 3)
                {
                    loai = LVID;
                }
                else if (type == 4)
                {
                    loai = LATS;
                }
                //if (tempt == 0)
                //{
                //    if (check_tinbai.Checked)
                //    {
                //        loai = LTBID;
                //    }
                //}
                //else if (tempt == 1)
                //{
                //    if (check_Video.Checked)
                //    {
                //        loai = LVID;
                //    }
                //}
                //else if (tempt == 2)
                //{
                //    if (check_TinAnh.Checked)
                //    {
                //        loai = LAID;
                //    }
                //}

                if (loai > 0)
                {
                    int catid = 0, langid = 0;
                    string tacgiaid = "";
                    if (DropCM.SelectedIndex > 0)
                    {
                        try { catid = int.Parse(DropCM.SelectedValue); }
                        catch { ;}
                    }

                    if (!String.IsNullOrEmpty(this.txt_Tacgia.Text.Trim()))
                    {
                        try { tacgiaid = this.txt_Tacgia.Text.Trim(); }
                        catch { ;}
                    }
                    if (Drop_Ngonngu.SelectedIndex > 0)
                    {
                        try { langid = int.Parse(Drop_Ngonngu.SelectedValue); }
                        catch { ;}
                    }

                    _dsBind = _ultil.GetStoreDataSet("[CMS_List_Nhuanbut]",
                        new string[] { "@fromdate", "@todate", "@Lang", "@Cate", "@UsercreateID", "@type" },
                        new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), langid, catid, tacgiaid, loai });


                    dgr_tintuc.DataSource = _dsBind.Tables[0];
                    dgr_tintuc.DataBind();

                    int _tongtien = UltilFunc.ReturnTotalNhuanbut(_dsBind.Tables[0]);
                    double _Allmoney = 0;
                    try
                    {
                        _Allmoney = double.Parse(_tongtien.ToString());
                    }
                    catch
                    { _Allmoney = 0; }
                    lbTongTien.Text = "Tổng tiền :" + string.Format("{0:#,#}", _Allmoney).Replace(".", ",");
                    foreach (DataGridItem item in dgr_tintuc.Items)
                    {

                        Label txt_tien = (Label)item.FindControl("lbTien");
                        double _money = 0;
                        try
                        {
                            _money = double.Parse(txt_tien.Text);
                        }
                        catch
                        { _money = 0; }
                        txt_tien.Text = string.Format("{0:#,#}", _money).Replace(".", ",");

                    }

                }
                //}
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Hãy nhập khoảng thời gian tìm kiếm!');", true);
            }


        }
        public void dgr_tintuc_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            Label lbSTT = (Label)e.Item.FindControl("lbSTT");
            if (lbSTT != null)
            {
                lbSTT.Text = (e.Item.ItemIndex + 1).ToString();
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void cmd_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected string SetLinkPopup(object _id)
        {
            string _strLink = string.Empty;
            if (type == 3)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/Multimedia/ViewVideo.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            else if (type == 1)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/View/ViewDetails.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            else if (type == 2)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/PhongSuAnh/T_Album_Categories_View.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&catps=" + _id.ToString() + "')";
            else if (type == 4)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/Quanlynhanbut/ViewImage.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            return _strLink;
        }
        private void ExportData()
        {
            if (checkDate() == true)
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                System.Globalization.CultureInfo vi = new System.Globalization.CultureInfo("vi-VN");
                string str_Thongbao = null;
                string strfilename = "/Nhuan_but_dien_tu_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                object template = Server.MapPath("../Template") + "/ThongKeNhuanbut.xlt";
                string dir_filename = Server.MapPath("../Data") + strfilename;
                object Missing = System.Reflection.Missing.Value;
                

                oExcel = new Excel1.Application();
                CultureInfo cultureInfo = new CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                oWorkBooks = oExcel.Workbooks;
                oWorkBook = oWorkBooks.Add(template);
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

                oDESCSheet = (Excel1.Worksheet)oWorkBook.ActiveSheet;
                oExcel.Visible = false;
                oExcel.UserControl = true;

                string loaitinbaiID = "", loaianhID = "", loaivideoID = "", loaianhTsID = "";
                loaitinbaiID = ConfigurationManager.AppSettings["NewsType"].ToString();
                loaianhID = ConfigurationManager.AppSettings["ImageType"].ToString();
                loaivideoID = ConfigurationManager.AppSettings["VideoType"].ToString();
                loaianhTsID = ConfigurationManager.AppSettings["AnhTSType"].ToString();
                int LTBID = int.Parse(loaitinbaiID);
                int LAID = int.Parse(loaianhID);
                int LVID = int.Parse(loaivideoID);
                int LATS = int.Parse(loaianhTsID);
                int IsReporter = int.Parse(rblIsReporter.SelectedValue);
                int sheetID = 1;
                int Total = 0;
                try
                {
                    string _date = "";
                    if ((txt_FromDate.Text != ""))
                    {
                        _date = _date + "TỪ NGÀY " + txt_FromDate.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txt_ToDate.Text))
                        _date = _date + " - ĐẾN NGÀY " + txt_ToDate.Text.Trim();

                    int lang = 0;
                    try { lang = int.Parse(Drop_Ngonngu.SelectedValue); }
                    catch { ;}

                    //for (int tempt = 0; tempt < 3; tempt++)
                    //{
                    int loai = 1;
                    int type = int.Parse(cbo_types.SelectedValue);
                    if (type == 1)
                    {
                        loai = LTBID;
                    }
                    else if (type == 2)
                    {
                        loai = LAID;
                    }
                    else if (type == 3)
                    {
                        loai = LVID;
                    }
                    else if (type == 4)
                    {
                        loai = LATS;
                    }
                    //if (tempt == 0)
                    //{
                    //    if (check_tinbai.Checked)
                    //    {
                    //        loai = LTBID;
                    //    }
                    //}
                    //else if (tempt == 1)
                    //{
                    //    if (check_Video.Checked)
                    //    {
                    //        loai = LVID;
                    //    }
                    //}
                    //else if (tempt == 2)
                    //{
                    //    if (check_TinAnh.Checked)
                    //    {
                    //        loai = LAID;
                    //    }
                    //}

                    if (loai > 0)
                    {
                        DataSet _ds1;
                        Excel1.Worksheet oSheet;
                        oDESCSheet.Copy(oDESCSheet, Missing);
                        oSheet = (Excel1.Worksheet)oWorkBook.Sheets[sheetID];

                        int catid = 0, langid = 0;
                        string tacgiaid = "";
                        if (DropCM.SelectedIndex > 0)
                        {
                            try { catid = int.Parse(DropCM.SelectedValue); }
                            catch { ;}
                        }

                        if (!String.IsNullOrEmpty(this.txt_Tacgia.Text.Trim()))
                        {
                            try { tacgiaid = this.txt_Tacgia.Text.Trim(); }
                            catch { ;}
                        }
                        if (Drop_Ngonngu.SelectedIndex > 0)
                        {
                            try { langid = int.Parse(Drop_Ngonngu.SelectedValue); }
                            catch { ;}
                        }
                        oSheet.get_Range("A5", "F5").Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                        oSheet.get_Range("A5", "F5").Value2 = "Nhuận bút phóng viên Báo Điện tử ";

                        oSheet.get_Range("A6", "F6").Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                        oSheet.get_Range("A6", "F6").Value2 = _date;

                        int _row = 7;



                        _ds1 = _ultil.GetStoreDataSet("[CMS_List_Nhuanbut]",
                            new string[] { "@fromdate", "@todate", "@Lang", "@Cate", "@UsercreateID", "@type" },
                            new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), langid, catid, tacgiaid, loai });

                        for (int j = 0; j < _ds1.Tables[0].Rows.Count; j++)
                        {
                            _row++;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = (j + 1).ToString();

                            oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlLeft;
                            oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Cells.WrapText = true;
                            oSheet.get_Range("B" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["Tieude"].ToString();

                            oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlLeft;
                            oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["Nguoitao"].ToString();

                            oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                            oSheet.get_Range("D" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["Tonganh"].ToString();

                            oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlLeft;
                            DateTime dt;
                            string _datePub = "";
                            try
                            {
                                if (_ds1.Tables[0].Rows[j]["NgayXB"].ToString() != "")
                                {
                                    dt = Convert.ToDateTime(_ds1.Tables[0].Rows[j]["NgayXB"].ToString());
                                    _datePub = dt.ToString("dd/MM/yyyy HH:mm");
                                }
                            }
                            catch
                            { _datePub = ""; }
                            oSheet.get_Range("E" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = _datePub;

                            oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlRight;
                            oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.NumberFormat = "#,###";
                            oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Value2 = _ds1.Tables[0].Rows[j]["Total"].ToString();

                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.VerticalAlignment = Excel1.Constants.xlCenter;

                            try
                            {
                                Total = Total + int.Parse(_ds1.Tables[0].Rows[j]["Total"].ToString());
                            }
                            catch { ;}
                        }
                        _row++;
                        // Bin tổng 
                        oSheet.get_Range("A" + _row.ToString().Trim(), "H" + _row.ToString()).Font.Bold = true;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "H" + _row.ToString()).RowHeight = 21.5;

                        oSheet.get_Range("A" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Cells.MergeCells = true;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = "Tổng cộng ";

                        oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlRight;
                        oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.NumberFormat = "#,###";
                        oSheet.get_Range("F" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Value2 = Total.ToString();

                        oSheet.get_Range("A8", "G" + _row.ToString().Trim()).Borders.LineStyle = Excel1.XlLineStyle.xlContinuous;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.VerticalAlignment = Excel1.Constants.xlCenter;
                        /*--So tien bang chu--*/
                        _row++;
                        if (Total > 0)
                        {
                            Number2Text _cv = new Number2Text();
                            string _strPrice;
                            _strPrice = _cv.ConvertNumber(Total.ToString());

                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString()).Font.Bold = true;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString()).RowHeight = 21.5;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.VerticalAlignment = Excel1.Constants.xlCenter;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.MergeCells = true;
                            oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Value2 = "Số tiền bằng chữ: " + _strPrice;
                        }

                        /*--Chu ky--*/
                        _row = _row + 3;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString().Trim()).Cells.HorizontalAlignment = Excel1.Constants.xlCenter;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString()).RowHeight = 21.5;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "G" + _row.ToString()).Font.Bold = true;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "B" + _row.ToString().Trim()).Cells.MergeCells = true;
                        oSheet.get_Range("A" + _row.ToString().Trim(), "A" + _row.ToString().Trim()).Value2 = "TỔNG BIÊN TẬP";

                        oSheet.get_Range("C" + _row.ToString().Trim(), "D" + _row.ToString().Trim()).Cells.MergeCells = true;
                        oSheet.get_Range("C" + _row.ToString().Trim(), "C" + _row.ToString().Trim()).Value2 = "BAN ĐIỆN TỬ";

                        //oSheet.get_Range("E" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.MergeCells = true;
                        //oSheet.get_Range("E" + (_row).ToString().Trim(), "E" + _row.ToString().Trim()).Value2 = "";

                        oSheet.get_Range("A" + _row.ToString().Trim(), "F" + _row.ToString().Trim()).Cells.VerticalAlignment = Excel1.Constants.xlCenter;
                        //_row = _row + 1;
                        if (oSheet != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                            oSheet = null;
                        }
                    }
                    //}
                }
                catch (Exception ex)
                {
                    str_Thongbao = ex.ToString();
                }
                finally
                {
                    object filename = @dir_filename;
                    oWorkBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Missing, Missing, Missing, Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing, Missing, Missing, Missing, Missing);
                    if (oDESCSheet != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oDESCSheet);
                        oDESCSheet = null;
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
                    Response.Redirect("~/Data/" + strfilename);
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
