using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
   public class T_NgonNgu
    {
       #region Member variables and contructor
        protected Int32 _ID;
        protected String _TenNgonNgu = string.Empty;
        protected String _MoTa = string.Empty;
        protected Boolean _HoatDong;
        protected Int32 _ThuTu;
        public T_NgonNgu()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String TenNgonNgu
        {
            get { return _TenNgonNgu; }
            set { _TenNgonNgu = value; }
        }
        public String MoTa
        {
            get { return _MoTa; }
            set { _MoTa = value; }
        }
        public Boolean HoatDong
        {
            get { return _HoatDong; }
            set { _HoatDong = value; }
        }
        public Int32 ThuTu
        {
            get { return _ThuTu; }
            set { _ThuTu = value; }
        }
        #endregion
    }
}
