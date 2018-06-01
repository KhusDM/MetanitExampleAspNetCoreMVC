using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MetanitExampleCoreMVC.TagHelpers
{
    [HtmlTargetElement("form-header", ParentTag = "form", Attributes = "form-title")]
    //[HtmlTargetElement(ParentTag = "form")]
    //[HtmlTargetElement("article-header")]
    //[HtmlTargetElement(Attributes = "header, divtitle")]
    public class HeaderTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h2";
            output.Attributes.RemoveAll("header");
        }
    }
}
