<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_SoBao.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.View_SoBao" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Danh sách số báo</title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-family: Times New Roman; font-size: medium">
        LỊCH SỬ THANH TOÁN HỢP ĐỒNG QUẢNG CÁO
    </div>
    <div style="height: 30px">
    </div>
    <div style="float: left; width: 80%; margin-left: 150px">
        <asp:Label ID="Label3" Width="38" runat="server">Số tiền</asp:Label>(<span style="color: Red;">*</span>):&nbsp;
        <asp:TextBox ID="txtsotien" runat="server" Width="100px" CssClass="inputtext">
        </asp:TextBox>
    </div>
    <div style="float: left; width: 80%; margin-left: 150px; margin-top: 3px">
        <asp:Label Width="38" ID="Label1" runat="server">Ngày thu</asp:Label>(<span style="color: Red;">*</span>):&nbsp;
        <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
            ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
            ScriptSource="../Dungchung/scripts/datepicker.js" ID="txtNgayThuTien" runat="server">
        </nbc:NetDatePicker>
    </div>
    <div style="float: left; width: 80%; margin-left: 150px; margin-top: 3px">
        <asp:Label Width="38" ID="Label2" runat="server">Tên người nộp</asp:Label>(<span
            style="color: Red;">*</span>):&nbsp;
        <asp:TextBox ID="txtTennguoinop" runat="server" Width="200px" CssClass="inputtext"
            TabIndex="1">
        </asp:TextBox>
    </div>
    <div style="height: 30px">
    </div>
    <div style="text-align: center">
        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="myButton" OnClick="btnSave_Click" />&nbsp;
        <input type="button" value="Thoát" class="myButton" onclick="window.close();" />
    </div>
    <div style="height: 30px">
        <strong>DANH SÁCH SỐ BÁO ĐƯỢC CHỌN QUẢNG CÁO</strong>
    </div>
    <div style="text-align: left">
        <asp:DataGrid runat="server" ID="dgLichsuthanhtoanQC" AlternatingItemStyle-BackColor="#F1F1F2"
            AutoGenerateColumns="false" DataKeyField="ID" BorderColor="#d4d4d4" CellPadding="0"
            BackColor="White" Width="100%" BorderWidth="1px">
            <ItemStyle CssClass="GridBorderVerSolid" Height="28px"></ItemStyle>
            <HeaderStyle CssClass="tbDataFlowList"></HeaderStyle>
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID">
                    <HeaderStyle Width="1%"></HeaderStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        Số HĐ
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#HPCBusinessLogic.UltilFunc.GetTenKhachHang(Eval("HOPDONG_SO"))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal12" runat="server" Text="Khách hàng"></asp:Literal>
                    </HeaderTemplate>
                    <ItemStyle Width="5%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%#HPCBusinessLogic.UltilFunc.GetTenKhachHang(DataBinder.Eval(Container.DataItem, "MA_KHACHHANG"))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal12" runat="server" Text="Số tiền"></asp:Literal>
                    </HeaderTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "SOTIEN")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal12" runat="server" Text="Ngày thu"></asp:Literal>
                    </HeaderTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%#Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "NGAYTHU")).ToString("dd/MM/yyyy")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal12" runat="server" Text="Người thu"></asp:Literal>
                    </HeaderTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%# HPCBusinessLogic.UltilFunc.GetTenTacGia(DataBinder.Eval(Container.DataItem, "NGUOITHU"))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal12" runat="server" Text="Người thanh toán"></asp:Literal>
                    </HeaderTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TENNGUOINOP")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div>
        <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
        </cc1:CurrentPage>
        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_ThanhtoanQC"></cc1:Pager>
    </div>
    </form>
</body>
</html>
