using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Datbao
	{
		#region Member variables and contructor
		protected Int32 _ID;
		protected Int32 _Ma_khachhang;
		protected Int32 _Ma_anpham;
		protected Int32 _Soluong;
		protected DateTime _NGAY_BATDAU;
		protected DateTime _NGAY_KETTHUC;
		protected String _Ghichu=string.Empty;
		protected String _Hopdong_so=string.Empty;
		protected Int32 _Sotien;
		public T_Datbao()
		{
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 Ma_khachhang
		{
				get{return _Ma_khachhang;}
				set{_Ma_khachhang=value;}
		}
		public Int32 Ma_anpham
		{
				get{return _Ma_anpham;}
				set{_Ma_anpham=value;}
		}
		public Int32 Soluong
		{
				get{return _Soluong;}
				set{_Soluong=value;}
		}
		public DateTime NGAY_BATDAU
		{
				get{return _NGAY_BATDAU;}
				set{_NGAY_BATDAU=value;}
		}
		public DateTime NGAY_KETTHUC
		{
				get{return _NGAY_KETTHUC;}
				set{_NGAY_KETTHUC=value;}
		}
		public String Ghichu
		{
				get{return _Ghichu;}
				set{_Ghichu=value;}
		}
		public String Hopdong_so
		{
				get{return _Hopdong_so;}
				set{_Hopdong_so=value;}
		}
		public Int32 Sotien
		{
				get{return _Sotien;}
				set{_Sotien=value;}
		}
		#endregion
		}
}
