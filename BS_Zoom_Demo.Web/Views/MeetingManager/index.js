(function ($) {
    $(function () { 
        var _meetingService = abp.services.app.meeting;

        var _$meetingStateCombobox = $('#MeetingStateCombobox');

        _$meetingStateCombobox.change(function () {
            location.href = '/MeetingManager?state=' + _$meetingStateCombobox.val();
        });

        $('#AddNew').click(function () {
            location.href = '/MeetingManager/Create'
        });

        $('.edit-meeting').click(function (e) {
            var meetingId = $(this).attr("data-meeting-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'MeetingManager/EditMeetingModal?meetingId=' + meetingId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#MeetingEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        $('.delete-meeting').click(function () {
            var meetingId = $(this).attr("data-meeting-id");
            var meetingName = $(this).attr('data-meeting-name');

            deleteMeeting(meetingId, meetingName);
        });

        function deleteMeeting(meetingId, meetingName) {
            abp.message.confirm(
                "Delete meeting '" + meetingName + "'?",
                "Delete Confirm",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _meetingService.delete({
                            id: meetingId
                        }).done(function () {
                            refreshMeetingList();
                        });
                    }
                }
            );
        }

        function refreshMeetingList() {
            location.reload(true); //reload page to see new user!
        }
    });
})(jQuery);