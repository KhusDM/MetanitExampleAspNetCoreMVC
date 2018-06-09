using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Controllers
{
    //[SimpleActionFilter]
    [RequireHttps]
    public class FiltersController : Controller
    {
        //[SimpleActionFilter]
        //[SimpleResourceFilter(30, "Hello")]
        //[ServiceFilter(typeof(SimpleResourceFilter))]
        //[TypeFilter(typeof(SimpleResourceFilter))]
        //[IEFilter]
        //[Whitespace]
        [TimeExecutionFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}