using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MetanitExampleCoreMVC.TagHelpers
{
    public class BookInfoTagHelper : TagHelper
    {
        public BookViewModel Book { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            string bookInfoContent = $@"<p>Название: <b>{Book.Title}</b></p>
                                    <p>Автор: <b>{Book.Author}</b></p>
                                    <p>Год издания: <b>{Book.Year}</b></p>";

            output.Content.SetHtmlContent(bookInfoContent);
        }
    }
}
