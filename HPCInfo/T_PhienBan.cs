using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_PhienBan
	{
		#region Member variables and contructor
		protected Double _Ma_Phienban;
		protected Double _Ma_Tinbai;
		protected Double _Ma_Chuyenmuc;
		protected Int32 _Ma_Anpham;
		protected String _Tieude=string.Empty;
		protected String _Tomtat=string.Empty;
		protected String _Noidung=string.Empty;
		protected String _TacGia=string.Empty;
		protected Double _Ma_TacGia;
		protected DateTime _NgayTao;
		protected Double _Ma_Nguoitao;
		protected Double _Trangthai;
		protected String _GhiChu=string.Empty;
		protected String _Doituong_DangXuly=string.Empty;
		protected Int32 _Sotu;
        protected Double _Tiennhuanbut;
        protected String _PathFileDocuments = string.Empty;
		public T_PhienBan()
		{
		}
		#endregion
		#region Public Properties
		public Double Ma_Phienban
		{
				get{return _Ma_Phienban;}
				set{_Ma_Phienban=value;}
		}
		public Double Ma_Tinbai
		{
				get{return _Ma_Tinbai;}
				set{_Ma_Tinbai=value;}
		}
		public Double Ma_Chuyenmuc
		{
				get{return _Ma_Chuyenmuc;}
				set{_Ma_Chuyenmuc=value;}
		}
		public Int32 Ma_Anpham
		{
				get{return _Ma_Anpham;}
				set{_Ma_Anpham=value;}
		}
		public String Tieude
		{
				get{return _Tieude;}
				set{_Tieude=value;}
		}
		public String Tomtat
		{
				get{return _Tomtat;}
				set{_Tomtat=value;}
		}
		public String Noidung
		{
				get{return _Noidung;}
				set{_Noidung=value;}
		}
		public String TacGia
		{
				get{return _TacGia;}
				set{_TacGia=value;}
		}
		public Double Ma_TacGia
		{
				get{return _Ma_TacGia;}
				set{_Ma_TacGia=value;}
		}
		public DateTime NgayTao
		{
				get{return _NgayTao;}
				set{_NgayTao=value;}
		}
		public Double Ma_Nguoitao
		{
				get{return _Ma_Nguoitao;}
				set{_Ma_Nguoitao=value;}
		}
		public Double Trangthai
		{
				get{return _Trangthai;}
				set{_Trangthai=value;}
		}
		public String GhiChu
		{
				get{return _GhiChu;}
				set{_GhiChu=value;}
		}
		public String Doituong_DangXuly
		{
				get{return _Doituong_DangXuly;}
				set{_Doituong_DangXuly=value;}
		}
		public Int32 Sotu
		{
				get{return _Sotu;}
				set{_Sotu=value;}
		}
        public Double Tiennhuanbut
        {
            get { return _Tiennhuanbut; }
            set { _Tiennhuanbut = value; }
        }
        public String PathFileDocuments
        {
            get { return _PathFileDocuments; }
            set { _PathFileDocuments = value; }
        }
		#endregion
		}
}
