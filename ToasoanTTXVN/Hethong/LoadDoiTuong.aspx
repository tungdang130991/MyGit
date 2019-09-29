<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadDoiTuong.aspx.cs" Inherits="ToasoanTTXVN.Hethong.LoadDoiTuong" %>

<html>
<head>

    <script src="../Dungchung/Scripts/StateMachine/stateMachineDemo.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/StateMachine/stateMachineDemo-jquery.js" type="text/javascript"></script>

    <!-- end demo code -->

    <script src="../Dungchung/Scripts/StateMachine/demo-list.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/StateMachine/demo-helper-jquery.js" type="text/javascript"></script>

</head>
<body>
    <input type="text" id="ctl00_MainContent_lblDTGui" style="display: none" runat="server" />
    <input type="text" id="ctl00_MainContent_lblDTNhan" style="display: none" runat="server" />
    <asp:Repeater ID="rptDoituong" runat="server">
        <ItemTemplate>
            <div class="w" id='<%# Eval("Ma_Doituong")%>' style="left: <%# Eval("CssLeft")%>;
                top: <%# Eval("CssTop")%>">
                <div title="Di chuyển đối tượng" style="display: inline">
                    <%# Eval("Ten_Doituong")%>
                </div>
                <div class="ep" title="Kéo mũi tên đến các đối tượng">
                </div>
                <div class="delItem" title="Xóa đối tượng" onclick="btnDelItem('<%# Eval("ID")%>','<%# Eval("Ten_Doituong")%>','<%# Eval("Ma_AnPham")%>');">
                </div>
            </div>
            </br><br />
            <br />
            <br />
        </ItemTemplate>
    </asp:Repeater>
</body>
<!-- demo code -->
</html>
