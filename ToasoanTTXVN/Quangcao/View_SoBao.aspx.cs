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
namespace ToasoanTTXVN.Quangcao
{
    public partial class View_SoBao : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                }
            }
            if (!IsPostBack)
            {

                BindDataLichSuThanhToanQuangCao();

            }
        }

        public void BindDataLichSuThanhToanQuangCao()
        {
            string where = "Loai=1 and HOPDONG_SO=" + Request["ID"];
            HPCBusinessLogic.DAL.LichsuthanhtoanDAL dal = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            
            pages.PageSize = Global.MembersPerPage;

            DataSet _ds;
            _ds = dal.BindGridT_LichsuThanhtoan(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = dal.BindGridT_LichsuThanhtoan(pages.PageIndex - 1, pages.PageSize, where);
            dgLichsuthanhtoanQC.DataSource = _ds;
            dgLichsuthanhtoanQC.DataBind();
            pages.TotalRecords = CurrentPage.TotalRecords = TotalRecords;
            CurrentPage.TotalPages = pages.CalculateTotalPages();
            CurrentPage.PageIndex = pages.PageIndex;
        }
        public T_LichsuThanhtoan SetOjectItems()
        {
            T_LichsuThanhtoan obj = new T_LichsuThanhtoan();
            obj.HOPDONG_SO = 1;
            obj.MA_KHACHHANG = 1;
            obj.SOTIEN = int.Parse(txtsotien.Text);
            obj.NGUOITHU = _user.UserID;
            obj.TENNGUOINOP = txtTennguoinop.Text;
            obj.Loai = 1;
            obj.NGAYTHU = Convert.ToDateTime(txtNgayThuTien.Text);
            return obj;
        }
        public void GetItems()
        {
            T_LichsuThanhtoan _obj = new T_LichsuThanhtoan();
            txtsotien.Text = _obj.SOTIEN.ToString();
            txtNgayThuTien.Text = _obj.NGAYTHU.ToString();
            txtTennguoinop.Text = _obj.TENNGUOINOP;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            HPCBusinessLogic.DAL.LichsuthanhtoanDAL dal = new HPCBusinessLogic.DAL.LichsuthanhtoanDAL();
            string Thaotac = "";
            T_LichsuThanhtoan _obj = SetOjectItems();
            if (ViewState["ID"] != null)
                Thaotac = "Sửa đổi thông tin lịch sử thanh toán quảng cáo";
            else
                Thaotac = "Thêm mới thông tin lịch sử thanh toán quảng cáo";
            dal.Sp_InsertT_LichsuThanhtoan(_obj);
            BindDataLichSuThanhToanQuangCao();
            UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"].ToString()), Thaotac);


        }
    }
}
