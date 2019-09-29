using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_LichsuThanhtoan
	{
		#region Member variables and contructor
		protected Int32 _ID;
		protected Int32 _MA_KHACHHANG;
        protected Int32 _HOPDONG_SO;
		protected Double _SOTIEN;
		protected DateTime _NGAYTHU;
		protected Int32 _NGUOITHU;
		protected String _TENNGUOINOP=string.Empty;
		protected Int16 _Loai;
		public T_LichsuThanhtoan()
		{
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 MA_KHACHHANG
		{
				get{return _MA_KHACHHANG;}
				set{_MA_KHACHHANG=value;}
		}
        public Int32 HOPDONG_SO
		{
				get{return _HOPDONG_SO;}
				set{_HOPDONG_SO=value;}
		}
        public Double SOTIEN
		{
				get{return _SOTIEN;}
				set{_SOTIEN=value;}
		}
		public DateTime NGAYTHU
		{
				get{return _NGAYTHU;}
				set{_NGAYTHU=value;}
		}
		public Int32 NGUOITHU
		{
				get{return _NGUOITHU;}
				set{_NGUOITHU=value;}
		}
		public String TENNGUOINOP
		{
				get{return _TENNGUOINOP;}
				set{_TENNGUOINOP=value;}
		}
		public Int16 Loai
		{
				get{return _Loai;}
				set{_Loai=value;}
		}
		#endregion
		}
}
