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
    public partial class ChiTietTin : System.Web.UI.Page
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected HPCInfo.T_RolePermission _Role = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (!IsPostBack)
            {
                try
                {
                    string ticket = "";
                    int proId = 0;
                    int id = 0;
                    if (Request["Ticket"] != null)
                        ticket = Request["Ticket"].ToString();
                    if (Request["ProductID"] != null)
                        proId = Convert.ToInt32(Request["ProductID"]);
                    if (Request["ID"] != null)
                        id = Convert.ToInt32(Request["ID"]);
                    if (id > 0)
                    {
                        if (proId == 1)
                            this.TIT.Text = "<title>XEM CHI TIẾT TIN NGUỒN</title>";
                        else
                            this.TIT.Text = "<title>XEM CHI TIẾT TIN TƯ LIỆU</title>";
                        LoadData(ticket, proId, id);
                    }
                }
                catch { }
            }
        }
        protected void LoadData(string ticket, int proID, int id)
        {
            try
            {
                WS_TTX.WebService1SoapClient _ws = new WS_TTX.WebService1SoapClient();
                WS_TTX.ObjectNews Info = _ws.getContentNewsByID(ticket, proID, id);
                if (Info != null)
                {
                    this.litDateTime.Text = Convert.ToDateTime(Info.DateCreate.ToString()).ToString("HH:mm, dd/MM/yyyy");
                    this.litTittle.Text = Info.Title;
                    this.litSapo.Text = ConvertWordToHTML(Info.Summary);
                    this.litContents.Text = ConvertWordToHTML(Info.Content);
                    this.litCategorys.Text = Info.CategoryName;
                    if (Info.Author != "")
                    {
                        this.litAuthor.Text += "<div class=\"author\">";
                        this.litAuthor.Text += Info.Author;
                        this.litAuthor.Text += "</div>";
                    }
                    InsertLogTin(Info.CategoryName, Info.Title,Info.ProductID, Info.ID, 0, _user.UserID, _user.UserName);
                }
                //if (_dr["News_DateEdit"] != System.DBNull.Value)
                //    this.litDateTime.Text = Convert.ToDateTime(_dr["News_DateEdit"]).ToString("HH:mm, dd/MM/yyyy");
                //if (_dr["News_DatePublished"] != System.DBNull.Value)
                //    this.litDateTime.Text = Convert.ToDateTime(_dr["News_DatePublished"]).ToString("HH:mm, dd/MM/yyyy");
                //this.litTittle.Text = _dr["News_Tittle"].ToString();
                //this.litSapo.Text = _dr["News_Summary"].ToString();
                //string contents = _dr["News_Body"].ToString();
                //this.litCategorys.Text = "<a href=\"#\">" + HPCBusinessLogic.UltilFunc.GetCategoryName(_dr["CAT_ID"]) + "</a>";
                ////this.litContents.Text = SearchImgTag(contents);
                //this.litContents.Text = contents;

                //if (_dr["News_AuthorName"] != null)
                //{
                //    this.litAuthor.Text += "<div class=\"author\">";
                //    this.litAuthor.Text += _dr["News_AuthorName"].ToString();
                //    this.litAuthor.Text += "</div>";
                //}
            }
            catch { }
        }
        public void InsertLogTin(string CategoryName, string Title, int ProductID, object ID, int MenuID, int UserID, string UserName)
        {
            int _userID = int.Parse(UserID.ToString());
            string _node = "";
            if (ProductID == 1)
            {
                _node = "Tra cứu tin nguồn";
            }
            else
            {
                _node = "Tra cứu tin tư liệu";
            }
            UltilFunc.WriteLogHistorySource(_userID, UserName, Title, CategoryName, 0, _node, ProductID);
        }
        private string ConvertWordToHTML(string wordContent)
        {
            string strOutput = wordContent;
            strOutput = strOutput.Replace(((char)(160)).ToString(), "&nbsp");
            // Thay the ki tu xuong dong, ve dau dong
            strOutput = strOutput.Replace("\r\n", "<br>");
            //  Thay the ki tu xuong dong
            strOutput = strOutput.Replace("\r", "<br>");
            strOutput = strOutput.Replace("\n", "<br>");
            //  Thay the cac ki tu khac
            strOutput = strOutput.Replace("BLOCKQUOTE", "DIV");
            return strOutput.Trim();
        }
    }
}
