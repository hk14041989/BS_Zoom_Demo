﻿(function ($) {
    $(function () {        
        var _$form = $('#MeetingCreationForm');

        _$form.find('input:first').focus();

        _$form.validate();

        _$form.find('button[type=submit]')
            .click(function (e) {
                e.preventDefault();

                if (!_$form.valid()) {
                    return;
                }

                var input = _$form.serializeFormToObject();
                abp.services.app.meeting.createMeeting(input, $('#accessToken').val())
                    .done(function () {
                        location.href = '/MeetingManager/Index';
                    });
            });
    });
})(jQuery);