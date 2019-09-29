

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Web.SessionState;

namespace XemBao
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string sCaptchaText = string.Empty;
            if (context.Session["captcha"] != null)
            {
                sCaptchaText = context.Session["captcha"].ToString();

                CaptchaImage ci = new CaptchaImage(sCaptchaText, 200, 50, "Century Schoolbook");
                MemoryStream oMemoryStream = new MemoryStream();
                ci.Image.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] oBytes = oMemoryStream.GetBuffer();

                oMemoryStream.Close();

                context.Response.BinaryWrite(oBytes);
                context.Response.End();
            }
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