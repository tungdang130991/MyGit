using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Phongban
	{
		#region Member variables and contructor
		protected Int32 _Ma_Phongban;
		protected String _Ten_Phongban=string.Empty;
		protected String _Mota=string.Empty;
		public T_Phongban()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Phongban
		{
				get{return _Ma_Phongban;}
				set{_Ma_Phongban=value;}
		}
		public String Ten_Phongban
		{
				get{return _Ten_Phongban;}
				set{_Ten_Phongban=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		#endregion
		}
}
