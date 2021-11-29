using Microsoft.AspNetCore.Components;
using SitelCommon;

namespace SitelERP.Components
{
    public partial class RowHourWork
    {
        [Parameter]
        public SitelHour Data { get; set; }

        public RowHourWork()
        {
        }
    }
}
