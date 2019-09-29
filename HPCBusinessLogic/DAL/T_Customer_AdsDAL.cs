using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HPCInfo;
using HPCShareDLL;
using System.Collections;
using HPCBusinessLogic.DAL;
using HPCServerDataAccess;

namespace HPCBusinessLogic.DAL
{
    public class T_Customer_AdsDAL
    {
        public DataSet Bind_T_Customer_AdsDynamic(int PageIndex, int PageSize, string WhereCondition)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_ListT_Customer_AdsDynamic]", new string[] { "@PageIndex", "@PageSize", "@where" }, new object[] { PageIndex, PageSize, WhereCondition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertT_Customer_Ads(HPCInfo.T_Customer_Ads obj)
        {
            int _inserted;
            try
            {
                _inserted = HPCDataProvider.Instance().InsertObjectReturn(obj, "[CMS_InsertT_Customer_Ads]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _inserted;
        }
        //public int InsertT_Customer_Ads_copy(HPCInfo.T_Customer_Ads obj)
        //{
        //    int _inserted;
        //    try
        //    {
        //        _inserted = HPCDataProvider.Instance().InsertObjectReturn(obj, "[Sp_InsertT_Customer_Ads_copy]");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return _inserted;
        //}
        public HPCInfo.T_Customer_Ads load_T_Customer_Ads(Int32 id)
        {

            return (T_Customer_Ads)HPCDataProvider.Instance().GetObjectByID("[Sp_SelectOneFromT_Customer_Ads]", id.ToString(), "T_Customer_Ads", "ID");

        }
        public void DeleteFrom_T_Customer_Ads(Int32 ID)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_DeleteOneFromT_Customer_Ads]", new string[] { "@ID" }, new object[] { ID });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Sp_Update_T_Customer_AdsDynamic(string WhereCondition)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Update_T_Customer_AdsDynamic]", new string[] { "@WhereCondition" }, new object[] { WhereCondition });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getListCategoryNameAds(int AdsLogoID)
        {
            //DataSet _ds = null;
            string strListCategorys = "";
            //UltilFunc _untilDAL = new UltilFunc();
            //DataTable _dt = null;
            T_Customer_Ads _objAds = null;
            ChuyenmucDAL _CateDAL = new ChuyenmucDAL();

            string[] sArrProdID = null;
            char[] sep = { ',' };
            try
            {
                strListCategorys = "<br>Trang đăng quảng cáo:";
                _objAds = GetOneFromT_Customer_AdsByID(AdsLogoID);
                sArrProdID = _objAds.Cat_ID.ToString().Trim().Split(sep);
                for (int i = 0; i < sArrProdID.Length; i++)
                {
                    if (sArrProdID[i].ToString() == "ALL")
                        strListCategorys = strListCategorys + "<br>&nbsp;&nbsp;&nbsp;&nbsp;-" + "ALL";
                    if (sArrProdID[i].ToString() == "0")
                        strListCategorys = strListCategorys + "<br>&nbsp;&nbsp;&nbsp;&nbsp;-" + "TRANG CHỦ";
                    if (IsNumeric(sArrProdID[i].ToString()) == true)
                    {
                        if (sArrProdID[i].ToString() != "0")
                        {
                            string _cateName = _CateDAL.GetCateNameByID(Convert.ToInt32(sArrProdID[i].ToString()));
                            if (_cateName != "")
                                strListCategorys = strListCategorys + "<br>&nbsp;&nbsp;&nbsp;&nbsp;-" + _CateDAL.GetCateNameByID(Convert.ToInt32(sArrProdID[i].ToString()));
                        }
                    }

                }

                //_ds = HPCDataProvider.Instance().GetStoreDataSet("[CMS_getAllCategorysNameAdsByAdsID]", new string[] { "@AdsLogoID" }, new object[] { AdsLogoID });
                //if (_ds != null)
                //{
                //    _dt = _ds.Tables[0];
                //    if (_dt.Rows.Count > 0)
                //    {
                //        strListCategorys = "<br>Trang đăng quảng cáo:";
                //        for (int i = 0; i < _dt.Rows.Count; i++)
                //        {
                //            strListCategorys = strListCategorys + "<br>&nbsp;&nbsp;&nbsp;&nbsp;-" + _dt.Rows[i]["Category_Name"].ToString();                           
                //        }
                //    }
                //}



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //_ds = null;
                //_dt = null;
            }
            return strListCategorys;



        }
        public bool IsNumeric(string str)
        {
            bool temp = true;
            try
            {
                str = str.Trim();
                int foo = int.Parse(str);
            }
            catch
            {
                temp = false;
            }
            return temp;
        }
        public bool UpdateOrderOfAdsLogo(int AdsID, int PossValue)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_UpdateOrderOfAdsLogo]", new string[] { "@Ads_ID", "@Order_Number" }, new object[] { AdsID, PossValue });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Send2Approver(int AdsID, int Status, double userSend)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[CMS_Send2ApproveAdsLogo]", new string[] { "@Ads_ID", "@Status", "@UserModify" }, new object[] { AdsID, Status, userSend });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public T_Customer_Ads GetOneFromT_Customer_AdsByID(Int32 ID)
        {
            try
            {
                return (T_Customer_Ads)HPCDataProvider.Instance().GetObjectByID("[CMS_SelectOneFromT_Customer_Ads]", ID.ToString(), "T_Customer_Ads", "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindGridT_Customer_Ads(string _position)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[CMS_SelectAllFromT_Customer_AdsByPosition]", new string[] { "@Possittion" }, new object[] { _position });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
