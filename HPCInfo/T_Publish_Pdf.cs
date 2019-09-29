using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Publish_Pdf
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Double _Publish_Number_ID;
        protected String _Publish_Pdf = string.Empty;
        protected String _Page_Number = string.Empty;
        protected Int16 _Status;
        protected String _Comments = string.Empty;
        public T_Publish_Pdf()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Double Publish_Number_ID
        {
            get { return _Publish_Number_ID; }
            set { _Publish_Number_ID = value; }
        }
        public String Publish_Pdf
        {
            get { return _Publish_Pdf; }
            set { _Publish_Pdf = value; }
        }
        public String Page_Number
        {
            get { return _Page_Number; }
            set { _Page_Number = value; }
        }
        public Int16 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public String Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        #endregion
    }
}
