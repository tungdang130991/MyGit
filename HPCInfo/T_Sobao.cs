using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Sobao
	{
		#region Member variables and contructor
		protected Int32 _Ma_Sobao;
		protected String _Ten_Sobao=string.Empty;
		protected String _Mota=string.Empty;
		protected DateTime _Ngay_Xuatban;
		protected Int32 _Ma_AnPham;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int32 _Nguoisua;
		protected DateTime _Ngaysua;
		public T_Sobao()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Sobao
		{
				get{return _Ma_Sobao;}
				set{_Ma_Sobao=value;}
		}
		public String Ten_Sobao
		{
				get{return _Ten_Sobao;}
				set{_Ten_Sobao=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		public DateTime Ngay_Xuatban
		{
				get{return _Ngay_Xuatban;}
				set{_Ngay_Xuatban=value;}
		}
		public Int32 Ma_AnPham
		{
				get{return _Ma_AnPham;}
				set{_Ma_AnPham=value;}
		}
		public Int32 Nguoitao
		{
				get{return _Nguoitao;}
				set{_Nguoitao=value;}
		}
		public DateTime Ngaytao
		{
				get{return _Ngaytao;}
				set{_Ngaytao=value;}
		}
		public Int32 Nguoisua
		{
				get{return _Nguoisua;}
				set{_Nguoisua=value;}
		}
		public DateTime Ngaysua
		{
				get{return _Ngaysua;}
				set{_Ngaysua=value;}
		}
		#endregion
		}
}
