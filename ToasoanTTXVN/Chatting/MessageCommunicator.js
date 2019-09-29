var UserLoggedIn = false;
var MessageChangeInterval = 1000;
var MessagePollingInterval = 3000;  // Interval of polling for message
var OLUsersPollingInterval = 9000;  // Interval of polling for online users
var CurrentReciepent = new Array();
var CurrentUser;
var MessagePollingTimer, OLUsersPollingTimer;
var EncryptionKey = 3;  //Encryption Key: 0 to disable encryption
var recepient_user;
var _ResponseUserFullName;
var _check = true;
function ShowPopupMessage() {
    $get("ctl00_UpdateProgress2").style.visibility = "visible";
    $get("ctl00_UpdateProgress2").style.display = "block";

}
function HidePopupMessage() {
    $get("ctl00_UpdateProgress2").style.visibility = "hidden";
    $get("ctl00_UpdateProgress2").style.display = "none";
    ClearMessageHistoryWindowWindow();
}


function ValidateSendMessageWindow(a, b) {

    if (a.value != "" && b.value != "") {
        return (true);
    }
    else {
        ShowErrorBox("Validation Error", "Tin nhắn hoặc Người nhận không được chống");
        return (false);
    }
}


function RemoveArray(arr, item) {
    for (var i = arr.length; i--; ) {
        if (arr[i] == item) {
            arr.splice(i, 1);
        }
    }
}
function ClearSendMessageWindow(a, b, c, d) {
    _check = true;
    document.getElementById(a).value = "";
    document.getElementById(b).value = "";
    RemoveArray(CurrentReciepent, d);
    MessageHistoryContainer = document.getElementById(c);
    while (MessageHistoryContainer.hasChildNodes()) {
        MessageHistoryContainer.removeChild(MessageHistoryContainer.firstChild);
    }
}



function ClearMessageHistoryWindowWindow() {
    MessageHistoryDiv = document.getElementById("MessageHistory");
    MessageHistoryDiv.innerHTML = "";


}
function RecieveMessageHistory(_Recepient_Username) {

    var URL = " ../Chatting/SecureChatServer.ashx";
    if (URL == null) { alert("Request URL is Empty"); }
    else {
        recepient_user = _Recepient_Username;
        AjaxRequest(ProcessGetRecieveMessageHistory, URL, "POST", '', '', { RequestCode: 'SC008', UserID: recepient_user });
    }
}
function ProcessGetRecieveMessageHistory() {
    var ResponseStatus = GetHeader(ResponseHeaderJSON, 'ResponseStatus');
    if (ResponseStatus == "RS-OK" && ResponseText != "") {
        if (_check == true)
            PopulateRecieveMessageHistory(ResponseText);

    }
}
//Bind message history với Icon @
function PopulateRecieveMessageHistory(MessageList) {

    var x;
    var MessageArrayList = new Array();
    MessageArrayList = MessageList.toString().split("|");

//    console.log(MessageArrayList);
    var fullname;
    var MessageHistoryContainerDiv = document.getElementById("RecepientConversation" + recepient_user);
    var MessageHistoryContainer = document.getElementById("ConversationMessages" + recepient_user);
    var MessageLine;
    var MessageText;
    for (x in MessageArrayList) {
        if (MessageArrayList[x] != "") {
            var all = MessageArrayList[x].split(';');

            fullname = $('#OnlineUsersListDisplay div:[id="' + all[0] + '"]').text();
            MessageText = all[1];
            MessageLine = document.createElement("div");
            $(MessageLine).addClass('clear-line');
            if (fullname == "")
                MessageLine.innerHTML = '<div class="avaruser1"><img src="../Chatting/images/man-icon.png"/></div><div class="khung1"><div class="t"> <div class="tr"> <div class="b"><div class="br"><div class="m"> <div class="ml"> <div class="mr"><div class="mc"><span><b>Tôi</b></span>:&nbsp;<span>' + replaceEmoticonsSend(MessageText) + '</span> </div> </div> </div></div></div></div></div></div><div class="point"> </div></div>';
            else
                MessageLine.innerHTML = '<div class="avaruser"><img src="../Chatting/images/man-icon.png"/></div><div class="khung"><div class="t"> <div class="tr"> <div class="b"><div class="br"><div class="m"> <div class="ml"> <div class="mr"><div class="mc"><span><b>' + fullname + '</b></span>:&nbsp;<span>' + replaceEmoticonsSend(MessageText) + '</span> </div> </div> </div></div></div></div></div></div><div class="point"> </div></div>';
            MessageHistoryContainer.appendChild(MessageLine);
        }
    }
    _check = false;
    MessageHistoryContainerDiv.scrollTop = MessageHistoryContainerDiv.scrollHeight;
}

