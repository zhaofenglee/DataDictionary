using System;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemExcelDto
    {
        public int Sequence { get; set; }
        public string Code { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}