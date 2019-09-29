using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ToasoanTTXVN.BaoCao
{
    public class GridViewToExcel
    {
        /// <summary>
        /// Export grid to excel
        /// </summary>
        /// <param name="fileName">tên file</param>
        /// <param name="gv">grid view</param>
        public static void Export(string fileName, GridView gv)
        {
            GridViewToExcel.Export(fileName, gv, "",
                "", "");
        }

        /// <summary>
        /// Export grid to excel with header text
        /// </summary>
        /// <param name="fileName">tên file</param>
        /// <param name="gv">grid view</param>
        /// <param name="headerText1">Đài truyền hình VN</param>
        /// <param name="headerText2">tiêu đề</param>
        /// <param name="headerText3">ghi chú</param>
        public static void Export(string fileName, GridView gv, string headerText1, string headerText2, string headerText3)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileName);
            #region write header
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"");
            sb.AppendLine("http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" >");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("table thead tr th {background-color:White;font-weight:bold;font-size:14px;text-align:left;border:none}");
            sb.AppendLine("table thead tr td {font-size:13px;text-align:center;border:1px}");
            sb.AppendLine("table tbody tr th {background-color:White;font-size:14px;text-align:center;border:none}");
            sb.AppendLine("table tbody tr td {font-size:13px;text-align:center;border:none}");
            sb.AppendLine("table tr th {background-color: #98FB98;font-size:14px;}");
            sb.AppendLine("table tr td {color:black;}");
            sb.AppendLine("td, th {border:.2pt solid windowtext;}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head><body>");
            HttpContext.Current.Response.Write(sb.ToString());
            #endregion
            #region write header text
            string headerSpan = "" + gv.HeaderRow.Cells.Count;
            //if (Convert.ToInt32(headerSpan) > 0)
            //{
            if (!headerText1.Equals("") || !headerText2.Equals("") || !headerText3.Equals(""))
            {
                sb = new StringBuilder();
                sb.AppendLine("<table>");
                sb.AppendLine("<thead>");
                //sb.AppendLine("<tr><td colspan='" + headerSpan + "'></td></tr>");
                //sb.AppendLine("<tr><th colspan='" + headerSpan + "'></th></tr>");
                sb.AppendLine("<tr><th colspan='" + headerSpan + "'></th></tr>");
                if (!headerText1.Trim().Equals(""))
                {
                    sb.AppendLine("<tr><th colspan='" + headerSpan + "'>" + headerText1 + "</th></tr>");
                }
                sb.AppendLine("</thead>");
                sb.AppendLine("<tbody>");
                if (!headerText2.Trim().Equals(""))
                {
                    sb.AppendLine("<tr><th colspan='" + headerSpan + "'>" + headerText2 + "</th></tr>");
                }
                if (!headerText3.Trim().Equals(""))
                {
                    sb.AppendLine("<tr><td colspan='" + headerSpan + "'>" + headerText3 + "</td></tr>");
                }
                sb.AppendLine("<tr><td colspan='" + headerSpan + "'></td></tr>");
                sb.AppendLine("</tbody>");
                sb.AppendLine("</table>");
                HttpContext.Current.Response.Write(sb.ToString());
            }
            //}
            #endregion
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        GridViewToExcel.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewToExcel.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewToExcel.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static void ExportNoHeader(string fileName, GridView gv)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + fileName);

            #region write header
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"");
            sb.AppendLine("http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" >");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("table thead tr th {background-color:White;font-weight:bold;font-size:14px;text-align:center;border:1px}");
            sb.AppendLine("table thead tr td {font-size:13px;text-align:center;border:1px}");
            sb.AppendLine("table tbody tr th {background-color:White;font-size:14px;text-align:center;border:1px}");
            sb.AppendLine("table tbody tr td {font-size:13px;text-align:center;border:1px}");
            sb.AppendLine("table tr th {background-color: #98FB98;font-size:14px;}");
            sb.AppendLine("table tr td {color:black;}");
            sb.AppendLine("td, th {border:.5pt solid windowtext;}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head><body>");
            HttpContext.Current.Response.Write(sb.ToString());
            #endregion

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        GridViewToExcel.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewToExcel.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewToExcel.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        public static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "Ðúng" : "Sai"));
                }

                if (current.HasControls())
                {
                    GridViewToExcel.PrepareControlForExport(current);
                }
            }
        }
    }
}
