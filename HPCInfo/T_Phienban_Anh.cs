using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Phienban_Anh
	{
		#region Member variables and contructor
		protected Double _ID;
		protected Double _Ma_Phienban;
		protected Double _Ma_Anh;
		protected String _ChuThich=string.Empty;
		public T_Phienban_Anh()
		{
		}
		#endregion
		#region Public Properties
		public Double ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Double Ma_Phienban
		{
				get{return _Ma_Phienban;}
				set{_Ma_Phienban=value;}
		}
		public Double Ma_Anh
		{
				get{return _Ma_Anh;}
				set{_Ma_Anh=value;}
		}
		public String ChuThich
		{
				get{return _ChuThich;}
				set{_ChuThich=value;}
		}
		#endregion
		}
}
