using Abp.Application.Services;
using BS_Zoom_Demo.Teachers.Dtos;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Teachers
{
    public interface IPersonAppService : IApplicationService
    {
        Task<GetAllTeachersOutput> GetAllTeachers();
    }
}