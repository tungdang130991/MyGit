<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDetails.aspx.cs" Inherits="ToasoanTTXVN.View.ViewDetails" %>

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
                            <span class="time"><asp:Literal runat="server" ID="litDateTime"></asp:Literal></span>
                            <div class="share">
                                <a href="javascript:window.print();" class="print" style="font-weight:bold;">IN BÀI VIẾT</a>
                            </div>
                        </div>
                        <div class="sapo">
                            <asp:Literal runat="server" ID="litSapo"></asp:Literal>
                        </div>
                        <div class="content">
                            <asp:Literal runat="server" ID="litContents"></asp:Literal>
                        </div>
                        <asp:Literal runat="server" ID="litAuthor"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

