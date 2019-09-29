using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCInfo
{
	public class T_RolePermission
	{
        #region Member variables and contructor
        private bool _R_Read;
        private bool _R_Write;
        private bool _R_Delete;
        
        #endregion

        #region Public Properties
        /// <summary>
        /// Quyền đọc
        /// </summary>
        public bool R_Read
        {
            get { return _R_Read; }
            set { _R_Read = value; }
        }
        /// <summary>
        /// Quyền ghi
        /// </summary>
        public bool R_Write
        {
            get { return _R_Write; }
            set { _R_Write = value; }
        }

        /// <summary>
        /// Quyền xóa
        /// </summary>
        public bool R_Delete
        {
            get { return _R_Delete; }
            set { _R_Delete = value; }
        }
        
        #endregion
	}
}
