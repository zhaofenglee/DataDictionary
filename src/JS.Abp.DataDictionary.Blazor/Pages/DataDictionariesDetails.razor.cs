using AutoMapper.Internal.Mappers;
using Blazorise.DataGrid;
using Blazorise;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlazoriseUI.Components;
using JS.Abp.DataDictionary.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace JS.Abp.DataDictionary.Blazor.Pages
{
    public partial class DataDictionariesDetails
    {
        [Parameter] public Guid DetailId { get; set; }
        private DataDictionaryUpdateDto EditingDataDictionary { get; set; }
        private IReadOnlyList<DataDictionaryItemWithNavigationPropertiesDto> DataDictionaryItemList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private DataDictionaryItemCreateDto NewDataDictionaryItem { get; set; }
        private Validations NewDataDictionaryItemValidations { get; set; }
        private DataDictionaryItemUpdateDto EditingDataDictionaryItem { get; set; }
        private Validations EditingDataDictionaryItemValidations { get; set; }
        private Guid EditingDataDictionaryItemId { get; set; }
        private bool CanCreateDataDictionary { get; set; }
        private bool CanEditDataDictionary { get; set; }
        private bool CanDeleteDataDictionary { get; set; }
        private GetDataDictionaryItemsInput Filter { get; set; }
        private DataGridEntityActionsColumn<DataDictionaryItemWithNavigationPropertiesDto> EntityActionsColumn { get; set; }
        private Modal CreateDataDictionaryItemModal { get; set; }
        private Modal EditDataDictionaryItemModal { get; set; }

        public DataDictionariesDetails()
        {
            NewDataDictionaryItem = new DataDictionaryItemCreateDto();
            EditingDataDictionaryItem = new DataDictionaryItemUpdateDto();
            Filter = new GetDataDictionaryItemsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetModelInfoAsync();
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
        private async Task OpenCreateDataDictionaryItemModalAsync()
        {
            NewDataDictionaryItem = new DataDictionaryItemCreateDto
            {
                Sequence = 1,
                DataDictionaryId = DetailId,
                IsStatic = true,
            };
            await NewDataDictionaryItemValidations.ClearAll();
            await CreateDataDictionaryItemModal.Show();
        }
        private async Task GetModelInfoAsync()
        {
            var modelInfo = await DataDictionariesAppService.GetAsync(DetailId);
            EditingDataDictionary = ObjectMapper.Map<DataDictionaryDto, DataDictionaryUpdateDto>(modelInfo);
        }
        private async Task GetModelDetailAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;
            Filter.DataDictionaryId = DetailId;

            var result = await DataDictionaryItemsAppService.GetListAsync(Filter);
            DataDictionaryItemList = result.Items;
            TotalCount = (int)result.TotalCount;

        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<DataDictionaryItemWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetModelDetailAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DeleteDataDictionaryItemAsync(DataDictionaryItemWithNavigationPropertiesDto input)
        {
            await DataDictionaryItemsAppService.DeleteAsync(input.DataDictionaryItem.Id);
            await GetModelDetailAsync();
        }
        private async Task OpenEditDataDictionaryItemModalAsync(DataDictionaryItemWithNavigationPropertiesDto input)
        {
            var dataDictionaryItem = await DataDictionaryItemsAppService.GetAsync(input.DataDictionaryItem.Id);

            EditingDataDictionaryItemId = dataDictionaryItem.Id;
            EditingDataDictionaryItem = ObjectMapper.Map<DataDictionaryItemDto, DataDictionaryItemUpdateDto>(dataDictionaryItem);
            await EditingDataDictionaryItemValidations.ClearAll();
            await EditDataDictionaryItemModal.Show();
        }

        private async Task CloseCreateDataDictionaryItemModalAsync()
        {
            NewDataDictionaryItem = new DataDictionaryItemCreateDto
            {


            };
            await CreateDataDictionaryItemModal.Hide();
        }
        private async Task CloseEditDataDictionaryItemModalAsync()
        {
            await EditDataDictionaryItemModal.Hide();
        }
        private async Task CreateDataDictionaryItemAsync()
        {
            try
            {
                if (await NewDataDictionaryItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await DataDictionaryItemsAppService.CreateAsync(NewDataDictionaryItem);
                await GetModelDetailAsync();
                await CloseCreateDataDictionaryItemModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        private async Task UpdateDataDictionaryItemAsync()
        {
            try
            {
                if (await EditingDataDictionaryItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await DataDictionaryItemsAppService.UpdateAsync(EditingDataDictionaryItemId, EditingDataDictionaryItem);
                await GetModelDetailAsync();
                await EditDataDictionaryItemModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
