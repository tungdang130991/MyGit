using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_Anh
    {
        #region Member variables and contructor
        protected Double _Ma_Anh;
        protected String _Duongdan_Anh = string.Empty;
        protected String _TenFile_Goc = string.Empty;
        protected String _TenFile_Hethong = string.Empty;
        protected String _TuKhoa = string.Empty;
        protected String _NguoiChup = string.Empty;
        protected String _TieuDe = string.Empty;
        protected DateTime _NgayTao;
        protected Int32 _NguoiTao;
        protected Double _Nhuanbut;
        protected Boolean _Thanhtoan;
        protected DateTime _Ngaythanhtoan;
        protected Int32 _Nguoithanhtoan;
        protected Int32 _Nguoicham;
        protected String _Chuthich;
        protected Int32 _Ma_Nguoichup;
        protected Boolean _Duyet;
        protected String _Nhanxet;
        public T_Anh()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_Anh
        {
            get { return _Ma_Anh; }
            set { _Ma_Anh = value; }
        }
        public String Duongdan_Anh
        {
            get { return _Duongdan_Anh; }
            set { _Duongdan_Anh = value; }
        }
        public String TenFile_Goc
        {
            get { return _TenFile_Goc; }
            set { _TenFile_Goc = value; }
        }
        public String TenFile_Hethong
        {
            get { return _TenFile_Hethong; }
            set { _TenFile_Hethong = value; }
        }
        public String TuKhoa
        {
            get { return _TuKhoa; }
            set { _TuKhoa = value; }
        }
        public String NguoiChup
        {
            get { return _NguoiChup; }
            set { _NguoiChup = value; }
        }
        public String TieuDe
        {
            get { return _TieuDe; }
            set { _TieuDe = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Int32 NguoiTao
        {
            get { return _NguoiTao; }
            set { _NguoiTao = value; }
        }
        public Double Nhuanbut
        {
            get { return _Nhuanbut; }
            set { _Nhuanbut = value; }
        }
        public Boolean Thanhtoan
        {
            get { return _Thanhtoan; }
            set { _Thanhtoan = value; }
        }
        public DateTime Ngaythanhtoan
        {
            get { return _Ngaythanhtoan; }
            set { _Ngaythanhtoan = value; }
        }
        public Int32 Nguoithanhtoan
        {
            get { return _Nguoithanhtoan; }
            set { _Nguoithanhtoan = value; }
        }
        public Int32 Nguoicham
        {
            get { return _Nguoicham; }
            set { _Nguoicham = value; }
        }
        public String Chuthich
        {
            get { return _Chuthich; }
            set { _Chuthich = value; }
        }
        public Int32 Ma_Nguoichup
        {
            get { return _Ma_Nguoichup; }
            set { _Ma_Nguoichup = value; }
        }
        public Boolean Duyet
        {
            get { return _Duyet; }
            set { _Duyet = value; }
        }
        public String Nhanxet
        {
            get { return _Nhanxet; }
            set { _Nhanxet = value; }
        }
        #endregion
    }
}
