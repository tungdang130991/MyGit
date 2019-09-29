using System;
using System.Collections.Generic;
using System.Text;

namespace HPCInfo
{
    public class T_News
    {
        #region Member variables and contructor
        protected Double _News_ID;
        protected Int32 _CAT_ID;
        protected Int32 _Lang_ID;
        protected String _News_Sub_Title = string.Empty;
        protected String _News_Tittle = string.Empty;
        protected String _News_Summary = string.Empty;
        protected String _News_Body = string.Empty;
        protected String _News_AuthorName = string.Empty;
        protected Int32 _News_AuthorID;
        protected DateTime _News_DateCreated;
        protected DateTime _News_DateEdit;
        protected Int32 _News_EditorID;
        protected Boolean _News_Lock;
        protected Int32 _News_Status;
        protected Boolean _News_Delete;
        protected Int32 _News_PublishNumber;
        protected Int32 _News_PublishYear;
        protected Int32 _News_CopyFrom;
        protected Int32 _News_AprovedID;
        protected Int32 _News_PublishedID;
        protected DateTime _News_DateApproved;
        protected DateTime _News_DatePublished;
        protected String _News_Comment = string.Empty;
        protected Int32 _News_Priority;
        protected String _News_PhotoAtt = string.Empty;
        protected Int32 _NumberOfRead;
        protected String _Images_Summary = string.Empty;
        protected String _Keywords = string.Empty;
        protected Boolean _News_IsCategorys;
        protected Boolean _News_IsHomePages;
        protected Boolean _News_IsCategoryParrent;
        protected Boolean _News_IsFocus;
        protected Boolean _News_IsHot;
        protected Int32 _News_IsType;
        protected String _News_Realate = string.Empty;
        //
        protected Double _News_TienNB;
        protected Int32 _News_NguoichamNBID;
        protected DateTime _News_Ngaycham;

        protected Boolean _News_IsImages;
        protected Boolean _News_IsVideo;
        protected Boolean _News_IsHistory;
        protected Int32 _News_TacgiaID;
        protected String _News_DescImages = string.Empty;
        protected Boolean _Image_Hot;
        protected String _News_Nguon = string.Empty;
        protected Boolean _News_DisplayMobile;
        protected Int32 _RefID;
        public T_News()
        {
        }
        #endregion
        #region Public Properties
        public Boolean News_IsCategorys
        {
            get { return _News_IsCategorys; }
            set { _News_IsCategorys = value; }
        }
        public Boolean News_IsHomePages
        {
            get { return _News_IsHomePages; }
            set { _News_IsHomePages = value; }
        }
        public Boolean News_IsCategoryParrent
        {
            get { return _News_IsCategoryParrent; }
            set { _News_IsCategoryParrent = value; }
        }
        public Boolean News_IsFocus
        {
            get { return _News_IsFocus; }
            set { _News_IsFocus = value; }
        }
        public Int32 News_IsType
        {
            get { return _News_IsType; }
            set { _News_IsType = value; }
        }
        public Boolean News_IsHot
        {
            get { return _News_IsHot; }
            set { _News_IsHot = value; }
        }
        public String Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }
        public Double News_ID
        {
            get { return _News_ID; }
            set { _News_ID = value; }
        }
        public Int32 CAT_ID
        {
            get { return _CAT_ID; }
            set { _CAT_ID = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public String News_Sub_Title
        {
            get { return _News_Sub_Title; }
            set { _News_Sub_Title = value; }
        }
        public String Images_Summary
        {
            get { return _Images_Summary; }
            set { _Images_Summary = value; }
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
        public Int32 News_AuthorID
        {
            get { return _News_AuthorID; }
            set { _News_AuthorID = value; }
        }
        public DateTime News_DateCreated
        {
            get { return _News_DateCreated; }
            set { _News_DateCreated = value; }
        }
        public DateTime News_DateEdit
        {
            get { return _News_DateEdit; }
            set { _News_DateEdit = value; }
        }
        public Int32 News_EditorID
        {
            get { return _News_EditorID; }
            set { _News_EditorID = value; }
        }
        public Boolean News_Lock
        {
            get { return _News_Lock; }
            set { _News_Lock = value; }
        }
        public Int32 News_Status
        {
            get { return _News_Status; }
            set { _News_Status = value; }
        }
        public Boolean News_Delete
        {
            get { return _News_Delete; }
            set { _News_Delete = value; }
        }
        public Int32 News_PublishNumber
        {
            get { return _News_PublishNumber; }
            set { _News_PublishNumber = value; }
        }
        public Int32 News_PublishYear
        {
            get { return _News_PublishYear; }
            set { _News_PublishYear = value; }
        }
        public Int32 News_CopyFrom
        {
            get { return _News_CopyFrom; }
            set { _News_CopyFrom = value; }
        }
        public Int32 News_AprovedID
        {
            get { return _News_AprovedID; }
            set { _News_AprovedID = value; }
        }
        public Int32 News_PublishedID
        {
            get { return _News_PublishedID; }
            set { _News_PublishedID = value; }
        }
        public DateTime News_DateApproved
        {
            get { return _News_DateApproved; }
            set { _News_DateApproved = value; }
        }
        public DateTime News_DatePublished
        {
            get { return _News_DatePublished; }
            set { _News_DatePublished = value; }
        }
        public String News_Comment
        {
            get { return _News_Comment; }
            set { _News_Comment = value; }
        }
        /// <summary>
        /// Loại tin
        /// 1: Tin bình thường
        /// 2: Tin nổi bật chuyên mục
        /// 3: Tin nổi bật trang chủ
        /// 4: Tin nổi bật chuyên mục cha
        /// </summary>
        public Int32 News_Priority
        {
            get { return _News_Priority; }
            set { _News_Priority = value; }
        }
        public String News_PhotoAtt
        {
            get { return _News_PhotoAtt; }
            set { _News_PhotoAtt = value; }
        }
        public Int32 NumberOfRead
        {
            get { return _NumberOfRead; }
            set { _NumberOfRead = value; }
        }
        public String News_Realate
        {
            get { return _News_Realate; }
            set { _News_Realate = value; }
        }
        public Boolean News_IsImages
        {
            get { return _News_IsImages; }
            set { _News_IsImages = value; }
        }
        public Boolean News_IsVideo
        {
            get { return _News_IsVideo; }
            set { _News_IsVideo = value; }
        }
        public Boolean News_IsHistory
        {
            get { return _News_IsHistory; }
            set { _News_IsHistory = value; }
        }
        public Double News_TienNB
        {
            get { return _News_TienNB; }
            set { _News_TienNB = value; }
        }
        public DateTime News_Ngaycham
        {
            get { return _News_Ngaycham; }
            set { _News_Ngaycham = value; }
        }
        public Int32 News_NguoichamNBID
        {
            get { return _News_NguoichamNBID; }
            set { _News_NguoichamNBID = value; }
        }
        public Int32 News_TacgiaID
        {
            get { return _News_TacgiaID; }
            set { _News_TacgiaID = value; }
        }
        public String News_DescImages
        {
            get { return _News_DescImages; }
            set { _News_DescImages = value; }
        }
        public Boolean Image_Hot
        {
            get { return _Image_Hot; }
            set { _Image_Hot = value; }
        }
        public String News_Nguon
        {
            get { return _News_Nguon; }
            set { _News_Nguon = value; }
        }
        public Boolean News_DisplayMobile
        {
            get { return _News_DisplayMobile; }
            set { _News_DisplayMobile = value; }
        }
        public Int32 RefID
        {
            get { return _RefID; }
            set { _RefID = value; }
        }
        #endregion
    }
}
