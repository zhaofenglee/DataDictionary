using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemCreateDto
    {
        [Required]
        public int? Sequence { get; set; }
        [Required]
        [StringLength(DataDictionaryItemConsts.CodeMaxLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(DataDictionaryItemConsts.DisplayTextMaxLength)]
        public string DisplayText { get; set; }
        [StringLength(DataDictionaryItemConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? DataDictionaryId { get; set; }
    }
}