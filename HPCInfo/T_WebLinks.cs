using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_WebLinks
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected String _URL = string.Empty;
        protected String _Tittle = string.Empty;
        protected String _Description = string.Empty;
        protected Int32 _Lang_ID;
        protected String _Logo = string.Empty;
        protected Int32 _OrderLinks;
        protected Int32 _IsType;
        public T_WebLinks()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        public String Tittle
        {
            get { return _Tittle; }
            set { _Tittle = value; }
        }
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public String Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }
        public Int32 OrderLinks
        {
            get { return _OrderLinks; }
            set { _OrderLinks = value; }
        }
        public Int32 IsType
        {
            get { return _IsType; }
            set { _IsType = value; }
        }
        #endregion
    }
}
