<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profil.aspx.cs" Inherits="Profil" %>
<%@ Register TagPrefix="shat" TagName="UserProfile" Src="~/Controls/UserProfile.ascx" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<shat:Login ID="LoginControl" runat="server" />
<br /><br /><br />
<shat:UserProfile ID="UserProfile" runat="server" />
<br /><br /><br /><br /><br /><br />

</asp:Content>