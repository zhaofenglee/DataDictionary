# DataDictionary

## Getting Started

### 1.Install the following NuGet packages.
  * JS.Abp.DataDictionary.Application
  * JS.Abp.DataDictionary.Application.Contracts
  * JS.Abp.DataDictionary.Domain
  * JS.Abp.DataDictionary.Domain.Shared
  * JS.Abp.DataDictionary.EntityFrameworkCore
  * JS.Abp.DataDictionary.HttpApi
  * JS.Abp.DataDictionary.HttpApi.Client
  
### 2.Add `DependsOn` attribute to configure the module
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
### 3. Add ` builder.ConfigureDataDictionary();` to the `OnModelCreating()` method in **YourProjectDbContext.cs**.

### 4. Add EF Core migrations and update your database.
Open a command-line terminal in the directory of the YourProject.EntityFrameworkCore project and type the following command:

````bash
> dotnet ef migrations add Added_DataDictionary
````
````bash
> dotnet ef database update
````

## Samples

See the [sample projects](https://github.com/zhaofenglee/JS.Abp.DataDictionary/tree/master/host/JS.Abp.DataDictionary.Blazor.Host)
