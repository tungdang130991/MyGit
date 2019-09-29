<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnPhamBao.aspx.cs" Inherits="ToasoanTTXVN.AnPhamBao" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ẤN PHẨM BÁO</title>
    <link href="Dungchung/Style/style.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 40px">
    </div>
    <div style="text-align: center">
        <span id="tieude" runat="server" style="font-family: Arial; font-size: 20px; font-weight: bold;
            color: Green">CHỌN QUY TRÌNH BIÊN TẬP</span>
    </div>
    <div style="height: 20px">
    </div>
    <div class="Iconqtbt">
        <asp:DataList ID="DataListAnpham" runat="server" OnItemCommand="DataListAnpham_ItemCommand"
            RepeatDirection="Horizontal" RepeatColumns="6" CellPadding="0" CellSpacing="0"
            BorderWidth="0" RepeatLayout="Table">
            <ItemTemplate>
                <div class="in">
                    <asp:LinkButton ID="lnkimg" runat="server" CssClass="removeUnder" CommandName="cmd"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID")%>'>
                                <img src='<%=Global.ApplicationPath%><%#Eval("Url_Img")%>' style="border:0; width:80px; height:80px" alt="" />
                    </asp:LinkButton>
                    <p>
                        <asp:LinkButton ID="lnkanpham" runat="server" CssClass="removeUnder" CommandName="cmd"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID")%>' Text='<%# Bind("Ten_QuyTrinh")%>'>
                        </asp:LinkButton></p>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
