using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;

using Volo.Abp;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionary : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string DisplayText { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool IsStatic { get; set; }

        public virtual ICollection<DataDictionaryItem> Items { get; protected set; }

        public DataDictionary()
        {
            Items = new Collection<DataDictionaryItem>();
        }

        public DataDictionary(Guid id, string code, string displayText, string description, bool isStatic)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), DataDictionaryConsts.CodeMaxLength, DataDictionaryConsts.CodeMinLength);
            Check.NotNull(displayText, nameof(displayText));
            Check.Length(displayText, nameof(displayText), DataDictionaryConsts.DisplayTextMaxLength, DataDictionaryConsts.DisplayTextMinLength);
            Check.Length(description, nameof(description), DataDictionaryConsts.DescriptionMaxLength, 0);
            Code = code;
            DisplayText = displayText;
            Description = description;
            IsStatic = isStatic;

            Items = new Collection<DataDictionaryItem>();
        }

       
    }
}