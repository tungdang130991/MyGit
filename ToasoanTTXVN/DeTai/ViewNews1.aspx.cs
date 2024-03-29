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
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using HPCServerDataAccess;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SSOLib;
using SSOLib.ServiceAgent;
namespace ToasoanTTXVN.DeTai
{
    public partial class ViewNews1 : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        protected T_Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!Page.IsPostBack)
                    {
                        int id = int.Parse(Page.Request.QueryString["ID"].ToString());

                        if (Page.Request.QueryString["Menu_ID"] != null)
                        {
                            T_Idiea obj = new T_Idiea();
                            T_IdieaDAL dal = new T_IdieaDAL();
                            ChuyenmucDAL caDal = new ChuyenmucDAL();
                            obj = dal.GetOneFromT_IdieaByID(id);
                            //add by Hung viet
                            if (obj.Cat_ID > 0)
                                this.litCatName.Text = caDal.GetOneFromT_ChuyenmucByID(int.Parse(obj.Cat_ID.ToString())).Ten_ChuyenMuc;
                            else
                                this.litCatName.Text = "";
                            this.litTittle.Text = obj.Title.ToString();
                            this.litTacgia.Text = UltilFunc.GetUserFullName(obj.User_Created);
                            this.litContent.Text = CleanHTMLFont(obj.Comment.ToString());
                            this.lit_baiviet.Text = CleanHTMLFont(obj.Diea_Articles.ToString());
                            string count = this.litContent.Text + "" + this.lit_baiviet.Text;
                            this.LitCount.Text = UltilFunc.WordCount(count) + " từ ";
                            if (obj.Status == 6)
                                this.LitDatePublisher.Text = obj.Date_Duyet.ToString("dd/MM/yyyy HH:mm") + " (GMT + 7)";
                            ViewState["ver"] = -1;


                        }
                    }
                }
            }

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
    }
}
