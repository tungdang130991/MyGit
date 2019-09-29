<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Commons.aspx.cs" Inherits="ToasoanTTXVN.Dungchung.Commons" %>

<%@ Import Namespace="HPCBusinessLogic" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-top: 10px">
        <div style="float: left; width: 48%; padding: 0; margin-left: 2%; margin-right: 1%">
            <div style="float: left; width: 100%; text-align: center; font-family: Arial; font-size: medium;
                font-weight: bold; border: solid #abcdef 1px; padding: 4px 0">
                <%=CommonLib.ReadXML("lblDanhsachtinbai")%></div>
            <div style="float: left; width: 100%; text-align: left; font-family: Times New Roman;
                font-size: medium; font-weight: bold; border-bottom: solid #abcdef 1px; border-left: solid #abcdef 1px;
                border-right: solid #abcdef 1px;">
                <asp:DataGrid ID="DataGridDanhSachTinBai" runat="server" Width="100%" AutoGenerateColumns="False"
                    CssClass="Grid" BorderColor="#7F8080" CellPadding="0" DataKeyField="Menu_ID"
                    BorderStyle="None" OnItemDataBound="DataGridDanhSachCV_ItemDataBound">
                    <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                    <AlternatingItemStyle CssClass="GridAltItem" />
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="Menu_ID" HeaderText="Menu_ID" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                            <HeaderTemplate>
                                Workflow
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class="linkGridDanhsachcongviec">
                                    <%# DataBinder.Eval(Container.DataItem, "href")%></span>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTinmoi%>">
                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="LabelBaiMoi" Visible='<%# DataBinder.Eval(Container.DataItem, "Off")%>'
                                    CssClass="linkGridDanhsachcongviec" runat="server" Text='<%#Eval("ChuaXL")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTinbientap%>">
                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="LabelBaidangxuly" Visible='<%# DataBinder.Eval(Container.DataItem, "Off")%>'
                                    CssClass="linkGridDanhsachcongviec" runat="server" Text='<%#Eval("DangXL")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTintralai%>">
                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="LabelBaiTraLai" Visible='<%# DataBinder.Eval(Container.DataItem, "Off")%>'
                                    CssClass="linkGridDanhsachcongviec" runat="server" Text='<%#Eval("Tralai")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
        <div style="float: left; width: 48%; padding: 0; margin-left: 1%">
            <div style="float: left; width: 96%; text-align: center; font-family: Arial; font-size: medium;
                font-weight: bold; border: solid #abcdef 1px; padding: 4px 1%">
                <%=CommonLib.ReadXML("lblDanhsachcongviec")%></div>
            <div style="float: left; width: 96%; text-align: left; font-family: Arial; font-size: 16px;
                border-bottom: solid #abcdef 1px; border-left: solid #abcdef 1px; border-right: solid #abcdef 1px;
                padding: 4px 1%">
                <a href="../Congviec/PhanCongCV.aspx?Menu_ID=173&Tab=0">
                    <%=CommonLib.ReadXML("lblCongviecduocgiao")%></a><asp:Label ID="lblthuchiencv" runat="server"></asp:Label>
            </div>
            <div style="float: left; width: 96%; text-align: left; font-family: Arial; font-size: 16px;
                border-bottom: solid #abcdef 1px; border-left: solid #abcdef 1px; border-right: solid #abcdef 1px;
                padding: 4px 1%">
                <a href="../Congviec/PhanCongCV.aspx?Menu_ID=173&Tab=1">
                    <%=CommonLib.ReadXML("lblCongviecdagiao")%></a><asp:Label ID="lblgiaoviec" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
