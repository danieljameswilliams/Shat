<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Chat.ascx.cs" Inherits="Controls_Chat" %>
<%@ Register TagPrefix="shat" TagName="UserProfile" Src="~/Controls/UserProfile.ascx" %>

<script language="javascript" type="text/javascript">
//----------------------------
// CHAT
//----------------------------

// TIL AJAXLOAD
//        AjaxPro.onLoading = function(currentVis) {
//        var loadingDiv = document.getElementById("loading");
//        loadingDiv.style.visibility = currentVis ? "visible" : "hidden";
//        }

    $(function () {
        Controls_Chat.AddChatter(queryString.getValue("Room"));
        getChatters();
        getMsgs();
        getPrivateMsgs();

        document.getElementById('txtMsg').focus();
    });
        
        function getChatters()
        {
            Controls_Chat.GetChatters(queryString.getValue("Room"), getChatters_CallBack ); 
        }
        
        var lastUsers;
        function getChatters_CallBack( res )
        {
            var userBox = document.getElementById('divUsers');
            if( res.value == '' ) 
            {
                setTimeout( "getChatters()", 2000 ); 
                return;
            }

            if( lastUsers != res.value )
            {
                userBox.innerHTML = res.value; 
           
               var thisAtag = $(this);
               var brugerid = thisAtag.attr("id");
           
//               $('#divUsers').find('a').each(function(i){
//               brugerid = $(this).attr("id");
//               $(this).simpletip({ 
//                  fixed: true, 
//                  position: 'top',
//                  content: '<a onclick="showProfile(' + brugerid + ')">Vis profil</a><br/>Chat privat<br />'
//                  });
//               });

                $("#divUsers a").single_double_click(function () {
                    showProfile($(this).attr("id"));
                }, function () {
                  chatWith($(this).attr("id"), true);
                })

            }
            
            lastUsers = res.value;
            setTimeout( "getChatters()", 2000 );
        }
        
        function getMsgs()
        {
            Controls_Chat.GetMsgs(queryString.getValue("Room"), getMsgs_CallBack );
        }
        
        function getMsgs_CallBack( res )
        {
            var chatBox = document.getElementById('divChatBox');
            if(res.value != "")
            {
//            var height = chatBox.clientHeight;
//            var scroll = chatBox.scrollHeight;
//            var position = chatBox.scrollTop; 
//                if((height + position) == scroll)
//                        {

//                        chatBox.innerHTML += res.value;
//                        scrollContentDown();
                //                    }

                $pane2 = $('#divChatBox');
                var autoScroll = $pane2.data('jScrollPanePosition') == $pane2.data('jScrollPaneMaxScroll');

                $pane2.append(
								res.value
								)
							.jScrollPane(
								{
								    showArrows: true,
								    scrollbarWidth: 20,
								    scrollbarMargin: 10,
								    wheelSpeed: 100
								}
                							);

                if (autoScroll) {
                    $pane2[0].scrollTo($pane2.data('jScrollPaneMaxScroll'));
                }
           }   

            setTimeout( "getMsgs()", 2000 );
        }
        
        function getPrivateMsgs()
        {
            Controls_Chat.GetPrivateMsgs(getPrivateMsgs_CallBack);
        }
        
        function getPrivateMsgs_CallBack( res )
        {
            //var chatBox = document.getElementById('divChatBox');
            
            
            if(res.value != "")
            {
//                $pane2 = $('#divChatBox');
//                var autoScroll = $pane2.data('jScrollPanePosition') == $pane2.data('jScrollPaneMaxScroll');

//                $pane2.append(
//								res.value
//								)
//							.jScrollPane(
//								{
//								    showArrows: true,
//								    scrollbarWidth: 20,
//								    scrollbarMargin: 10,
//								    wheelSpeed: 100
//								}
//                							);

//                if (autoScroll) {
//                    $pane2[0].scrollTo($pane2.data('jScrollPaneMaxScroll'));
//                } 
                var rows = res.value.Tables[0].Rows;
                for (x in rows)
                {
                    var row = rows[x];
                    if(row["message"] != null)
                    {
                        var chatWindow = document.getElementById('user_' + row["from"]);
                        if(!chatWindow)
                        {
                            chatWith(row["from"]); 
                            
                        }
                        setTimeout( "appendMessage(" + row["from"] + ", " + row["message"] + ")", 500 )
                        
                        //var chatbox = $('.privMsgBox', '#user_' + row["from"]); 
                        //chatbox.append(row["message"]);
                        
                        //alert('#user_' + row["from"] + ' .privMsgBox');
                    }
                }
           }   

            setTimeout( "getPrivateMsgs()", 2000 );
        }
        
        function appendMessage(user, message) {             
                        var chatbox = $('#chatbox_' + user); 
                        chatbox.append(message.toString());
        }

        function sendMsg() 
        {
            var newMsg = '';
            var txtMsg = document.getElementById('txtMsg');
            var msg = txtMsg.value;
            var chatBox = document.getElementById('divChatBox');
            txtMsg.value = '';
            txtMsg.focus();

            if(msg != '') {
                $pane2 = $('#divChatBox');
                var autoScroll = $pane2.data('jScrollPanePosition') == $pane2.data('jScrollPaneMaxScroll');

                // Kør serverside funktion
                newMsg = Controls_Chat.SendMsg(queryString.getValue("Room"), msg).value;

                $pane2.append(
								'<div class="ChatMessageWrapper ColorOwn"><div class=\'ChatNameStyle\'>' + 
                newMsg.TimeStamp + ' | [<b>' + newMsg.Name + '</b>]</div><div class=\'ChatMessageStyle\'>' +
                newMsg.Message + '</div></div>'
								)
							.jScrollPane(
								{
								    showArrows: true,
								    scrollbarWidth: 20,
								    scrollbarMargin: 10,
                                    wheelSpeed: 100,
                                    animateTo: true
								}
                							);
								
                if (autoScroll) {
                    $pane2[0].scrollTo($pane2.data('jScrollPaneMaxScroll'));
                }
            }
        }

        function showProfile(userid) {
            Controls_Chat.GetProfile(userid, showProfile_CallBack);
        }

        function showProfile_CallBack(res) {
            var user = res.value;

            var profilvisning = document.getElementById('ChatProfilVisning');
            profilvisning.innerHTML = "<img style='margin-right: 10px;' alt=' ' src='Media/img/Major/User_GenderBlackLarge_1.jpg' />" + user.name + " " + user.sirname + " / " +  user.username;

            var profilbillede = $('ProfileSitePicture');
            profilbillede.innerHTML = "<img src='Media/img/Major/ProfilePictures/danielgogo.jpg' />";

            var chatWith = document.getElementById('chatWith');
            chatWith.innerHTML = "<a href='#' onclick='chatWith(" + user.id + ", true)'>" + user.username + "</a>";

            $("#UserProfileOpening").animate({
                "height": "show", "opacity": "show"
            }, 1000);
        }
        
        function chatWith(userid, selfStarted) {
            Controls_Chat.GetProfile(userid, newMsg(selfStarted));
        }
        
        function keyDownMessage( e )
        {
            e = e || window.event;
            var charCode = e.keyCode || e.which;

            if( charCode == 13 )
            {
                e.returnvalue = false;
                e.cancel = true;
                sendMsg();
            }
        }
        
        function keyPressed(e) {
	        var keycode;
	        if (window.event) {
		        keycode = window.event.keyCode;
	        }
	        else if (e) {
		        keycode = e.which;
	        }
	        else {
		        return (false);
	        }
	        return (keycode != 13);
        }
        
        var nav = window.event ? true : false;
        if (nav) {
	        window.captureEvents(Event.KEYDOWN);
	        window.onkeydown = NetscapeEventHandler_KeyDown;
        } else {
	        document.onkeydown = IEEventHandler_KeyDown;
        }

        function NetscapeEventHandler_KeyDown(e) {
	        if (e.which == 13 && e.target.type != 'textarea' && e.target.type != 'submit') { return false; }
	        return true;
        }

        function IEEventHandler_KeyDown(event) {
	        if (event.keyCode == 13 && event.srcElement.type != 'textarea' && event.srcElement.type != 'submit')
		        return false;
	        return true;
        }
        
        function scrollContentDown() 
        { 
            var theDiv = document.getElementById('divChatBox');
            theDiv.scrollTop = theDiv.scrollHeight - theDiv.clientHeight;  
        }
        
        window.onbeforeunload = function(){
        Controls_Chat.RemoveChatter();
        }

