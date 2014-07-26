<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>
<%@ Register TagPrefix="shat" TagName="UserProfile" Src="~/Controls/UserProfile.ascx" %>
<%@ Register TagPrefix="shat" TagName="Chatten" Src="~/Controls/Chat.ascx" %>
<%@ Register TagPrefix="shat" TagName="Login" Src="~/Controls/Login.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" media="all" href="Media/css/demoStyles.css" />
<link rel="stylesheet" type="text/css" media="all" href="Media/css/jScrollPane.css" />

<%--<script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>--%>

<script type="text/javascript" src="Media/scripts/jquery.mousewheel.js"></script>
<script type="text/javascript" src="Media/scripts/jScrollPane.js"></script>
<script type="text/javascript" src="Media/scripts/jquery.simpletip-1.3.1.pack.js"></script>
<script type="text/javascript" src="Media/scripts/jquery.single_double_click.js"></script>

<script type="text/javascript" src="Media/scripts/jquery-ui-1.8.2.custom.min.js"></script>

<link href="Media/css/ChatLayout.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.UserProfileOpening { width:950px; 
height:500px; display:none; }
</style>

<link href="Media/css/privChat.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<script type="text/javascript">

$(document.body).click(function () {
$("#UserProfileOpening").animate({
"height": "toggle", "opacity": "toggle"
}, 1000);
}); 
</script>--%>

<%--<div id="UserProfileOpening" class="UserProfileOpening">
    <div id="UserProfileGogo">
    <shat:UserProfile ID="UserProfile" runat="server" />
    </div>
</div>--%>
<div id="everything">
        
        <div id="droppable">
            <div id="innerdroppable">
                <div class="placeholder">Træk elementer her ned</div>
            </div>
        </div>
        

        <shat:Login ID="LoginStatus" runat="server" />
        <br /><br />
        <shat:Chatten ID="Chat1" runat="server" />
        
        <div style="display:block;width:1px;height:50px;clear:both;"></div>
</div>
</asp:Content>

