using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Idiea
    {
        #region Member variables and contructor
        protected Int32 _Diea_ID;
        protected Int32 _Cat_ID;
        protected String _Title = string.Empty;
        protected String _Comment = string.Empty;
        protected Int32 _User_Created;
        protected Int32 _User_Duyet;
        protected DateTime _Date_Created;
        protected DateTime _Date_Duyet;
        protected Int32 _Status;
        protected Int32 _Lang_ID;
        protected Boolean _Diea_Lock;
        protected Int32 _Diea_Stype;
        protected String _Diea_Articles = string.Empty;
        protected Int32 _User_Edit;
        protected DateTime _Date_Edit;
        protected Int32 _Number;
        protected String _Date_From = string.Empty;
        protected String _Date_To = string.Empty;
        protected Int32 _User_NguoiNhan;
        protected Int32 _CV_id;
        //protected String _CATIDDynamic = string.Empty;
        public T_Idiea()
        {
        }
        #endregion
        #region Public Properties
        public Int32 Diea_ID
        {
            get { return _Diea_ID; }
            set { _Diea_ID = value; }
        }
        public Int32 Cat_ID
        {
            get { return _Cat_ID; }
            set { _Cat_ID = value; }
        }
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        public Int32 User_Created
        {
            get { return _User_Created; }
            set { _User_Created = value; }
        }
        public Int32 User_Duyet
        {
            get { return _User_Duyet; }
            set { _User_Duyet = value; }
        }
        public DateTime Date_Created
        {
            get { return _Date_Created; }
            set { _Date_Created = value; }
        }
        public DateTime Date_Duyet
        {
            get { return _Date_Duyet; }
            set { _Date_Duyet = value; }
        }
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public Boolean Diea_Lock
        {
            get { return _Diea_Lock; }
            set { _Diea_Lock = value; }
        }
        public Int32 Diea_Stype
        {
            get { return _Diea_Stype; }
            set { _Diea_Stype = value; }
        }
        public String Diea_Articles
        {
            get { return _Diea_Articles; }
            set { _Diea_Articles = value; }
        }
        public Int32 User_Edit
        {
            get { return _User_Edit; }
            set { _User_Edit = value; }
        }
        public DateTime Date_Edit
        {
            get { return _Date_Edit; }
            set { _Date_Edit = value; }
        }
        public Int32 Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        public String Date_From
        {
            get { return _Date_From; }
            set { _Date_From = value; }
        }
        public String Date_To
        {
            get { return _Date_To; }
            set { _Date_To = value; }
        }
        public Int32 User_NguoiNhan
        {
            get { return _User_NguoiNhan; }
            set { _User_NguoiNhan = value; }
        }
        public Int32 CV_id
        {
            get { return _CV_id; }
            set { _CV_id = value; }
        }
        
        #endregion
    }
}
