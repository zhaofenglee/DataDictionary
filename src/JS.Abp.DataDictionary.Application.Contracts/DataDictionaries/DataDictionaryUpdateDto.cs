using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(DataDictionaryConsts.CodeMaxLength, MinimumLength = DataDictionaryConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(DataDictionaryConsts.DisplayTextMaxLength, MinimumLength = DataDictionaryConsts.DisplayTextMinLength)]
        public string DisplayText { get; set; }
        [StringLength(DataDictionaryConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsStatic { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}