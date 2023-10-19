using JS.Abp.DataDictionary.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaryItems
{
    public class EditModalModel : DataDictionaryPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DataDictionaryItemUpdateViewModel DataDictionaryItem { get; set; }

        public List<SelectListItem> DataDictionaryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IDataDictionaryItemsAppService _dataDictionaryItemsAppService;

        public EditModalModel(IDataDictionaryItemsAppService dataDictionaryItemsAppService)
        {
            _dataDictionaryItemsAppService = dataDictionaryItemsAppService;

            DataDictionaryItem = new();
        }

        public virtual async Task OnGetAsync()
        {
            var dataDictionaryItemWithNavigationPropertiesDto = await _dataDictionaryItemsAppService.GetWithNavigationPropertiesAsync(Id);
            DataDictionaryItem = ObjectMapper.Map<DataDictionaryItemDto, DataDictionaryItemUpdateViewModel>(dataDictionaryItemWithNavigationPropertiesDto.DataDictionaryItem);

            DataDictionaryLookupList.AddRange((
                                    await _dataDictionaryItemsAppService.GetDataDictionaryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _dataDictionaryItemsAppService.UpdateAsync(Id, ObjectMapper.Map<DataDictionaryItemUpdateViewModel, DataDictionaryItemUpdateDto>(DataDictionaryItem));
            return NoContent();
        }
    }

    public class DataDictionaryItemUpdateViewModel : DataDictionaryItemUpdateDto
    {
    }
}