using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ToasoanTTXVN
{
    public class HPC_ConvertUni2Vni
    {     
        private static string NCR2Unicode(string str)
        {
            int p;  
            string mStr= "";
            for (int i=0;i<str.Length;i++ )
            {
                if (str[i].ToString() == "&")
                {
                    if (str[i + 1].ToString() == "#")
                    {
                        p = str.IndexOf(";", i + 2);
                        if (p != -1)
                        {
                            if (p - i < 7)
                            {
                                mStr += Char.ConvertFromUtf32(int.Parse(str.Substring(i + 2, p - i - 2)));
                                i = p;
                            }
                        }
                    }
                    else
                    {
                        p = str.IndexOf(";", i + 1);
                        if (p != -1)
                        {
                            switch (str.Substring(i + 1, p - i - 1))
                            {
                                case "amp":
                                    mStr += "&";
                                    break;
                                case "quot":
                                    mStr += "\"";
                                    break;
                                case "lt":
                                    mStr += "<";
                                    break;
                                case "gt":
                                    mStr += ">";
                                    break;
                            }
                        }
                    }
                }   
                else
                {
                    mStr += str[i];
                }                
            }
            mStr = mStr.Replace("nbsp;", " ");
            mStr = mStr.Replace("quot;", "");
            return mStr;
        }

        private static string ConvertUni2BK2(string strInputext)
        {
            string strOutput = "";
            if (strInputext == null || String.Empty == strInputext)
            {
                strOutput= "";
            }

            string strUniCode ="ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠạẢảẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẸẹẺẻẼẽẾếỀềỂểỄễỆệỈỉỊịỌọỎỏỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợỤụỦủỨứỪừỬửỮữỰựỲỳỴỵỶỷỸỹ";                               
            string[] Vni = new string[]
                               {
                                   "AÂ","AÁ","Ê","AÄ","EÂ","EÁ","Ï","Ò","Ñ","OÂ","OÁ","Ö","OÄ","UÂ",
                                    "UÁ","YÁ","aâ","aá","ê","aä","eâ","eá","ï","ò","ñ","oâ","oá","ö",
                                    "oä","uâ","uá","yá","Ù","ù","À","à","Ô","ô","UÄ","uä","Ú","ú",
                                    "Û","û","AÅ","aå","AÃ","aã","ÊË","êë","ÊÌ","êì","ÊÍ","êí","ÊÎ","êî",
                                    "ÊÅ","êå","ÙÆ","ùæ","ÙÇ","ùç","ÙÈ","ùè","ÙÉ","ùé","ÙÅ","ùå","EÅ","eå",
                                    "EÃ","eã","EÄ","eä","ÏË","ïë","ÏÌ","ïì","ÏÍ","ïí","ÏÎ","ïî","ÏÅ","ïå",
                                    "Ó","ó","Õ","õ","OÅ","oå","OÃ","oã","ÖË","öë","ÖÌ","öì","ÖÍ","öí",
                                    "ÖÎ","öî","ÖÅ","öå","ÚÁ","úá","ÚÂ","úâ","ÚÃ","úã","ÚÄ","úä","ÚÅ","úå",
                                    "UÅ","uå","UÃ","uã","ÛÁ","ûá","ÛÂ","ûâ","ÛÃ","ûã","ÛÄ","ûä","ÛÅ","ûå",
                                    "YÂ","yâ","YÅ","yå","YÃ","yã","YÄ","yä"

                               };
            string strInputTemp = NCR2Unicode(strInputext);
            int posChar;
            for (int i = 0; i < strInputTemp.Length; i++)
            {                
                posChar = strUniCode.IndexOf(strInputTemp[i]);
                if (posChar != -1)
                {
                    strOutput += Vni[posChar];
                }
                else
                    strOutput += strInputTemp[i].ToString();
            }
            return strOutput;
        }

        private static string ConvertUni2Vni(string strInputext)
        {
            string strOutput = "";
            if (strInputext == null || String.Empty == strInputext)
            {
                strOutput = "";
            }

            string strUniCode = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠạẢảẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẸẹẺẻẼẽẾếỀềỂểỄễỆệỈỉỊịỌọỎỏỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợỤụỦủỨứỪừỬửỮữỰựỲỳỴỵỶỷỸỹ";
            string[] Vni = new string[]
                               {
                                   "AØ","AÙ","AÂ","AÕ","EØ","EÙ","EÂ","Ì","Í","OØ","OÙ","OÂ","OÕ","UØ",
                                    "UÙ","YÙ","aø","aù","aâ","aõ","eø","eù","eâ","ì","í","oø","où","oâ",
                                    "oõ","uø","uù","yù","AÊ","aê","Ñ","ñ","Ó","ó","UÕ","uõ","Ô","ô",
                                    "Ö","ö","AÏ","aï","AÛ","aû","AÁ","aá","AÀ","aà","AÅ","aå","AÃ","aã",
                                    "AÄ","aä","AÉ","aé","AÈ","aè","AÚ","aú","AÜ","aü","AË","aë","EÏ","eï",
                                    "EÛ","eû","EÕ","eõ","EÁ","eá","EÀ","eà","EÅ","eå","EÃ","eã","EÄ","eä",
                                    "Æ","æ","Ò","ò","OÏ","oï","OÛ","oû","OÁ","oá","OÀ","oà","OÅ","oå",
                                    "OÃ","oã","OÄ","oä","ÔÙ","ôù","ÔØ","ôø","ÔÛ","ôû","ÔÕ","ôõ","ÔÏ","ôï",
                                    "UÏ","uï","UÛ","uû","ÖÙ","öù","ÖØ","öø","ÖÛ","öû","ÖÕ","öõ","ÖÏ","öï",
                                    "YØ","yø","Î","î","YÛ","yû","YÕ","yõ"
                               };
            string strInputTemp = NCR2Unicode(strInputext);
            int posChar;
            for (int i = 0; i < strInputTemp.Length; i++)
            {
                posChar = strUniCode.IndexOf(strInputTemp[i]);
                if (posChar != -1)
                {
                    strOutput += Vni[posChar];
                }
                else
                    strOutput += strInputTemp[i].ToString();
            }
            return strOutput;
        }

        public static string ConvertUni2Vietnam(string textInput, FontDest fontDess)
        {
            string outPutText = "";
            switch (fontDess)
            {
                case  FontDest.vni:
                    outPutText= ConvertUni2Vni(textInput);
                    break;
                case FontDest.bk2:
                    outPutText= ConvertUni2BK2(textInput);
                    break;
            }
            return outPutText;
        }
        public enum FontDest
        {
            vni,
            bk2
        }

    }
}
