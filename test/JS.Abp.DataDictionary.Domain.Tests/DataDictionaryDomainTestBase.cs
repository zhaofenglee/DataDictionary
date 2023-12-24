using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DataDictionaryDomainTestBase<TStartupModule> : DataDictionaryTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
