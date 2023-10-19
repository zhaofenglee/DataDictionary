using JS.Abp.DataDictionary.DataDictionaries;
using System;
using System.Collections.Generic;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public  class DataDictionaryItemWithNavigationProperties
    {
        public DataDictionaryItem DataDictionaryItem { get; set; }

        public DataDictionaries.DataDictionary DataDictionary { get; set; }
        

        
    }
}