using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MetanitExampleCoreMVC.TagHelpers
{
    //public class SocialsTagHelper : TagHelper
    //{
    //    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        output.TagName = "div";
    //        // получаем вложенный контекст из дочерних tag-хелперов
    //        var target = await output.GetChildContentAsync();
    //        var content = "<h3>Социальные сети</h3>" + target.GetContent();
    //        output.Content.SetHtmlContent(content);
    //    }
    //}

    //public class VKTagHelper : TagHelper
    //{
    //    private const string address = "https://vk.com/metanit";

    //    public string Group { get; set; }
    //    public bool Condition { get; set; }
    //    public LinkInfo Info { get; set; }

    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        if (!Condition)
    //        {
    //            output.SuppressOutput();
    //        }
    //        else
    //        {
    //            output.TagName = "a";    // заменяет тег <vk> тегом <a> // присваивает атрибуту href значение из address
    //            output.Attributes.SetAttribute("href", address);
    //            output.Content.SetContent("Группа в ВК");
    //            output.TagMode = TagMode.StartTagAndEndTag;
    //            output.PreElement.SetHtmlContent("<h3>Социальные сети</h3>");
    //            output.PostElement.SetHtmlContent("<p>Элемент после ссылки</p>");
    //            output.Attributes.SetAttribute("href", address + Group);

    //            string style = $"color:{Info.Color};font-size:{Info.FontSize}px;font-family:{Info.FontFamily};";
    //            output.Attributes.SetAttribute("href", address);
    //            output.Attributes.SetAttribute("style", style);
    //        }
    //    }
    //}

    //public class LinkInfo
    //{
    //    public string Color { get; set; }
    //    public int FontSize { get; set; }
    //    public string FontFamily { get; set; }
    //}
    public class VkTagHelper : TagHelper
    {
        private const string domain = "https://vk.com/id";
        IHostingEnvironment environment;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        // получаем сервис IHostingEnvironment
        public VkTagHelper(IHostingEnvironment env)
        {
            environment = env;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // получаем из параметров маршрута id
            string id = ViewContext?.RouteData?.Values["id"]?.ToString();
            if (String.IsNullOrEmpty(id)) id = "1";
            output.TagName = "a";
            output.Attributes.SetAttribute("href", domain + id);
            if (environment.IsDevelopment())
                output.Attributes.SetAttribute("style", "color:red;");
        }
    }
}
