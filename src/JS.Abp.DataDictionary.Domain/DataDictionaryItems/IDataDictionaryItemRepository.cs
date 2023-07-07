using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public interface IDataDictionaryItemRepository : IRepository<DataDictionaryItem, Guid>
    {
        Task<DataDictionaryItemWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DataDictionaryItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            Guid? dataDictionaryId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<DataDictionaryItem>> GetListAsync(
                    string filterText = null,
                    int? sequenceMin = null,
                    int? sequenceMax = null,
                    string code = null,
                    string displayText = null,
                    string description = null,
                    bool? isStatic = null,
                    Guid? dataDictionaryId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            Guid? dataDictionaryId = null,
            CancellationToken cancellationToken = default);
    }
}