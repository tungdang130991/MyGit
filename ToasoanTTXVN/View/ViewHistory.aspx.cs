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
using Rainbow.MergeEngine;

namespace ToasoanTTXVN.View
{
    public partial class ViewHistory : System.Web.UI.Page
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
                            this.TIT.Text = "<title>" + CommonLib.ReadXML("titXemversion") + "</title>";
                            Search();
                        }
                        //double _News_ID = 0;
                        //if (Request["id"] != null)
                        //    _News_ID = Convert.ToDouble(Request["id"]);
                        //if (_News_ID > 0)
                        //{
                        //    if (Request["Old_id"] != null)
                        //    {
                        //        this.TIT.Text = "<title>XEM, IN, VERSION BÀI VIẾT</title>";
                        //        LoadData_Version(_News_ID);
                        //    }
                        //    else
                        //    {
                        //        this.TIT.Text = "<title>XEM, IN CHI TIẾT</title>";
                        //        LoadData(_News_ID);
                        //    }
                        //}
                    }
                    catch { }
                }
            }
        }
        public void Search()
        {
            string NewsID = "0";
            try { NewsID = Page.Request.QueryString["ID"].ToString(); }
            catch { ;}
            if (NewsID != "0")
            {
                HPCBusinessLogic.DAL.T_NewsDAL obj = new HPCBusinessLogic.DAL.T_NewsDAL();
                DataSet ds = obj.GetHistory(int.Parse(NewsID));
                LoadDetails(int.Parse(NewsID), false, false, 0, false);
                if (ds != null)
                {
                    try
                    {
                        dgr_tintuc1.DataSource = ds.Tables[0];
                        dgr_tintuc1.DataBind();
                    }
                    catch
                    {
                        dgr_tintuc1.DataSource = null;
                        dgr_tintuc1.DataBind();
                    }
                }
            }
            else
            {
                dgr_tintuc1.DataSource = null;
                dgr_tintuc1.DataBind();
            }

        }
        public void LoadDetails(int NewsID, bool ViewHistory, bool chieu, int currentindex, bool clickgrid)
        {
            if (!ViewHistory)
            {
                T_News obj = new T_News();
                T_NewsDAL dal = new T_NewsDAL();
                ChuyenmucDAL caDal = new ChuyenmucDAL();
                obj = dal.load_T_news(NewsID);
                System.Text.StringBuilder _Builer = new System.Text.StringBuilder();
                string _image = "";
                if (obj != null && obj.News_ID > 0)
                {
                    try
                    {
                        if (obj.News_DateEdit != null && obj.News_DateEdit.ToString().Length > 0)
                            this.litDateTime.Text = Convert.ToDateTime(obj.News_DateEdit.ToString()).ToString("dd/MM/yyyy HH:mm");
                        if (obj.News_DatePublished != null && obj.News_DatePublished.ToString().Length > 0)
                            this.litDateTime.Text = Convert.ToDateTime(obj.News_DatePublished.ToString()).ToString("dd/MM/yyyy HH:mm");
                        this.litTittle.Text = obj.News_Tittle.ToString();
                        this.litSapo.Text = obj.News_Summary.ToString();
                        this.litCategorys.Text = "<a href=\"#\">" + HPCBusinessLogic.UltilFunc.GetCategoryName(obj.CAT_ID) + "</a>";
                        if (obj.News_PhotoAtt != null && obj.News_PhotoAtt.ToString().Length > 0)
                        {
                            if (UltilFunc.CheckFrames(obj.News_PhotoAtt.ToString()) == false)
                            {

                                _Builer.Append("<div id=\"liveTV\"></div>");
                                _Builer.Append(" <script type=\"text/javascript\">jwplayer('liveTV').setup({ ");
                                _Builer.Append(" image: '" + HPCComponents.Global.TinPath + obj.Images_Summary.ToString() + "',");
                                _Builer.Append(" file: '" + HPCComponents.Global.TinPath + obj.News_PhotoAtt.ToString() + "', ");
                                _Builer.Append(" width: 665,height: 400,autostart: false,");
                                _Builer.Append(" backcolor: '#000000',frontcolor: '#ffffff',lightcolor: '#ffffff',screencolor: '#ffffff',");
                                _Builer.Append(" allowscriptaccess: true,allowfullscreen: true,controlbar: 'over',stretching:'fill'});</script>");
                                if (obj.News_DescImages != null && obj.News_DescImages.ToString().Length > 0)
                                {
                                    _Builer.Append("<div class=\"title\"><span>" + obj.News_DescImages.ToString() + "</span></div>");
                                }
                                //this.litImage.Text = _Builer.ToString();
                            }
                            else
                            {
                                string _youReziHeigh = "";
                                try
                                {
                                    string _youReziWith = UltilFunc.ReplapceYoutoubeWidth(obj.News_PhotoAtt.ToString(), "665");
                                    _youReziHeigh = UltilFunc.ReplapceYoutoubeHight(_youReziWith.ToString(), "400");
                                }
                                catch
                                {
                                    _youReziHeigh = "";
                                }
                                //this.litImage.Text = _youReziHeigh.ToString();
                            }

                        }
                        else
                        {
                            if (obj.Images_Summary != null && obj.Images_Summary.ToString().Length > 0)
                            {

                                _image = "<img style=\"width:665px;\" alt=\"" + obj.News_Tittle.ToString() + "\" title=\"" + obj.News_Tittle.ToString() + "\" src=\"" + HPCComponents.Global.TinPath + obj.Images_Summary.ToString() + "\" />";
                                if (obj.News_DescImages != null && obj.News_DescImages.ToString().Length > 0)
                                    _image += "<div class=\"title\"><span>" + obj.News_DescImages.ToString() + "</span></div>";
                            }
                            //this.litImage.Text = _image.ToString();

                        }
                        this.litContents.Text = SearchImgTag(SearchSlideImg(obj.News_Body.ToString()));
                        //this.litCategorys.Text = "<a href=\"#\">" + dt.Rows[0]["Categorys_Name"] + "</a>";
                        if (obj.News_AuthorName != null && obj.News_AuthorName.ToString().Length > 0)
                        {
                            this.litAuthor.Text = "<div class=\"author\">" + obj.News_AuthorName.ToString() + "</div>";
                        }
                        //this.LitNhuanbut.Text = obj.News_TienNB.ToString();
                        if (obj.News_TienNB > 0.0)
                            this.LitNhuanbut.Text = string.Format("{0:#,#}", obj.News_TienNB).Replace(".", ",");
                        else
                            this.LitNhuanbut.Text = "";
                        //string count = string.Empty;
                        //count = this.litContents.Text;
                        //if (count != "")
                        //    this.LitCount.Text = UltilFunc.WordCount(count) + " từ ";
                        //else
                        //    this.LitCount.Text = "";
                        this.Literal_nguoinhap.Text = HPCBusinessLogic.UltilFunc.GetUserFullName(obj.News_AuthorID);
                        this.Literal_nguoiluu.Text = HPCBusinessLogic.UltilFunc.GetUserFullName(obj.News_EditorID);
                        this.Literal_ngayluu.Text = Convert.ToDateTime(obj.News_DateEdit).ToString("dd/MM/yyyy HH:mm");

                        ViewState["ver"] = -1;
                    }
                    catch { ;}
                }
            }
            else
            {
                T_News obj = new T_News();
                T_NewsDAL dal = new T_NewsDAL();
                ChuyenmucDAL caDal = new ChuyenmucDAL();
                obj = dal.load_T_news(NewsID);
                DataSet ds = dal.GetDetailHistory(NewsID);
                System.Text.StringBuilder _Builer = new System.Text.StringBuilder();
                string _image = "";
                int gridview_index = 0;
                if (ds != null)
                {
                    try
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows[0]["News_DateEdit"] != null && dt.Rows[0]["News_DateEdit"].ToString().Length > 0)
                            this.litDateTime.Text = Convert.ToDateTime(dt.Rows[0]["News_DateEdit"].ToString()).ToString("dd/MM/yyyy HH:mm");
                        if (dt.Rows[0]["News_DatePublished"] != null && dt.Rows[0]["News_DatePublished"].ToString().Length > 0)
                            this.litDateTime.Text = Convert.ToDateTime(dt.Rows[0]["News_DatePublished"].ToString()).ToString("dd/MM/yyyy HH:mm");
                        this.litTittle.Text = dt.Rows[0]["News_Tittle"].ToString();
                        this.litSapo.Text = dt.Rows[0]["News_Summary"].ToString();
                        this.litCategorys.Text = "<a href=\"#\">" + HPCBusinessLogic.UltilFunc.GetCategoryName(dt.Rows[0]["CAT_ID"]) + "</a>";

                        if (dt.Rows[0]["News_PhotoAtt"] != null && dt.Rows[0]["News_PhotoAtt"].ToString().Length > 0)
                        {
                            if (UltilFunc.CheckFrames(dt.Rows[0]["News_PhotoAtt"].ToString()) == false)
                            {

                                _Builer.Append("<div id=\"liveTV\"></div>");
                                _Builer.Append(" <script type=\"text/javascript\">jwplayer('liveTV').setup({ ");
                                _Builer.Append(" image: '" + HPCComponents.Global.TinPath + dt.Rows[0]["Images_Summary"].ToString() + "',");
                                _Builer.Append(" file: '" + HPCComponents.Global.TinPath + dt.Rows[0]["News_PhotoAtt"].ToString() + "', ");
                                _Builer.Append(" width: 665,height: 400,autostart: false,");
                                _Builer.Append(" backcolor: '#000000',frontcolor: '#ffffff',lightcolor: '#ffffff',screencolor: '#ffffff',");
                                _Builer.Append(" allowscriptaccess: true,allowfullscreen: true,controlbar: 'over',stretching:'fill'});</script>");

                                //this.litImage.Text = _Builer.ToString();
                            }
                            else
                            {
                                string _youReziHeigh = "";
                                try
                                {
                                    string _youReziWith = UltilFunc.ReplapceYoutoubeWidth(dt.Rows[0]["News_PhotoAtt"].ToString(), "665");
                                    _youReziHeigh = UltilFunc.ReplapceYoutoubeHight(_youReziWith.ToString(), "400");
                                }
                                catch
                                {
                                    _youReziHeigh = "";
                                }
                                //this.litImage.Text = _youReziHeigh.ToString();
                            }

                        }
                        else
                        {
                            if (dt.Rows[0]["Images_Summary"] != null && dt.Rows[0]["Images_Summary"].ToString().Length > 0)
                            {
                                _image = "<img style=\"width:665px;\" alt=\"" + dt.Rows[0]["News_Tittle"].ToString() + "\" title=\"" + dt.Rows[0]["News_Tittle"].ToString() + "\" src=\"" + HPCComponents.Global.TinPath + dt.Rows[0]["Images_Summary"].ToString() + "\" />";
                            }
                            //this.litImage.Text = _image.ToString();

                        }

                        if (!string.IsNullOrEmpty(lbl_index.Text))
                        {
                            gridview_index = int.Parse(lbl_index.Text);
                            DataSet ds1 = dal.GetDetailHistory(int.Parse(this.dgr_tintuc1.DataKeys[gridview_index].ToString()));

                            if (clickgrid)
                            {
                                this.litContents.Text = MergeEngineCompare(ds1.Tables[0].Rows[0]["News_Body"].ToString(),
                                    dt.Rows[0]["News_Body"].ToString());
                            }
                            else
                            {
                                this.litContents.Text = MergeEngineCompare(ds1.Tables[0].Rows[0]["News_Body"].ToString(), dt.Rows[0]["News_Body"].ToString());
                            }
                        }
                        else
                        {
                            this.litContents.Text = SearchImgTag(SearchSlideImg(dt.Rows[0]["News_Body"].ToString()));
                        }
                        if (dt.Rows[0]["News_AuthorName"] != null && dt.Rows[0]["News_AuthorName"].ToString().Length > 0)
                        {
                            this.litAuthor.Text = "<div class=\"author\">" + dt.Rows[0]["News_AuthorName"].ToString() + "</div>";
                        }
                        if (double.Parse(dt.Rows[0]["News_TienNB"].ToString()) > 0.0)
                            this.LitNhuanbut.Text = string.Format("{0:#,#}", dt.Rows[0]["News_TienNB"]).Replace(".", ",");
                        else
                            this.LitNhuanbut.Text = "";
                        //string count = string.Empty;
                        //count = this.litContents.Text;
                        //if (count != "")
                        //    this.LitCount.Text = UltilFunc.WordCount(count) + " từ ";
                        //else
                        //    this.LitCount.Text = "";
                        this.Literal_nguoinhap.Text = HPCBusinessLogic.UltilFunc.GetUserFullName(dt.Rows[0]["News_AuthorID"]);
                        this.Literal_nguoiluu.Text = HPCBusinessLogic.UltilFunc.GetUserFullName(dt.Rows[0]["News_EditorID"]);
                        this.Literal_ngayluu.Text = Convert.ToDateTime(dt.Rows[0]["News_DateEdit"]).ToString("dd/MM/yyyy HH:mm");

                        ViewState["ver"] = -1;

                    }
                    catch { ;}
                }
            }
        }
        protected void Imageback_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(lbl_index.Text))
            {
                int gridview_index = int.Parse(lbl_index.Text);
                if (gridview_index >= 1)
                {
                    int currentID = int.Parse(this.dgr_tintuc1.DataKeys[gridview_index].ToString());
                    int NewsID = int.Parse(this.dgr_tintuc1.DataKeys[gridview_index - 1].ToString());
                    LoadDetails(NewsID, true, false, gridview_index - 1, false);
                    lbl_index.Text = (gridview_index - 1).ToString();
                }
            }
        }

        protected void Imagenext_Click(object sender, ImageClickEventArgs e)
        {
            int gridview_index = 0;
            if (!string.IsNullOrEmpty(lbl_index.Text))
            {
                gridview_index = int.Parse(lbl_index.Text);
            }

            if (gridview_index < dgr_tintuc1.Items.Count - 1)
            {
                int currentID = int.Parse(this.dgr_tintuc1.DataKeys[gridview_index].ToString());
                int NewsID = int.Parse(this.dgr_tintuc1.DataKeys[gridview_index + 1].ToString());
                LoadDetails(NewsID, true, true, gridview_index + 1, false);
                lbl_index.Text = (gridview_index + 1).ToString();
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
        protected void dgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        protected void dgData_EditCommand(object source, DataGridCommandEventArgs e)
        {
            ActionHistoryDAL actionDAL = new ActionHistoryDAL();
            T_ActionHistory action = new T_ActionHistory();
            HPCBusinessLogic.DAL.T_NewsDAL tt = new HPCBusinessLogic.DAL.T_NewsDAL();
            T_News _obj_T_News = new T_News();
            T_NewsVersion _obj_T_NewsVecion = new T_NewsVersion();

            action.UserID = _user.UserID;
            action.FullName = _user.UserName;
            action.HostIP = IpAddress();
            action.DateModify = DateTime.Now;
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {

                int _ID = Convert.ToInt32(this.dgr_tintuc1.DataKeys[e.Item.ItemIndex].ToString());
                LoadDetails(_ID, true, true, e.Item.ItemIndex, true);
                lbl_index.Text = e.Item.ItemIndex.ToString();
            }
        }
        #region Return URLMap Image And Video In Content
        private string SearchImgTag(string str)
        {
            try
            {
                Regex regex = new Regex(
                    @"(?<=<img[^<]+?src=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _url = matchCollect[i].Value.Trim();
                    if (_url.Length > 0)
                    {
                        if (!_url.StartsWith("http"))
                        {
                            string _urlImg = matchCollect[i].Value.Trim();
                            string _urlImgReplate = System.Configuration.ConfigurationSettings.AppSettings["ImageServer_Replate"] + _urlImg;
                            if (!str.ToLower().Contains(_urlImgReplate.Trim().ToLower()))
                                str = System.Text.RegularExpressions.Regex.Replace(str, _urlImg, _urlImgReplate, RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            catch { }
            return str;
        }
        #endregion


        public string SearchSlideImg(string str)
        {
            System.Text.StringBuilder _strB = new System.Text.StringBuilder();
            try
            {
                Regex regex = new Regex(
                    @"(?<=<a[^<]+?href=\"")[^\""]+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _url = matchCollect[i].Value.Trim();
                    if (_url.Length > 0)
                    {
                        if (!_url.StartsWith("http"))
                        {
                            if (_url.Contains("Slide"))
                            {
                                string _urlImg = "<a href=\"" + matchCollect[i].Value + "\">";
                                string _urlImgReplate = "";//"<a href=\"" + PageBase.tinpath(matchCollect[i].Value.ToString().Replace("/uploadcmsbienphong/", ""), 670, 430, true) + "\">";//;
                                str = System.Text.RegularExpressions.Regex.Replace(str, _urlImg, _urlImgReplate, RegexOptions.IgnoreCase);
                            }
                        }
                    }
                }
            }
            catch { }

            return str;
        }
        public string MergeEngineCompare(string str1, string str2)
        {
            string Noidung = "";
            Merger merger = new Merger(str1, str2);
            Noidung = merger.merge();
            return Noidung;
        }
        public string Getstatus(object status)
        {
            string _statusOutput = "";
            try
            {
                int _status = Convert.ToInt32(status);
                switch (_status)
                {
                    case 1: _statusOutput = CommonLib.ReadXML("lblNhaptinbai"); break;
                    case 7: _statusOutput = CommonLib.ReadXML("lblTrinhbay"); break;
                    case 8: _statusOutput = CommonLib.ReadXML("lblBientap"); break;
                    case 9: _statusOutput = CommonLib.ReadXML("lblDuyettinbai"); break;
                    case 12: _statusOutput = CommonLib.ReadXML("lblTinmoi"); break;
                    case 13: _statusOutput = CommonLib.ReadXML("lblTralainguoinhaptin"); break;
                    case 72: _statusOutput = CommonLib.ReadXML("lblChotrinhbay"); break;
                    case 73: _statusOutput = CommonLib.ReadXML("lblTralaitrinhbay"); break;
                    case 82: _statusOutput = CommonLib.ReadXML("lblChobientap"); break;
                    case 83: _statusOutput = CommonLib.ReadXML("lblTralaibientap"); break;
                    case 92: _statusOutput = CommonLib.ReadXML("lblBaichoduyet"); break;
                    case 4: _statusOutput = CommonLib.ReadXML("lblTinngungdang"); break;
                    case 6: _statusOutput = CommonLib.ReadXML("lblTinxuatban"); break;
                }
            }
            catch { ;}
            return _statusOutput;
        }
    }
}
