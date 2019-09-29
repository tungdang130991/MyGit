using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_Ten_Quytrinh
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected String _Ten_Quytrinh = string.Empty;
        protected String _Mota = string.Empty;
        protected String _Url_Img = string.Empty;
        protected Boolean _Active;
        public T_Ten_Quytrinh()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String Ten_Quytrinh
        {
            get { return _Ten_Quytrinh; }
            set { _Ten_Quytrinh = value; }
        }
        public String Mota
        {
            get { return _Mota; }
            set { _Mota = value; }
        }
        public String Url_Img
        {
            get { return _Url_Img; }
            set { _Url_Img = value; }
        }
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        #endregion
    }
}
