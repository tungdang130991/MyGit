using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_Yeucau
    {
        #region Member variables and contructor
        protected Double _ID;
        protected String _TenQuangCao;
        protected String _NoidungQC = string.Empty;
        protected Int32 _Ma_Khachhang;        
        protected Int32 _Nguoitao;
        protected DateTime _Ngaytao;
        protected Int32 _Nguoisua;
        protected DateTime _Ngaysua;
        protected Int16 _Trangthai;
        protected Int16 _Loai;
        public T_Yeucau()
        {
        }
        #endregion

        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String TenQuangCao
        {
            get { return _TenQuangCao; }
            set { _TenQuangCao = value; }
        }
        public String NoidungQC
        {
            get { return _NoidungQC; }
            set { _NoidungQC = value; }
        }
        public Int32 Ma_Khachhang
        {
            get { return _Ma_Khachhang; }
            set { _Ma_Khachhang = value; }
        }

        public Int32 Nguoitao
        {
            get { return _Nguoitao; }
            set { _Nguoitao = value; }
        }
        public DateTime Ngaytao
        {
            get { return _Ngaytao; }
            set { _Ngaytao = value; }
        }
        public Int32 Nguoisua
        {
            get { return _Nguoisua; }
            set { _Nguoisua = value; }
        }
        public DateTime Ngaysua
        {
            get { return _Ngaysua; }
            set { _Ngaysua = value; }
        }
        public Int16 Trangthai
        {
            get { return _Trangthai; }
            set { _Trangthai = value; }
        }
        public Int16 Loai
        {
            get { return _Loai; }
            set { _Loai = value; }
        }
        #endregion
    }
}
