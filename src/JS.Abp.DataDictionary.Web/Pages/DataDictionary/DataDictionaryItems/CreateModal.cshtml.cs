using JS.Abp.DataDictionary.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaryItems
{
    public class CreateModalModel : DataDictionaryPageModel
    {
        [BindProperty(SupportsGet = true)] 
        public Guid DataDictionaryId { get; set; }
        [BindProperty]
        public DataDictionaryItemCreateViewModel DataDictionaryItem { get; set; }

        public List<SelectListItem> DataDictionaryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IDataDictionaryItemsAppService _dataDictionaryItemsAppService;

        public CreateModalModel(IDataDictionaryItemsAppService dataDictionaryItemsAppService)
        {
            _dataDictionaryItemsAppService = dataDictionaryItemsAppService;

            DataDictionaryItem = new();
        }

        public virtual async Task OnGetAsync()
        {
            DataDictionaryItem = new DataDictionaryItemCreateViewModel()
            {
                DataDictionaryId = DataDictionaryId,
            };
            DataDictionaryLookupList.AddRange((
                                    await _dataDictionaryItemsAppService.GetDataDictionaryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _dataDictionaryItemsAppService.CreateAsync(ObjectMapper.Map<DataDictionaryItemCreateViewModel, DataDictionaryItemCreateDto>(DataDictionaryItem));
            return NoContent();
        }
    }

    public class DataDictionaryItemCreateViewModel : DataDictionaryItemCreateDto
    {
    }
}