using System;
using JS.Abp.DataDictionary.Attributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Demos
{
    public class DemoDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}