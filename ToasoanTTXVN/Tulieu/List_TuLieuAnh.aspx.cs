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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.Tulieu
{
    public partial class List_TuLieuAnh : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        T_Users _user;
        T_RolePermission _Role = null;

        AnhDAL _DalAnh = new AnhDAL();
        SSOLibDAL lib = new SSOLibDAL();
        UltilFunc Ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                }
                if (!IsPostBack)
                { }
            }
        }
        public string GetWhere()
        {
            string _where = "1=1";
            if (txt_tieude.Text.Trim() != "")
                _where += " and Ma_Tulieu in (select Ma_Tulieu from T_TuLieu where Tieude like N'%" + txt_tieude.Text.Trim() + "%')";
            if (txt_chuthich.Text.Trim() != "")
                _where += " and Chuthich like N'%" + txt_chuthich.Text.Trim() + "%'";
            if (txt_file.Text.Trim() != "")
                _where += " and TenFile_Goc like N'%" + txt_file.Text.Trim() + "%'";
            if (txt_tungay.Text.Trim() != "" && txt_denngay.Text.Trim() != "")
                _where += " and Ngaytao >='" + txt_chuthich.Text.Trim() + " 00:00:00' and Ngaytao<='" + txt_denngay.Text.Trim() + " 23:59:59'";
            if (txt_PVCTV.Text.Trim() != "")
                _where += " and Nguoichup like N'%" + txt_PVCTV.Text.Trim() + "%'";
            if (HiddenFieldTacgiatin.Value != "")
                _where += " and Ma_Nguoichup=" + HiddenFieldTacgiatin.Value;
            return _where;
        }
        public void LoadDataImage()
        {
            DataSet _ds = _DalAnh.Sp_SelectTulieuanhDynamic(GetWhere(), "NgayTao DESC");
            this.dgrListImages.DataSource = _ds;
            this.dgrListImages.DataBind();

        }
        protected void btnTimkiem_Click(object sender, EventArgs e)
        {
            LoadDataImage();
        }
        public void dgrListImages_EditCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandArgument.ToString().ToLower() == "download")
            {
                Label lbFileAttach = (Label)e.Item.FindControl("lbFileAttach");
                string filePath = Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["viewimg"].ToString() + lbFileAttach.Text);
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(lbFileAttach.Text));
                Response.WriteFile(filePath);
                Response.Flush();
                Response.End();
            }
        }
    }
}
