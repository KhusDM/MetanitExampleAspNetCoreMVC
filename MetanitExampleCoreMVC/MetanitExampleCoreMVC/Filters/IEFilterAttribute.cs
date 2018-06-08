using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Filters
{
    public class IEFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            string userAgent = "MSIE [6-9]";

            if (Regex.IsMatch(userAgent, "MSIE [6-9]"))
            {
                context.Result = new ContentResult { Content = "Ваш браузер устарел" };
            }
        }
    }
}
