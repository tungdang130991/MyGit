using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Nhom
    {
        #region Member variables and contructor
        protected Double _Ma_Nhom;
        protected String _Ten_Nhom = string.Empty;
        protected String _MoTa = string.Empty;
        protected DateTime _NgayTao;
        public T_Nhom()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_Nhom
        {
            get { return _Ma_Nhom; }
            set { _Ma_Nhom = value; }
        }
        public String Ten_Nhom
        {
            get { return _Ten_Nhom; }
            set { _Ten_Nhom = value; }
        }
        public String MoTa
        {
            get { return _MoTa; }
            set { _MoTa = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        #endregion
    }
}

