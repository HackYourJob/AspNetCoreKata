using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HYJ.Formation.AspNetCore
{
    [HtmlTargetElement("a", Attributes = ActionAttributeName)]
    [HtmlTargetElement("a", Attributes = ControllerAttributeName)]
    [HtmlTargetElement("a", Attributes = AreaAttributeName)]
    [HtmlTargetElement("a", Attributes = PageAttributeName)]
    [HtmlTargetElement("a", Attributes = PageHandlerAttributeName)]
    [HtmlTargetElement("a", Attributes = FragmentAttributeName)]
    [HtmlTargetElement("a", Attributes = HostAttributeName)]
    [HtmlTargetElement("a", Attributes = ProtocolAttributeName)]
    [HtmlTargetElement("a", Attributes = RouteAttributeName)]
    [HtmlTargetElement("a", Attributes = RouteValuesDictionaryName)]
    [HtmlTargetElement("a", Attributes = RouteValuesPrefix + "*")]
    public class CultureAnchorTagHelper : AnchorTagHelper
    {
        public CultureAnchorTagHelper(IHtmlGenerator generator) 
            : base(generator)
        {
        }
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string AreaAttributeName = "asp-area";
        private const string PageAttributeName = "asp-page";
        private const string PageHandlerAttributeName = "asp-page-handler";
        private const string FragmentAttributeName = "asp-fragment";
        private const string HostAttributeName = "asp-host";
        private const string ProtocolAttributeName = "asp-protocol";
        private const string RouteAttributeName = "asp-route";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var culture = (string)ViewContext.RouteData.Values["culture"];

            if (culture != null && !context.AllAttributes.ContainsName("asp-route-culture"))
            {
                RouteValues["culture"] = culture;
            }

            base.Process(context, output);
        }
    }
}