//----------------------------
// Private Chat
//----------------------------
//////////////////////////////////////////////
function chatWith(userid) {
    Controls_Chat.GetProfile(userid, newMsg);
}

function updateDraggables() {
    $("div.draggable").draggable({
			appendTo: '#droppable',
			scroll: true,
			zIndex: 9999,
			stack: ".draggable",
			start: function(event, ui) { 
//			    if($(this).parent().attr('id') == 'gridbox')
//			        $(this).css('position', 'fixed');
			    
			        $(this).data("position", { left: $(this).css('left'), top: $(this).css('top') });
			        //console.debug("Left: %s", $(this).data("position").left);
			 }
		});	
}

$(function() {		
    $("#droppable").droppable({
			activeClass: "droppable",
			hoverClass: "droppablehover",
//			accept: ":not(.ui-sortable-helper)",
            tolerance: 'touch',
			drop: function(event, ui) {
			if(ui.draggable.parent().attr('id') != 'droppable')
                $('.content',ui.draggable).toggleClass('hide');
//			    minimizeMe(ui.draggable);
			    
				$(this).find(".placeholder").remove();
				ui.draggable.css('position', 'relative');
				ui.draggable.css('left', '0');
				ui.draggable.css('top', '0');
				if(ui.draggable.parent().attr('id') != 'droppable')
				    ui.draggable.appendTo(this);
			}
        });
        
        $("#gridbox").droppable({
			activeClass: "droppable",
			hoverClass: "droppablehover",
//			accept: ":not(.ui-sortable-helper)",
            tolerance: 'intersect',
			drop: function(event, ui) {	
			if(ui.draggable.parent().attr('id') == 'droppable')
			    minimizeMe(ui.draggable);
			    	    
				$(this).find(".placeholder").remove();
				ui.draggable.css('position', 'relative');
				ui.draggable.css('left', '0');
				ui.draggable.css('top', '0');
				if(ui.draggable.parent().attr('id') != 'gridbox')
				    ui.draggable.appendTo(this);	    
			}
        });
        
        $("#everything").droppable({
//			activeClass: "everything",
//			hoverClass: "everythinghover",
//			accept: ":not(.ui-sortable-helper)",
            tolerance: 'fit',
			drop: function(event, ui) {
				if(ui.draggable.parent().attr('id') != 'everything')
				{
				if(ui.draggable.parent().attr('id') != 'gridbox')
				minimizeMe(ui.draggable);
				    ui.draggable.css('position', 'fixed');
				    ui.draggable.css('left', event.pageX-(ui.draggable.width()/2));
				    ui.draggable.css('top', event.pageY-(ui.draggable.height()/2)-$(window).scrollTop());
				    ui.draggable.appendTo($('#everything')); 
				}
			}
        });
});
function minimizeMe(dragger) {
                $('.content',dragger).toggleClass('hide');
                
				if($(dragger).parent().attr('id') != 'droppable')
				{
				    $('#droppable').find(".placeholder").remove();
				    $(dragger).css('position', 'relative');
				    
				    //Gem boksens position
				    $(dragger).data("position", { left: $(dragger).css('left'), top: $(dragger).css('top') });
				    
				    $(dragger).css('left', '0');
				    $(dragger).css('top', '0');
				    $(dragger).appendTo($('#droppable'));
				} else if($(dragger).parent().attr('id') != 'everything') {
				    //console.debug("Left: %s", $(dragger).data("position").left);
				    $(dragger).css('position', 'fixed');
                    $(dragger).css('left', $(dragger).data("position").left);
				    $(dragger).css('top', $(dragger).data("position").top);
				    
				    $(dragger).appendTo($('#everything'));
				}
}
function gridMe(dragger) {      
                if($(dragger).parent().attr('id') == 'droppable')
                    $('.content',$(dragger)).toggleClass('hide');
        
				if($(dragger).parent().attr('id') != 'gridbox')
				{
				    $('#gridbox').find(".placeholder").remove();
				    $(dragger).css('position', 'relative');
				    
				    //Gem boksens position
				    $(dragger).data("position", { left: $(dragger).css('left'), top: $(dragger).css('top') });
				    
				    $(dragger).css('left', '0');
				    $(dragger).css('top', '0');
				    $(dragger).appendTo($('#gridbox'));
				} else if($(dragger).parent().attr('id') != 'everything') {
				    //console.debug("Left: %s", $(dragger).data("position").left);
				    $(dragger).css('position', 'fixed');
                    $(dragger).css('left', $(dragger).data("position").left);
				    $(dragger).css('top', $(dragger).data("position").top);
				    
				    $(dragger).appendTo($('#everything'));
				}
}
function closeMe(dragger) {
$(dragger).draggable( "destroy" );
$(dragger).remove();
}

