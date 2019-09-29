using System;
using System.Text;
using System.Data;

namespace HPCInfo
{
    public class Prev_Template
    {
        #region Member variables and contructor
        protected Int32 _TempID;
        protected Int32 _Ma_Tinbai;
        protected String _TempName = string.Empty;
        protected String _TempWidth;
        protected String _TempHeight;
        protected String _TempColumn;
        protected String _Temp_FontFamily = string.Empty;
        protected String _Temp_FontFamily_Title = string.Empty;
        protected String _Temp_FontFamily_Sapo = string.Empty;
        protected String _Temp_FontSize = string.Empty;
        protected String _Temp_FontSize_Title = string.Empty;
        protected String _Temp_FontSize_Sapo = string.Empty;
        protected Int32 _Temp_Title_Width;
        protected String _Temp_LineHeight_Title = string.Empty;
        protected String _Temp_LineHeight_Sapo = string.Empty;
        protected String _Temp_LineHeight_Content = string.Empty;
        protected String _Temp_Scale_Title = string.Empty;
        protected String _Temp_FontWeight_Title = string.Empty;
        protected Boolean _Temp_IsImage;
        protected Int32 _Temp_Image_Width;
        protected Int32 _Temp_Image_Height;
        protected String _TempLogo;
        protected Int32 _TempType;
        protected String _Temp_Sapo_FontWeight;
        protected Int32 _Temp_Sapo_Width;
        public Prev_Template() { }
        #endregion
        #region Public Properties
        public Int32 TempID
        {
            get { return _TempID; }
            set { _TempID = value; }
        }
        public Int32 Ma_Tinbai
        {
            get { return _Ma_Tinbai; }
            set { _Ma_Tinbai = value; }
        }
        public String TempName
        {
            get { return _TempName; }
            set { _TempName = value; }
        }
        public String TempWidth
        {
            get { return _TempWidth; }
            set { _TempWidth = value; }
        }
        public String TempHeight
        {
            get { return _TempHeight; }
            set { _TempHeight = value; }
        }
        public String TempColumn
        {
            get { return _TempColumn; }
            set { _TempColumn = value; }
        }
        public String Temp_FontFamily
        {
            get { return _Temp_FontFamily; }
            set { _Temp_FontFamily = value; }
        }
        public String Temp_FontFamily_Title
        {
            get { return _Temp_FontFamily_Title; }
            set { _Temp_FontFamily_Title = value; }
        }
        public String Temp_FontFamily_Sapo
        {
            get { return _Temp_FontFamily_Sapo; }
            set { _Temp_FontFamily_Sapo = value; }
        }
        public String Temp_FontSize
        {
            get { return _Temp_FontSize; }
            set { _Temp_FontSize = value; }
        }
        public String Temp_FontSize_Title
        {
            get { return _Temp_FontSize_Title; }
            set { _Temp_FontSize_Title = value; }
        }
        public String Temp_FontSize_Sapo
        {
            get { return _Temp_FontSize_Sapo; }
            set { _Temp_FontSize_Sapo = value; }
        }
        public Int32 Temp_Title_Width
        {
            get { return _Temp_Title_Width; }
            set { _Temp_Title_Width = value; }
        }
        public String Temp_LineHeight_Title
        {
            get { return _Temp_LineHeight_Title; }
            set { _Temp_LineHeight_Title = value; }
        }
        public String Temp_LineHeight_Sapo
        {
            get { return _Temp_LineHeight_Sapo; }
            set { _Temp_LineHeight_Sapo = value; }
        }
        public String Temp_LineHeight_Content
        {
            get { return _Temp_LineHeight_Content; }
            set { _Temp_LineHeight_Content = value; }
        }
        public String Temp_Scale_Title
        {
            get { return _Temp_Scale_Title; }
            set { _Temp_Scale_Title = value; }
        }
        public String Temp_FontWeight_Title
        {
            get { return _Temp_FontWeight_Title; }
            set { _Temp_FontWeight_Title = value; }
        }
        public Int32 Temp_Image_Width
        {
            get { return _Temp_Image_Width; }
            set { _Temp_Image_Width = value; }
        }
        public Int32 Temp_Image_Height
        {
            get { return _Temp_Image_Height; }
            set { _Temp_Image_Height = value; }
        }
        public Boolean Temp_IsImage
        {
            get { return _Temp_IsImage; }
            set { _Temp_IsImage = value; }
        }
        public String TempLogo
        {
            get { return _TempLogo; }
            set { _TempLogo = value; }
        }
        public Int32 TempType
        {
            get { return _TempType; }
            set { _TempType = value; }
        }
        public String Temp_Sapo_FontWeight
        {
            get { return _Temp_Sapo_FontWeight; }
            set { _Temp_Sapo_FontWeight = value; }
        }
        public Int32 Temp_Sapo_Width
        {
            get { return _Temp_Sapo_Width; }
            set { _Temp_Sapo_Width = value; }
        }
        #endregion
    }
}
