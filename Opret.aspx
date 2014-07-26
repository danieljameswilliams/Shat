<%@ Page ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Opret.aspx.cs" Inherits="Opret" Title="Opret Bruger på Shat - Community Chat" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="Media/css/CreateUser.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <shat:Login ID="LoginControl" runat="server" /> 

<div class="OpretContent">
                
    <asp:Panel ID="ViewOpret1" DefaultButton="lbNextStep" runat="server">
        <div class="FacebookWrapper">
            <div class="FacebookText">
                <h1>Du kan synkronisere din shat profil med facebook, så dine oplysninger på shat altid er ens med facebook</h1>
                <div class="SyncFacebook"></div>
            </div>
        </div>  
        <h1 class="CreatingHeadline">Opret Profil</h1>

        <br />
        <div class="CreateInfoWrapper">
            <div class="InfoHeadline">Navn & Mail</div>
            
            <div class="InfoLabel"><asp:Label ID="Label1" runat="server" Text="Navn" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Mandatory" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Mandatory" runat="server" ControlToValidate="txtSirName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></div>
            <div class="InfoTextbox">
                <asp:TextBox CssClass="txtNameInfo" MaxLength="30" ID="txtName" runat="server" />
                <asp:TextBox CssClass="txtNameInfo" MaxLength="15" ID="txtSirName" runat="server" />
                
                <div style="clear:both;"></div>
            </div>
            
            <div class="InfoLabel"><asp:Label ID="Label2" runat="server" Text="E-Mail" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Mandatory" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="true" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Mandatory" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator></div>
            <div class="InfoTextbox"><asp:TextBox CssClass="txtInfo" MaxLength="50" ID="txtEmail" runat="server" />
            </div>
        </div> 
        <div class="CreateInfoWrapper">
            <div class="InfoHeadline">Brugernavn & Kode</div>
            
            <div class="InfoLabel"><asp:Label ID="Label3" runat="server" Text="Brugernavn" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Mandatory" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></div>
            <div class="InfoTextbox"><asp:TextBox CssClass="txtInfo" MaxLength="20" ID="txtUsername" runat="server" />
            </div>
            
            <div class="InfoLabel"><asp:Label ID="Label4" runat="server" Text="Adgangskode" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Mandatory" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></div>
            <div class="InfoTextbox"><asp:TextBox TextMode="Password" CssClass="txtInfo" ID="txtPassword" runat="server" />
            </div>
        </div>
        
        <asp:LinkButton ID="LinkButton1" CssClass="GoBack" OnClientClick="history.go(-1);return false;" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lbNextStep" CssClass="NextStep" runat="server" ValidationGroup="Mandatory" onclick="lbNextStep_Click"></asp:LinkButton>
        <div style="clear:both;"></div>
    </asp:Panel>
    
    <asp:Panel ID="ViewOpret2" DefaultButton="lbFinish" runat="server">
    <div class="FacebookWrapper">
                    <div class="FacebookText">
                        <h1>Er du sikker på du ikke vil synkronisere din profil med Facebook?</h1>
                        <div class="SyncFacebook"></div>
                    </div>
                </div>  
                <h1 class="CreatingHeadline">Opret Profil</h1>
                <br />
                <div class="CreateInfoWrapper">
                    <div class="InfoHeadline">Fødselsdato & By</div>
                    
                    <div class="InfoLabel"><asp:Label ID="lblFoedsel" runat="server" Text="Fødselsdato" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDay" ValidationExpression="^[0-9]+$" Display="Dynamic" ValidationGroup="Mandatory2" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtMonth" ValidationExpression="^[0-9]+$" Display="Dynamic" ValidationGroup="Mandatory2" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtYear" ValidationExpression="^([1][9]\d\d|200[0-6])$" ValidationGroup="Mandatory2" Display="Dynamic" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>
                    
                    </div>
                    <div class="InfoTextbox"></div>
                    
                    <asp:TextBox ID="txtDay" CssClass="txtFoedsel" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtMonth" CssClass="txtFoedsel" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtYear" CssClass="txtFoedselyear" runat="server"></asp:TextBox>
                    
                    <div class="InfoLabel"><asp:Label ID="lblBy" runat="server" Text="By (valgfri)" /></div>
                    <div class="InfoTextbox"><asp:TextBox CssClass="txtInfo" ID="txtBy" runat="server" /></div>
                </div> 
                
                <div class="CreateInfoWrapper">
                    <div class="InfoHeadline">Personlig Information</div>
                    
                    <div class="InfoddlWrapperen">
                    <div class="InfoLabel"><asp:Label ID="lbl" runat="server" Text="Køn" /></div>
                    <div class="InfoTextbox">
                        <asp:DropDownList ID="ddlSex" CssClass="ddlCivilstatusClass" runat="server">
                        <asp:ListItem Value="3" Text="-- Vælg et Køn --"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Kvinde"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Mand"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div class="InfoLabel"><asp:Label ID="Label8" runat="server" Text="Civilstatus" /></div>
                    <div class="InfoTextbox">
                        <asp:DropDownList ID="ddlCivilstatus" CssClass="ddlCivilstatusClass" runat="server">
                        <asp:ListItem Value="0" Text="-- Vælg en Status --"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Single"></asp:ListItem>
                        <asp:ListItem Value="2" Text="I et forhold"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Forlovet"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Gift"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Det er kompliceret"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Enke / Enkemand"></asp:ListItem>
                        <asp:ListItem Value="7" Text="I et åbent forhold"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>
                    
                </div>
                
                <asp:LinkButton ID="lbPrev" CssClass="GoBack" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="lbFinish" ValidationGroup="Mandatory2" CssClass="FinishOpret" 
            CommandName="btnFinish" runat="server" onclick="lbFinish_Click"></asp:LinkButton>
                <div style="clear:both;"></div>
    </asp:Panel>       
</div>

</asp:Content>