function newMsg(res, selfStarted) {
var user = res.value;

var chatWindow = document.getElementById('user_' + user.id);
if(!chatWindow)
{
    var thisUsersID = Controls_Chat.GetThisUsersID();
    if(user.id != thisUsersID.value)
    {
        $('#everything').append('<div id="user_' + user.id + '" class="draggable"> \
                <div class="top"><div class="name">' + user.username + '</div> \
                    <div class="rightButtons"> \
                        <a onclick="gridMe(this.parentNode.parentNode.parentNode)"><img alt="-" src="Media/img/Minor/PrivateMsg/grid.jpg" /> </a> \
                        <a onclick="minimizeMe(this.parentNode.parentNode.parentNode)"><img alt="-" src="Media/img/Minor/PrivateMsg/minimize.jpg" /> </a> \
                        <a onclick="closeMe(this.parentNode.parentNode.parentNode)"><img alt="-" src="Media/img/Minor/PrivateMsg/close.jpg" /> </a> \
                    </div> \
                </div> \
                <div class="content"> \
                    <div class="privMsgBox" id="chatbox_' + user.id + '"></div> \
                    <div id="txtMsgWrapper"> \
                        <textarea onkeyup="privateKeyDownMessage(event, ' + user.id + ');" class="txtMsg" id="txt_' + user.id + '" type="text" /> \
                    </div> \
                </div> \
            </div>');
            if(selfStarted.value) { alert(selfStarted.value); document.getElementById('txt_' + user.id).focus(); }
    }
}
        
     updateDraggables();   
}

