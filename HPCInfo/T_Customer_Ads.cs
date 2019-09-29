using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Customer_Ads
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected Int32 _Cust_ID;
        protected String _Cat_ID;
        protected Int32 _Possittion;
        protected DateTime _Start_Date;
        protected DateTime _End_Date;
        protected String _Ads_Images = string.Empty;
        protected String _URL = string.Empty;
        protected Int32 _Target;
        protected int _Status;
        protected Int32 _Lang_ID;
        protected Int32 _Order_Number;
        protected Int32 _AdvType;
        protected String _Height;
        protected String _Width;
        //protected Int32 _ViewNumber;
        protected String _DisplayType;
        protected Int32 _UserCreated;
        protected Int32 _UserApprover;
        protected DateTime _DateCreated;
        protected DateTime _DateApprover;
        protected Int32 _UserModify;
        protected DateTime _DateModify;
        protected String _Ads_ImgVideo = string.Empty;
        public T_Customer_Ads()
        {
        }
        #endregion
        #region Public Properties
        //public Int32 ViewNumber
        //{
        //    get { return _ViewNumber; }
        //    set { _ViewNumber = value; }
        //}
        public String DisplayType
        {
            get { return _DisplayType; }
            set { _DisplayType = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int Cust_ID
        {
            get { return _Cust_ID; }
            set { _Cust_ID = value; }
        }
        public String Cat_ID
        {
            get { return _Cat_ID; }
            set { _Cat_ID = value; }
        }
        public int Possittion
        {
            get { return _Possittion; }
            set { _Possittion = value; }
        }
        public int UserApprover
        {
            get { return _UserApprover; }
            set { _UserApprover = value; }
        }
        public int UserCreated
        {
            get { return _UserCreated; }
            set { _UserCreated = value; }
        }
        public int UserModify
        {
            get { return _UserModify; }
            set { _UserModify = value; }
        }
        public DateTime Start_Date
        {
            get { return _Start_Date; }
            set { _Start_Date = value; }
        }
        public DateTime End_Date
        {
            get { return _End_Date; }
            set { _End_Date = value; }
        }
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        public DateTime DateApprover
        {
            get { return _DateApprover; }
            set { _DateApprover = value; }
        }
        public DateTime DateModify
        {
            get { return _DateModify; }
            set { _DateModify = value; }
        }
        public String Ads_Images
        {
            get { return _Ads_Images; }
            set { _Ads_Images = value; }
        }
        public String URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        public int Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int Lang_ID
        {
            get { return _Lang_ID; }
            set { _Lang_ID = value; }
        }
        public int Order_Number
        {
            get { return _Order_Number; }
            set { _Order_Number = value; }
        }
        public Int32 AdvType
        {
            get { return _AdvType; }
            set { _AdvType = value; }
        }
        public String Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        public String Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public String Ads_ImgVideo
        {
            get { return _Ads_ImgVideo; }
            set { _Ads_ImgVideo = value; }
        }
        #endregion  
    }
}
