<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPhienBanTinBai.aspx.cs"
    Inherits="ToasoanTTXVN.Quytrinh.ViewPhienBanTinBai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />

    <script language="javascript" type="text/javascript" src="../Dungchung/Scripts/Lib.js"></script>

    <script type="text/javascript" src='../Dungchung/Scripts/AutoFormatDatTime.js'></script>

    <script src="../Dungchung/jscolor/jscolor.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td style="width: 65%; vertical-align: top; border: 1px solid #abcdef;">
                    <table cellspacing="2" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td>
                                <span class="chuyenmuc">
                                    <%=Chuyenmuc%></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <span style="font-family: Arial; font-size: 16px; font-weight: bold;">
                                    <%=Tieude%></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="ghichutinbai" align="center">
                                <%=Sotrang%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="word-spacing: normal; width: 100%; padding-left: 20px; padding-right: 30px;
                                padding-top: 30px;">
                                <%=Noidung%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="2" cellpadding="2" width="800px" border="0">
                                    <tr>
                                        <td style="font-family: Arial; font-size: 13px;">
                                            <%=CommonLib.ReadXML("lblSotu")%>: <b>
                                                <%=Sotu%></b>
                                        </td>
                                        <td style="font-family: Arial; font-size: 13px;">
                                            <%=CommonLib.ReadXML("lblNhuanbut")%>: <b>
                                                <%=Nhuanbut%></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 800px; height: 1px" bgcolor="#cccccc">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right; float: right">
                                <input id="button1" style="width: 100px;" onclick="window.close();" type="button"
                                    value="[Close]" class="iconExit" name="button1" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 35%; vertical-align: top; border: 1px solid #abcdef;">
                    <table style="width: 100%" border="0px">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                    <tr>
                                        <td align="left" style="height: 1px; background-color: #cccccc" colspan="2">
                                            <asp:CheckBox ID="checkversion" runat="server" Checked="false" ForeColor="Blue" Font-Size="Medium"
                                                Font-Names="Time news roman" />Track changes
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial; font-size: 14px;"><%=CommonLib.ReadXML("lblTacgia")%>: <b>
                                                <%=Tacgia%></b></span>
                                        </td>
                                        <td>
                                            <span style="font-family: Arial; font-size: 14px;"><%=CommonLib.ReadXML("lblNguoixuly")%>: <b>
                                                <%=nguoisua%></b></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:DataGrid ID="DataGridTacGiaTinBai" runat="server" Width="100%" BorderWidth="0px"
                                    CssClass="Grid" CellSpacing="1" ItemStyle-CssClass="tbDataFlowList" AllowSorting="True"
                                    PageSize="2" BackColor="#ffffff" AlternatingItemStyle-BackColor="#f1f1f1" AutoGenerateColumns="False"
                                    CellPadding="1" DataKeyField="Ma_Phienban" OnEditCommand="DataGridTacGiaTinBai_EditCommand">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixuly%>">
                                            <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Height="25px" Width="50%" CssClass="GridBorderVerSolid">
                                            </ItemStyle>
                                            
                                            <ItemTemplate>
                                                <asp:LinkButton Text='<%# HPCBusinessLogic.UltilFunc.GetUserFullName( DataBinder.Eval(Container.DataItem, "Ma_Nguoitao")) %>'
                                                    CssClass="linkGridForm" Enabled='true' runat="server" ID="linkTittle" CommandName="Edit"
                                                    CommandArgument="Edit" ToolTip="Detail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxuly%>">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Height="25px" Width="50%" CssClass="GridBorderVerSolid">
                                            </ItemStyle>
                                            
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "NgayTao")).ToString("dd/MM/yyyy HH:mm:ss")%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <asp:DataList ID="DataListAnh" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                    DataKeyField="Ma_Anh" Width="100%">
                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 90%; float: left;">
                                            <img id="imgView" style="cursor: hand" onclick="OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>')"
                                                height="103px" src="<%= Global.TinPath%>/<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>"
                                                width="121px" border="1" alt="" />
                                        </div>
                                        <div style="width: 90%; float: left; margin-top: 3px; text-align: left">
                                            <%=CommonLib.ReadXML("lblNhuanbut")%>:
                                            <asp:Label ID="lblnhuanbut" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nhuanbut") %>'>
                                            </asp:Label>
                                        </div>
                                        <div style="width: 90%; float: left; font-family: Arial; font-size: 13px" id="ImgDesc1_<%# DataBinder.Eval(Container.DataItem, "Ma_Anh") %>">
                                            <%=CommonLib.ReadXML("lblChuthich")%>:
                                            <%# DataBinder.Eval(Container.DataItem, "Chuthich")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
