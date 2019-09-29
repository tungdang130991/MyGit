using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_NhuanBut
    {
        #region Member variables and contructor
        protected Double _Ma_NhuanBut;
        protected Double _Ma_TinBai;
        protected Double _Sotien;
        protected Double _Ma_tacgia;
        protected Double _Ma_Anh;
        protected Boolean _THANHTOAN;
        protected DateTime _NGAY_THANHTOAN;
        protected Int32 _NGUOITHANHTOAN;
        protected String _GHICHU = string.Empty;
        protected Int32 _Nguoicham;
        public T_NhuanBut()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_NhuanBut
        {
            get { return _Ma_NhuanBut; }
            set { _Ma_NhuanBut = value; }
        }
        public Double Ma_TinBai
        {
            get { return _Ma_TinBai; }
            set { _Ma_TinBai = value; }
        }
        public Double Sotien
        {
            get { return _Sotien; }
            set { _Sotien = value; }
        }
        public Double Ma_tacgia
        {
            get { return _Ma_tacgia; }
            set { _Ma_tacgia = value; }
        }
        public Double Ma_Anh
        {
            get { return _Ma_Anh; }
            set { _Ma_Anh = value; }
        }
        public Boolean THANHTOAN
        {
            get { return _THANHTOAN; }
            set { _THANHTOAN = value; }
        }
        public DateTime NGAY_THANHTOAN
        {
            get { return _NGAY_THANHTOAN; }
            set { _NGAY_THANHTOAN = value; }
        }
        public Int32 NGUOITHANHTOAN
        {
            get { return _NGUOITHANHTOAN; }
            set { _NGUOITHANHTOAN = value; }
        }
        public String GHICHU
        {
            get { return _GHICHU; }
            set { _GHICHU = value; }
        }
        public Int32 Nguoicham
        {
            get { return _Nguoicham; }
            set { _Nguoicham = value; }
        }
        #endregion
    }
}
