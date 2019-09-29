<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewNews1.aspx.cs" Inherits="ToasoanTTXVN.DeTai.ViewNews1" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html dir="ltr" lang="en" xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="REFRESH" content="5400" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="description" content="VNp -  ">
    <meta name="keywords" content="tin tức, thời sự, xã hội,thể thao">
    <meta name="robots" content="INDEX,FOLLOW">
    <title>BÁO ẢNH - DTMN VIỆT NAM </title>
    <link href="../Dungchung/CSSVIEW/import.css" rel="Stylesheet" rev="Stylesheet" />
    <link href="../Dungchung/CSSVIEW/default.css" rel="Stylesheet" rev="Stylesheet" />
</head>
<body style="background-color: White; margin-left: 2px;">
    <form id="form1" runat="server">
    <div id="content">
        <div id="content-in">
            <div class="nav navDetail">
                <div class="title-nav b1">
                    <h5>
                        <u>
                            <asp:Label runat="server" Text="Chuyên mục: " Font-Bold="true"></asp:Label></u>
                        <i>
                            <asp:Literal ID="litCatName" runat="server"></asp:Literal></i>
                    </h5>
                </div>
                <div class="box-content">
                    <div class="box-detail-news">
                        <div class="item-list-news">
                            <div style="font-family: Arial; font- font-size: 13px; text-decoration: underline;
                                text-align: left;">
                                <em>
                                    <asp:Literal ID="litDanNhap" runat="server"></asp:Literal></em>
                            </div>
                            <div class="title-news">
                                <h5>
                                    <u>
                                        <asp:Label runat="server" Text="Tên đề tài:" Font-Bold="true"></asp:Label></u>
                                    <i>
                                        <asp:Literal ID="litTittle" runat="server"></asp:Literal></i><br />
                                    <br />
                                    <u>
                                        <asp:Label ID="lbTacGia" runat="server" Text="Người viết :" Font-Bold="true"></asp:Label></u>
                                    <i>
                                        <asp:Literal ID="litTacgia" runat="server"></asp:Literal></i>
                                </h5>
                            </div>
                            <div class="info-link">
                                <p>
                                    <asp:Literal ID="LitDatePublisher" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <div class="content-news">
                                <div class="listdetail">
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
                                        <strong>
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal></strong>
                                    </p>
                                    <p>
                                        <asp:Literal ID="lit_baiviet" runat="server"></asp:Literal>
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
