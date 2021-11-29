using ExcelManagement;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using SitelCommon;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SitelERP.Pages
{
    public partial class HoursPage
    {
        IEnumerable<SitelHour> validations;
        int itemsCount = 0;
        int SelecteYear = 2020;
        bool ShowData = false;
        bool isLoading = false;
        public ElementReference _list;
        bool HasNotData = false;
        string SelectedCollaborator;
        List<string> Collaborators;

        public HoursPage()
        {
            Collaborators = new List<string>();
        }

        public async Task ShowDataByYear()
        {
            isLoading = true;
            HasNotData = false;
            ShowData = false;
            itemsCount = 0;
            validations = null;
            await Task.Run(GetAllHoursAsync);
            if (itemsCount > 0)
            {
                ShowData = true;
            }
            else
            {
                HasNotData = true;
            }
            isLoading = false;
        }

        async Task GetAllHoursAsync()
        {
            switch (SelecteYear)
            {
                case 2020:
                    validations = await HoursDedication.ReadValidationsAsync(HoursDedication.PathWorkbook2020, true, 0, 8);
                    break;
                case 2021:
                    validations = await HoursDedication.ReadValidationsAsync(HoursDedication.PathWorkbook2021, true, 0, 8);
                    break;
                default:
                    validations = null;
                    break;
            }
            itemsCount = validations?.ToList().Count ?? 0;
            if (itemsCount > 0)
            {
                Collaborators = validations.Select(v => v.Collaborator).Distinct().ToList();
            }
            else
            {
                Collaborators.Clear();
            }
        }

        public Task<ReadOnlyCollection<SitelHour>> GetHoursAsync(int startIndex, int count)
        {
            var result = new List<SitelHour>();
            if (validations != null)
            {
                if ((startIndex + count) > itemsCount)
                {
                    count = itemsCount - startIndex;
                    if (count < 0)
                    {
                        count = 0;
                    }
                }
                if (count > 0)
                {
                    result.AddRange(validations.ToList().GetRange(startIndex, count));
                }
            }
            return Task.FromResult(result.AsReadOnly());
        }

        private async ValueTask<ItemsProviderResult<SitelHour>> LoadAsync(ItemsProviderRequest request)
        {
            if (validations == null)
            {
                await GetAllHoursAsync();
            }
            var result = await GetHoursAsync(request.StartIndex, request.Count);
            return new ItemsProviderResult<SitelHour>(result, itemsCount);
        }
    }
}
