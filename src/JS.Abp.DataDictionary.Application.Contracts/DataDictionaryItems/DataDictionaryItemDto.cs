using JS.Abp.DataDictionary.Attributes;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int? Sequence { get; set; }
        public string Code { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public bool IsStatic { get; set; }
        public Guid? DataDictionaryId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}