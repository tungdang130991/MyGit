using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Vung
	{
		#region Member variables and contructor
		protected Int32 _Ma_Vung;
		protected String _Ten_Vung=string.Empty;
		protected String _Mota=string.Empty;
		public T_Vung()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Vung
		{
				get{return _Ma_Vung;}
				set{_Ma_Vung=value;}
		}
		public String Ten_Vung
		{
				get{return _Ten_Vung;}
				set{_Ten_Vung=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		#endregion
		}
}
