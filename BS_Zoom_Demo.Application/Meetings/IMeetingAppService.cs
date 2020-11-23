using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BS_Zoom_Demo.Meetings.Dtos;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Meetings
{
    public interface IMeetingAppService : IApplicationService
    {
        GetMeetingsOutput GetMeetings(GetMeetingsInput input);

        void UpdateMeeting(UpdateMeetingInput input, string meetingPass);

        void CreateMeeting(CreateMeetingInput input, string accessToken);

        void Delete(EntityDto<long> input);

        string GetMeetingInfor(long meetingId, string accessToken);

        string GetListMeettings();
    }
}