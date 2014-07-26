<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Support.aspx.cs" Inherits="Support" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Media/css/Support.css" media="screen" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<shat:Login ID="LoginControl" runat="server" />
<div id="BringToCenter">
<asp:Panel ID="pnlOnline" runat="server">
    <div id="HeadlineWelcome">Velkommen til Supporten</div>
    <div id="YourSupporterPanel">
        <div class="ProfileBackground">
          <div class="ProfilePictureFrame"><a style="border: 0;" href="Profil.aspx?id=1"><img style="border: 0;" width="122" height="160" src="Media/img/Major/ProfilePictures/danielgogo.jpg" alt="Profilbillede" /></a></div>
          <div class="SupportProfileName">Daniel Williams</div>
        </div>

        <div id="YourSupporter">
            Din Online Supporter:<br />
            <span class="SupporterName">Daniel Williams</span>
        </div>

        <div id="SupporterPanelLeft">
            <div class="SupporterItemsLeft">Chat med Daniel</div>
            <div class="SupporterItemsLeft">Se Daniels Profil</div>
            <div class="SupporterItemsLeft">Tjek dine sager</div>
        </div>
        <div id="SupporterPanelRight">
            <div class="SupporterItemsRight">Send Besked</div>
            <div class="SupporterItemsRight">Rapporter fejl</div>
            <div class="SupporterItemsRight">Ønskeliste</div>
        </div>
    </div>
    
    <div id="OtherSupportersOnline">
        <div class="OtherSupportHeadline">Online Supportere</div>
        <div class="darklist2"></div>
        <div class="lightlist2"></div>
    </div>
</asp:Panel>
<div style="clear:both;"></div>
<asp:Panel ID="pnlWriteSomething" Visible="false" runat="server">
        <asp:TextBox ID="TextBox1" TextMode="Multiline" runat="server"></asp:TextBox>
        <asp:LinkButton ID="lbSubmit" OnClick="Submit_Click" runat="server"></asp:LinkButton>
</asp:Panel>
</div>
</asp:Content>