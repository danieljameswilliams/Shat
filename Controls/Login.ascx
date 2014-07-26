<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Controls_WebUserControl" %>

<script type="text/javascript">
    $(document).ready(function()
        {
                $(".LoginBoks").formHighlighter({
            classFocus: 'loginformfocus',
            classBlur: 'loginformblur',
            classKeyDown: 'loginformkeydown'
                });
        });
</script>

<asp:Panel ID="isauth" runat="server">
    <div class="LoggedInBoks">
    <div id="TopToolbarMenu">
        <div id="SupportLogonBox"><asp:LinkButton ID="SupportLogon" CssClass="SupportLogonButton" OnClick="SupportLogon_Click" runat="server">Log på Supporten</asp:LinkButton></div>
        <div class="TopToolbarMenuItem OnlineCount">1.200 Online</div><div class="TopToolbarMenuItem"><a class="LogOutStyle" href="Support.aspx">Support</a></div><div class="TopToolbarMenuItem"><asp:LoginStatus CssClass="LogOutStyle" ID="LoginStatus1" LogoutText="Log ud" runat="server" /></div></div> 
        <div class="LoggedInLeft">
            <div class="AvatarFrame">
                <asp:Image ID="AvatarControlPanel" Width="100" Height="132" runat="server" />
            </div>
            <div class="NavnogUpdate">
                <h1><asp:Literal ID="ProfileRealName" runat="server" /></h1>
                <p>Sidste Login: igår kl 10:00</p>
                <asp:LinkButton ID="lbChooseChatRoom" CssClass="ChooseRoom" runat="server"></asp:LinkButton>
                <asp:Literal ID="litProfileUpdates" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="LoggedInRight">
        <div id="VennelisteName">Venneliste</div>
<%--            <div id="VennelisteBox">
            qweqweqwe
            </div>--%>
            <div id="RightMenuWrapper">
                <asp:Literal ID="litRightMenu" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Panel>

<asp:Panel ID="isnotauth" runat="server">
    <div class="LoginBoks">
        <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate">
            <LayoutTemplate>
                <div id="username">
                    <asp:TextBox ID="UserName" Text="Brugernavn" CssClass="usernametxt" runat="server"></asp:TextBox>
                </div>
                <div id="password">
                    <asp:TextBox ID="Password" CssClass="passwordtxt" TextMode="Password" Value="●●●●●●●" runat="server"></asp:TextBox>
                </div>
                <div id="glemtkode"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal> <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="loginelement">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="loginelement">*</asp:RequiredFieldValidator> <a href="GlemtKodeord.aspx">Glemt Kodeord?</a></div>
                <div id="Signin">
                    <asp:ImageButton ID="LoginButton" ImageUrl="../Media/img/Minor/Front/Login_LoginButton.png" CssClass="Signinbutton" runat="server" AlternateText=" " CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>
</asp:Panel>
