using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Multimedia
    {
        #region Member variables and contructor
        protected Double _ID;
        protected String _Tittle = string.Empty;
        protected String _Contents = string.Empty;
        protected String _URLPath = string.Empty;
        protected String _URL_Images = string.Empty;
        protected DateTime _DateCreated;
        protected DateTime _DateModify;
        protected DateTime _DatePublish;
        protected Int32 _UserCreated;
        protected Int32 _UserModify;
        protected Int32 _UserPublish;
        protected Boolean _Display;
        protected Int32 _Languages_ID;
        protected Int32 _Category;
        protected Int32 _NumberOfRead;
        protected Double _Copy_From;
        protected Int32 _Status;
        protected String _Tacgia;
        //protected Int32 _Chatluong;
        protected Int32 _TienNB;
        protected Int32 _AuthorID;
        protected bool _IsLog;
        protected String _Comment = string.Empty;

        public T_Multimedia()
        {
        }
        #endregion
        #region Public Properties
        public Int32 NumberOfRead
        {
            get { return _NumberOfRead; }
            set { _NumberOfRead = value; }
        }
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String Tittle
        {
            get { return _Tittle; }
            set { _Tittle = value; }
        }
        public String Contents
        {
            get { return _Contents; }
            set { _Contents = value; }
        }
        public String URLPath
        {
            get { return _URLPath; }
            set { _URLPath = value; }
        }
        public String URL_Images
        {
            get { return _URL_Images; }
            set { _URL_Images = value; }
        }
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public DateTime DatePublish
        {
            get { return _DatePublish; }
            set { _DatePublish = value; }
        }
        public DateTime DateModify
        {
            get { return _DateModify; }
            set { _DateModify = value; }
        }
        public Int32 UserCreated
        {
            get { return _UserCreated; }
            set { _UserCreated = value; }
        }
        public Int32 UserModify
        {
            get { return _UserModify; }
            set { _UserModify = value; }
        }
        public Int32 UserPublish
        {
            get { return _UserPublish; }
            set { _UserPublish = value; }
        }
        public Boolean Display
        {
            get { return _Display; }
            set { _Display = value; }
        }
        public Int32 Languages_ID
        {
            get { return _Languages_ID; }
            set { _Languages_ID = value; }
        }
        public Int32 Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        public Double Copy_From
        {
            get { return _Copy_From; }
            set { _Copy_From = value; }
        }
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public bool IsLog
        {
            get { return _IsLog; }
            set { _IsLog = value; }
        }
        public String Tacgia
        {
            get { return _Tacgia; }
            set { _Tacgia = value; }
        }
        //public Int32 Chatluong
        //{
        //    get { return _Chatluong; }
        //    set { _Chatluong = value; }
        //}
        public Int32 TienNB
        {
            get { return _TienNB; }
            set { _TienNB = value; }
        }
        public Int32 AuthorID
        {
            get { return _AuthorID; }
            set { _AuthorID = value; }
        }
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        #endregion
    }
}
