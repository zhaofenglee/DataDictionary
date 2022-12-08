using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DataDictionary.EntityFrameworkCore;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class EfCoreDataDictionaryRepository : EfCoreRepository<IDataDictionaryDbContext, DataDictionary, Guid>, IDataDictionaryRepository
    {
        public EfCoreDataDictionaryRepository(IDbContextProvider<IDataDictionaryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<DataDictionary>> GetListAsync(
            string filterText = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, displayText, description, isStatic);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, displayText, description, isStatic);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DataDictionary> ApplyFilter(
            IQueryable<DataDictionary> query,
            string filterText,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.DisplayText.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isStatic.HasValue, e => e.IsStatic == isStatic);
        }
    }
}