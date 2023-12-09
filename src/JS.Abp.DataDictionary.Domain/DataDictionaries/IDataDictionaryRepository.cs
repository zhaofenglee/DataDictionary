using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public interface IDataDictionaryRepository : IRepository<DataDictionary, Guid>
    {
        Task<DataDictionary> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<List<DataDictionary>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default);
    }
}