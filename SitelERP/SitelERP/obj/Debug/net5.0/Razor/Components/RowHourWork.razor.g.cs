#pragma checksum "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82d985bc5610380fd0360f09371e40e2b3aa8c8c"
// <auto-generated/>
#pragma warning disable 1591
namespace SitelERP.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using SitelERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using SitelERP.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using SitelERP.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\MyProjects\SitelERP\SitelERP\_Imports.razor"
using SitelCommon;

#line default
#line hidden
#nullable disable
    public partial class RowHourWork : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "tr");
            __builder.AddAttribute(1, "b-eviioso017");
            __builder.OpenElement(2, "td");
            __builder.AddAttribute(3, "b-eviioso017");
            __builder.AddContent(4, 
#nullable restore
#line 2 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.ProjectManager

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n    ");
            __builder.OpenElement(6, "td");
            __builder.AddAttribute(7, "b-eviioso017");
            __builder.AddContent(8, 
#nullable restore
#line 3 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Client

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n    ");
            __builder.OpenElement(10, "td");
            __builder.AddAttribute(11, "b-eviioso017");
            __builder.AddContent(12, 
#nullable restore
#line 4 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.System

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n    ");
            __builder.OpenElement(14, "td");
            __builder.AddAttribute(15, "b-eviioso017");
            __builder.AddContent(16, 
#nullable restore
#line 5 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.ProjectTitle

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(17, "\r\n    ");
            __builder.OpenElement(18, "td");
            __builder.AddAttribute(19, "b-eviioso017");
            __builder.AddContent(20, 
#nullable restore
#line 6 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.ProjectOF

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n    ");
            __builder.OpenElement(22, "td");
            __builder.AddAttribute(23, "b-eviioso017");
            __builder.AddContent(24, 
#nullable restore
#line 7 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.InitHours

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n    ");
            __builder.OpenElement(26, "td");
            __builder.AddAttribute(27, "b-eviioso017");
            __builder.AddContent(28, 
#nullable restore
#line 8 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Status

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n    ");
            __builder.OpenElement(30, "td");
            __builder.AddAttribute(31, "b-eviioso017");
            __builder.AddContent(32, 
#nullable restore
#line 9 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Collaborator

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n    ");
            __builder.OpenElement(34, "td");
            __builder.AddAttribute(35, "b-eviioso017");
            __builder.AddContent(36, 
#nullable restore
#line 10 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Department

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n    ");
            __builder.OpenElement(38, "td");
            __builder.AddAttribute(39, "b-eviioso017");
            __builder.AddContent(40, 
#nullable restore
#line 11 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.DayOfYear

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\r\n    ");
            __builder.OpenElement(42, "td");
            __builder.AddAttribute(43, "b-eviioso017");
            __builder.AddContent(44, 
#nullable restore
#line 12 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Week

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(45, "\r\n    ");
            __builder.OpenElement(46, "td");
            __builder.AddAttribute(47, "b-eviioso017");
            __builder.AddContent(48, 
#nullable restore
#line 13 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.DaysBetweenEndDate

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n    ");
            __builder.OpenElement(50, "td");
            __builder.AddAttribute(51, "b-eviioso017");
            __builder.AddContent(52, 
#nullable restore
#line 14 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.DeliveryDate

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n    ");
            __builder.OpenElement(54, "td");
            __builder.AddAttribute(55, "b-eviioso017");
            __builder.AddContent(56, 
#nullable restore
#line 15 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
          $"{Data.HoursDedication}:{Data.MinutesDedication}"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(57, "\r\n    ");
            __builder.OpenElement(58, "td");
            __builder.AddAttribute(59, "b-eviioso017");
            __builder.AddContent(60, 
#nullable restore
#line 16 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Task

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(61, "\r\n    ");
            __builder.OpenElement(62, "td");
            __builder.AddAttribute(63, "b-eviioso017");
            __builder.AddContent(64, 
#nullable restore
#line 17 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Description

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n    ");
            __builder.OpenElement(66, "td");
            __builder.AddAttribute(67, "b-eviioso017");
            __builder.AddContent(68, 
#nullable restore
#line 18 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.InvoiceNumber

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(69, "\r\n    ");
            __builder.OpenElement(70, "td");
            __builder.AddAttribute(71, "b-eviioso017");
            __builder.AddContent(72, 
#nullable restore
#line 19 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.IncidenceId

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(73, "\r\n    ");
            __builder.OpenElement(74, "td");
            __builder.AddAttribute(75, "b-eviioso017");
            __builder.AddContent(76, 
#nullable restore
#line 20 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Closed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(77, "\r\n    ");
            __builder.OpenElement(78, "td");
            __builder.AddAttribute(79, "b-eviioso017");
            __builder.AddContent(80, 
#nullable restore
#line 21 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Solution

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(81, "\r\n    ");
            __builder.OpenElement(82, "td");
            __builder.AddAttribute(83, "b-eviioso017");
            __builder.AddContent(84, 
#nullable restore
#line 22 "C:\MyProjects\SitelERP\SitelERP\Components\RowHourWork.razor"
         Data.Observation

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591