using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_QuangCao
    {
        #region Member variables and contructor
        protected Int32 _Ma_Quangcao;
        protected String _Ten_QuangCao = string.Empty;
        protected String _Noidung = string.Empty;
        protected Int32 _Trang;
        protected Int32 _Kichthuoc;
        protected DateTime _Ngaydang;
        protected Int32 _Nguoitao;
        protected DateTime _NgayTao;
        protected Int32 _Ma_KhachHang;
        protected Int32 _Ma_Loaibao;
        protected Int32 _Trangthai;
        public T_QuangCao()
        {
        }
        #endregion
        #region Public Properties
        public Int32 Ma_Quangcao
        {
            get { return _Ma_Quangcao; }
            set { _Ma_Quangcao = value; }
        }
        public String Ten_QuangCao
        {
            get { return _Ten_QuangCao; }
            set { _Ten_QuangCao = value; }
        }
        public String Noidung
        {
            get { return _Noidung; }
            set { _Noidung = value; }
        }
        public Int32 Trang
        {
            get { return _Trang; }
            set { _Trang = value; }
        }
        public Int32 Kichthuoc
        {
            get { return _Kichthuoc; }
            set { _Kichthuoc = value; }
        }
        public DateTime Ngaydang
        {
            get { return _Ngaydang; }
            set { _Ngaydang = value; }
        }
        public Int32 Nguoitao
        {
            get { return _Nguoitao; }
            set { _Nguoitao = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Int32 Ma_KhachHang
        {
            get { return _Ma_KhachHang; }
            set { _Ma_KhachHang = value; }
        }
        public Int32 Ma_Loaibao
        {
            get { return _Ma_Loaibao; }
            set { _Ma_Loaibao = value; }
        }
        public Int32 Trangthai
        {
            get { return _Trangthai; }
            set { _Trangthai = value; }
        }
        #endregion
    }
}
