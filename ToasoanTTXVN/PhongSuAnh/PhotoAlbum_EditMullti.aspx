<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="PhotoAlbum_EditMullti.aspx.cs" Inherits="ToasoanTTXVN.PhongSuAnh.PhotoAlbum_EditMullti" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link rel="Stylesheet" type="text/css" href="../Dungchung/scripts/UploadMulti/uploadify.css" />

    <script src="../Dungchung/scripts/UploadMulti/jquery-1.7.2.min.js" type="text/javascript" />

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.min.js"></script>

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.js"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>--%>

    <script language="Javascript" type="text/javascript">
        function CheckAll_One(objRef) {
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
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
        
        function OnCheckBox(_values) {
            document.getElementById(_values).checked = true;
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
            var elm = GridView.getElementsByTagName("input");
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != _values) {
                    if (elm[i].checked) {
                        elm[i].checked = false;
                    }
                }
            }
        }
    </script>

    <script type="text/javascript">

        function AutoCompleteSearch_Author(arr_Name, arr_ID) {
            try {
                var i = 0;
                var arr_ID_Name = arr_Name.split(",");
                var arr_ID_ID = arr_ID.split(",");
                for (i = 0; i < arr_ID_Name.length; i++) {
                    var a = arr_ID_Name[i];
                    var b = arr_ID_ID[i]
                    if (a.length > 0 && b.length > 0)
                        Run(a, b);
                }
            } catch (err) {
                //alert(err); 
            }
        }
        function Run(a, b) {
            try {
            $(document).ready(function() {
                $('#' + a).autocomplete("AutoCompleteSearch.ashx").result(
                    function(event, data, formatted) {
                        try {
                            if (data) { $('#' + b).val(data[1]); }
                            else { $('#' + b).val('0'); }
                        } catch (err) {
                            //alert(err);
                        }
                    });
            });
            }
            catch (err) {
                //alert(err);
            }
        }
        
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 2%">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titCapnhatgocanh")%>:
                                <asp:Label runat="server" ID="lblTenPhongsu"></asp:Label></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table width="100%" align="center" cellspacing="2" cellpadding="2">
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <div>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="2">
                            <asp:UpdatePanel ID="UpdatePL" runat="server">
                                <ContentTemplate>
                                    <div class="classSearchHeader">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="Alb_Photo_ID"
                                                        Width="100%" CssClass="Grid" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:CheckAll_One(this);" runat="server"
                                                                        ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn Visible="False" DataField="Alb_Photo_ID">
                                                                <HeaderStyle Width="1%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblHinhanh%>">
                                                                <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblUrlPath" Text='<%# DataBinder.Eval(Container.DataItem, "Abl_Photo_Origin")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <div>
                                                                        <img style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Abl_Photo_Origin"))%>;
                                                                            padding-top: 3px; border: 0; cursor: pointer;" src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Abl_Photo_Origin")) %>"
                                                                            width="120px" height="80px" align="middle" alt="" onclick="return openNewImage(this, 'close')" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtTitle" Width="90%" TextMode="MultiLine" Rows="3"
                                                                        CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "Abl_Photo_Name")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuthich%>">
                                                                <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtDesc" Width="90%" TextMode="MultiLine" Rows="3"
                                                                        CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "Abl_Photo_Desc")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_tacgia" CssClass="inputtext" Width="95%" runat="server" Text='<%#GetTen_BD(DataBinder.Eval(Container.DataItem, "AuthorID"))%>'>
                                                                    </asp:TextBox>
                                                                    <asp:TextBox ID="txt_tacgiaID" CssClass="inputtext" Width="150px" runat="server"
                                                                        Style="display: none"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhuanbut%>">
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txt_tienNB" Width="90%" onKeyPress="return check_num(this,15,event);"
                                                                        onkeyup="javascript:return CommaMonney(this.id);" CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "TongtienTT")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txt_OrderByPhoto" onKeyPress='return check_num(this,5,event)'
                                                                        CssClass="inputtext" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "OrderByPhoto")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnIsReporter" runat="server" ImageUrl='<%#IsStatusGet(DataBinder.Eval(Container.DataItem, "Abl_Isweek_Photo").ToString())%>'
                                                                        ImageAlign="AbsMiddle" ToolTip="Trạng thái" CommandName="Edit" CommandArgument="IsNoibat"
                                                                        BorderStyle="None" />
                                                                    <%--<asp:ImageButton ID="btnIsReporter" runat="server" ImageUrl='<%#IsStatusGet(DataBinder.Eval(Container.DataItem, "Abl_Isweek_Photo").ToString())%>'
                                                                        ImageAlign="AbsMiddle" ToolTip="Trạng thái" CommandName="Edit" CommandArgument="IsNoibat"
                                                                        BorderStyle="None" Visible="false" />
                                                                    <asp:CheckBox ID="chkIsHomeAlbum" onclick="OnCheckBox(this.id);" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "Abl_Isweek_Photo")%>'
                                                                        ToolTip="Ảnh nổi bật"></asp:CheckBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaycapnhat%>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Alb_Photo_ID")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 5px">
                                                    <span style="font-weight: bold; font-size: medium; color: #EB0C27; float: left;">
                                                        <asp:Literal runat="server" ID="litMessages"></asp:Literal>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 30%; text-align: left">
                                                    <asp:Button runat="server" ID="LinkCancel" CssClass="iconDel" CausesValidation="false"
                                                        Font-Bold="true" OnClick="LinkCancel_Click" Text="<%$Resources:cms.language, lblXoa%>" />
                                                    <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                                        Text="<%$Resources:cms.language, lblLuu%>" />
                                                    <asp:Button runat="server" ID="Link_Back" CssClass="iconExit" Font-Bold="true" CausesValidation="false"
                                                        OnClick="Link_Back_Click" Text="<%$Resources:cms.language, lblThoat%>" />
                                                </td>
                                                <td class="Titlelbl" style="text-align: right">
                                                    <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal">
                                                    </cc1:CurrentPage>
                                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged">
                                                    </cc1:Pager>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_bottom_left">
            </td>
            <td class="datagrid_bottom_center">
            </td>
            <td class="datagrid_bottom_right">
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(window).load(
            function() {
                $("#<%=FileUpload1.ClientID %>").uploadify({
                'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
                    'buttonText': '<%= CommonLib.ReadXML("lblChonanh")%>',
                    'uploader': 'UploadImgMulti.ashx?user=<%=GetUserName()%>',
                    'folder': 'Upload',
                    'fileDesc': 'Images Files',
                    'fileExt': '*.jpg;*.png;*.gif;*.jpeg;',
                    'multi': true,
                    'auto': true,
                    'onQueueComplete': function(queueData) {
                        var url = '<%=Global.ApplicationPath %>/PhongSuAnh/PhotoAlbum_EditMullti.aspx?<%=getUrlParameter()%>';
                        window.location = url;
                    }
                });
            }
    );
    </script>

</asp:Content>
