using JS.Abp.DataDictionary.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public interface IDataDictionaryItemsAppService : IApplicationService
    {
        Task<PagedResultDto<DataDictionaryItemWithNavigationPropertiesDto>> GetListAsync(GetDataDictionaryItemsInput input);
        Task<PagedResultDto<DataDictionaryItemDto>> GetListWithItemAsync(GetDataDictionaryItemsWithCodeInput input);
        Task<DataDictionaryItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DataDictionaryItemDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetDataDictionaryLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DataDictionaryItemDto> CreateAsync(DataDictionaryItemCreateDto input);

        Task<DataDictionaryItemDto> UpdateAsync(Guid id, DataDictionaryItemUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryItemExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}