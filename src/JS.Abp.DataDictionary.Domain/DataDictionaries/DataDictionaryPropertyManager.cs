using JS.Abp.DataDictionary.Attributes;
using JS.Abp.DataDictionary.DataDictionaryItems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryPropertyManager : DomainService
    {
        private readonly IDataDictionaryManager _dataDictionaryManager;
        public DataDictionaryPropertyManager(IDataDictionaryManager dataDictionaryManager)
        {
            _dataDictionaryManager = dataDictionaryManager;
        }

        public virtual async Task<TDto> GetAsync<TDto>(TDto sourceDto)
        {
            var type = typeof(TDto);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DataDictionaryColumnInfo columnInfo = new();
            var list = props.Select(p => 
            {
                var dictionaryCode = p.GetAttribute<ColumnTextAttribute>();
                if (dictionaryCode == null)
                    return null;
                return new DataDictionaryColumnInfo()
                {
                    Property = p,
                    DictionaryCode = dictionaryCode.Code,
                };
            } ).Where(_ => _ != null);
            if(list.Any())
            {
                foreach(var info in list)
                {
                    var dataDictionary = await _dataDictionaryManager.FindByCodeAsync(info.DictionaryCode);
                    if (dataDictionary != null && dataDictionary.Items.Count > 0)
                    {
                        var dataCode = (string)info.Property.GetValue(sourceDto);
                        if (dataCode == null)
                        {
                            break;
                        }
                        var value = dataDictionary.Items.Where(c => c.Code == dataCode).FirstOrDefault();
                        info.Property.SetValue(sourceDto, value?.DisplayText);
                    }

                }
            }
            return sourceDto;
        }

        public async Task<List<TDto>> GetListAsync<TDto>(IEnumerable<TDto> sourceListDto)
        {
            foreach (var dto in sourceListDto)
            {
                await GetAsync(dto);
            }
            return sourceListDto.ToList();
        }
    }

    public class DataDictionaryColumnInfo
    {
        public string DictionaryCode { get; set; }

        public PropertyInfo Property { get; set; }
    }
}
