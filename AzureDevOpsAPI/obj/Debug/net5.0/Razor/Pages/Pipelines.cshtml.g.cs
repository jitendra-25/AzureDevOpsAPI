#pragma checksum "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a3235bc5cdb5e19232c0fd7cb3f83bbb810969d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AzureDevOpsAPI.Pages.Pages_Pipelines), @"mvc.1.0.razor-page", @"/Pages/Pipelines.cshtml")]
namespace AzureDevOpsAPI.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\_ViewImports.cshtml"
using AzureDevOpsAPI;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3235bc5cdb5e19232c0fd7cb3f83bbb810969d4", @"/Pages/Pipelines.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0de4e27b2922e48092248b9b24b6fe0e52136856", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Pipelines : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"container row p-3 mt-5\">\r\n    <table class=\"table table-bordered\">\r\n        <thead class=\"thead-light\">\r\n            <tr>\r\n                <th>Pipeline Name</th>\r\n                <th>Pipeline URL</th>\r\n            </tr>\r\n        </thead>\r\n");
#nullable restore
#line 12 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml"
         foreach (var item in Model.Pipelines)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 16 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    <a");
            BeginWriteAttribute("href", " href=", 502, "", 517, 1);
#nullable restore
#line 19 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml"
WriteAttributeValue("", 508, item.URL, 508, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\"> ");
#nullable restore
#line 19 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml"
                                                  Write(item.URL);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </a>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 22 "D:\Workspace\_DotNetWork\AzureDevOpsAPI\AzureDevOpsAPI\Pages\Pipelines.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AzureDevOpsAPI.Pages.PipelinesModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<AzureDevOpsAPI.Pages.PipelinesModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<AzureDevOpsAPI.Pages.PipelinesModel>)PageContext?.ViewData;
        public AzureDevOpsAPI.Pages.PipelinesModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
