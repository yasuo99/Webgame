#pragma checksum "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c7ed52ff4637aa222432698b7d3fea3032fb94fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Customer_Views_Cart_Checkout), @"mvc.1.0.view", @"/Areas/Customer/Views/Cart/Checkout.cshtml")]
namespace AspNetCore
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
#line 1 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\_ViewImports.cshtml"
using DichVuGame;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\_ViewImports.cshtml"
using DichVuGame.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7ed52ff4637aa222432698b7d3fea3032fb94fa", @"/Areas/Customer/Views/Cart/Checkout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"983c4a7f1991df620ced787059c0b171deaefb73", @"/Areas/Customer/_ViewImports.cshtml")]
    public class Areas_Customer_Views_Cart_Checkout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DichVuGame.Models.ViewModels.SuperCartViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Customer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "HOme", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
  
    ViewData["Title"] = "Checkout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""container"">
    <div class=""row"">
        <div class=""col-sm-12 col-md-10 col-md-offset-1"">
            <table class=""table table-hover"">
                <thead>
                    <tr>
                        <th>Sản phẩm</th>
                        <th>Số lượng</th>
                        <th class=""text-center"">Giá</th>
                        <th class=""text-center"">Tổng</th>
                        <th> </th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 21 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                       var subtotal = 0.0;
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                         if (Model.CartVM1.Count > 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <tr>
                                <td>  Mua game    </td>
                                <td></td>
                                <td>   </td>
                                <td></td>
                                <td></td>
                            </tr>
");
#nullable restore
#line 32 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                             foreach (var product in Model.CartVM1)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <tr>
                                    <td class=""col-sm-8 col-md-6"">
                                        <div class=""media"">
                                            <a class=""thumbnail pull-left"" href=""#""> <img class=""media-object""");
            BeginWriteAttribute("src", " src=\"", 1432, "\"", 1462, 1);
#nullable restore
#line 37 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
WriteAttributeValue("", 1438, product.Game.GamePoster, 1438, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width: 80px; height: 80px;\"> </a>\r\n                                            <div class=\"media-body\">\r\n                                                <h4 class=\"media-heading\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7ed52ff4637aa222432698b7d3fea3032fb94fa7991", async() => {
                WriteLiteral(" Tên sản phẩm");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 39 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                                                                              WriteLiteral(product.Game.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h4>\r\n                                                <h5 class=\"media-heading\"> by");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7ed52ff4637aa222432698b7d3fea3032fb94fa10781", async() => {
                WriteLiteral(" ");
#nullable restore
#line 40 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                                                      Write(product.Studio.Studioname);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</h5>
                                                <span>Tình trạng: </span><span class=""text-success""><strong>Còn hàng</strong></span>
                                            </div>
                                        </div>
                                    </td>
                                    <td class=""col-sm-1 col-md-1"" style=""text-align: center"">
                                        <p>");
#nullable restore
#line 46 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                      Write(product.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    </td>\r\n                                    <td class=\"col-sm-1 col-md-1 text-center\"><strong>");
#nullable restore
#line 48 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                 Write(product.Game.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></td>\r\n");
#nullable restore
#line 49 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                       var total = product.Game.Price * product.Amount;
                                        subtotal += total;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"col-sm-1 col-md-1 text-center\"><strong>");
#nullable restore
#line 51 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                 Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </strong></td>\r\n                                    <td></td>\r\n                                </tr>\r\n");
#nullable restore
#line 54 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                         if (Model.CartVM2.Count > 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <tr>
                                <td>  Thuê tài khoản game </td>
                                <td>   </td>
                                <td>   </td>
                                <td></td>
                                <td></td>
                            </tr>
");
#nullable restore
#line 65 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                             foreach (var product in Model.CartVM2)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <tr>
                                    <td class=""col-sm-8 col-md-6"">
                                        <div class=""media"">
                                            <a class=""thumbnail pull-left"" href=""#""> <img class=""media-object""");
            BeginWriteAttribute("src", " src=\"", 3706, "\"", 3736, 1);
#nullable restore
#line 70 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
WriteAttributeValue("", 3712, product.Game.GamePoster, 3712, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width: 80px; height: 80px;\"> </a>\r\n                                            <div class=\"media-body\">\r\n                                                <h4 class=\"media-heading\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7ed52ff4637aa222432698b7d3fea3032fb94fa16893", async() => {
                WriteLiteral(" Tên sản phẩm");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 72 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                                                                              WriteLiteral(product.Game.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h4>\r\n                                                <h5 class=\"media-heading\"> by");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7ed52ff4637aa222432698b7d3fea3032fb94fa19684", async() => {
                WriteLiteral(" ");
#nullable restore
#line 73 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                                                      Write(product.Studio.Studioname);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</h5>
                                                <span>Tình trạng: </span><span class=""text-success""><strong>Còn hàng</strong></span>
                                            </div>
                                        </div>
                                    </td>
                                    <td class=""col-sm-1 col-md-1"" style=""text-align: center"">
                                        <p>");
#nullable restore
#line 79 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                      Write(product.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    </td>\r\n                                    <td class=\"col-sm-1 col-md-1 text-center\"><strong>");
#nullable restore
#line 81 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                 Write(product.Game.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></td>\r\n");
#nullable restore
#line 82 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                       var total = product.Game.Price * product.Amount;
                                        subtotal += total;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"col-sm-1 col-md-1 text-center\"><strong>");
#nullable restore
#line 84 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                                                 Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </strong></td>\r\n                                    <td></td>\r\n                                </tr>\r\n");
#nullable restore
#line 87 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 87 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <tr>
                            <td>  </td>
                            <td>  </td>
                            <td>  </td>
                            <td><h5>Tổng hóa đơn</h5></td>
                            <td class=""text-right""><h5><strong>");
#nullable restore
#line 94 "D:\Must save\WebGame_TMDT-20200701T023531Z-001\WebGame_TMDT\DichVuGame\Areas\Customer\Views\Cart\Checkout.cshtml"
                                                          Write(Model.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong></h5></td>
                        </tr>
                        <tr>
                            <td>   </td>
                            <td>   </td>
                            <td>   </td>
                            <td><h5>Kết quả</h5></td>
                            <td class=""text-right""><h5><strong>Thanh toán thành công</strong></h5></td>
                        </tr>
                        <tr>
                            <td>   </td>
                            <td>   </td>
                            <td>   </td>
                            <td>   </td>
                            <td>
                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7ed52ff4637aa222432698b7d3fea3032fb94fa25240", async() => {
                WriteLiteral("\r\n                                    <span class=\"glyphicon glyphicon-shopping-cart\"></span> Tiếp tục mua hàng\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DichVuGame.Models.ViewModels.SuperCartViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
