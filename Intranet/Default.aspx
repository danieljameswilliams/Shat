<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Intranet_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Media/css/Intranet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="Menulistingbox">
<b>Vejledning</b><br />
<a href="#">Skrifttyper</a><br />
<a>HEX Farver</a><br />
<a>Opbygning</a><br />
<br />
<b>Personligt</b><br />
<a>Kontakt</a><br />
<a>Presse</a><br />
<a>Upload</a><br />
<a>Kalender</a><br /><br />
<b>Teknisk</b><br />
<a>FTP</a><br />
<a>MySQL</a><br />
<a>Koder</a><br />
</div>
<div class="Contentbox"></div>

</asp:Content>