using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_AnPhamMau_Layout
	{
		#region Member variables and contructor
		protected Int32 _ID;
		protected Int32 _Ma_layout;
		protected Int32 _Ma_Mau;
		protected Int32 _Trang;
		public T_AnPhamMau_Layout()
		{
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 Ma_layout
		{
				get{return _Ma_layout;}
				set{_Ma_layout=value;}
		}
		public Int32 Ma_Mau
		{
				get{return _Ma_Mau;}
				set{_Ma_Mau=value;}
		}
		public Int32 Trang
		{
				get{return _Trang;}
				set{_Trang=value;}
		}
		#endregion
		}
}
