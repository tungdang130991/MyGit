using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Text;
using HtmlAgilityPack;
public partial class word_savefile : System.Web.UI.Page
{
    TinBaiAnhDAL _DAL = new TinBaiAnhDAL();
    HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
    T_Users _user;
    string Filedoc = string.Empty;
    string Pathfolder = string.Empty;
    double matinbai = 0;
    Aceoffix.FileRequest freq = new Aceoffix.FileRequest();
    UltilFunc ulti = new UltilFunc();
    HPCBusinessLogic.DAL.TinBaiDAL Daltinbai = new HPCBusinessLogic.DAL.TinBaiDAL();
    T_TinBai obj = new T_TinBai();
    protected void Page_Load(object sender, EventArgs e)
    {
        _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
        if (Request["matinbai"] != null)
            matinbai = double.Parse(Request["matinbai"].ToString());

        obj = SetItem();
        matinbai = Daltinbai.InsertT_Tinbai_WordOnline(obj);
        Session["matinbai"] = matinbai;
        Pathfolder = Global.Pathfiledoc;
        if (freq.FileExtName == ".htm")
            Filedoc = _user.UserID.ToString() + _user.UserName + matinbai.ToString() + "_" + Request["MaDoiTuong"].ToString() + ".htm";
        else
            Filedoc = _user.UserID.ToString() + _user.UserName + matinbai.ToString() + "_" + Request["MaDoiTuong"].ToString() + ".doc";
        CreateFolderByUserName(Pathfolder);
        Filedoc = Server.MapPath("/" + Pathfolder + Filedoc);
        freq.SaveToFile(Filedoc);
        freq.Close();
        string filehtm = _user.UserID.ToString() + _user.UserName + matinbai.ToString() + "_" + Request["MaDoiTuong"].ToString() + ".htm";
        string pathfilehtm = Server.MapPath("/" + Pathfolder + filehtm);
        Daltinbai.sp_updatenoidungtin(matinbai, ReadHtmlFile(pathfilehtm));

    }
    private T_TinBai SetItem()
    {

        if (Request["matinbai"] != null)
            obj.Ma_Tinbai = double.Parse(Request["matinbai"].ToString());
        else
            if (Session["matinbai"] != null)
                obj.Ma_Tinbai = double.Parse(Session["matinbai"].ToString());
            else
                obj.Ma_Tinbai = 0;
        obj.Tieude = "Tittle empty";
        obj.Noidung = freq.DocumentText;
        obj.Sotu = CountWords(freq.DocumentText);
        obj.Ma_Nguoitao = _user.UserID;
        obj.Trangthai = 2;
        obj.Bizhub = false;
        obj.VietNamNews = false;
        int startchar = Global.Pathfiledoc.Substring(1, Global.Pathfiledoc.Length - 1).IndexOf("/");
        startchar += 1;
        string _Filedoc = Global.Pathfiledoc.Substring(startchar, Global.Pathfiledoc.Length - startchar);
        if (Page.Request["matinbai"] != null)
            obj.PathFileDocuments = _Filedoc + _user.UserID.ToString() + _user.UserName + Request["matinbai"].ToString() + "_" + Request["MaDoiTuong"].ToString() + ".doc";
        if (Session["matinbai"] != null)
            obj.PathFileDocuments = _Filedoc + _user.UserID.ToString() + _user.UserName + Session["matinbai"].ToString() + "_" + Request["MaDoiTuong"].ToString() + ".doc";
        return obj;
    }
    private void CreateFolderByUserName(string FolderName)
    {
        string strRootPath = "";
        strRootPath = HttpContext.Current.Server.MapPath("/" + FolderName);
        if (Directory.Exists(strRootPath) == false)
            Directory.CreateDirectory(strRootPath);
    }
    private int SplitWords(string fbStatus)
    {
        return fbStatus.Split(new char[] { ' ' },
        StringSplitOptions.RemoveEmptyEntries).Length;
    }
    public int CountWords(string test)
    {
        int count = 0;
        bool wasInWord = false;
        bool inWord = false;

        for (int i = 0; i < test.Length; i++)
        {
            if (inWord)
            {
                wasInWord = true;
            }

            if (Char.IsWhiteSpace(test[i]))
            {
                if (wasInWord)
                {
                    count++;
                    wasInWord = false;
                }
                inWord = false;
            }
            else
            {
                inWord = true;
            }
        }

        // Check to see if we got out with seeing a word
        if (wasInWord)
        {
            count++;
        }

        return count;
    }

    public string ReadHtmlFile(string htmlFileNameWithPath)
    {

        if (!File.Exists(htmlFileNameWithPath)) return "";

        string PathAfterDownload = "";
        HtmlDocument doc = new HtmlDocument();
        doc.Load(htmlFileNameWithPath);

        foreach (HtmlNode item in doc.DocumentNode.SelectNodes("//html/body/div"))//select only those img that have a src attribute..ahh not required to do [@src] i guess
        {
            PathAfterDownload = item.InnerHtml.ToString();
        }

        return PathAfterDownload;

    }
}
