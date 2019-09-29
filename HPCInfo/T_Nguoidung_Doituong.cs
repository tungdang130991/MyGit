using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nguoidung_Doituong
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected Int32 _Ma_Nguoidung;
        protected String _Ma_Doituong = string.Empty;
        protected DateTime _NgayTao;
        public T_Nguoidung_Doituong()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Int32 Ma_Nguoidung
        {
            get { return _Ma_Nguoidung; }
            set { _Ma_Nguoidung = value; }
        }
        public String Ma_Doituong
        {
            get { return _Ma_Doituong; }
            set { _Ma_Doituong = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        #endregion
    }
}
