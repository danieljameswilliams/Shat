<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Filensomloeseralleproblemer.aspx.cs" Inherits="SaaSkalDerLegesMedjQuery" %>

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
            background:blue;
            float:left;	
            margin: 4px;
            position: absolute;
            left: 50px;
            top: 300px;
            padding: 10px;
        }
        #droppable .draggable
        {
            height: 40px !important;
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
            z-index: 9;
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
            padding: 2px 20px 0px 20px;
            margin: 13px 0 0 0;	
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
			zIndex: 10,
			stack: ".draggable"
		});	
}

$(function() {		
    $("#droppable").droppable({
			activeClass: "droppable",
			hoverClass: "droppablehover",
//			accept: ":not(.ui-sortable-helper)",
            tolerance: 'touch',
			drop: function(event, ui) {
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
            tolerance: 'touch',
			drop: function(event, ui) {
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
				    ui.draggable.css('position', 'absolute');
				    ui.draggable.css('left', event.pageX-(ui.draggable.width()/2));
				    ui.draggable.css('top', event.pageY-(ui.draggable.height()/2));
				    ui.draggable.appendTo($('#everything'));
				    }
			}
        });
});
function minimizeMe(dragger) {
				if($(dragger).parent().attr('id') != 'droppable')
				{
				    $('#droppable').find(".placeholder").remove();
				    $(dragger).css('position', 'relative');
				    $(dragger).css('left', '0');
				    $(dragger).css('top', '0');
				    $(dragger).appendTo($('#droppable'));
				    $(dragger).html('<div class="content">Element 4<br /><a onclick="minimizeMe(this.parentNode.parentNode)">Gendan</a></div>');
				} else if($(dragger).parent().attr('id') != 'everything') {
				    $(dragger).css('position', 'absolute');
				    $(dragger).css('left', '50%');
				    $(dragger).css('top', '50%');
				    $(dragger).appendTo($('#everything'));
				    $(dragger).html('<div class="content">Element 4<br /><a onclick="minimizeMe(this.parentNode.parentNode)">Minimér</a></div>');
				}
}
function newMsg(userId) {
    $('#everything').append('<div id="hej' + userId + '" class="draggable"> \
                <div class="top"><div class="name">' + userId + '</div> \
                    <div class="rightButtons"> \
                        <a onclick="minimizeMe(this.parentNode.parentNode)">-</a> \
                        <a onclick="minimizeMe(this.parentNode.parentNode)">||</a> \
                        <a onclick="minimizeMe(this.parentNode.parentNode)">X</a> \
                    </div> \
                </div> \
                <div class="content"> \
                    <div class="privMsgBox"></div> \
                    <div id="txtMsgWrapper"> \
                    <input onkeyup="keyDownMessage(event);" class="txtMsg" type="text" /> \
                    </div> \
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
