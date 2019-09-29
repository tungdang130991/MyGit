using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPCBusinessLogic;
using HPCBusinessLogic.DAL;
using HPCInfo;
using HPCComponents;
using SSOLib;
using SSOLib.ServiceAgent;
using System.Collections;
using System.Data;
namespace ToasoanTTXVN.Danhmuc
{
    public partial class PreviewTemplate : System.Web.UI.Page
    {
        HPCBusinessLogic.NguoidungDAL _NguoidungDAL = new NguoidungDAL();
        NgonNgu_DAL _dalLang = new NgonNgu_DAL();
        protected T_Users _user;
        protected T_RolePermission _Role = null;
        UltilFunc ulti = new UltilFunc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Menu_ID"] != null && Request["Menu_ID"].ToString() != "" && Request["Menu_ID"].ToString() != String.Empty)
            {
                if (CommonLib.IsNumeric(Request["Menu_ID"]) == true)
                {
                    if (!HPCSecurity.IsAccept(Convert.ToInt32(Request["Menu_ID"])))
                        Response.Redirect("~/Errors/AccessDenied.aspx");
                    _user = _NguoidungDAL.GetUserByUserName(HPCSecurity.CurrentUser.Identity.Name);
                    _Role = _NguoidungDAL.GetRole4UserMenu(_user.UserID, Convert.ToInt32(Request["Menu_ID"]));

                    this.LinkDelete.Visible = _Role.R_Delete;
                    this.LinkDelete.Attributes.Add("onclick", "return ConfirmQuestion('Bạn có chắc muốn xóa?','ctl00_MainContent_grdListTemplate_ctl01_chkAll');");
                    if (!IsPostBack)
                    {
                        if (Session["CurrentPage"] != null)
                        {
                            pages.PageIndex = int.Parse(Session["CurrentPage"].ToString());
                            LoadData();
                            Session["CurrentPage"] = null;
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            string where = " 1=1 ";
            where += " AND TempType = 0 ";
            where += " Order by TempID DESC";
            pages.PageSize = Global.MembersPerPage;
            PreviewTemplateDAL _PrevDAL = new PreviewTemplateDAL();
            DataSet _ds;
            _ds = _PrevDAL.BindGridT_PrevTemplate(pages.PageIndex, pages.PageSize, where);
            int TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0].ItemArray[0].ToString());
            int TotalRecord = Convert.ToInt32(_ds.Tables[0].Rows.Count);
            if (TotalRecord == 0)
                _ds = _PrevDAL.BindGridT_PrevTemplate(pages.PageIndex - 1, pages.PageSize, where);
            grdListTemplate.DataSource = _ds;
            grdListTemplate.DataBind();
            pages.TotalRecords = curentPages.TotalRecords = TotalRecords;
            curentPages.TotalPages = pages.CalculateTotalPages();
            curentPages.PageIndex = pages.PageIndex;
            Session["PageIndex"] = pages.PageIndex;
        }
        protected void btnLinkDelete_Click(object sender, EventArgs e)
        {
            ArrayList ar = new ArrayList();
            foreach (DataGridItem m_Item in grdListTemplate.Items)
            {
                CheckBox chk_Select = (CheckBox)m_Item.FindControl("optSelect");
                if (chk_Select != null && chk_Select.Checked)
                {
                    ar.Add(int.Parse(grdListTemplate.DataKeys[int.Parse(m_Item.ItemIndex.ToString())].ToString()));
                }
            }
            LoadData();
            for (int i = 0; i < ar.Count; i++)
            {
                int _IDs = int.Parse(ar[i].ToString());
                PreviewTemplateDAL PrevDAL = new PreviewTemplateDAL();
                PrevDAL.DeleteOneFromT_PrevTemplate(_IDs);
                string ActionsCode = "[Thao tác xóa Tempalte][ID:" + int.Parse(_IDs.ToString()) + "";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
            }
            LoadData();
        }
        private void GeDataByID(int IDQ)
        {
            Prev_Template _objSet = new Prev_Template();
            PreviewTemplateDAL _DAL = new PreviewTemplateDAL();
            _objSet = _DAL.GetOneFromT_PrevTemplateByID(IDQ);
            this.txtIDs.Text = _objSet.TempID.ToString();
            this.txtName.Text = _objSet.TempName;
            this.txtWidth.Text = _objSet.TempWidth;
            this.txtHeight.Text = _objSet.TempHeight;
            this.selColumn.Value = _objSet.TempColumn.ToString();
            this.txtFontFamily.Text = _objSet.Temp_FontFamily;
            this.txtFontFamilyTitle.Text = _objSet.Temp_FontFamily_Title;
            this.txtFontFamilySapo.Text = _objSet.Temp_FontFamily_Sapo;
            this.txtFontSizeContent.Text = _objSet.Temp_FontSize;
            this.txtFontSizeTitle.Text = _objSet.Temp_FontSize_Title;
            this.txtfontsizeSapo.Text = _objSet.Temp_FontSize_Sapo;
            this.selWidthtitle.Value = _objSet.Temp_Title_Width.ToString();
            this.selSapowidth.Value = _objSet.Temp_Sapo_Width.ToString();
            this.txtLineheightitle.Text = _objSet.Temp_LineHeight_Title;
            this.txtLineheighSapo.Text = _objSet.Temp_LineHeight_Sapo;
            this.txtLineheighContent.Text = _objSet.Temp_LineHeight_Content;
            this.txtScaleTitle.Text = _objSet.Temp_Scale_Title;
            this.selFontweightTitle.Value = _objSet.Temp_FontWeight_Title;
            this.selSapoFontWeight.Value = _objSet.Temp_Sapo_FontWeight;
            this.chkIsImage.Checked = _objSet.Temp_IsImage;
            this.txtImagewidth.Text = _objSet.Temp_Image_Width.ToString();
            this.txtImageHeight.Text = _objSet.Temp_Image_Height.ToString();
            this.txtThum.Text = _objSet.TempLogo;
            if (_objSet.TempLogo.ToString().Length > 0)
            {
                this.ImgTemp.Src = HPCComponents.Global.UploadPathBDT + _objSet.TempLogo;
                this.ImgTemp.Attributes.CssStyle.Add("display", "inline");
            }
            else this.ImgTemp.Attributes.CssStyle.Add("display", "none");

        }
        protected void pages_IndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public void grdListTemplate_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"Bạn có chắc chắn muốn xóa không?\");");
            }
            e.Item.Attributes.Add("onmouseover", "currColor=this.style.backgroundColor;this.style.backgroundColor='" + CommonLib.HPCOnmouseoverGrid() + "'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=currColor");
        }
        public void grdListTemplate_EditCommand(object source, DataGridCommandEventArgs e)
        {
            PreviewTemplateDAL PrevDAL = new PreviewTemplateDAL();
            if (e.CommandArgument.ToString().ToLower() == "edit")
            {
                int TID = Convert.ToInt32(this.grdListTemplate.DataKeys[e.Item.ItemIndex].ToString());
                GeDataByID(TID);
            }
            if (e.CommandArgument.ToString().ToLower() == "delete")
            {
                PrevDAL.DeleteOneFromT_PrevTemplate(Convert.ToInt32(this.grdListTemplate.DataKeys[e.Item.ItemIndex].ToString()));

                string ActionsCode = "[Thao tác xóa Tempalte][ID:" + this.grdListTemplate.DataKeys[e.Item.ItemIndex].ToString() + " ]";
                UltilFunc.Log_Action(_user.UserID, _user.UserFullName, DateTime.Now, int.Parse(Request["Menu_ID"]), ActionsCode);
                this.LoadData();
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Reset();
        }
        public Prev_Template SetItem()
        {
           
            Prev_Template obj_Prev = new Prev_Template();
            if (txtIDs.Text != "")
                obj_Prev.TempID = Convert.ToInt32(txtIDs.Text.ToString());
            else
                obj_Prev.TempID = 0;
            if (txtName.Text != "")
                obj_Prev.TempName = txtName.Text;
            obj_Prev.TempWidth = txtWidth.Text;
            obj_Prev.TempHeight = txtHeight.Text;
            obj_Prev.TempColumn = selColumn.Value;
            obj_Prev.Temp_FontFamily = txtFontFamily.Text;
            obj_Prev.Temp_FontFamily_Title = txtFontFamilyTitle.Text;
            obj_Prev.Temp_FontFamily_Sapo = txtFontFamilySapo.Text;
            obj_Prev.Temp_FontSize = txtFontSizeContent.Text;
            obj_Prev.Temp_FontSize_Title = txtFontSizeTitle.Text;
            obj_Prev.Temp_FontSize_Sapo = txtfontsizeSapo.Text;
            obj_Prev.Temp_Title_Width = Convert.ToInt32(selWidthtitle.Value);
            obj_Prev.Temp_Sapo_Width = Convert.ToInt32(selSapowidth.Value);
            obj_Prev.Temp_LineHeight_Title = txtLineheightitle.Text;
            if (obj_Prev.Temp_LineHeight_Title == "")
                obj_Prev.Temp_LineHeight_Title = "Normal";
            obj_Prev.Temp_LineHeight_Sapo = txtLineheighSapo.Text;
            if (obj_Prev.Temp_LineHeight_Sapo == "")
                obj_Prev.Temp_LineHeight_Sapo = "Normal";
            obj_Prev.Temp_LineHeight_Content = txtLineheighContent.Text;
            if (obj_Prev.Temp_LineHeight_Content == "")
                obj_Prev.Temp_LineHeight_Content = "Normal";
            obj_Prev.Temp_Scale_Title = txtScaleTitle.Text;
            if (obj_Prev.Temp_Scale_Title == "")
                obj_Prev.Temp_Scale_Title = "100";
            obj_Prev.Temp_FontWeight_Title = selFontweightTitle.Value;
            obj_Prev.Temp_Sapo_FontWeight = selSapoFontWeight.Value;
            obj_Prev.Temp_IsImage = chkIsImage.Checked;
            if (chkIsImage.Checked == true)
            {
                obj_Prev.Temp_Image_Width = Convert.ToInt32(txtImagewidth.Text);
                obj_Prev.Temp_Image_Height = Convert.ToInt32(txtImageHeight.Text);
            }
            else
            {
                obj_Prev.Temp_Image_Width = 0;
                obj_Prev.Temp_Image_Height =0;
            }
            if (this.txtThum.Text.Trim().Length > 0)
                obj_Prev.TempLogo = this.txtThum.Text.Trim();
            obj_Prev.TempType = 0;
            return obj_Prev;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                FuncAlert.AlertJS(this, "Bạn chưa nhập tên mẫu");
                return;
            }
            if (Page.IsValid)
            {
                PreviewTemplateDAL _PrevDAL = new PreviewTemplateDAL();
                Prev_Template _PrevObj = SetItem();
                int _return = _PrevDAL.InsertT_PrevTemplate(_PrevObj);
                FuncAlert.AlertJS(this, "Cập nhật thành công");
                LoadData();
                Reset();
            }
        }
        private void Reset()
        {
            this.txtIDs.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtWidth.Text = string.Empty;
            this.txtHeight.Text = string.Empty;
            this.selColumn.SelectedIndex = 0;
            this.txtFontFamily.Text = string.Empty;
            this.txtFontFamilySapo.Text = string.Empty;
            this.txtFontFamilyTitle.Text = string.Empty;
            this.txtFontSizeContent.Text = string.Empty;
            this.txtFontSizeTitle.Text = string.Empty;
            this.txtfontsizeSapo.Text = string.Empty;
            this.txtLineheightitle.Text = string.Empty;
            this.txtLineheighSapo.Text = string.Empty;
            this.txtLineheighContent.Text = string.Empty;
            this.selWidthtitle.SelectedIndex = 0;
            this.txtImageHeight.Text = string.Empty;
            this.txtImagewidth.Text = string.Empty;
            this.txtScaleTitle.Text = string.Empty;
            this.chkIsImage.Checked = false;
            this.selFontweightTitle.SelectedIndex = 0;
            this.txtThum.Text = string.Empty;

        }
    }
}
