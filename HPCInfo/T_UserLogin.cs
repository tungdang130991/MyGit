using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_UserLogin
    {
        #region Member variables and contructor
        protected Double _ID;
        protected DateTime _Login_Date;
        protected String _Session_ID = string.Empty;
        protected String _User_Name = string.Empty;
        protected String _Full_Name = string.Empty;
        protected DateTime _LastMessage_Time;
        protected String _User_Ipaddress = string.Empty;
        protected Boolean _Loggedin;
        public T_UserLogin()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public DateTime Login_Date
        {
            get { return _Login_Date; }
            set { _Login_Date = value; }
        }
        public String Session_ID
        {
            get { return _Session_ID; }
            set { _Session_ID = value; }
        }
        public String User_Name
        {
            get { return _User_Name; }
            set { _User_Name = value; }
        }
        public String Full_Name
        {
            get { return _Full_Name; }
            set { _Full_Name = value; }
        }
        public DateTime LastMessage_Time
        {
            get { return _LastMessage_Time; }
            set { _LastMessage_Time = value; }
        }
        public String User_Ipaddress
        {
            get { return _User_Ipaddress; }
            set { _User_Ipaddress = value; }
        }
        public Boolean Loggedin
        {
            get { return _Loggedin; }
            set { _Loggedin = value; }
        }
        #endregion
    }
}
