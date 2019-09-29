using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;

namespace ToasoanTTXVN.Multimedia
{
    public partial class ViewVideoPath : System.Web.UI.Page
    {
        public string _urlFile = "";
        public string _urlImg = "";
        public int _cao = 300, _rong = 300;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Page.Request["url"] != null && Page.Request["imgvod"] != null && Convert.ToString(Page.Request["url"]) != "")
                {
                    string _url = "";
                    if (Page.Request["imgvod"].ToString().Length > 0)
                        _urlImg = Page.Request["imgvod"].ToString();
                    try
                    {
                        _url = System.IO.Path.GetExtension(Page.Request["url"].Split('/').GetValue(Page.Request["url"].Split('/').Length - 1).ToString().Trim());
                        if (_url.ToLower() == ".jpg" || _url.ToLower() == ".png" || _url.ToLower() == ".gif" || _url.ToLower() == ".jpeg" || _url.ToLower() == ".bmp")
                        {
                            this.checkDisplay.Visible = false;
                            this.checkDisplayVideo.Visible = false;
                            this.litContent.Text = "<img  src=\"" + HPCComponents.Global.TinPath + Page.Request["url"] + "\" alt=\"View\" />";
                        }
                        else if (_url.ToLower() == ".flv" || _url.ToLower() == ".wmv" || _url.ToLower() == ".mp4")
                        {
                            this.checkDisplay.Visible = false;
                            this.checkDisplayVideo.Visible = true;
                            this.litContent.Text = "";
                        }
                        else
                        {
                            _urlFile = HPCComponents.Global.TinPath + Page.Request["url"];
                            this.checkDisplay.Visible = true;
                            this.checkDisplayVideo.Visible = false;
                            this.litContent.Text = "";
                        }
                    }
                    catch
                    {
                        this.checkDisplay.Visible = false;
                        this.checkDisplayVideo.Visible = false;
                        litContent.Text = Page.Request["url"].ToString();
                    }

                }

            }
        }
        public string imgPath()
        {
            string _return = "";
            if (Page.Request["imgvod"] != null)
                _return = HPCComponents.Global.TinPath + Request["imgvod"].ToString();
            return _return;
        }
        public string videoPath()
        {
            string _return = "";
            if (Page.Request["url"] != null)
                _return = HPCComponents.Global.TinPath + Request["url"].ToString();
            return _return;
        }
    }
}
