<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>
<%@ Register TagPrefix="shat" TagName="RetProfil" Src="~/Controls/RetProfil.ascx" %>
<%@ Register TagPrefix="shat" TagName="Friendlist" Src="~/Controls/Friendlist.ascx" %>
<%@ Register TagPrefix="shat" TagName="VisProfil" Src="~/Controls/UserProfile.ascx" %>
<%@ Register TagPrefix="shat" TagName="Chatten" Src="~/Controls/Chat.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls" TagPrefix="mb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link href="Media/css/ChatLayout.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(".FAQSetupIcon").click();
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="preload1"></div><div id="preload2"></div><div id="preload3"></div>

    <shat:Login ID="LoginControl" runat="server" />


<asp:LoginView ID="LoginView1" runat="server">
    <%--AnonymousTemplate--%>
    <AnonymousTemplate>
        <div class="pictureframe">
            <div class="ProfileFrontheadliner">Tilfældige profiler..</div>
            <asp:Literal ID="litPictureFrame" runat="server"></asp:Literal>

            <asp:Repeater ID="repTest" runat="server">
                <ItemTemplate>
                <div class="ProfileBackground">
                <div class="ProfilePictureFrame"><a style="border:0;" href="Profil.aspx?id=<%# DataBinder.Eval(Container.DataItem, "fldID") %>"><img style="border: 0;" width="122" height="160" src="Media/img/Major/ProfilePictures/<%# DataBinder.Eval(Container.DataItem, "fldAvatar") %>" alt="Profilbillede" /></a></div>
                <div class="ProfileSex"><img src="Media/img/Major/User_Gender_<%# DataBinder.Eval(Container.DataItem, "fldSex") %>.jpg" alt="Køn" /></div>
                <div class="ProfileName"><%# DataBinder.Eval(Container.DataItem, "fldUsername") %></div>
                </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div id="InfoPoster">
            <h1 class="forsideheadline1">Har du ikke en profil på Shat?</h1>
            <br />
            <div class="opretprofilhere"><a href="Opret.aspx"></a></div>
            <div class="InfoTexts">
                <div class="InfoTextsItemWrapper">
                    <div class="InfoTextsLeft">
                        <img alt="1" src="Media/img/Minor/Front/InfoTexts_1.png" />
                    </div>
                    <div class="InfoTextsRight">
                        Personaliser dine chat vinduer lige hvordan du vil, ved at hive alle vinduerne rundt, som du arbejder med!
                    </div>
                </div>
                
                <div class="InfoTextsItemWrapper">
                    <div class="InfoTextsLeft">
                        <img alt="2" src="Media/img/Minor/Front/InfoTexts_2.png" />
                    </div>
                    <div class="InfoTextsRight">
                        Synkroniser din profil med Facebook, så hvis du ændrer dit profilbillede på Facebook, ændrer du det også på Shat!
                    </div>
                </div>
                
                <div id="laesmereomshat">
                <a href="#">Læs meget mere om<br />                mulighederne på Shat her..</a>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </AnonymousTemplate>

    <%--LoggedInTemplate--%>
    <LoggedInTemplate>
    <div id="LoggedInWelcomeHeader">
        <div id="LoggedInWelcomeYou">Velkommen Daniel Williams</div>
        <div id="LoggedInWelcomeChat">Chat med</div>
    </div>
    <div id="LoggedInWrapper">
        <div id="LoggedInLeftWrapper">
            <div class="FAQSetupHeadline">
                <div class="FAQSetupText">Du har nogle mangler på din profil</div>
                <div class="FAQSetupIcon"></div>
                <div class="clear"></div>
            </div>
            <div class="FAQContent">
                <div class="FAQContentItem">
                    Profilbillede: <asp:FileUpload ID="FileUp" runat="server" />
                </div>
                
                <div class="FAQContentItem">
                    Civilstatus:
                        <asp:DropDownList ID="ddlCivilstatus" CssClass="DefaultSelects" runat="server">
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
                
                <div class="FAQContentItem">
                    Interesseret i:
                        <asp:DropDownList ID="ddlInteresse" CssClass="DefaultSelects" runat="server">
                            <asp:ListItem Value="0" Text="-- Vælg --"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Mænd"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Kvinder"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mænd & Kvinder"></asp:ListItem>
                        </asp:DropDownList>
                </div>
                
                <div class="FAQContentItem">
                    Søger efter:
                    <asp:CheckBoxList Width="340" style="float: right; margin-right:-5px;" ID="CheckBoxList1" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="1" Text="Venskab"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Dating"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Et Forhold"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Networking"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>

        </div>
        
        <div id="LoggedInRightWrapper">
            <asp:Literal ID="litRooms" runat="server"></asp:Literal>
        </div>
        <div style="clear:both;"></div>
    </div>
    </LoggedInTemplate>
    
</asp:LoginView>
</asp:Content>