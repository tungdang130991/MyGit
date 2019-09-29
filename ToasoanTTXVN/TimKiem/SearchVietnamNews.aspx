<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="SearchVietnamNews.aspx.cs" Inherits="ToasoanTTXVN.TimKiem.SearchVietnamNews" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Paging/Phantrang.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_tungay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
            $("#<%=txt_denngay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
        });
  
 
    </script>

    <div id="loading" style="display: none; z-index: 1000; width: 100%; height: 100%;
        background: url('../Dungchung/Images/BackgroundBusy.png') repeat scroll left top rgba(0, 0, 0, 0);
        opacity: 1; position: fixed">
        <img style="border: 0px; width: 500px; height: 266px" src="../Dungchung/images/loading-gif-animation.gif" />
    </div>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td style="width: 100%">
                <div class="classSearchHeader">
                    <table style="width: 100%; border: 0; text-align: right">
                        <tr>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblAnpham") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblSobao") %>:
                            </td>
                            <td style="width: 8%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblTrang") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblTukhoa") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblTacgia") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblTungay") %>:
                            </td>
                            <td style="width: 10%; text-align: left" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblDenngay") %>:
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: left">
                                <span id="cboanpham" style="width: 100%;"></span>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <span id="cbosobao" style="width: 100%;"></span>
                            </td>
                            <td style="width: 8%; text-align: left">
                                <span id="cbotrang" style="width: 100%;"></span>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <span id="cbochuyenmuc" style="width: 100%;"></span>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <input type="text" id="txtTieuDe" class="inputtext" style="width: 98%;" onkeypress="return clickButton(event,'btnSearch');" />
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_tacgia" runat="server" Width="98%" CssClass="inputtext"></asp:TextBox>
                                <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_tacgia"
                                    ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                    ContextKey="2" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                    CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                </ajaxtoolkit:AutoCompleteExtender>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                    ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                    ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="8">
                                <input type="button" id="btnSearch" class="iconFind" value="<%=CommonLib.ReadXML("lblTimkiem") %>" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 5px;">
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="text-align: left">
                            <div class="classSearchHeader">
                                <table width="100%" class="Grid">
                                    <tr>
                                        <td class="GridBorderVerSolid" style="padding: 0px !important; border: 0px !important">
                                            <div id="divcontent" style="background-color: White; border-color: #d4d4d4; border-width: 1px;
                                                border-style: None; width: 100%; border-collapse: collapse;" class="Grid">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

    <script type="text/javascript" language="javascript">
        $(window).load(function() { Load_cboanpham(); }
        );
    </script>

    <script type="text/javascript" language="javascript">
        var _option = "";
        var _AnphamID = "";
        var _SobaoID = "";
        var _Trang = "";
        var _ChuyenmucID = "";
        var _Tungay = "";
        var _Denngay = "";
        var _Tieude = "";        
        var _Tacgia = "";
        var _matacgia = "";
        function Load_cboanpham() {

            $.ajax({
                type: "POST",
                url: "../Quytrinh/AutoSave.asmx/BindDatatoDropdownAnpham",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    _option = "<select id='ddlanpham' class='inputtext' onchange='Load_cboTrang();Load_cbosobao();Load_cbochuyenmuc();' style='width:100%;' >";
                    _option += '<option value="0">---Select ALL---</option>';
                    jQuery.each(response.d, function(rec) {
                        _option += '<option value="' + this.Ma_LoaiBao + '">' + this.Ten_LoaiBao + '</option>';
                    });
                    _option += "</select>";
                    $("#cboanpham").html(_option);
                    Load_cboTrang();
                    Load_cbosobao();
                    Load_cbochuyenmuc();
                },
                error: function(response) {

                }
            });
        }
        function Load_cboTrang() {

            _AnphamID = $("#ddlanpham").val();
            $('#loading').css('display', '');
            $.ajax({
                type: "POST",
                url: "../Quytrinh/AutoSave.asmx/BindDatatoDropdownTrang",
                data: "{AnphamID:'" + _AnphamID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    _option = "<select id='ddltrang' class='inputtext' style='width:100%;' >";
                    _option += '<option value="0">---Select ALL---</option>';
                    jQuery.each(response.d, function(rec) {
                        _option += '<option value="' + this.ID + '">' + this.Name + '</option>';
                    });
                    _option += "</select>";
                    $("#cbotrang").html(_option);
                    $('#loading').css('display', 'none');
                },
                error: function(response) {
                    $('#loading').css('display', 'none');
                }
            });
        };
        function Load_cbosobao() {
            $('#loading').css('display', '');
            _AnphamID = $("#ddlanpham").val();
            $.ajax({
                type: "POST",
                url: "../Quytrinh/AutoSave.asmx/BindDatatoDropdownSobao",
                data: "{AnphamID:'" + _AnphamID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    _option = "<select id='ddlsobao' class='inputtext' style='width:100%;' >";
                    _option += '<option value="0">---Select ALL---</option>';

                    jQuery.each(response.d, function(rec) {
                        _option += '<option value="' + this.ID + '">' + this.Name + '</option>';
                    });
                    _option += "</select>";
                    $("#cbosobao").html(_option);
                    $('#loading').css('display', 'none');
                },
                error: function(response) {
                    $('#loading').css('display', 'none');
                }
            });
        };

        function Load_cbochuyenmuc() {
            $('#loading').css('display', '');
            _AnphamID = $("#ddlanpham").val();
            $.ajax({
                type: "POST",
                url: "../Quytrinh/AutoSave.asmx/BindDatatoDropdownChuyenmuc",
                data: "{AnphamID:'" + _AnphamID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    _option = "<select id='ddlCategorys' class='inputtext' style='width:100%;' >";
                    _option += '<option value="0">---Select ALL---</option>';
                    jQuery.each(response.d, function(rec) {
                        _option += '<option value="' + this.Ma_ChuyenMuc + '">' + this.Ten_ChuyenMuc + '</option>';
                    });
                    _option += "</select>";
                    $("#cbochuyenmuc").html(_option);
                    $('#loading').css('display', 'none');
                },
                error: function(response) {
                    $('#loading').css('display', 'none');
                }
            });
        };




        $("#btnSearch").click(function(event) {event.preventDefault();
            loadDataListNews(0,'');
            
        });
        
        function loadDataListNews(pageindex, totalpages) {
          
            $('#loading').css('display', '');
            _AnphamID = $("#ddlanpham").val();
            _SobaoID = $("#ddlsobao").val();
            _Trang = $("#ddltrang").val();
            _ChuyenmucID = $("#ddlCategorys").val();
            _Tungay = $("#ctl00_MainContent_txt_tungay").val();
            _Denngay = $("#ctl00_MainContent_txt_denngay").val();
            _Tieude = encodeURIComponent($("#txtTieuDe").val());            
            _Tacgia = encodeURIComponent($("#ctl00_MainContent_txt_tacgia").val());
            _matacgia =encodeURIComponent( $("#ctl00_MainContent_HiddenFieldTacgiatin").val());
           
            if (_Tungay == '') {
                $("#ctl00_MainContent_txt_tungay").focus();
            }
            if (_Denngay == '') {
                $("#ctl00_MainContent_txt_denngay").focus();
            }
          
            var url = "DisplayContent.aspx?Menu_ID=<%=Request["Menu_ID"]%>&AnphamID=" + _AnphamID + "&Trang=" + _Trang + "&SobaoID=" + _SobaoID + "&ChuyenmucID=" + _ChuyenmucID + "&Tungay=" + _Tungay + "&Denngay=" + _Denngay + "&Tieude=" + _Tieude + "&Tacgia=" + _Tacgia + "&Matacgia=" + _matacgia + "&PageIndex=" + pageindex + "&listcontents";
             $.ajax({
                type: "POST",
                url: url,
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                cache: false,
                success: function(response) {

                    $("#divcontent").html(response);
                    $('#loading').css('display', 'none');
                },
                error: function(response) {
                     $('#loading').css('display', 'none');
                    
                }
            });
        }                
        
    </script>

</asp:Content>
