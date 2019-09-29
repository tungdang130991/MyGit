using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HPCBusinessLogic;
using HPCInfo;
using HPCComponents;
using System.IO;
using System.Data;
using System.Configuration;
using HPCServerDataAccess;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
namespace ToasoanTTXVN.UploadFileMulti
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AutoSelectMulti : IHttpHandler
    {
        UltilFunc Ulti = new UltilFunc();
        public void ProcessRequest(HttpContext context)
        {
            HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
            DataTable dt = new DataTable();
            string sqlselect = string.Empty;
            string type = context.Request.QueryString["type"].Split('?').GetValue(0).ToString();
            string searchText = context.Request.QueryString["term"];
            Collection<AutoCompleteDTO> collection;
            collection = new Collection<AutoCompleteDTO>();
            AutoCompleteDTO dto;
            if (type == "1")
            {
                sqlselect = "select top 20 Ma_Nguoidung, LTRIM(RTRIM(TenDaydu))+case when Loai=1 then ' -- (CTV)' when Loai=0 then '' end as Ten_Dangnhap from T_Nguoidung where (Trangthai_Xoa=0 or Trangthai_Xoa is null) and TenDaydu LIKE N'%" + searchText.Trim() + "%' and Ten_Dangnhap is not null";
                dt = Ulti.ExecSqlDataSet(sqlselect).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    dto = new AutoCompleteDTO();
                    dto.value = dto.label = (string)dt.Rows[i]["Ten_Dangnhap"];
                    dto.id = Convert.ToString(dt.Rows[i]["Ma_Nguoidung"]);
                    collection.Add(dto);

                }
            }
            if (type == "2")
            {
                string _where = "IsDeleted = 0 and UserFullName LIKE N'%" + searchText.Trim() + "%'";
                dt = _NguoidungDAL.GetT_User_Dynamic(_where).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dto = new AutoCompleteDTO();
                    dto.value = dto.label = (string)dt.Rows[i]["UserFullName"];
                    dto.id = Convert.ToString(dt.Rows[i]["UserID"]);
                    collection.Add(dto);

                }
            }



            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string jsonString = serializer.Serialize(collection);

            context.Response.Write(jsonString);
        }
        public class AutoCompleteDTO
        {
            public string id { get; set; }
            public string label { get; set; }
            public string value { get; set; }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
