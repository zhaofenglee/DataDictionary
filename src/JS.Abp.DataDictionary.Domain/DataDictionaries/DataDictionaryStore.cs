using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using System.Linq;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Threading;

namespace JS.Abp.DataDictionary.DataDictionaries;

/// <summary>
/// There is a problem with retrieving data dictionary from cache, it is now modified to directly retrieve it from the repository. This functionality is deprecated.
/// </summary>
public class DataDictionaryStore:IDataDictionaryStore, ITransientDependency
{
    private readonly IDataDictionaryRepository _dataDictionaryRepository;
    private readonly IDataDictionaryItemRepository _dataDictionaryItemRepository;
    private readonly IDistributedCache<DataDictionary, string> _cache;
    protected DataDictionaryOptions Options { get; }
    
    public DataDictionaryStore(IDataDictionaryRepository dataDictionaryRepository, 
        IDataDictionaryItemRepository dataDictionaryItemRepository,
        IDistributedCache<DataDictionary, string> cache,
        IOptions<DataDictionaryOptions> options)
    {
        _dataDictionaryRepository = dataDictionaryRepository;
        _dataDictionaryItemRepository = dataDictionaryItemRepository;
        _cache = cache;
        Options = options.Value;
    }
    public virtual DataDictionary FindByCode(string code)
    {
        return AsyncHelper.RunSync(()=> FindByCodeAsync(code));;
    }

    public virtual async Task<DataDictionary> FindByCodeAsync(string code)
    {
        
        // await _cache.GetOrAddAsync(
        //     $"DataDictionary:{code}",
        //     async () =>  new DataDictionary() ,
        //     () => new DistributedCacheEntryOptions
        //     {
        //         AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Options.CacheExpirationTime)
        //     }
        // );
         //return await GetDictFromDatabaseAsync(code);
        return await _cache.GetOrAddAsync(
            $"DataDictionary:{code}",
            async () => await GetDictFromDatabaseAsync(code),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Options.CacheExpirationTime)
            }
        );
    }
    
    private async Task<DataDictionary> GetDictFromDatabaseAsync(string code)
    {
        var dataDictionaries = await _dataDictionaryRepository.GetListAsync(x => x.Code== code&&x.IsActive);
        if (!dataDictionaries.Any())
        {
            return null;
        }
        var dataDictionary = dataDictionaries.FirstOrDefault();
        var items = await _dataDictionaryItemRepository.GetListAsync(x => x.DataDictionaryId == dataDictionary.Id&&x.IsActive);
        if (items.Count>0)
        {
            return dataDictionary;
        }
        else
        { 
            return null; 
        }
    }
}