using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nhom_Chucnang
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Int32 _Ma_Nhom;
        protected Int32 _Ma_ChucNang;
        protected Boolean _Doc;
        protected Boolean _Ghi;
        protected Boolean _Xoa;
        protected DateTime _NgayTao;
        public T_Nhom_Chucnang()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Int32 Ma_Nhom
        {
            get { return _Ma_Nhom; }
            set { _Ma_Nhom = value; }
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
        #endregion
    }
}
