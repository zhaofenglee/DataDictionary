using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public interface IDataDictionariesAppService : IApplicationService
    {
        Task<PagedResultDto<DataDictionaryDto>> GetListAsync(GetDataDictionariesInput input);

        Task<DataDictionaryDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DataDictionaryDto> CreateAsync(DataDictionaryCreateDto input);

        Task<DataDictionaryDto> UpdateAsync(Guid id, DataDictionaryUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<DataDictionaryDto> FindByCodeAsync(string code);
    }
}