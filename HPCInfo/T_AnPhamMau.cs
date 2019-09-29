using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_AnPhamMau
	{
		#region Member variables and contructor
		protected Int32 _MA_Mau;
		protected String _Mota=string.Empty;
		protected Int32 _Ma_Anpham;
		public T_AnPhamMau()
		{
		}
		#endregion
		#region Public Properties
		public Int32 MA_Mau
		{
				get{return _MA_Mau;}
				set{_MA_Mau=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		public Int32 Ma_Anpham
		{
				get{return _Ma_Anpham;}
				set{_Ma_Anpham=value;}
		}
		#endregion
		}
}
