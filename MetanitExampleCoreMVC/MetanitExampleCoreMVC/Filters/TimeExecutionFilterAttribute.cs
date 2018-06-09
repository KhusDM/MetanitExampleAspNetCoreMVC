using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MetanitExampleCoreMVC.Filters
{
    public class TimeExecutionFilterAttribute : Attribute, IResultFilter
    {
        DateTime start;

        public async void OnResultExecuted(ResultExecutedContext context)
        {
            DateTime end = DateTime.Now;
            double processTime = end.Subtract(start).TotalMilliseconds;

            await context.HttpContext.Response.WriteAsync($"Время выполнения результата: {processTime} миллисекунд");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            start = DateTime.Now;
        }
    }
}
