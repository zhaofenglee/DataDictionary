using JS.Abp.DataDictionary.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.DataDictionary.DataDictionaries;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries
{
    public class CreateModalModel : DataDictionaryPageModel
    {
        [BindProperty]
        public DataDictionaryCreateViewModel DataDictionary { get; set; }

        protected IDataDictionariesAppService _dataDictionariesAppService;

        public CreateModalModel(IDataDictionariesAppService dataDictionariesAppService)
        {
            _dataDictionariesAppService = dataDictionariesAppService;

            DataDictionary = new();
        }

        public virtual async Task OnGetAsync()
        {
            DataDictionary = new DataDictionaryCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _dataDictionariesAppService.CreateAsync(ObjectMapper.Map<DataDictionaryCreateViewModel, DataDictionaryCreateDto>(DataDictionary));
            return NoContent();
        }
    }

    public class DataDictionaryCreateViewModel : DataDictionaryCreateDto
    {
    }
}