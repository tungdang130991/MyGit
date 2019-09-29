using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nguoidung_Chuyenmuc
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Int32 _Ma_Nguoidung;
        protected Int32 _Ma_chuyenmuc;
        protected Int32 _Ma_Nhom;
        protected DateTime _NgayTao;
        public T_Nguoidung_Chuyenmuc()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Int32 Ma_Nguoidung
        {
            get { return _Ma_Nguoidung; }
            set { _Ma_Nguoidung = value; }
        }
        public Int32 Ma_chuyenmuc
        {
            get { return _Ma_chuyenmuc; }
            set { _Ma_chuyenmuc = value; }
        }
        public Int32 Ma_Nhom
        {
            get { return _Ma_Nhom; }
            set { _Ma_Nhom = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        #endregion
    }
}