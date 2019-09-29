<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindListCongviec.aspx.cs"
    Inherits="ToasoanTTXVN.QL_SanXuat.BindListCongviec" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; float: left" id="DIVDataGridCongviec">

        <script type="text/javascript">
            $(document).ready(function() {
                $("#DIVDataGridCongviec :checkbox").click(function() {
                    var selected1 = this.value;
                    var e = $(".chkItemsListCV");
                    for (i = 0; i < e.length; i++) {
                        if (e[i].value != selected1)
                            e[i].checked = false;
                    }
                    var TextDiv = 'divtext' + $('#ctl00_MainContent_LayoutID').val();
                    var $loader = $('#' + TextDiv);
                    if (this.checked == true) {
                        var id_ = this.value;
                        var Links = 'BindTextCongviec.aspx?IDcv=' + id_ + '&LayoutID=' + $('#ctl00_MainContent_LayoutID').val();
                        $loader.load(Links);
                    }
                    else {
                        var Links = 'BindTextCongviec.aspx';
                        $loader.load(Links);
                    }
                });
            });
        </script>

        <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="Ma_Congviec"
            Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
            OnItemDataBound="grdList_ItemDataBound">
            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
            <AlternatingItemStyle CssClass="GridAltItem" />
            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
            <Columns>
                <asp:BoundColumn Visible="False" DataField="Ma_Congviec">
                    <HeaderStyle Width="1%"></HeaderStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        STT
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Nội dung
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="35%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Tencongviec")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        Số từ
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sotu")%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        Người nhận
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#BindUserName(DataBinder.Eval(Container.DataItem, "NguoiNhan").ToString())%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        Ngày hoàn thành
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("NgayHoanthanh") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayHoanthanh")).ToString("dd/MM/yyyy") : ""%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                    <HeaderTemplate>
                        Chọn
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" name="chk<%#Eval("Ma_Congviec")%>" class="chkItemsListCV"
                            id="chk<%#Eval("Ma_Congviec")%>" value="<%#Eval("Ma_Congviec")%>" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div style="width: 90%; float: left; padding-left: 5px;" class="pageNav">
        <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>
        &nbsp;
        <cc1:PagerAjax runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
    </div>
    </form>
</body>
</html>
