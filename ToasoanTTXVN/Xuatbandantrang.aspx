<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xuatbandantrang.aspx.cs"
    Inherits="ToasoanTTXVN.Xuatbandantrang" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XUẤT BẢN DÀN TRANG</title>
    <link type="text/css" rel="Stylesheet" href="Dungchung/Style/style.css" />

    <script language="javascript" src="Dungchung/Scripts/JSDantrang/prototype.js" type="text/javascript">
    </script>

    <script language="javascript" src="Dungchung/Scripts/JSDantrang/effects.js" type="text/javascript">
    </script>

    <script language="javascript" src="Dungchung/Scripts/JSDantrang/scriptaculous.js"
        type="text/javascript">
    </script>

    <script language="javascript" src="Dungchung/Scripts/Lib.js" type="text/javascript"></script>

    <script language="JavaScript" src="Dungchung/Scripts/JSDantrang/vietuni.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="Dungchung/Scripts/JSDantrang/vumods.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="Dungchung/Scripts/JSDantrang/vumaps.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="Dungchung/Scripts/JSDantrang/vumaps2.js" type='text/javascript'>
    </script>

    <script language="javascript" src="Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        var tmp_Window;

        function OpenImage(_value) {

            tmp_Window = window.open("UploadFileMulti/ViewImages.aspx?url=" + _value, "", "directories=no,menubar=no, resizable=no,toolbar=no");

        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="60000" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function selectText(containerid) {
            if (document.selection) {
                var range = document.body.createTextRange();
                range.moveToElementText(document.getElementById(containerid));
                range.select();
            } else if (window.getSelection) {
                var range = document.createRange();
                range.selectNode(document.getElementById(containerid));
                window.getSelection().addRange(range);
            }
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td style="text-align: center; width: 100%">
                <table id="tableall" cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td valign="top" style="text-align: left; width: 100%">
                            <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnTimkiem" />
                                </Triggers>
                                <ContentTemplate>
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left">
                                        <tr>
                                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                                Loại ấn phẩm:
                                            </td>
                                            <td style="width: 40%; text-align: left">
                                                <asp:DropDownList ID="cboAnPham" runat="server" Width="50%" CssClass="inputtext"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged" TabIndex="1">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                                Số báo:
                                            </td>
                                            <td style="width: 40%; text-align: left">
                                                <asp:DropDownList ID="cboSoBao" runat="server" Width="50%" CssClass="inputtext" TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                                Số trang:
                                            </td>
                                            <td style="width: 40%; text-align: left">
                                                <asp:DropDownList AutoPostBack="true" ID="cboTrang" runat="server" Width="50%" CssClass="inputtext"
                                                    OnSelectedIndexChanged="cboTrang_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right">
                                            </td>
                                            <td style="width: 40%; text-align: left">
                                                <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                                    Text="Tìm kiếm"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; width: 100%">
                            <asp:UpdatePanel ID="UpdatePanelList" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="dgr_tintuc" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="PanelLisTin" runat="server" GroupingText="Tin bài xuất bản">
                                        <div style="text-align: center; width: 100%">
                                            <asp:DataGrid ID="dgr_tintuc" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" DataKeyField="Ma_Tinbai"
                                                BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                OnEditCommand="dgData_EditCommand" OnItemDataBound="dgData_ItemDataBound">
                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Ảnh
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server" Visible='<%# CheckImageNews(DataBinder.Eval(Container.DataItem, "Ma_Tinbai")) %>'>
                                                                <img src='<%=Global.ApplicationPath%>/Dungchung/Images/vietnam.gif' alt="Có ảnh Đăng"
                                                                    style="border: 0px; text-align: center; width: 20px">
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Width="50%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Tên bài viết
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude" )%>'
                                                                runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="xem bài"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Trang
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                        <div style="text-align: right" class="pageNav">
                                            <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                            </cc1:CurrentPage>
                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baichoxuly"></cc1:Pager>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: left; width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="dgr_tintuc" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="PnlDetail" runat="server" GroupingText="Tin chi tiết">
                                        <input type="hidden" id="txtOriginal" />
                                        <table cellspacing="2" cellpadding="2" width="100%" border="0" bgcolor="#ffffff">
                                            <tr>
                                                <td class="chuyenmuc">
                                                    <%=Chuyenmuc%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="TieudeBKHCM" id="td_tieude" style="font-size: 13pt;">
                                                    <%=Tieude%>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="store_content" type="hidden" name="_store_content" runat="server" style="width: 80px;
                                                        height: 25px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="900px" valign="top" class="NoidungBKHCM" align="left" id="Noidung" style="font-family: VnHelv2;
                                                    font-size: 13pt;" onmousedown="selectText('Noidung')">
                                                    <%=Noidung%>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <asp:DataList ID="DataListAnh" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <ItemStyle Width="50%" BorderWidth="0" HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td valign="top" width="20%">
                                                                    <img id="imgView" style="cursor: hand" onclick="OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>')"
                                                                        height="103" src="<%= Global.ApplicationPath%>/<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>"
                                                                        width="121" border="1" alt="" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="ImgDesc1_<%# DataBinder.Eval(Container.DataItem, "Ma_Anh") %>" class="GhichuBKHCM">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Chuthich")%>
                                                                    <input id="<%# DataBinder.Eval(Container.DataItem, "Ma_Anh") %>" name="<%# DataBinder.Eval(Container.DataItem, "Ma_Anh") %>"
                                                                        value="" type="hidden">
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                        </table>

                                        <script language="javascript" type="text/javascript">

                                            var FontID = '<%=ConfigurationManager.AppSettings["FontID"].ToString() %>';
                                            HPC_Convert_Text('txtOriginal', 'td_tieude', FontID);
                                            HPC_Convert_Text('txtOriginal', 'Noidung', FontID);
                                            ConvertBKHCM2_IMGDESC('txtOriginal', 'DataListAnh', FontID);
        
                                        </script>

                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
