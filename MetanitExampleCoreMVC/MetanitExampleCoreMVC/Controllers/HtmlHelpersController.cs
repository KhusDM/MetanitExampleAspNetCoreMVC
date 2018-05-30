using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MetanitExampleCoreMVC.Controllers
{
    public enum TimeOfDay
    {
        [Display(Name = "Утро")]
        Morning,
        [Display(Name = "День")]
        Afternoon,
        [Display(Name = "Вечер")]
        Evening,
        [Display(Name = "Ночь")]
        Night
    }

    public class HtmlHelpersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            List<Phone> phones = new List<Phone>
            {
                new Phone {Id=1, Name="iPhone 7 Pro", Price=680 },
                new Phone {Id=2, Name="Galaxy 7 Edge", Price=640 },
                new Phone {Id=3, Name="HTC 10", Price=500 },
                new Phone {Id=4, Name="Honor 5X", Price=400 },
            };
            ViewBag.Phones = new SelectList(phones, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Phone3 phone)
        {
            return Content(phone.Name);
        }

        public IActionResult Display()
        {
            Phone phone = new Phone() { Id = 1, Name = "Nexus 6P", Price = 49000 };

            return View(phone);
        }

        [HttpPost]
        public IActionResult Display(Phone phone)
        {
            return Content($"{phone.Name} - {phone.Price}");
        }
    }
}