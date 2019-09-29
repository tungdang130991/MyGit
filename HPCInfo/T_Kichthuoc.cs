using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Kichthuoc
	{
		#region Member variables and contructor
		protected Int32 _Ma_Kichthuoc;
		protected String _Ten_Kichthuoc=string.Empty;
		protected String _Mota=string.Empty;
		protected Int32 _Ma_Anpham;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int32 _Nguoisua;
		protected DateTime _Ngaysua;
		public T_Kichthuoc()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Kichthuoc
		{
				get{return _Ma_Kichthuoc;}
				set{_Ma_Kichthuoc=value;}
		}
		public String Ten_Kichthuoc
		{
				get{return _Ten_Kichthuoc;}
				set{_Ten_Kichthuoc=value;}
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
