using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        [Route("[area]/[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}