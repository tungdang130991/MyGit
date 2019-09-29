using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_KhachHang
	{
		#region Member variables and contructor
		protected Int32 _Ma_KhachHang;
		protected String _Ten_KhachHang=string.Empty;
		protected String _DiaChi=string.Empty;
		protected String _SoDienThoai=string.Empty;
		protected String _Email=string.Empty;
		protected String _NguoiDaiDien=string.Empty;
		protected String _Tendaydu=string.Empty;
		protected String _FAX=string.Empty;
		protected String _MaSoThue=string.Empty;
		protected String _Ghichu=string.Empty;
		protected String _TaiKhoan=string.Empty;
		protected Int16 _Loai_KH;
		protected Int32 _Nguoitao;
		protected DateTime _Ngaytao;
		protected Int32 _Nguoisua;
		protected DateTime _Ngaysua;
		public T_KhachHang()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_KhachHang
		{
				get{return _Ma_KhachHang;}
				set{_Ma_KhachHang=value;}
		}
		public String Ten_KhachHang
		{
				get{return _Ten_KhachHang;}
				set{_Ten_KhachHang=value;}
		}
		public String DiaChi
		{
				get{return _DiaChi;}
				set{_DiaChi=value;}
		}
		public String SoDienThoai
		{
				get{return _SoDienThoai;}
				set{_SoDienThoai=value;}
		}
		public String Email
		{
				get{return _Email;}
				set{_Email=value;}
		}
		public String NguoiDaiDien
		{
				get{return _NguoiDaiDien;}
				set{_NguoiDaiDien=value;}
		}
		public String Tendaydu
		{
				get{return _Tendaydu;}
				set{_Tendaydu=value;}
		}
		public String FAX
		{
				get{return _FAX;}
				set{_FAX=value;}
		}
		public String MaSoThue
		{
				get{return _MaSoThue;}
				set{_MaSoThue=value;}
		}
		public String Ghichu
		{
				get{return _Ghichu;}
				set{_Ghichu=value;}
		}
		public String TaiKhoan
		{
				get{return _TaiKhoan;}
				set{_TaiKhoan=value;}
		}
		public Int16 Loai_KH
		{
				get{return _Loai_KH;}
				set{_Loai_KH=value;}
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
