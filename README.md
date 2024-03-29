# 数据字典

## 准备工作

### 1.安装 NuGet packages.
  * JS.Abp.DataDictionary.Application
  * JS.Abp.DataDictionary.Application.Contracts
  * JS.Abp.DataDictionary.Domain
  * JS.Abp.DataDictionary.Domain.Shared
  * JS.Abp.DataDictionary.EntityFrameworkCore
  * JS.Abp.DataDictionary.HttpApi
  * JS.Abp.DataDictionary.HttpApi.Client
  
### 2.添加“依赖”属性以配置模块
 * [DependsOn(typeof(DataDictionaryApplicationModule))]
 * [DependsOn(typeof(DataDictionaryApplicationContractsModule))]
 * [DependsOn(typeof(DataDictionaryDomainModule))]
 * [DependsOn(typeof(DataDictionaryDomainSharedModule))]
 * [DependsOn(typeof(DataDictionaryEntityFrameworkCoreModule))]
 * [DependsOn(typeof(DataDictionaryHttpApiModule))]
 * [DependsOn(typeof(DataDictionaryHttpApiClientModule))]
 * [DependsOn(typeof(DataDictionaryBlazorModule))]
 * [DependsOn(typeof(DataDictionaryBlazorServerModule))]
 * [DependsOn(typeof(DataDictionaryBlazorWebAssemblyModule))]
### 3. 在你的Dbcontext添加 ` builder.ConfigureDataDictionary();`

### 4. 添加 EF Core 迁移并更新数据库.
在 YourProject.EntityFrameworkCore 项目的目录中打开命令行终端，然后键入以下命令:

````bash
> dotnet ef migrations add Added_DataDictionary
````
````bash
> dotnet ef database update
````

## 如何使用
* 以下是以Demo方案为例

### 1.需要先维护数据字典（目前只有Blazor支持前端维护）
* 字典代码是需要唯一，不可重复，是根据字典代码获取子项

![20230507115711](/docs/images/20230507115711.png           )

### 2.维护完成后需要在需要使用数据字典的服务引入服务“IDataDictionaryStore”或者使用IDataDictionariesAppService
````csharp
DataDictionariesList = (await DataDictionariesAppService.FindByCodeAsync("DemoType")).Items;
````

## Samples

See the [sample projects](https://github.com/zhaofenglee/DataDictionary/tree/master/host/JS.Abp.DataDictionary.Blazor.Host)
