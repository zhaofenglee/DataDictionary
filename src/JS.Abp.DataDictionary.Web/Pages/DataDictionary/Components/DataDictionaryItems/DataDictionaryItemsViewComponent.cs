using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.Components.DataDictionaryItems;
[Widget(
    StyleFiles = new[]
    {
        "/Pages/DataDictionary/Components/DataDictionaryItems/default.css"
    },
    ScriptFiles = new[]
    {
        "/Pages/DataDictionary/Components/DataDictionaryItems/default.js"
    })]
public class DataDictionaryItemsViewComponent: AbpViewComponent
{

    protected IDataDictionaryItemsAppService _dataDictionaryItemsAppService;

    public DataDictionaryItemsViewComponent(IDataDictionaryItemsAppService dataDictionaryItemsAppService)
    {
        _dataDictionaryItemsAppService = dataDictionaryItemsAppService;
    }
    
    public virtual async Task<IViewComponentResult> InvokeAsync(Guid? id)
    {
        return View(
            "~/Pages/DataDictionary/Components/DataDictionaryItems/Default.cshtml",new DataDictionaryItemsViewModel
            {
                DataDictionaryId = id
            } );
    }
}

public class DataDictionaryItemsViewModel
{
    public Guid? DataDictionaryId { get; set; }
    public string? ObjectNameFilter { get; set; }
    public string? DescriptionFilter { get; set; }
    public string? TargetTypeFilter { get; set; }
    [SelectItems(nameof(CanReadBoolFilterItems))]
    public string CanReadFilter { get; set; }

    public List<SelectListItem> CanReadBoolFilterItems { get; set; } =
        new List<SelectListItem>
        {
            new SelectListItem("", ""),
            new SelectListItem("Yes", "true"),
            new SelectListItem("No", "false"),
        };
    [SelectItems(nameof(CanCreateBoolFilterItems))]
    public string CanCreateFilter { get; set; }

    public List<SelectListItem> CanCreateBoolFilterItems { get; set; } =
        new List<SelectListItem>
        {
            new SelectListItem("", ""),
            new SelectListItem("Yes", "true"),
            new SelectListItem("No", "false"),
        };
    [SelectItems(nameof(CanEditBoolFilterItems))]
    public string CanEditFilter { get; set; }

    public List<SelectListItem> CanEditBoolFilterItems { get; set; } =
        new List<SelectListItem>
        {
            new SelectListItem("", ""),
            new SelectListItem("Yes", "true"),
            new SelectListItem("No", "false"),
        };
    [SelectItems(nameof(CanDeleteBoolFilterItems))]
    public string CanDeleteFilter { get; set; }

    public List<SelectListItem> CanDeleteBoolFilterItems { get; set; } =
        new List<SelectListItem>
        {
            new SelectListItem("", ""),
            new SelectListItem("Yes", "true"),
            new SelectListItem("No", "false"),
        };
}