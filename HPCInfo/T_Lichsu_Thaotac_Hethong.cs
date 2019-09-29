using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Lichsu_Thaotac_Hethong
	{
		#region Member variables and contructor
		protected Double _Log_ID;
		protected Double _Ma_Nguoidung;
		protected String _TenDaydu=string.Empty;
		protected String _HostIP=string.Empty;
		protected DateTime _NgayThaotac;
		protected String _Thaotac=string.Empty;
		protected Double _Ma_Chucnang;
		public T_Lichsu_Thaotac_Hethong()
		{
		}
		#endregion
		#region Public Properties
		public Double Log_ID
		{
				get{return _Log_ID;}
				set{_Log_ID=value;}
		}
		public Double Ma_Nguoidung
		{
				get{return _Ma_Nguoidung;}
				set{_Ma_Nguoidung=value;}
		}
		public String TenDaydu
		{
				get{return _TenDaydu;}
				set{_TenDaydu=value;}
		}
		public String HostIP
		{
				get{return _HostIP;}
				set{_HostIP=value;}
		}
		public DateTime NgayThaotac
		{
				get{return _NgayThaotac;}
				set{_NgayThaotac=value;}
		}
		public String Thaotac
		{
				get{return _Thaotac;}
				set{_Thaotac=value;}
		}
		public Double Ma_Chucnang
		{
				get{return _Ma_Chucnang;}
				set{_Ma_Chucnang=value;}
		}
		#endregion
		}
}
