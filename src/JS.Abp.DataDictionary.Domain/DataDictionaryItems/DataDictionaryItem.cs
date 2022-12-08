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
        public virtual JS.Abp.DataDictionary.DataDictionaries.DataDictionary DataDictionary { get; set; } 
        public virtual int? Sequence { get; set; }
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string DisplayText { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool IsStatic { get; set; }
        public Guid? DataDictionaryId { get; set; }

        public DataDictionaryItem()
        {

        }

        public DataDictionaryItem(Guid id, Guid? dataDictionaryId, string code, string displayText, string description, bool isStatic, int? sequence = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), DataDictionaryItemConsts.CodeMaxLength, 0);
            Check.NotNull(displayText, nameof(displayText));
            Check.Length(displayText, nameof(displayText), DataDictionaryItemConsts.DisplayTextMaxLength, 0);
            Check.Length(description, nameof(description), DataDictionaryItemConsts.DescriptionMaxLength, 0);
            Code = code;
            DisplayText = displayText;
            Description = description;
            IsStatic = isStatic;
            Sequence = sequence;
            DataDictionaryId = dataDictionaryId;
        }

        //public override object[] GetKeys()
        //{
        //    return new object[] { DataDictionaryId };
        //}

        //public void SetContent(string displayText, string description)
        //{
        //    DisplayText = displayText;
        //    Description = description;
        //}

    }
}