function RecieveMessage() {

    var URL = "../Chatting/SecureChatServer.ashx";
    if (URL == null) { alert("Request URL is Empty"); }
    else {
        var MessageOwner = document.getElementById('MessageHistoryOwner');
        if (MessageOwner != null)
            ShowPopupMessage();
        AjaxRequest(ProcessRecieveMessageResponse, URL, "POST", '', '', { RequestCode: 'SC003' });
        PollForMessages();

    }
}


function ProcessRecieveMessageResponse() {
    var ResponseStatus = GetHeader(ResponseHeaderJSON, 'ResponseStatus');
    var ResponseSender = GetHeader(ResponseHeaderJSON, 'Sender');
    if (ResponseStatus == "RS-OK") {
        //        Message = Decrypt(ResponseText, EncryptionKey);
        Sender = Decrypt(ResponseSender, EncryptionKey);
        Message = ResponseText;
        AppendMessageToMessageWindow(Sender, Message, Sender);

    }
    else if (ResponseStatus == "RS-LoggedOut") {
        //ShowErrorBox("Error", "You are not Logged in, Please Login to Recieve messages");
        LogoutEvents();

    }
    else if (ResponseStatus == "RS-Failed") {

        // Do nothing

    }
    else {

        //ShowErrorBox("Unknown Error Code-05 ", "Request cannot be processed ,please try again.");

    }
}



function GetOnlineUsers() {

    var URL = "../Chatting/SecureChatServer.ashx";
    if (URL == null) { alert("Request URL is Empty"); }
    else {

        AjaxRequest(ProcessGetOnlineUsersResponse, URL, "POST", '', '', { RequestCode: 'SC004' });

        PollForOnlineUsers();
    }

}


function ProcessGetOnlineUsersResponse() {
    var ResponseStatus = GetHeader(ResponseHeaderJSON, 'ResponseStatus');
    if (ResponseStatus == "RS-OK" && ResponseText != "") {

        PopulateOnlineUserList(ResponseText);

    }
}
function PopulateOnlineUserList(UserList) {
    var x;
    var OnlineUsersArrayList = new Array();
    var OnlineUsersDisplay = document.getElementById("OnlineUsersListDisplay");
    OnlineUsersDisplay.innerHTML = "";

    OnlineUsersArrayList = UserList.toString().split("|");
    var usernode;
    var uservalue;
    var useronline;
    var photo;
    for (x in OnlineUsersArrayList) {
        if (OnlineUsersArrayList[x] != "") {
            var all = OnlineUsersArrayList[x].split(';');
            useronline = all[2];
            usernode = document.createElement("div");
            usernode.id = all[0];
            $(usernode).addClass('user-item');
            usernode.setAttribute("onclick", "SendMessageToSelecetdUser('" + all[0] + "','" + all[1] + "');");

            usernode.innerHTML = '<span class="avar"><img src="../Chatting/images/man-icon.png"/></span><span class="name">' + all[1] + '</span>';
            if (useronline == 'True') {
                useronline = document.createElement("span");
                $(useronline).addClass('active');
                $(useronline).innerHTML = "Web";
                usernode.appendChild(useronline);
            }
            OnlineUsersDisplay.appendChild(usernode);
        }
    }

}
function SendMessage(a, b) {
    if (ValidateSendMessageWindow(a, b)) {
        var URL = "../Chatting/SecureChatServer.ashx";
        var covert = "False";
        if (URL == null) { alert("Request URL is Empty"); }
        else {
            HTMLmessage = document.getElementById(b).value.toString().replace(/\r\n?/g, '<br/>');

            //message = Encrypt(HTMLmessage, EncryptionKey);
            message = HTMLmessage;
            message = message.replace(/^\s+|\s+$/g, "");
            message = message.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\"/g, "&quot;");

            recepient = Encrypt(a, EncryptionKey);

            AjaxRequest(ProcessSendMessageResponse, URL, "POST", { Message: message, Recepient: recepient }, '', { RequestCode: 'SC005' });
            CurrentReciepent.push(a);
            AppendMessageToMessageWindow("You", HTMLmessage, a);
            document.getElementById(b).value = "";
        }
    }
}


