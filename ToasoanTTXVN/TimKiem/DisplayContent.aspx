<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayContent.aspx.cs"
    Inherits="ToasoanTTXVN.TimKiem.DisplayContent" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="ToasoanTTXVN" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />
</head>
<body>
    <form id="fromcontents" runat="server">
    <div style="width: 100%; float: left; text-align: left;">
        <table class="Grid" id="listcontents" width="100%">
            <thead>
                <tr class="GridHeader" style="color: Black;">
                    <td style="width: 3%; text-align: center">
                        #ID
                    </td>
                    <td style="width: 30%; text-align: center">
                        <%=CommonLib.ReadXML("lblTieude")%>
                    </td>
                    <td style="width: 5%; text-align: center">
                        <%=CommonLib.ReadXML("lblTrang")%>
                    </td>
                    <td style="width: 12%; text-align: center">
                        <%=CommonLib.ReadXML("lblSobao")%>
                    </td>
                    <td style="width: 12%; text-align: center">
                        <%=CommonLib.ReadXML("lblTacgia")%>
                    </td>
                    <td style="width: 12%; text-align: center">
                        <%=CommonLib.ReadXML("lblNguoixuly")%>
                    </td>
                    <td style="width: 10%; text-align: center">
                        <%=CommonLib.ReadXML("lblNgayxuly")%>
                    </td>
                    <td style="width: 15%; text-align: center">
                        <%=CommonLib.ReadXML("lblTrangthai")%>
                    </td>
                </tr>
            </thead>
            <tbody>
                <% if (_dt != null && _dt.Rows.Count > 0)
                   {
                       foreach (DataRow row in _dt.Rows)
                       { %>
                <tr>
                    <td>
                        <%= row["Ma_Tinbai"].ToString()%>
                    </td>
                    <td style="text-align: left">
                        <div>
                            <div style="float: left; width: 100%; text-align: left">
                                <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%= row["Ma_Tinbai"].ToString()%>',50,500,100,800);">
                                    <b>
                                        <%=row["Tieude"].ToString()%></b></a>
                            </div>
                            <div style="float: left; width: 100%; text-align: left">
                                <div style="text-align: right; float: left; width: 20%; color: #AA0000">
                                    Chuyên mục:
                                </div>
                                <div style="text-align: left; float: left; width: 75%; padding-left: 5px; color: #006600">
                                    <b>
                                        <%=HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(row["Ma_Chuyenmuc"].ToString())%></b>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; text-align: left">
                                <div style="text-align: right; float: left; width: 20%; color: #AA0000">
                                    Loại báo:</div>
                                <div style="text-align: left; float: left; width: 75%; padding-left: 5px; color: #006600">
                                    <b>
                                        <%=HPCBusinessLogic.UltilFunc.GetTenAnpham(row["Ma_Anpham"].ToString())%></b></div>
                            </div>
                        </div>
                    </td>
                    <td style="text-align: center">
                        <span class="linkGridForm">
                            <%=HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(row["Ma_Tinbai"].ToString(), 1)%></span>
                    </td>
                    <td style="text-align: center">
                        <span class="linkGridForm">
                            <%=HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(row["Ma_Tinbai"].ToString(), 0)%></span>
                    </td>
                    <td>
                        <span class="linkGridForm">
                            <%=row["TacGia"].ToString()%></span>
                    </td>
                    <td style="text-align: left">
                        <span class="linkGridForm">
                            <%=HPCBusinessLogic.UltilFunc.GetUserFullName(HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(row["Ma_Tinbai"].ToString(), 0))%></span>
                    </td>
                    <td style="text-align: center">
                        <span class="linkGridForm">
                            <%=HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(row["Ma_Tinbai"].ToString(), 1)%></span>
                    </td>
                    <td>
                        <span class="linkGridForm">
                            <%=HPCComponents.Global.GetTrangThaiFrom_T_version(row["Ma_Tinbai"].ToString())%></span>
                    </td>
                </tr>
                <%}
                   }%>
            </tbody>
        </table>
    </div>
    <div id="DivPaging" style="float: left; width: 98%; text-align: right;" class="pageNav">
    </div>
    <div style="width: 98%; float: left; text-align: left;">
        <div id="tongsobanghi" style="text-align: right; float: left; width: 40%; font-family: Arial;
            font-size: 16px">
        </div>
        <div id="pagging" style="width: 40%; text-align: left; float: right; cursor: pointer">
            <input runat="server" type="hidden" id="totalItem1" />
            <input runat="server" type="hidden" id="pagesize1" />
            <input runat="server" type="hidden" id="currentpage1" value="1" />
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(document).ready(function() {
        //phan trang
        var _totalRecords = $('#totalItem1').val();
        var _currentpage = $('#currentpage1').val();
        var _pagesize = $('#pagesize1').val();
        var dv_pageNav = ".pageNav";
        var loadData = "loadDataListNews";
        PagingHungViet(_currentpage, _pagesize, _totalRecords, dv_pageNav, loadData);
    });

    
   
</script>

