#pragma checksum "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d70aeed6d35185870ef74d3e1da4a5fd63dc35ff"
// <auto-generated/>
#pragma warning disable 1591
namespace WebApplication2.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using WebApplication2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\_Imports.razor"
using WebApplication2.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\Pages\Index.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(0);
            __builder.AddAttribute(1, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 4 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\Pages\Index.razor"
                 this

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputCheckbox>(3);
                __builder2.AddAttribute(4, "style", "display:inline");
                __builder2.AddAttribute(5, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 5 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\Pages\Index.razor"
                                Value

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(6, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => Value = __value, Value))));
                __builder2.AddAttribute(7, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.Boolean>>>(() => Value));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 8 "C:\Users\apolenyaka\source\repos\WebApplication2\WebApplication2\Pages\Index.razor"
      
    private bool _value = false;

    [Parameter]
    public bool Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            ValueChanged.InvokeAsync(_value);
        }
    }
    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
