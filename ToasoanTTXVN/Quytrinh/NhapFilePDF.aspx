<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="NhapFilePDF.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.NhapFilePDF" %>

<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

    <script type="text/javascript" language="javascript">

        function showuploadpdf() {
            var divuploadpdf = $('#divuploadpdf')
            var trang = $('#<%= cboPage.ClientID%>').val();
            if (trang != null && trang > 0)
                divuploadpdf.css("display", "");

            else
                divuploadpdf.css("display", "none");
            LoadFileUpload();
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td style="text-align: center;">
                <div class="divPanelSearch">
                    <div style="width: 99%; padding: 5px 0.5%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblAnpham") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblSobao") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTrang")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTungay")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblDenngay")%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanellang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_Anpham" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_Anpham_SelectedIndexChanged"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelsb" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSoBao" runat="server" AutoPostBack="true" Width="100%" CssClass="inputtext"
                                                onclick="showuploadpdf();" TabIndex="5" OnSelectedIndexChanged="cboSoBao_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 8%; text-align: left">
                                    <asp:UpdatePanel ID="pnlTrang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                                onclick="showuploadpdf();" CssClass="inputtext">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>&nbsp;&nbsp;
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="text-align: left; width: 5%">
                                    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="divPanelResult">
                    <div id="divuploadpdf" style="width: 99%; text-align: left; padding: 5px 0.5%; display: none">
                        <div style="text-align: left; width: auto; float: left">
                            <div style="width: auto; float: left; text-align: left; padding-right: 10px">
                                <asp:FileUpload ID="FileUploadPDF" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div id="divlistpdf" style="width: 99%; text-align: left; padding: 5px 0.5%;">
                        <div style="text-align: left; width: 100%; float: left">
                            <asp:UpdatePanel ID="UpnlListPDF" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="DataGrid_FilePDF" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DataList ID="DataGrid_FilePDF" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                        DataKeyField="ID" Width="100%" CellPadding="4" OnEditCommand="dgrListPDF_EditCommand">
                                        <ItemStyle Width="10%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <div style="width: 90%; height: auto; float: left; background-color: #DCDCDC; position: relative;">
                                                <asp:CheckBox runat="server" Text='' ID="optSelect" AutoPostBack="False"></asp:CheckBox>
                                                <asp:ImageButton ID="Imagebuttondelete" OnClientClick="confirm('Bạn chắc chắn xóa file?');"
                                                    Style="width: 20px; height: 20px; position: absolute; right: 5px; top: 0px; cursor: poiter"
                                                    CausesValidation="false" runat="server" ImageUrl="../Dungchung/Images/icon-delete.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Hủy file pdf" CommandName="Edit" CommandArgument="Delete"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </div>
                                            <div style="width: 90%; float: left; text-align: center">
                                                <a href="<%=Global.TinPath%><%# DataBinder.Eval(Container.DataItem, "Publish_Pdf")%>"
                                                    target="_blank">
                                                    <%#HPCBusinessLogic.UltilFunc.GetPath_PDF(Eval("Page_Number"))%><br />
                                                    <img src="../Dungchung/Images/pdf.png" style="border: 0; width: 80px; height: 80px"
                                                        alt="" />
                                                </a>
                                                <asp:Label ID="lbFileAttach" runat="server" Text='<%#Eval("Publish_Pdf")%>' Visible="false"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div style="width: auto; float: left; text-align: left; padding-right: 10px">
                            <asp:UpdatePanel ID="UpdatePaneldeletePDF" runat="server">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="btnXoaFile" CssClass="iconDel" Text="<%$Resources:cms.language, lblXoa%>"
                                        CausesValidation="false" OnClientClick=" return confirm('Bạn chắc chắn xóa?');"
                                        OnClick="btnXoaFile_Click"></asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        $(window).load(
    function() {

        LoadFileUpload();
    }
);
        function LoadFileUpload() {

            $("#<%=FileUploadPDF.ClientID %>").uploadify({
                'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
                'uploader': '../UploadFileMulti/UploadFilePDF.ashx?user=<%=GetUserName()%> ' + ',' + $('#<%=cboPage.ClientID%>').val() + ',' + $('#<%=cboSoBao.ClientID%>').val() + ',' + 3,
                'auto': true,
                'multi': true,
                'folder': 'Upload',
                'fileDesc': 'Image Files',
                'fileExt': '*.pdf;*.jpeg;',
                'buttonText': '<%=(string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonfilepdf")%>',
                'onUploadSuccess': function() {
                    doPostBackAsync('UploadImageSuccess', '');
                }
            });

        }

        function doPostBackAsync(eventTarget, eventArgs) {
            var pageReqMgr = Sys.WebForms.PageRequestManager.getInstance();
            if (!Array.contains(pageReqMgr._asyncPostBackControlIDs, eventTarget)) {
                pageReqMgr._asyncPostBackControlIDs.push(eventTarget);
            }
            if (!Array.contains(pageReqMgr._asyncPostBackControlClientIDs, eventTarget)) {
                pageReqMgr._asyncPostBackControlClientIDs.push(eventTarget);
            }
            __doPostBack(eventTarget, eventArgs);
        }
        
    </script>

</asp:Content>
