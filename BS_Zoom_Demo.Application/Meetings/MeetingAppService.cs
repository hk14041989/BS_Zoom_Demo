using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;
using BS_Zoom_Demo.Meetings.Dtos;
using BS_Zoom_Demo.Teachers;
using System.Collections.Generic;

namespace BS_Zoom_Demo.Meetings
{
    public class MeetingAppService : ApplicationService, IMeetingAppService
    {
        //These members set in constructor using constructor injection.

        private readonly IMeetingRepository _meetingRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public MeetingAppService(IMeetingRepository meetingRepository, IRepository<Person> personRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public GetMeetingsOutput GetMeetings(GetMeetingsInput input)
        {
            //Called specific GetAllWithPeople method of task repository.
            var meetings = _meetingRepository.GetAllWithTeachers(input.AssignedPersonId, input.State);

            //Used AutoMapper to automatically convert List<Task> to List<TaskDto>.
            return new GetMeetingsOutput
            {
                Meetings = _mapper.Map<List<MeetingDto>>(meetings)
            };
        }

        public void UpdateMeeting(UpdateMeetingInput input)
        {
            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Updating a task for input: " + input);

            //Retrieving a task entity with given id using standard Get method of repositories.
            var task = _meetingRepository.Get(input.MeetingId);

            //Updating changed properties of the retrieved task entity.

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }

        public void CreateMeeting(CreateMeetingInput input)
        {
            //We can use Logger, it's defined in ApplicationService class.
            Logger.Info("Creating a task for input: " + input);

            //Creating a new Task entity with given input's properties
            var meeting = new Meeting { Description = input.Description };

            if (input.AssignedPersonId.HasValue)
            {
                meeting.AssignedPersonId = input.AssignedPersonId.Value;
            }

            //Saving entity with standard Insert method of repositories.
            _meetingRepository.Insert(meeting);
        }
    }
}
