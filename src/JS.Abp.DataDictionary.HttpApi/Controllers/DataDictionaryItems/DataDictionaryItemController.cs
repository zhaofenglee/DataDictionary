using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Versioning;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace JS.Abp.DataDictionary.Controllers.DataDictionaryItems
{
    [RemoteService(Name = DataDictionaryRemoteServiceConsts.RemoteServiceName)]
    [Area("dataDictionary")]
    [ControllerName("DataDictionaryItem")]
    [Route("api/data-dictionary/data-dictionary-item")]
    public class DataDictionaryItemController : AbpController, IDataDictionaryItemsAppService
    {
        private readonly IDataDictionaryItemsAppService _dataDictionaryItemsAppService;
        public DataDictionaryItemController(IDataDictionaryItemsAppService dataDictionaryItemsAppService)
        {
            _dataDictionaryItemsAppService = dataDictionaryItemsAppService;
        }
        [HttpPost]
        public virtual Task<DataDictionaryItemDto> CreateAsync(DataDictionaryItemCreateDto input)
        {
           return _dataDictionaryItemsAppService.CreateAsync(input);
        }
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _dataDictionaryItemsAppService.DeleteAsync(id);
        }
        [HttpGet]
        [Route("{id}")]
        public virtual Task<DataDictionaryItemDto> GetAsync(Guid id)
        {
            return _dataDictionaryItemsAppService.GetAsync(id);
        }
        [HttpGet]
        [Route("data-dictionary-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetDataDictionaryLookupAsync(LookupRequestDto input)
        {
            return _dataDictionaryItemsAppService.GetDataDictionaryLookupAsync(input);
        }
        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _dataDictionaryItemsAppService.GetDownloadTokenAsync();
        }
        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryItemExcelDownloadDto input)
        {
            return _dataDictionaryItemsAppService.GetListAsExcelFileAsync(input);
        }
        [HttpGet]
        public virtual Task<PagedResultDto<DataDictionaryItemWithNavigationPropertiesDto>> GetListAsync(GetDataDictionaryItemsInput input)
        {
            return _dataDictionaryItemsAppService.GetListAsync(input);
        }
        [HttpGet]
        [Route("with-items")]
        public Task<PagedResultDto<DataDictionaryItemDto>> GetListWithItemAsync(GetDataDictionaryItemsWithCodeInput input)
        {
            return _dataDictionaryItemsAppService.GetListWithItemAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<DataDictionaryItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _dataDictionaryItemsAppService.GetWithNavigationPropertiesAsync(id);
        }
        [HttpPut]
        [Route("{id}")]
        public virtual Task<DataDictionaryItemDto> UpdateAsync(Guid id, DataDictionaryItemUpdateDto input)
        {
            return _dataDictionaryItemsAppService.UpdateAsync(id, input);
        }
    }
}
