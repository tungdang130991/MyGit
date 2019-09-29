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
using System.Text.RegularExpressions;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.QlyAnh
{
    public partial class ListImages : System.Web.UI.Page
    {
        NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
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
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, MenuID);
                    btnUpload.Enabled = _Role.R_Write;
                    btnDelete.Enabled = _Role.R_Delete;
                    if (!IsPostBack)
                    {
                        if (Session["CurrentPage"] != null)
                        {
                            pageappro.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            LoadDataApprovied();
                        }
                        else
                        {
                            LoadDataApprovied();
                        }

                    }
                }
            }
        }

        public void LoadDataApprovied()
        {
            string where = " TieuDe IS NULL And NguoiTao =" + _user.UserID;
            if (!String.IsNullOrEmpty(this.txt_FromDate.Text.Trim()))
                where += " AND " + string.Format(" NgayTao >='{0}'", UltilFunc.ToDate(this.txt_FromDate.Text.Trim(), "dd/MM/yyyy").ToShortDateString() + " 00:00:00 ");
            if (!String.IsNullOrEmpty(this.txt_ToDate.Text.Trim()))
                where += " AND " + string.Format(" NgayTao <='{0}'", UltilFunc.ToDate(this.txt_ToDate.Text.Trim(), "dd/MM/yyyy").ToShortDateString() + " 23:59:59 ");
            where += " ORDER BY NgayTao DESC ";
            AnhDAL _DAL = new AnhDAL();
            DataSet _ds;

            pageappro.PageSize = 5;
            _ds = _DAL.BindGridT_Anh(pageappro.PageIndex, pageappro.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _DAL.BindGridT_Anh(pageappro.PageIndex - 1, pageappro.PageSize, where);
            dgrListAppro.DataSource = _ds;
            dgrListAppro.DataBind();
            pageappro.TotalRecords = CurrentPage1.TotalRecords = TotalRecords;
            CurrentPage1.TotalPages = pageappro.CalculateTotalPages();
            CurrentPage1.PageIndex = pageappro.PageIndex;
            Session["CurrentPage"] = pageappro.PageIndex;
        }


        public void dgrListAppro_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }
        public void dgrListAppro_EditCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                dgrListAppro.EditItemIndex = e.Item.ItemIndex;
                LoadDataApprovied();
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                AnhDAL _DAL = new AnhDAL();
                int _ID = Convert.ToInt32(dgrListAppro.DataKeys[e.Item.ItemIndex].ToString());
                _DAL.DeleteFromT_Anh(_ID);
                LoadDataApprovied();
            }
        }
        public void dgrListAppro_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            T_Anh objimage = new T_Anh();
            AnhDAL _DAL = new AnhDAL();
            int _ID = Convert.ToInt32(dgrListAppro.DataKeys[e.Item.ItemIndex].ToString());
            TextBox txtTieude = e.Item.FindControl("txtTieude") as TextBox;
            TextBox txtChuthich = e.Item.FindControl("txtChuthich") as TextBox;
            TextBox txtTacgia = e.Item.FindControl("txtTacgia") as TextBox;
            string _tieude = "";
            string _chuthich = "";
            string _tacgia = "";
            int _tacgiaID = 0;
            if (txtTieude != null)
            {
                if (!String.IsNullOrEmpty(txtTieude.Text.Trim()))
                    _tieude = txtTieude.Text.Trim();

            }
            if (txtChuthich != null)
            {
                if (!String.IsNullOrEmpty(txtChuthich.Text.Trim()))
                    _chuthich = txtChuthich.Text.Trim();
            }
            if (txtTacgia != null)
            {
                if (!String.IsNullOrEmpty(txtTacgia.Text.Trim()))
                {
                    _tacgia = txtTacgia.Text;
                    _tacgiaID = Convert.ToInt32(UltilFunc.GetTacgiaID(_tacgia).ToString());
                }
            }
            objimage.Ma_Anh = _ID;
            objimage.TieuDe = _tieude;
            objimage.Chuthich = _chuthich;
            objimage.NguoiChup = _tacgia;
            objimage.Ma_Nguoichup = _tacgiaID;
            _DAL.InsertUpdateT_Anh(objimage);
            //_DAL.UpdateinfoT_Anh("TieuDe=N'" + _tieude + "',Chuthich=N'" + _chuthich + "',NguoiChup=N'" + _tacgia + "',Ma_Nguoichup=" + _tacgiaID + " WHERE Ma_Anh=" + _ID);

            dgrListAppro_CancelCommand(source, e);
        }
        public void dgrListAppro_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            dgrListAppro.EditItemIndex = -1;
            LoadDataApprovied();
        }

        protected void pageappro_IndexChanged(object sender, EventArgs e)
        {
            LoadDataApprovied();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pageappro.PageIndex = 0;
            LoadDataApprovied();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            LoadDataApprovied();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList ar = new ArrayList();
            foreach (DataGridItem m_Item in dgrListAppro.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    ar.Add(double.Parse(dgrListAppro.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            if (ar.Count > 0)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    int _ID = int.Parse(ar[i].ToString());
                    AnhDAL _DAL = new AnhDAL();
                    _DAL.DeleteFromT_Anh(_ID);
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('Chưa chọn ảnh muốn xóa.!!')", true);
                return;
            }
            LoadDataApprovied();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }
        public static string GetUserName()
        {
            string strTemp = HPCSecurity.CurrentUser.Identity.Name.ToString();
            return strTemp;

        }
        protected bool IsRoleDelete()
        {
            bool _delete = false;
            return _delete = _NguoidungDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Delete;
        }
        protected bool IsRoleWrite()
        {
            bool _delete = false;
            return _delete = _NguoidungDAL.GetRole4UserMenu(_user.UserID, MenuID).R_Write;
        }
        protected string GetListIDselect()
        {
            string _listID = null;
            foreach (DataGridItem m_Item in dgrListAppro.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    string _EID = this.dgrListAppro.DataKeys[m_Item.ItemIndex].ToString();
                    if (_listID == null)
                        _listID += _EID.ToString();
                    else
                        _listID += "," + _EID.ToString();
                }
            }
            return _listID;

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

    }
}
