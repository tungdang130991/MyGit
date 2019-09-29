using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCShareDLL;
using HPCInfo;
using System.Data;

namespace HPCBusinessLogic.DAL
{
    public class AdsPosDAL
    {
        public void InsertAdsPos(T_AdsPos _T_AdsPos)
        {
            try
            {
                HPCDataProvider.Instance().InsertObject(_T_AdsPos, "CMS_UpdateAdsPos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T_AdsPos GetOneFromT_AdsPosByID(Int32 ID)
        {
            try
            {
                return (T_AdsPos)HPCDataProvider.Instance().GetObjectByID("[CMS_getAdsPosByID]", ID.ToString(), "T_AdsPos", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromT_AdsPosByID(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("CMS_DeleteAdsPosByID", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet BindGridT_AdsPos()
        {
            try
            {
                return HPCDataProvider.Instance().ExecStoreDataSet("CMS_getAllAdsPos");//, new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Return Type Adv
        /// </summary>
        /// <param name="Path2FileAdvs"></param>
        /// <returns></returns>
        public static int getAdvType(string Path2FileAdvs)
        {
            int intAdvType = 0;
            string strExten = string.Empty;
            if (Path2FileAdvs.Trim().Length > 0)
            {
                strExten = System.IO.Path.GetExtension(Path2FileAdvs).ToLower();
                if (strExten.Contains(".flv") || strExten.Contains(".swf") || strExten.Contains(".wmv") || strExten.Contains(".mp4"))
                    intAdvType = 1;
            }
            return intAdvType;
        }
    }
}
