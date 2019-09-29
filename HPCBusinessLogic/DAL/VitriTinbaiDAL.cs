using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPCInfo;
using HPCShareDLL;
using System.Data;
using System.Collections;
using SSOLib;
using SSOLib.ServiceAgent;

namespace HPCBusinessLogic
{
    public class VitriTinbaiDAL
    {

        public void T_Vitri_Tinbai_Update(int Ma_Vitri, int Ma_Sobao, int Trang, int Ma_Tinbai, int MaCV, int MaQC, int Trai, int Tren, int Rong, int Dai)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_T_Vitri_Tinbai_Update", new string[] { "@Ma_Vitri", "@Ma_Sobao", "@Trang", "@Ma_Tinbai", "@Ma_Congviec", "@Ma_QuangCao", "@Trai", "@Tren", "@Rong", "@Dai" }, new object[] { Ma_Vitri, Ma_Sobao, Trang, Ma_Tinbai, MaCV, MaQC, Trai, Tren, Rong, Dai });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void T_Vitri_Tinbai_UpdateByPara(int type, int Para, int Ma_Sobao, int Trang, int Trai, int Tren, int Rong, int Dai)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_T_Vitri_Tinbai_UpdateByPara", new string[] { "@Type", "@Para", "Ma_Sobao", "@Trang", "@Trai", "@Tren", "@Rong", "@Dai" }, new object[] { type, Para, Ma_Sobao, Trang, Trai, Tren, Rong, Dai });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void T_Vitri_Tinbai_Insert(int Ma_Sobao, int Trang, int Ma_Tinbai, int MaCV, int MaQC, int Trai, int Tren, int Rong, int Dai)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("Sp_T_Vitri_Tinbai_Insert", new string[] { "@Ma_Sobao", "@Trang", "@Ma_Tinbai", "@Ma_Congviec", "@Ma_QuangCao", "@Trai", "@Tren", "@Rong", "@Dai" }, new object[] { Ma_Sobao, Trang, Ma_Tinbai, MaCV, MaQC, Trai, Tren, Rong, Dai });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet T_Sobao_GetByID(int MasoBao)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_Sobao_SelectByID", new string[] { "@Ma_Sobao", }, new object[] { MasoBao.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Layout_SoBao_GetPageBySobao(int MasoBao)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_Layout_SoBao_SelectBySobao", new string[] { "@Ma_Sobao", }, new object[] { MasoBao.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Vitri_Tinbai_SelectBySoBaoAndPageNo(int MasoBao, int Trang)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_Vitri_Tinbai_SelectBySoBaoAndPageNo", new string[] { "@Ma_Sobao", "@Trang_Bao" }, new object[] { MasoBao.ToString(), Trang.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Tinbai_GetByAnpham_And_Sobao(int MaAnpham, int MaSobao, int page, string doituongxl)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_TinBai_SelectByAnpham_And_Sobao", new string[] { "@Ma_Anpham", "@Ma_Sobao", "Page", "@Doituong_DangXuly" }, new object[] { MaAnpham, MaSobao, page, doituongxl });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Tinbai_GetByAnpham_And_Sobao(int MaAnpham, int MaSobao)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_TinBai_SelectByAnpham_And_Sobao", new string[] { "@Ma_Anpham", "@Ma_Sobao" }, new object[] { MaAnpham, MaSobao });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_QuangCao_GetList(int MaAnpham, int MaSobao, string Trang)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[Sp_T_QuangCao_SelectList]", new string[] { "@Ma_Anpham", "@Ma_Sobao", "Trang", }, new object[] { MaAnpham, MaSobao, Trang });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_QuangCao_GetById(int maQC)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("[Sp_T_QuangCao_SelectById]", new string[] { "@Ma_Quangcao", }, new object[] { maQC });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Tinbai_GetById(int _id)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_TinBai_SelectByID", new string[] { "@Ma_Tinbai", }, new object[] { _id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Congviec_GetById(int _id)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_Congviec_SelectByID", new string[] { "@Ma_Congviec", }, new object[] { _id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet T_Vitri_Tinbai_GetByParamater(int _type, int _para)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_T_Vitri_Tinbai_CheckExist", new string[] { "@Type", "@Paramater", }, new object[] { _type, _para });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*Type=1 Check cong viec*/
        /*Type=2 Check Tin bai*/
        /*Type=3 Check quang cao*/
        public bool T_Vitri_Tinbai_CheckExistRow(int _type, int _para)
        {
            try
            {
                DataSet _ds = T_Vitri_Tinbai_GetByParamater(_type, _para);

                if (_ds.Tables[0].Rows.Count < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckExist_Layout(int MasoBao, int Trang)
        {
            try
            {
                DataSet _ds = T_Vitri_Tinbai_SelectBySoBaoAndPageNo(MasoBao, Trang);

                if (_ds.Tables[0].Rows.Count < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void T_Vitri_Tinbai_DelByID(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_T_Vitri_Tinbai_DelByID]", new string[] { "@Ma_Vitri" }, new object[] { _Id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void T_Vitri_Tinbai_UpdateByID(int _Id)
        {
            try
            {
                HPCDataProvider.Instance().ExecStore("[Sp_T_Vitri_Tinbai_UpdateByID]", new string[] { "@Ma_Vitri" }, new object[] { _Id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTypeLayout(int MasoBao, int Trang)
        {
            try
            {
                return HPCDataProvider.Instance().GetStoreDataSet("Sp_GetTypeLayout", new string[] { "@Ma_Sobao", "@Trang_Bao" }, new object[] { MasoBao.ToString(), Trang.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
