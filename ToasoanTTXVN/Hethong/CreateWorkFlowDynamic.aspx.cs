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
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Hethong
{
    public partial class CreateWorkFlowDynamic : System.Web.UI.Page
    {
        UltilFunc Ulti = new UltilFunc();
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        HPCBusinessLogic.DAL.QuyTrinhDAL _QTDAL = new HPCBusinessLogic.DAL.QuyTrinhDAL();
        private int maanpham
        {
            get { if (ViewState["maanpham"] != null) return Convert.ToInt32(ViewState["maanpham"]); else return 0; }

            set { ViewState["maanpham"] = value; }
        }
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
                        BindData();
                    }
                }
            }
        }

        protected void BindData()
        {
            string where = "1=1";
            if (!String.IsNullOrEmpty(txtTenAnpham.Text.Trim()))
                where += "AND " + string.Format(" Ten_Anpham like N'%{0}%'", UltilFunc.SqlFormatText(this.txtTenAnpham.Text.Trim()));

            pages.PageSize = Global.MembersPerPage;
            DataSet _ds;
            _ds = _QTDAL.BindGridT_QuyTrinh(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _QTDAL.BindGridT_QuyTrinh(pages.PageIndex - 1, pages.PageSize, where);
            grdListAnpham.DataSource = _ds.Tables[0].DefaultView;
            grdListAnpham.DataBind();
            pages.TotalRecords = currentPage.TotalRecords = TotalRecords;
            currentPage.TotalPages = pages.CalculateTotalPages();
            currentPage.PageIndex = pages.PageIndex;
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void linkSearch_OnClick(object sender, EventArgs e)
        {
            BindData();
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
        private void SetAddEdit(bool isList, bool isRole)
        {
            pnlList.Visible = isList;
            plRole.Visible = isRole;
        }
        private void BinDoituong()
        {
            string sql = "Select Ma_Doituong,Ten_Doituong from T_Doituong where 1=1";
            DataTable dt = Ulti.ExecSqlDataSet(sql).Tables[0];
            lst_Quytrinh.DataSource = dt;
            lst_Quytrinh.DataValueField = "Ma_doituong";
            lst_Quytrinh.DataTextField = "Ten_Doituong";
            lst_Quytrinh.DataBind();
            lst_Quytrinh.AutoUpdateAfterCallBack = true;
        }
        private void BinDoituongTheoAnpham(string MaDT, int STT)
        {

            DataTable dt = _QTDAL.BindGridT_T_QuytrinhWorkFlow(MaDT, maanpham, STT);
            gdListQuytrinh.DataSource = dt;
            gdListQuytrinh.DataBind();
            gdListQuytrinh.AutoUpdateAfterCallBack = true;
        }
        private void BinQuytrinhtheoAnPham()
        {
            string sql = "Select ID, Ma_Doituong_Gui,Ma_Doituong_Nhan from T_Quytrinh where  Ma_AnPham=" + maanpham;
            DataTable dt = Ulti.ExecSqlDataSet(sql).Tables[0];
            DataGridWorkFlow.DataSource = dt;
            DataGridWorkFlow.DataBind();
            DataGridWorkFlow.AutoUpdateAfterCallBack = true;
        }
        private void BinQuytrinhtheoAnPhamMaDoiTuong(string MaQT)
        {
            string sql = "Select ID, Ma_Doituong_Gui,Ma_Doituong_Nhan from T_Quytrinh where Ma_Doituong_Gui='" + MaQT + "' and Ma_AnPham=" + maanpham;
            DataTable dt = Ulti.ExecSqlDataSet(sql).Tables[0];
            DataGridWorkFlow.DataSource = dt;
            DataGridWorkFlow.DataBind();
            DataGridWorkFlow.AutoUpdateAfterCallBack = true;
        }
        private void InsertQuytrinh()
        {
            HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            action.Ma_Chucnang = int.Parse(Request["Menu_ID"].ToString());
            ArrayList ar = new ArrayList();
            string MAQT = "";
            int i = 0;
            foreach (DataGridItem m_Item in gdListQuytrinh.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    if (i == 0)
                    {
                        MAQT = "'" + gdListQuytrinh.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString() + "'";
                    }
                    else
                    {
                        MAQT += ",'" + gdListQuytrinh.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString() + "'";
                    }
                    i++;

                }
            }
            _QTDAL.InsertT_Quytrinh(lst_Quytrinh.SelectedValue.ToString(), MAQT, maanpham, _user.UserID, DateTime.Now, _user.UserID, DateTime.Now);
            BinQuytrinhtheoAnPham();
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);

        }
        public void grdListWorkFlow_EditCommand(object source, DataGridCommandEventArgs e)
        {

            #region GhiLog
            HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion

            if (e.CommandArgument.ToString().ToLower() == "role")
            {
                SetAddEdit(false, true);
                maanpham = Convert.ToInt32(this.grdListAnpham.DataKeys[e.Item.ItemIndex]);
                string Tenanpham = UltilFunc.GetTenAnpham_Display(maanpham);
                plRole.GroupingText = "Thiết lập quy trình cho loại ấn phẩm " + Tenanpham;
                BinDoituong();
                BinQuytrinhtheoAnPham();
                action.Thaotac = "Tạo quy trình động theo loại ấn phẩm: Ma_Anpham:" + Tenanpham;
            }
            action.Ma_Chucnang = int.Parse(Request["Menu_ID"].ToString());
            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
        }

        protected void lst_Quytrinh_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int STT = _QTDAL.GetOneFromT_DoiTuongByID(lst_Quytrinh.SelectedValue.ToString()).STT;
            BinDoituongTheoAnpham(lst_Quytrinh.SelectedValue.ToString(), STT);
        }
        protected void LinkExit_Click(object sender, EventArgs e)
        {
            ViewState["maanpham"] = null;
            SetAddEdit(true, false);
        }
        protected void linkSave_Click(object sender, EventArgs e)
        {
            SetAddEdit(false, true);
            InsertQuytrinh();

        }
    }
}
