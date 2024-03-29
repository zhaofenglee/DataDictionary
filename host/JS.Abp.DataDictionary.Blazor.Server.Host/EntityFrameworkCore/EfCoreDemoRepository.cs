using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DataDictionary.Blazor.Server.Host.Demos;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.EntityFrameworkCore
{
    public class EfCoreDemoRepository : EfCoreRepository<UnifiedDbContext, Demo, Guid>, IDemoRepository
    {
     

        public EfCoreDemoRepository(IDbContextProvider<UnifiedDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        public async Task<Demo> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var item = await query.FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));
            return item;
        }
        public async Task<List<Demo>> GetListAsync(
            string filterText = null,
            string name = null,
            string displayName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, name, displayName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DemoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string displayName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetDbSetAsync(), filterText, name, displayName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Demo> ApplyFilter(
            IQueryable<Demo> query,
            string filterText,
            string name = null,
            string displayName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.DisplayName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.DisplayName.Contains(displayName));
        }
    }
}