function InitializeWindow(windowDivID, titleDivID, cssClass, resizable, invisible) {
    if (windowDivID == null || cssClass == null) {
        alert("Invalid Arguments to function : InitializeWindow ");
    }
    else {

        windowDivSelector = "#" + windowDivID;
        titleDivSelector = "#" + titleDivID;

        // Set Appreance class to Window
        $(windowDivSelector).addClass(cssClass);

        //Make Window Resizable
        if (resizable) {
            //   windowDivResizeSelector = "#" + windowDivID + 'ResizeContent';
            //  windowMinHeight = $(windowDivSelector).height(); //limit minimum height resize
            ////  windowMinWidth = $(windowDivSelector).width();  //limit minimum width resize
            //   $(windowDivResizeSelector).resizable({ minHeight: windowMinHeight,
            //                                    minWidth: windowMinWidth,
            //                                   // animate: true,
            //                                    //autoHide: true,
            //                                    ghost: true,
            //                                    handles: 'n, e, s, w, se '                                         
            //                                 });
        }
        //Make Window Dragable
        // hungviet comment       
        //        if (titleDivID != null) {
        //            $(windowDivSelector).draggable({ opacity: 0.35, cursor: 'move', handle: titleDivSelector });
        //        }
        //        else {
        //            $(windowDivSelector).draggable({ opacity: 0.35, cursor: 'move' });
        //        }

        //        if (invisible) {
        //            $(windowDivSelector).hide();
        //        }

    }

}

//Nozel: Added to make Window visible at topleft position
function MinizeChatBox(id) {
    if ($('#' + id + ' .WindowBorderLeft').css('display') == 'block') {
        $('#' + id + ' .WindowBorderLeft').css('display', 'none');
        $('#' + id).css('height', '28px');
        $('#' + id).css('top', 'auto');
        $('#' + id).css('bottom', '7');
    }
    else {
        $('#' + id + ' .WindowBorderLeft').css('display', 'block');
        $('#' + id).css('height', 'auto');
    }

}
function MinizeChatBoxOnlineUser(id) {
    if ($('#' + id + ' #OnlineUsersWindowBorderLeft').css('display') == 'block') {
        $('#' + id + ' #OnlineUsersWindowBorderLeft').css('display', 'none');
        $('#' + id).css('height', '25px');
        $('#' + id).css('top', 'auto');
        $('#' + id).css('bottom', '30');
    }
    else {
        $('#' + id + ' #OnlineUsersWindowBorderLeft').css('display', 'block');
        $('#' + id).css('height', 'auto');
    }

}
function restructureChatBoxes() {
    var chatBoxes = new Array();
    var dem = 0;
    var windowDivID;
    var listchat = $("#chatting").find('div[id^="Chat"]');
    $.each(listchat, function(index, value) {
        windowDivID = $(value).attr("id");

        if (dem == 0) {
            $("#" + windowDivID).css('right', '220px');
        } else {
            width = (dem) * (280 + 5) + 220;
            $("#" + windowDivID).css('right', width + 'px');
        }
        dem++;

    });

}
function ShowWindowAt(windowDivID, animate) {
    var totalwidth = 0;
    if (windowDivID == null) {
        alert("Invalid Arguments to function : ShowWindow ");
    }
    else {
        $("#" + windowDivID).css('bottom', '7px');
        restructureChatBoxes();


        windowDivSelector = "#" + windowDivID;
        if (animate) {
            $(windowDivSelector).fadeIn('slow', function() { $(windowDivSelector).reposition(0, 20); })

        }
        else {
            $(windowDivSelector).show('fast', function() { $(windowDivSelector).reposition(0, 20); });
        }
    }
}

function ShowWindow(windowDivID, animate) {

    if (windowDivID == null) {
        alert("Invalid Arguments to function : ShowWindow ");
    }
    else {

        windowDivSelector = "#" + windowDivID;
        if (animate) {
            $(windowDivSelector).center();
            $(windowDivSelector).fadeIn('slow')

        }
        else {
            if ($(windowDivSelector).css('display') == 'block') {
                $(windowDivSelector).css('display', 'none');
            }
            else {
                $(windowDivSelector).center();
                $(windowDivSelector).show('fast');
            }
        }
    }
}
function CloseWindow(windowDivID) {


    if (windowDivID == null) {
        alert("Invalid Arguments to function : ShowWindow ");
    }
    else {
        $('#' + windowDivID).remove();
        restructureChatBoxes();

    }
}
//Nozel: Added to make Window disappear
function HideWindow(windowDivID, animate) {


    if (windowDivID == null) {
        alert("Invalid Arguments to function : ShowWindow ");
    }
    else {
        $('#' + windowDivID).css('display', 'none');

    }
}


(function($) {
    $.fn.extend({
        center: function() {
            return this.each(function() {
                var top = ($(window).height() - $(this).outerHeight()) / 2;
                var left = ($(window).width() - $(this).outerWidth()) / 2;
                //$(this).css({ top: 0, left: 0 });
                $(this).css({ position: 'absolute', margin: 0, top: (top > 0 ? top : 0) + 'px', left: (left > 0 ? left : 0) + 'px' });
            });
        }
    });

    $.fn.extend({
        reposition: function(topPosition, leftPosition) {
            return this.each(function() {
                //                $(this).css({ top: 0, left: 0 });
                $(this).css({ position: 'absolute', margin: 0, topPosition: (topPosition > 0 ? topPosition : 0) + 'px', leftPosition: (leftPosition > 0 ? leftPosition : 0) + 'px' });
            });
        }
    });
})(jQuery);