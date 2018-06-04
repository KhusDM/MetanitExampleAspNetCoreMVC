using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult Index()
        {
            Person p = new Person
            {
                Name = "Элронд Смит",
                Age = 58,
                Email = "elrond.smith@gmail.com",
                Password = "qwerty"
            };

            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            //if (string.IsNullOrEmpty(person.Name))
            //{
            //    ModelState.AddModelError("Name", "Некорректное имя");
            //}
            //else if (person.Name.Length > 5)
            //{
            //    ModelState.AddModelError("Name", "Недопустимая длина строки");
            //}
            //if (ModelState.IsValid)
            //{
            //    return Content($"{person.Name} - {person.Email}");
            //}

            //if (ModelState.IsValid)
            //{
            //    return Content($"{person.Name} - {person.Email}");
            //}
            //else
            //{
            //    return View(person);
            //}

            if (person.Name == person.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                return Content($"{person.Name} - {person.Email}");
            }

            return View(person);
        }

        public IActionResult CheckEmail(string email)
        {
            if (email == "admin@mail.ru" || email == "aaa@gmail.com")
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}