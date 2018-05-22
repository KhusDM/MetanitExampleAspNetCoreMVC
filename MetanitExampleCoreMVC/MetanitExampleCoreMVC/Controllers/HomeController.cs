using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.Util;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MetanitExampleCoreMVC.Controllers
{
    //[NonController]
    public class HomeController : Controller
    {
        readonly IHostingEnvironment hostingEnvironment;
        MobileContext db;

        public HomeController(MobileContext context, IHostingEnvironment hostingEnvironment)
        {
            db = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }

        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.PhoneId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return "Спасибо, " + order.User + ", за покупку!";
        }

        //[NonAction]
        //[ActionName("Welcome")]
        public string Hello(int id)
        {
            return $"id={id}";
        }

        [HttpGet]
        public IActionResult Square()
        {
            return View();
        }

        //[HttpPost]
        //public string Square(int altitude, int height)
        //{
        //    double square = altitude * height / 2;
        //    return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        //}

        [HttpPost]
        public string SquareResponse()
        {
            string altitudeString = Request.Form.FirstOrDefault(p => p.Key == "altitude").Value;
            int altitude = Int32.Parse(altitudeString);

            string heightString = Request.Form.FirstOrDefault(p => p.Key == "height").Value;
            int height = Int32.Parse(heightString);

            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        }

        //public string Square(Geometry geometry)
        //{
        //    return $"Площадь треугольника с основанием {geometry.Altitude} и высотой {geometry.Height} равна {geometry.GetSquare()}";
        //}

        //public string Square()
        //{
        //    string altitudeString = Request.Query.FirstOrDefault(p => p.Key == "altitude").Value;
        //    int altitude = Int32.Parse(altitudeString);

        //    string heightString = Request.Query.FirstOrDefault(p => p.Key == "height").Value;
        //    int height = Int32.Parse(heightString);

        //    double square = altitude * height / 2;
        //    return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        //}

        //public string Sum(int[] nums)
        //{
        //    return $"Сумма чисел равна {nums.Sum()}";
        //}

        public string Sum(Geometry[] geoms)
        {
            return $"Сумма площадей равна {geoms.Sum(g => g.GetSquare())}";
        }

        public HtmlResult GetHtml()
        {
            return new HtmlResult("<h2>Привет ASP.NET Core</h2>");
        }

        //public IActionResult GetVoid()
        //{
        //    return new EmptyResult();
        //}

        public IActionResult GetVoid()
        {
            return new NoContentResult();
        }

        public JsonResult GetName()
        {
            string name = "Tom";
            return Json(name);
        }

        public JsonResult GetUser()
        {
            User user = new User { Name = "Tom", Age = 28 };
            return Json(user);
        }

        public IActionResult Index2()
        {
            return RedirectToAction("Square", "Home", new { altitude = 10, height = 3 });
        }

        public IActionResult Index3()
        {
            return RedirectToRoute("default", new { controller = "Home", action = "Square", height = 2, altitude = 20 });
        }

        public IActionResult Index4()
        {
            return StatusCode(401);
        }

        public IActionResult Index5()
        {
            return NotFound("No");
        }

        public IActionResult Index6(int age)
        {
            if (age < 18)
            {
                return Unauthorized();
            }

            return Content("accept");
        }

        public IActionResult Index7(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return BadRequest("Не указаны параметры запроса");
            }
            return View();
        }

        public IActionResult Index8()
        {
            return Ok("Запрос успешно выполнен");
        }

        public IActionResult GetFile()
        {
            string file_path = Path.Combine(hostingEnvironment.ContentRootPath, "Files/1.txt");
            string file_type = "application/octet-stream";
            string file_name = "1.txt";

            return PhysicalFile(file_path, file_type, file_name);
        }

        public FileResult GetBytes()
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, "Files/1.txt");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/octet-stream";
            string file_name = "1.txt";

            return File(mas, file_type, file_name);
        }
    }

    public class Geometry
    {
        public int Altitude { get; set; }
        public int Height { get; set; }

        public double GetSquare()
        {
            return Altitude * Height / 2;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
