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
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;

namespace ToasoanTTXVN.DeTai
{
    public partial class ViewCompare : System.Web.UI.Page
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
                            T_IdieaVersion _obj2 = new T_IdieaVersion();
                            T_IdieaDAL dal = new T_IdieaDAL();
                            ChuyenmucDAL caDal = new ChuyenmucDAL();
                            obj = dal.GetOneFromT_IdieaByID(id);

                            //add by nvthai
                            T_IdieaVersion _objVer = new T_IdieaVersion();
                            if (obj.Diea_Stype == 1)
                            {
                                _objVer = dal.GetOneFromT_IdieaVersionByIDVersion(id, 4, 54);
                                _obj2 = dal.GetOneFromT_IdieaVersionByIDVersion(id, 1, 62);
                            }
                            else
                            {
                                _objVer = dal.GetOneFromT_IdieaVersionByIDVersion(id, 4, 54);
                                _obj2 = dal.GetOneFromT_IdieaVersionByIDVersion(id, 3, 23);
                            }


                            //add by Hung viet
                            if (obj.Cat_ID > 0)
                            {
                                this.litCatName.Text = caDal.GetOneFromT_ChuyenmucByID(int.Parse(_obj2.Cat_ID.ToString())).Ten_ChuyenMuc;
                                this.litCM.Text = caDal.GetOneFromT_ChuyenmucByID(int.Parse(_objVer.Cat_ID.ToString())).Ten_ChuyenMuc;
                            }
                            else
                            {
                                this.litCatName.Text = "";
                                this.litCM.Text = "";
                            }
                            this.litTittle.Text = _obj2.Title.ToString();
                            this.litTacgia.Text = UltilFunc.GetUserFullName(_obj2.User_Created);
                            this.litContent.Text = CleanHTMLFont(_obj2.Comment.ToString());
                            this.lit_baiviet.Text = CleanHTMLFont(_obj2.Diea_Articles.ToString());
                            string coutstring = this.litContent.Text + " " + this.lit_baiviet.Text;
                            this.litCounter.Text = UltilFunc.WordCount(coutstring).ToString() + " từ";

                            this.litTenDetai.Text = _objVer.Title.ToString();
                            this.literNguoiviet.Text = UltilFunc.GetUserFullName(_objVer.User_Edit);
                            this.litContents.Text = CleanHTMLFont(_objVer.Comment.ToString());
                            this.litbai.Text = CleanHTMLFont(_objVer.Diea_Articles.ToString());
                            string coutstring2 = this.litContents.Text + " " + this.litbai.Text;
                            this.litCouter2.Text = UltilFunc.WordCount(coutstring2).ToString() + " từ";
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