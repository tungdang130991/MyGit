using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Album_Photo
    {
        #region Member variables and contructor
        protected Double _Alb_Photo_ID;
        protected String _Abl_Photo_Name = string.Empty;
        protected String _Abl_Photo_Desc = string.Empty;
        protected String _Abl_Photo_Thumnail = string.Empty;
        protected String _Abl_Photo_Medium = string.Empty;
        protected String _Abl_Photo_Origin = string.Empty;
        protected String _File_Type = string.Empty;
        protected String _File_Size = string.Empty;
        protected String _FileSquare = string.Empty;
        protected String _Authod_Name = string.Empty;
        protected Int32 _Creator;
        protected Int32 _UserModify;
        protected DateTime _Date_Modify;
        protected DateTime _Date_Create;
        protected Int32 _Lang_ID;
        protected Int32 _Cat_Album_ID;
        protected Int32 _Abl_Photo_Status;
        protected Boolean _Abl_Isweek_Photo;
        protected Int32 _Copy_From;
        protected Int32 _OrderByPhoto;
        protected Int32 _AuthorID;
        protected Int32 _TongtienTT;
        public T_Album_Photo()
        {
        }
        #endregion
        #region Public Properties
        public Int32 OrderByPhoto
        {
            get { return _OrderByPhoto; }
            set { _OrderByPhoto = value; }
        }
        public Double Alb_Photo_ID
        {
            get { return _Alb_Photo_ID; }
            set { _Alb_Photo_ID = value; }
        }
        public String Abl_Photo_Name
        {
            get { return _Abl_Photo_Name; }
            set { _Abl_Photo_Name = value; }
        }
        public String Abl_Photo_Desc
        {
            get { return _Abl_Photo_Desc; }
            set { _Abl_Photo_Desc = value; }
        }
        public String Abl_Photo_Thumnail
        {
            get { return _Abl_Photo_Thumnail; }
            set { _Abl_Photo_Thumnail = value; }
        }
        public String Abl_Photo_Medium
        {
            get { return _Abl_Photo_Medium; }
            set { _Abl_Photo_Medium = value; }
        }
        public String Abl_Photo_Origin
        {
            get { return _Abl_Photo_Origin; }
            set { _Abl_Photo_Origin = value; }
        }
        public String File_Type
        {
            get { return _File_Type; }
            set { _File_Type = value; }
        }
        public String File_Size
        {
            get { return _File_Size; }
            set { _File_Size = value; }
        }
        public String FileSquare
        {
            get { return _FileSquare; }
            set { _FileSquare = value; }
        }
        public String Authod_Name
        {
            get { return _Authod_Name; }
            set { _Authod_Name = value; }
        }
        public Int32 Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }
        public Int32 UserModify
        {
            get { return _UserModify; }
            set { _UserModify = value; }
        }
        public DateTime Date_Create
        {
            get { return _Date_Create; }
            set { _Date_Create = value; }
        }
        public Int32 Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public Int32 Cat_Album_ID
        {
            get { return _Cat_Album_ID; }
            set { _Cat_Album_ID = value; }
        }
        public Int32 Abl_Photo_Status
        {
            get { return _Abl_Photo_Status; }
            set { _Abl_Photo_Status = value; }
        }
        public DateTime Date_Modify
        {
            get { return _Date_Modify; }
            set { _Date_Modify = value; }
        }
        public Boolean Abl_Isweek_Photo
        {
            get { return _Abl_Isweek_Photo; }
            set { _Abl_Isweek_Photo = value; }
        }
        public Int32 Copy_From
        {
            get { return _Copy_From; }
            set { _Copy_From = value; }
        }
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
        #endregion
    }
}
