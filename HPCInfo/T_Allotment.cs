using System; 
using System.Collections.Generic;
using System.Text;
 namespace HPCInfo
 {
	public class T_Allotments
	{
		#region Member variables and contructor
		protected Int32 _ID; 
		protected Int32 _Idiea_ID; 
		protected Int32 _Lang_ID; 
		protected Int32 _User_Created; 
		protected Int32 _User_Duyet; 
		protected DateTime _Date_Created; 
		protected DateTime _Date_Duyet; 
		protected Int32 _Type; 
		protected String _Request = string.Empty; 
		protected Int32 _Numbers; 
		protected String _Date_start = string.Empty; 
		protected String _Date_End = string.Empty; 
		protected Boolean _Lock; 
		protected Int32 _Status;
        protected Int32 _User_NguoiNhan;
        protected Int32 _Cat_ID;
        protected String _Title = string.Empty;
		
		public T_Allotments()
		{ 
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{ 
			 get {return _ID; }
			 set { _ID = value; }
		}
		public Int32 Idiea_ID
		{ 
			 get {return _Idiea_ID; }
			 set { _Idiea_ID = value; }
		}
		public Int32 Lang_ID
		{ 
			 get {return _Lang_ID; }
			 set { _Lang_ID = value; }
		}
		public Int32 User_Created
		{ 
			 get {return _User_Created; }
			 set { _User_Created = value; }
		}
		public Int32 User_Duyet
		{ 
			 get {return _User_Duyet; }
			 set { _User_Duyet = value; }
		}
		public DateTime Date_Created
		{ 
			 get {return _Date_Created; }
			 set { _Date_Created = value; }
		}
		public DateTime Date_Duyet
		{ 
			 get {return _Date_Duyet; }
			 set { _Date_Duyet = value; }
		}
		public Int32 Type
		{ 
			 get {return _Type; }
			 set { _Type = value; }
		}
		public String Request
		{ 
			 get {return _Request; }
			 set { _Request = value; }
		}
		public Int32 Numbers
		{ 
			 get {return _Numbers; }
			 set { _Numbers = value; }
		}
		public String Date_start
		{ 
			 get {return _Date_start; }
			 set { _Date_start = value; }
		}
		public String Date_End
		{ 
			 get {return _Date_End; }
			 set { _Date_End = value; }
		}
		public Boolean Lock
		{ 
			 get {return _Lock; }
			 set { _Lock = value; }
		}
		public Int32 Status
		{ 
			 get {return _Status; }
			 set { _Status = value; }
		}
        public Int32 User_NguoiNhan
        {
            get { return _User_NguoiNhan; }
            set { _User_NguoiNhan = value; }
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

		#endregion
	}
}
