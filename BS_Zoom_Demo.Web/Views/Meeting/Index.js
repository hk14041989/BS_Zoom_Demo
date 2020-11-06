
$(function () {
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

    // some help code, remember mn, pwd, lang to cookie, and autofill.
    document.getElementById("display_name").value =
        "CDN" +
        ZoomMtg.getJSSDKVersion()[0] +
        testTool.detectOS() +
        "#" +
        testTool.getBrowserInfo();

    var tmpMn = $('#meeting_id').val().replace(/([^0-9])+/i, "");
    if (tmpMn.match(/([0-9]{9,11})/)) {
        tmpMn = tmpMn.match(/([0-9]{9,11})/)[1];
    }
    var tmpPwd = $('#meetting_password').val().match(/pwd=([\d,\w]+)/);
    if (tmpPwd) {
        testTool.setCookie("meeting_pwd", tmpPwd[1]);
    }
    testTool.setCookie(
        "meeting_number",
        $('#meeting_id').val()
    );

    $('#zmmtg-root').css('display', 'none');

    document.getElementById('create_meeting').addEventListener("click", function (e) {
        let userId = $('#user_id').val();
        let accessToken = $('#access_token').val();
        let topicName = $('#topic_name').val();
        let meetingType = $('#meeting_type option:selected').val();
        let startTime = $('#start_time').val();
        let duration = $('#duration').val();
        let scheduleFor = $('#schedule_for').val();
        let timeZone = $('#time_zone option:selected').val();
        let password = $('#password').val();
        let agenda = $('#agenda').val();
        let recurrenceType = $('#recurrence_type option:selected').val();
        let repeatInterval = $('#repeat_interval').val();
        let weeklyDays = $('#weekly_days option:selected').val();
        let monthlyDay = $('#monthly_day').val();
        let monthlyWeek = $('#monthly_week option:selected').val();
        let monthlyWeekDay = $('#monthly_week_day option:selected').val();
        let endTimes = $('#end_times').val();
        let endDateTime = $('#end_date_time').val();
        let hostVideo = $('#host_video option:selected').val() == 0 ? "false" : "true";
        let participantVideo = $('#participant_video option:selected').val() == 0 ? "false" : "true";
        let cnMeeting = $('#cn_meeting option:selected').val() == 0 ? "false" : "true";
        let inMeeting = $('#in_meeting option:selected').val() == 0 ? "false" : "true";
        let joinBeforeHost = $('#join_before_host option:selected').val() == 0 ? "false" : "true";
        let muteUponEntry = $('#mute_upon_entry option:selected').val() == 0 ? "false" : "true";
        let watermark = $('#watermark option:selected').val() == 0 ? "false" : "true";
        let usePmi = $('#use_pmi option:selected').val() == 0 ? "false" : "true";
        let enforceLogin = $('#enforce_login option:selected').val() == 0 ? "false" : "true";
        let enforceLoginDomains = $('#enforce_login_domains').val();
        let approvalType = $('#approval_type option:selected').val();
        let registrationType = $('#registration_type option:selected').val();
        let audio = $('#audio option:selected').val();
        let autoRecording = $('#auto_recording option:selected').val();
        let alternativeHosts = $('#alternative_hosts').val();
        let closeRegistration = $('#close_registration option:selected').val() == 0 ? "false" : "true";
        let waitingRoom = $('#waiting_room option:selected').val() == 0 ? "false" : "true";
        let globalDialInOuntries = $('#global_dial_in_ountries').val();
        let contactName = $('#contact_name').val();
        let contactEmail = $('#contact_email').val();
        let registransEmailNotification = $('#registrans_email_notification option:selected').val() == 0 ? "false" : "true";
        let meetingAuthentication = $('#meeting_authentication option:selected').val() == 0 ? "false" : "true";

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Meeting/CreateMeeting',
            data: JSON.stringify({
                userId: userId, accessToken: accessToken, topicName: topicName, meetingType: meetingType, startTime: startTime, duration: duration,
                scheduleFor: scheduleFor, timeZone: timeZone, password: password, agenda: agenda, recurrenceType: recurrenceType, repeatInterval: repeatInterval,
                weeklyDays: weeklyDays, monthlyDay: monthlyDay, monthlyWeek: monthlyWeek, monthlyWeekDay: monthlyWeekDay, endTimes: endTimes, endDateTime: endDateTime, hostVideo: hostVideo,
                participantVideo: participantVideo, cnMeeting: cnMeeting, inMeeting: inMeeting, joinBeforeHost: joinBeforeHost, muteUponEntry: muteUponEntry, watermark: watermark,
                usePmi: usePmi, enforceLogin: enforceLogin, enforceLoginDomains: enforceLoginDomains, approvalType: approvalType, registrationType: registrationType, audio: audio,
                autoRecording: autoRecording, alternativeHosts: alternativeHosts, closeRegistration: closeRegistration, waitingRoom: waitingRoom, globalDialInOuntries: globalDialInOuntries,
                contactName: contactName, contactEmail: contactEmail, registransEmailNotification: registransEmailNotification, meetingAuthentication: meetingAuthentication
            }),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (content) {
                var result = content.data;

                $('#start_url').val(result.start_url);
                $('#meeting_id').val(result.meeting_id);
                $('#meetting_password').val(result.password);
                alert(content.message);
            },
            error: function (e) {
                alert(e.message);
            }
        });
    });

    document.getElementById('join_meeting_host').addEventListener("click", function (e) {
        e.preventDefault();
        var meetingConfig = testTool.getMeetingConfig();
        meetingConfig.mn = $('#meeting_id').val();
        meetingConfig.pwd = $('#meetting_password').val();
        meetingConfig.role = 1;

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
                    url: abp.appPath + 'Meeting/JoinMeetingHost',
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
    });

    document.getElementById('join_meeting_attendee').addEventListener("click", function (e) {
        e.preventDefault();
        var meetingConfig = testTool.getMeetingConfig();
        meetingConfig.mn = $('#meeting_id').val();
        meetingConfig.pwd = $('#meetting_password').val();
        meetingConfig.role = 0;

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
                    url: abp.appPath + 'Meeting/JoinMeetingHost',
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
    });
});