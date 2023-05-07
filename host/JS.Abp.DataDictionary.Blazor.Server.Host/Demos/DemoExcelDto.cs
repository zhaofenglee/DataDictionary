using System;
using JS.Abp.DataDictionary.Attributes;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Demos
{
    public class DemoExcelDto
    {
        public string Name { get; set; }
        [ColumnText("DemoType")]
        public string DisplayName { get; set; }
    }
}