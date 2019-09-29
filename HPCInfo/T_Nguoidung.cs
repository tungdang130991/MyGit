using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nguoidung
    {
        #region Member variables and contructor
        protected Double _Ma_Nguoidung;
        protected String _Ten_Dangnhap = string.Empty;
        protected String _MatKhau = string.Empty;
        protected String _TenDaydu = string.Empty;
        protected String _CMTND = string.Empty;
        protected String _Email = string.Empty;
        protected String _Mobile = string.Empty;
        protected String _Diachi = string.Empty;
        protected DateTime _Ngaysinh;
        protected Boolean _Hoatdong;
        protected DateTime _NgayTao;
        protected Int32 _NguoiTao;
        protected Int32 _Loai;
        protected Int32 _Ma_Vung;
        protected Int32 _Ma_PhongBan;
        protected String _Image = string.Empty;
        public T_Nguoidung()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_Nguoidung
        {
            get { return _Ma_Nguoidung; }
            set { _Ma_Nguoidung = value; }
        }
        public String Ten_Dangnhap
        {
            get { return _Ten_Dangnhap; }
            set { _Ten_Dangnhap = value; }
        }
        public String MatKhau
        {
            get { return _MatKhau; }
            set { _MatKhau = value; }
        }
        public String TenDaydu
        {
            get { return _TenDaydu; }
            set { _TenDaydu = value; }
        }
        public String CMTND
        {
            get { return _CMTND; }
            set { _CMTND = value; }
        }
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public String Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public String Diachi
        {
            get { return _Diachi; }
            set { _Diachi = value; }
        }
        public DateTime Ngaysinh
        {
            get { return _Ngaysinh; }
            set { _Ngaysinh = value; }
        }
        public Boolean Hoatdong
        {
            get { return _Hoatdong; }
            set { _Hoatdong = value; }
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
        public Int32 Loai
        {
            get { return _Loai; }
            set { _Loai = value; }
        }
        public Int32 Ma_Vung
        {
            get { return _Ma_Vung; }
            set { _Ma_Vung = value; }
        }
        public Int32 Ma_PhongBan
        {
            get { return _Ma_PhongBan; }
            set { _Ma_PhongBan = value; }
        }
        public String Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        #endregion
    }
}
