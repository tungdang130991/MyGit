<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewHistory.aspx.cs" Inherits="ToasoanTTXVN.View.ViewHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal runat="server" ID="TIT"></asp:Literal>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="REFRESH" content="1800" />
    <meta name="author" content="HPC-LTD" />
    <link href="Layout/Styles.css" rel="stylesheet" type="text/css" />
    <link href="Layout/menu.css" rel="stylesheet" type="text/css" />
    <link href="Layout/jcarousel.css" rel="stylesheet" type="text/css" />
    <link href="Layout/simple-scroll.css" rel="stylesheet" type="text/css" />
    <link href="Layout/fonts.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lbl_index" runat="server" Text="" Visible="false"></asp:Label>
    <div id="wrapper">
        <div id="main">
            <div class="pagel" style="margin-left: 120px;">
                <div class="p-category">
                    <div class="_category">
                        <asp:Literal runat="server" ID="litCategorys"></asp:Literal>
                    </div>
                </div>
                <div class="p-news pdt15">
                    <div class="detail">
                        <div class="title">
                            <asp:Literal runat="server" ID="litTittle"></asp:Literal>
                        </div>
                        <div class="func">
                            <span class="time">
                                <asp:Literal runat="server" ID="litDateTime"></asp:Literal></span>
                            <div class="share">
                                <a href="javascript:window.print();" class="print" style="font-weight: bold;"><%= HPCComponents.CommonLib.ReadXML("titIn")%></a>
                            </div>
                        </div>
                        <div class="sapo">
                            <asp:Literal runat="server" ID="litSapo"></asp:Literal>
                        </div>
                        <div class="content">
                            <asp:Literal runat="server" ID="litContents"></asp:Literal>
                        </div>
                        <asp:Literal runat="server" ID="litAuthor"></asp:Literal>
                        <div style="width: 100%; height: 20px;">
                        </div>
                        <div>
                            <b><%= HPCComponents.CommonLib.ReadXML("lblNhuanbut")%>:</b>
                            <asp:Literal ID="LitNhuanbut" runat="server"></asp:Literal>
                            <br />
                            <b><%= HPCComponents.CommonLib.ReadXML("lblTongso")%>:</b> <strong style="font-size: 15px; color: Red;">
                                <asp:Literal ID="LitCount" runat="server"></asp:Literal></strong>
                            <br />
                            <b><%= HPCComponents.CommonLib.ReadXML("lblNguoinhap")%>:</b>
                            <asp:Literal ID="Literal_nguoinhap" Text="" runat="server"></asp:Literal>
                            <br />
                            <b><%= HPCComponents.CommonLib.ReadXML("lblNguoigui")%>:</b>
                            <asp:Literal ID="Literal_nguoiluu" Text="" runat="server"></asp:Literal>
                            <br />
                            <b><%= HPCComponents.CommonLib.ReadXML("lblNgaygui")%>:</b>
                            <asp:Literal ID="Literal_ngayluu" Text="" runat="server" />
                            <br />
                        </div>
                        <div>
                            <asp:ImageButton ID="Imageback" runat="server" Height="30px" Width="30px" ImageUrl="~/Dungchung/Images/back-icon.png"
                                OnClick="Imageback_Click" />
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="Imagenext" runat="server" Height="30px" Width="30px" ImageUrl="~/Dungchung/Images/next-icon.png"
                                OnClick="Imagenext_Click" />
                        </div>
                        <asp:DataGrid ID="dgr_tintuc1" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyField="ID" CssClass="Grid" OnEditCommand="dgData_EditCommand" OnItemDataBound="dgData_ItemDataBound">
                            <ItemStyle CssClass="GridItem"></ItemStyle>
                            <AlternatingItemStyle CssClass="GridAltItem" />
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="linkEdit" Text=' <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("News_EditorID"))%>'
                                            runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Xem nội dung "></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                    <ItemTemplate>
                                        <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateEdit")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateEdit")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                        </asp:Label>
                                        <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTu%>">
                                    <ItemTemplate>
                                        <%#Getstatus(Eval("News_Status"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDen%>">
                                    <ItemTemplate>
                                        <%#Getstatus(Eval("Action"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
