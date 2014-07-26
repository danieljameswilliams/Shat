<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GlemtKodeord.aspx.cs" Inherits="GlemtKodeord" Title="Glemt Kodeord til Shat - Community Chat" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="Media/css/CreateUser.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <shat:Login ID="LoginControl" runat="server" /> 

    <div id="GlobalErrorMessage">
        <asp:Literal ID="Status" runat="server"></asp:Literal>
    </div>

<div class="OpretContent">
    <div class="KodeordsInfoWrapper">
            <div class="InfoHeadline">Glemt Kodeord</div>
            
            <div class="InfoLabel">
                <asp:Label ID="Label1" runat="server" Text="Din Email"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Needed" runat="server" ControlToValidate="txtGlemtKodeord" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Needed" ControlToValidate="txtGlemtKodeord" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>
            </div>
            
            <div class="InfoTextbox">
                <asp:TextBox ID="txtGlemtKodeord" CssClass="txtInfo" runat="server"></asp:TextBox>
            </div>
        </div> 
        <div style="clear:both"></div>
        
        <asp:LinkButton ID="lbPrev" CssClass="GoBack" OnClientClick="history.go(-1);return false;" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="SendMeThePassword" CssClass="FinishGlemtKodeord" ValidationGroup="Needed" runat="server" onclick="SendMeThePassword_Click"></asp:LinkButton>
</div>
</asp:Content>

