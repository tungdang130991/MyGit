Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoadedHandler)
function pageLoadedHandler(sender, args)
{ SetCalendarStartTime(); }
function EndRequestHandler(sender, args)
{ SetCalendarStartTime(); }
function SetCalendarStartTime() {
    Protoplasm.use('datepicker')
    .transform('.datepicker', { strings: {
        'Now': 'Hiện tại',
        'Today': 'Hôm nay',
        'Time': 'Thời gian',
        'Exact minutes': 'Exact minutes',
        'Select Date and Time': 'Chọn ngày và giờ',
        'Select Time': 'lựa chọn thời gian',
        'Open calendar': 'Mở lịch'
}
    })
    .transform('.datetimepicker', { timePicker: true, strings: {
        'Now': 'Hiện tại',
        'Today': 'Hôm nay',
        'Time': 'Thời gian',
        'Exact minutes': 'Exact minutes',
        'Select Date and Time': 'Chọn ngày và giờ',
        'Select Time': 'lựa chọn thời gian',
        'Open calendar': 'Mở lịch'
    }
});
    Protoplasm.use('timepicker')
		    .transform('input.timepicker', { use24hrs: true });

}