using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HPCComponents;
using HPCInfo;
using HPCBusinessLogic;
using HPCServerDataAccess;
using System.IO;
using ToasoanTTXVN.BaoDienTu;
using HPCBusinessLogic.DAL;

namespace ToasoanTTXVN.PhongSuAnh
{
    public partial class Album_Edit : BasePage
    {
        #region Variable Member
        HPCBusinessLogic.NguoidungDAL _userDAL = new NguoidungDAL();
        protected SSOLib.ServiceAgent.T_Users _user = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (UltilFunc.IsNumeric(Request["Menu_ID"]))
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _userDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    if (!IsPostBack)
                    {
                        LoadComboBox();
                        DataBind();
                        int pageback = 0;
                        try { pageback = int.Parse(Session["PageFromID"].ToString()); }
                        catch { ;}
                        if (pageback == 2 || pageback == 3)
                        {
                            lbl_tien.Visible = true;
                            txt_tiennhanbut.Visible = true;
                        }
                        else
                        {
                            lbl_tien.Visible = false;
                            txt_tiennhanbut.Visible = false;
                        }
                    }
                }
            }

        }
        #region Event methods

        protected void linkSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int pageback = 0;
                try { pageback = int.Parse(Session["PageFromID"].ToString()); }
                catch { ;}
                if (pageback == 2 || pageback == 3)
                {
                    if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
                    {
                        try { int.Parse(txt_tiennhanbut.Text.Replace(",", "")); }
                        catch { System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "alert('" + CommonLib.ReadXML("lblXacnhanTien") + "');", true); return; }
                    }
                }
                HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                string strActionNotes = "";
                HPCInfo.T_Album_Categories _catObj = GetObject();

                int _return = _cateDAL.InsertT_Album_Categories(_catObj);

                if (_catObj.Cat_Album_ID == 0)
                    strActionNotes = "[Thêm mới] [Thêm mới Phóng sự ảnh: " + _cateDAL.load_T_Album_Categories(_return).Cat_Album_Name + "]";
                else
                    strActionNotes = "[Cập nhật] [Cập nhật Phóng sự ảnh: " + _cateDAL.load_T_Album_Categories(_return).Cat_Album_Name + "]";
                WriteLogHistory2Database.WriteHistory2Database(_user.UserID, _user.UserFullName, _catObj.Cat_Album_Name, Request["Menu_ID"].ToString(), strActionNotes, 0, ConstAction.GocAnh);
                if (pageback == 3) {
                    #region Sync
                    // DONG BO ANH
                    try
                    {
                        SynFiles _syncfile = new SynFiles();
                        T_Album_PhotoDAL _DAL = new T_Album_PhotoDAL();
                        DataSet _ds = _DAL.Bind_T_Album_Photo(_return);
                        foreach (DataRow theRow in _ds.Tables[0].Rows)
                        {
                            string _img = theRow["Abl_Photo_Origin"].ToString();
                            if (_img.Trim().Length > 0)
                            {
                                _syncfile.SynData_UploadImgOne(_img, Global.ImagesService);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    
                    //END
                    #endregion
                }
                if (_catObj.Cat_Album_ID == 0)
                {
                    ResetForm();
                    Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                }
                else
                {
                    if (pageback == 1)
                        Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                    else if (pageback == 2)
                        Response.Redirect("~/PhongSuAnh/Approves_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                    else if (pageback == 3)
                        Response.Redirect("~/PhongSuAnh/Approved_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                    else
                        Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());

                }
            }
        }
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            if (Page.Request["Menu_ID"] != null)
            {
                int pageback = 0;
                try { pageback = int.Parse(Session["PageFromID"].ToString()); }
                catch { ;}
                if (pageback == 1)
                    Page.Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                else if (pageback == 2)
                    Page.Response.Redirect("~/PhongSuAnh/Approves_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                else if (pageback == 3)
                    Page.Response.Redirect("~/PhongSuAnh/Approved_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString() + "&Back=1");
                else
                    Page.Response.Redirect("~/PhongSuAnh/Album_List.aspx?Menu_ID=" + this.Page.Request["Menu_ID"].ToString());

            }
            else return;
        }

        #endregion

        #region User-methods
        private HPCInfo.T_Album_Categories GetObject()
        {
            HPCInfo.T_Album_Categories _objCate = new HPCInfo.T_Album_Categories();
            if (Page.Request.Params["id"] != null)
                _objCate.Cat_Album_ID = int.Parse(Page.Request["id"].ToString());
            else _objCate.Cat_Album_ID = 0;
            _objCate.Cat_Album_Name = Txt_tieudeAbum.Text;
            _objCate.Lang_ID = Convert.ToInt32(cbo_lanquage.SelectedValue);
            _objCate.Status = true;// this.chkDisplay.Checked;
            if (txtOrder.Text.Length > 0)
                if (UltilFunc.IsNumeric(this.txtOrder.Text.Trim()))
                    _objCate.Possition = Convert.ToInt32(this.txtOrder.Text.Trim());
            _objCate.Links = "";
            _objCate.Cat_AlbumDesc = this.txt_noidungAlbum.Text.Trim();
            _objCate.Target = true;
            _objCate.Copy_From = 0;
            _objCate.Cat_Album_DateCreate = DateTime.Now;
            _objCate.DateModify = DateTime.Now;
            _objCate.UserCreated = _user.UserID;
            _objCate.UserModify = _user.UserID;
            _objCate.Cat_Album_DateSend = DateTime.Now;
            _objCate.Cat_Album_DateApprove = DateTime.Now;
            _objCate.Cat_Album_UserIDApprove = _user.UserID;
            int statusid = 1;
            try { statusid = int.Parse(lbl_status.Text); }
            catch { ;}
            _objCate.Cat_Album_Status_Approve = statusid;
            if (this.cbo_chuyenmuc.SelectedIndex > 0)
                _objCate.Cat_Album_CATID = int.Parse(cbo_chuyenmuc.SelectedValue);
            else _objCate.Cat_Album_CATID = 0;
            _objCate.Tacgia = txt_Author_name.Text;
            //_objCate.Chatluong = ddlnews_chatluong.SelectedIndex;

            HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
            T_Butdanh obj_BD = new T_Butdanh();
            int butdanhID = 0;
            if (!string.IsNullOrEmpty(txt_Author_name.Text.Trim()))
            {
                obj_BD.BD_ID = 0;
                obj_BD.BD_Name = txt_Author_name.Text.Trim();
                obj_BD.UserID = _user.UserID;
                butdanhID = obj.Insert_Butdang(obj_BD);
            }
            _objCate.AuthorID = butdanhID;
            if (!string.IsNullOrEmpty(txt_tiennhanbut.Text))
            {
                _objCate.TongtienTT = int.Parse(txt_tiennhanbut.Text.Replace(",", ""));
            }
            else
            {
                _objCate.TongtienTT = 0;
            }
            //_objCate.Theloai = int.Parse(ddlNews_IsType.SelectedValue);
            //if (ddlnews_chatluong.SelectedIndex > 0)
            //    _objCate.Loaihinh = int.Parse(Drop_loaihinh.SelectedValue);
            //else
            //    _objCate.Loaihinh = 0;

            //_objCate.NguoichamNBID = _user.UserID;
            //_objCate.NgaychamNB = DateTime.Now;
            //_objCate.HesoTT = 0.0;
            _objCate.Comment = txtGhichu.Text;
            return _objCate;
        }
        private void PopulateItem(int _id)
        {
            HPCInfo.T_Album_Categories _cateObj = new HPCInfo.T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _cateObj = _cateDAL.load_T_Album_Categories(_id);
            if (_cateObj != null)
            {
                Txt_tieudeAbum.Text = _cateObj.Cat_Album_Name.ToString();
                txt_noidungAlbum.Text = _cateObj.Cat_AlbumDesc.ToString();
                txtOrder.Text = _cateObj.Possition.ToString();
                lbl_status.Text = _cateObj.Cat_Album_Status_Approve.ToString();
                this.cbo_lanquage.SelectedValue = _cateObj.Lang_ID.ToString();
                this.cbo_chuyenmuc.Items.Clear();
                if (cbo_lanquage.SelectedIndex > 0)
                {
                    UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                    cbo_chuyenmuc.UpdateAfterCallBack = true;
                    cbo_chuyenmuc.SelectedIndex = CommonLib.GetIndexControl(cbo_chuyenmuc, _cateObj.Cat_Album_CATID.ToString());
                }
                else
                {
                    this.cbo_chuyenmuc.DataSource = null;
                    this.cbo_chuyenmuc.DataBind();
                    this.cbo_chuyenmuc.UpdateAfterCallBack = true;
                }
                //ddlnews_chatluong.SelectedIndex = _cateObj.Chatluong;
                txt_Author_name.Text = _cateObj.Tacgia;
                if (_cateObj.TongtienTT > 0)
                {
                    if (_cateObj.TongtienTT > 0)
                        this.txt_tiennhanbut.Text = string.Format("{0:#,#}", _cateObj.TongtienTT).Replace(".", ",");
                }
                this.txtGhichu.Text = _cateObj.Comment;
                HPCBusinessLogic.DAL.T_ButdanhDAL obj = new HPCBusinessLogic.DAL.T_ButdanhDAL();
                this.txt_Author_name.Text = obj.GetBD_Name(_cateObj.AuthorID);
            }
        }
        public override void DataBind()
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "" && Request["ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["ID"]) == true)
                {
                    HPCBusinessLogic.DAL.T_Album_CategoriesDAL dal = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
                    int _id = Convert.ToInt32(Request["ID"].ToString());
                    if (CheckPermission(_id))
                    {
                        PopulateItem(_id);
                        if (dal.load_T_Album_Categories(_id).Lang_ID != 1)
                            cbo_lanquage.Enabled = false;
                        else
                            cbo_lanquage.Enabled = true;
                    }
                }
            }
            else
            {
                if (cbo_lanquage.SelectedIndex > 0)
                {
                    cbo_chuyenmuc.Items.Clear();
                    if (cbo_lanquage.SelectedIndex >= 0)
                    {
                        UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                        cbo_chuyenmuc.UpdateAfterCallBack = true;
                    }
                    else
                    {
                        this.cbo_chuyenmuc.DataSource = null;
                        this.cbo_chuyenmuc.DataBind();
                        this.cbo_chuyenmuc.UpdateAfterCallBack = true;
                    }
                }
            }
        }
        private void LoadComboBox()
        {
            UltilFunc.BindCombox(cbo_lanquage, "Ma_AnPham", "Ten_AnPham", "T_AnPham", " 1=1 ", CommonLib.ReadXML("lblTatca"));
            if (cbo_lanquage.Items.Count >= 3)
                cbo_lanquage.SelectedIndex = HPCComponents.Global.DefaultLangID;
            else
                cbo_lanquage.SelectedIndex = UltilFunc.GetIndexControl(this.cbo_lanquage, HPCComponents.Global.DefaultCombobox);
            if (cbo_lanquage.SelectedIndex != 0)
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" 1=1 and HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
        }
        protected void cbo_lanquage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_chuyenmuc.Items.Clear();
            if (cbo_lanquage.SelectedIndex >= 0)
            {
                UltilFunc.BindCombox(cbo_chuyenmuc, "Ma_ChuyenMuc", "Ten_ChuyenMuc", "T_ChuyenMuc", string.Format(" HoatDong = 1 and HienThi_BDT = 1 and Ma_AnPham= " + this.cbo_lanquage.SelectedValue + " AND Ma_ChuyenMuc IN ({0})", UltilFunc.GetCategory4User(_user.UserID)), CommonLib.ReadXML("lblTatca"), "Ma_Chuyenmuc_Cha", " Order by ThuTuHienThi ASC");
                cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
            else
            {
                this.cbo_chuyenmuc.DataSource = null;
                this.cbo_chuyenmuc.DataBind();
                this.cbo_chuyenmuc.UpdateAfterCallBack = true;
            }
        }
        private void ResetForm()
        {
            txtOrder.Text = "";
            Txt_tieudeAbum.Text = "";
            cbo_lanquage.SelectedIndex = 0;
            txt_noidungAlbum.Text = "";
        }
        public bool CheckPermission(int CatID)
        {
            HPCInfo.T_Album_Categories _cateObj = new HPCInfo.T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _cateDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _cateObj = _cateDAL.load_T_Album_Categories(CatID);
            Double trangthai = _cateObj.Cat_Album_Status_Approve;
            int pageback = 0;
            try
            {
                if (Session["PageFromID"] != null)
                    pageback = Convert.ToInt32(Session["PageFromID"].ToString());
            }
            catch { ;}
            if (pageback == 1)// chuc nang bien tap
            {
                if (trangthai != 1 && trangthai != 3)
                {
                    linkSave.Visible = false;
                    return false;
                }
                else
                {
                    linkSave.Visible = true;
                    return true;
                }
            }
            else if (pageback == 2)// chuc nang duyet bai
            {
                if (trangthai != 2 && trangthai != 5)
                {
                    linkSave.Visible = false;
                    return false;
                }
                else
                {
                    linkSave.Visible = true;
                    return true;
                }
            }
            else if (pageback == 3)// chuc nang xuat ban
            {
                if (trangthai != 4)
                {
                    linkSave.Visible = false;
                    return false;
                }
                else
                {
                    linkSave.Visible = true;
                    return true;
                }
            }
            else
            {
                linkSave.Visible = true;
                return true;
            }

        }

        #endregion

        #region SYNC----
        private void SynData_InsertAlbum_CategoriesAny(int ID, ArrayList _arr)
        {
            if (_arr.Count > 0)
            {
                for (int i = 0; i < _arr.Count; i++)
                {
                    SynData_InsertAlbum_Categories(ID, _arr[i].ToString());
                }
            }
        }
        private void SynData_InsertAlbum_Categories(int ID, string urlService)
        {
            ServicesInfor.Album_Categories obj = SetItemSyn(ID);
            ServicesPutDataBusines.Album_CategoriesDAL _PutDataDAL = new ServicesPutDataBusines.Album_CategoriesDAL(urlService);
            try
            {
                _PutDataDAL.Insert_Album_Categories(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ServicesInfor.Album_Categories SetItemSyn(int _ID)
        {
            ServicesInfor.Album_Categories obj = new ServicesInfor.Album_Categories();
            HPCInfo.T_Album_Categories _objGet = new T_Album_Categories();
            HPCBusinessLogic.DAL.T_Album_CategoriesDAL _untilDAL = new HPCBusinessLogic.DAL.T_Album_CategoriesDAL();
            _objGet = _untilDAL.load_T_Album_Categories(_ID);
            if (Page.Request.Params["ID"] != null)
                obj.Cat_Album_ID = _objGet.Cat_Album_ID;
            else
                obj.Cat_Album_ID = _objGet.Cat_Album_ID;
            obj.Cat_Album_Name = _objGet.Cat_Album_Name;
            obj.Cat_AlbumDesc = _objGet.Cat_AlbumDesc;
            obj.Copy_From = _objGet.Copy_From;
            if (txtOrder.Text.Length > 0)
                if (UltilFunc.IsNumeric(this.txtOrder.Text.Trim()))
                    obj.Possition = _objGet.Possition;
            obj.Links = _objGet.Links;
            obj.Lang_ID = _objGet.Lang_ID;
            obj.Status = _objGet.Status;
            obj.Target = _objGet.Target;
            return obj;
        }
        #endregion
    }
}
