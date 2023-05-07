using JS.Abp.DataDictionary.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Demos
{
    public interface IDemosAppService : IApplicationService
    {
        Task<PagedResultDto<DemoDto>> GetListAsync(GetDemosInput input);

        Task<DemoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DemoDto> CreateAsync(DemoCreateDto input);

        Task<DemoDto> UpdateAsync(Guid id, DemoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DemoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}