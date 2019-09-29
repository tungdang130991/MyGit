using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Album_Categories
    {
        #region Member variables and contructor
        protected Double _Cat_Album_ID;
        protected String _Cat_Album_Name = string.Empty;
        protected Int32 _Lang_ID;
        protected Boolean _Status;
        protected Int32 _Possition;
        protected String _Links = string.Empty;
        protected String _Cat_AlbumDesc = string.Empty;
        protected Boolean _Target;
        protected Int32 _Copy_From;
        protected DateTime _Cat_Album_DateCreate;
        protected DateTime _DateModify;
        protected Int32 _UserCreated;
        protected Int32 _UserModify;
        protected DateTime _Cat_Album_DateSend;
        protected DateTime _Cat_Album_DateApprove;
        protected Int32 _Cat_Album_UserIDApprove;
        protected Int32 _Cat_Album_Status_Approve;
        protected Int32 _Cat_Album_CATID;
        protected String _Tacgia = string.Empty;
        //protected Int32 _Chatluong;
        protected Int32 _AuthorID;
        //protected Int32 _Theloai;
        //protected Int32 _Loaihinh;
        protected Int32 _TongtienTT;
        //protected Int32 _NguoichamNBID;
        //protected DateTime _NgaychamNB;
        //protected Double _HesoTT;
        protected String _Comment = string.Empty;
        public T_Album_Categories()
        {
        }
        #endregion
        #region Public Properties
        public Double Cat_Album_ID
        {
            get { return _Cat_Album_ID; }
            set { _Cat_Album_ID = value; }
        }
        public String Cat_Album_Name
        {
            get { return _Cat_Album_Name; }
            set { _Cat_Album_Name = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public Boolean Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Int32 Possition
        {
            get { return _Possition; }
            set { _Possition = value; }
        }
        public String Links
        {
            get { return _Links; }
            set { _Links = value; }
        }
        public String Cat_AlbumDesc
        {
            get { return _Cat_AlbumDesc; }
            set { _Cat_AlbumDesc = value; }
        }
        public Boolean Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
        public Int32 Copy_From
        {
            get { return _Copy_From; }
            set { _Copy_From = value; }
        }
        public DateTime Cat_Album_DateCreate
        {
            get { return _Cat_Album_DateCreate; }
            set { _Cat_Album_DateCreate = value; }
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
        public DateTime Cat_Album_DateSend
        {
            get { return _Cat_Album_DateSend; }
            set { _Cat_Album_DateSend = value; }
        }
        public DateTime Cat_Album_DateApprove
        {
            get { return _Cat_Album_DateApprove; }
            set { _Cat_Album_DateApprove = value; }
        }
        public Int32 Cat_Album_UserIDApprove
        {
            get { return _Cat_Album_UserIDApprove; }
            set { _Cat_Album_UserIDApprove = value; }
        }
        public Int32 Cat_Album_Status_Approve
        {
            get { return _Cat_Album_Status_Approve; }
            set { _Cat_Album_Status_Approve = value; }
        }
        public Int32 Cat_Album_CATID
        {
            get { return _Cat_Album_CATID; }
            set { _Cat_Album_CATID = value; }
        }
        public String Tacgia
        {
            get { return _Tacgia; }
            set { _Tacgia = value; }
        }
        //public Int32 Chatluong
        //{
        //        get{return _Chatluong;}
        //        set{_Chatluong=value;}
        //}
        public Int32 TongtienTT
        {
            get { return _TongtienTT; }
            set { _TongtienTT = value; }
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
