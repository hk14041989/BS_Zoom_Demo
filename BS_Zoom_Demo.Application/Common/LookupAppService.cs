using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using BS_Zoom_Demo.Teachers;
using System.Linq;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Common
{
    public class LookupAppService : BS_Zoom_DemoAppServiceBase, ILookupAppService
    {
        private readonly IRepository<Person> _personRepository;

        public LookupAppService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetTeachersComboboxItems()
        {
            var people = await _personRepository.GetAllListAsync();
            return new ListResultDto<ComboboxItemDto>(
                people.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.Name)).ToList()
            );
        }
    }
}
