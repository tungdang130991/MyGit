using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic.DAL;
using System.Text.RegularExpressions;
using System.Data;
using HPCServerDataAccess;
using System.Data.SqlClient;

namespace ToasoanTTXVN.View
{
    public partial class ViewDetails : System.Web.UI.Page
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        double _News_ID = 0;
                        if (Request["id"] != null)
                            _News_ID = Convert.ToDouble(Request["id"]);
                        if (_News_ID > 0)
                        {
                            if (Request["Old_id"] != null)
                            {
                                this.TIT.Text = "<title>XEM, IN, VERSION BÀI VIẾT</title>";
                                LoadData_Version(_News_ID);
                            }
                            else
                            {
                                this.TIT.Text = "<title>XEM, IN CHI TIẾT</title>";
                                LoadData(_News_ID);
                            }
                        }
                    }
                    catch { }
                }
            }
        }
        private void LoadData_Version(double News_ID)
        {
            SqlService _service = new SqlService();
            System.Data.SqlClient.SqlDataReader _dr = null;
            try
            {
                _service.AddParameter("@News_ID", SqlDbType.Int, News_ID);
                _dr = _service.ExecuteSPReader(@"CMS_NEWSDETAILPrintCMS_VerSion");
                if (_dr != null)
                {
                    while (_dr.Read())
                    {
                        if (_dr["News_DateEdit"] != null)
                            this.litDateTime.Text = Convert.ToDateTime(_dr["News_DateEdit"]).ToString("HH:mm, dd/MM/yyyy");
                        //if (_dr["News_DatePublished"] != null)
                        //    this.litDateTime.Text = Convert.ToDateTime(_dr["News_DatePublished"]).ToString("HH:mm, dd/MM/yyyy");
                        this.litTittle.Text = _dr["News_Tittle"].ToString();
                        this.litSapo.Text = _dr["News_Summary"].ToString();
                        string contents = _dr["News_Body"].ToString();
                        this.litCategorys.Text = "<a href=\"#\">" + HPCBusinessLogic.UltilFunc.GetCategoryName(_dr["CAT_ID"]) + "</a>";
                        //this.litContents.Text = SearchImgTag(contents);
                        this.litContents.Text = contents;

                        if (_dr["News_AuthorName"] != null)
                        {
                            this.litAuthor.Text += "<div class=\"author\">";
                            this.litAuthor.Text += _dr["News_AuthorName"].ToString();
                            this.litAuthor.Text += "</div>";
                        }
                    }
                }
            }
            catch { }
            finally { if (_dr != null) { _dr.Close(); _dr.Dispose(); } _service.CloseConnect(); _service.Disconnect(); }
        }
        protected void LoadData(double _newsID)
        {
            SqlService _service = new SqlService();
            System.Data.SqlClient.SqlDataReader _dr = null;
            try
            {
                _service.AddParameter("@News_ID", SqlDbType.Int, _newsID);
                _dr = _service.ExecuteSPReader(@"CMS_GetNewDetailsPrints");
                if (_dr != null)
                {
                    while (_dr.Read())
                    {
                        if (_dr["News_DateEdit"] != System.DBNull.Value)
                            this.litDateTime.Text = Convert.ToDateTime(_dr["News_DateEdit"]).ToString("HH:mm, dd/MM/yyyy");
                        if (_dr["News_DatePublished"] != System.DBNull.Value)
                            this.litDateTime.Text = Convert.ToDateTime(_dr["News_DatePublished"]).ToString("HH:mm, dd/MM/yyyy");
                        this.litTittle.Text = _dr["News_Tittle"].ToString();
                        this.litSapo.Text = _dr["News_Summary"].ToString();
                        string contents = _dr["News_Body"].ToString();
                        this.litCategorys.Text = "<a href=\"#\">" + HPCBusinessLogic.UltilFunc.GetCategoryName(_dr["CAT_ID"]) + "</a>";
                        //this.litContents.Text = SearchImgTag(contents);
                        this.litContents.Text = contents;

                        if (_dr["News_AuthorName"] != null)
                        {
                            this.litAuthor.Text += "<div class=\"author\">";
                            this.litAuthor.Text += _dr["News_AuthorName"].ToString();
                            this.litAuthor.Text += "</div>";
                        }
                    }
                }
            }
            catch { }
            finally { if (_dr != null) { _dr.Close(); _dr.Dispose(); } _service.CloseConnect(); _service.Disconnect(); }
        }
    }
}
