using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_Doituong
    {
        #region Member variables and contructor
        protected Double _ID;
        protected String _Ma_Doituong = string.Empty;
        protected String _Ten_Doituong = string.Empty;
        protected String _EnglishName = string.Empty;
        protected Int32 _STT;
        protected Int32 _Nguoitao;
        protected DateTime _Ngaytao;
        protected Int32 _Nguoisua;
        protected DateTime _Ngaysua;
        protected String _CssLeft = string.Empty;
        protected String _CssTop = string.Empty;
        public T_Doituong()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String Ma_Doituong
        {
            get { return _Ma_Doituong; }
            set { _Ma_Doituong = value; }
        }
        public String Ten_Doituong
        {
            get { return _Ten_Doituong; }
            set { _Ten_Doituong = value; }
        }
        public String EnglishName
        {
            get { return _EnglishName; }
            set { _EnglishName = value; }
        }
        public Int32 STT
        {
            get { return _STT; }
            set { _STT = value; }
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
        public String CssLeft
        {
            get { return _CssLeft; }
            set { _CssLeft = value; }
        }
        public String CssTop
        {
            get { return _CssTop; }
            set { _CssTop = value; }
        }
        #endregion
    }
}
