using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class GetDataDictionaryItemsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public int? SequenceMin { get; set; }
        public int? SequenceMax { get; set; }
        public string? Code { get; set; }
        public string? DisplayText { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public Guid? DataDictionaryId { get; set; }

        public GetDataDictionaryItemsInput()
        {

        }
    }
}