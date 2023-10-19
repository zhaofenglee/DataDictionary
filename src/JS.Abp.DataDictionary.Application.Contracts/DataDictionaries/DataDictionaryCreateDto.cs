using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryCreateDto
    {
        [Required]
        [StringLength(DataDictionaryConsts.CodeMaxLength, MinimumLength = DataDictionaryConsts.CodeMinLength)]
        public string Code { get; set; } = null!;
        [Required]
        [StringLength(DataDictionaryConsts.DisplayTextMaxLength, MinimumLength = DataDictionaryConsts.DisplayTextMinLength)]
        public string DisplayText { get; set; } = null!;
        [StringLength(DataDictionaryConsts.DescriptionMaxLength)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}