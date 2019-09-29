using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_AnPham
	{
		#region Member variables and contructor
		protected Int32 _Ma_AnPham;
		protected String _Ten_AnPham=string.Empty;
		protected String _Mota=string.Empty;
		protected Int16 _Sotrang;
		protected String _Url_Img=string.Empty;
		protected Boolean _Trangthai;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int32 _Nguoisua;
		protected DateTime _Ngaysua;
		public T_AnPham()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_AnPham
		{
				get{return _Ma_AnPham;}
				set{_Ma_AnPham=value;}
		}
		public String Ten_AnPham
		{
				get{return _Ten_AnPham;}
				set{_Ten_AnPham=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		public Int16 Sotrang
		{
				get{return _Sotrang;}
				set{_Sotrang=value;}
		}
		public String Url_Img
		{
				get{return _Url_Img;}
				set{_Url_Img=value;}
		}
		public Boolean Trangthai
		{
				get{return _Trangthai;}
				set{_Trangthai=value;}
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
