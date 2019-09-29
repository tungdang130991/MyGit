using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
    public class T_AdsPos
    {
        #region Member variables and contructor
        protected Int32 _ID;
        protected String _Ads_Name = string.Empty;
        protected int _Ads_DisplayType;
        protected String _Ads_Width;
        protected String _Ads_Height;
        public T_AdsPos()
        {
        }
        #endregion
        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public String Ads_Name
        {
            get { return _Ads_Name; }
            set { _Ads_Name = value; }
        }
        public int Ads_DisplayType
        {
            get { return _Ads_DisplayType; }
            set { _Ads_DisplayType = value; }
        }
        public String Ads_Width
        {
            get { return _Ads_Width; }
            set { _Ads_Width = value; }
        }

        public String Ads_Height
        {
            get { return _Ads_Height; }
            set { _Ads_Height = value; }
        }
        #endregion
    }
}
