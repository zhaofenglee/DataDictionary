using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public bool? IsStatic { get; set; }

        public DataDictionaryExcelDownloadDto()
        {

        }
    }
}