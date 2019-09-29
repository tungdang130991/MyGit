using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_Publish_QuangCao
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected Int32 _Quangcao_ID;
        protected Int32 _Ma_Loaibao;
        protected Int32 _Ma_Sobao;
        protected Int32 _Trang;
        protected DateTime _Ngaydang;
        protected Int32 _Nguoitao;
        public T_Publish_QuangCao()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Int32 Quangcao_ID
        {
            get { return _Quangcao_ID; }
            set { _Quangcao_ID = value; }
        }
        public Int32 Ma_Loaibao
        {
            get { return _Ma_Loaibao; }
            set { _Ma_Loaibao = value; }
        }
        public Int32 Ma_Sobao
        {
            get { return _Ma_Sobao; }
            set { _Ma_Sobao = value; }
        }
        public Int32 Trang
        {
            get { return _Trang; }
            set { _Trang = value; }
        }
        public DateTime Ngaydang
        {
            get { return _Ngaydang; }
            set { _Ngaydang = value; }
        }
        public Int32 Nguoitao
        {
            get { return _Nguoitao; }
            set { _Nguoitao = value; }
        }
        #endregion
    }
}
