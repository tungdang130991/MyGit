<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindReview.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindReview" %>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {
        var count = $('.drsMoveHandle');
        var MaXuatBan = '<%=ConfigurationManager.AppSettings["MaXuatBan"].ToString() %>';
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
            else {
                IdSetTB.removeAttribute('class');
                IdSetTB.className = 'drsMoveHandle StatusBgColorChoXL';
            }
        }
        for (j = 0; j < countStatic.length; j++) {
            var _ids = countStatic[j].id;
            var StatusID = 'txtStatusTB' + _ids.substring(4, _ids.length);
            var IdSet2 = document.getElementById(_ids);

            var _StatusTB = document.getElementById(StatusID).value;
            var txtCV = 'txtCV' + _ids.substring(4, _ids.length);
            var txtCVValue = document.getElementById(txtCV).value;
            var txtAdv = 'txtAdv' + _ids.substring(4, _ids.length);
            var txtAdvValue = document.getElementById(txtAdv).value;

            if (txtAdvValue != '0') {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandleStatic StatusBgColorChoXL';
            }
            if (_StatusTB == '' && txtCVValue != '0') {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandleStatic StatusBgColorChuabai';
            }
            else if (_StatusTB == MaXuatBan && txtCVValue != '0') {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandleStatic StatusBgColorXuatBan';
            }
            else if (_StatusTB != MaXuatBan && txtCVValue != '0') {
                IdSet2.removeAttribute('class');
                IdSet2.className = 'drsMoveHandleStatic StatusBgColorChoXL';
            }
        }
    });
    
    
</script>

<asp:literal runat="server" id="ltrBindData"></asp:literal>
<%=CheckTypeLayout()%>