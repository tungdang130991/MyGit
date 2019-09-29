using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nhom_Chuyenmuc
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Int32 _Ma_Nhom;
        protected Int32 _Ma_Chuyenmuc;
        protected DateTime _NgayTao;
        public T_Nhom_Chuyenmuc()
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
        public Int32 Ma_Chuyenmuc
        {
            get { return _Ma_Chuyenmuc; }
            set { _Ma_Chuyenmuc = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        #endregion
    }
}