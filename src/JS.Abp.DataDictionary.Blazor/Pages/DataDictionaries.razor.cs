using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.Permissions;
using JS.Abp.DataDictionary.Shared;

namespace JS.Abp.DataDictionary.Blazor.Pages
{
    public partial class DataDictionaries
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<DataDictionaryDto> DataDictionaryList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateDataDictionary { get; set; }
        private bool CanEditDataDictionary { get; set; }
        private bool CanDeleteDataDictionary { get; set; }
        private DataDictionaryCreateDto NewDataDictionary { get; set; }
        private Validations NewDataDictionaryValidations { get; set; }
        private DataDictionaryUpdateDto EditingDataDictionary { get; set; }
        private Validations EditingDataDictionaryValidations { get; set; }
        private Guid EditingDataDictionaryId { get; set; }
        private Modal CreateDataDictionaryModal { get; set; }
        private Modal EditDataDictionaryModal { get; set; }
        private GetDataDictionariesInput Filter { get; set; }
        private DataGridEntityActionsColumn<DataDictionaryDto> EntityActionsColumn { get; set; }
        protected string SelectedCreateTab = "dataDictionary-create-tab";
        protected string SelectedEditTab = "dataDictionary-edit-tab";
        
        public DataDictionaries()
        {
            NewDataDictionary = new DataDictionaryCreateDto();
            EditingDataDictionary = new DataDictionaryUpdateDto();
            Filter = new GetDataDictionariesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:DataDictionaries"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewDataDictionary"], async () =>
            {
                await OpenCreateDataDictionaryModalAsync();
            }, IconName.Add, requiredPolicyName: DataDictionaryPermissions.DataDictionaries.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateDataDictionary = await AuthorizationService
                .IsGrantedAsync(DataDictionaryPermissions.DataDictionaries.Create);
            CanEditDataDictionary = await AuthorizationService
                            .IsGrantedAsync(DataDictionaryPermissions.DataDictionaries.Edit);
            CanDeleteDataDictionary = await AuthorizationService
                            .IsGrantedAsync(DataDictionaryPermissions.DataDictionaries.Delete);
        }

        private async Task GetDataDictionariesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await DataDictionariesAppService.GetListAsync(Filter);
            DataDictionaryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetDataDictionariesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await DataDictionariesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("DataDictionary") ??
                                await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/data-dictionary/data-dictionaries/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<DataDictionaryDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetDataDictionariesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateDataDictionaryModalAsync()
        {
            NewDataDictionary = new DataDictionaryCreateDto{
                
                IsActive = true,
            };
            await NewDataDictionaryValidations.ClearAll();
            await CreateDataDictionaryModal.Show();
        }

        private async Task CloseCreateDataDictionaryModalAsync()
        {
            NewDataDictionary = new DataDictionaryCreateDto{
                
                
            };
            await CreateDataDictionaryModal.Hide();
        }

        private async Task OpenEditDataDictionaryModalAsync(DataDictionaryDto input)
        {
            var dataDictionary = await DataDictionariesAppService.GetAsync(input.Id);
            
            EditingDataDictionaryId = dataDictionary.Id;
            EditingDataDictionary = ObjectMapper.Map<DataDictionaryDto, DataDictionaryUpdateDto>(dataDictionary);
            await EditingDataDictionaryValidations.ClearAll();
            await EditDataDictionaryModal.Show();
        }

        private async Task OpenEditDataDictionaryDetailModalAsync(DataDictionaryDto input)
        {
            NavigationManager.NavigateTo($"/data-dictionaries-details/{input.Id}");
        }

        private async Task DeleteDataDictionaryAsync(DataDictionaryDto input)
        {
            await DataDictionariesAppService.DeleteAsync(input.Id);
            await GetDataDictionariesAsync();
        }

        private async Task CreateDataDictionaryAsync()
        {
            try
            {
                if (await NewDataDictionaryValidations.ValidateAll() == false)
                {
                    return;
                }

                await DataDictionariesAppService.CreateAsync(NewDataDictionary);
                await GetDataDictionariesAsync();
                await CloseCreateDataDictionaryModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditDataDictionaryModalAsync()
        {
            await EditDataDictionaryModal.Hide();
        }

        private async Task UpdateDataDictionaryAsync()
        {
            try
            {
                if (await EditingDataDictionaryValidations.ValidateAll() == false)
                {
                    return;
                }

                await DataDictionariesAppService.UpdateAsync(EditingDataDictionaryId, EditingDataDictionary);
                await GetDataDictionariesAsync();
                await EditDataDictionaryModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

    }
}
