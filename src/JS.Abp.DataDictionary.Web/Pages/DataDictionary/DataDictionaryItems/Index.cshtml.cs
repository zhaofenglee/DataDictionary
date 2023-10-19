using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaryItems
{
    public class IndexModel : AbpPageModel
    {
        public int? SequenceFilterMin { get; set; }

        public int? SequenceFilterMax { get; set; }
        public string? CodeFilter { get; set; }
        public string? DisplayTextFilter { get; set; }
        public string? DescriptionFilter { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(DataDictionaryLookupList))]
        public Guid? DataDictionaryIdFilter { get; set; }
        public List<SelectListItem> DataDictionaryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IDataDictionaryItemsAppService _dataDictionaryItemsAppService;

        public IndexModel(IDataDictionaryItemsAppService dataDictionaryItemsAppService)
        {
            _dataDictionaryItemsAppService = dataDictionaryItemsAppService;
        }

        public virtual async Task OnGetAsync()
        {
            DataDictionaryLookupList.AddRange((
                    await _dataDictionaryItemsAppService.GetDataDictionaryLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}