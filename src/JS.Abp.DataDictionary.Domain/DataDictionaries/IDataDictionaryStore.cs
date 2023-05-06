using System.Threading.Tasks;

namespace JS.Abp.DataDictionary.DataDictionaries;

public interface IDataDictionaryStore
{
    DataDictionary FindByCode(string code);
    
    Task<DataDictionary> FindByCodeAsync(string code);
}