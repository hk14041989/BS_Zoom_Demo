using Abp.Localization;
using BS_Zoom_Demo.Meetings;
using BS_Zoom_Demo.Meetings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Models.Meetings
{
    public class IndexViewModel
    {
        public IReadOnlyList<MeetingDto> Meetings { get; }        

        public MeetingState? SelectedMeetingState { get; set; }

        public ILocalizationManager LocalizationManager;

        public IndexViewModel(IReadOnlyList<MeetingDto> meetings, ILocalizationManager localizationManager)
        {
            Meetings = meetings;
            LocalizationManager = localizationManager;
        }

        public string GetMeetingLabel(MeetingDto meeting)
        {
            switch (meeting.State)
            {
                case (byte)MeetingState.Active:
                    return "label-success";
                default:
                    return "label-default";
            }
        }

        public List<SelectListItem> GetMeetingsStateSelectListItems(ILocalizationManager localizationManager)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = localizationManager.GetString(BS_Zoom_DemoConsts.LocalizationSourceName, "AllMeetings"),
                    Value = "",
                    Selected = SelectedMeetingState == null
                }
            };

            list.AddRange(Enum.GetValues(typeof(MeetingState))
                    .Cast<MeetingState>()
                    .Select(state =>
                        new SelectListItem
                        {
                            Text = localizationManager.GetString(BS_Zoom_DemoConsts.LocalizationSourceName, $"MeetingState_{state}"),
                            Value = state.ToString(),
                            Selected = state == SelectedMeetingState
                        })
            );

            return list;
        }
    }
}