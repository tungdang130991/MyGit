using System;
using System.Collections.Generic;
using System.Text;

namespace HPCInfo
{
   [Serializable]
    public  class T_Butdanh
    {
        protected int _BD_ID;
        protected int _UserID;
        protected string _BD_Name;

        public T_Butdanh()
        {
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int BD_ID
        {
            get { return _BD_ID; }
            set { _BD_ID = value; }
        }
        public string BD_Name
        {
            get { return _BD_Name; }
            set { _BD_Name = value; }
        }
    }
}
