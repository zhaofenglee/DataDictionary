using System;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries;

public class DetailsModel : AbpPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public DataDictionaryUpdateViewModel DataDictionary { get; set; }
    
    protected IDataDictionariesAppService _dataDictionariesAppService;

    public DetailsModel(IDataDictionariesAppService dataDictionariesAppService)
    {
        _dataDictionariesAppService = dataDictionariesAppService;

        DataDictionary = new();
    }

    public async Task OnGetAsync()
    {
        var dataDictionary = await _dataDictionariesAppService.GetAsync(Id);
        DataDictionary = ObjectMapper.Map<DataDictionaryDto, DataDictionaryUpdateViewModel>(dataDictionary);
        
        await Task.CompletedTask;
    }
}