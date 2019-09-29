using System;
using System.Collections.Generic;
using System.Text;

namespace HPCInfo
{
    public class T_NewsVersion
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Double _News_ID;
        protected Double _CAT_ID;
        protected Double _Lang_ID;
        protected String _News_Sub_Title = string.Empty;
        protected String _News_Tittle = string.Empty;
        protected String _News_Summary = string.Empty;
        protected String _News_Body = string.Empty;
        protected String _News_AuthorName = string.Empty;
        protected Double _News_UserCreated;
        protected DateTime _News_DateCreated;
        protected DateTime _News_DateModify;
        protected Double _News_UserModify;
        protected Double _News_Status;
        protected Boolean _News_Delete;
        protected Double _News_PublishNumber;
        protected Double _News_PublishYear;
        protected Double _News_CopyFrom;
        protected Double _News_AprovedID;
        protected Double _News_PublishedID;
        protected String _News_Comment = string.Empty;
        protected Double _News_Priority;
        protected String _News_PhotoAtt = string.Empty;
        protected Double _Action;
        protected String _Images_Summary = string.Empty;
        public T_NewsVersion()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Double News_ID
        {
            get { return _News_ID; }
            set { _News_ID = value; }
        }
        public Double CAT_ID
        {
            get { return _CAT_ID; }
            set { _CAT_ID = value; }
        }
        public Double Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public String News_Sub_Title
        {
            get { return _News_Sub_Title; }
            set { _News_Sub_Title = value; }
        }
        public String News_Tittle
        {
            get { return _News_Tittle; }
            set { _News_Tittle = value; }
        }
        public String News_Summary
        {
            get { return _News_Summary; }
            set { _News_Summary = value; }
        }
        public String News_Body
        {
            get { return _News_Body; }
            set { _News_Body = value; }
        }
        public String News_AuthorName
        {
            get { return _News_AuthorName; }
            set { _News_AuthorName = value; }
        }
        public Double News_UserCreated
        {
            get { return _News_UserCreated; }
            set { _News_UserCreated = value; }
        }
        public DateTime News_DateCreated
        {
            get { return _News_DateCreated; }
            set { _News_DateCreated = value; }
        }
        public DateTime News_DateModify
        {
            get { return _News_DateModify; }
            set { _News_DateModify = value; }
        }
        public Double News_UserModify
        {
            get { return _News_UserModify; }
            set { _News_UserModify = value; }
        }
        public Double News_Status
        {
            get { return _News_Status; }
            set { _News_Status = value; }
        }
        public Boolean News_Delete
        {
            get { return _News_Delete; }
            set { _News_Delete = value; }
        }
        public Double News_PublishNumber
        {
            get { return _News_PublishNumber; }
            set { _News_PublishNumber = value; }
        }
        public Double News_PublishYear
        {
            get { return _News_PublishYear; }
            set { _News_PublishYear = value; }
        }
        public Double News_CopyFrom
        {
            get { return _News_CopyFrom; }
            set { _News_CopyFrom = value; }
        }
        public Double News_AprovedID
        {
            get { return _News_AprovedID; }
            set { _News_AprovedID = value; }
        }
        public Double News_PublishedID
        {
            get { return _News_PublishedID; }
            set { _News_PublishedID = value; }
        }
        public String News_Comment
        {
            get { return _News_Comment; }
            set { _News_Comment = value; }
        }
        public Double News_Priority
        {
            get { return _News_Priority; }
            set { _News_Priority = value; }
        }
        public String News_PhotoAtt
        {
            get { return _News_PhotoAtt; }
            set { _News_PhotoAtt = value; }
        }
        public Double Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public String Images_Summary
        {
            get { return _Images_Summary; }
            set { _Images_Summary = value; }
        }
        #endregion
    }
}
