using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Collections.Generic;

namespace ToasoanTTXVN.Phathanh
{
    public partial class YeucauPHList : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        T_Users _user = null;
        T_RolePermission _Role = null;
        private int MenuID
        {
            get
            {
                if (!string.IsNullOrEmpty("" + Request["Menu_ID"]))
                    return Convert.ToInt32(Request["Menu_ID"]);
                else return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UltilFunc.IsNumeric(Request["Menu_ID"]))
            {
                if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                    Response.Redirect("~/Admin/Errors/AccessDenied.aspx");
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                _Role = _userDAL.GetRole4UserMenu(_user.UserID, MenuID);
                this.btnAdd.Visible = _Role.R_Read;
                if (!IsPostBack)
                {
                    if (Session["CurrentPage"] != null)
                    {
                        pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                        Session["CurrentPage"] = null;
                        Danhsach_Yeucau();
                    }
                    else
                    {
                        Danhsach_Yeucau();
                    }
                }

            }
        }

        #region Method
        public void Danhsach_Yeucau()
        {
            string where = " Loai = 2 ";
            if (!String.IsNullOrEmpty(this.hdnValue.Value.ToString().Trim()))
                where += " AND Ma_Khachhang = " + string.Format(hdnValue.Value.ToString()) + " ";
            if (!String.IsNullOrEmpty(this.txt_Noidung.Text.Trim()))
                where += " AND TenQuangCao like N'%" + this.txt_Noidung.Text.Trim()  + "%'";
            pages.PageSize = Global.MembersPerPage;
            HPCBusinessLogic.DAL.YeucauDAL _yeucauDAL = new HPCBusinessLogic.DAL.YeucauDAL();
            DataSet _ds;
            _ds = _yeucauDAL.BindGridT_Yeucauquangcao(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _yeucauDAL.BindGridT_Yeucauquangcao(pages.PageIndex - 1, pages.PageSize, where);
            grdList.DataSource = _ds.Tables[0];
            grdList.DataBind();
            _ds.Clear();
            hdnValue.Value = "";
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;                    
        }
        protected string TenKhachHang(string _Id)
        {
            string strReturn = "";
            T_KhachHang _kh = new T_KhachHang();
            KhachhangDAL _dal = new KhachhangDAL();
            if (!String.IsNullOrEmpty(_Id) && Convert.ToInt32(_Id) > 0)
            {
                _kh = _dal.GetOneFromT_KhachHangByID(Convert.ToInt32(_Id));
                strReturn = _kh.Ten_KhachHang;
            }
            else
                strReturn = "";
            return strReturn;
        }
        protected string IsStatusGet(string str)
        {
            string strReturn = "";
            if (str == "1") //da duyet
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/OK.gif";
            if (str == "0") //chua duyet
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";
            if (str == "2") //da co hop dong
                strReturn = Global.ApplicationPath + "/Dungchung/Images/acces_file.png";
            return strReturn;
        }
        protected string GetTooltip(string str)
        {
            string strReturn = "";
            if (str == "1") //da duyet
                strReturn = "Yêu cầu đã duyệt";
            if (str == "0") //chua duyet
                strReturn = "Yêu cầu chưa duyệt";
            if (str == "2") //da co hop dong
                strReturn = "Yêu cầu đã có hợp đồng";
            return strReturn;
        }
        protected bool IsHopdongExsits(int _ID)
        {
            bool _exits = false;
            DataSet _ds;
            UltilFunc _ultil = new UltilFunc();
            _ds = _ultil.ExecSqlDataSet("select Ma_Yeucau from T_Hopdong where Ma_Yeucau = " + _ID.ToString());
            if (_ds.Tables[0].Rows.Count > 0)
                _exits = true;
            else
                _exits = false;
            return _exits;
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
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _write = false;
            return _write = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Write;
        }
        protected bool IsRoleRead()
        {
            bool _Read = false;
            return _Read = _userDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Read;
        }
        #endregion

        #region Click

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Phathanh/YeucauPHEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString());
        }
         protected void Search_Click(object sender, EventArgs e)
         {
             pages.PageIndex = 0;
             Danhsach_Yeucau();
         }
         protected void pages_IndexChanged(object sender, EventArgs e)
         {
             Danhsach_Yeucau();
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
                 bool _bool;
                 _bool = IsHopdongExsits(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                 if (_bool == true)
                     btnDelete.Attributes.Add("onclick", "return alert(\"Yêu cầu này đã làm hợp đồng!\");");
                 else
                     btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
             }
             if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
             {
                 e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                 e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
             }
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
             HPCBusinessLogic.DAL.YeucauDAL objDAL = new HPCBusinessLogic.DAL.YeucauDAL();
             if (e.CommandArgument.ToString().ToLower() == "edit")
             {
                 int cusID = Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                 Response.Redirect("~/Phathanh/YeucauPHEdit.aspx?Menu_ID=" + Page.Request["Menu_ID"].ToString() + "&ID=" + cusID);
             }
             if (e.CommandArgument.ToString().ToLower() == "isstatus")
             {
                double _ID = Convert.ToDouble(this.grdList.DataKeys[e.Item.ItemIndex].ToString());
                Int16 _check = objDAL.GetOneFromT_YeuCauByID(_ID).Trangthai;
                if (_check == 0)
                {
                    objDAL.UpdateinfoT_Yeucau(" [Trangthai] = 1 where ID = " + _ID.ToString());
                    action.Thaotac = "[Cập nhật trạng thái bảng T_Yeucau][ID:" + _ID.ToString() + " ][Trangthai = 1]";
                    Danhsach_Yeucau();
                }
                if (_check == 1)
                {
                    bool _bool;
                    _bool = IsHopdongExsits(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                    if (_bool == false)
                    {
                        objDAL.UpdateinfoT_Yeucau(" [Trangthai] = 0 where ID = " + _ID.ToString());
                        action.Thaotac = "[Cập nhật trạng thái bảng T_Yeucau][ID:" + _ID.ToString() + " ][Trangthai = 0]";                        
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Yêu cầu này đã làm hợp đồng!');", true);
                        return;
                    }
                    Danhsach_Yeucau();
                }                
             }
             if (e.CommandArgument.ToString().ToLower() == "delete")
             {
                 bool _bool;
                 _bool = IsHopdongExsits(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                 if (_bool == false)
                 {
                     objDAL.DeleteFromT_YeuCauQCByID(Convert.ToInt32(this.grdList.DataKeys[e.Item.ItemIndex].ToString()));
                     action.Thaotac = "[Thao tác Xóa Trong Bảng T_Yeucau][ID:" + this.grdList.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                     Danhsach_Yeucau();
                 }
                 else
                     return;                
             }
         }
        #endregion

    }
}
