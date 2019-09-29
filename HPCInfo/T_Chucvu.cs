using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Chucvu
	{
		#region Member variables and contructor
		protected Int32 _Ma_Chucvu;
		protected String _Chucvu=string.Empty;
		protected String _Mota=string.Empty;
		public T_Chucvu()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Chucvu
		{
				get{return _Ma_Chucvu;}
				set{_Ma_Chucvu=value;}
		}
		public String Chucvu
		{
				get{return _Chucvu;}
				set{_Chucvu=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		#endregion
		}
}
