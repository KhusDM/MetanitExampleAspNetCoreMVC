using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.ViewModels;

namespace MetanitExampleCoreMVC.Controllers
{
    public class ModelController : Controller
    {
        IEnumerable<Company> companies;
        IEnumerable<Phone3> phones;
        static List<Event> events;

        public ModelController()
        {
            Company apple = new Company { Id = 1, Name = "Apple", Country = "USA" };
            Company microsoft = new Company { Id = 2, Name = "Microsoft", Country = "USA" };
            Company google = new Company { Id = 3, Name = "Google", Country = "USA" };

            companies = new List<Company>() { apple, microsoft, google };
            phones = new List<Phone3>
            {
                new Phone3 { Id=1, Manufacturer= apple, Name="iPhone 6S", Price=56000 },
                new Phone3 { Id=2, Manufacturer= apple, Name="iPhone 5S", Price=41000 },
                new Phone3 { Id=3, Manufacturer= microsoft, Name="Lumia 550", Price=9000 },
                new Phone3 { Id=4, Manufacturer= microsoft, Name="Lumia 950", Price=40000 },
                new Phone3 { Id=5, Manufacturer= google, Name="Nexus 5X", Price=30000 },
                new Phone3 { Id=6, Manufacturer= google, Name="Nexus 6P", Price=50000 }
            };

            events = events ?? new List<Event>();
        }

        public IActionResult Index2()
        {
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event ev)
        {
            //ev.Id = Guid.NewGuid().ToString();
            events.Add(ev);
            return RedirectToAction("Index2");
        }

        public IActionResult Index(int? companyId)
        {
            IEnumerable<CompanyModel> companyModels = companies.Select(c => new CompanyModel { Id = c.Id, Name = c.Name }).ToList();

            ((List<CompanyModel>)companyModels).Insert(0, new CompanyModel { Id = 0, Name = "All" });

            IndexViewModel ivm = new IndexViewModel { Companies = companyModels, Phones = phones };

            if (companyId != null && companyId > 0)
            {
                ivm.Phones = phones.Where(p => p.Manufacturer.Id == companyId);
            }

            return View(ivm);
        }

        //[HttpGet]
        //public IActionResult GetData(string[] items)
        //{
        //    string result = "";
        //    foreach (var item in items)
        //    {
        //        result += item + "; ";
        //    }

        //    return Content(result);
        //}

        public IActionResult GetData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetData(string[] items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += item + "! ";
            }

            return Content(result);
        }

        public IActionResult GetDictionary(Dictionary<string, string> items)
        {
            string result = "";
            foreach (var item in items)
            {
                result += item.Key + "=" + item.Value + "; ";
            }

            return Content(result);
        }

        //public IActionResult GetDictionary()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult GetDictionary(Dictionary<string, string> items)
        //{
        //    string result = "";
        //    foreach (var item in items)
        //    {
        //        result += item.Key + "=" + item.Value + "; ";
        //    }

        //    return Content(result);
        //}

        //public IActionResult GetPhone(Phone3 myPhone)
        //{
        //    if (myPhone != null)
        //    {
        //        return Content($"Name:{myPhone.Name} Price:{myPhone.Price} Company:{myPhone.Manufacturer?.Name}");
        //    }

        //    return StatusCode(404);
        //}

        public IActionResult GetPhone()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPhone(Phone3 myPhone)
        {
            return Content($"Name: {myPhone?.Name}  Price:{myPhone.Price}  Company: {myPhone.Manufacturer?.Name}");
        }

        public IActionResult GetPhones()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPhones(Phone3[] phones)
        {
            string result = "";
            foreach (var p in phones)
                result += $"{p.Name} - {p.Price} - {p.Manufacturer?.Name} \n";
            return Content(result);
        }

        //public IActionResult AddUser(User2 user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string userInfo = $"Id: {user.Id}  Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
        //        return Content(userInfo);
        //    }

        //    return Content($"Количество ошибок: {ModelState.ErrorCount}");
        //}

        public IActionResult GetUserAgent([FromHeader(Name = "User_Agent")] string userAgent)
        {
            return Content(userAgent);
        }


        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser([FromQuery] User2 user)
        {
            string userInfo = $"Name: {user.Name}  Age: {user.Age}";
            return Content(userInfo);
        }
    }
}