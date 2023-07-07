using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace JS.Abp.DataDictionary.DataDictionaryItems;

public class GetDataDictionaryItemsWithCodeInput: PagedAndSortedResultRequestDto
{
    [Required]
    public string DataDictionaryCode { get; set; }
    [CanBeNull] public string FilterText { get; set; }
    [CanBeNull] public string Code { get; set; }
    [CanBeNull] public string DisplayText { get; set; }
    [CanBeNull] public string Description { get; set; }
    public bool? IsStatic { get; set; }

    public GetDataDictionaryItemsWithCodeInput()
    {

    }
}