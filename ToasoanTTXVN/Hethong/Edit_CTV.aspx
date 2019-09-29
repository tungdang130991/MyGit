<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_CTV.aspx.cs" Inherits="ToasoanTTXVN.Hethong.Edit_CTV" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txtBirth.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });

        });
        function f_SubmitImage() {
            SubmitImage('../Danhmuc/UploadFile.aspx', 840, 600);
        }
        function getPath(valuePath) {

            document.getElementById("ctl00_MainContent_ImgCTV").src = '<%=Global.ApplicationPath %>' + valuePath;
            document.getElementById("ctl00_MainContent_txt_image").value = valuePath;


        }
        function ClearImage() {
            document.getElementById("ctl00_MainContent_ImgCTV").src = "";
            document.getElementById('ctl00_MainContent_txt_image').value = "";
        }
        
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel"></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table width="80%" cellpadding="0" cellspacing="0" align="center">
                    <tr align="center">
                        <td style="text-align: center; width: 70%; vertical-align: top">
                            <table cellspacing="2" style="text-align: center;"  cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td style="text-align: right;" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblAnhdaidien") %>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_image" runat="server" CssClass="inputtext" Style="float: left"
                                            Width="60%"></asp:TextBox>
                                        <input accesskey="S" onclick="f_SubmitImage()" style="margin-left:5px" type="button" class="myButton blue"
                                             value="Browse" name="cmd_SavePath2" />
                                        <img style="cursor: hand;" onclick="ClearImage();" alt="Xóa ảnh" src="../Dungchung/Images/delete.gif"
                                            width="15px" height="15px" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblButdanh") %>(<span class="req_Field">*</span>):
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic" ErrorMessage="Chưa nhập tên truy cập" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblTendaydu") %>(<span
                                            class="req_Field">*</span>):
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblSocmnd")%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_CMTND" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblNgaysinh")%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBirth" runat="server" CssClass="inputtext" Width="150px" onKeyPress="AscciiDisable()"
                                            onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                            onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl" valign="top"><%=CommonLib.ReadXML("lblDiachi")%>(<span
                                            class="req_Field">*</span>):
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="inputtext" Width="95%" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblThudientu")%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblDienthoai")%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl"><%=CommonLib.ReadXML("lblVungmien")%>(<span
                                            class="req_Field">*</span>):
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cbo_vungmien" runat="server" CssClass="inputtext" Width="50%">
                                            <asp:ListItem Value="0">-----Chọn-----</asp:ListItem>
                                            <asp:ListItem Value="1">Bưu điện</asp:ListItem>
                                            <asp:ListItem Value="2">Hà nội</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="vs_Themmoi" runat="server" ValidationGroup="ValidCapNhap"
                                            ShowSummary="false" />
                                        <asp:Label ID="txtMessage" CssClass="Error" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center" class="Titlelbl_ghichu">
                                        <b>- <u><%=CommonLib.ReadXML("lblGhichu")%>:</u></b> <%=CommonLib.ReadXML("lblGhichuluu")%>.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:LinkButton runat="server" ID="btnSave" Text="<%$Resources:cms.language,lblLuu %>" CssClass="iconSave" OnClick="linkSave_Click">
                                            
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnThoat" Text="<%$Resources:cms.language,lblThoat %>" CssClass="iconExit" CausesValidation="false"
                                            OnClick="LinkCancel_Click">
                                        
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left; width: 30%; vertical-align: top">
                            <asp:Image ID="ImgCTV" Width="120px" runat="server" ImageAlign="Top" Height="150px"
                                BorderStyle="Solid" BorderWidth="1px" BorderColor="#abcdef" ImageUrl="../Dungchung/Images/no_images.jpeg">
                            </asp:Image>
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
</asp:Content>
