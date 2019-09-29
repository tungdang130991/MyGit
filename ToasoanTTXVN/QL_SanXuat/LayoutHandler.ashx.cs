using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.SessionState;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
namespace ToasoanTTXVN.QL_SanXuat
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>

    public class LayoutHandler : IHttpHandler, IRequiresSessionState
    {

        enum ActionType
        {
            Addnew = 1,
        }
        HttpContext mContext;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            mContext = context;
            SaveInfoDiv();
            //ActionType mType = (ActionType)int.Parse(mContext.Request.QueryString["type"]);
            //switch (mType)
            //{
            //    case ActionType.Addnew:
            //        SaveInfoDiv();
            //        break;
            //}
        }

        private void SaveInfoDiv()
        {
            VitriTinbaiDAL _objvttb = new VitriTinbaiDAL();
            int Maso_bao = 0;
            int TrangBao = 0;
            int title = 0;
            int width = 0;
            int height = 0;
            int top = 0;
            int left = 0;
            int MaTinbai = 0;
            int MaCV = 0;
            int MaQC = 0;
            int type = 0;
            try
            {
                System.Globalization.CultureInfo mProvider = new System.Globalization.CultureInfo("en-US", false);
                if (mContext.Request.QueryString["title"] != "undefined")
                {
                    title = int.Parse(mContext.Request.QueryString["title"], mProvider);
                }
                if (mContext.Request.QueryString["width"] != "undefined")
                {
                    width = int.Parse(mContext.Request.QueryString["width"].Substring(0, mContext.Request.QueryString["width"].Length - 2), mProvider);
                }
                if (mContext.Request.QueryString["height"] != "undefined")
                {
                    height = int.Parse(mContext.Request.QueryString["height"].Substring(0, mContext.Request.QueryString["height"].Length - 2), mProvider);
                }
                if (mContext.Request.QueryString["top"] != "undefined")
                {
                    top = int.Parse(mContext.Request.QueryString["top"].Substring(0, mContext.Request.QueryString["top"].Length - 2), mProvider);
                }
                if (mContext.Request.QueryString["left"] != "undefined")
                {
                    left = int.Parse(mContext.Request.QueryString["left"].Substring(0, mContext.Request.QueryString["left"].Length - 2), mProvider);
                }
                if (mContext.Request.QueryString["masobao"] != "undefined")
                {
                    Maso_bao = int.Parse(mContext.Request.QueryString["masobao"]);
                }
                if (mContext.Request.QueryString["trangbao"] != "undefined")
                {
                    TrangBao = int.Parse(mContext.Request.QueryString["trangbao"], mProvider);
                }
                if (mContext.Request.QueryString["matinbai"] != "undefined")
                {
                    MaTinbai = int.Parse(mContext.Request.QueryString["matinbai"]);
                }
                if (mContext.Request.QueryString["macv"] != "undefined")
                {
                    MaCV = int.Parse(mContext.Request.QueryString["macv"]);
                }
                if (mContext.Request.QueryString["maqc"] != "undefined")
                {
                    MaQC = int.Parse(mContext.Request.QueryString["maqc"]);
                }
                if (mContext.Request.QueryString["type"] != "undefined")
                {
                    type = int.Parse(mContext.Request.QueryString["type"]);
                }
                if (title == 0)
                {
                    if (type == 1)
                    {
                        if (!_objvttb.T_Vitri_Tinbai_CheckExistRow(type, MaCV))
                            _objvttb.T_Vitri_Tinbai_Insert(Maso_bao, TrangBao, MaTinbai, MaCV, MaQC, left, top, width, height);
                        else
                            _objvttb.T_Vitri_Tinbai_UpdateByPara(type,MaCV,Maso_bao, TrangBao, left, top, width, height);
                    }
                    else if (type == 2)
                    {
                        if (!_objvttb.T_Vitri_Tinbai_CheckExistRow(type, MaTinbai))
                            _objvttb.T_Vitri_Tinbai_Insert(Maso_bao, TrangBao, MaTinbai, MaCV, MaQC, left, top, width, height);
                        else
                            _objvttb.T_Vitri_Tinbai_UpdateByPara(type, MaTinbai, Maso_bao, TrangBao, left, top, width, height);
                    }
                   else
                    {
                        _objvttb.T_Vitri_Tinbai_Insert(Maso_bao, TrangBao, MaTinbai, MaCV, MaQC, left, top, width, height);
                       
                    }
                }
                else
                {
                    _objvttb.T_Vitri_Tinbai_Update(title, Maso_bao, TrangBao, MaTinbai, MaCV, MaQC, left, top, width, height);
                }
                mContext.Response.Write("1");
            }
            catch { mContext.Response.Write("0"); }
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
