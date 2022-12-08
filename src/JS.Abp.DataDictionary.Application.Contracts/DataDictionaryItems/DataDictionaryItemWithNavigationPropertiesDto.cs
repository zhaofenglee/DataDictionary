using JS.Abp.DataDictionary.DataDictionaries;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemWithNavigationPropertiesDto
    {
        public DataDictionaryItemDto DataDictionaryItem { get; set; }

        public DataDictionaryDto DataDictionary { get; set; }

    }
}