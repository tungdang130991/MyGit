<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindTextCongviec.aspx.cs"
    Inherits="ToasoanTTXVN.QL_SanXuat.BindTextCongviec" %>

<asp:repeater runat="server" id="rptbindData">
        <ItemTemplate>
           <div style="display:none">
            <input id="txtCV<%#Request["LayoutID"]%>" type="text" value="<%#Eval("Ma_Congviec")%>" />
           </div>
            <div class="divtextclassTitle"><span>Công việc: </span><%#Eval("Tencongviec")%></div>
           <!--<div class="divtextclassDes"><span>Người nhận: </span><%#BindUserName(Eval("NguoiNhan").ToString())%></div>-->
        </ItemTemplate>
    </asp:repeater>
