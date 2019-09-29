<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindText.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindText" %>

<%@ Import Namespace="HPCComponents" %>
<asp:repeater runat="server" id="rptbindData">
        <ItemTemplate>
           <div style="display:none">
             <input id="txtTB<%#Request["LayoutID"]%>" type="text" value="<%#Eval("Ma_Tinbai")%>" />
           </div>
           <div class="divtextclassTitle"><span>Tin bài: </span>
           <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewTinbaiDantrang.aspx?Menu_ID=52&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,800,300,900);">
           <%#Eval("Tieude")%></a></div>
           <div class="divtextclassDes"><span>Số từ: </span> <%#Eval("Sotu")%></div>
        </ItemTemplate>
    </asp:repeater>
