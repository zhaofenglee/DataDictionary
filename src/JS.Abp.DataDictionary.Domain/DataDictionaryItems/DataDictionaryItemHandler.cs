using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaries;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace JS.Abp.DataDictionary.DataDictionaryItems;

public class DataDictionaryItemHandler: ILocalEventHandler<EntityChangedEventData<DataDictionaryItem>>, ITransientDependency
{
    private IDistributedCache<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, string> Cache { get; }
    private IUnitOfWorkManager UnitOfWorkManager { get; }
    private readonly IDataDictionaryRepository _dataDictionaryRepository;
    
    public DataDictionaryItemHandler(IDistributedCache<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, string> cache,
        IUnitOfWorkManager unitOfWorkManager,
    IDataDictionaryRepository dataDictionaryRepository)
    {
        Cache = cache;
        UnitOfWorkManager = unitOfWorkManager;
        _dataDictionaryRepository = dataDictionaryRepository;
    }
    
    public async Task HandleEventAsync(EntityChangedEventData<DataDictionaryItem> eventData)
    {
        if (eventData.Entity.DataDictionaryId == null)
        {
            return;
        }
        var dataDictionary = await _dataDictionaryRepository.GetAsync(eventData.Entity.DataDictionaryId.Value);
        await Cache.RemoveAsync($"DataDictionary:{dataDictionary.Code}");
        
        if(UnitOfWorkManager.Current != null)
        {
            await Cache.RemoveAsync($"DataDictionary:{dataDictionary.Code}", considerUow: true);
        }
    }
}