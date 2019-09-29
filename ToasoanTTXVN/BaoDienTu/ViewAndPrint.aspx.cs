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
using HPCBusinessLogic.DAL;
using System.Text.RegularExpressions;
using HPCShareDLL;

namespace ToasoanTTXVN.BaoDienTu
{
    public partial class ViewAndPrint : System.Web.UI.Page
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            if (_user != null)
            {
                if (!Page.IsPostBack)
                {
                    int id = int.Parse(Page.Request.QueryString["ID"].ToString());
                    T_News obj = new T_News();
                    T_NewsDAL dal = new T_NewsDAL();
                    ChuyenmucDAL caDal = new ChuyenmucDAL();
                    obj = dal.load_T_news(id);
                    if (obj.CAT_ID > 0)
                        this.litCatName.Text = caDal.GetOneFromT_ChuyenmucByID(int.Parse(obj.CAT_ID.ToString())).Ten_ChuyenMuc;
                    else
                        this.litCatName.Text = "";
                    this.litDanNhap.Text = obj.News_Sub_Title.ToString();
                    this.litTittle.Text = obj.News_Tittle.ToString();
                    this.LitSummery.Text = CleanHTMLFont(CleanHTMLSummary(obj.News_Summary.ToString()));
                    this.litContent.Text = CleanHTMLFont(obj.News_Body.ToString());
                    if (obj.News_Status == 6)
                        this.LitDatePublisher.Text = obj.News_DateEdit.ToString("dd/MM/yyyy HH:mm") + " (GMT + 7)";
                    string count = this.litContent.Text;
                    this.LitCount.Text = UltilFunc.WordCount(count) + " từ ";
                }
            }
            else Response.Redirect("~/Errors/AccessDenied.aspx");
        }
        public static string CleanHTMLSummary(string Contents)
        {
            Contents = Regex.Replace(Contents, "<(select|option|script|style|title)(.*?)>((.|\n)*?)</(select|option|script|style|title)>", " ", RegexOptions.IgnoreCase);
            Contents = Regex.Replace(Contents, "<div>", "");
            Contents = Regex.Replace(Contents, "</div>", "");
            Contents = Regex.Replace(Contents, "(;|--|create|drop|select|insert|delete|update|union|sp_|xp_)", "");
            Contents = Regex.Replace(Contents, "<([\\s\\S])+?>", " ", RegexOptions.IgnoreCase).Replace("  ", " ");
            Contents = Regex.Replace(Contents, "\r\n", "").Trim();
            Contents = Regex.Replace(Contents.Trim(), "Normal 0 false false false MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal 0  false false false     MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal 0 false false false MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal  0    false  false  false         MicrosoftInternetExplorer4", "");
            Contents = Regex.Replace(Contents.Trim(), "Normal  0    false  false  false          MicrosoftInternetExplorer4", "");
            return (Contents);
        }
        public static string CleanHTMLFont(string Contents)
        {
            Contents = Regex.Replace(Contents, "Verdana", "Arial");
            Contents = Regex.Replace(Contents, "Times New Roman", "Arial");
            Contents = Regex.Replace(Contents, "Tahoma", "Arial");
            return (Contents);
        }
        public string CssViewLangues()
        {
            int _ID = 0;
            if (Request["ID"] != null)
                _ID = Convert.ToInt32(Request["ID"]);
            string str = "";
            str = "<link href=\"" + Global.ApplicationPath + "/Style/CSSVIEW/import.css\" rel=\"Stylesheet\" rev=\"Stylesheet\" />";
            str += "<link href=\"" + Global.ApplicationPath + "/Style/CSSVIEW/default.css\" rel=\"Stylesheet\" rev=\"Stylesheet\" />";
            return str;
        }
    }
}
