using Abp.Application.Services;
using BS_Zoom_Demo.Meetings.Dtos;

namespace BS_Zoom_Demo.Meetings
{
    public interface IMeetingAppService : IApplicationService
    {
        GetMeetingsOutput GetMeetings(GetMeetingsInput input);

        void UpdateMeeting(UpdateMeetingInput input);

        void CreateMeeting(CreateMeetingInput input);
    }
}