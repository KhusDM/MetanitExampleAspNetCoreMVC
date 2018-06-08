using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Controllers
{
    //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class CacheController : Controller
    {
        ProductService productService;

        public CacheController(ProductService service)
        {
            productService = service;
            productService.Initialize();
        }

        [ResponseCache(CacheProfileName = "NoCaching")]
        //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                Product product = await productService.GetProduct(id.Value);

                if (product != null)
                {
                    return Content($"Product: {product.Name}");
                }
            }

            return Content("Product not found");
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult CacheView()
        {
            return View();
        }
    }
}