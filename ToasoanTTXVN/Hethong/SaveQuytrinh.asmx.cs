using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SSOLib.ServiceAgent;
using HPCInfo;
using System.Collections;
using HPCBusinessLogic;
using HPCComponents;
using System.Data;
using System.Web.Script.Services;
using System.Text;
namespace ToasoanTTXVN.Hethong
{
    /// <summary>
    /// Summary description for SaveQuytrinh
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    public class SaveQuytrinh : System.Web.Services.WebService
    {
        HPCBusinessLogic.Lichsu_Thaotac_HethongDAL actionDAL = new Lichsu_Thaotac_HethongDAL();
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        HPCBusinessLogic.DAL.QuyTrinhDAL _QTDAL = new HPCBusinessLogic.DAL.QuyTrinhDAL();
        HPCBusinessLogic.DoituongDAL _DTDAL = new HPCBusinessLogic.DoituongDAL();
        T_Users _user = null;
        string thaotac = "";
        T_Lichsu_Thaotac_Hethong action = new T_Lichsu_Thaotac_Hethong();

        [WebMethod]
        public string Save(string Ma_Doituong_Gui, string Ma_Doituong_Nhan, string MaAnpham, string MenuID, string IpAddress)
        {
            try
            {
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                #region GhiLog
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserName;
                action.HostIP = IpAddress;
                action.NgayThaotac = DateTime.Now;
                action.Ma_Chucnang = int.Parse(MenuID);
                #endregion
                int maAnPham = int.Parse(MaAnpham);
                int id = 0;
                id = _QTDAL.Check_QuyTrinh(Ma_Doituong_Gui, Ma_Doituong_Nhan, maAnPham);
                if (id == 0)
                {
                    int _connect;
                    _connect = _QTDAL.InsertT_QuytrinhTemp(Ma_Doituong_Gui, Ma_Doituong_Nhan, maAnPham, _user.UserID, _user.UserID);
                    if (_connect == 0)
                    {
                        return "ConnectError";
                    }
                    else
                    {
                        thaotac = "Thiết lập quy trinh động theo ấn phẩm";
                        action.Thaotac = thaotac;
                        actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                        return "ConnectSuccess";

                    }

                }
                else
                {
                    return "SaveError";
                }

            }
            catch
            {
                return "Error,,";
            }
        }
        [WebMethod]
        public string DeleteQuytrinh(string Ma_Doituong_Gui, string Ma_Doituong_Nhan, string MaAnpham, string MenuID, string IpAddress)
        {
            try
            {
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                #region GhiLog
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserName;
                action.HostIP = IpAddress;
                action.NgayThaotac = DateTime.Now;
                action.Ma_Chucnang = int.Parse(MenuID);
                #endregion
                int maAnPham = int.Parse(MaAnpham);
                _QTDAL.DeleteT_QuytrinhTemp(Ma_Doituong_Gui, Ma_Doituong_Nhan, maAnPham);
                thaotac = "Xóa quy trình theo ấn phẩm";
                action.Thaotac = thaotac;
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                return "Item Saved,,";
            }
            catch
            {
                return "Error,,";
            }
        }

        [WebMethod]
        public string UpdatePosition(string MaDoituong, string MaAnpham, string Left, string Top, string MenuID, string IpAddress)
        {
            try
            {
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                #region GhiLog
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserName;
                action.HostIP = IpAddress;
                action.NgayThaotac = DateTime.Now;
                action.Ma_Chucnang = int.Parse(MenuID);
                #endregion
                int maAnPham = int.Parse(MaAnpham);
                _DTDAL.UpdatePosition_Doituong(MaDoituong, maAnPham, Left, Top);
                thaotac = "Lưu vị trí của đối tượng";
                action.Thaotac = thaotac;
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                return "Item Saved,,";
            }
            catch
            {
                return "Error,,";
            }
        }

        [WebMethod]
        public string InsertDoiTuong(string ID, string MaDoituong, string TenDoiTuong, string Stt, string MenuID, string IpAddress)
        {
            try
            {
                #region GhiLog
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserName;
                action.HostIP = IpAddress;
                action.NgayThaotac = DateTime.Now;
                action.Ma_Chucnang = int.Parse(MenuID);
                #endregion
                if (ID == "")
                {
                    ID = "0";
                }
                int _id = int.Parse(ID);
                T_Doituong _dt = new T_Doituong();
                _dt.Ma_Doituong = MaDoituong;
                _dt.Ten_Doituong = TenDoiTuong;
                if (!String.IsNullOrEmpty(Stt.Trim()))
                    _dt.STT = Convert.ToInt32(Stt.Trim());
                _dt.Ngaysua = DateTime.Now;
                _dt.Nguoitao = _user.UserID;
                _dt.Ngaytao = DateTime.Now;
                _dt.Nguoisua = _user.UserID;
                if (_id == 0)
                {
                    DataSet _ds = _DTDAL.Check_Doituong(MaDoituong, TenDoiTuong);
                    if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                    {
                        string ten = _ds.Tables[0].Rows[0][0].ToString();
                        string ma = _ds.Tables[0].Rows[0][1].ToString();
                        if (ten.ToLower() == TenDoiTuong.ToLower())
                        {
                            return "TenDTError";
                        }
                        else if (ma.ToLower() == MaDoituong.ToLower())
                        {
                            return "MaDTError";
                        }
                        return "Đối tượng đã tồn tại";
                    }
                    else
                    {
                        DataSet _dsSTT = _DTDAL.Select_TenDoiTuong_By_Stt(Stt);
                        if (_dsSTT.Tables[0] == null || _dsSTT.Tables[0].Rows.Count == 0)
                        {
                            _dt.ID = 0;
                            int _return = _DTDAL.InsertT_Doituong(_dt);
                            action.Thaotac = "[Thêm mới đối tượng]-->[Mã mã đối tượng:" + _return.ToString() + " ]";
                            actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                            return "AddSuccess";
                        }
                        else
                        {
                            return "STTError";
                        }
                    }
                }
                else
                {
                    _dt.ID = _id;
                    int _return = _DTDAL.InsertT_Doituong(_dt);
                    action.Thaotac = "[Sửa đối tượng]-->[Mã mã đối tượng:" + _return.ToString() + " ]";
                    actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                    return "EditSuccess";
                }
            }
            catch
            {
                return "Error,,";
            }
        }
        [WebMethod]
        public string Delete_DoiTuong_AnPham(string ID, string Name, string Ma_Anpham, string MenuID, string IpAddress)
        {
            try
            {
                _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                #region GhiLog
                action.Ma_Nguoidung = _user.UserID;
                action.TenDaydu = _user.UserName;
                action.HostIP = IpAddress;
                action.NgayThaotac = DateTime.Now;
                action.Ma_Chucnang = int.Parse(MenuID);
                #endregion
                int _id = Convert.ToInt32(ID);
                int _maanpham = Convert.ToInt32(Ma_Anpham);
                _DTDAL.DeleteOneFromT_Doituong_AnPham(_id, _maanpham);
                thaotac = "Xóa đối tượng trong ấn phẩm'" + Name + "'";
                action.Thaotac = thaotac;
                actionDAL.InserT_Lichsu_Thaotac_Hethong(action);
                return "Item Saved,,";
            }
            catch
            {
                return "Error,,";
            }
        }

        [WebMethod]
        public List<string> AutoCompleteTenDT(string TenDT)
        {
            try
            {
                List<string> result = new List<string>();
                DoituongDAL _DAL = new DoituongDAL();
                DataSet _ds;
                _ds = _DAL.Select_TenDoiTuong(TenDT);
                DataTable dt = _ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(item["Ten_Doituong"].ToString());
                    }
                }
                else
                {
                    result.Add("Tên đối tượng này chưa tồn tại !!");
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [WebMethod]
        public List<string> AutoCompleteMaDT(string MaDT)
        {
            try
            {
                List<string> result = new List<string>();
                DoituongDAL _DAL = new DoituongDAL();
                DataSet _ds;
                _ds = _DAL.Select_MaDoiTuong(MaDT);
                DataTable dt = _ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(item["Ma_Doituong"].ToString());
                    }
                }
                else
                {
                    result.Add("Mã đối tượng này chưa tồn tại !!");
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [WebMethod]
        public List<string> AutoCompleteStt(string Stt)
        {
            try
            {
                List<string> result = new List<string>();
                DoituongDAL _DAL = new DoituongDAL();
                DataSet _ds;
                _ds = _DAL.Select_TenDoiTuong_By_Stt(Stt);
                DataTable dt = _ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(item["Ten_Doituong"].ToString());
                    }
                }
                else
                {
                    result.Add("Số thứ tự này chưa tồn tại !!");
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
