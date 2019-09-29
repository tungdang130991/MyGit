<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="PreviewTemplate.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.PreviewTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function checkAllTemplate(objRef) {
            var GridView = document.getElementById('<%=grdListTemplate.ClientID%>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked && inputList[i].disabled == false) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function f_SubmitImage(check) {
            SubmitImage("../UploadFileMulti/Videos_Managerment.aspx?vType=1&vKey=" + check + "", 840, 540);
        }
        function getPath(valuePath, numArg) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThum").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.TinPathBDT%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'inline';
            }
        }
        function ClearImage() {
            document.getElementById("ctl00_MainContent_txtThum").value = "";
            document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
        }
    </script>

    <div style="width: 98%; padding: 10px 1%; float: left;">
        <div style="width: 49%; float: left; text-align: left">
            <div style="width: 100%; float: left; background-color: #cccccc; padding: 7px 0;
                margin-bottom: 14px">
                <img width="16px" style="padding: 0 0 0 5px; float: left;" alt="" src="../Dungchung/Images/Icons/checklist-icon.png" /><span
                    style="font-weight: bold; font-size: 14px; color: #454545; text-transform: uppercase;
                    padding-left: 5px">Danh sách mẫu xem trước</span>
            </div>
            <asp:DataGrid runat="server" ID="grdListTemplate" AutoGenerateColumns="false" DataKeyField="TempID"
                Width="100%" CssClass="Grid" OnEditCommand="grdListTemplate_EditCommand" OnItemDataBound="grdListTemplate_ItemDataBound">
                <ItemStyle CssClass="GridItem"></ItemStyle>
                <AlternatingItemStyle CssClass="GridAltItem" />
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAllTemplate(this);" runat="server"
                                ToolTip="Chọn tất cả" Enabled='<%#_Role.R_Write%>'></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" Text='' Enabled='<%#_Role.R_Write%>' ID='optSelect'
                                AutoPostBack="False"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn Visible="False" DataField="TempID">
                        <HeaderStyle Width="1%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Left" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                        <HeaderTemplate>
                            Tên
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "TempName")%>'
                                ToolTip="Sửa đổi" Enabled='<%#_Role.R_Write%>' CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Width
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TempWidth")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Height
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TempHeight")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Columns
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TempColumn")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Family Content
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontFamily")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Family Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontFamily_Title")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Family Description
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontFamily_Sapo")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Size Content
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontSize")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Size Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontSize_Title")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Size Description
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontSize_Sapo")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    --%>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Width Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_Title_Width")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Line Height Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_LineHeight_Title")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Line Height Sapo
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_LineHeight_Sapo")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Line Height Content
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_LineHeight_Content")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Scale Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_Scale_Title")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Font-Weight Title
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_FontWeight_Title")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    --%>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            IsImage
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_IsImage")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Image Width
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_Image_Width")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                        <HeaderTemplate>
                            Image Height
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Temp_Image_Height")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    --%>
                </Columns>
            </asp:DataGrid>
            <div style="width: 100%; padding-top: 10px;">
                <div style="width: 30%; float: left; text-align: left">
                    <asp:Button runat="server" ID="LinkDelete" CausesValidation="false" CssClass="iconDel"
                        Text="Xóa" OnClick="btnLinkDelete_Click" />
                </div>
                <div style="width: 65%; float: right; text-align: right">
                    <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal">
                    </cc1:CurrentPage>&nbsp;
                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged">
                    </cc1:Pager>
                </div>
            </div>
        </div>
        <div style="width: 45%; float: right; text-align: left; background-color: #ededed;
            padding-bottom: 50px">
            <div style="width: 100%; float: left; background-color: #cccccc; padding: 8px 0;
                margin-bottom: 14px">
                <img width="16px" style="padding: 0 0 0 5px; float: left;" height="16px" alt="" src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" /><span
                    style="font-weight: bold; font-size: 14px; color: #454545; text-transform: uppercase;
                    padding-left: 5px">Cập nhật mẫu xem trước</span>
            </div>
            <div style="padding: 0 10px;">
                <div style="display: none">
                    <asp:TextBox ID="txtIDs" runat="server"></asp:TextBox>
                </div>
                <div style="width: 100%; float: left">
                    <span class="Titlelbl">Template Name</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <asp:TextBox ID="txtName" Width="90%" CssClass="inputtext" runat="server"></asp:TextBox>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 80px">Columns</span>
                    <select runat="server" id="selColumn" class="inputtext" style="width: 110px">
                        <option value="1" selected="selected">1 column </option>
                        <option value="2">2 columns </option>
                        <option value="3">3 columns </option>
                        <option value="4">4 columns </option>
                        <option value="5">5 columns </option>
                    </select>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 50%; float: left">
                        <span class="Titlelbl" style="float: left; line-height: 30px; width: 80px">Width
                        
                        </span>
                        <asp:TextBox ID="txtWidth" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(mm)</span>
                    </div>
                    <div style="width: 50%; float: left">
                        <span class="Titlelbl" style="float: left; line-height: 30px; width: 70px">Height</span>
                        <asp:TextBox ID="txtHeight" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(mm)</span>
                    </div>
                </div>
                <div>
                    <span class="Titlelbl" style="float: left; width: 80px; line-height: 30px;">Logo
                    </span>
                    <div style="float: left">
                        <asp:TextBox ID="txtThum" runat="server" Width="300px" CssClass="inputtext"></asp:TextBox>
                        <input accesskey="S" onclick="f_SubmitImage(1)" class="PhotoSel" type="button" style="margin-left: 5px"
                            value="Browse" name="cmd_SavePath2" />
                        <img runat="server" id="ImgTemp" onclick="openNewImage(this,'Close');" alt="View"
                            title="" style="width: 40px; height: 25px; border: 0px; vertical-align: middle;
                            cursor: pointer; display: none" />
                        <img style="cursor: pointer; vertical-align: middle;" onclick="ClearImage();" height="18"
                            alt="Delete" src="<%= Global.ApplicationPath %>/Dungchung/Images/delete.gif"
                            width="20" border="0" />
                    </div>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0; padding-top: 10px; border-top: 1px solid #999">
                    <span class="Titlelbl" style="float: left; width: 110px; line-height: 30px;">Font-family
                        Title </span>
                    <asp:TextBox ID="txtFontFamilyTitle" Width="250px" CssClass="inputtext" runat="server"></asp:TextBox>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Width Title</span></p>
                        <select runat="server" id="selWidthtitle" class="inputtext" style="width: 110px">
                            <option value="1" selected="selected">1 column </option>
                            <option value="2">2 columns </option>
                            <option value="3">3 columns </option>
                            <option value="4">4 columns </option>
                            <option value="5">5 columns </option>
                        </select>
                    </div>
                    <div style="width: 33%; float: left;">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Font-weight Title</span></p>
                        <select runat="server" id="selFontweightTitle" class="inputtext" style="width: 110px">
                            <option value="Bold" selected="selected">Bold </option>
                            <option value="Normal">Nomal</option>
                        </select>
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Scale Title</span></p>
                        <asp:TextBox ID="txtScaleTitle" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(%)</span>
                    </div>
                </div>
                <div style="width: 100%; float: left;">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Font-size Title</span></p>
                        <asp:TextBox ID="txtFontSizeTitle" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Line-height Title</span></p>
                        <asp:TextBox ID="txtLineheightitle" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 10px; margin-top: 10px; border-top: 1px solid #999">
                    <span class="Titlelbl">Font-family Sapo</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <asp:TextBox ID="txtFontFamilySapo" Width="250px" CssClass="inputtext" runat="server"></asp:TextBox>
                </div>
                  <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Width Sapo</span></p>
                        <select runat="server" id="selSapowidth" class="inputtext" style="width: 110px">
                            <option value="1" selected="selected">1 column </option>
                            <option value="2">2 columns </option>
                            <option value="3">3 columns </option>
                            <option value="4">4 columns </option>
                            <option value="5">5 columns </option>
                        </select>
                    </div>
                    <div style="width: 33%; float: left;">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Font-weight Sapo</span></p>
                        <select runat="server" id="selSapoFontWeight" class="inputtext" style="width: 110px">
                            <option value="Bold" selected="selected">Bold </option>
                            <option value="Normal">Nomal</option>
                        </select>
                    </div>
                    
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Font-size Sapo</span></p>
                        <asp:TextBox ID="txtfontsizeSapo" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Line-height Sapo</span></p>
                        <asp:TextBox ID="txtLineheighSapo" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 10px; margin-top: 10px; border-top: 1px solid #999">
                    <span class="Titlelbl">Font-family Content</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <asp:TextBox ID="txtFontFamily" Width="250px" CssClass="inputtext" runat="server"></asp:TextBox>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Font-size Content</span></p>
                        <asp:TextBox ID="txtFontSizeContent" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Line-height Content</span></p>
                        <asp:TextBox ID="txtLineheighContent" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(pt)</span>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 10px; margin-top: 10px; border-top: 1px solid #999">
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Image</span></p>
                        <asp:CheckBox ID="chkIsImage" runat="server" />
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Image Width</span></p>
                        <asp:TextBox ID="txtImagewidth" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(mm)</span>
                    </div>
                    <div style="width: 33%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Image Height</span></p>
                        <asp:TextBox ID="txtImageHeight" Width="100px" CssClass="inputtext" runat="server"></asp:TextBox><span
                            style="padding-left: 10px">(mm)</span>
                    </div>
                </div>
                <div style="width: 100%; float: left; padding-top: 10px; margin-top: 10px; border-top: solid 1px #cdcdcd">
                    <asp:Button runat="server" ID="btnSave" CssClass="iconSave" Font-Bold="true" OnClick="btnSave_Click"
                        Text="Cập nhật" />
                    <asp:Button runat="server" ID="btnAddNew" CssClass="iconAdd" CausesValidation="false"
                        OnClick="btnAddNew_Click" Text="Thêm mới" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
