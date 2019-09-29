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
using System.Text;
using HPCInfo;
using HPCBusinessLogic;
using HPCComponents;
using System.Data.SqlClient;
using HPCServerDataAccess;
using SSOLib;
using SSOLib.ServiceAgent;
namespace ToasoanTTXVN.Masters
{
    public partial class ITC : System.Web.UI.MasterPage
    {
        UltilFunc _ulti = new UltilFunc();
        T_Users user;
        AnPhamDAL _dalanpham = new AnPhamDAL();
        int Ma_QTBT = 0;
        protected string lblXinChao = "Xin chào";
        protected string lblDoimatkhau = "Đổi mật khẩu";
        protected string _lang = "vi";
        protected void Page_Load(object sender, EventArgs e)
        {

            #region--Show/hide language link
            if (!string.IsNullOrEmpty(Convert.ToString(Session["culture"])))
            {
                _lang = Convert.ToString(Session["culture"]);
                if (_lang == "en")
                {
                    lbtVietnam.Visible = true;
                    lbtEnglish.Visible = false;
                    lblXinChao = "Hello";
                    lblDoimatkhau = "Change password";
                    lb_Exit.Text = "[ Exit ]";
                }
                else
                {
                    lbtEnglish.Visible = true;
                    lbtVietnam.Visible = false;
                    lb_Exit.Text = "[Thoát hệ thống ]";
                }
            }
            else
            {
                Session["culture"] = "vi";
                lbtVietnam.Visible = false;
                lbtEnglish.Visible = true;
                lb_Exit.Text = "[Thoát hệ thống ]";
            }
            #endregion--

            if (!IsPostBack)
            {

                if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
                {
                    if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                    {
                        if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                            Response.Redirect("~/Errors/AccessDenied.aspx");
                        this.litImageIcon.Text = "<img src=\" ../Dungchung/Images/Hethong.png \" style=\"border: 0px; height: 20px\">";
                        this.litTitleMenuName.Text = GetMenuName(Convert.ToInt32(Page.Request["Menu_ID"].ToString()), _lang);
                    }
                }


                string _name = HPCSecurity.CurrentUser.Identity.Name;
                NguoidungDAL _userDAL = new NguoidungDAL();

                user = _userDAL.GetUserByUserName(_name);
                if (user != null)
                {
                    Ma_QTBT = UltilFunc.GetColumnValuesOne("T_NguoidungQTBT", "Ma_QTBT", "Ma_Nguoidung=" + user.UserID);
                    if (Ma_QTBT == 0)
                    {
                        FuncAlert.AlertJS(this, "Người dùng chưa được phân quyền vào quy trình biên tập!");
                        return;
                    }
                    litMenu.Text = BindNavigation(Convert.ToInt32(user.UserID));
                    litUserName.Text = user.UserFullName;
                    lb_Exit.Visible = true;

                }
                else
                {
                    lb_Exit.Visible = false;
                    Page.Response.Redirect("~/login.aspx", true);
                }

            }

        }


