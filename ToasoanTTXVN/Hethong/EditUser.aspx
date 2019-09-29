<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs" Inherits="ToasoanTTXVN.Hethong.EditUser" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #passwordTest
        {
            width: 300px;
            margin-left: auto;
            margin-right: auto;
            background: #F0F0F0;
            padding: 20px;
            border: 1px solid #DDD;
            border-radius: 4px;
        }
        #passwordTest input[type="password"]
        {
            width: 97.5%;
            height: 25px;
            margin-bottom: 5px;
            border: 1px solid #DDD;
            border-radius: 4px;
            line-height: 25px;
            padding-left: 5px;
            font-size: 25px;
            color: #829CBD;
        }
        #pass-info
        {
            width: 100%;
            height: 25px;
            line-height: 20px;
            border: 1px solid #DDD;
            border-radius: 4px;
            color: #DD0000;
            text-align: center;
            font: ffont:12px/25pxArial,Helvetica,sans-serif;
        }
        #pass-info.weakpass
        {
            border: 1px solid #FF9191;
            background: #FFFFFF;
            color: #CC0000;
            text-shadow: 1px 1px 1px #FFF;
        }
        #pass-info.stillweakpass
        {
            border: 1px solid #FBB;
            background: #FFFFFF;
            color: #AA0000;
            text-shadow: 1px 1px 1px #FFF;
        }
        #pass-info.goodpass
        {
            border: 1px solid #C4EEC8;
            background: #E4FFE4;
            color: #51926E;
            text-shadow: 1px 1px 1px #FFF;
        }
        #pass-info.strongpass
        {
            border: 1px solid #6ED66E;
            background: #79F079;
            color: #348F34;
            text-shadow: 1px 1px 1px #FFF;
        }
        #pass-info.vrystrongpass
        {
            border: 1px solid #379137;
            background: #48B448;
            color: #FFF;
            text-shadow: 1px 1px 1px #296429;
        }
    </style>

    <script type="text/javascript" src="../Dungchung/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            var password1 = $('#ctl00_MainContent_password1'); //id of first password field
            var password2 = $('#ctl00_MainContent_password2'); //id of second password field
            var passwordsInfo = $('#pass-info'); //id of indicator element

            passwordStrengthCheck(password1, password2, passwordsInfo); //call password check function

        });

        function passwordStrengthCheck(password1, password2, passwordsInfo) {
            //Must contain 5 characters or more
            var WeakPass = /(?=.{5,}).*/;
            //Must contain lower case letters and at least one digit.
            var MediumPass = /^(?=\S*?[a-z])(?=\S*?[0-9])\S{5,}$/;
            //Must contain at least one upper case letter, one lower case letter and one digit.
            var StrongPass = /^(?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])\S{5,}$/;
            //Must contain at least one upper case letter, one lower case letter and one digit.
            var VryStrongPass = /^(?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[^\w\*])\S{5,}$/;

            $(password1).on('keyup', function(e) {
                if (VryStrongPass.test(password1.val())) {
                    passwordsInfo.removeClass().addClass('vrystrongpass').html("Rất mạnh! (Tuyệt vời, xin đừng quên pass của bạn ngay bây giờ!)");
                }
                else if (StrongPass.test(password1.val())) {
                    passwordsInfo.removeClass().addClass('strongpass').html("Mạnh! (Nhập ký tự đặc biệt để làm cho mạnh mẽ hơn nữa");
                }
                else if (MediumPass.test(password1.val())) {
                    passwordsInfo.removeClass().addClass('goodpass').html("Rất tốt! (Nhập ký tự hoa để làm cho mạnh mẽ)");
                }
                else if (WeakPass.test(password1.val())) {
                    passwordsInfo.removeClass().addClass('stillweakpass').html("Vẫn còn yếu! (Nhập chữ số và ký tự đặc biệt để làm mật khẩu mạnh hơn)");
                }
                else {
                    passwordsInfo.removeClass().addClass('weakpass').html("Mật khẩu chưa đủ mạnh! Mật khẩu mới phải là 6 hoặc nhiều ký tự,trong đó có số, chữ hoa, thường và ký tự đặc biệt như là: (^*!@#$%&~)");
                }
            });

            $(password2).on('keyup', function(e) {

                if (password1.val() !== password2.val()) {
                    passwordsInfo.removeClass().addClass('weakpass').html("Mật khẩu không phù hợp!");
                } else {
                    passwordsInfo.removeClass().addClass('goodpass').html("Mật khẩu phù hợp!");
                }

            });
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;"></span>
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
                        <td align="center">
                            <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td colspan="2" style="text-align: center; width: 100%; font-size: 16px; font-family: arial;
                                        color: red">
                                        <span id="lblkhuyencao" runat="server">-
                                            <%=CommonLib.ReadXML("lblMatkhauthongbao")%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblUserName" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblTendangnhap")%></asp:Label><span
                                            class="req_Field">*</span>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="inputtext" Width="250px"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic" ErrorMessage="Chưa nhập tên truy cập" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlPass" runat="server">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblpassword1" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblMatkhau")%></asp:Label><span
                                                class="req_Field">*</span>:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="password1" runat="server" CssClass="inputtext" Width="250px" TextMode="Password"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="password1"
                                                Display="Dynamic" ErrorMessage="Chưa nhập mật khẩu" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblpassword2" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblXacnhanmatkhau")%></asp:Label><span
                                                class="req_Field">*</span>:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="password2" runat="server" CssClass="inputtext" Width="250px" TextMode="Password"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="password2"
                                                Display="Dynamic" ErrorMessage="Chưa nhập xác nhận mật khẩu" Font-Size="Small"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="pass-info">
                                            </div>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label3" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblTendaydu")%></asp:Label><span
                                            class="req_Field">*</span>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="inputtext" Width="250px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFullName"
                                            Display="Dynamic" ErrorMessage="Chưa nhập họ tên đầy đủ" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblNgaysinh")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBirth" runat="server" Width="150px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txtBirth"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="LabelCMTND" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblSocmnd")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_CMTND" runat="server" CssClass="inputtext" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label ID="Label4" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblDiachi")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="inputtext" Width="250px" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblEmail" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblThudientu")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="inputtext" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblDienthoai")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="inputtext" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblPhongban")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cbo_phongban" runat="server" CssClass="inputtext" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblVungmien")%></asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cbo_vungmien" runat="server" CssClass="inputtext" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblActive" runat="server" CssClass="Titlelbl"><%=CommonLib.ReadXML("lblKichhoat")%></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="ckActivec" CssClass="checkbox" Height="16px" runat="server"></asp:CheckBox>
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
                                    <td colspan="2" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: left" class="Titlelbl_ghichu">
                                        <b>- <u>
                                            <%=CommonLib.ReadXML("lblGhichu")%>:</u></b>
                                        <%=CommonLib.ReadXML("lblGhichuluu")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="clear: both; text-align: center;">
                                            <asp:LinkButton runat="server" ID="btnSave" CssClass="iconSave" OnClick="linkSave_Click"
                                                Text="<%$Resources:cms.language,lblLuu %>">
                                            
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnThoat" CssClass="iconExit" CausesValidation="false"
                                                Text="<%$Resources:cms.language,lblThoat %>" OnClick="LinkCancel_Click">
                                            
                                            </asp:LinkButton>
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
