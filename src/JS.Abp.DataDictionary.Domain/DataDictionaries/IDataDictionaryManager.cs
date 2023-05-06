using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public interface IDataDictionaryManager
    {
        Task<DataDictionary> CreateAsync(
        string code, string displayText, string description, bool isStatic);
        Task<DataDictionary> UpdateAsync(
            Guid id,
            string code, string displayText, string description, bool isStatic, [CanBeNull] string concurrencyStamp = null
        );
    }
}
