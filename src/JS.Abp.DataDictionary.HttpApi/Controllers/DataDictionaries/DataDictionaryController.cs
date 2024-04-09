using JS.Abp.DataDictionary.DataDictionaries;
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

namespace JS.Abp.DataDictionary.Controllers.DataDictionaries
{
    [RemoteService(Name = DataDictionaryRemoteServiceConsts.RemoteServiceName)]
    [Area("dataDictionary")]
    [ControllerName("DataDictionary")]
    [Route("api/data-dictionary/data-dictionaries")]
    public class DataDictionaryController : AbpController, IDataDictionariesAppService
    {
        private readonly IDataDictionariesAppService _dataDictionariesAppService;
        public DataDictionaryController(IDataDictionariesAppService dataDictionariesAppService)
        {
            _dataDictionariesAppService = dataDictionariesAppService;
        }
        [HttpPost]
        public virtual Task<DataDictionaryDto> CreateAsync(DataDictionaryCreateDto input)
        {
            return _dataDictionariesAppService.CreateAsync(input);
        }
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _dataDictionariesAppService.DeleteAsync(id);
        }
        [HttpGet]
        [Route("find-by-code/{code}")]
        public virtual Task<DataDictionaryDto> FindByCodeAsync(string code)
        {
            return _dataDictionariesAppService.FindByCodeAsync(code);
        }
        [HttpGet]
        [Route("{id}")]
        public virtual Task<DataDictionaryDto> GetAsync(Guid id)
        {
            return _dataDictionariesAppService.GetAsync(id);
        }
        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _dataDictionariesAppService.GetDownloadTokenAsync();
        }
        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DataDictionaryExcelDownloadDto input)
        {
            return _dataDictionariesAppService.GetListAsExcelFileAsync(input);
        }
        [HttpGet]
        public virtual Task<PagedResultDto<DataDictionaryDto>> GetListAsync(GetDataDictionariesInput input)
        {
            return _dataDictionariesAppService.GetListAsync(input);
        }
        [HttpPut]
        [Route("{id}")]
        public virtual Task<DataDictionaryDto> UpdateAsync(Guid id, DataDictionaryUpdateDto input)
        {
            return _dataDictionariesAppService.UpdateAsync(id,input);
        }
    }
}
