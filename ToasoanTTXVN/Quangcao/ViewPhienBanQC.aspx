<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPhienBanQC.aspx.cs"
    Inherits="ToasoanTTXVN.Quangcao.ViewPhienBanQC" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Phiên bản quảng cáo</title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />

    <script language="javascript" type="text/javascript" src="../Dungchung/Scripts/Lib.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; float: left">
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td style="width: 60%; vertical-align: top; border: 1px solid #abcdef;">
                    <table cellspacing="2" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Tên quảng cáo:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                                <%=Tenquangcao%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Loại báo:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                                <%=Loaibao%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Trang:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                                <%=Sotrang%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Kích thước:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                                <%=Kichthuoc%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Ngày đăng:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                                <%=Ngaydang%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 12%; text-align: right; font-family: Arial; font-size: 14px;">
                                Nội dung:
                            </td>
                            <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="word-spacing: normal; width: 100%; padding-left: 20px;
                                padding-right: 30px; padding-top: 5px;">
                                <%=Noidung%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 800px; height: 1px" bgcolor="#cccccc">
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
                <td style="width: 40%; vertical-align: top; border: 1px solid #abcdef;">
                    <table style="width: 100%" border="0px">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                    <tr>
                                        <td align="left" style="height: 1px; background-color: #cccccc" colspan="2">
                                            <asp:CheckBox ID="checkversion" runat="server" Checked="false" ForeColor="Blue" Font-Size="Medium"
                                                Font-Names="Time news roman" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial; font-size: 14px;">Người nhập: <b>
                                                <%=Nguoinhap%></b></span>
                                        </td>
                                        <td>
                                            <span style="font-family: Arial; font-size: 14px;">Người sửa: <b>
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
                                    CellPadding="1" DataKeyField="ID" OnEditCommand="DataGridTacGiaTinBai_EditCommand">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Height="25px" Width="50%" CssClass="GridBorderVerSolid">
                                            </ItemStyle>
                                            <HeaderTemplate>
                                                Người duyệt
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton Text='<%# HPCBusinessLogic.UltilFunc.GetUserFullName( DataBinder.Eval(Container.DataItem, "Nguoitao")) %>'
                                                    CssClass="linkGridForm" Enabled='true' runat="server" ID="linkTittle" CommandName="Edit"
                                                    CommandArgument="Edit" ToolTip="Detail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Height="25px" Width="50%" CssClass="GridBorderVerSolid">
                                            </ItemStyle>
                                            <HeaderTemplate>
                                                Ngày gửi
                                            </HeaderTemplate>
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
                                    DataKeyField="ID" Width="100%">
                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 90%; float: left;">
                                            <img id="imgView" style="cursor: hand" onclick="OpenImage('<%=Global.PathImageFTP%><%# DataBinder.Eval(Container.DataItem, "PathFile")%>')"
                                                height="103px" src="<%#HPCComponents.CommonLib.GetPathImgWordPDF( DataBinder.Eval(Container.DataItem, "PathFile")) %>"
                                                width="121px" border="1" alt="" />
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
