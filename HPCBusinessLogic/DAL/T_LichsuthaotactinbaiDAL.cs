using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using System.Collections;

namespace HPCBusinessLogic.DAL
{
    public class T_LichsuthaotactinbaiDAL
    {
        public void InserT_Lichsu_Thaotac_TinBai(T_Lichsu_Thaotac_TinBai objActionTinbai)
        {
            HPCDataProvider.Instance().InsertObject(objActionTinbai, "Sp_InsertT_Lichsu_Thaotac_TinBai");
        }
    }
}
