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
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace ToasoanTTXVN.Quanlynhanbut
{
    public partial class Duyetnhanbut_Tin : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected int type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    if (!IsPostBack)
                    {
                        //HPCBusinessLogic.DAL.T_ThantoanTinbai obj = new HPCBusinessLogic.DAL.T_ThantoanTinbai();
                        //obj.GetLuongtoithieu();
                        //txt_luong.Text = obj.GetLuongtoithieu().ToString();
                        loadRole();
                        LoadCombox();
                        LoadTacgia();
                        type = int.Parse(cbo_types.SelectedValue);
                    }
                }
            }
        }

        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cboNgonNgu.SelectedIndex > 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                this.cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }

        protected void cmd_Search_Click(object sender, EventArgs e)
        {
            pages.PageIndex = 0;
            Search();
        }

        protected void linkExport_OnClick(object sender, EventArgs e)
        {
            if (checkDate())
            {

            }
        }

        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            ActionHistoryDAL actionDAL = new ActionHistoryDAL();
            T_ActionHistory action = new T_ActionHistory();
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            T_NewsVersion _obj_T_NewsVecion = new T_NewsVersion();
            action.UserID = _user.UserID;
            action.FullName = _user.UserName;
            action.HostIP = IpAddress();
            action.DateModify = DateTime.Now;
            //if (e.CommandArgument.ToString().ToLower() == "edittt")
            //{
            //    Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
            //    Label lbl_TL = (Label)e.Item.FindControl("lbltheloai");
            //    Label lbl_CLID = (Label)e.Item.FindControl("lblchatluongID");
            //    Label lbl_TLID = (Label)e.Item.FindControl("lbltheloaiID");
            //    DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
            //    DropDownList Drop_TL = (DropDownList)e.Item.FindControl("ddlNews_Theloai");
            //    ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
            //    ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
            //    ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");

            //    if (lbl_CLID.Text.ToLower() != "null" && !string.IsNullOrEmpty(lbl_CLID.Text))
            //        Drop_CL.SelectedIndex = int.Parse(lbl_CLID.Text.Trim());
            //    if (lbl_TLID.Text.ToLower() != "null" && !string.IsNullOrEmpty(lbl_TLID.Text))
            //    {
            //        if (lbl_TLID.Text.ToLower() != "3" && lbl_TLID.Text.ToLower() != "4")
            //        {
            //            Drop_TL.SelectedIndex = int.Parse(lbl_TLID.Text.Trim());
            //            Drop_TL.Visible = true;
            //            lbl_TL.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        Drop_TL.SelectedIndex = 0;
            //        Drop_TL.Visible = true;
            //        lbl_TL.Visible = false;
            //    }

            //    Image_Edit.Visible = false;
            //    Image_Update.Visible = true;
            //    Image_Cancel.Visible = true;
            //    lbl_CL.Visible = false;
            //    Drop_CL.Visible = true;

            //}
            //else if (e.CommandArgument.ToString().ToLower() == "update")
            //{
            //    Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
            //    Label lbl_TL = (Label)e.Item.FindControl("lbltheloai");
            //    Label lbl_CLID = (Label)e.Item.FindControl("lblchatluongID");
            //    Label lbl_TLID = (Label)e.Item.FindControl("lbltheloaiID");
            //    DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
            //    DropDownList Drop_TL = (DropDownList)e.Item.FindControl("ddlNews_Theloai");
            //    ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
            //    ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
            //    ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");
            //    DropDownList drop_heso = (DropDownList)e.Item.FindControl("Drop_heso");
            //    int NewsID = int.Parse(dgr_tintuc.DataKeys[e.Item.ItemIndex].ToString());
            //    lbl_CL.Text = Drop_CL.SelectedItem.Text;
            //    lbl_TL.Text = Drop_TL.SelectedItem.Text;
            //    lbl_CLID.Text = Drop_CL.SelectedIndex.ToString();
            //    lbl_TLID.Text = Drop_TL.SelectedIndex.ToString();
            //    Image_Edit.Visible = true;
            //    Image_Update.Visible = false;
            //    Image_Cancel.Visible = false;
            //    lbl_CL.Visible = true;
            //    lbl_TL.Visible = true;
            //    Drop_CL.Visible = false;
            //    Drop_TL.Visible = false;
            //    if (!string.IsNullOrEmpty(lbl_TLID.Text.Trim()))
            //    {
            //        drop_heso.Items.Clear();
            //        int chatluong = 0; try { chatluong = int.Parse(lbl_CLID.Text.Trim()); }
            //        catch { ;}
            //        int theloai = 0; try { theloai = int.Parse(lbl_TLID.Text.Trim()); }
            //        catch { ;}
            //        string where = " where 1=1 ";
            //        if (chatluong != 0 || theloai != 0)
            //        {

            //            if (theloai == 3 || theloai == 4)
            //            {
            //                if (theloai == 4)
            //                {
            //                    where = where + " and LoaiTT_TLID = " + theloai.ToString() + " and LoaiTT_CLID=" + chatluong.ToString();
            //                }
            //                else
            //                {
            //                    where = " where 1=1 LoaiTT_TLID=5 ";
            //                }
            //            }
            //            else
            //            {
            //                where = where + " and LoaiTT_TLID = 1 and LoaiTT_CLID=" + chatluong.ToString() + " and LoaiTT_LoaiTinBai = " + theloai.ToString();
            //            }
            //            UltilFunc.BindCombox(drop_heso, "HesoID", "Heso", "T_HesoTT",
            //            " 1 = 1 and Heso>= (select LoaiTT_Tuheso from T_LoaihinhTT " + where + " )" +
            //            " and  Heso <= (select LoaiTT_Denheso from T_LoaihinhTT " + where + " ) order by Heso", "0");
            //        }
            //        else
            //        {
            //            UltilFunc.BindCombox(drop_heso, "HesoID", "Heso", "T_HesoTT",
            //           " 1 = 1 and Heso>= 0 and  Heso <= 1 order by Heso", "0");
            //        }

            //        //if (lbl_CLID.Text.Trim() == "1")
            //        //{
            //        //    UltilFunc.BindCombox(drop_heso, "HesoID", "Heso", "T_HesoTT", " 1 = 1 and Heso>= (select LoaiTT_Tuheso from T_LoaihinhTT where  LoaiTTID=" + lbl_TLID.Text.Trim() + " and LoaiTT_Type=1 )" +
            //        //        " and  Heso <= (select LoaiTT_Denheso from T_LoaihinhTT where   LoaiTTID=" + lbl_TLID.Text.Trim() + " and LoaiTT_Type=1 ) order by Heso", "0");
            //        //}
            //        //else
            //        //{
            //        //    drop_heso.DataSource = null;
            //        //    drop_heso.DataBind();
            //        //}
            //    }
            //    HPCBusinessLogic.DAL.T_NewsDAL obj = new HPCBusinessLogic.DAL.T_NewsDAL();
            //    obj.Update_Theloai_News(NewsID, Drop_TL.SelectedIndex, Drop_CL.SelectedIndex);

            //}
            //else if (e.CommandArgument.ToString().ToLower() == "back")
            //{
            //    Label lbl_CL = (Label)e.Item.FindControl("lblchatluong");
            //    Label lbl_TL = (Label)e.Item.FindControl("lbltheloai");
            //    DropDownList Drop_CL = (DropDownList)e.Item.FindControl("ddlnews_chatluong");
            //    DropDownList Drop_TL = (DropDownList)e.Item.FindControl("ddlNews_Theloai");
            //    ImageButton Image_Edit = (ImageButton)e.Item.FindControl("btnEdit");
            //    ImageButton Image_Update = (ImageButton)e.Item.FindControl("btnUpdate");
            //    ImageButton Image_Cancel = (ImageButton)e.Item.FindControl("btnCancel");
            //    Image_Edit.Visible = true;
            //    Image_Update.Visible = false;
            //    Image_Cancel.Visible = false;
            //    lbl_CL.Visible = true;
            //    lbl_TL.Visible = true;
            //    Drop_CL.Visible = false;
            //    Drop_TL.Visible = false;
            //}
            //else if (e.CommandArgument.ToString().ToLower() == "view")
            //{
            //    Label lbl_TL = (Label)e.Item.FindControl("lbltheloaiID");
            //    Label lbl_ID = (Label)e.Item.FindControl("lbl_ID");

            //    if (!String.IsNullOrEmpty(lbl_TL.Text))
            //    { 

            //        string js = "";
            //        if (lbl_TL.Text.Trim() == "3")
            //        {
            //            js = "alert('Trường ngày tháng phải nhập đúng theo định dang dd/MM/yyyy!');";
            //        }
            //        else if (lbl_TL.Text.Trim() == "4")
            //        {
            //            js = "alert('Trường ngày tháng phải nhập đúng theo định dang dd/MM/yyyy!');";
            //        }
            //        else
            //        {
            //            js = "Javascript:open_window_Scroll('" + Global.ApplicationPath 
            //                + "/Article/ViewAndPrint.aspx?Menu_ID=" 
            //                + Page.Request.QueryString["Menu_ID"].ToString() +
            //                "&ID=" + lbl_ID.Text.Trim()+"',50,500,100,800);";

            //        }
            //        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), " ", js, true);

            //    }
            //}
            //Search();
        }

        protected void cmd_Chamnhanbut_Click(object sender, EventArgs e)
        {
            bool checkall = true;
            foreach (DataGridItem item in dgr_tintuc.Items)
            {
                bool check = true;
                TextBox txt_tien = (TextBox)item.FindControl("txt_tien");
                Label lbl_NewsID = (Label)item.FindControl("lbl_ID");
                Label lbl_loaiID = (Label)item.FindControl("lbl_loaiID");
                Label lbl_title = (Label)item.FindControl("lbl_title");
                //LinkButton linkTittle = (LinkButton)item.FindControl("linkTittle");
                int tien = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_tien.Text))
                    {
                        check = true;
                        tien = int.Parse(txt_tien.Text.Replace(",", "")); txt_tien.ForeColor = System.Drawing.Color.Black;
                    }
                }
                catch
                {
                    check = false; txt_tien.ForeColor = System.Drawing.Color.Red; checkall = false;
                }
                if (check)
                {
                    HPCBusinessLogic.DAL.T_NewsDAL _Obj = new HPCBusinessLogic.DAL.T_NewsDAL();
                    if (tien > 0)
                        _Obj.Update_TiennhanbutTin(int.Parse(lbl_loaiID.Text), int.Parse(lbl_NewsID.Text), 0, tien, _user.UserID);
                    else
                        _Obj.Update_TiennhanbutTin(int.Parse(lbl_loaiID.Text), int.Parse(lbl_NewsID.Text), 0, 0, 0);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, lbl_title.Text,
                        Request["Menu_ID"].ToString(), "Chấm nhận bút tin ", int.Parse(lbl_NewsID.Text), type);

                }
            }
            if (checkall)
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", " alert('Chấm nhận bút thành công') ;", true);
            else
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", " alert('Hệ số nhận bút và tiền phải là kiểu số') ;", true);
        }

        public void pages_IndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        public void LoadCombox()
        {
            UltilFunc.BindCombox(cboNgonNgu, "ID", "TenNgonNgu", "T_NgonNgu", string.Format(" hoatdong=1 AND ID IN ({0}) Order by ThuTu ", UltilFunc.GetLanguagesByUser(_user.UserID)), "---Tất cả---");
            cboNgonNgu.SelectedValue = Global.DefaultCombobox;

            if (cboNgonNgu.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cboNgonNgu.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            else
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 AND Ma_AnPham in (" + UltilFunc.GetLanguagesByUser(_user.UserID) + ") AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), "---Tất cả---", "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            cboNgonNgu.UpdateAfterCallBack = true;
            cbo_chuyenmuc.UpdateAfterCallBack = true;

        }

        public bool checkDate()
        {
            bool success = true;
            CultureInfo cultureInfo = new CultureInfo("fr-FR");
            if (!string.IsNullOrEmpty(txt_tungay.Text.Trim()))
            {
                try
                {
                    DateTime.Parse(txt_tungay.Text.Trim(), cultureInfo);
                }
                catch
                {
                    success = false;

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày phải nhập theo định dạng dd/MM/yyyy');", true);
                }
            }
            if (!string.IsNullOrEmpty(txt_denngay.Text.Trim()))
            {
                try
                {
                    DateTime.Parse(txt_denngay.Text.Trim(), cultureInfo);
                }
                catch
                {
                    success = false;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Ngày phải nhập theo định dạng dd/MM/yyyy');", true);
                }
            }
            if (cboNgonNgu.SelectedIndex == 0)
            {

                success = false;

                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Phải chọn ngôn ngữ trước !');", true);

            }
            return success;
        }
        public void Search()
        {
            string Str_Search = "";
            string Str_SearchPic = "", Str_SearchVideo = "", Str_SearchAnhTS = "";
            int SearchPic = 0, SearchVideo = 0, SearchTinbai = 0, SearchAnhTS = 0;
            type = int.Parse(cbo_types.SelectedValue);
            if (type == 1)
            {
                SearchTinbai = 1;
                Str_Search = GetSQLSearch();
            }
            if (type == 2)
            {
                SearchPic = 1;
                Str_SearchPic = GetSQLSearchPic();
            }

            if (type == 3)
            {
                SearchVideo = 1;
                Str_SearchVideo = GetSQLSearchVideo();
            }
            if (type == 4)
            {
                SearchAnhTS = 1;
                Str_SearchAnhTS = GetSQLSearchAnhTS();
            }
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.T_NewsDAL _T_newsDAL = new HPCBusinessLogic.DAL.T_NewsDAL();
            DataSet _ds;
            _ds = _T_newsDAL.Search_All_News_Nhanbut(pages.PageIndex, pages.PageSize, SearchTinbai, SearchPic, SearchVideo, SearchAnhTS, Str_Search, Str_SearchPic, Str_SearchVideo, Str_SearchAnhTS);

            if (_ds != null)
            {

                try
                {
                    int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());

                    int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
                    if (TotalRecord == 0)
                        _ds = _T_newsDAL.Search_All_News_Nhanbut(pages.PageIndex - 1, pages.PageSize, SearchTinbai, SearchPic, SearchVideo, SearchAnhTS, Str_Search, Str_SearchPic, Str_SearchVideo, Str_SearchAnhTS);

                    if (TotalRecord > 0)
                    {
                        dgr_tintuc.DataSource = _ds.Tables[0];
                        dgr_tintuc.DataBind();
                        pages.TotalRecords = CurrentPage2.TotalRecords = TotalRecords;
                        CurrentPage2.TotalPages = pages.CalculateTotalPages();
                        CurrentPage2.PageIndex = pages.PageIndex;

                        dgr_tintuc.Columns[8].Visible = true;
                        cmd_Chamnhanbut.Visible = true;

                        foreach (DataGridItem item in dgr_tintuc.Items)
                        {
                            item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                            item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");

                            TextBox txt_tien = (TextBox)item.FindControl("txt_tien");
                            double _money = 0;
                            try
                            {
                                _money = double.Parse(txt_tien.Text);
                            }
                            catch
                            { _money = 0; }
                            txt_tien.Text = string.Format("{0:#,#}", _money).Replace(".", ",");

                        }
                        Panel_DS_Ketqua.Visible = true;

                    }
                    else
                    {
                        dgr_tintuc.DataSource = null;
                        dgr_tintuc.DataBind();
                        pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                        CurrentPage2.TotalPages = 1;
                        CurrentPage2.PageIndex = 1;
                        cmd_Chamnhanbut.Visible = false;
                        Panel_DS_Ketqua.Visible = false;

                    }
                }
                catch
                {
                    dgr_tintuc.DataSource = null;
                    dgr_tintuc.DataBind();
                    pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                    CurrentPage2.TotalPages = 1;
                    CurrentPage2.PageIndex = 1;
                    cmd_Chamnhanbut.Visible = false;
                    Panel_DS_Ketqua.Visible = false;

                }
            }
            else
            {
                dgr_tintuc.DataSource = null;
                dgr_tintuc.DataBind();
                pages.TotalRecords = CurrentPage2.TotalRecords = 0;
                CurrentPage2.TotalPages = 1;
                CurrentPage2.PageIndex = 1;
                cmd_Chamnhanbut.Visible = false;
                Panel_DS_Ketqua.Visible = false;

            }
        }

        public string GetSQLSearchPic()
        {

            string _SQL = " Where 1 = 1 and Cat_Album_Status_Approve =4 ";
            if (cboNgonNgu.SelectedValue != "0")
            {
                _SQL = _SQL + " AND Lang_ID = " + cboNgonNgu.SelectedValue;
            }
            else
            {
                _SQL = _SQL + " AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = "
                    + _user.UserID + ") ";
            }
            if (!string.IsNullOrEmpty(txt_tieude.Text.Trim()))
            {
                _SQL = _SQL + " and Cat_Album_Name like N'%" + txt_tieude.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_tungay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and Cat_Album_DateApprove  >= convert(datetime,'" + DateTime.Parse(txt_tungay.Text.Trim(), new CultureInfo("fr-FR")).ToString("dd/MM/yyyy") + "',103) ";
                }
                catch { return ""; }
            }
            if (!string.IsNullOrEmpty(txt_denngay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and Cat_Album_DateApprove <= DATEADD(day,1,convert(datetime,'" + DateTime.Parse(txt_denngay.Text.Trim(), new CultureInfo("fr-FR")).AddDays(1).ToString("dd/MM/yyyy") + "',103)) ";
                }
                catch { return ""; }
            }
            if (Drop_nhanbut.SelectedIndex == 1)
                _SQL = _SQL + " and  TongtienTT > 0  and NguoichamNBID > 0 ";
            else
            {
                _SQL = _SQL + "  and ( NguoichamNBID <= 0 or NguoichamNBID is null)";
            }
            return _SQL;
        }

        public string GetSQLSearchVideo()
        {

            string _SQL = " Where Status = 3 ";

            if (cboNgonNgu.SelectedValue != "0")
            {
                _SQL = _SQL + " AND Languages_ID = " + cboNgonNgu.SelectedValue;
            }
            else
            {
                _SQL = _SQL + " AND Languages_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = "
                    + _user.UserID + ") ";
            }
            if (!string.IsNullOrEmpty(txt_tieude.Text.Trim()))
            {
                _SQL = _SQL + " and [Tittle] like N'%" + txt_tieude.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_tungay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and DatePublish >= convert(datetime,'" + DateTime.Parse(txt_tungay.Text.Trim(), new CultureInfo("fr-FR")).ToString("dd/MM/yyyy") + "',103) ";
                }
                catch { return ""; }
            }
            if (!string.IsNullOrEmpty(txt_denngay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and DatePublish <= DATEADD(day,1,convert(datetime,'" + DateTime.Parse(txt_denngay.Text.Trim(), new CultureInfo("fr-FR")).AddDays(1).ToString("dd/MM/yyyy") + "',103)) ";
                }
                catch { return ""; }
            }
            if (Drop_Tacgia.SelectedValue != "0")
            {
                _SQL = _SQL + " and UserCreated = " + Drop_Tacgia.SelectedValue;
            }
            if (Drop_nhanbut.SelectedIndex == 0)
            {
                _SQL = _SQL + " and ( NguoichamNBID = 0 or NguoichamNBID is null ) ";
            }
            else
            {
                _SQL = _SQL + " and  TienNB > 0  and  NguoichamNBID > 0  ";
            }

            if (Drop_nhanbut.SelectedIndex == 1)
                _SQL = _SQL + "  and NguoichamNBID>0 ";
            else
            {
                _SQL = _SQL + "  and ( NguoichamNBID<=0 or NguoichamNBID is null)";
            }

            return _SQL;

        }

        public string GetSQLSearchAnhTS()
        {

            string _SQL = " Where 1 = 1 and Photo_Status = 3 ";
            if (cboNgonNgu.SelectedValue != "0")
            {
                _SQL = _SQL + " AND Lang_ID = " + cboNgonNgu.SelectedValue;
            }
            else
            {
                _SQL = _SQL + " AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = "
                    + _user.UserID + ") ";
            }
            if (!string.IsNullOrEmpty(txt_tieude.Text.Trim()))
            {
                _SQL = _SQL + " and Photo_Name like N'%" + txt_tieude.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_tungay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and Date_Update  >= convert(datetime,'" + DateTime.Parse(txt_tungay.Text.Trim(), new CultureInfo("fr-FR")).ToString("dd/MM/yyyy") + "',103) ";
                }
                catch { return ""; }
            }
            if (!string.IsNullOrEmpty(txt_denngay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and Date_Update <= DATEADD(day,1,convert(datetime,'" + DateTime.Parse(txt_denngay.Text.Trim(), new CultureInfo("fr-FR")).AddDays(1).ToString("dd/MM/yyyy") + "',103)) ";
                }
                catch { return ""; }
            }
            if (Drop_nhanbut.SelectedIndex == 1)
                _SQL = _SQL + " and  TienNB > 0  and NguoichamNBID > 0 ";
            else
            {
                _SQL = _SQL + "  and ( NguoichamNBID <= 0 or NguoichamNBID is null)";
            }
            return _SQL;
        }

        public string GetSQLSearch()
        {
            string _SQL = " Where News_Status=6  And News_DatePublished is NOT null ";
            if (cboNgonNgu.SelectedValue != "0")
            {
                _SQL = _SQL + " AND Lang_ID = " + cboNgonNgu.SelectedValue;
            }
            else
            {
                _SQL = _SQL + " AND Lang_ID IN (SELECT T_Nguoidung_NgonNgu.Ma_Ngonngu FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ") ";
            }

            if (cbo_chuyenmuc.SelectedValue != "0")
            {
                _SQL = _SQL + " and " + string.Format(" CAT_ID IN (SELECT * FROM [fn_Return_Category_Tree] ({0}))", this.cbo_chuyenmuc.SelectedValue);
            }
            else
            {
                _SQL = _SQL + " and CAT_ID in (select T_Nguoidung_Chuyenmuc.Ma_chuyenmuc from T_Nguoidung_Chuyenmuc where Ma_Nguoidung = " + _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name).UserID + ") ";
            }
            if (!string.IsNullOrEmpty(txt_tieude.Text.Trim()))
            {
                _SQL = _SQL + " and News_Tittle like N'%" + txt_tieude.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txt_tungay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and News_DatePublished >= convert(datetime,'" + txt_tungay.Text.Trim() + " 00:00:00',103)  ";
                }
                catch { return ""; }
            }
            if (!string.IsNullOrEmpty(txt_denngay.Text.Trim()))
            {
                try
                {
                    _SQL = _SQL + " and News_DatePublished <= convert(datetime,'" + txt_denngay.Text.Trim() + " 23:59:59',103) ";
                }
                catch { return ""; }
            }
            if (Drop_nhanbut.SelectedIndex == 0)
            {
                _SQL = _SQL + " and ( News_TienNB = 0 or News_TienNB is null ) " + " and ( News_NguoichamNBID = 0 or News_NguoichamNBID is null ) ";

            }
            else
            {
                _SQL = _SQL + " and  News_TienNB > 0  and  News_NguoichamNBID > 0  ";
            }
            return _SQL;
        }

        protected string IpAddress()
        {
            string strIp;
            strIp = Page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIp == null)
            {
                strIp = Page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIp;
        }

        public void LoadTacgia()
        {
            NguoidungDAL _Obj = new NguoidungDAL();
            DataTable dt = _Obj.GetAllUser_By_CatID(0);
            Drop_Tacgia.DataSource = null;
            Drop_Tacgia.DataBind();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Drop_Tacgia.Items.Add(new ListItem("<<-----Tác giả----->>", "0", true));
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.Drop_Tacgia.Items.Add(new ListItem(dt.Rows[i]["Fullname"].ToString(), dt.Rows[i]["Ma_Nguoidung"].ToString()));
                    }
                }
            }
        }

        public string ConvertDate(object date)
        {
            string _date = "";
            try
            {
                _date = date.ToString();
                _date = DateTime.Parse(_date, new CultureInfo("fr-FR")).ToString("dd/MM/yyyy HH:mm");
            }
            catch { ;}
            if (string.IsNullOrEmpty(_date))
                _date = "";
            return _date;
        }

        public string Visible_CountImage(object _count)
        {
            int count = 0;
            string _visible = "None";
            try
            {
                count = int.Parse(_count.ToString());
            }
            catch { ;}
            if (count > 0)
            {
                if (type == 1 || type == 2)
                    _visible = "Display";
                else
                    _visible = "None";
            }
            return _visible;
        }

        public string gettonganh(object _count)
        {
            string count = "";
            try
            {
                count = _count.ToString();
                if (count.Trim() == "0")
                    count = "";
            }
            catch { ;}
            return count;
        }

        public string GetstatusNoibat(object status, object Focus, object ishot)
        {
            string _status = "";
            int i = 0;
            try
            {
                _status = status.ToString();
                if (_status == "1")
                {
                    _status = "Bình thường"; i++;
                }
                else if (_status == "2")
                {
                    _status = "Nổi bật chuyên mục"; i++;
                }
                else if (_status == "3")
                {
                    _status = "Nổi bật trang chủ"; i++;
                }
            }
            catch { ;}
            string str_focus = "";
            string str_ishost = "";
            try
            {
                str_focus = Focus.ToString();
                if (str_focus.Trim() == "1" || str_focus.Trim().ToLower() == "true")
                {
                    if (i > 0)
                    {
                        _status = status + "<br /> Tiêu điểm"; i++;
                    }
                    else { _status = status + "Tiêu điểm"; i++; }
                }
            }
            catch { ;}
            try
            {
                str_ishost = ishot.ToString();
                if (str_ishost.Trim() == "1" || str_focus.Trim().ToLower() == "true")
                {
                    if (i > 0)
                    {
                        _status = status + "<br /> Tin nóng"; i++;
                    }
                    else { _status = status + "Tin nóng"; i++; }
                }
            }
            catch { ;}
            return _status;
        }

        public string GetChatluong(object cl)
        {
            string Chatluong = "Copy";
            try
            {
                int _cl = int.Parse(cl.ToString());
                switch (_cl)
                {
                    case 0:
                        Chatluong = "Copy";
                        break;
                    case 1:
                        Chatluong = "Thực hiện";
                        break;
                    default:
                        Chatluong = "Copy";
                        break;
                }
            }
            catch { ;}
            return Chatluong;
        }

        public string GetInt(object obj)
        {
            int _int = 0;
            try
            {
                _int = int.Parse(obj.ToString());
            }
            catch { ;}
            return _int.ToString();
        }

        public string GetHeso(object obj)
        {
            string heso = "0";
            try
            {
                heso = obj.ToString().Replace(',', '.');
            }
            catch { ;}
            return heso;
        }

        public string GetCategoryName(Object ID, Object loaiid)
        {
            string str = "";
            ChuyenmucDAL _dal = new ChuyenmucDAL();
            if (_dal.GetOneFromT_ChuyenmucByID(Convert.ToInt32(loaiid)) != null)
            {
                string loaitinbaiID = "", loaianhID = "", loaivideoID = "", loaianhTsID = "";
                loaitinbaiID = ConfigurationManager.AppSettings["NewsType"].ToString();
                loaianhID = ConfigurationManager.AppSettings["ImageType"].ToString();
                loaivideoID = ConfigurationManager.AppSettings["VideoType"].ToString(); ;
                loaianhTsID = ConfigurationManager.AppSettings["AnhTSType"].ToString();
                if (loaiid.ToString().Trim() == loaitinbaiID)
                {
                    try
                    {
                        if (_dal.GetOneFromT_ChuyenmucByID(Convert.ToInt32(ID)) == null)
                            str = "";
                        else
                            str = _dal.GetOneFromT_ChuyenmucByID(Convert.ToInt32(ID)).Ten_ChuyenMuc.ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (loaiid.ToString().Trim() == loaianhID)
                {
                    str = "Góc ảnh";
                }
                else if (loaiid.ToString().Trim() == loaivideoID)
                {
                    str = "Âm thanh - Hình ảnh";
                }
                else if (loaiid.ToString().Trim() == loaianhTsID)
                {
                    str = "Thời sự qua ảnh";
                }
            }
            return str;
        }
        protected string SetLinkPopup(object _loai, object _id)
        {
            string _strLink = string.Empty;
            int _loaiId = Convert.ToInt32(_loai.ToString());
            if (_loaiId == 3)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/Multimedia/ViewVideo.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            else if (_loaiId == 1)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/View/ViewDetails.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            else if (_loaiId == 2)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/PhongSuAnh/T_Album_Categories_View.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&catps=" + _id.ToString() + "')";
            else if (_loaiId == 4)
                _strLink = "Javascript:PopupWindow('" + Global.ApplicationPath + "/Quanlynhanbut/ViewImage.aspx?Menu_ID=" + Request.QueryString["Menu_ID"] + "&ID=" + _id.ToString() + "')";
            return _strLink;
        }
        public void loadRole()
        {
            cmd_Chamnhanbut.Enabled = true;//_Role.R_Pub;

        }
    }
}
