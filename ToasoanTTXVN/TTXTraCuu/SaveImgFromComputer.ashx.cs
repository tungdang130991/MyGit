using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using HPCBusinessLogic;

namespace ToasoanTTXVN.TTXTraCuu
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SaveImgFromComputer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            int userId = 0;
            if (context.Request["userID"] != null)
            {
                userId = Convert.ToInt32(context.Request["userID"]);
            }
            string userName = string.Empty;
            if (context.Request["userName"] != null)
            {
                userName = context.Request["userName"].ToString();
            }
            int imgID = 0;
            if (context.Request["imgID"] != null)
                imgID = Convert.ToInt32(context.Request["imgID"]);
            string Ticket = string.Empty;
            if (context.Request["Ticket"] != null)
                Ticket = context.Request["Ticket"].ToString(); ;
            string ProductID = string.Empty;
            if (context.Request["ProductID"] != null)
            {
                ProductID = context.Request["ProductID"].ToString();
            }
            try
            {
                if (imgID > 0)
                {
                    Image objPhoto = GetContentImageByID(Ticket, ProductID, imgID);
                    string fileName = objPhoto.URLImg;
                    byte[] bytes = Convert.FromBase64String(objPhoto.dataBinary);
                    string filename = Path.GetFileName(fileName);
                    if (filename.Length == 0)
                    {
                        filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Millisecond.ToString() + ".jpg";
                    }
                    //Insert Log
                    UltilFunc.WriteLogHistorySource(userId, userName, System.Uri.UnescapeDataString(objPhoto.Title), objPhoto.CategoryName, 0, "Tra cứu ảnh : Lấy ảnh về máy", 3);
                    //WriteLogHistory2Database.WriteHistory2Database(userId, userName, System.Uri.UnescapeDataString(objPhoto.Title), 0, "Tra cứu ảnh : Lấy ảnh về máy", 0, 3);
                    context.Response.ContentType = "image/jpeg";
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                    context.Response.BinaryWrite(bytes);
                    objPhoto = null;
                    context.Response.StatusCode = 200;
                    //context.Response.End();
                }
            }
            catch //(Exception ex)
            {
            }
        }
        public Image GetContentImageByID(object Ticket, object ProductID, object ID)
        {
            WS_TTX.WebService1SoapClient _ws;
            Image objImage = new Image();
            _ws = new WS_TTX.WebService1SoapClient();
            WS_TTX.ObjectImage Info = _ws.getContentImageByID(Ticket.ToString(), int.Parse(ProductID.ToString()), int.Parse(ID.ToString()));
            if (Info != null)
            {
                objImage.ID = Info.ID;
                objImage.Title = System.Uri.EscapeDataString(Info.Title);
                objImage.Summary = "";// System.Uri.EscapeDataString(ConvertWordToHTML(Info.Summary));
                objImage.dataBinary = Info.dataBinary;
                objImage.Author = Info.Author;
                objImage.ProductID = Info.ProductID;
                objImage.ProductName = Info.ProductName;
                objImage.CategoryID = Info.CategoryID;
                objImage.CategoryName = Info.CategoryName;
                objImage.DateCreate = Convert.ToDateTime(Info.DateCreate.ToString()).ToString("dd/MM/yyyy HH:mm:ss").ToString();
                objImage.URLImg = Info.URLImg;
            }
            return objImage;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
