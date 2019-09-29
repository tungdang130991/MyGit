using System;
using System.Collections.Generic;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;

namespace HPCBusinessLogic
{
    public class NhuanbutDAL
    {
        public void InsertUpdateT_NhuanBut(T_NhuanBut _Obj)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_Obj, "[Sp_InsertT_NhuanBut]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
