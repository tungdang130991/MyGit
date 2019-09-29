using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_ChuyenMuc
    {
        #region Member variables and contructor
        protected Int32 _Ma_ChuyenMuc;
        protected String _Ten_ChuyenMuc = string.Empty;
        protected String _Anh_ChuyenMuc = string.Empty;
        protected Int32 _Ma_Chuyenmuc_Cha;
        protected DateTime _NgayTao;
        protected Boolean _HoatDong;
        protected Int32 _Ma_AnPham;
        protected Int32 _Nguoitao;
        protected Int32 _Nguoisua;
        protected DateTime _Ngaysua;
        protected Int32 _ThuTuHienThi;
        protected Boolean _HienThi_BaoIn;
        protected Boolean _HienThi_BDT;
        protected Boolean _ChuyenDe;
        protected Boolean _HienThi_RSS;
        protected Boolean _HienThiTrai;
        protected Boolean _HienThiPhai;
        protected Boolean _HienThiGiua;
        protected Boolean _HienThiDuoi;
        protected Boolean _HienThiTren;
        protected Boolean _HienThi;
        public T_ChuyenMuc()
        {
        }
        #endregion
        #region Public Properties
        public Int32 Ma_ChuyenMuc
        {
            get { return _Ma_ChuyenMuc; }
            set { _Ma_ChuyenMuc = value; }
        }
        public String Ten_ChuyenMuc
        {
            get { return _Ten_ChuyenMuc; }
            set { _Ten_ChuyenMuc = value; }
        }
        public String Anh_ChuyenMuc
        {
            get { return _Anh_ChuyenMuc; }
            set { _Anh_ChuyenMuc = value; }
        }
        public Int32 Ma_Chuyenmuc_Cha
        {
            get { return _Ma_Chuyenmuc_Cha; }
            set { _Ma_Chuyenmuc_Cha = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Boolean HoatDong
        {
            get { return _HoatDong; }
            set { _HoatDong = value; }
        }
        public Boolean HienThi
        {
            get { return _HienThi; }
            set { _HienThi = value; }
        }
        public Int32 Ma_AnPham
        {
            get { return _Ma_AnPham; }
            set { _Ma_AnPham = value; }
        }
        public Int32 Nguoitao
        {
            get { return _Nguoitao; }
            set { _Nguoitao = value; }
        }
        public Int32 Nguoisua
        {
            get { return _Nguoisua; }
            set { _Nguoisua = value; }
        }
        public DateTime Ngaysua
        {
            get { return _Ngaysua; }
            set { _Ngaysua = value; }
        }
        public Int32 ThuTuHienThi
        {
            get { return _ThuTuHienThi; }
            set { _ThuTuHienThi = value; }
        }
        public Boolean HienThi_BaoIn
        {
            get { return _HienThi_BaoIn; }
            set { _HienThi_BaoIn = value; }
        }
        public Boolean HienThi_BDT
        {
            get { return _HienThi_BDT; }
            set { _HienThi_BDT = value; }
        }
        public Boolean ChuyenDe
        {
            get { return _ChuyenDe; }
            set { _ChuyenDe = value; }
        }
        public Boolean HienThi_RSS
        {
            get { return _HienThi_RSS; }
            set { _HienThi_RSS = value; }
        }
        public Boolean HienThiTrai
        {
            get { return _HienThiTrai; }
            set { _HienThiTrai = value; }
        }
        public Boolean HienThiPhai
        {
            get { return _HienThiPhai; }
            set { _HienThiPhai = value; }
        }
        public Boolean HienThiGiua
        {
            get { return _HienThiGiua; }
            set { _HienThiGiua = value; }
        }
        public Boolean HienThiDuoi
        {
            get { return _HienThiDuoi; }
            set { _HienThiDuoi = value; }
        }
        public Boolean HienThiTren
        {
            get { return _HienThiTren; }
            set { _HienThiTren = value; }
        }
        #endregion
    }
}
