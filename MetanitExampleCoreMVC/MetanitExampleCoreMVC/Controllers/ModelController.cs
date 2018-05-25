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
    }
}