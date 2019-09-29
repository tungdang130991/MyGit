using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Logo
    {
        protected Int32 _Ma_Logo;
        protected String _Ten_Logo = string.Empty;
        protected String _FileName_Logo = string.Empty;
        protected String _Path_Logo = string.Empty;
        public T_Logo()
        {
        }
        public Int32 Ma_Logo
        {
            get { return _Ma_Logo; }
            set { _Ma_Logo = value; }
        }
        public String Ten_Logo
        {
            get { return _Ten_Logo; }
            set { _Ten_Logo = value; }
        }
        public String FileName_Logo
        {
            get { return _FileName_Logo; }
            set { _FileName_Logo = value; }
        }
        public String Path_Logo
        {
            get { return _Path_Logo; }
            set { _Path_Logo = value; }
        }
    }
}
