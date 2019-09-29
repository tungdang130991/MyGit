using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_FileQuangCao
    {
        #region Member variables and contructor
        protected Double _ID;
        protected String _PathFile = string.Empty;
        protected String _FileRoot = string.Empty;
        protected DateTime _Ngaytao;
        protected Int32 _Nguoitao;
        protected Double _Ma_Quangcao;
        public T_FileQuangCao()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String PathFile
        {
            get { return _PathFile; }
            set { _PathFile = value; }
        }
        public String FileRoot
        {
            get { return _FileRoot; }
            set { _FileRoot = value; }
        }
        public DateTime Ngaytao
        {
            get { return _Ngaytao; }
            set { _Ngaytao = value; }
        }
        public Int32 Nguoitao
        {
            get { return _Nguoitao; }
            set { _Nguoitao = value; }
        }
        public Double Ma_Quangcao
        {
            get { return _Ma_Quangcao; }
            set { _Ma_Quangcao = value; }
        }
        #endregion
    }
}
