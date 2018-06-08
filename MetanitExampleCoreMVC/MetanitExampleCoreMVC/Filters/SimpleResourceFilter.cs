using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetanitExampleCoreMVC.Filters
{
    public class SimpleResourceFilter : Attribute, IResourceFilter
    {
        ILogger Logger { get; set; }
        int Age { get; set; }
        string Message { get; set; }

        //public SimpleResourceFilter(ILoggerFactory loggerFactory, int age, string message)
        //{
        //    Logger = loggerFactory.CreateLogger("SimpleResourceFilter");
        //    Age = age;
        //    Message = message;
        //}

        public SimpleResourceFilter(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger("SimpleResourceFilter");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Logger.LogInformation($"OnResourceExecuted - {DateTime.Now}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (Age != null && Message != null)
            {
                context.HttpContext.Response.Headers.Add("Age", Age.ToString());
                context.HttpContext.Response.Headers.Add("Message", Message);
            }

            context.Result = new ContentResult { Content = "Ресурс не найден" };
            Logger.LogInformation($"OnResourceExecuting - {DateTime.Now}");
        }
    }
}
