using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using HPCBusinessLogic;
using HPCBusinessLogic.DAL;
using HPCInfo;
namespace ToasoanTTXVN.Ajax
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class GetData : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public ListData GetTemplateValue(object TempID, object Ma_Tinbai, object TempType)
        {
            var data = new ListData();
            DataSet _objSet = new DataSet();
            PreviewTemplateDAL _DAL = new PreviewTemplateDAL();
            try
            {
                _objSet = _DAL.GetOneFromT_PrevTemplateCommon(int.Parse(TempID.ToString()), int.Parse(Ma_Tinbai.ToString()), int.Parse(TempType.ToString()));
                data.TempWidth = _objSet.Tables[0].Rows[0]["TempWidth"].ToString();
                data.TempHeight = _objSet.Tables[0].Rows[0]["TempHeight"].ToString();
                data.TempFontfamily = _objSet.Tables[0].Rows[0]["Temp_FontFamily"].ToString();
                data.TempFontfamilyTitle = _objSet.Tables[0].Rows[0]["Temp_FontFamily_Title"].ToString();
                data.TempFontfamilySapo = _objSet.Tables[0].Rows[0]["Temp_FontFamily_Sapo"].ToString();
                data.TempColumns = _objSet.Tables[0].Rows[0]["TempColumn"].ToString();
                data.TempFontsize = _objSet.Tables[0].Rows[0]["Temp_FontSize"].ToString();
                data.TempFontsizeTitle = _objSet.Tables[0].Rows[0]["Temp_FontSize_Title"].ToString();
                data.TempFontsizeSapo = _objSet.Tables[0].Rows[0]["Temp_FontSize_Sapo"].ToString();
                data.TempTitleWidth = _objSet.Tables[0].Rows[0]["Temp_Title_Width"].ToString();
                data.TempLineHeightTitle = _objSet.Tables[0].Rows[0]["Temp_LineHeight_Title"].ToString();
                data.TempLineHeightSapo = _objSet.Tables[0].Rows[0]["Temp_LineHeight_Sapo"].ToString();
                data.TempLineHeightContent = _objSet.Tables[0].Rows[0]["Temp_LineHeight_Content"].ToString();
                data.TempScaleTitle = _objSet.Tables[0].Rows[0]["Temp_Scale_Title"].ToString();
                data.TempFontWeightTitle = _objSet.Tables[0].Rows[0]["Temp_FontWeight_Title"].ToString();
                data.TempSapoWidth = _objSet.Tables[0].Rows[0]["Temp_Sapo_Width"].ToString();
                data.TempSapoFontWeight = _objSet.Tables[0].Rows[0]["Temp_Sapo_FontWeight"].ToString();
                data.TempIsImage = _objSet.Tables[0].Rows[0]["Temp_IsImage"].ToString();
                if (_objSet.Tables[0].Rows[0]["Temp_Image_Width"].ToString() != "")
                    data.TempImageWidth = _objSet.Tables[0].Rows[0]["Temp_Image_Width"].ToString();
                else
                    data.TempImageWidth = "0";
                if (_objSet.Tables[0].Rows[0]["Temp_Image_Height"].ToString() != "")
                    data.TempImageHeight =_objSet.Tables[0].Rows[0]["Temp_Image_Height"].ToString();
                else
                    data.TempImageHeight = "0";
                data.TempLogo = _objSet.Tables[0].Rows[0]["TempLogo"].ToString();
                data.Ma_Tinbai = _objSet.Tables[0].Rows[0]["Ma_Tinbai"].ToString();
            }
            catch { }
            return data;
        }
        //public ListData GetTemplateValue(object TempID)
        //{
        //    var data = new ListData();
        //    Prev_Template _objSet = new Prev_Template();
        //    PreviewTemplateDAL _DAL = new PreviewTemplateDAL();
        //    try
        //    {
        //        _objSet = _DAL.GetOneFromT_PrevTemplateByID(int.Parse(TempID.ToString()));
        //        data.TempWidth = _objSet.TempWidth;
        //        data.TempHeight = _objSet.TempHeight;
        //        data.TempFontfamily = _objSet.Temp_FontFamily;
        //        data.TempFontfamilyTitle = _objSet.Temp_FontFamily_Title;
        //        data.TempFontfamilySapo = _objSet.Temp_FontFamily_Sapo;
        //        data.TempColumn = _objSet.TempColumn;
        //        data.TempFontsize = _objSet.Temp_FontSize;
        //        data.TempFontsizeTitle = _objSet.Temp_FontSize_Title;
        //        data.TempFontsizeSapo = _objSet.Temp_FontSize_Sapo;
        //        data.TempTitleWidth = _objSet.Temp_Title_Width.ToString();
        //        data.TempLineHeightTitle = _objSet.Temp_LineHeight_Title;
        //        data.TempLineHeightSapo = _objSet.Temp_LineHeight_Sapo;
        //        data.TempLineHeightContent = _objSet.Temp_LineHeight_Content;
        //        data.TempScaleTitle = _objSet.Temp_Scale_Title;
        //        data.TempFontWeightTitle = _objSet.Temp_FontWeight_Title;
        //        data.TempIsImage = _objSet.Temp_IsImage.ToString();
        //        if (_objSet.Temp_Image_Width != -1)
        //            data.TempImageWidth = _objSet.Temp_Image_Width.ToString();
        //        else
        //            data.TempImageWidth = "0";
        //        if (_objSet.Temp_Image_Height != -1)
        //            data.TempImageHeight = _objSet.Temp_Image_Height.ToString();
        //        else
        //            data.TempImageHeight = "0";
        //        data.TempLogo = _objSet.TempLogo;
        //        data.Ma_Tinbai = _objSet.Ma_Tinbai.ToString();
        //    }
        //    catch { }
        //    return data;
        //}
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string SaveTemp(object Ma_Tinbai1,object column1,object width1,object height1,object fontFamilyTitle1,object fontSizeTitle1,object ScaleTitle1,
               object fontWeightTitle1,object widthTitle1,object lineHeightTitle1,object fontFamilySapo1,object lineHeightSapo1,object fontSizeSapo1,object fontFamily1,
            object fontSize1, object lineHeightContent1, object TempIsImage1, object TempImageWidth1, object TempImageHeight1, object SapoWidth1, object SapoFontWeight1)
        {
            Prev_Template obj_Prev = new Prev_Template();
            PreviewTemplateDAL _DAL = new PreviewTemplateDAL();
            string LineHtitle = lineHeightTitle1.ToString();
            string LineHContent = lineHeightContent1.ToString();
            string LineHsapo = lineHeightSapo1.ToString();
            if (LineHtitle == "")
                LineHtitle = "Normal";
            if (LineHContent == "")
                LineHContent = "Normal";
            if (LineHsapo == "")
                LineHsapo = "Normal";
            try
            {
                obj_Prev.TempID = 0;
                obj_Prev.Ma_Tinbai = Convert.ToInt32(Ma_Tinbai1);
                obj_Prev.TempName = "Template news";
                obj_Prev.TempColumn = column1.ToString();
                obj_Prev.TempWidth = width1.ToString();
                obj_Prev.TempHeight = height1.ToString();

                obj_Prev.Temp_FontFamily_Title = fontFamilyTitle1.ToString();
                obj_Prev.Temp_FontSize_Title = fontSizeTitle1.ToString();
                obj_Prev.Temp_Scale_Title = ScaleTitle1.ToString();
                obj_Prev.Temp_FontWeight_Title = fontWeightTitle1.ToString();
                obj_Prev.Temp_Title_Width = Convert.ToInt32(widthTitle1.ToString());
                obj_Prev.Temp_LineHeight_Title = LineHtitle;

                obj_Prev.Temp_Sapo_FontWeight = SapoFontWeight1.ToString();
                obj_Prev.Temp_Sapo_Width = Convert.ToInt32(SapoWidth1.ToString());
                obj_Prev.Temp_FontFamily_Sapo = fontFamilySapo1.ToString();
                obj_Prev.Temp_FontSize_Sapo = fontSizeSapo1.ToString();
                obj_Prev.Temp_LineHeight_Sapo = LineHsapo;

                obj_Prev.Temp_FontFamily = fontFamily1.ToString();
                obj_Prev.Temp_FontSize = fontSize1.ToString();
                obj_Prev.Temp_LineHeight_Content = LineHContent;
                obj_Prev.Temp_IsImage =bool.Parse(TempIsImage1.ToString());
                obj_Prev.Temp_Image_Width = Convert.ToInt32(TempImageWidth1.ToString());
                obj_Prev.Temp_Image_Height = Convert.ToInt32(TempImageHeight1.ToString());

                obj_Prev.TempType = 1;
                _DAL.InsertT_PrevTemplateForNews(obj_Prev);
            }
            catch { return "0"; }
            return "1";
        }
    }
    public class ListData
    {
        public string Ma_Tinbai { get; set; }
        public string TempWidth { get; set; }
        public string TempHeight { get; set; }
        public string TempColumns { get; set; }
        public string TempFontfamily { get; set; }
        public string TempFontfamilyTitle { get; set; }
        public string TempFontfamilySapo { get; set; }
        public string TempFontsize { get; set; }
        public string TempFontsizeTitle { get; set; }
        public string TempFontsizeSapo { get; set; }
        public string TempTitleWidth { get; set; }
        public string TempLineHeightTitle { get; set; }
        public string TempLineHeightSapo { get; set; }
        public string TempLineHeightContent { get; set; }
        public string TempScaleTitle { get; set; }
        public string TempFontWeightTitle { get; set; }
        public string TempIsImage { get; set; }
        public string TempImageWidth { get; set; }
        public string TempImageHeight { get; set; }
        public string TempLogo { get; set; }
        public string TempSapoWidth { get; set; }
        public string TempSapoFontWeight { get; set; }
    }
}
