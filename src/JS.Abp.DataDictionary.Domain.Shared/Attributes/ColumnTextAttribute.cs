using System;
using System.Collections.Generic;
using System.Text;

namespace JS.Abp.DataDictionary.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnTextAttribute : Attribute
    {
        public string Code { get; set; }
        public ColumnTextAttribute(string code)
        {
            Code = code;
        }
    }
}
