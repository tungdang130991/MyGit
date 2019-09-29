//
// Warning: This code is reserved by Aceoffix and can not be modified.
//
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace aceoffix_runtime
{
    public partial class server : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strFilePath = Request.QueryString["FilePath"];
            string strContentType = Request.QueryString["ContentType"];
            string strFileName = Request.QueryString["FileName"];
            if (((strFilePath != null) && (strFilePath.Length > 0)) && ((strContentType != null) && (strContentType.Length > 0)) && ((strFileName != null) && (strFileName.Length > 0)))
            {
                Response.ContentType = strContentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
                strFilePath = Server.MapPath(strFilePath);
                if (!File.Exists(strFilePath))
                    throw new Exception("The file \"" + strFilePath + "\" is missing.");

                FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                int iLen = (int)fs.Length;
                byte[] b = new byte[iLen];
                fs.Read(b, 0, iLen);
                fs.Close();
                Response.Clear();
                Response.OutputStream.Write(b, 0, iLen);
                Response.End();
                return;
            }

            Aceoffix.Xafnetrt.Server rtServer;
            try
            {
                rtServer = new Aceoffix.Xafnetrt.Server();
            }
            catch
            {
                LabelReg.Text = "<span style=\"color:Red;\">You must install Aceoffix (setup-server.exe) on the web server before use it.</span>";
                return;
            }

            //For security reasons, you can view the system information of Aceoffix only with a local access.
            string ServerIP = Request.ServerVariables["HTTP_HOST"].ToLower();
            if ((ServerIP.StartsWith("localhost")) || (ServerIP.StartsWith("127.0.0.1")))
            {
                if (rtServer.SerialNumber != "")
                {
                    LabelReg.Text = "LicenseKey: " + rtServer.SerialNumber + "<br>" + "Version: " + rtServer.VersionInfo + "<br>" + "User: " + rtServer.Company;
                    if (rtServer.TrialEndTime != "")
                        LabelReg.Text = LabelReg.Text + "<br>" + "This product is a <span style=\"color:Red;\">trial edition</span> and it will expire on " + rtServer.TrialEndTime + ".";
                }
                else
                {
                    LabelReg.Text = "<span style=\"color:Red;\">The product is not registered.</span>";
                }
                LabelLog.Text = rtServer.GetSysLog();
            }
            else
            {
                Response.Write("For security reasons, you can view the system information of Aceoffix only with a local access.");
                Response.End();
            }
        }
    }
}
