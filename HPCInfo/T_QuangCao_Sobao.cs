using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_QuangCao_Sobao
	{
		#region Member variables and contructor
		protected Int32 _ID;
		protected Int32 _Ma_Quangcao;
		protected Double _Ma_Sobao;
		protected String _Trang;
		protected Int32 _Tien;
		protected Int32 _Ma_Kichco;
		public T_QuangCao_Sobao()
		{
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 Ma_Quangcao
		{
				get{return _Ma_Quangcao;}
				set{_Ma_Quangcao=value;}
		}
		public Double Ma_Sobao
		{
				get{return _Ma_Sobao;}
				set{_Ma_Sobao=value;}
		}
		public String Trang
		{
				get{return _Trang;}
				set{_Trang=value;}
		}
		public Int32 Tien
		{
				get{return _Tien;}
				set{_Tien=value;}
		}
		public Int32 Ma_Kichco
		{
				get{return _Ma_Kichco;}
				set{_Ma_Kichco=value;}
		}
		#endregion
		}
}
