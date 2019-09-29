using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nguoidung_Nhom
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Double _Ma_Nhom;
        protected Double _Ma_Nguoidung;
        public T_Nguoidung_Nhom()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Double Ma_Nhom
        {
            get { return _Ma_Nhom; }
            set { _Ma_Nhom = value; }
        }
        public Double Ma_Nguoidung
        {
            get { return _Ma_Nguoidung; }
            set { _Ma_Nguoidung = value; }
        }
        #endregion
    }
}
