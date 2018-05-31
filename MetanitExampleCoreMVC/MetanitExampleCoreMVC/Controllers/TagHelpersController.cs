using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MetanitExampleCoreMVC.Controllers
{
    public class TagHelpersController : Controller
    {
        IEnumerable<Company> companies = new List<Company>()
        {
            new Company { Id = 1, Name = "Apple" },
            new Company { Id = 2, Name = "Samsung" },
            new Company { Id=3, Name="Microsoft" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DayTimeViewModel model)
        {
            return Content(model.Period.ToString());
        }

        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        public string Create(Phone phone)
        {
            Company company = companies.FirstOrDefault(c => c.Id == phone.CompanyId);
            return $"Добавлен новый элемент: {phone.Name} ({company?.Name})";
        }

        public IActionResult PhoneRequired()
        {
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        public string PhoneRequired(PhoneRequired phoneRequired)
        {
            return $"{phoneRequired.Name}";
        }
    }
}