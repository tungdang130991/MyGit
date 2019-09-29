using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_TinBai
    {
        #region Member variables and contructor
        protected Double _Ma_Tinbai;
        protected Int32 _Ma_Chuyenmuc;
        protected Int32 _Ma_Anpham;
        protected Int32 _Ma_Sobao;
        protected Int32 _Ma_NgonNgu;
        protected String _Tieude = string.Empty;
        protected String _Tomtat = string.Empty;
        protected String _Noidung = string.Empty;
        protected String _TacGia = string.Empty;
        protected Double _Ma_TacGia;
        protected DateTime _NgayTao;
        protected Double _Ma_Nguoitao;
        protected Double _Nguoi_Khoa;
        protected Double _Trangthai;
        protected Boolean _Trangthai_Xoa;
        protected String _GhiChu = string.Empty;
        protected String _Doituong_DangXuly = string.Empty;
        protected String _LuuVet = string.Empty;
        protected Double _Sotu;
        protected Double _CopyFrom;
        protected Double _Tiennhuanbut;
        protected String _PathFileDocuments = string.Empty;
        protected Int32 _Ma_QTBT;
        protected Boolean _Bizhub;
        protected Boolean _VietNamNews;
        protected Int32 _UpdateContents;
        public T_TinBai()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_Tinbai
        {
            get { return _Ma_Tinbai; }
            set { _Ma_Tinbai = value; }
        }
        public Int32 Ma_Chuyenmuc
        {
            get { return _Ma_Chuyenmuc; }
            set { _Ma_Chuyenmuc = value; }
        }
        public Int32 Ma_Anpham
        {
            get { return _Ma_Anpham; }
            set { _Ma_Anpham = value; }
        }
        public Int32 Ma_Sobao
        {
            get { return _Ma_Sobao; }
            set { _Ma_Sobao = value; }
        }
        public Int32 Ma_NgonNgu
        {
            get { return _Ma_NgonNgu; }
            set { _Ma_NgonNgu = value; }
        }
        public String Tieude
        {
            get { return _Tieude; }
            set { _Tieude = value; }
        }
        public String Tomtat
        {
            get { return _Tomtat; }
            set { _Tomtat = value; }
        }
        public String Noidung
        {
            get { return _Noidung; }
            set { _Noidung = value; }
        }
        public String TacGia
        {
            get { return _TacGia; }
            set { _TacGia = value; }
        }
        public Double Ma_TacGia
        {
            get { return _Ma_TacGia; }
            set { _Ma_TacGia = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public Double Ma_Nguoitao
        {
            get { return _Ma_Nguoitao; }
            set { _Ma_Nguoitao = value; }
        }
        public Double Nguoi_Khoa
        {
            get { return _Nguoi_Khoa; }
            set { _Nguoi_Khoa = value; }
        }
        public Double Trangthai
        {
            get { return _Trangthai; }
            set { _Trangthai = value; }
        }
        public Boolean Trangthai_Xoa
        {
            get { return _Trangthai_Xoa; }
            set { _Trangthai_Xoa = value; }
        }
        public String GhiChu
        {
            get { return _GhiChu; }
            set { _GhiChu = value; }
        }
        public String Doituong_DangXuly
        {
            get { return _Doituong_DangXuly; }
            set { _Doituong_DangXuly = value; }
        }
        public String LuuVet
        {
            get { return _LuuVet; }
            set { _LuuVet = value; }
        }
        public Double Sotu
        {
            get { return _Sotu; }
            set { _Sotu = value; }
        }
        public Double CopyFrom
        {
            get { return _CopyFrom; }
            set { _CopyFrom = value; }
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
        public Int32 Ma_QTBT
        {
            get { return _Ma_QTBT; }
            set { _Ma_QTBT = value; }
        }
        public Boolean Bizhub
        {
            get { return _Bizhub; }
            set { _Bizhub = value; }
        }
        public Boolean VietNamNews
        {
            get { return _VietNamNews; }
            set { _VietNamNews = value; }
        }
        public Int32 UpdateContents
        {
            get { return _UpdateContents; }
            set { _UpdateContents = value; }
        }
        #endregion
    }
}
