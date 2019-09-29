using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nguoidung_Chucnang
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected Int32 _Ma_Nguoidung;
        protected Int32 _Ma_ChucNang;
        protected Boolean _Doc;
        protected Boolean _Ghi;
        protected Boolean _Xoa;
        protected DateTime _NgayTao;
        protected Int32 _Ma_Nhom;
        public T_Nguoidung_Chucnang()
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
        public Int32 Ma_ChucNang
        {
            get { return _Ma_ChucNang; }
            set { _Ma_ChucNang = value; }
        }
        public Boolean Doc
        {
            get { return _Doc; }
            set { _Doc = value; }
        }
        public Boolean Ghi
        {
            get { return _Ghi; }
            set { _Ghi = value; }
        }
        public Boolean Xoa
        {
            get { return _Xoa; }
            set { _Xoa = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Int32 Ma_Nhom
        {
            get { return _Ma_Nhom; }
            set { _Ma_Nhom = value; }
        }
        #endregion
    }
}
