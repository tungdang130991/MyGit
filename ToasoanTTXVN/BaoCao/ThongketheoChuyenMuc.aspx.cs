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
    public partial class ThongketheoChuyenMuc :BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (HPCComponents.CommonLib.IsNumeric(Request["Menu_ID"]) == true)
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
            UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 "), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
            DataTable dt = _userDAL.GetAllUser_By_CatID(0);
            drop_User.Items.Clear();
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
            SqlService _sqlservice = new SqlService();
            DataSet _ds = null;
            if (fd != "")
            {
                _sqlservice.AddParameter("@fromdate", SqlDbType.NVarChar, fd + " 00:00:01", 25);
            }
            if (td != "")
            {
                _sqlservice.AddParameter("@todate", SqlDbType.NVarChar, td + " 23:59:59", 25);
            }

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

        protected void ViewReport_OnClick(object sender, EventArgs e)
        {
            if (checkDate())
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                DataSet _ds;
                int intChuyenMuc = 0;
                int intUser = 0;
                if (cbo_chuyenmuc.SelectedIndex > 0) intChuyenMuc = Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString());
                if (drop_User.SelectedIndex > 0) intUser = Convert.ToInt32(drop_User.SelectedValue.ToString());
                _ds = _ultil.GetStoreDataSet("[CMS_List_ChuyenMuc_BaiViet]", new string[] { "@fromdate", "@todate", "@Cate", "@UserID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), intChuyenMuc, intUser });
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
                _ds.Dispose();
            }
        }
        #endregion

        protected void cbo_chuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            NguoidungDAL _Obj = new NguoidungDAL();
            int catid = 0;
            try
            {
                catid = int.Parse(cbo_chuyenmuc.SelectedValue);
            }
            catch { ;}
            DataTable dt = _Obj.GetAllUser_By_CatID(catid);
            drop_User.DataSource = null;
            drop_User.DataBind();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    drop_User.DataSource = dt;
                    drop_User.DataTextField = "Fullname";
                    drop_User.DataValueField = "Ma_Nguoidung";
                    drop_User.DataBind();
                    drop_User.Items.Add(new ListItem(CommonLib.ReadXML("lblTatca"), "0"));

                    drop_User.SelectedValue = "0";

                }
            }
        }


        #region Print report

        protected void Print_OnClick(object sender, EventArgs e)
        {
            PrintData();
        }
        private void PrintData()
        {
            if (checkDate())
            {
                HPCBusinessLogic.UltilFunc _ultil = new UltilFunc();
                DataSet _ds;
                int intChuyenMuc = 0;
                int intUser = 0;
                if (cbo_chuyenmuc.SelectedIndex > 0) intChuyenMuc = Convert.ToInt32(cbo_chuyenmuc.SelectedValue.ToString());
                if (drop_User.SelectedIndex > 0) intUser = Convert.ToInt32(drop_User.SelectedValue.ToString());
                _ds = _ultil.GetStoreDataSet("[CMS_List_ChuyenMuc_BaiViet]", new string[] { "@fromdate", "@todate", "@Cate", "@UserID" }, new object[] { txt_FromDate.Text.Trim(), txt_ToDate.Text.Trim(), intChuyenMuc, intUser });
                if (_ds.Tables[0].Rows.Count > 0 && _ds != null)
                {
                    gvList.DataSource = _ds.Tables[0];
                    gvList.DataBind();
                    GridViewToExcel.Export("Ket_Qua_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".xls", gvList, "THỐNG KÊ BÀI VIẾT THEO CHUYÊN MỤC", " Chuyên mục: " + this.cbo_chuyenmuc.SelectedItem.Text + "<br>Từ ngày " + txt_FromDate.Text.Trim() + " đến ngày " + txt_ToDate.Text.Trim(), "");
                    this.gvList.AllowPaging = true;
                    this.gvList.DataBind();
                    _ds.Dispose();
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Không có dữ liệu!');", true);
                    return;
                }
            }
        }
        int total = 0;
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "sl"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblamount = (Label)e.Row.FindControl("lblTotal");
                lblamount.Text = total.ToString();
            }
        }
        #endregion
    }
}
