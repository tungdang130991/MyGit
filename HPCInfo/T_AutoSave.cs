using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_AutoSave
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Int32 _Ma_TinBai;
        protected String _Tieude = string.Empty;
        protected String _Noidung = string.Empty;
        protected DateTime _NgayTao;
        protected Double _Ma_Nguoitao;
        public T_AutoSave()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Int32 Ma_TinBai
        {
            get { return _Ma_TinBai; }
            set { _Ma_TinBai = value; }
        }
        public String Tieude
        {
            get { return _Tieude; }
            set { _Tieude = value; }
        }
        public String Noidung
        {
            get { return _Noidung; }
            set { _Noidung = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Double Ma_Nguoitao
        {
            get { return _Ma_Nguoitao; }
            set { _Ma_Nguoitao = value; }
        }
        #endregion
    }
}
