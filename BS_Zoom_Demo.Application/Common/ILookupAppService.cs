using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Common
{
    public interface ILookupAppService : IApplicationService
    {
        ListResultDto<ComboboxItemDto> GetTeachersComboboxItems();
    }
}