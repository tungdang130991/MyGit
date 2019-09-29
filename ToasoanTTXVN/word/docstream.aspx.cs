using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using HPCServerDataAccess;
public partial class word_docstream : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strID = Request.QueryString["id"];
        if (strID == null)
        {
            Response.Clear();
            Response.End();
            return;
        }

        Response.Clear(); // Required
        Response.ContentType = "application/msword"; // Required
        Response.AddHeader("Content-Disposition", "attachment; filename=down.doc"); // Required

        string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectSQLQDND"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstring);
        conn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        //convert(Binary(8000),convert(nvarchar(max),Comments)) as Comments
        cmd.CommandText = "SELECT FileBinary from t_news where ID = @id";
        SqlParameter spID = new SqlParameter("@id", SqlDbType.Int);
        spID.Value = strID;
        cmd.Parameters.Add(spID);
        SqlDataReader dr = cmd.ExecuteReader();

        int iFileSize = 0;
        if (dr.Read())
        {
            int FileCol = 0; // the column # of the BLOB field
            Byte[] b = new Byte[(dr.GetBytes(FileCol, 0, null, 0, int.MaxValue))];
            dr.GetBytes(FileCol, 0, b, 0, b.Length);
            iFileSize = b.Length;
            System.IO.Stream fs = Response.OutputStream;
            fs.Write(b, 0, b.Length);
            fs.Close();
            fs.Dispose();
        }
        dr.Close();
        conn.Close();
        Response.AppendHeader("Content-Length", iFileSize.ToString()); // Required
        Response.End(); // Required
    }
}
