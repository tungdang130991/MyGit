<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindLayout.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindLayout" %>

<script type="text/javascript" language="javascript">
    //SetStyleLayoutByStatus
    $(document).ready(function() {
        var count = $('.drsMoveHandle');
        for (i = 0; i < count.length; i++) {
            var _id = count[i].id;
            var StatusID = 'txtStatusTB' + _id.substring(4, _id.length);
            var IdSet = document.getElementById(_id);
            var _StatusTB = document.getElementById(StatusID).value;

            if (_StatusTB != 'DT' && _StatusTB != '') {
                IdSet.removeAttribute('class');
                IdSet.className = 'drsMoveHandle StatusBgColorChoXL';
            }
            if (_StatusTB == 'DT') {
                IdSet.removeAttribute('class');
                IdSet.className = 'drsMoveHandle StatusBgColorXuatBan';
            }
            if (_StatusTB == '') {
                var txtAdv = 'txtAdv' + _id.substring(4, _id.length);
                var txtAdvValue = document.getElementById(txtAdv).value;
                if (txtAdvValue != '') {
                    IdSet.removeAttribute('class');
                    IdSet.className = 'drsMoveHandle StatusBgColorXuatBan';
                }
                else {
                    IdSet.removeAttribute('class');
                    IdSet.className = 'drsMoveHandle StatusBgColorChuabai';
                }
            }
        }
    });
</script>

<asp:repeater runat="server" id="rptbindData">
        <ItemTemplate>
            <div title="<%#Eval("Ma_Vitri")%>" id="<%#Eval("Ma_Vitri")%>" class="drsElement" style='left: <%#Eval("Trai")%>px; top: <%#Eval("Tren")%>px; cursor: move;
                width: <%#Eval("Rong")%>px; height: <%#Eval("Dai")%>px;'>
                <div class="drsMoveHandle" id="Move<%#Eval("Ma_Vitri")%>">
                </div>
                <input type="button" id="btnremoveItem" class="btnremoveChild" value="Xóa" onclick="return removeDivChild('<%#Eval("Ma_Vitri")%>');"/>
                <input type="button" id="btnPhanviec" class="btnPhanviec" value="Phân việc" onclick="PhanViec(this,'<%#Eval("Ma_Vitri")%>');"/>
                <input type="button" id="ChonTinBai" class="btnChontinbai" value="Tin bài" onclick="Chonbaiviet(this,'<%#Eval("Ma_Vitri")%>');"/>
                <input type="button" id="btnAdvertis" class="btnAdvertis" value="Quảng cáo" onclick="ChonAdv(this,'<%#Eval("Ma_Vitri")%>');"/>
                <div class="divtextclass" id="divtext<%#Eval("Ma_Vitri")%>">
                   <div style="display:none">
                       <%#CheckDisplay(Eval("Ma_Congviec"), Eval("Ma_Tinbai"),Eval("Ma_Vitri"))%>
                       <input id="txtStatusTB<%#Eval("Ma_Vitri")%>" type="text" value="<%#Eval("Doituong_DangXuly")%>"/>
                        <!--<input id="txtCV<%#Eval("Ma_Vitri")%>" type="text" value="<%#Eval("Ma_Congviec")%>"/>-->
                   </div>
                   <div class="divtextclassTitle"><%#CheckBindTextTitle(Eval("Ma_Congviec"), Eval("Ma_Tinbai"), Eval("Tieude"), Eval("Noidung_Congviec"))%></div>
                   <div class="divtextclassDes"><%#CheckBindText(Eval("Ma_Congviec"),Eval("Ma_Tinbai"),Eval("TacGia"), BindUserName(Eval("NguoiNhan").ToString()))%></div>
                 </div>
                 <div class="divtextclass" id="divtextAdv<%#Eval("Ma_Vitri")%>">
                     <div style="display:none">
                          <input type="text" value="<%#Eval("Ma_QuangCao")%>" id="txtAdv<%#Eval("Ma_Vitri")%>"/>
                     </div>
                     <div class="divtextclassTitle"><%#CheckBindTextTitleAdv(Eval("Ma_QuangCao"),Eval("Ten_QuangCao"))%></div>
                 </div>
                <!-- <div class="divtextclass" id="divtextTB<%#Eval("Ma_Vitri")%>">
                   <div style="display:none">
                        <input id="txtTB<%#Eval("Ma_Vitri")%>" type="text" value="<%#Eval("Ma_Tinbai")%>"/>
                        <input id="txtStatusTB<%#Eval("Ma_Vitri")%>" type="text" value="<%#Eval("Trangthai")%>"/>
                   </div>
                   <div class="divtextclassTitle"><%#Eval("Tieude")%></div>
                   <div class="divtextclassDes"><%#Eval("TacGia")%></div>
                 </div>-->
            </div>
        </ItemTemplate>
    </asp:repeater>
<%=CheckTypeLayout()%>