        #region Menu Bind Data
        public string GetMenu4User(int User_ID, int MaAnpham)
        {
            HPCBusinessLogic.UltilFunc _untilDAL = new HPCBusinessLogic.UltilFunc();
            string _sql = "GetChucNang4User";
            DataTable _dt;
            string _tmp = string.Empty;
            try
            {
                _dt = _untilDAL.GetStoreDataSet(_sql, new string[] { "@User_ID", "@MaAnPham" }, new object[] { User_ID, MaAnpham }).Tables[0];
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_tmp.Trim() == "")
                        {
                            _tmp = _dt.Rows[i]["Ma_Chucnang"].ToString();
                        }
                        else
                        {
                            _tmp = _tmp + "," + _dt.Rows[i]["Ma_Chucnang"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _tmp;
        }
        private string isParent()
        {
            int Menu_ID = CommonLib.CheckNullInt(Request["Menu_ID"]);
            if (Menu_ID > 0)
            {
                HPCBusinessLogic.UltilFunc ultilDAL = new UltilFunc();
                if (CommonLib.isParrentMenu(Menu_ID))
                    return Menu_ID.ToString();
                else
                    return UltilFunc.GetColumnValues("T_Chucnang", "Ma_Chucnang_Cha", " Ma_Chucnang=" + Menu_ID.ToString());
            }
            else return "0";
        }
        public string BindNavigation(int User_ID)
        {
            StringBuilder _sbHeader = new StringBuilder();
            StringBuilder _sb = new StringBuilder();

            string _tmp = string.Empty;
            if (Ma_QTBT != 0)
                _tmp = GetMenu4User(User_ID, Ma_QTBT);

            DataTable _dt;
            DataTable _dtChild;

            string _sql = string.Empty; string _sqlChild = string.Empty;
            _sql = "BindNavigationByUserID";
            HPCBusinessLogic.UltilFunc _untilDAL = new HPCBusinessLogic.UltilFunc();
            _sb.Append(" <tr> <td align=\"left\"> <table cellspacing=\"0\" cellpadding=\"0\" border=\"0\"> <tr>");
            _sb.Append("  <td width=\"2px\">  </td>");
            try
            {
                _dt = _untilDAL.GetStoreDataSet(_sql, new string[] { "@User_ID", "@lang_ID" }, new object[] { User_ID, _lang }).Tables[0];

                if (_dt.Rows.Count > 0)
                {

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {

                        if (CommonLib.CheckNullInt(_dt.Rows[i]["node"]) > 0)
                        {

                            _sb.Append("<td class=\"MenuBar\"><div id=\"idchildMenu\" onmouseover=\"ShowMenu('SubMenu" + CommonLib.CheckNullStr(_dt.Rows[i]["Ma_Chucnang"]) + "')\" onmouseout=\"HideMenu('SubMenu" + CommonLib.CheckNullStr(_dt.Rows[i]["Ma_Chucnang"]) + "',2)\" >");
                            _sb.Append(" <div id=\"Menu" + CommonLib.CheckNullStr(_dt.Rows[i]["Ma_Chucnang"]) + "\">");
                            _sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td width=\"6\">");
                            if (CommonLib.CheckNullInt(isParent()) == CommonLib.CheckNullInt(_dt.Rows[i]["Ma_Chucnang"]))
                            {
                                _sb.Append("<img src='" + Global.ApplicationPath + "/Dungchung/images/Active/Left.jpg'>");
                                _sb.Append("</td>");
                                _sb.Append("<td background=\"" + Global.ApplicationPath + "/Dungchung/images/Active/Center.jpg\" style=\"background-repeat: repeat-x;\">");

                            }
                            else
                            {
                                _sb.Append("<img src='" + Global.ApplicationPath + "/Dungchung/images/Left.jpg'>");
                                _sb.Append("</td>");
                                _sb.Append("<td background=\"" + Global.ApplicationPath + "/Dungchung/images/Center.jpg\" style=\"background-repeat: repeat-x;\">");

                            }

                            _sb.Append("<table cellpadding=\"3\" cellspacing=\"0\" border=\"0\" class=\"MenuBar\"><tr>");
                            if (CommonLib.CheckNullStr(_dt.Rows[i]["Icon"]).Length > 0)
                                _sb.Append("<td><img alt=\"\" src=\"" + UltilFunc.TinPath + CommonLib.CheckNullStr(_dt.Rows[i]["Icon"]) + "\" align=\"absmiddle\" onload=\"FixPNG(this)\" /></td>");
                            if (CommonLib.CheckNullInt(isParent()) == CommonLib.CheckNullInt(_dt.Rows[i]["Ma_Chucnang"]))
                            {
                                _sb.Append("<td class=\"TextAtive\">");
                            }
                            else { _sb.Append("<td class=\"Text\">"); }

                            if (CommonLib.CheckNullStr(_dt.Rows[i]["URL_Chucnang"]).Length > 0)
                            {
                                _sb.Append("<a href=\"" + Global.ApplicationPath + CommonLib.CheckNullStr(_dt.Rows[i]["URL_Chucnang"]) + "\">" + CommonLib.CheckNullStr(_dt.Rows[i]["Ten_chucnang"]) + "</a>");
                            }
                            else
                            {
                                _sb.Append(CommonLib.CheckNullStr(_dt.Rows[i]["Ten_chucnang"]));

                            }
                            if (CommonLib.CheckNullInt(isParent()) == CommonLib.CheckNullInt(_dt.Rows[i]["Ma_Chucnang"]))
                            {
                                _sb.Append("</td><td><img src=\"" + Global.ApplicationPath + "/Dungchung/images/Active/Arrow.gif\" alt=\"\" /></td></tr></table></td>");
                            }
                            else
                            {
                                _sb.Append("</td><td><img src=\"" + Global.ApplicationPath + "/Dungchung/images/Arrow.gif\" alt=\"\" /></td></tr></table></td>");
                            }

                            _sb.Append("<td width=\"6\">");
                            if (CommonLib.CheckNullInt(isParent()) == CommonLib.CheckNullInt(_dt.Rows[i]["Ma_Chucnang"]))
                            {
                                _sb.Append("<img src=\"" + Global.ApplicationPath + "/Dungchung/images/Active/Right.jpg\">");
                            }
                            else
                            {
                                _sb.Append("<img src=\"" + Global.ApplicationPath + "/Dungchung/images/Right.jpg\">");
                            }

                            _sb.Append("</td></tr></table></div>");


                            if (_tmp.Length == 1)
                            {
                                if (_lang == "vi")
                                {
                                    _sqlChild = "SELECT Ten_chucnang,URL_Chucnang,STT,Ma_Chucnang,Icon,Mota,Quytrinh,Ma_Doituong,(select count(*) FROM T_Chucnang WHERE  T_Chucnang.HoatDong=1 and  Ma_Chucnang_Cha = mn.Ma_Chucnang) childnodecount ";
                                    _sqlChild += " FROM T_Chucnang mn WHERE  mn.HoatDong=1 and  Ma_Chucnang_Cha = " + _dt.Rows[i]["Ma_Chucnang"] + " AND Ma_Chucnang =" + _tmp + " ORDER BY mn.STT";
                                }
                                else
                                {
                                    _sqlChild = "SELECT MenuEnglish as Ten_chucnang,URL_Chucnang,STT,Ma_Chucnang,Icon,Mota,Quytrinh,Ma_Doituong,(select count(*) FROM T_Chucnang WHERE  T_Chucnang.HoatDong=1 and  Ma_Chucnang_Cha = mn.Ma_Chucnang) childnodecount ";
                                    _sqlChild += " FROM T_Chucnang mn WHERE  mn.HoatDong=1 and  Ma_Chucnang_Cha = " + _dt.Rows[i]["Ma_Chucnang"] + " AND Ma_Chucnang =" + _tmp + " ORDER BY mn.STT";
                                }
                            }
                            else
                            {
                                if (_lang == "vi")
                                {
                                    _sqlChild = " SELECT Ten_chucnang,[dbo].[fn_MenuQuytrinh]('" + Ma_QTBT + "',Quytrinh,Ma_Chucnang) as URL_Chucnang,STT,Ma_Chucnang,Icon,Mota,Quytrinh,Ma_Doituong,(select count(*) FROM T_Chucnang WHERE T_Chucnang.HoatDong=1  and Ma_Chucnang_Cha = mn.Ma_Chucnang) childnodecount";
                                    _sqlChild += " FROM T_Chucnang mn WHERE mn.HoatDong=1 and Ma_Chucnang_Cha = " + _dt.Rows[i]["Ma_Chucnang"] + " AND Ma_Chucnang IN (" + _tmp + ") ORDER BY mn.STT";
                                }
                                else
                                {
                                    _sqlChild = " SELECT MenuEnglish as Ten_chucnang,[dbo].[fn_MenuQuytrinh]('" + Ma_QTBT + "',Quytrinh,Ma_Chucnang) as URL_Chucnang,STT,Ma_Chucnang,Icon,Mota,Quytrinh,Ma_Doituong,(select count(*) FROM T_Chucnang WHERE T_Chucnang.HoatDong=1  and Ma_Chucnang_Cha = mn.Ma_Chucnang) childnodecount";
                                    _sqlChild += " FROM T_Chucnang mn WHERE mn.HoatDong=1 and Ma_Chucnang_Cha = " + _dt.Rows[i]["Ma_Chucnang"] + " AND Ma_Chucnang IN (" + _tmp + ") ORDER BY mn.STT";
                                }
                            }
                            try
                            {
                                _dtChild = _untilDAL.ExecSqlDataSet(_sqlChild).Tables[0];
                                if (_dtChild.Rows.Count > 0)
                                {
                                    _sb.Append("<div id=\"SubMenu" + CommonLib.CheckNullStr(_dt.Rows[i]["Ma_Chucnang"]) + "\" class=\"Hide\">");
                                    _sb.Append("<div style=\"padding-top: 0px; position: absolute;z-index:200px;\" align=\"left\">");
                                    _sb.Append("<div style=\"width: 200px; padding-top: 5px;z-index:50000px;\" class=\"SubMenu\">");
                                    _sb.Append("<table border=\"0\" class=\"cssMenuTables\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");

                                    for (int j = 0; j < _dtChild.Rows.Count; j++)
                                    {
                                        _sb.Append("<tr class=\"SubMenuItem\" onmouseover=\"this.className='SubMenuItemOver'\" onmouseout=\"this.className='SubMenuItem'\">");
                                        _sb.Append("<td width=\"100%\" style=\"padding-top: 5px;\">");
                                        _sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\" align=\"center\">");
                                        _sb.Append("<tr> <td width=\"24px\" align=\"left\" style=\"padding-left: 4px;\" valign=\"top\">");
                                        _sb.Append("<td style=\"padding-right: 10px;\" align=\"left\">");
                                        bool quytrinh = Convert.ToBoolean(_dtChild.Rows[j]["Quytrinh"].ToString());

                                        if (quytrinh == true)
                                            _sb.Append("<a class=\"SubMenuItem\" href='" + Global.ApplicationPath + "/" + _dtChild.Rows[j]["URL_Chucnang"].ToString() + "?Menu_ID=" + _dtChild.Rows[j]["Ma_Chucnang"].ToString() + "&MaDoiTuong=" + _dtChild.Rows[j]["Ma_Doituong"].ToString() + "'>" + CommonLib.CheckNullStr(_dtChild.Rows[j]["Ten_chucnang"]) + "</a>");
                                        else
                                            _sb.Append("<a class=\"SubMenuItem\" href='" + Global.ApplicationPath + "/" + _dtChild.Rows[j]["URL_Chucnang"].ToString() + "?Menu_ID=" + _dtChild.Rows[j]["Ma_Chucnang"].ToString() + "'>" + CommonLib.CheckNullStr(_dtChild.Rows[j]["Ten_chucnang"]) + "</a>");

                                        _sb.Append("<div class=\"SubMenuItemDesc\" align=\"left\" style=\"padding-top: 3px; font-size: 10px\">");
                                        _sb.Append(CommonLib.CheckNullStr(_dtChild.Rows[j]["Mota"]));
                                        _sb.Append("  </div>  </td>  </tr>  <tr><td height=\"10px\" >  </td></tr></table></td></tr>");

                                    }
                                    _sb.Append("</table></div></div> </div> ");

                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            _sb.Append(" </div></td><td width=\"2px\"></td>");
                        }
                        else
                        {
                            //_sb.Append("<td class=\"MenuBar\"><div>");
                            //_sb.Append(" <div id=\"Menu" + CommonLib.CheckNullStr(_dt.Rows[i]["Ma_Chucnang"]) + "\">");
                            //_sb.Append("<td background=\"" + Global.ApplicationPath + "/Dungchung/images/Center.jpg\" style=\"background-repeat: repeat-x;\">");
                            //_sb.Append("<table cellpadding=\"3\" cellspacing=\"0\" border=\"0\" class=\"MenuBar\"><tr>");

                            //if (CommonLib.CheckNullStr(_dt.Rows[i]["URL_Chucnang"]).Length > 0)
                            //{
                            //    _sb.Append("<td class=\"Text\"><a href=\"" + Global.ApplicationPath + CommonLib.CheckNullStr(_dt.Rows[i]["URL_Chucnang"]) + "\">" + CommonLib.CheckNullStr(_dt.Rows[i]["Ten_chucnang"]) + "</a></td><td><img src=\"" + Global.ApplicationPath + "/images/Arrow.gif\" alt=\"\" /></td></tr></table></td>");
                            //}
                            //else
                            //{
                            //    _sb.Append("<td class=\"Text\">" + CommonLib.CheckNullStr(_dt.Rows[i]["Ten_chucnang"]) + "</td>");
                            //}
                            //_sb.Append("</td></tr></table></td><td width=\"6\"><img src=\"" + Global.ApplicationPath + "/Dungchung/images/Right.jpg\"></td></tr></table></div>");
                            //_sb.Append(" </div></td><td width=\"5px\"></td>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            _sb.Append(" </tr>  </table></td> </tr> <tr><td height=\"4\" bgcolor=\"#167cba\" align=\"right\"/></tr>");

            return _sb.ToString();
        }
        private string GetMenuName(int Menu_ID)
        {

            SqlService _service = new SqlService();
            try
            {
                string str = "";
                string strParrent = "";
                _service.AddParameter("@Menu_ID", SqlDbType.Int, Menu_ID);
                SqlDataReader _drMenu = null;
                _drMenu = _service.ExecuteSPReader("SP_GetTen_chucnangMaster");
                if (_drMenu.HasRows)
                {
                    while (_drMenu.Read())
                    {
                        str = _drMenu["Ten_chucnang"].ToString();
                        strParrent = _drMenu["MenuParrent"].ToString();
                    }
                }
                _drMenu.Close();
                _service.CloseConnect();
                _service.Disconnect();

                return "&nbsp;" + strParrent + " » " + str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _service.CloseConnect();
                _service.Disconnect();
            }
        }
        private string GetMenuName(int Menu_ID, string languageCode)
        {

            SqlService _service = new SqlService();
            try
            {
                string str = "";
                string strParrent = "";
                _service.AddParameter("@Menu_ID", SqlDbType.Int, Menu_ID);
                _service.AddParameter("@LanguageCode", SqlDbType.NVarChar, languageCode.ToLower());
                SqlDataReader _drMenu = null;
                _drMenu = _service.ExecuteSPReader("[SP_GetTen_chucnangMasterWithLang]");
                if (_drMenu.HasRows)
                {
                    while (_drMenu.Read())
                    {
                        str = _drMenu["Ten_chucnang"].ToString();
                        strParrent = _drMenu["MenuParrent"].ToString();
                    }
                }
                _drMenu.Close();
                _service.CloseConnect();
                _service.Disconnect();

                return "&nbsp;" + strParrent + " » " + str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _service.CloseConnect();
                _service.Disconnect();
            }
        }
        #endregion

        #region Buttom
        protected void lb_Exit_Click(object sender, EventArgs e)
        {
            Session["capchaimgvna"] = null;
            string _name = HPCSecurity.CurrentUser.Identity.Name;
            NguoidungDAL _userDAL = new NguoidungDAL();
            user = _userDAL.GetUserByUserName(_name);
            if (user != null)
            {
                UltilFunc ulti = new UltilFunc();
                string sqlupdate = " update T_TinBai set Nguoi_Khoa=0 where Nguoi_Khoa=" + user.UserID;
                ulti.ExecSql(sqlupdate);
                string sqlupdateT_news = " UPDATE T_News SET News_EditorID=0,News_Lock =0 WHERE News_EditorID=" + user.UserID;
                ulti.ExecSql(sqlupdateT_news);
                string sqlupdateUserLogin = " update T_UserLogin set Loggedin=0 where User_Name='" + user.UserName + "'";
                ulti.ExecSql(sqlupdateUserLogin);
            }
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Page.Response.Cookies.Clear();
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            FormsAuthentication.SignOut();
            Page.Response.Cookies.Remove("hpcinfomation");
            Page.Response.Cookies["hpcinfomation"].Expires = DateTime.Now.AddMilliseconds(-1);
            UltilFunc.Log_Action(user.UserID, user.UserFullName, DateTime.Now, 0, "Thoát khỏi hệ thống");
            Page.Response.Redirect(Global.ApplicationPath + "/Login.aspx");
        }
        protected void LinkButtonAnpham_Click(object sender, EventArgs e)
        {
            Session["Ma_QTBT"] = null;
            Page.Response.Redirect(Global.ApplicationPath + "/AnPhamBao.aspx");
        }
        #endregion

        protected void RequestLanguageChange_Click(object sender, EventArgs e)
        {
            LinkButton senderLink = sender as LinkButton;

            //store requested language as new culture in the session
            Session["culture"] = senderLink.CommandArgument;

            //reload last requested page with new culture
            Server.Transfer(Request.Path);
        }
    }
}
