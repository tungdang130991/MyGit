<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using  System.Data.SqlClient;

public class Handler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string prefixText = context.Request.QueryString["q"];
        HPCServerDataAccess.SqlService _sqlService = new HPCServerDataAccess.SqlService();
        using (SqlConnection conn = new SqlConnection())
        {
            
            conn.ConnectionString = _sqlService.ConnectionString.ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select KeyWord from T_KeyWords where " +
                "KeyWord like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        sb.Append(sdr["KeyWord"])
                            .Append(Environment.NewLine);
                    }
                }
                conn.Close();
                context.Response.Write(sb.ToString());
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}