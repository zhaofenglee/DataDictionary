using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries
{
    public class IndexModel : AbpPageModel
    {
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

        protected IDataDictionariesAppService _dataDictionariesAppService;

        public IndexModel(IDataDictionariesAppService dataDictionariesAppService)
        {
            _dataDictionariesAppService = dataDictionariesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}