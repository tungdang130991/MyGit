using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Vitri_Tinbai
	{
		#region Member variables and contructor
		protected Double _Ma_Vitri;
		protected Int32 _Ma_Sobao;
		protected Int32 _Trang;
		protected Double _Ma_Tinbai;
		protected Double _Trai;
		protected Double _Tren;
		protected Double _Rong;
		protected Double _Dai;
		public T_Vitri_Tinbai()
		{
		}
		#endregion
		#region Public Properties
		public Double Ma_Vitri
		{
				get{return _Ma_Vitri;}
				set{_Ma_Vitri=value;}
		}
		public Int32 Ma_Sobao
		{
				get{return _Ma_Sobao;}
				set{_Ma_Sobao=value;}
		}
		public Int32 Trang
		{
				get{return _Trang;}
				set{_Trang=value;}
		}
		public Double Ma_Tinbai
		{
				get{return _Ma_Tinbai;}
				set{_Ma_Tinbai=value;}
		}
		public Double Trai
		{
				get{return _Trai;}
				set{_Trai=value;}
		}
		public Double Tren
		{
				get{return _Tren;}
				set{_Tren=value;}
		}
		public Double Rong
		{
				get{return _Rong;}
				set{_Rong=value;}
		}
		public Double Dai
		{
				get{return _Dai;}
				set{_Dai=value;}
		}
		#endregion
		}
}
