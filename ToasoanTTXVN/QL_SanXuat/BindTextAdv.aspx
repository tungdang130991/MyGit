<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindTextAdv.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindTextAdv" %>

<asp:repeater runat="server" id="rptbindData">
        <ItemTemplate>
           <div style="display:none">
             <input id="txtAdv<%#Request["LayoutID"]%>" type="text" value="<%#Eval("Ma_Quangcao")%>" />
           </div>
           <div class="divtextclassTitle"><span>Quảng cáo: </span><%#Eval("Ten_QuangCao")%></div>
        </ItemTemplate>
    </asp:repeater>
