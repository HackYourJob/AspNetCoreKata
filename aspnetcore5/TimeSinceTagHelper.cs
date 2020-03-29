using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HYJ.Formation.AspNetCore
{
    [HtmlTargetElement("time-since", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TimeSinceTagHelper : TagHelper
    {
        public DateTime At { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "time";
            output.Attributes.SetAttribute("datetime", At.ToString("O"));
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetContent((DateTime.Now - At).ToString());
        }
    }
}