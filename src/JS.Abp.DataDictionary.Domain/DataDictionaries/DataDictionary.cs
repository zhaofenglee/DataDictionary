using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Volo.Abp;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionary : FullAuditedAggregateRoot<Guid>
    {
        [NotNull] public virtual string Code { get; set; }

        [NotNull] public virtual string DisplayText { get; set; }

        [CanBeNull] public virtual string? Description { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual Collection<DataDictionaryItem> Items { get; protected set; }

        protected DataDictionary()
        {
            //Items = new List<DataDictionaryItem>();
        }

        public DataDictionary(Guid id, string code, string displayText, bool isActive, string? description = null)
        {
            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), DataDictionaryConsts.CodeMaxLength, DataDictionaryConsts.CodeMinLength);
            Check.NotNull(displayText, nameof(displayText));
            Check.Length(displayText, nameof(displayText), DataDictionaryConsts.DisplayTextMaxLength,
                DataDictionaryConsts.DisplayTextMinLength);
            Check.Length(description, nameof(description), DataDictionaryConsts.DescriptionMaxLength, 0);
            Code = code;
            DisplayText = displayText;
            IsActive = isActive;
            Description = description;
            Items = new Collection<DataDictionaryItem>();
        }

        public void AddItem(Collection<DataDictionaryItem> items)
        {
            Items = items;
        }
    }
}