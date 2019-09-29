using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Hopdong
	{
		#region Member variables and contructor
		protected Int32 _ID;
		protected Int32 _Ma_Yeucau;
        protected Int32 _MaKhachHang;
		protected String _hopdongso=string.Empty;
		protected DateTime _ngayky;
		protected String _duongdan_file=string.Empty;
		protected String _Tomtatnoidung=string.Empty;
		protected Double _Sotien;
		protected DateTime _Ngayketthuc;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int16 _loai;
		public T_Hopdong()
		{
		}
		#endregion
		#region Public Properties
		public Int32 ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 Ma_Yeucau
		{
				get{return _Ma_Yeucau;}
				set{_Ma_Yeucau=value;}
		}
        public Int32 Ma_KhachHang
        {
            get { return _MaKhachHang; }
            set { _MaKhachHang = value; }
        }
		public String hopdongso
		{
				get{return _hopdongso;}
				set{_hopdongso=value;}
		}
		public DateTime ngayky
		{
				get{return _ngayky;}
				set{_ngayky=value;}
		}
		public String duongdan_file
		{
				get{return _duongdan_file;}
				set{_duongdan_file=value;}
		}
		public String Tomtatnoidung
		{
				get{return _Tomtatnoidung;}
				set{_Tomtatnoidung=value;}
		}
		public Double Sotien
		{
				get{return _Sotien;}
				set{_Sotien=value;}
		}
		public DateTime Ngayketthuc
		{
				get{return _Ngayketthuc;}
				set{_Ngayketthuc=value;}
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
		public Int16 loai
		{
				get{return _loai;}
				set{_loai=value;}
		}
		#endregion
		}
}
