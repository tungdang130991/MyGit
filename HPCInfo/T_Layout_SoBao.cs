using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_Layout_SoBao
	{
		#region Member variables and contructor
		protected Double _ID;
		protected Int32 _Ma_Layout;
		protected Int32 _Ma_SoBao;
		protected Int32 _Trang;
        protected Int32 _Ma_Mau;
		public T_Layout_SoBao()
		{
		}
		#endregion
		#region Public Properties
		public Double ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public Int32 Ma_Layout
		{
				get{return _Ma_Layout;}
				set{_Ma_Layout=value;}
		}
		public Int32 Ma_SoBao
		{
				get{return _Ma_SoBao;}
				set{_Ma_SoBao=value;}
		}
		public Int32 Trang
		{
				get{return _Trang;}
				set{_Trang=value;}
		}
        public Int32 Ma_Mau
        {
            get { return _Ma_Mau; }
            set { _Ma_Mau = value; }
        }
		#endregion
		}
}
