using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DataDictionaryApplicationTestBase<TStartupModule> : DataDictionaryTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
