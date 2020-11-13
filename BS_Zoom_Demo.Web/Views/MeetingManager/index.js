(function ($) {
    $(function () {        
        var _$meetingStateCombobox = $('#MeetingStateCombobox');

        _$meetingStateCombobox.change(function () {
            location.href = '/MeetingManager?state=' + _$meetingStateCombobox.val();
        });

        $('#AddNew').click(function () {
            location.href = '/MeetingManager/Create'
        });
    });
})(jQuery);