using System;
using System.Collections.Generic;
using System.Text;

namespace HPCInfo
{
    public class T_Photo_Event
    {
        #region Member variables and contructor
        protected Double _Photo_ID;
        protected String _Photo_Name = string.Empty;
        protected String _Photo_Desc = string.Empty;
        protected String _Photo_Thumnail = string.Empty;
        protected String _Photo_Medium = string.Empty;
        protected String _File_Type = string.Empty;
        protected String _File_Size = string.Empty;
        protected String _FileSquare = string.Empty;
        protected String _Author_Name = string.Empty;
        protected Double _Creator;
        protected DateTime _Date_Create;
        protected Double _Lang_ID;
        protected Double _Photo_Status;
        protected DateTime _Date_Update;
        protected Double _Photo_Display;
        protected Double _Copy_From;
        protected Int32 _TienNB;
        protected Int32 _AuthorID;
        //protected Int32 _UserEditor;
        public T_Photo_Event()
        {
        }
        #endregion
        #region Public Properties
        public Double Photo_ID
        {
            get { return _Photo_ID; }
            set { _Photo_ID = value; }
        }
        public String Photo_Name
        {
            get { return _Photo_Name; }
            set { _Photo_Name = value; }
        }
        public String Photo_Desc
        {
            get { return _Photo_Desc; }
            set { _Photo_Desc = value; }
        }
        public String Photo_Thumnail
        {
            get { return _Photo_Thumnail; }
            set { _Photo_Thumnail = value; }
        }
        public String Photo_Medium
        {
            get { return _Photo_Medium; }
            set { _Photo_Medium = value; }
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
        public String Author_Name
        {
            get { return _Author_Name; }
            set { _Author_Name = value; }
        }
        public Double Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }
        public DateTime Date_Create
        {
            get { return _Date_Create; }
            set { _Date_Create = value; }
        }
        public Double Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public Double Photo_Status
        {
            get { return _Photo_Status; }
            set { _Photo_Status = value; }
        }
        public DateTime Date_Update
        {
            get { return _Date_Update; }
            set { _Date_Update = value; }
        }
        public Double Photo_Display
        {
            get { return _Photo_Display; }
            set { _Photo_Display = value; }
        }
        public Double Copy_From
        {
            get { return _Copy_From; }
            set { _Copy_From = value; }
        }
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
        //public Int32 UserEditor
        //{
        //    get { return _UserEditor; }
        //    set { _UserEditor = value; }
        //}
        #endregion
    }
}
