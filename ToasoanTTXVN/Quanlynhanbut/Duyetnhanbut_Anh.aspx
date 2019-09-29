<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Duyetnhanbut_Anh.aspx.cs" Inherits="ToasoanTTXVN.Quanlynhanbut.Duyetnhanbut_Anh" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <%--<script language="javascript" type="text/javascript" src='http://toquoc.vn/cms/Scripts/Lib.js'></script>--%>

    <meta http-equiv="REFRESH" content="3600" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="description" content="VNp -  ">
    <meta name="robots" content="INDEX,FOLLOW">
   <%-- <link type="text/css" rel="Stylesheet" href="http://toquoc.vn/cms/Style/style.css" />--%>
    <link href="../Dungchung/Style/style.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/Lib.js" type="text/javascript"></script>
    <title>CHẤM NHẬN BÚT CHO ẢNH</title>

    <script type="text/javascript" language="javascript">
   
    function openNewImage(file, imgText) {
            if (file.lang == 'no-popup') return;
            picfile = new Image();
            picfile.src = (file.src);
            width = picfile.width;
            height = picfile.height;

            if (imgText != '' && height > 0) {
                height += 40;
            }
            else if (height == 0) {
                height = screen.height;
            }

            winDef = 'status=no,resizable=yes,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=yes,height='.concat(height).concat(',').concat('width=').concat(width).concat(',');
            winDef = winDef.concat('top=').concat((screen.height - height) / 2).concat(',');
            winDef = winDef.concat('left=').concat((screen.width - width) / 2);
            newwin = open('', '_blank', winDef);

            newwin.document.writeln('<style>a:visited{color:blue;text-decoration:none}</style>');
            newwin.document.writeln('<body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">');
            newwin.document.writeln('<div style="width:100%;height:100%;overflow:auto;"><a style="cursor:pointer" href="javascript:window.close()"><img src="', file.src, '" border=0></a>');
            if (imgText != '') {
                newwin.document.writeln('<div align="center" style="padding-top:5px;font-weight:bold;font-family:arial,Verdana,Tahoma;color:blue">', imgText, '</div></div>');
            }
            newwin.document.writeln('</body>');
            newwin.document.close();
        }
    </script>

    <style type="text/css">
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
        .HideLable
        {
            display: none;
        }
        .boundposition
        {
            position: relative;
            width: 50px;
            float: left;
            top: 0;
            left: 0;
        }
        .Classtextbox
        {
            width: 30px;
            position: absolute;
            top: -10px;
            left: 0px;
            z-index: 2;
        }
        .Classdrop
        {
            top: -10px;
            left: 0px;
            z-index: 1;
            width: 51px;
            position: absolute;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txt_luong" runat="server" Text="85000" CssClass="HideLable"></asp:TextBox>
        <table border="0" cellpadding="0" width="100%" cellspacing="0">
            <tr>
                <td class="datagrid_top_left">
                </td>
                <td class="datagrid_top_center">
                    <table border="0" cellpadding="1" cellspacing="1" style="float: left;">
                        <tr>
                            <td>
                                <img alt="" src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px" height="16px" />
                            </td>
                            <td style="vertical-align: middle;">
                                <span class="TitlePanel">DANH SÁCH PHIÊN ẢNH TRONG BÀI VIẾT</span>
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
                <td class="datagrid_content_center">
                    <asp:Label ID="lbl_News_ID" runat="server" Text="" Visible="false"></asp:Label>
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td>
                                Tiêu đề bài viết :
                                <asp:Label ID="lbl_tieude" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Tiền nhận but :
                                <asp:TextBox ID="txt_tienNB" onKeyPress="return check_num(this,15,event);" onkeyup="javascript:return CommaMonney(this.id);"
                                    runat="server"></asp:TextBox>
                                <asp:CheckBox ID="checkCham" runat="server"  />
                                <asp:Button ID="cmd_Chamnhanbut" runat="server" Text="Chấm nhận bút" OnClick="cmd_Chamnhanbut_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td  colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DataGrid ID="dgr_anh" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                    CellPadding="0" DataKeyField="ID" Font-Size="Medium" CssClass="Grid"  OnEditCommand="dgr_anh_EditCommand">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                STT
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <%# Container.ItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" CssClass="GridBorderVerSolid">
                                            </ItemStyle>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <img alt="<%#Cut_Filename(DataBinder.Eval(Container.DataItem,"ImageFileName"))%>"
                                                    src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "ImgeFilePath")) %>" title="<%#DataBinder.Eval(Container.DataItem,"ImageFileName")%>"
                                                    border="1" width="80px" height="60px" style="cursor: pointer;" onclick="return openNewImage(this, '')" />
                                                <asp:Label ID="lbl_Image_ID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Image_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Người tạo">
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nguồn ảnh">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Nguonanh" runat="server" Text='<%#Eval("Nguon_Anh")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tiền nhận bút">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_tien" onKeyPress="return check_num(this,15,event);" onkeyup="javascript:return CommaMonney(this.id);"
                                                    Width="65%" runat="server" Text='<%#Eval("Tien_Nhanbut")%>'></asp:TextBox>(VNĐ)
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        
                    </table>
                </td>
                <td class="datagrid_content_right">
                </td>
                <tr>
                    <td class="datagrid_bottom_left">
                    </td>
                    <td class="datagrid_bottom_center">
                    </td>
                    <td class="datagrid_bottom_right">
                    </td>
                </tr>
        </table>
    </div>
    </form>
</body>
</html>
