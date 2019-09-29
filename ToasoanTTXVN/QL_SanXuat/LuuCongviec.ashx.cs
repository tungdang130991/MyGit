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
    public class LuuCongviec : IHttpHandler, IRequiresSessionState
    {
        HttpContext mContext;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            mContext = context;
            Save_Click();
        }

        protected void Save_Click()
        {
            int Menu_ID = 0;
            string NoiDung = string.Empty;
            string SoTu = string.Empty;
            string NgayHT = string.Empty;
            string NguoiNhan = string.Empty;
            string tieudecv = string.Empty;
            string ip = string.Empty;
            try
            {
                System.Globalization.CultureInfo mProvider = new System.Globalization.CultureInfo("en-US", false);
                if (mContext.Request.QueryString["mn_id"] != "undefined")
                {
                    Menu_ID = int.Parse(mContext.Request.QueryString["mn_id"], mProvider);
                }
                if (mContext.Request.QueryString["nguoinhan"] != "undefined")
                {
                    NguoiNhan = mContext.Request.QueryString["nguoinhan"].ToString();
                }
                if (mContext.Request.QueryString["tieudecv"] != "undefined")
                {
                    tieudecv = mContext.Request.QueryString["tieudecv"].ToString();
                }
                if (mContext.Request.QueryString["noidung"] != "undefined")
                {
                    NoiDung = mContext.Request.QueryString["noidung"].ToString();
                }
                if (mContext.Request.QueryString["sotu"] != "undefined")
                {
                    SoTu = mContext.Request.QueryString["sotu"].ToString();
                }
                if (mContext.Request.QueryString["ngayht"] != "undefined")
                {
                    NgayHT = mContext.Request.QueryString["ngayht"].ToString();
                }
                if (mContext.Request.QueryString["ip"] != "undefined")
                {
                    ip = mContext.Request.QueryString["ip"].ToString();
                }

                HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
                T_Users _user;
                T_RolePermission _Role = null;
                _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Menu_ID);
                #region GhiLog
                Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
                T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserFullName;
                action.HostIP = ip;
                action.NgayThaotac = DateTime.Now;
                #endregion

                CongviecDAL _cvDAL = new CongviecDAL();
                T_Congviec _cv = SetItem(NoiDung, SoTu, NgayHT, NguoiNhan,tieudecv);
                double _return = _cvDAL.InsertT_Congviec(_cv);

                action.Thaotac = "[Thêm mới công việc]-->[mã công việc:" + _return.ToString() + " ]";
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                mContext.Response.Write("1");
            }
            catch { mContext.Response.Write("0"); }

        }
        private T_Congviec SetItem(string noidung_, string sotu_, string ngayHT_, string nguoinhan_,string tieudecv)
        {
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
            T_Users _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
            T_Congviec _obj = new T_Congviec();
            _obj.Ma_Congviec = 0;
            _obj.Noidung_Congviec = UltilFunc.SqlFormatText(noidung_);
            _obj.Sotu = Convert.ToInt16(sotu_);
            _obj.NgayTao = DateTime.Now;
            _obj.NgayHoanthanh = UltilFunc.ToDate(ngayHT_, "dd/MM/yyyy");
            _obj.NguoiNhan = Convert.ToDouble(nguoinhan_);
            _obj.Tencongviec = tieudecv;
            _obj.NguoiTao = _user.UserID;
            _obj.NguoiGiaoViec = _user.UserID;
            return _obj;
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
