using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;
using HPCServerDataAccess;
using System.Data;
namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class BindListCongviec : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (!IsPostBack)
            {
                if (Request["pageid"] != null)
                {
                    Session["CurrentPage"] = Request["pageid"].ToString();
                }
                if (Session["CurrentPage"] != null)
                {
                    pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                    BindList_CongViec();
                }
                else
                {
                    BindList_CongViec();
                }
            }
        }
        //Phần danh sách công việc
        #region

        public void BindList_CongViec()
        {
            string where = " NguoiNhan = " + _user.UserID.ToString() + " OR NguoiTao = " + _user.UserID.ToString() + " OR NguoiGiaoViec = " + _user.UserID.ToString() + "  ";

            CongviecDAL _DAL = new CongviecDAL();
            DataSet _ds;
            pages.PageSize = 8;
            _ds = _DAL.BindGridT_Congviec(pages.PageIndex, pages.PageSize, where);

            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Congviec(pages.PageIndex - 1, pages.PageSize, where);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                grdList.DataSource = _ds;
                grdList.DataBind();
            }
            else
            {
                _ds.Tables[0].Rows.Add(_ds.Tables[0].NewRow());
                grdList.DataSource = _ds;
                grdList.DataBind();
                int columncount = grdList.Items[0].Cells.Count;
                grdList.Items[0].Cells.Clear();
                grdList.Items[0].Cells.Add(new TableCell());
                grdList.Items[0].Cells[0].ColumnSpan = columncount;
                grdList.Items[0].Cells[0].Text = "Không có bản ghi nào";
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["CurrentPage"] = pages.PageIndex;
        }
        protected string BindUserName(string _Id)
        {
            string strReturn = "";
            T_Users _nguoidung = new T_Users();

            if (!String.IsNullOrEmpty(_Id) && Convert.ToInt32(_Id) > 0)
            {
                _nguoidung = _NguoidungDAL.GetUserByUserName_ID(Convert.ToInt32(_Id));
                strReturn = _nguoidung.UserFullName;
            }
            else
                strReturn = "";
            return strReturn;
        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            BindList_CongViec();
        }
        public void grdList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            if (lblSTT != null)
            {
                lblSTT.Text = (pages.PageIndex * pages.PageSize + e.Item.ItemIndex + 1).ToString();
            }
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }

            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdList_EditCommand(object source, DataGridCommandEventArgs e)
        {
            #region GhiLog
            Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
            T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
            action.Ma_Nguoidung = _user.UserID;
            action.TenDaydu = _user.UserFullName;
            action.HostIP = IpAddress();
            action.NgayThaotac = DateTime.Now;
            #endregion
            CongviecDAL objDAL = new CongviecDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int cvID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                Response.Redirect("~/Congviec/EditCongviec.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + cvID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                objDAL.DeleteOneFromT_Congviec(Convert.ToDouble(this.grdList.DataKeys[e.Item.ItemIndex].ToString()), _user.UserID);
                action.Thaotac = "[Xóa T_Congviec]-->[Thao tác Xóa Trong Bảng T_Congviec][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                this.BindList_CongViec();
            }
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
        #endregion
    }
}
