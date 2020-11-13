using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace BS_Zoom_Demo.Teachers.Dtos
{
    [AutoMapFrom(typeof(Person))]
    public class PersonDto : EntityDto
    {
        public string Name { get; set; }
    }
}
