using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Chucnang
    {
        #region Member variables and contructor
        protected Int32 _Ma_Chucnang;
        protected String _Ten_chucnang = string.Empty;
        protected String _Ma_Doituong = string.Empty;
        protected String _Mota = string.Empty;
        protected Int32 _STT;
        protected Int32 _Ma_Chucnang_Cha;
        protected DateTime _NgayTao;
        protected String _URL_Chucnang = string.Empty;
        protected String _Icon = string.Empty;
        protected Int32 _Ma_Nguoitao;
        protected Boolean _HoatDong;
        protected Int32 _Nguoisua;
        protected DateTime _Ngaysua;
        protected Boolean _Quytrinh;
        protected String _MenuEnglish;
        public T_Chucnang()
        {
        }
        #endregion
        #region Public Properties
        public Int32 Ma_Chucnang
        {
            get { return _Ma_Chucnang; }
            set { _Ma_Chucnang = value; }
        }
        public String Ten_chucnang
        {
            get { return _Ten_chucnang; }
            set { _Ten_chucnang = value; }
        }
        public String Ma_Doituong
        {
            get { return _Ma_Doituong; }
            set { _Ma_Doituong = value; }
        }
        public String Mota
        {
            get { return _Mota; }
            set { _Mota = value; }
        }
        public Int32 STT
        {
            get { return _STT; }
            set { _STT = value; }
        }
        public Int32 Ma_Chucnang_Cha
        {
            get { return _Ma_Chucnang_Cha; }
            set { _Ma_Chucnang_Cha = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public String URL_Chucnang
        {
            get { return _URL_Chucnang; }
            set { _URL_Chucnang = value; }
        }
        public String Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        public Int32 Ma_Nguoitao
        {
            get { return _Ma_Nguoitao; }
            set { _Ma_Nguoitao = value; }
        }
        public Boolean HoatDong
        {
            get { return _HoatDong; }
            set { _HoatDong = value; }
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
        public Boolean Quytrinh
        {
            get { return _Quytrinh; }
            set { _Quytrinh = value; }
        }
        public String MenuEnglish
        {
            get { return _MenuEnglish; }
            set { _MenuEnglish = value; }
        }
        #endregion
    }
}
