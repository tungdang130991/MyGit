<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndPrint.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.ViewAndPrint" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html dir="ltr" lang="en" xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="REFRESH" content="1800" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="robots" content="INDEX,FOLLOW">
    <title>XEM TRƯỚC TIN TỨC</title>
    <%=CssViewLangues()%>
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.js"></script>
</head>
<body style="background-color: White; margin-left: 2px;">
    <form id="form1" runat="server">
    <div id="content">
        <div id="content-in">
            <div class="nav navDetail">
                <div class="title-nav b1">
                    <h5>
                        <asp:Literal ID="litCatName" runat="server"></asp:Literal>
                    </h5>
                </div>
                <div class="box-content">
                    <div class="box-detail-news">
                        <div class="item-list-news">
                            <div style="font-family: Arial; font-size: 15px; text-decoration: underline; text-align: left;">
                                <em>
                                    <asp:Literal ID="litDanNhap" runat="server"></asp:Literal></em>
                            </div>
                            <div class="title-news">
                                <h3>
                                    <asp:Literal ID="litTittle" runat="server"></asp:Literal>
                                </h3>
                            </div>
                            <div class="sub-link">
                                <div class="info-link">
                                    <p>
                                        <asp:Literal ID="LitDatePublisher" runat="server"></asp:Literal>
                                    </p>
                                </div>
                                <div class="info-comment">
                                    <ul>
                                        <li style="background: transparent url('<%=Global.ApplicationPath %>/Dungchung/Images/icon-print.gif') no-repeat center left;
                                            padding-left: 23px;"><a href="javascript:window.print()">Print </a></li>
                                    </ul>
                                </div>
                            </div>
                            <div>
                                <div>
                                    <p>
                                        <strong>
                                            <asp:Literal ID="LitSummery" runat="server"></asp:Literal></strong>
                                    </p>
                                    <p>
                                        <asp:Literal ID="litContent" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </div>
                            <div class="sub-link">
                                <div class="info-comment">
                                    <ul>
                                        <li style="background: transparent url('<%=Global.ApplicationPath %>/Dungchung/Images/icon-print.gif') no-repeat center left;
                                            padding-left: 23px;"><a href="javascript:window.print()">Print </a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="content-news">
                                <div class="listdetail">
                                    <p>
                                        <strong style="font-size: 15px;">
                                            <asp:Literal ID="literCount" Text="Tổng số :" runat="server"></asp:Literal></strong>
                                        <strong style="font-size: 15px; color: Red;">
                                            <asp:Literal ID="LitCount" runat="server"></asp:Literal></strong>
                                    </p>
                                    <p>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="line_dot">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
