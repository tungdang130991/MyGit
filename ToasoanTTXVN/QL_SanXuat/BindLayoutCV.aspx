<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindLayoutCV.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindLayoutCV" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {
        var MaXuatBan = '<%=ConfigurationManager.AppSettings["MaXuatBan"].ToString() %>';
        var count = $('.drsMoveHandle');
        var countStatic = $('.drsMoveHandleStatic');
        for (i = 0; i < count.length; i++) {
            var _id = count[i].id;
            var IDTB = 'txtStatusTB' + _id.substring(4, _id.length);
            var IdSetTB = document.getElementById(_id);
            var _StatusTB1 = document.getElementById(IDTB).value;

            if (_StatusTB1 == MaXuatBan) {
                IdSetTB.removeAttribute('class');
                IdSetTB.className = 'drsMoveHandle StatusBgColorXuatBan';
            }
            if (_StatusTB1 == '') {
                IdSetTB.removeAttribute('class');
                IdSetTB.className = 'drsMoveHandle StatusBgColorChuabai';
            }
            else if (_StatusTB1 != MaXuatBan && _StatusTB1 != '') {
                IdSetTB.removeAttribute('class');
                IdSetTB.className = 'drsMoveHandle StatusBgColorChoXL';
            }
        }
        for (j = 0; j < countStatic.length; j++) {
            var _ids = countStatic[j].id;
            var StatusID = 'txtStatusTB' + _ids.substring(4, _ids.length);
            var IdSet2 = document.getElementById(_ids);
            var _StatusTB = document.getElementById(StatusID).value;
            if (_StatusTB == '') {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandle StatusBgColorChuabai';
            }
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