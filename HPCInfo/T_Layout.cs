using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Layout
	{
		#region Member variables and contructor
		protected Int32 _Ma_Layout;
		protected String _Mota=string.Empty;
		protected Double _ChieuRong;
		protected Double _Chieudai;
		public T_Layout()
		{
		}
		#endregion
		#region Public Properties
		public Int32 Ma_Layout
		{
				get{return _Ma_Layout;}
				set{_Ma_Layout=value;}
		}
		public String Mota
		{
				get{return _Mota;}
				set{_Mota=value;}
		}
		public Double ChieuRong
		{
				get{return _ChieuRong;}
				set{_ChieuRong=value;}
		}
		public Double Chieudai
		{
				get{return _Chieudai;}
				set{_Chieudai=value;}
		}
		#endregion
		}
}
