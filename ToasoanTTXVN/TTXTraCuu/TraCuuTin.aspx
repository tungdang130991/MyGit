<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="TraCuuTin.aspx.cs" Inherits="ToasoanTTXVN.TTXTraCuu.TraCuuTin" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/Paging.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/TraCuuTin.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Dungchung/Scripts/jquery-1.4.2.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            Login();
        });
        var _IpClient = '<%= IpClient() %>';
        var _Ticket;
        var fromDate = '<%= txt_FromDate.ClientID %>';
        var toDate = '<%= txtToDate.ClientID %>';
        var _menuid = '<%= Request["Menu_ID"] %>';
        var _userid = '<%=_user.UserID.ToString()%>';
        var _username = '<%=_user.UserName.ToString()%>';
        var _TypeID = 1;
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td colspan="3" style="width: 100%">
                            <div class="classSearchHeader">
                                <div style="width: 99%">
                                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: right">
                                        <tr>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblType") %>:
                                            </td>
                                            <td style="width: 13%">
                                                <span id="spTypes" style="width: 10px;"></span>
                                            </td>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblNguon") %>:
                                            </td>
                                            <td style="width: 13%">
                                                <span id="spProducts" style="width: 10px;"></span>
                                            </td>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblTenngonngu") %>:
                                            </td>
                                            <td style="width: 22%">
                                                <span id="spLanguages" style="width: 10px;"></span>
                                            </td>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                                            </td>
                                            <td style="width: 26%" colspan="3">
                                                <span id="spCategorys" style="width: 10px;"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblTungay") %>:
                                            </td>
                                            <td style="width: 13%">
                                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                                    Width="90%" ScriptSource="../Dungchung/Scripts/datepicker.js" CssClass="inputtext"
                                                    ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                            </td>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblDenngay") %>:
                                            </td>
                                            <td style="width: 13%">
                                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                                    Width="90%" ScriptSource="../Dungchung/Scripts/datepicker.js" CssClass="inputtext"
                                                    ID="txtToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                            </td>
                                            <td style="width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblTieude") %>:
                                            </td>
                                            <td style="width: 30%" colspan="2">
                                                <input type="text" id="txtTieuDe" class="inputtext" style="width: 95%;" onkeypress="return clickButton(event,'btnSearch');" />
                                            </td>
                                            <td style="width: 26%" align="center">
                                                <input type="button" id="btnSearch" class="iconFind" value="<%=CommonLib.ReadXML("lblTimkiem") %>"
                                                    onclick="LoadData(1,'');" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 100%; height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 100%">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="left">
                                        <div class="classSearchHeader">
                                            <table width="100%" class="Grid">
                                                <tr>
                                                    <td class="GridBorderVerSolid" style="padding: 0px !important; border: 0px !important">
                                                        <div id="headerNews">
                                                            <table cellspacing="0" cellpadding="4" rules="all" border="1" id="dgr_tintuc1" style="background-color: White;
                                                                border-color: #d4d4d4; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;"
                                                                class="Grid">
                                                                <tr class="GridHeader" style="color: Black;">
                                                                    <td align="center" style="width: 65%;">
                                                                        <%=CommonLib.ReadXML("lblTieude") %>
                                                                    </td>
                                                                    <td align="center" style="width: 15%;">
                                                                        <%=CommonLib.ReadXML("lblTacgia") %>
                                                                    </td>
                                                                    <td align="center" style="width: 15%;">
                                                                        <%=CommonLib.ReadXML("lblNgaynhap")%>
                                                                    </td>
                                                                    <td align="center" style="width: 5%;">
                                                                        Xem
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="headerPhotos">
                                                            <table cellspacing="0" cellpadding="4" rules="all" border="1" id="Table1" style="background-color: White;
                                                                border-color: #d4d4d4; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;"
                                                                class="Grid">
                                                                <tr class="GridHeader" style="color: Black;">
                                                                    <td align="center" style="width: 2%">
                                                                        <input id="chkAll" type="checkbox" title="Chọn tất cả" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                    </td>
                                                                    <td align="center" style="width: 12%">
                                                                        <%=CommonLib.ReadXML("lblAnh") %>
                                                                    </td>
                                                                    <td align="center" style="width: 18%">
                                                                        <%=CommonLib.ReadXML("lblTieude") %>
                                                                    </td>
                                                                    <td align="center" style="width: 42%">
                                                                        <%=CommonLib.ReadXML("lblMota")%>
                                                                    </td>
                                                                    <td align="center" style="width: 9%">
                                                                        <%=CommonLib.ReadXML("lblChuyenmuc")%>
                                                                    </td>
                                                                    <td align="center" style="width: 7%">
                                                                        <%=CommonLib.ReadXML("lblTacgia")%>
                                                                    </td>
                                                                    <%--<td align="center" style="width: 5%">
                                                                    Nguồn
                                                                </td>--%>
                                                                    <td align="center" style="width: 10%">
                                                                        <%=CommonLib.ReadXML("lblNgaytao")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="GridBorderVerSolid" style="padding: 0px !important; border: 0px !important">
                                                        <table cellspacing="4" cellpadding="4" rules="all" border="1" id="divcontent" style="background-color: White;
                                                            border-color: #d4d4d4; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;"
                                                            class="Grid">
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="DivPaging" class="pageNav">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_bottom_left">
            </td>
            <td class="datagrid_bottom_center">
            </td>
            <td class="datagrid_bottom_right">
            </td>
        </tr>
    </table>
</asp:Content>
