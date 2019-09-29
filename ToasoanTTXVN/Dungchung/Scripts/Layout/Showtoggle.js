//Hiện ẩn tìm kiếm nâng cao
function Show_Hide_Toggle(divID, spanID, imgID)
{
    var ctr_div = document.getElementById(divID);
    var ctr_span = document.getElementById(spanID);
    var ctr_img = document.getElementById(imgID);

    if (ctr_div.style.display == 'none')
    {
        ctr_div.style.display = 'inline';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Down.png"
        ctr_span.innerHTML = "Đóng lại";
        ctr_div.focus();
    }
    else
    {
        ctr_div.style.display = 'none';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Up.png"
        ctr_span.innerHTML = "Mở ra";

    }
    return false;
}
//Hiện ẩn tìm kiếm nâng cao
function Show_Hide_Toggle2(divID, imgID,IDThis,IDSet) {
    var ctr_div = document.getElementById(divID);
    var ctr_img = document.getElementById(imgID);
    var ctr_this = document.getElementById(IDThis);
    var ctr_set = document.getElementById(IDSet);

    if (ctr_div.style.display == 'none') {
        ctr_div.style.display = 'inline';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Down.png"
        ctr_this.style.left = '173px';
        ctr_set.style.width = '100%';
        ctr_div.focus();
    }
    else {
        ctr_div.style.display = 'none';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Up.png"
        ctr_this.style.left = '0px';
        ctr_set.style.width = '100%';
        
    }
    return false;
}
//Hiện ẩn mới thám số truyền vào
function Show_Hide_Toggle_1(divID, spanID, imgID, IsShow)
{
    var ctr_div = document.getElementById(divID);
    var ctr_span = document.getElementById(spanID);
    var ctr_img = document.getElementById(imgID);

    if (IsShow)
    {
        ctr_div.style.display = 'inline';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Down.png"
        ctr_span.innerHTML = "Đóng lại";
        ctr_div.focus();
    }
    else
    {
        ctr_div.style.display = 'none';
        ctr_img.src = "../Dungchung/Style/Layout/Search_Toggle_Up.png"
        ctr_span.innerHTML = "Mở ra";

    }
    return false;
}






