function ProcessSendMessageResponse() {
    var ResponseStatus = GetHeader(ResponseHeaderJSON, 'ResponseStatus');
    if (ResponseStatus == "RS-OK") {

    }
    else if (ResponseStatus == "RS-LoggedOut") {

        //ShowErrorBox("Error", "You are not Logged in, Please Login to Send messages");
        LogoutEvents();

    }
    else if (ResponseStatus == "RS-Failed") {


        AppendInfoToMessageWindow("Message could not be sent ,please try again.");
    }

    else {

        //ShowErrorBox("Unknown Error Code-04 ", "Request cannot be processed ,please try again.");

    }
}
function showEmoticons(Emoticons) {

    if ($("#" + Emoticons).css('display') == 'none')
        $("#" + Emoticons).css('display', 'block')
    else
        $("#" + Emoticons).css('display', 'none')
}
function selectEmoticons(Emoticons, txtboxmesage, code) {
    $('#' + txtboxmesage).focus();
    $('#' + txtboxmesage).val($('#' + txtboxmesage).val() + code + ' ');

    $("#" + Emoticons).css('display', 'none')
}
function SendMessageToSelecetdUser(idUser, text) {
    var OnlineUsersDisplay = document.getElementById("OnlineUsersListDisplay");
    if (OnlineUsersDisplay != null) {
        recepient_user = idUser.trim();
        var RecepientUsername = idUser.trim();
        var RecepientFullname = text.trim();
        if (RecepientUsername != "") {

            var divChat = document.getElementById('chatting');
            var divId = 'Chat' + RecepientUsername;
            var Recepient = "Recepient" + RecepientUsername;
            var check = document.getElementById(divId);
            if (check == null || check == 'undefined') {
                var divchat1 = document.createElement('div');
                divchat1.id = divId;
                divchat1.className = "CoolTheme ui-draggable";
                divchat1.style.height = "auto";
                divchat1.style.width = "280px";
                divchat1.style.position = "absolute";
                divchat1.style.display = "none";


                divchat1.innerHTML = "<div id='SendMessageWindowTitleBorderLeft" + RecepientUsername + "' class='WindowTitleBorderLeft'>" +
                                                "<div id='SendMessageWindowTitleBorderRight" + RecepientUsername + "' class='WindowTitleBorderRight'>" +
                                                    "<div id='SendMessageWindowTitle" + RecepientUsername + "' class='WindowTitle'>" +
                                                     "<input type='text' id='" + Recepient + "' disabled='disabled' style='width: 80px; background-color: #4682b4;" +
                                                           "border: 0; color: #fff; font-size: 14px; font-family: Arial;font-weight:bold;height:18px; display:none' />" +
                                                     "<input type='text' id='" + Recepient + "values' disabled='disabled' style='width: 180px; background-color: #4682b4;" +
                                                           "border: 0; color: #fff; font-size: 14px; font-family: Arial' value='" + RecepientFullname + "' />" +
                                                   "</div>" +
                                               "</div>" +
                                               "<a href='javascript:void(0)' title='Xem lịch sử tin nhắn' class='TitleMessageHistory' onclick=\"RecieveMessageHistory('" + RecepientUsername + "')\" >@</a>" +
                                               "<a href='javascript:void(0)' class='TitleMimizeButton' onclick=\"MinizeChatBox('" + divId + "')\" >-</a>" +
                                               "<a href='javascript:void(0)' class='TitleCloseButton' onclick=\"ClearSendMessageWindow('Message" + RecepientUsername + "','" + Recepient + "','ConversationMessages" + RecepientUsername + "','" + RecepientUsername + "');CloseWindow('" + divId + "')\" >X</a>" +

                                           "</div>" +

                                         "<div id='SendMessageWindowBorderLeft" + RecepientUsername + "' class='WindowBorderLeft'>" +
                                            "<div id='SendMessageWindowBorderRight" + RecepientUsername + "' class='WindowBorderRight'>" +
                                                "<div id='SendMessageWindowBody" + RecepientUsername + "' class='WindowBody'>" +

                                                    "<div id='RecepientConversation" + RecepientUsername + "' style='height: 260px; width: 100%;overflow:auto'>" +
                                                       "<div id='ConversationMessages" + RecepientUsername + "'>" +
                                                        "</div>" +
                                                   "</div>" +
                                                   "<div id='Emoticons" + RecepientUsername + "' style='float: left; margin: 0; padding: 0; width: auto;display:none'>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer'  onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-)')\"><span>" + replaceEmoticonsSend(':-)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':D')\"><span>" + replaceEmoticonsSend(':D') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-|')\"><span>" + replaceEmoticonsSend(':-|') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':@')\"><span>" + replaceEmoticonsSend(':@') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':(')\"><span>" + replaceEmoticonsSend(':(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':o')\"><span>" + replaceEmoticonsSend(':o') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':,(')\"><span>" + replaceEmoticonsSend(':,(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','+o(')\"><span>" + replaceEmoticonsSend('+o(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',';)')\"><span>" + replaceEmoticonsSend(';)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':p')\"><span>" + replaceEmoticonsSend(':p') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','|-)')\"><span>" + replaceEmoticonsSend('|-)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-#')\"><span>" + replaceEmoticonsSend(':-#') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','(Y)')\"><span>" + replaceEmoticonsSend('(Y)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','(N)')\"><span>" + replaceEmoticonsSend('(N)') + "</span>" +
                                                       " </div>" +
                                                   " </div>" +
                                                    "<div style='float: left; margin: 0; padding: 0; width: auto;background-color:#fff;border:solid 1px #e3e3e3;position:relative;width:277px'>" +
                                                        "<input type='button'  id='SendMessageButton" + RecepientUsername + "' onclick=\"SendMessage('" + RecepientUsername + "','Message" + RecepientUsername + "')\"  title='' style='float: right; display: none; height: 20px; width: 20px' value='Send' />" +
                                                        "<textarea  id='Message" + RecepientUsername + "' style='height: 40px; width: 245px; border: solid 1px #fff;padding: 0;margin: 0 0 0 1px;'   onkeypress=\"return clickButton(event,'SendMessageButton" + RecepientUsername + "')\" ></textarea>" +
                                                        "<img id='ImgEmoticons" + RecepientUsername + "' alt='' src='../Chatting/images/Emoticons/face_smile.png' style='border: 0px;position:absolute;right:5px;top:10px;cursor:pointer' onclick=\"showEmoticons('Emoticons" + RecepientUsername + "')\" />" +
                                                   " </div>" +
                                                "</div>" +
                                           " </div>" +
                                        "</div>";
                divChat.appendChild(divchat1);
            }

            document.getElementById(Recepient).value = RecepientUsername;
            var titleId = '#SendMessageWindowTitle' + RecepientUsername;
            // $('#' + divId).draggable({ opacity: 0.35, cursor: 'move', handle: titleId });
            ShowWindowAt(divId, false);
            $("#Message" + RecepientUsername).focus();
            OnlineUsersDisplay.selectedIndex = -1;

        }

    }

}
// reply message when Message To Receive
function SendMessageToReceive(a) {
    var OnlineUsersDisplay = document.getElementById("OnlineUsersListDisplay");
    if (OnlineUsersDisplay != null) {
        recepient_user = $(a).attr("userrecive");
        RecieveMessageHistory(recepient_user);
        var RecepientUsername = $(a).attr("userrecive"); //.innerHTML.trim();
        var RecepientFullname = a.innerHTML;
        if (RecepientUsername != "") {
            var divChat = document.getElementById('chatting');
            var divId = 'Chat' + RecepientUsername;
            var Recepient = "Recepient" + RecepientUsername;
            var check = document.getElementById(divId);
            if (check == null || check == 'undefined') {
                var divchat1 = document.createElement('div');
                divchat1.id = divId;
                divchat1.className = "CoolTheme ui-draggable";
                divchat1.style.height = "auto";
                divchat1.style.width = "280px";
                divchat1.style.position = "absolute";
                divchat1.style.display = "none";


                divchat1.innerHTML = "<div id='SendMessageWindowTitleBorderLeft" + RecepientUsername + "' class='WindowTitleBorderLeft'>" +
                                                "<div id='SendMessageWindowTitleBorderRight" + RecepientUsername + "' class='WindowTitleBorderRight'>" +
                                                    "<div id='SendMessageWindowTitle" + RecepientUsername + "' class='WindowTitle'>" +
                                                     "<input type='text' id='" + Recepient + "' disabled='disabled' style='width: 80px; background-color: #4682b4;" +
                                                           "border: 0; color: #fff; font-size: 14px; font-family: Arial; display:none' />" +
                                                     "<input type='text' id='" + Recepient + "values' disabled='disabled' style='width: 180px; background-color: #4682b4;" +
                                                           "border: 0; color: #fff; font-size: 14px; font-family: Arial' value='" + RecepientFullname + "' />" +
                                                   "</div>" +
                                               "</div>" +
                                               "<a href='javascript:void(0)' title='Xem lịch sử tin nhắn' class='TitleMessageHistory' onclick=\"RecieveMessageHistory('" + RecepientUsername + "')\" >@</a>" +
                                                "<a href='javascript:void(0)' class='TitleMimizeButton' onclick=\"MinizeChatBox('" + divId + "')\" >-</a>" +
                                               "<a href='javascript:void(0)' class='TitleCloseButton' onclick=\"ClearSendMessageWindow('Message" + RecepientUsername + "','" + Recepient + "','ConversationMessages" + RecepientUsername + "','" + RecepientUsername + "');CloseWindow('" + divId + "')\" >X</a>" +

                                           "</div>" +

                                         "<div id='SendMessageWindowBorderLeft" + RecepientUsername + "' class='WindowBorderLeft'>" +
                                            "<div id='SendMessageWindowBorderRight" + RecepientUsername + "' class='WindowBorderRight'>" +
                                                "<div id='SendMessageWindowBody" + RecepientUsername + "' class='WindowBody'>" +

                                                    "<div id='RecepientConversation" + RecepientUsername + "' style='height: 260px; width: 100%;overflow:auto '>" +

                                                       "<div id='ConversationMessages" + RecepientUsername + "'>" +
                                                        "</div>" +

                                                   "</div>" +
                                                    "<div id='Emoticons" + RecepientUsername + "' style='float: left; margin: 0; padding: 0; width: auto;display:none'>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer'  onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-)')\"><span>" + replaceEmoticonsSend(':-)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':D')\"><span>" + replaceEmoticonsSend(':D') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-|')\"><span>" + replaceEmoticonsSend(':-|') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':@')\"><span>" + replaceEmoticonsSend(':@') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':(')\"><span>" + replaceEmoticonsSend(':(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':o')\"><span>" + replaceEmoticonsSend(':o') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':,(')\"><span>" + replaceEmoticonsSend(':,(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','+o(')\"><span>" + replaceEmoticonsSend('+o(') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',';)')\"><span>" + replaceEmoticonsSend(';)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':p')\"><span>" + replaceEmoticonsSend(':p') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','|-)')\"><span>" + replaceEmoticonsSend('|-)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "',':-#')\"><span>" + replaceEmoticonsSend(':-#') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','(Y)')\"><span>" + replaceEmoticonsSend('(Y)') + "</span>" +
                                                       " </div>" +
                                                       " <div style='float: left; margin: 0; padding: 0 2px; width: auto;cursor:pointer' onclick=\"selectEmoticons('Emoticons" + RecepientUsername + "','Message" + RecepientUsername + "','(N)')\"><span>" + replaceEmoticonsSend('(N)') + "</span>" +
                                                       " </div>" +
                                                   " </div>" +
                                                    "<div style='float: left; margin: 0; padding: 0; width: auto;background-color:#fff;border:solid 1px #e3e3e3;position:relative;width:277px'>" +
                                                        "<input type='button'  id='SendMessageButton" + RecepientUsername + "' onclick=\"SendMessage('" + RecepientUsername + "','Message" + RecepientUsername + "')\"  title='' style='float: right; display: none; height: 20px; width: 20px' value='Send' />" +
                                                        "<textarea  id='Message" + RecepientUsername + "' style='height: 40px; width: 245px; border: solid 1px #fff;padding: 0;margin: 0 0 0 1px;'   onkeypress=\"return clickButton(event,'SendMessageButton" + RecepientUsername + "')\" ></textarea>" +
                                                        "<img id='ImgEmoticons" + RecepientUsername + "' alt='' src='../Chatting/images/Emoticons/face_smile.png' style='border: 0px;position:absolute;right:5px;top:10px;cursor:pointer' onclick=\"showEmoticons('Emoticons" + RecepientUsername + "')\" />" +
                                                   " </div>" +
                                                "</div>" +
                                           " </div>" +
                                        "</div>";
                divChat.appendChild(divchat1);
            }

            document.getElementById(Recepient).value = RecepientUsername;
            var titleId = '#SendMessageWindowTitle' + RecepientUsername;
            // $('#' + divId).draggable({ opacity: 0.35, cursor: 'move', handle: titleId });
            ShowWindowAt(divId, false);

            $("#Message" + RecepientUsername).focus();
            OnlineUsersDisplay.selectedIndex = -1;
        }
    }

}


function CheckCurrentSession() {

    var URL = "../Chatting/SecureChatServer.ashx";

    if (URL == null) { alert("Request URL is Empty"); }
    else {
        AjaxRequest(CheckCurrentSessionResponse, URL, "POST", '', '', { RequestCode: 'SC007' });

    }
}


function CheckCurrentSessionResponse(ResponseText, ResponseStatus, ResponseUserName, ResponseUserFullName) {
    var ResponseStatus = GetHeader(ResponseHeaderJSON, 'ResponseStatus');
    var ResponseUserName = GetHeader(ResponseHeaderJSON, 'RUN');
    var hotendaydu = GetHeader(ResponseHeaderJSON, 'ResponseUserFullName');
    if (ResponseStatus == "RS-OK") {

        LoginEvents(ResponseUserName, true, hotendaydu);

    }
    else if (ResponseStatus == "RS-Failed") {

        // continue with a new session
        ShowWindow('LoginWindow', true);

    }
    else {

        //ShowErrorBox("Unknown Error Code-07 ", "Request cannot be processed ,please try reloading page again.");

    }
}
function LoginEvents(UserName, IsRefresh, FullName) {
    _ResponseUserFullName = FullName.toString();
    UserLoggedIn = true;
    CurrentUser = UserName;
    PollForMessages();
    GetOnlineUsers();

}



function LogoutEvents() {

    UserLoggedIn = false;
    CurrentUser = "";


    try {
        document.getElementById("Sender").value = "";
        clearTimeout(MessagePollingTimer);  //Nozel : Stop timer for checking for messages
        clearTimeout(OLUsersPollingTimer);  //Nozel : Stop timer for checking ol users

        //        HideWindow('MessageHistoryWindow', true, '50px', '10px');
        //        HideElement('ShowMessageHistoryWindow', true);

        //        HideWindow('OnlineUsersWindow', true);
        //        HideElement('ShowOnlineUsersWindow', true);
        //        HideWindow('SendMessageWindow', true);
        //        HideElement('ShowSendMessageWindow', true);

        ClearMessageHistoryWindowWindow();

        ShowAlertMessage("Logout Sucess", "", "Logged out Sucessfully");
    }
    catch (e) {
        AppendInfoToMessageWindow("Not a valid session, Please Login....");
    }

}

function PollForMessages() {
    if (UserLoggedIn) {
        MessagePollingTimer = setTimeout("RecieveMessage()", MessagePollingInterval);
    }

}
function PollForOnlineUsers() {
    if (UserLoggedIn) {
        OLUsersPollingTimer = setTimeout("GetOnlineUsers()", OLUsersPollingInterval);
    }

}

function CheckUserExit(a) {
    var exit = false; var i = 0;
    for (i = 0; i <= CurrentReciepent.length; i++) {
        if (CurrentReciepent[i] == a) {
            exit = true;
            break;
        }
    }
    return exit;
}

function AppendMessageToMessageWindow(MessageOwner, MessageText, Recieve) {
    if (MessageOwner != "" && MessageText != "" && Recieve != "") {
        var fullname = $('#OnlineUsersListDisplay div:[id="' + MessageOwner + '"]').text();
        if (CheckUserExit(MessageOwner) || MessageOwner == "You") {

            MessageHistoryContainerDiv = document.getElementById("RecepientConversation" + Recieve);
            MessageHistoryContainer = document.getElementById("ConversationMessages" + Recieve);
            MessageLine = document.createElement("div");
            $(MessageLine).addClass('clear-line');
            if (MessageOwner == "You") {
                MessageLine.innerHTML = '<div class="avaruser1"><img src="../Chatting/images/man-icon.png"/></div><div class="khung1"><div class="t"> <div class="tr"> <div class="b"><div class="br"><div class="m"> <div class="ml"> <div class="mr"><div class="mc"><span>' + replaceEmoticonsSend(MessageText) + '</span> </div> </div> </div></div></div></div></div></div><div class="point"> </div></div>';
            }
            else
                MessageLine.innerHTML = '<div class="avaruser"><img src="../Chatting/images/man-icon.png"/></div><div class="khung"><div class="t"> <div class="tr"> <div class="b"><div class="br"><div class="m"> <div class="ml"> <div class="mr"><div class="mc"><span><b>' + fullname + '</b></span>:&nbsp;<span>' + replaceEmoticonsSend(MessageText) + '</span> </div> </div> </div></div></div></div></div></div><div class="point"> </div></div>';

            MessageHistoryContainer.appendChild(MessageLine);
            MessageHistoryContainerDiv.scrollTop = MessageHistoryContainerDiv.scrollHeight;
        }
        else {


            MessageHistoryDiv = document.getElementById("MessageHistory");
            MessageLine = document.createElement("div");
            $(MessageLine).addClass('clear-messHistory');
            DisplayMessageHTML = "<div id='MessageHistoryOwner' userrecive ='" + MessageOwner + "' style='width: 100%;text-align:left;font-family:Helvetica,Arial,tahoma,verdana,arial,sans-serif;font-size:14px;font-weight:bold' "
            + "onclick='SendMessageToReceive(this)'>" + fullname + " </div><div style='width: 100%;text-align:left; float:left;font-family:Helvetica,Arial,tahoma,verdana,arial,sans-serif;font-size:13px;color:#333;padding-top:2px'> " + replaceEmoticonsSend(MessageText) + "</div> ";
            MessageLine.innerHTML = DisplayMessageHTML;
            MessageHistoryDiv.appendChild(MessageLine);
        }
    }

}

function AppendInfoToMessageWindow(InfoText) {

    MessageHistoryDiv = document.getElementById("MessageHistory");
    MessageLine = document.createElement("div");
    DisplayMessageHTML = "<center>---" + InfoText + "---</center> ";
    MessageLine.innerHTML = DisplayMessageHTML;
    MessageHistoryDiv.appendChild(MessageLine);
    MessageHistoryDiv.scrollTop = MessageHistoryDiv.scrollHeight;
}

function ToggleCovertMessageBlock() {
    var CovertMessagingState = document.getElementById('CovertMessageExists').checked;
    if (CovertMessagingState) {
        ShowElement('CovertMessaging', false);
    }
    else {
        HideElement('CovertMessaging', false);
    }
}

function AjaxRequest(ReadyHandler, URL, Method, Params, QueryString, HttpHeaders) {
    if (URL == null) { alert("Request URL is Empty"); }
    else {

        if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
            xmlhttp = new XMLHttpRequest();
        }
        else {// code for IE6, IE5
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }

        //An anonymous function is assigned to the event indicator.
        xmlhttp.onreadystatechange = function() {

            //200 status means ok, otherwise some error code is returned, 404 for example
            //The 4 state means for the response is ready and sent by the server.  
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                ResponseText = xmlhttp.responseText;   //get text data in the response
                ResponseXML = xmlhttp.responseXML; //get xml data in the response
                ResponseHeaderJSON = xmlhttp.getResponseHeader("CustomHeaderJSON");  // Extract Data in http header
                alert(ResponseHeaderJSON);
                ResponseHeaders = xmlhttp.getAllResponseHeaders();   //Get a string containing all http headers returned by server

                // Make all the results available in tha ReadyHandler via prototyping.
                ReadyHandler.prototype.ResponseText = ResponseText;
                ReadyHandler.prototype.ResponseHeaderJSON = ResponseHeaderJSON;
                ReadyHandler.prototype.ResponseXML = ResponseXML;
                ReadyHandler.prototype.ResponseHeaders = ResponseHeaders;
                // Execute function passed as ReadyHandelr
                ReadyHandler();
            }

        }

        //If querystring is provided Attach it to the url
        if (QueryString != "") {
            var QueryStringData = "";
            for (QueryStringAttribute in QueryString) {
                QueryStringData = QueryStringAttribute + "=" + QueryString[QueryStringAttribute] + "&" + QueryStringData;
            }
            QueryStringData = QueryStringData.substring(0, QueryStringData.lastIndexOf('&'));
            URL = URL + "?" + escape(QueryStringData);      //Here is where the query string ia attached to the request url.
        }

        //POST or GET URL of the script to execute.true for asynchronous (false for synchronous).
        xmlhttp.open(Method, URL, true);
        xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        //        xmlhttp.setRequestHeader("Content-Type", "text/plain;charset=UTF-8");
        if (HttpHeaders != "") {
            var HttpHeadersData = "";
            for (HttpHeaderName in HttpHeaders) {
                xmlhttp.setRequestHeader(HttpHeaderName, HttpHeaders[HttpHeaderName]);  // Here the custom headers are added
            }

        }

        //Post data provided then assemble it into single string to be posted to server
        if (Params != "") {
            var ParamsData = "";
            for (ParamName in Params) {
                ParamsData = ParamName + "=" + Params[ParamName] + "&" + ParamsData;
            }
            ParamsData = ParamsData.substring(0, ParamsData.lastIndexOf('&'));
        }

        xmlhttp.send(ParamsData); //Send the request with the post data
    }
}

function GetHeader(ResponseHeaderJSON, HeaderName) {

    eval("var CustomHeaders = { " + ResponseHeaderJSON + "};");
    var header;
    var HeaderValue = "";
    if (CustomHeaders != "" && HeaderName != '') {
        for (header in CustomHeaders) {

            if (header.toString() == HeaderName) {

                HeaderValue = CustomHeaders[header];
                alert(HeaderValue);
                break;
            }
        }
    }
    return (HeaderValue);
}

