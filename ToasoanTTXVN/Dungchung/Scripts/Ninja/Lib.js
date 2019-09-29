
function HPC_Convert_Text(src_Element, des_Element, FontBKID) {

    HPC_DetectEnc(); //convert font for firefox
    //convert font for IE
    var content_AfterConvert, content_Before; convertArea;

    document.getElementById(src_Element).value = document.getElementById(des_Element).innerHTML;

    content_Before = document.getElementById(src_Element).value;

    if (!convertArea(document.getElementById(src_Element), "UNICODE")) {

        return;
    }
    // Convert from Unicode Font to BKTPHCM2 Font_ID:12
    content_AfterConvert = convertTo(document.getElementById(src_Element), parseInt(FontBKID));
    document.getElementById(des_Element).innerHTML = document.getElementById(src_Element).value;

}
function HPC_DetectEnc() {

    var id = detectMap(document.getElementById('convert_content'));
    if (id)
        document.all['curmap'].options[id - 1].selected = true;
    return true;
}
function CheckBrowser() {
    var bName = navigator.appName;
    if (bName == "Microsoft Internet Explorer")
        return true;
    else
        return false;
}
function getEditorValue_export(instanceName) {

    var oEditor = document.getElementById(instanceName);
    return oEditor.innerHTML;
}
//end
