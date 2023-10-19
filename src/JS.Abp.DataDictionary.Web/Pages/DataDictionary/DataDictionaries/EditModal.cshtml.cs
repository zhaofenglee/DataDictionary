using JS.Abp.DataDictionary.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.DataDictionary.DataDictionaries;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries
{
    public class EditModalModel : DataDictionaryPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DataDictionaryUpdateViewModel DataDictionary { get; set; }

        protected IDataDictionariesAppService _dataDictionariesAppService;

        public EditModalModel(IDataDictionariesAppService dataDictionariesAppService)
        {
            _dataDictionariesAppService = dataDictionariesAppService;

            DataDictionary = new();
        }

        public virtual async Task OnGetAsync()
        {
            var dataDictionary = await _dataDictionariesAppService.GetAsync(Id);
            DataDictionary = ObjectMapper.Map<DataDictionaryDto, DataDictionaryUpdateViewModel>(dataDictionary);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _dataDictionariesAppService.UpdateAsync(Id, ObjectMapper.Map<DataDictionaryUpdateViewModel, DataDictionaryUpdateDto>(DataDictionary));
            return NoContent();
        }
    }

    public class DataDictionaryUpdateViewModel : DataDictionaryUpdateDto
    {
    }
}