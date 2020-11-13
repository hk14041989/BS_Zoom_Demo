using Abp.AutoMapper;
using Abp.Domain.Repositories;
using BS_Zoom_Demo.Teachers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Teachers
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonAppService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        //This method uses async pattern that is supported by ASP.NET Boilerplate
        [Obsolete]
        public async Task<GetAllTeachersOutput> GetAllTeachers()
        {
            var teachers = await _personRepository.GetAllListAsync();
            return new GetAllTeachersOutput
            {
                Teachers = teachers.MapTo<List<PersonDto>>()
            };
        }
    }
}
