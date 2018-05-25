using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace MetanitExampleCoreMVC.Components
{
    public class Header : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string htmlContent = String.Empty;
            using (FileStream fileStream = new FileStream("Files/header.html", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    htmlContent = await reader.ReadToEndAsync();
                }
            }

            return new HtmlContentViewComponentResult(new HtmlString(htmlContent));
        }
    }
}
