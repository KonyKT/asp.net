using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication7.TagHelpers
{
    [HtmlTargetElement("ColorHelper")]
    public class Helper : TagHelper
    {


            public string Text { get; set; }


            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                output.TagName = "b";
                output.TagName = "font";
                output.Attributes.SetAttribute("color", "red");
                output.Attributes.SetAttribute("size", 10);
                output.Attributes.SetAttribute("face", "impact");
                output.Content.AppendHtml($"{Text}</br>");

            }
    }
}

