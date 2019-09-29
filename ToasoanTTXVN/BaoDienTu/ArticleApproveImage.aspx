<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleApproveImage.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.ArticleApproveImage" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quản lý file</title>
    <link rel="Stylesheet" type="text/css" href="CSS/uploadify.css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.js" type="text/javascript"></script>
    
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />
    
    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>
    
    <script src="../Dungchung/Scripts/Lib.js" type="text/javascript"></script>

    <link href="../Dungchung/Style/style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
     function AutoCompleteSearch_Author(arr_Name, arr_ID)
        {  
            var i=0;
            var arr_ID_Name =arr_Name.split(",");
            var arr_ID_ID = arr_ID.split(",");
            for(i=0;i<arr_ID_Name.length;i++)
            {
                var a = arr_ID_Name[i];
                var b = arr_ID_ID[i]
                Run(a,b);
            }
        }
        
      function Run(a,b){
          $(document).ready(function() {
              $('#' + a).autocomplete("../PhongSuAnh/AutoCompleteSearch.ashx").result(
                function (event, data, formatted) {
                    if (data) {$('#'+b).val(data[1]);}
                    else { $('#'+b).val('0');}
                });});
        }
    </script>
    <style type="text/css">
        BODY
        {
            border-right: 0px;
            border-top: 0px;
            margin: 0px;
            overflow: hidden;
            border-left: 0px;
            border-bottom: 0px;
            background-color: buttonface;
        }
        .button
        {
            font-size: 12px;
            color: #000099;
            font-family: 'Arial';
        }
        .inputtext
        {
            border-right: #cccccc 1px solid;
            border-top: #cccccc 1px solid;
            font-weight: normal;
            font-size: 12px;
            border-left: #cccccc 1px solid;
            cursor: hand;
            color: #000000;
            border-bottom: #cccccc 1px solid;
            font-family: Arial, Helvetica, sans-serif;
        }
        .Time
        {
            font-weight: normal;
            font-size: 11px;
            color: #000000;
            font-family: Arial, Helvetica, sans-serif;
        }
        #displayContainer
        {
            padding-right: 1px;
            padding-left: 1px;
            scrollbar-face-color: #cacaca;
            font-size: 10pt;
            padding-bottom: 1px;
            margin: 0px;
            scrollbar-highlight-color: #cacaca;
            overflow: auto;
            width: 100%;
            scrollbar-shadow-color: #cacaca;
            color: #000000;
            padding-top: 1px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            height: 350px;
        }
        .pageNav
        {
            font-weight: normal;
            font-size: 12px;
            color: #000099;
            font-family: 'Tahoma';
        }
        TD.currentFolder
        {
            border-right: #cccccc 1px solid;
            border-top: #cccccc 1px solid;
            font-weight: bold;
            font-size: 12px;
            border-left: #cccccc 1px solid;
            color: #000099;
            border-bottom: #cccccc 1px solid;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f1f1f1;
        }
        .currentFolderText
        {
            font-weight: bold;
            font-size: 12px;
            color: #000099;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f1f1f1;
        }
        TD.currentFolderContent
        {
            border-right: #cccccc 2px solid;
            border-top: #cccccc 2px solid;
            font-weight: bold;
            font-size: 12px;
            border-left: #cccccc 2px solid;
            color: #000099;
            border-bottom: #cccccc 2px solid;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #ffffff;
        }
        fieldset
        {
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            border: solid 1px #d4d4d4;
            padding: 2px;
        }
        legend
        {
            color: black;
            font-size: 100%;
            width: 150px;
        }
        .HeaderGrid
        {
            background: transparent url(../Dungchung/Images/bgMenu.jpg) repeat-x scroll left top;
            height: auto;
            color: White;
            font-size: 12px;
            height: 30px;
            font-weight: bold;
            padding-left: 3px;
        }
        .UpdateProgressContent
        {
            position: absolute;
            top: 50%;
            left: 50%;
            width: 600px;
            height: 300px;
            z-index: 1001;
            margin-top: -150px;
            margin-left: -300px;
        }
        .UpdateProgressBackground
        {
            width: 100%;
            height: 100%;
            z-index: 1000;
            background-color: #cccccc;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true"
        AssociatedUpdatePanelID="UpdatePanel1">
        <progresstemplate>
        <script type="text/javascript">document.write("<div class='UpdateProgressBackground'></div>");</script>
        <center>
            <div class="UpdateProgressContent">
                <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Dungchung/Images/processing_icon.gif" />
                <br />
                <font color="black">Waiting...</font>
            </div>
        </center>
    </progresstemplate>
    </asp:UpdateProgress>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 5px">
    <form id="frmUpLoad" method="post" enctype="multipart/form-data" runat="server">
    <cc2:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel">+ <asp:Label ID="Label2" runat="server" Text="<%$Resources:cms.language, titDuyetanh%>"></asp:Label></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                    <tr>
                        <td align="center" style="width: 100%" colspan="2">
                        <b><asp:Label ID="Label1" runat="server" Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label> :</b> 
                            <asp:Label ID="lbl_tieude" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; height: 25px; padding-left: 4px" bgcolor="#cccccc"
                            class="TitlePanel">
                            + <asp:Label ID="Label3" runat="server" Text="<%$Resources:cms.language, titDsAnh%>"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                
                                    <asp:Label ID="lbl_News_ID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:DataGrid ID="dgr_anh" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                        CellPadding="0" DataKeyField="ID" CssClass="Grid"  Font-Size="Medium" OnEditCommand="dgr_anh_EditCommand">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                       <AlternatingItemStyle CssClass="GridAltItem" />
                                       <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# Container.ItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Font-Bold="true" CssClass="GridBorderVerSolid">
                                                </ItemStyle>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <img alt="<%#Cut_Filename(DataBinder.Eval(Container.DataItem,"ImageFileName"))%>"
                                                        src="<%=Global.TinPathBDT%><%#DataBinder.Eval(Container.DataItem,"ImgeFilePath")%>"
                                                        border="1" width="80px" height="60px" style="cursor: pointer;" title="<%#DataBinder.Eval(Container.DataItem,"ImageFileName")%>" onclick="return openNewImage(this, '')" >
                                                    <br />
                                                    <asp:Label ID="lbl_Image_ID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Image_ID")%>'></asp:Label>
                                                    <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                                    <asp:Label ID="lbl_URL" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ImgeFilePath")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%-- <asp:TemplateColumn HeaderText="Tác giả">
                                                <ItemTemplate>
                                                    
                                                    <asp:TextBox ID="txt_nguonanh" runat="server" Text='<%#Eval("Nguon_Anh")%>'></asp:TextBox>
                                                    <asp:TextBox ID="txt_tacgiaID" runat="server" style="display:none"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>
                                           <asp:TemplateColumn HeaderText="Chất lượng">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_chatluongID" runat="server" Text='<%#Eval("Chat_Luong")%>' Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="ddlImage_chatluong"  runat="server" CssClass="inputtext" Width="90px" TabIndex="13">
                                                        <asp:ListItem Value="0">Copy</asp:ListItem>
                                                        <asp:ListItem Value="1">Thực hiện</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>--%>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhuanbut%>">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_tiennhanbut" onKeyPress="return check_num(this,15,event);" 
                                                    onkeyup="javascript:return CommaMonney(this.id);"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Tien_Nhanbut")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <%--<asp:TemplateColumn HeaderText="xem">
                                                <ItemTemplate>
                                                    <img width="20px" src="../Dungchung/Images/view.gif" onclick="return openNewImage('<%#GetFileURL(DataBinder.Eval(Container.DataItem,"ImgeFilePath"))%>')"
                                                        title="xem ảnh"></img>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Lưu">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdate" Width="20px" runat="server" ImageUrl="~/Dungchung/Images/save.gif"
                                                        ToolTip="Cập nhật thông tin" CommandName="Edit" CommandArgument="update" BorderStyle="None" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>--%>
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                      <tr>
                        <td style="height:10px">    </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <%--<td align="left"> 
                                        Tác giả : <asp:TextBox ID="txt_tacgia" runat="server"></asp:TextBox>
                                      <asp:TextBox ID="txt_tacgiaID" runat="server" style="display:none"></asp:TextBox>
                                    </td>--%>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:cms.language, lblNhuanbut%>"></asp:Label> : <asp:TextBox ID="txt_tienNB"  onKeyPress="return check_num(this,15,event);" 
                                                    onkeyup="javascript:return CommaMonney(this.id);"  runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="checkCham" runat="server"  />
                                        <asp:Button ID="cmd_chamall" runat="server" Text="<%$Resources:cms.language, lblChamnhuanbut%>" 
                                            onclick="cmd_chamall_Click" />
                                    </td>
                                    
                                    
                                </tr>
                            </table>
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
    </form>
</body>
</html>
