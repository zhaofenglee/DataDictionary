using Volo.Abp;
using Volo.Abp.MongoDB;

namespace JS.Abp.DataDictionary.MongoDB;

public static class DataDictionaryMongoDbContextExtensions
{
    public static void ConfigureDataDictionary(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
