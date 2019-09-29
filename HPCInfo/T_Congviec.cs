using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class T_Congviec
    {
        #region Member variables and contructor
        protected Double _Ma_Congviec;
        protected String _Tencongviec = string.Empty;
        protected String _Noidung_Congviec = string.Empty;
        protected Int32 _Sotu;
        protected DateTime _NgayTao;
        protected DateTime _NgayHoanthanh;
        protected Double _NguoiNhan;
        protected String _TenNguoiNhan = string.Empty;
        protected Double _NguoiTao;
        protected Double _NguoiGiaoViec;
        protected String _Vet = string.Empty;
        protected Int32 _Loai; // 1 = viet bai; 0 = cv khac
        protected Int32 _Status; //0= chua hoan thanh; 1= hoan thanh
        protected Int32 _Phong_ID;
        protected String _Attachfile = string.Empty;
        public T_Congviec()
        {
        }
        #endregion
        #region Public Properties
        public Double Ma_Congviec
        {
            get { return _Ma_Congviec; }
            set { _Ma_Congviec = value; }
        }
        public String Tencongviec
        {
            get { return _Tencongviec; }
            set { _Tencongviec = value; }
        }
        public String Noidung_Congviec
        {
            get { return _Noidung_Congviec; }
            set { _Noidung_Congviec = value; }
        }
        public Int32 Sotu
        {
            get { return _Sotu; }
            set { _Sotu = value; }
        }
        public DateTime NgayTao
        {
            get { return _NgayTao; }
            set { _NgayTao = value; }
        }
        public DateTime NgayHoanthanh
        {
            get { return _NgayHoanthanh; }
            set { _NgayHoanthanh = value; }
        }
        public Double NguoiNhan
        {
            get { return _NguoiNhan; }
            set { _NguoiNhan = value; }
        }
        public String TenNguoiNhan
        {
            get { return _TenNguoiNhan; }
            set { _TenNguoiNhan = value; }
        }
        public Double NguoiTao
        {
            get { return _NguoiTao; }
            set { _NguoiTao = value; }
        }
        public Double NguoiGiaoViec
        {
            get { return _NguoiGiaoViec; }
            set { _NguoiGiaoViec = value; }
        }
        public String Vet
        {
            get { return _Vet; }
            set { _Vet = value; }
        }
        public Int32 Loai
        {
            get { return _Loai; }
            set { _Loai = value; }
        }
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Int32 Phong_ID
        {
            get { return _Phong_ID; }
            set { _Phong_ID = value; }
        }
        public String Attachfile
        {
            get { return _Attachfile; }
            set { _Attachfile = value; }
        }
        #endregion
    }
}
