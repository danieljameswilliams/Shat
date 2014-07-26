<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeriousBusiness.aspx.cs" Inherits="SaaSkalDerLegesMedjQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <script src="jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="Media/scripts/jquery-ui-1.8.2.custom.min.js"></script>
    
    <style type="text/css">
        .draggable 
        {
        	width:250px;
            height:350px;
            background: url('PrivateMsg.bg');
            float:left;	
            margin: 4px;
            position: fixed;
            left: 50px;
            top: 300px;
            padding: 10px;
        }
        #droppable .draggable
        {
            height: 30px !important;
        }
        #droppable 
        {
            position: fixed;
            left: 0;
            bottom: 0;	
            width:100%;
            height:50px;
            background-color: Red;
            float:left;
            padding:10px;
            z-index: 9998;
        }
        #gridbox 
        {	
        	margin: 50px;
            width: 1000px;
            height:350px;
            background-color: Gray;
            float:left;
            padding:10px;
        }
        .droppablehover
        {
            background:black !important;	
        }
        .everythinghover
        {
            background:#eee !important;	
        }
        .everything
        {
            background:#fff !important;	
        }
        html, body
        {
            margin: 0;
            padding: 0;	
            width:100%;
            height:100%;
        }
        body 
        {
            color:White;
        }
        a:hover
        {
            cursor: pointer;
	        _cursor: hand;	
        }
        #everything a 
        {
            color: Black;	
        }
        #everything .draggable a 
        {
            color: White;	
        }
        .privMsgBox
        {
            float: left;
            width: 240px;
            height: 250px;
        }
        .draggable .top 
        {
            float:left;
            width: 250px;
            height: 20px;
        }
        .draggable .rightButtons 
        {
            float: right;
            width: 40px;
        }
        .draggable .name 
        {
            float: left;
            width: 100px;
        }
        .draggable .txtMsg
        {
            padding: 2px 3px 0px 3px;
            margin: 13px 0 0 0;	
        }
        .hide
        {
            display:none;
        }
    </style>
    
    <style type="text/css">
    * {
    padding: 0;
    margin: 0;
    }
    html, body {
    height: 100%;
    width: 100%;
    }
    #everything {
    min-height: 100%;
    min-width: 100%;
    margin: 0 auto;
    position: absolute;
    left: 0;
    top: 0;
    }
    * html #everything {
    height: 100%;
    width: 100%;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    
<script language="javascript" type="text/javascript">
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

function newMsg(userId) {
    $('#everything').append('<div id="hej' + userId + '" class="draggable"> \
                <div class="top"><div class="name">' + userId + '</div> \
                    <div class="rightButtons"> \
                        <a onclick="minimizeMe(this.parentNode.parentNode.parentNode)">-</a> \
                        <a onclick="gridMe(this.parentNode.parentNode.parentNode)">||</a> \
                        <a onclick="closeMe(this.parentNode.parentNode.parentNode)">X</a> \
                    </div> \
                </div> \
                <div class="content"> \
                    <div class="privMsgBox">chat chat chat</div> \
                    <input onkeyup="keyDownMessage(event);" class="txtMsg" type="text" /> \
                </div> \
            </div>');
        
     updateDraggables();   
}
</script>

<div id="everything">

        <div id="droppable">
                <div class="placeholder">Træk elementer her ned</div>
        </div>
        
        <div id="gridbox">
                <div class="placeholder">Træk elementer her hen</div>
        </div>
<a onclick="newMsg(1)">Msg id 1</a><br />
<a onclick="newMsg(2)">Msg id 2</a><br />
<a onclick="newMsg(3)">Msg id 3</a><br />
<a onclick="newMsg(4)">Msg id 4</a><br />

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

</div>
    
    </form>
</body>
</html>
