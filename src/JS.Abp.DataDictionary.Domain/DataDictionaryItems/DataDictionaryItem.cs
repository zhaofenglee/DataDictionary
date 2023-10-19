using JS.Abp.DataDictionary.DataDictionaries;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItem : FullAuditedAggregateRoot<Guid>
    {
        public virtual int Sequence { get; set; }

        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? DisplayText { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public virtual bool IsActive { get; set; }
        public Guid? DataDictionaryId { get; set; }

        protected DataDictionaryItem()
        {

        }

        public DataDictionaryItem(Guid id, Guid? dataDictionaryId, int sequence, bool isActive, string? code = null, string? displayText = null, string? description = null)
        {

            Id = id;
            Check.Length(code, nameof(code), DataDictionaryItemConsts.CodeMaxLength, 0);
            Check.Length(displayText, nameof(displayText), DataDictionaryItemConsts.DisplayTextMaxLength, 0);
            Check.Length(description, nameof(description), DataDictionaryItemConsts.DescriptionMaxLength, 0);
            Sequence = sequence;
            IsActive = isActive;
            Code = code;
            DisplayText = displayText;
            Description = description;
            DataDictionaryId = dataDictionaryId;
        }

    }
}