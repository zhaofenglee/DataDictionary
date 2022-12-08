using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JS.Abp.DataDictionary.Permissions;
using JS.Abp.DataDictionary.DataDictionaryItems;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.DataDictionary.Shared;
using JS.Abp.DataDictionary.DataDictionaries;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    [RemoteService(IsEnabled = false)]
    // [Authorize(DataDictionaryPermissions.DataDictionaryItems.Default)]
    public class DataDictionaryItemsAppService : ApplicationService, IDataDictionaryItemsAppService
    {
        private readonly IDistributedCache<DataDictionaryItemExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDataDictionaryItemRepository _dataDictionaryItemRepository;
        private readonly DataDictionaryItemManager _dataDictionaryItemManager;
        private readonly IRepository<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, Guid> _dataDictionaryRepository;
       
        public DataDictionaryItemsAppService(IDataDictionaryItemRepository dataDictionaryItemRepository, DataDictionaryItemManager dataDictionaryItemManager, IDistributedCache<DataDictionaryItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, Guid> dataDictionaryRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _dataDictionaryItemRepository = dataDictionaryItemRepository;
            _dataDictionaryItemManager = dataDictionaryItemManager;
            _dataDictionaryRepository = dataDictionaryRepository;
           
        }

        public virtual async Task<PagedResultDto<DataDictionaryItemWithNavigationPropertiesDto>> GetListAsync(GetDataDictionaryItemsInput input)
        {
            var totalCount = await _dataDictionaryItemRepository.GetCountAsync(input.FilterText,input.SequenceMin,input.SequenceMax, input.Code, input.DisplayText, input.Description, input.IsStatic, input.DataDictionaryId);
            var items = await _dataDictionaryItemRepository.GetListWithNavigationPropertiesAsync(input.FilterText,input.SequenceMin,input.SequenceMax, input.Code, input.DisplayText, input.Description, input.IsStatic, input.DataDictionaryId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DataDictionaryItemWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DataDictionaryItemWithNavigationProperties>, List<DataDictionaryItemWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DataDictionaryItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DataDictionaryItemWithNavigationProperties, DataDictionaryItemWithNavigationPropertiesDto>
                (await _dataDictionaryItemRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DataDictionaryItemDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<DataDictionaryItem, DataDictionaryItemDto>(await _dataDictionaryItemRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetDataDictionaryLookupAsync(LookupRequestDto input)
        {
            var query = (await _dataDictionaryRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DisplayText != null &&
                         x.DisplayText.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<JS.Abp.DataDictionary.DataDictionaries.DataDictionary>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<JS.Abp.DataDictionary.DataDictionaries.DataDictionary>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _dataDictionaryItemRepository.DeleteAsync(id);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Create)]
        public virtual async Task<DataDictionaryItemDto> CreateAsync(DataDictionaryItemCreateDto input)
        {

            var dataDictionaryItem = await _dataDictionaryItemManager.CreateAsync(
            input.DataDictionaryId, input.Code, input.DisplayText, input.Description, input.IsStatic, input.Sequence
            );

            return ObjectMapper.Map<DataDictionaryItem, DataDictionaryItemDto>(dataDictionaryItem);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Edit)]
        public virtual async Task<DataDictionaryItemDto> UpdateAsync(Guid id, DataDictionaryItemUpdateDto input)
        {

            var dataDictionaryItem = await _dataDictionaryItemManager.UpdateAsync(
            id,
            input.DataDictionaryId, input.Code, input.DisplayText, input.Description, input.IsStatic,input.Sequence, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<DataDictionaryItem, DataDictionaryItemDto>(dataDictionaryItem);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryItemExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _dataDictionaryItemRepository.GetListAsync(input.FilterText, input.SequenceMin,input.SequenceMax, input.Code, input.DisplayText, input.Description, input.IsStatic);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DataDictionaryItem>, List<DataDictionaryItemExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "DataDictionaryItems.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DataDictionaryItemExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}