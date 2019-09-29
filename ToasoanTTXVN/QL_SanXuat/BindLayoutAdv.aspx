<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindLayoutAdv.aspx.cs"
    Inherits="ToasoanTTXVN.QL_SanXuat.BindLayoutAdv" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {
        var count = $('.drsMoveHandle');
        var MaXuatBan = '<%=ConfigurationManager.AppSettings["MaXuatBan"].ToString() %>';
        var countStatic = $('.drsMoveHandleStatic');
        for (i = 0; i < count.length; i++) {
            var _id = count[i].id;
            var IDAdv = 'txtAdv' + _id.substring(4, _id.length);
            var IdSet = document.getElementById(_id);
            var _StatusAdv = document.getElementById(IDAdv).value;
            if (_StatusAdv == '') {
                IdSet.removeAttribute('class');
                IdSet.className = 'drsMoveHandle StatusBgColorChuabai';
            }
            else {
                IdSet.removeAttribute('class');
                IdSet.className = 'drsMoveHandle StatusBgColorXuatBan';
            }
        }
        for (j = 0; j < countStatic.length; j++) {
            var _ids = countStatic[j].id;
            var StatusID = 'txtStatusTB' + _ids.substring(4, _ids.length);
            var IdSet2 = document.getElementById(_ids);
            var _StatusTB = document.getElementById(StatusID).value;
            if (_StatusTB == MaXuatBan) {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandle StatusBgColorXuatBan';
            }
            else {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandle StatusBgColorChoXL';
            }
        }
    });
    
    
</script>

<asp:literal runat="server" id="ltrBindData"></asp:literal>
<%=CheckTypeLayout()%>