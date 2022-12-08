using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public interface IDataDictionaryItemManager
    {
        Task<DataDictionaryItem> CreateAsync(
        Guid? dataDictionaryId, string code, string displayText, string description, bool isStatic, int? sequence = null);

        Task<DataDictionaryItem> UpdateAsync(
            Guid id,
            Guid? dataDictionaryId, string code, string displayText, string description, bool isStatic, int? sequence = null, [CanBeNull] string concurrencyStamp = null
        );
    }
}
