using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCInfo;
using System.Data;
using HPCComponents;
using System.Text.RegularExpressions;
namespace ToasoanTTXVN.Quytrinh
{
    public partial class PreviewOLD : System.Web.UI.Page
    {
        protected string Tieude = string.Empty;
        protected string Noidung = string.Empty;
        protected string ImageTB = string.Empty;
        protected string ChuthichAnh = string.Empty;
        protected string Sapo = string.Empty;
        protected int IDNews = 0;
        HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
        HPCBusinessLogic.AnhDAL DalAnh = new HPCBusinessLogic.AnhDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ID"] != null)
                IDNews = int.Parse(Request["ID"].ToString());
            GetDataNews(IDNews);
            BindImage(IDNews);
            BindListTemplate();
        }
        private void GetDataNews(int IDQ)
        {
            T_TinBai _objSet = new T_TinBai();
            _objSet = Daltinbai.load_T_news(IDQ);
            Tieude = _objSet.Tieude;
            Noidung = CleanStyle(_objSet.Noidung);
            Sapo = CleanStyle(_objSet.Tomtat);
        }
        private void BindListTemplate()
        {
            PreviewTemplateDAL _PrevDAL = new PreviewTemplateDAL();
            DataSet _ds = null;
            try
            {
                _ds = _PrevDAL.BindList_PrevTemplate(0);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        rptTemplate.DataSource = _ds.Tables[0].DefaultView;
                        rptTemplate.DataBind();
                    }
                }
            }
            catch { }
        }
        private string CleanStyle(string str)
        {
            System.Text.StringBuilder _strB = new System.Text.StringBuilder();
            try
            {
                Regex regex = new Regex(
                    @"(?<=style=')[^']+  ",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );
                MatchCollection matchCollect = regex.Matches(str);
                for (int i = 0; i < matchCollect.Count; i++)
                {
                    string _strFind = matchCollect[i].Value.Trim();
                    if (_strFind.Length > 0)
                    {
                        str = System.Text.RegularExpressions.Regex.Replace(str, _strFind, " ", RegexOptions.IgnoreCase);
                    }
                }
            }
            catch { }

            return str;
        }
        private void BindImage(int IDTB)
        {
            DataSet _ds = null;
            try
            {
                _ds = DalAnh.GetListImageByTinbai(IDTB);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        if (_ds.Tables[0].Rows[0]["Duongdan_Anh"] != null)
                            ImageTB = Global.TinPathBDT + _ds.Tables[0].Rows[0]["Duongdan_Anh"].ToString();
                        else
                            ImageTB = "../Dungchung/Images/icon_no_images4.jpg";
                        ChuthichAnh = _ds.Tables[0].Rows[0]["ChuThich"].ToString();
                        lblYesImage.InnerText = "( Bài có ảnh )";
                    }
                    else
                        ImageTB = "../Dungchung/Images/icon_no_images4.jpg";
                }
                else
                {
                    ImageTB = "../Dungchung/Images/icon_no_images4.jpg";
                }
            }
            catch { }
        }
    }
}
