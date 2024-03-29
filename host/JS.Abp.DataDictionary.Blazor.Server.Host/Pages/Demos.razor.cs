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
using JS.Abp.DataDictionary.Blazor.Server.Host.Demos;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Pages
{
    public partial class Demos
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<DemoDto> DemoList { get; set; }

        private IReadOnlyList<DataDictionaryItemDto> DataDictionariesList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateDemo { get; set; }
        private bool CanEditDemo { get; set; }
        private bool CanDeleteDemo { get; set; }
        private DemoCreateDto NewDemo { get; set; }
        private Validations NewDemoValidations { get; set; } = new();
        private DemoUpdateDto EditingDemo { get; set; }
        private Validations EditingDemoValidations { get; set; } = new();
        private Guid EditingDemoId { get; set; }
        private Modal CreateDemoModal { get; set; } = new();
        private Modal EditDemoModal { get; set; } = new();
        private GetDemosInput Filter { get; set; }
        private DataGridEntityActionsColumn<DemoDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "demo-create-tab";
        protected string SelectedEditTab = "demo-edit-tab";

        public Demos()
        {
            NewDemo = new DemoCreateDto();
            EditingDemo = new DemoUpdateDto();
            Filter = new GetDemosInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            DemoList = new List<DemoDto>();
            DataDictionariesList = new List<DataDictionaryItemDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            var dataDictionariesList = await DataDictionariesAppService.FindByCodeAsync("DemoType");
            DataDictionariesList = dataDictionariesList.Items;
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Demos"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {


            Toolbar.AddButton(L["NewDemo"], async () =>
            {
                await OpenCreateDemoModalAsync();
            }, IconName.Add);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateDemo = true;
            CanEditDemo = true;
            CanDeleteDemo = true;
        }

        private async Task GetDemosAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await DemosAppService.GetListAsync(Filter);
            DemoList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetDemosAsync();
            await InvokeAsync(StateHasChanged);
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<DemoDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetDemosAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateDemoModalAsync()
        {
            NewDemo = new DemoCreateDto
            {


            };
            await NewDemoValidations.ClearAll();
            await CreateDemoModal.Show();
        }

        private async Task CloseCreateDemoModalAsync()
        {
            NewDemo = new DemoCreateDto
            {


            };
            await CreateDemoModal.Hide();
        }

        private async Task OpenEditDemoModalAsync(DemoDto input)
        {
            var demo = await DemosAppService.GetAsync(input.Id);
            EditingDemoId = demo.Id;
            EditingDemo = ObjectMapper.Map<DemoDto, DemoUpdateDto>(demo);
            await EditingDemoValidations.ClearAll();
            await EditDemoModal.Show();
        }

        private async Task DeleteDemoAsync(DemoDto input)
        {
            await DemosAppService.DeleteAsync(input.Id);
            await GetDemosAsync();
        }

        private async Task CreateDemoAsync()
        {
            try
            {
                if (await NewDemoValidations.ValidateAll() == false)
                {
                    return;
                }

                await DemosAppService.CreateAsync(NewDemo);
                await GetDemosAsync();
                await CloseCreateDemoModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditDemoModalAsync()
        {
            await EditDemoModal.Hide();
        }

        private async Task UpdateDemoAsync()
        {
            try
            {
                if (await EditingDemoValidations.ValidateAll() == false)
                {
                    return;
                }

                await DemosAppService.UpdateAsync(EditingDemoId, EditingDemo);
                await GetDemosAsync();
                await EditDemoModal.Hide();
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
