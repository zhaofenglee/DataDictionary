using JS.Abp.DataDictionary.DataDictionaryItems;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DataDictionaryDto()
        {
            this.Items = new List<DataDictionaryItemDto>();
        }
        public string Code { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public bool IsStatic { get; set; }

        public string ConcurrencyStamp { get; set; }

        public List<DataDictionaryItemDto> Items { get; set; }
    }
}