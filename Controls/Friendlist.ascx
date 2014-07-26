<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Friendlist.ascx.cs" Inherits="Controls_Friendlist" %>

<asp:Repeater ID="Repeatme" runat="server">
    <ItemTemplate>
        <a href="Default.aspx?Profil=<%# DataBinder.Eval(Container.DataItem, "fldtheFriend") %>">
        <%# DataBinder.Eval(Container.DataItem, "fldtheFriend") %><br />
        </a>
    </ItemTemplate>
</asp:Repeater>