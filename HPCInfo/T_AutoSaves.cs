using System;
using System.Collections.Generic;
using System.Text;
namespace HPCInfo
{
    public class T_AutoSaves
    {
        #region Member variables and contructor
        protected Double _ID;
        protected Int32 _USER_ID;
        protected int _UserCreated;
        protected int _UserModify;
        protected Int32 _CAT_ID;
        protected String _Tittle = string.Empty;
        protected String _BodySaves = string.Empty;
        protected DateTime _DateCreated;
        protected DateTime _DateModify;
        protected String _Summary;
        protected String _Images_Summary;
        protected String _Sub_Title;
        protected String _Keywords;
        protected int _Lang_ID;
        //protected int _Priority;
        //protected int _IsType;
        protected String _Comment;
        protected int _News_ID;
        protected int _Status;
        protected String _News_AuthorName = string.Empty;
        protected Int32 _News_IsFocus;
        protected Int32 _News_IsHot;
        protected Int32 _News_IsImages;
        protected Int32 _News_IsVideo;
        protected Int32 _News_IsHistory;
        protected Int32 _News_IsCategorys;
        protected Int32 _News_IsHomePages;
        protected Int32 _News_IsCategoryParrent;
        protected Double _News_TienNB;

        public T_AutoSaves()
        {
        }
        #endregion
        #region Public Properties
        public Double ID
        {
            get { return _ID; }
            set { _ID = value; }
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
        public Int32 USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
        public Int32 CAT_ID
        {
            get { return _CAT_ID; }
            set { _CAT_ID = value; }
        }
        public String Tittle
        {
            get { return _Tittle; }
            set { _Tittle = value; }
        }
        public String BodySaves
        {
            get { return _BodySaves; }
            set { _BodySaves = value; }
        }
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public DateTime DateModify
        {
            get { return _DateModify; }
            set { _DateModify = value; }
        }
        public String Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }
        public String Images_Summary
        {
            get { return _Images_Summary; }
            set { _Images_Summary = value; }
        }
        public String Sub_Title
        {
            get { return _Sub_Title; }
            set { _Sub_Title = value; }
        }
        public String Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        //public Int32 Priority
        //{
        //    get { return _Priority; }
        //    set { _Priority = value; }
        //}
        //public Int32 IsType
        //{
        //    get { return _IsType; }
        //    set { _IsType = value; }
        //}
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        public Int32 News_ID
        {
            get { return _News_ID; }
            set { _News_ID = value; }
        }
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public String News_AuthorName
        {
            get { return _News_AuthorName; }
            set { _News_AuthorName = value; }
        }
        public Int32 News_IsFocus
        {
            get { return _News_IsFocus; }
            set { _News_IsFocus = value; }
        }
        public Int32 News_IsHot
        {
            get { return _News_IsHot; }
            set { _News_IsHot = value; }
        }
        public Int32 News_IsImages
        {
            get { return _News_IsImages; }
            set { _News_IsImages = value; }
        }
        public Int32 News_IsVideo
        {
            get { return _News_IsVideo; }
            set { _News_IsVideo = value; }
        }
        public Int32 News_IsHistory
        {
            get { return _News_IsHistory; }
            set { _News_IsHistory = value; }
        }
        public Int32 News_IsCategorys
        {
            get { return _News_IsCategorys; }
            set { _News_IsCategorys = value; }
        }
        public Int32 News_IsHomePages
        {
            get { return _News_IsHomePages; }
            set { _News_IsHomePages = value; }
        }
        public Int32 News_IsCategoryParrent
        {
            get { return _News_IsCategoryParrent; }
            set { _News_IsCategoryParrent = value; }
        }
        public Double News_TienNB
        {
            get { return _News_TienNB; }
            set { _News_TienNB = value; }
        }
        #endregion
    }
}