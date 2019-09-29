using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HPCBusinessLogic;
using HPCComponents;
using System.IO;
using HPCInfo;
using ToasoanTTXVN.BaoDienTu;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class PhotoAlbum_EditMullti : BasePage
    {
        #region Variable Member
        NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        protected T_RolePermission _Role = null;
        #endregion
        protected int cat_id = 0;
        protected double LangID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _userDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));
                    this.LinkCancel.Attributes.Add("onclick", "return ConfirmQuestion('" + CommonLib.ReadXML("lbBanmuonxoa") + "','ctl00_MainContent_grdListCate');");
                    if (Page.Request.Params["id"] != null)
                    {
                        if (CommonLib.IsNumeric(Page.Request.Params["id"].ToString()))
                            cat_id = int.Parse(Page.Request.Params["id"].ToString());
                    }
                    lblTenPhongsu.Text = PopulateItem(cat_id).Cat_Album_Name;
                    LangID = PopulateItem(cat_id).Lang_ID;
                    if (!IsPostBack)
                    {
                        if (CheckPermission(cat_id))
                        {
                            if (Request["BackID"] != null && Request["BackID"] != "")
                            {
                                int page_index = 0;
                                try { page_index = int.Parse(Session["PageIndex_DetailCAT"].ToString()); }
                                catch { ;}
                                pages.PageIndex = page_index;
                            }
                            LoadData(cat_id);
                        }
                    }
                }
            }
        }

        #region "Event Click and method"

        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Duyet danh sach cac doi tuong tren luoi"
                HPCBusinessLogic.DAL.T_Album_PhotoDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
                HPCInfo.T_Album_Photo _catObj;
                bool checktien = true;
                foreach (DataGridItem m_Item in grdListCate.Items)
                {
                    TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                    if (!string.IsNullOrEmpty(txt_tienNB.Text))
                    {
                        try { int.Parse(txt_tienNB.Text.Replace(",", "")); }
                        catch { checktien = false; }
                    }
                }
                if (checktien)
                {
                    HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                    foreach (DataGridItem m_Item in grdListCate.Items)
                    {
                        T_Butdanh obj_BD = new T_Butdanh();

                        TextBox txtTitle = (TextBox)m_Item.FindControl("txtTitle");
                        TextBox txt_tienNB = (TextBox)m_Item.FindControl("txt_tienNB");
                        TextBox txtDesc = (TextBox)m_Item.FindControl("txtDesc");
                        TextBox txttacgia = (TextBox)m_Item.FindControl("txt_tacgia");
                        TextBox txt_tacgiaID = (TextBox)m_Item.FindControl("txt_tacgiaID");
                        TextBox txt_OrderByPhoto = (TextBox)m_Item.FindControl("txt_OrderByPhoto");
                        Label lblUrlPath = (Label)m_Item.FindControl("lblUrlPath");
                        //CheckBox _chkIsHomeAlbum = (CheckBox)m_Item.FindControl("chkIsHomeAlbum");
                        int tien = 0;
                        if (!string.IsNullOrEmpty(txt_tienNB.Text))
                        {
                            try { tien = int.Parse(txt_tienNB.Text.Replace(",", "")); }
                            catch { ;}
                        }
                        int intPhotoID = Convert.ToInt32(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                        int butdanhID = 0;

                        if (!string.IsNullOrEmpty(txttacgia.Text.Trim()))
                        {
                            obj_BD.BD_ID = 0;
                            obj_BD.BD_Name = txttacgia.Text.Trim();
                            obj_BD.UserID = _user.UserID;
                            butdanhID = obj.Insert_Butdang(obj_BD);
                        }
                        _catObj = setItem(intPhotoID, txtTitle.Text, txtDesc.Text, Convert.ToInt32(txt_OrderByPhoto.Text), tien, txttacgia.Text, butdanhID);
                        _cateDAL.InsertT_Album_Photo(_catObj);
                    }
                    LoadData(cat_id);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true);
                }
                #region Sync
                // DONG BO ANH
                int pageback = 0;
                try { pageback = int.Parse(Session["PageFromID"].ToString()); }
                catch { ;}
                if (pageback == 3)
                {
                    SynFiles _syncfile = new SynFiles();
                    T_Album_PhotoDAL _DAL = new T_Album_PhotoDAL();
                    DataSet _ds = _DAL.Bind_T_Album_Photo(cat_id);
                    foreach (DataRow theRow in _ds.Tables[0].Rows)
                    {
                        string _img = theRow["Abl_Photo_Origin"].ToString();
                        if (_img.Trim().Length > 0)
                        {
                            _syncfile.SynData_UploadImgOne(_img, Global.ImagesService);
                        }
                    }
                }
                //END
                #endregion
                this.litMessages.Text = "Lưu giữ thành công";
                #endregion
            }
            catch (Exception ex)
            {
                HPCServerDataAccess.Lib.ShowAlertMessage(ex.Message.ToString());
            }

        }

        protected void Link_Back_Click(object sender, EventArgs e)
        {
            int pageback = 0;
            try { pageback = int.Parse(Session["PageFromID"].ToString()); }
            catch { ;}
            if (pageback == 1)
                Page.Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
            else if (pageback == 2)
                Page.Response.Redirect("~/PhongSuAnh/Approves_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
            else if (pageback == 3)
                Page.Response.Redirect("~/PhongSuAnh/Approved_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() );//+ "&Back=1"
            else
                Page.Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());

        }
        
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem m_Item in grdListCate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    int ID = int.Parse(grdListCate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString());
                    HPCBusinessLogic.DAL.T_Album_PhotoDAL tt = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
                    tt.DeleteFrom_T_Album_Photo(ID);
                    WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Xóa", Request["Menu_ID"].ToString(), "[Xóa] [Thao tác Xóa ảnh của Phóng sự ảnh]", 0, ConstAction.GocAnh);

                }
            }
            LoadData(cat_id);
        }

        public void grdListCategory_EditCommand(object source, DataGridCommandEventArgs e)
        {
            HPCBusinessLogic.DAL.T_Album_PhotoDAL obj_Cate = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
            if (e.CommandArgument.ToString().ToLower() == "isnoibat")
            {
                HPCBusinessLogic.DAL.T_Album_PhotoDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
                int Cate_id = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
                if (Cate_id != 0)
                {
                    bool check = _cateDAL.GetOneFromT_Album_PhotoByID(Cate_id).Abl_Isweek_Photo;
                    if (check)
                    {
                        _cateDAL.UpdateStatusNoiBat_AlbumPhoto(Cate_id, 0, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Cập nhật", Request["Menu_ID"].ToString(), "[Cập nhật] [Thao tác cập nhật trạng thái ảnh của Phóng sự ảnh]", 0, ConstAction.GocAnh);
                    }
                    else
                    {
                        _cateDAL.UpdateStatusNoiBat_AlbumPhoto(Cate_id, 1, _user.UserID, DateTime.Now);
                        WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, "Cập nhật", Request["Menu_ID"].ToString(), "[Cập nhật] [Thao tác cập nhật trạng thái ảnh của Phóng sự ảnh]", 0, ConstAction.GocAnh);
                    }
                    this.LoadData(cat_id);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("msgCapnhatthatbai") + "');", true);
                    return;
                }
            }
            //else if (e.CommandArgument.ToString().ToLower() == "edit_detail")
            //{
            //    int Cate_id = Convert.ToInt32(this.grdListCate.DataKeys[e.Item.ItemIndex].ToString());
            //    Session["CATEditID"] = cat_id;
            //    Session["PageIndex_DetailCAT"] = pages.PageIndex;
            //    Page.Response.Redirect("~/PhongSuAnh/PhotoAlbum_Image_EditDetail.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&PhotoID=" + Cate_id.ToString());
            //}
        }

        public void grdListCategory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + CommonLib.ReadXML("lbBanmuonxoa") + "\");");
            if (e.Item.ItemIndex >= 0)
            {
                e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
            }
        }

        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData(cat_id);
        }

        public bool CheckPermission(int CatID)
        {
            HPCInfo.T_Album_Categories _cateObj = new HPCInfo.T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _cateObj = _cateDAL.load_T_Album_Categories(CatID);
            Double trangthai = _cateObj.Cat_Album_Status_Approve;
            int pageback = 0;
            try { pageback = Convert.ToInt32(Session["PageFromID"].ToString()); }
            catch { ;}
            if (pageback == 1)// chuc nang bien tap
            {
                if (trangthai != 1 && trangthai != 3)
                {
                    FileUpload1.Visible = false;
                    linkSave.Enabled = false;
                    LinkCancel.Enabled = false;
                    return false;
                }
                else
                {
                    FileUpload1.Visible = true;
                    linkSave.Enabled = true;
                    LinkCancel.Enabled = true;
                    return true;
                }
            }
            else if (pageback == 2)// chuc nang duyet bai
            {
                if (trangthai != 2 && trangthai != 5)
                {
                    FileUpload1.Visible = false;
                    linkSave.Enabled = false;
                    LinkCancel.Enabled = false;
                    return false;
                }
                else
                {
                    FileUpload1.Visible = true;
                    linkSave.Enabled = true;
                    LinkCancel.Enabled = true;
                    return true;
                }
            }
            else if (pageback == 3)// chuc nang xuat ban
            {
                if (trangthai != 4)
                {
                    FileUpload1.Visible = false;
                    linkSave.Enabled = false;
                    LinkCancel.Enabled = false;
                    return false;
                }
                else
                {
                    FileUpload1.Visible = true;
                    linkSave.Enabled = true;
                    LinkCancel.Enabled = true;
                    return true;
                }
            }
            else
            {
                FileUpload1.Visible = false;
                linkSave.Enabled = false;
                LinkCancel.Enabled = false;
                return false;
            }

        }

        #endregion

        #region "Load and Populate data"

        protected string GetUserName()
        {
            string strTemp = "";
            if (_user != null)
                strTemp = _user.UserName + "," + GetvType() + "," + _user.UserID.ToString();
            return strTemp;

        }

        protected string GetvType()
        {
            string strTemp = "4," + cat_id.ToString();
            return strTemp;
        }

        protected string getUrlParameter()
        {
            return "Menu_ID=" + Request["Menu_ID"].ToString() + "&ID=" + Request["ID"].ToString() + "&vType=4";
        }

        private HPCInfo.T_Album_Photo setItem(int PhotoID, string PhotoTitle, string PhotoDesc, int PhotoOrder, int tienNB, string tacgia, int tacgiaID)
        {
            HPCInfo.T_Album_Photo _objPoto = new HPCInfo.T_Album_Photo();
            HPCBusinessLogic.DAL.T_Album_PhotoDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
            _objPoto = _cateDAL.load_T_Album_Photo(PhotoID);
            //_objPoto.Alb_Photo_ID = PhotoID;
            _objPoto.Abl_Photo_Name = PhotoTitle;
            _objPoto.Abl_Photo_Desc = PhotoDesc;
            _objPoto.OrderByPhoto = PhotoOrder;
            _objPoto.TongtienTT = tienNB;
            _objPoto.AuthorID = tacgiaID;
            //_objPoto.Cat_Album_ID = cat_id;
            //_objPoto.Date_Create = DateTime.Now;
            //_objPoto.Creator = _user.UserID;
            _objPoto.Authod_Name = tacgia;
            //_objPoto.Abl_Isweek_Photo = _imageAlbum;
            //_objPoto.Lang_ID = LangID;
            //_objPoto.Copy_From = 0;
            //_objPoto.Abl_Photo_Origin = PhotoPath;
            //_objPoto.Abl_Photo_Thumnail = PhotoPath;
            //_objPoto.Abl_Photo_Status = PhotoStatus;
            return _objPoto;
        }

        private HPCInfo.T_Album_Categories PopulateItem(int _id)
        {
            HPCInfo.T_Album_Categories _cateObj = new HPCInfo.T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _cateObj = _cateDAL.load_T_Album_Categories(_id);
            return _cateObj;
        }

        private void LoadData(int _id)
        {
            string where = " 1=1 AND Lang_ID IN (SELECT DISTINCT(T_Nguoidung_NgonNgu.Ma_Ngonngu) FROM T_Nguoidung_NgonNgu WHERE T_Nguoidung_NgonNgu.[Ma_Nguoidung] = " + _user.UserID + ")";
            where += " AND Cat_Album_ID=" + _id.ToString() + " Order by OrderByPhoto ASC";
            pages.PageSize = 50;//
            HPCBusinessLogic.DAL.T_Album_PhotoDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
            DataSet _ds;
            _ds = _cateDAL.Bind_T_Album_PhotoDynamic(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _cateDAL.Bind_T_Album_PhotoDynamic(pages.PageIndex - 1, pages.PageSize, where);
            grdListCate.DataSource = _ds;
            grdListCate.DataBind(); _ds.Clear();
            if (TotalRecords == 0)
            {
                pages.Visible = false;
                curentPages.Visible = false;
            }
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            int pageback = 0;
            grdListCate.Columns[3].Visible = false;
            try { pageback = int.Parse(Session["PageFromID"].ToString()); }
            catch { ;}
            if (pageback == 2 || pageback == 3) { grdListCate.Columns[6].Visible = true; }
            else { grdListCate.Columns[6].Visible = false; }
            string _str_ID = "'";
            string _strtextID = "'";
            int i = 0;
            foreach (DataGridItem item in grdListCate.Items)
            {
                TextBox txt_tienNB = (TextBox)item.FindControl("txt_tienNB");
                TextBox txt_tacgia = (TextBox)item.FindControl("txt_tacgia");
                TextBox txt_tacgiaID = (TextBox)item.FindControl("txt_tacgiaID");
                if (i == 0)
                {
                    _str_ID = _str_ID + txt_tacgiaID.ClientID;
                    _strtextID = _strtextID + txt_tacgia.ClientID;
                }
                else
                {
                    _str_ID = _str_ID + "," + txt_tacgiaID.ClientID;
                    _strtextID = _strtextID + "," + txt_tacgia.ClientID;
                }
                i++;
                if (!string.IsNullOrEmpty(txt_tienNB.Text))
                    txt_tienNB.Text = string.Format("{0:#,#}", double.Parse(txt_tienNB.Text.Replace(",", ""))).Replace(".", ",");
            }
            _str_ID = _str_ID + "'";
            _strtextID = _strtextID + "'";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", " AutoCompleteSearch_Author(" + _strtextID + "," + _str_ID + ");", true);
        }
        public string GetTen_BD(object ID)
        {
            string BD = "";
            if (ID != null && !string.IsNullOrEmpty(ID.ToString()))
            {
                double BDID = double.Parse(ID.ToString());
                HPCBusinessLogic.DAL.T_ButdanhDAL BDDAL = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                BD = BDDAL.GetBD_Name(BDID);
            }
            return BD;
        }
        #endregion

        #region "Functions for Synchronize data"
        protected string IsStatusGet(string str)
        {
            string strReturn = "";
            if (str.ToLower() == "true")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/Display.gif";
            if (str.ToLower() == "false")
                strReturn = Global.ApplicationPath + "/Dungchung/Images/Icons/uncheck.gif";//D:\BaoAnhMienNui\ToasoanTTXVN\ToasoanTTXVN\Dungchung\Images\Icons\uncheck.gif
            return strReturn;
        }
        private ServicesInfor.Album_Photo SetItemSyn(double News_ID)
        {
            HPCBusinessLogic.DAL.T_Album_PhotoDAL _untilDAL = new HPCBusinessLogic.DAL.T_Album_PhotoDAL();
            ServicesInfor.Album_Photo obj = new ServicesInfor.Album_Photo();
            HPCInfo.T_Album_Photo objGet = new HPCInfo.T_Album_Photo();
            objGet = _untilDAL.load_T_Album_Photo(int.Parse(News_ID.ToString()));
            obj.Alb_Photo_ID = objGet.Alb_Photo_ID;
            obj.Cat_Album_ID = objGet.Cat_Album_ID;
            obj.Lang_ID = objGet.Lang_ID;
            obj.Abl_Photo_Name = objGet.Abl_Photo_Name;
            obj.Abl_Photo_Desc = objGet.Abl_Photo_Desc;
            obj.Abl_Isweek_Photo = objGet.Abl_Isweek_Photo;
            obj.Abl_Photo_Medium = objGet.Abl_Photo_Medium;
            obj.Abl_Photo_Origin = objGet.Abl_Photo_Origin;
            obj.Abl_Photo_Thumnail = objGet.Abl_Photo_Thumnail;
            obj.Authod_Name = objGet.Authod_Name;
            obj.Copy_From = objGet.Copy_From;
            obj.Date_Create = objGet.Date_Create;
            obj.OrderByPhoto = objGet.OrderByPhoto;
            obj.Creator = objGet.Creator;
            obj.File_Size = objGet.File_Size;
            obj.File_Type = objGet.File_Type;
            obj.FileSquare = objGet.FileSquare;
            obj.Abl_Photo_Status = objGet.Abl_Photo_Status;

            return obj;
        }
        private void SynData_InsertT_AlbumPhoto(double Alb_Photo_ID, string urlService)
        {
            ServicesInfor.Album_Photo obj = SetItemSyn(Alb_Photo_ID);
            ServicesPutDataBusines.Album_PhotoDAL _PutDataDAL = new ServicesPutDataBusines.Album_PhotoDAL(urlService);
            try
            {
                _PutDataDAL.Insert_Album_Photo(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SynData_InsertT_AlbumPhotoOne(double News_ID, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    SynData_InsertT_AlbumPhoto(News_ID, _arr[i].ToString());
                }
            }
        }
        #endregion
    }
}
