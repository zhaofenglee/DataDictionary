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
using JS.Abp.DataDictionary.DataDictionaries;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    [RemoteService(IsEnabled = false)]
    // [Authorize(DataDictionaryPermissions.DataDictionaries.Default)]
    public class DataDictionariesAppService : ApplicationService, IDataDictionariesAppService
    {
        private readonly IDistributedCache<DataDictionaryExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly DataDictionaryManager _dataDictionaryManager;

        public DataDictionariesAppService(IDataDictionaryRepository dataDictionaryRepository, DataDictionaryManager dataDictionaryManager, IDistributedCache<DataDictionaryExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _dataDictionaryRepository = dataDictionaryRepository;
            _dataDictionaryManager = dataDictionaryManager;  
        }

        public virtual async Task<PagedResultDto<DataDictionaryDto>> GetListAsync(GetDataDictionariesInput input)
        {
            var totalCount = await _dataDictionaryRepository.GetCountAsync(input.FilterText, input.Code, input.DisplayText, input.Description, input.IsActive);
            var items = await _dataDictionaryRepository.GetListAsync(input.FilterText, input.Code, input.DisplayText, input.Description, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);

            
            return new PagedResultDto<DataDictionaryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DataDictionary>, List<DataDictionaryDto>>(items)
            };
        }

        public virtual async Task<DataDictionaryDto> GetAsync(Guid id)
        {
            var item =  ObjectMapper.Map<DataDictionary, DataDictionaryDto>(await _dataDictionaryRepository.GetAsync(id));
            return item;
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _dataDictionaryRepository.DeleteAsync(id);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Create)]
        public virtual async Task<DataDictionaryDto> CreateAsync(DataDictionaryCreateDto input)
        {

            var dataDictionary = await _dataDictionaryManager.CreateAsync(
            input.Code, input.DisplayText,  input.IsActive,input.Description
            );

            return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(dataDictionary);
        }

        [Authorize(DataDictionaryPermissions.DataDictionaries.Edit)]
        public virtual async Task<DataDictionaryDto> UpdateAsync(Guid id, DataDictionaryUpdateDto input)
        {

            var dataDictionary = await _dataDictionaryManager.UpdateAsync(
            id,
            input.DisplayText,input.IsActive, input.Description,  input.ConcurrencyStamp
            );

            return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(dataDictionary);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _dataDictionaryRepository.GetListAsync(input.FilterText, input.Code, input.DisplayText, input.Description, input.IsActive);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DataDictionary>, List<DataDictionaryExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "DataDictionaries.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DataDictionaryExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public virtual async Task<DataDictionaryDto> FindByCodeAsync(string code)
        {
            return ObjectMapper.Map<DataDictionary, DataDictionaryDto>(await _dataDictionaryRepository.FindByCodeAsync(code)) ;
        }
        
       
    }
}