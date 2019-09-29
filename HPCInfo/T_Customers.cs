using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Customers
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected String _Name = string.Empty;
        protected String _Address = string.Empty;
        protected String _Phone = string.Empty;
        protected String _Fax = string.Empty;
        protected String _Email = string.Empty;
        protected DateTime _Date_Created;
        protected Int32 _User_Created;
        protected DateTime _Date_Modify;
        protected Int32 _User_Modify;
        public T_Customers()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public String Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public String Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public DateTime Date_Created
        {
            get { return _Date_Created; }
            set { _Date_Created = value; }
        }
        public Int32 User_Created
        {
            get { return _User_Created; }
            set { _User_Created = value; }
        }
        public DateTime Date_Modify
        {
            get { return _Date_Modify; }
            set { _Date_Modify = value; }
        }
        public Int32 User_Modify
        {
            get { return _User_Modify; }
            set { _User_Modify = value; }
        }
        #endregion
    }
}
