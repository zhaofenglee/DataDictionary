using System;
using JetBrains.Annotations;
using JS.Abp.DataDictionary.Attributes;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JS.Abp.DataDictionary;

public class DemoTest 
{

    public virtual string Name { get; set; }

    [ColumnText("DemoType")]
    public virtual string DisplayName { get; set; }

    public DemoTest()
    {

    }

    public DemoTest(string name, string displayName)
    {
        
        Name = name;
        DisplayName = displayName;
    }

}