using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
	public class T_File_Dinhkem
	{
		#region Member variables and contructor
		protected Double _ID;
		protected String _Duongdan_File=string.Empty;
		protected String _Ten_file=string.Empty;
		protected String _Dinhdang=string.Empty;
		protected Double _Ma_TinBai;
		public T_File_Dinhkem()
		{
		}
		#endregion
		#region Public Properties
		public Double ID
		{
				get{return _ID;}
				set{_ID=value;}
		}
		public String Duongdan_File
		{
				get{return _Duongdan_File;}
				set{_Duongdan_File=value;}
		}
		public String Ten_file
		{
				get{return _Ten_file;}
				set{_Ten_file=value;}
		}
		public String Dinhdang
		{
				get{return _Dinhdang;}
				set{_Dinhdang=value;}
		}
		public Double Ma_TinBai
		{
				get{return _Ma_TinBai;}
				set{_Ma_TinBai=value;}
		}
		#endregion
		}
}
