using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Phathanh
	{
		#region Member variables and contructor
		protected Int32 _Id;
		protected Int32 _Ma_Anpham;
		protected Int32 _Ma_Sobao;
		protected DateTime _Ngay_Phathanh;
		protected Int32 _Soluong_PH;
		protected Int32 _Soluong_ton;
		protected String _Ghichu=string.Empty;
		public T_Phathanh()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Id
		{
				get{return _Id;}
				set{_Id=value;}
		}
		public Int32 Ma_Anpham
		{
				get{return _Ma_Anpham;}
				set{_Ma_Anpham=value;}
		}
		public Int32 Ma_Sobao
		{
				get{return _Ma_Sobao;}
				set{_Ma_Sobao=value;}
		}
		public DateTime Ngay_Phathanh
		{
				get{return _Ngay_Phathanh;}
				set{_Ngay_Phathanh=value;}
		}
		public Int32 Soluong_PH
		{
				get{return _Soluong_PH;}
				set{_Soluong_PH=value;}
		}
		public Int32 Soluong_ton
		{
				get{return _Soluong_ton;}
				set{_Soluong_ton=value;}
		}
		public String Ghichu
		{
				get{return _Ghichu;}
				set{_Ghichu=value;}
		}
		#endregion
		}
}
