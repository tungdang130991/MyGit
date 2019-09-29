using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using HPCServerDataAccess;


namespace ToasoanTTXVN.QL_SanXuat
{
    public partial class BindLayoutCV : System.Web.UI.Page
    {
        private int mabao = 0;
        private int trang = 0;
        StringBuilder bd = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            DisableClientCaching();
            if (!IsPostBack)
            {
                try
                {
                    if (Request["mabao"] != null)
                        mabao = Convert.ToInt32(Request["mabao"].ToString());
                    if (Request["trangbao"] != null)
                        trang = Convert.ToInt32(Request["trangbao"].ToString());
                    BinData(mabao, trang);
                }
                catch (Exception ex) { throw ex; }
            }
        }
        private void DisableClientCaching()
        {
            // Do any of these result in META tags e.g. <META HTTP-EQUIV="Expire" CONTENT="-1">
            // HTTP Headers or both?

            // Does this only work for IE?
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Is this required for FireFox? Would be good to do this without magic strings.
            // Won't it overwrite the previous setting
            Response.Headers.Add("Cache-Control", "no-cache, no-store");

            // Why is it necessary to explicitly call SetExpires. Presume it is still better than calling
            // Response.Headers.Add( directly
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        }
        private void BinData(int masobao, int trang)
        {
            try
            {
                HPCBusinessLogic.VitriTinbaiDAL _objVitritb = new HPCBusinessLogic.VitriTinbaiDAL();
                DataSet _ds;
                _ds = _objVitritb.T_Vitri_Tinbai_SelectBySoBaoAndPageNo(masobao, trang);
                DataTable tb = _ds.Tables[0];
                if (tb.Rows.Count > 0)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        DataRow mrow = tb.Rows[i];
                        if (mrow["Ma_Tinbai"].ToString() == "0" && mrow["Ma_QuangCao"].ToString() == "0")
                        {
                            bd.Append("<div title=" + mrow["Ma_Vitri"] + " id=" + mrow["Ma_Vitri"] + " class=\"drsElement\" style='left: " + mrow["Trai"] + "px; top: " + mrow["Tren"] + "px; cursor: move; width: " + mrow["Rong"] + "px; height: " + mrow["Dai"] + "px;'>");
                            bd.Append("<div class=\"drsMoveHandle\" id=\"Move" + mrow["Ma_Vitri"] + "\"></div>");
                            bd.Append("<input type=\"button\" id=\"btnremoveItem\" class=\"btnremoveChild\" value=\"Xóa\" onclick=\"return removeDivChildAdv('" + mrow["Ma_Vitri"] + "');\"/>");
                            bd.Append("<input type=\"button\" id=\"btnPhanviec\" class=\"btnPhanviec\" value=\"Phân việc\" onclick=\"PhanViec(this,'" + mrow["Ma_Vitri"] + "');\"/>");
                            bd.Append("<div class=\"divtextclass\" id=\"divtext" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<div style=\"display:none\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Congviec"] + "\" id=\"txtCV" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Tinbai"] + "\" id=\"txtTB" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input id=\"txtStatusTB" + mrow["Ma_Vitri"] + "\" type=\"text\" value=\"" + mrow["Doituong_DangXuly"] + "\"/>");
                            bd.Append("</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextCV(mrow["Ma_Congviec"], mrow["Tencongviec"]) + "</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTB(mrow["Ma_Tinbai"], mrow["Tieude"]) + "</div>");
                            bd.Append("<div class=\"divtextclassDes\">" + CheckBindTextTBSoTu(mrow["Ma_Tinbai"], mrow["Sotu"]) + "</div>");
                            bd.Append("</div>");
                            bd.Append("</div>");
                        }
                        else if (mrow["Ma_Tinbai"].ToString() != "0" && mrow["Ma_Congviec"].ToString() != "0")
                        {
                            bd.Append("<div title=" + mrow["Ma_Vitri"] + " id=" + mrow["Ma_Vitri"] + " class=\"drsElement\" style='left: " + mrow["Trai"] + "px; top: " + mrow["Tren"] + "px; cursor: move; width: " + mrow["Rong"] + "px; height: " + mrow["Dai"] + "px;'>");
                            bd.Append("<div class=\"drsMoveHandle\" id=\"Move" + mrow["Ma_Vitri"] + "\"></div>");
                            bd.Append("<input type=\"button\" id=\"btnremoveItem\" class=\"btnremoveChild\" value=\"Xóa\" onclick=\"return removeDivChildAdv('" + mrow["Ma_Vitri"] + "');\"/>");
                            bd.Append("<input type=\"button\" id=\"btnPhanviec\" class=\"btnPhanviec\" value=\"Phân việc\" onclick=\"PhanViec(this,'" + mrow["Ma_Vitri"] + "');\"/>");
                            bd.Append("<div class=\"divtextclass\" id=\"divtext" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<div style=\"display:none\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Congviec"] + "\" id=\"txtCV" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Tinbai"] + "\" id=\"txtTB" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input id=\"txtStatusTB" + mrow["Ma_Vitri"] + "\" type=\"text\" value=\"" + mrow["Doituong_DangXuly"] + "\"/>");
                            bd.Append("</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextCV(mrow["Ma_Congviec"], mrow["Tencongviec"]) + "</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTB(mrow["Ma_Tinbai"], mrow["Tieude"]) + "</div>");
                            bd.Append("<div class=\"divtextclassDes\">" + CheckBindTextTBSoTu(mrow["Ma_Tinbai"], mrow["Sotu"]) + "</div>");
                            bd.Append("</div>");
                            bd.Append("</div>");
                        }
                        else if (mrow["Ma_Tinbai"].ToString() != "0" && mrow["Ma_Congviec"].ToString() == "0")
                        {
                            bd.Append("<div title=" + mrow["Ma_Vitri"] + " id=" + mrow["Ma_Vitri"] + " class=\"drsElementStatic\" style='left: " + mrow["Trai"] + "px; top: " + mrow["Tren"] + "px; cursor: move; width: " + mrow["Rong"] + "px; height: " + mrow["Dai"] + "px;'>");
                            bd.Append("<div class=\"drsMoveHandleStatic\" id=\"Move" + mrow["Ma_Vitri"] + "\"></div>");
                            bd.Append("<div class=\"divtextclass\" id=\"divtext" + mrow["Ma_Vitri"] + "\">");

                            bd.Append("<div style=\"display:none\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_QuangCao"] + "\" id=\"txtAdv" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Tinbai"] + "\" id=\"txtTB" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Doituong_DangXuly"] + "\" id=\"txtStatusTB" + mrow["Ma_Vitri"] + "\"  />");
                            bd.Append("</div>");
                            //bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTitleAdv(mrow["Ma_QuangCao"], mrow["Ten_QuangCao"]) + "</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTB(mrow["Ma_Tinbai"], mrow["Tieude"]) + "</div>");
                            bd.Append("<div class=\"divtextclassDes\">" + CheckBindTextTBSoTu(mrow["Ma_Tinbai"], mrow["Sotu"]) + "</div>");
                            bd.Append("</div>");
                            bd.Append("</div>");
                        }
                        else if (mrow["Ma_QuangCao"].ToString() != "0")
                        {
                            bd.Append("<div title=" + mrow["Ma_Vitri"] + " id=" + mrow["Ma_Vitri"] + " class=\"drsElementStatic\" style='left: " + mrow["Trai"] + "px; top: " + mrow["Tren"] + "px; cursor: move; width: " + mrow["Rong"] + "px; height: " + mrow["Dai"] + "px;'>");
                            bd.Append("<div class=\"drsMoveHandleStatic\" id=\"Move" + mrow["Ma_Vitri"] + "\"></div>");
                            bd.Append("<div class=\"divtextclass\" id=\"divtext" + mrow["Ma_Vitri"] + "\">");

                            bd.Append("<div style=\"display:none\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_QuangCao"] + "\" id=\"txtAdv" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Ma_Tinbai"] + "\" id=\"txtTB" + mrow["Ma_Vitri"] + "\">");
                            bd.Append("<input type=\"text\" value=\"" + mrow["Doituong_DangXuly"] + "\" id=\"txtStatusTB" + mrow["Ma_Vitri"] + "\"  />");
                            bd.Append("</div>");
                            bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTitleAdv(mrow["Ma_QuangCao"], mrow["Ten_QuangCao"]) + "</div>");
                            //bd.Append("<div class=\"divtextclassTitle\">" + CheckBindTextTB(mrow["Ma_Tinbai"], mrow["Tieude"]) + "</div>");
                            //bd.Append("<div class=\"divtextclassDes\">" + CheckBindTextTBSoTu(mrow["Ma_Tinbai"], mrow["Sotu"]) + "</div>");
                            bd.Append("</div>");
                            bd.Append("</div>");
                        }
                    }
                }
                ltrBindData.Text = bd.ToString();
                _ds.Clear();
            }
            catch (Exception ex) { throw ex; }
        }
        public string CheckDisplay(object idCV, object idTB, object Vitri)
        {
            string str = string.Empty;
            if (idTB.ToString() != "")
                str = "<input id=\"txtTB" + Vitri.ToString() + "\" type=\"text\" value=\"" + idTB.ToString() + "\"/>";
            if (idCV.ToString() != "")
                str = "<input id=\"txtCV" + Vitri.ToString() + "\" type=\"text\" value=\"" + idCV.ToString() + "\"/>";
            return str;
        }
        protected string CheckBindTextCV(object MaCV, object textCV)
        {
            string str = "";
            if (MaCV.ToString() != "")
                str = "<span>Công việc:</span> " + textCV.ToString();
            else str = "";
            return str;
        }
        protected string CheckBindTextTitleAdv(object MaQC, object TenQC)
        {
            string str = "";
            if (MaQC.ToString() != "")
                str = "<span>Quảng cáo: </span> " + TenQC.ToString();
            return str;
        }
        protected string CheckBindTextTB(object MaTB, object textTB)
        {
            string str = "";
            if (MaTB.ToString() != "")
            {
                str = "<span>Tin bài:</span><a href=\"Javascript:open_window_Scroll('" + Global.ApplicationPath + "/Quytrinh/ViewTinbaiDantrang.aspx?Menu_ID=52&ID=" + MaTB.ToString() + "',50,800,300,900);\"> " + textTB.ToString() + "</a>";
            }
            else { str = ""; }
            return str;
        }
        protected string CheckBindTextTBSoTu(object MaTB, object sotu)
        {
            string str = "";
            if (MaTB.ToString() != "")
            {
                str = "<span>Số từ:</span> " + sotu.ToString();
            }
            else { str = ""; }
            return str;
        }
        protected string CheckTypeLayout()
        {
            try
            {
                string str = "";
                HPCBusinessLogic.VitriTinbaiDAL _objVitritb = new HPCBusinessLogic.VitriTinbaiDAL();
                DataSet _ds;
                _ds = _objVitritb.GetTypeLayout(mabao, trang);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    DataRow mrow = _ds.Tables[0].Rows[0];
                    if (mrow["Type"].ToString() == "2")
                        str = "<div class=\"separated\"></div>";
                }
                _ds.Clear();
                return str;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

