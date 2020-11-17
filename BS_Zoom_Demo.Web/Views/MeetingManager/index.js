(function ($) {
    $(function () { 
        var _$form = $('#MeetingCreateModal');

        _$form.find('input:first').focus();

        _$form.validate();

        $('#zmmtg-root').css('display', 'none');

        document.getElementById("display_name").value =
            "CDN" +
            ZoomMtg.getJSSDKVersion()[0] +
            testTool.detectOS() +
            "#" +
            testTool.getBrowserInfo();

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

        _$form.find('button[type="submit"]').click(function (e) {
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

        $('.join-host').click(function (e) {
            var meetingId = $(this).attr("data-meeting-id");
            var meetingType = $(this).attr("data-meeting-type");
            var meetingPass = $(this).attr("data-meeting-pass");

            joinMeeting(meetingId, meetingPass, meetingType, e);
        });

        $('.join-attendee').click(function (e) {
            var meetingId = $(this).attr("data-meeting-id");
            var meetingType = $(this).attr("data-meeting-type");
            var meetingPass = $(this).attr("data-meeting-pass");

            joinMeeting(meetingId, meetingPass, meetingType, e);
        });

        function joinMeeting(meetingId, meetingPass, meetingType, e) {
            var testTool = window.testTool;
            if (testTool.isMobileDevice()) {
                vConsole = new VConsole();
            }
            console.log("checkSystemRequirements");
            console.log(JSON.stringify(ZoomMtg.checkSystemRequirements()));

            // it's option if you want to change the WebSDK dependency link resources. setZoomJSLib must be run at first
            // if (!china) ZoomMtg.setZoomJSLib('https://source.zoom.us/1.8.1/lib', '/av'); // CDN version default
            // else ZoomMtg.setZoomJSLib('https://jssdk.zoomus.cn/1.8.1/lib', '/av'); // china cdn option
            // ZoomMtg.setZoomJSLib('http://localhost:9999/node_modules/@zoomus/websdk/dist/lib', '/av'); // Local version default, Angular Project change to use cdn version
            ZoomMtg.preLoadWasm(); // pre download wasm file to save time.

            var API_KEY = "vS_E4sK3S7uFEJk6I76Krw";

            /**
             * NEVER PUT YOUR ACTUAL API SECRET IN CLIENT SIDE CODE, THIS IS JUST FOR QUICK PROTOTYPING
             * The below generateSignature should be done server side as not to expose your api secret in public
             * You can find an eaxmple in here: https://marketplace.zoom.us/docs/sdk/native-sdks/web/essential/signature
             */
            var API_SECRET = "1d5M36uFNCg2zwdhkiJ2bOP1D1Ej67dU8ZKP";

            var tmpMn = meetingId.replace(/([^0-9])+/i, "");
            if (tmpMn.match(/([0-9]{9,11})/)) {
                tmpMn = tmpMn.match(/([0-9]{9,11})/)[1];
            }
            var tmpPwd = meetingPass.match(/pwd=([\d,\w]+)/);
            if (tmpPwd) {
                testTool.setCookie("meeting_pwd", tmpPwd[1]);
            }
            testTool.setCookie(
                "meeting_number",
                meetingId
            );

            e.preventDefault();
            var meetingConfig = testTool.getMeetingConfig();
            meetingConfig.mn = meetingId;
            meetingConfig.pwd = meetingPass;
            meetingConfig.role = meetingType;

            testTool.setCookie("meeting_number", meetingConfig.mn);
            testTool.setCookie("meeting_pwd", meetingConfig.pwd);

            var signature = ZoomMtg.generateSignature({
                meetingNumber: meetingConfig.mn,
                apiKey: API_KEY,
                apiSecret: API_SECRET,
                role: meetingConfig.role,
                success: function (res) {
                    console.log(res.result);
                    meetingConfig.signature = res.result;
                    meetingConfig.apiKey = API_KEY;

                    e.preventDefault();
                    abp.ajax({
                        url: abp.appPath + 'MeetingManager/JoinMeetingHost',
                        type: 'POST',
                        dataType: 'html',
                        success: function (content) {
                            $('#tmpArgs').val(testTool.serialize(meetingConfig));
                            $('#MeetingModal div.modal-content').html(content);
                        },
                        error: function (e) {
                            alert(e.message);
                        }
                    });
                },
            });
        }

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