function privateKeyDownMessage( e, userId )
        {
            e = e || window.event;
            var charCode = e.keyCode || e.which;

            if( charCode == 13 )
            {
                var field = document.getElementById('txt_' + userId);
                field.value = field.value.replace(/\n/g, "");
                e.returnValue = false;
                if (e.preventDefault) {
                    e.preventDefault();
                }
                
                e.cancel = true;
                sendPrivateMsg(userId);
            }
        }

function sendPrivateMsg(userId) 
        {
            var newMsg = '';
            var txtMsg = document.getElementById('txt_' + userId);
            var msg = txtMsg.value;
            var chatBox = document.getElementById('divChatBox');
            txtMsg.value = '';
            txtMsg.focus();

            if(msg != '') {
                $pane2 = $('#chatbox_' + userId);
                //var autoScroll = $pane2.data('jScrollPanePosition') == $pane2.data('jScrollPaneMaxScroll');

                // Kør serverside funktion
                newMsg = Controls_Chat.SendPrivateMsg(userId, msg).value;
                
                $pane2.append('<div class="ChatMessageWrapper ColorOwn"><div class=\'ChatNameMessage\'>' + 
                newMsg.TimeStamp + ' | <b>' + newMsg.Name + ': </b>' +
                newMsg.Message + '</div></div>');
                
//                $pane2.append(
//								'<div class="ChatMessageWrapper ColorOwn"><div class=\'ChatNameStyle\'>' + 
//                newMsg.TimeStamp + ' | [<b>' + newMsg.Name + '</b>]</div><div class=\'ChatMessageStyle\'>' +
//                newMsg.Message + '</div></div>'
//								)
//							.jScrollPane(
//								{
//								    showArrows: true,
//								    scrollbarWidth: 20,
//								    scrollbarMargin: 10,
//                                    wheelSpeed: 100,
//                                    animateTo: true
//								}
//                							);
//								
//                if (autoScroll) {
//                    $pane2[0].scrollTo($pane2.data('jScrollPaneMaxScroll'));
//                }
            }
        }
