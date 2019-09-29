using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Quytrinh
	{
		#region Member variables and contructor
		protected Double _ID;
		protected String _Ma_Doituong_Gui=string.Empty;
		protected String _Ma_Doituong_Nhan=string.Empty;
		protected Int32 _Ma_AnPham;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int32 _Nguoisua;
		protected DateTime _Ngaysua;
		public T_Quytrinh()
		{
		}
		#endregion
		#region Public Properties
		public Double ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public String Ma_Doituong_Gui
		{
				get{return _Ma_Doituong_Gui;}
				set{_Ma_Doituong_Gui=value;}
		}
		public String Ma_Doituong_Nhan
		{
				get{return _Ma_Doituong_Nhan;}
				set{_Ma_Doituong_Nhan=value;}
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
