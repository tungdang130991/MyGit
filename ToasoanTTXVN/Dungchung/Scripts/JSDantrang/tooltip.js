


var offsetfromcursorX = 12;
var offsetfromcursorY = 10;
var offsetdivfrompointerX = 10;
var offsetdivfrompointerY = 14;

//document.write('<div id="dhtmltooltip"></div>');
document.write('<img src="Images/tooltiparrow.gif"          id="dhtmlpointer"          class="dhtmlpointer"  />');
document.write('<img src="Images/tooltiparrowROnTip.GIF"    id="dhtmlpointerROnTip"    class="dhtmlpointer" />');
document.write('<img src="Images/tooltiparrowRUnderTip.gif" id="dhtmlpointerRUnderTip" class="dhtmlpointer"/>');
document.write('<img src="Images/tooltiparrowUnderTip.gif"  id="dhtmlpointerUnderTip"  class="dhtmlpointer" />');

var ie = document.all;
var ns6 = document.getElementById && ! document.all;
var enabletip = false;

if (ie || ns6)
	var tipobj = document.all ? document.all["dek"] : document.getElementById ? document.getElementById("dek") : "";

var pointerobj = document.all ? document.all["dhtmlpointer"] : document.getElementById ? document.getElementById("dhtmlpointer") : "";
var pointerobjROnTip= document.all ? document.all["dhtmlpointerROnTip"] : document.getElementById ? document.getElementById("dhtmlpointerROnTip") : "";
var pointerobjRUnderTip= document.all ? document.all["dhtmlpointerRUnderTip"] : document.getElementById ? document.getElementById("dhtmlpointerRUnderTip") : "";
var pointerobjUnderTip= document.all ? document.all["dhtmlpointerUnderTip"] : document.getElementById ? document.getElementById("dhtmlpointerUnderTip") : "";

function ietruebody() {
	return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}

function showtip(thetext, thewidth, thecolor) {
	if (ns6 || ie) {
		if (typeof thewidth != "undefined")
			tipobj.style.width = thewidth + "px";
		if (typeof thecolor != "undefined" && thecolor != "")
			tipobj.style.backgroundColor = thecolor;
			//thetext="<table border=1><tr><td>tieude</td></tr><tr><td>"+thetext+"</td></tr></table>"
		tipobj.innerHTML = thetext;		
		enabletip = true;
		return false;
	}
}

function positiontip(e) {
	if (enabletip) {		
		var nondefaultpos = false;//~ nondefaultposOnTip
		var nondefaultposROnTip=true;
		var nondefaultposRUnderTip=true;
		var nondefaultposUnderTip=true;
		var showRight=false;
		var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
		var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
		
		var winwidth = ie && ! window.opera ? ietruebody().clientWidth : window.innerWidth - 20;
		var winheight = ie && ! window.opera ? ietruebody().clientHeight : window.innerHeight - 20;

		var rightedge = ie && ! window.opera ? winwidth - event.clientX - offsetfromcursorX : winwidth - e.clientX - offsetfromcursorX;
		var bottomedge = ie && ! window.opera ? winheight - event.clientY - offsetfromcursorY : winheight - e.clientY - offsetfromcursorY;

		var leftedge = (offsetfromcursorX < 0) ? offsetfromcursorX * (- 1) : - 1000;

		if (rightedge < tipobj.offsetWidth) {
			tipobj.style.left =-5+ curX - tipobj.offsetWidth + "px";
			pointerobjROnTip.style.left=-5+ curX - tipobj.offsetWidth +tipobj.offsetWidth -30+ "px";
			pointerobjRUnderTip.style.left=-5+ curX - tipobj.offsetWidth +tipobj.offsetWidth -30+ "px";
			nondefaultpos = true;
			showRight=true;
			window.satus="true";
		}
		else if (curX < leftedge)
			tipobj.style.left = "5px";
		else {
			tipobj.style.left =15+ curX + offsetfromcursorX - offsetdivfrompointerX + "px";
			pointerobj.style.left =15+ curX + offsetfromcursorX + "px";
			pointerobjUnderTip.style.left =15+ curX + offsetfromcursorX + "px";
		}

		if (bottomedge < tipobj.offsetHeight) {		    
			tipobj.style.top =  5+curY - tipobj.offsetHeight - offsetfromcursorY + "px";
			nondefaultpos = true;
			if (showRight) {
			    //tinh toa do cho rundertip
			    pointerobjRUnderTip.style.top=tipobj.offsetHeight+4+curY - tipobj.offsetHeight - offsetfromcursorY + "px"
			    nondefaultposRUnderTip=false;
			}	
			else {
			//tinh toa do cho undertip
			pointerobjUnderTip.style.top=tipobj.offsetHeight+4+curY - tipobj.offsetHeight - offsetfromcursorY + "px"
			  nondefaultposUnderTip=false;
			}		
		}
		else {
		    if (!showRight){
			    tipobj.style.top = 15+curY + offsetfromcursorY + offsetdivfrompointerY + "px";
			    pointerobj.style.top =15+ curY + offsetfromcursorY + "px";
			}
			else {
			   tipobj.style.top = 15+curY + offsetfromcursorY + offsetdivfrompointerY + "px";			   
			   pointerobjROnTip.style.top=15+ curY + offsetfromcursorY + "px";
			   nondefaultposROnTip=false;			    
			}
		}

		tipobj.style.visibility = "visible";
		
		if (! nondefaultpos)
			pointerobj.style.visibility = "visible";
		else
			pointerobj.style.visibility = "hidden";
			
		if (! nondefaultposUnderTip)
			pointerobjUnderTip.style.visibility = "visible";
		else
			pointerobjUnderTip.style.visibility = "hidden";
		
		if (! nondefaultposRUnderTip)
			pointerobjRUnderTip.style.visibility = "visible";
		else
			pointerobjRUnderTip.style.visibility = "hidden";
		
		if (! nondefaultposROnTip)
			pointerobjROnTip.style.visibility = "visible";
		else
			pointerobjROnTip.style.visibility = "hidden";
		
		
		
			
	}
}

function hidetip() {
	if (ns6 || ie) {
		enabletip = false;
		tipobj.style.visibility = "hidden";
		pointerobj.style.visibility = "hidden";
		pointerobjUnderTip.style.visibility = "hidden";
		pointerobjRUnderTip.style.visibility = "hidden";
		pointerobjROnTip.style.visibility = "hidden";
		
		
		
		tipobj.style.left = "-1000px";
		tipobj.style.backgroundColor = '';
		tipobj.style.width = '';
		//tipobj.innerHTML='';
	}
}

if ( typeof window.addEventListener != "undefined" )
document.addEventListener( "mousemove", positiontip, false );
else if ( typeof window.attachEvent != "undefined" )
document.attachEvent( "onmousemove", positiontip );
else {
if ( document.onmousemove != null ) {
var oldOnmousemove = document.onmousemove;
document.onmousemove = function ( e ) {
oldOnmousemove( e );
positiontip(e);
};
}
else
document.onmousemove = positiontip;
}
 