//////////////////////////////////////////////




    </script>
    
    <div id="loading" style="visibility:hidden;position:absolute;top:50%;left:50%;margin:-24px -24px 0 0;">
    <img src="Media/img/Major/AjaxLoader.gif" alt="Loading..." />
    </div>

    <div id="UserProfileOpening" class="UserProfileOpening">
        <div id="UserProfileGogo">
            <div id='Profileheader'><div id='ChatProfilVisning'></div></div>
            <div class='leftwrapper'><div class='picturewrapper'><div class='ProfileSitePicture'></div></div>
            <div class='infowrapper'>
            <div class='ProfileSiteHeadline' style='margin-top:25px'>Alder</div><div class='ProfileSiteName'>18</div>

            <br /><div class='ProfileSiteHeadline'>Civilstatus</div><div class='ProfileSiteName'>Single</div></div>
            <div id='mulighedsbox2'><div id='dinemuligheder'>Hvad vil du?</div>
            <div id='mulighedswrapper'>
            <div class='mulighedsitem'>Tilføj som ven</div><div class='mulighedsitem'>Anbefal Venner</div><div class='mulighedsitem'>Vis Billeder (0000)</div>
            <div class='mulighedsitem'>Blokér</div><div class='mulighedsitem'>Validér Profil</div><div class='mulighedsitem'>Fælles Venner (000)</div>
            <div class='mulighedsitem'>Send Besked</div><div class='mulighedsitem'>Anmeld</div><div class='mulighedsitem'>Venner (0000)</div>
            <div class='mulighedsitem'><span id="chatWith"></span></div><div class='mulighedsitem'>Del</div>
            </div></div></div>

            <div class='rightwrapper'>
            <div class='Oplysningsfelt'>Oplysninger</div>
            <div class='darklist'><div class='rightfieldname'>Fødselsdato:</div><div class='rightfieldanswer'>23. Maj 1992</div></div>
            <div class='lightlist'><div class='rightfieldname'>Interesseret i:</div><div class='rightfieldanswerspecial'><img src='Media/img/Major/User_GenderLarge_0.png' /></div></div>
            <div class='darklist'><div class='rightfieldname'>Søger:</div><div class='rightfieldanswer'>Dating / Venskab</div></div><br />
            
            <div class='lightlist'><div class='rightfieldname'>E-Mail:</div><div class='rightfieldanswer'>mail@danielwilliams.dk</div></div>
            <div class='darklist'><div class='rightfieldname'>Messenger:</div><div class='rightfieldanswer'>msn@rofistick.com</div></div>
            <div class='lightlist'><div class='rightfieldname'>Website</div><div class='rightfieldanswer'>www.danielwilliams.dk</div></div>
            <div class='darklist'><div class='rightfieldname whitecolor'>Link til Facebook</div></div><br />
            
            <div class='lightlist'><div class='rightfieldname'>Mobilnummer:</div><div class='rightfieldanswer'>+45 28 97 95 05</div></div><br />
            </div>
        </div>
    </div>

    <div id="ChatBackground">
        <div id="left">
            <input type="text" style="display:none" />
            <div id="divChatBox" class="scroll-pane"></div>
        </div>
    
        <div id="ChattersInRightColumn">
        <div id="OnlineiRummet">Brugere Online i Rummet</div>
        <div id="divUsers">
        </div>
        <div id="lastUsers"></div>
        </div>
    
        <div id="Tools">
            <input onkeyup="keyDownMessage(event);" id="txtMsg" type="text" />
            <div class="send"><a onclick="sendMsg()"></a></div>
        </div>
    </div>