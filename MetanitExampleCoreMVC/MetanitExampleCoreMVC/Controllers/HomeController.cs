using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Models;
using MetanitExampleCoreMVC.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using MetanitExampleCoreMVC.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MetanitExampleCoreMVC.Controllers
{
    //[NonController]
    //[Route("Store")]
    public class HomeController : Controller
    {
        readonly ITimeService timeService;
        readonly IHostingEnvironment hostingEnvironment;
        MobileContext db;

        public HomeController(MobileContext context, IHostingEnvironment hostingEnvironment, ITimeService timeService)
        {
            db = context;
            this.hostingEnvironment = hostingEnvironment;
            this.timeService = timeService;
        }

        //[Route("Main")]     // сопоставляется с Home/Main, либо с Store/Main
        //[Route("Index")] // сопоставляется с Home/Index, либо с Store/Index
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

        public void Headers()
        {
            string table = "";
            foreach (var header in Request.Headers)
            {
                table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
            }

            Response.WriteAsync(String.Format("<table>{0}</table>", table));
        }

        public void NotFound()
        {
            Response.StatusCode = 404;
            Response.WriteAsync("Not Found!");
        }

        public string TimeService()
        {
            return timeService.Time;
        }

        public string TimeServiceFromServices([FromServices]ITimeService timeService)
        {
            return timeService.Time;
        }

        public string TimeServiceRequestServices()
        {
            ITimeService timeService = HttpContext.RequestServices.GetService<ITimeService>();

            return timeService?.Time;
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Hello ASP.NET Core";

        //    return View();
        //}

        //public IActionResult About()
        //{
        //    ViewBag.Message = "Hello ASP.NET Core";

        //    return View();
        //}

        //public IActionResult About()
        //{
        //    ViewBag.Countries = new List<string> { "Бразилия", "Аргентина", "Уругвай", "Чили" };
        //    return View();
        //}

        public IActionResult About()
        {
            IEnumerable<string> countries = new List<string>() { "Бразилия", "Аргентина", "Уругвай", "Чили" };

            return View(countries);
        }

        public IActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password, int age, string comment, bool isMarried, string color, string[] phones)
        {
            string result = "";
            foreach (string p in phones)
            {
                result += p;
                result += ";";
            }

            string authData = $"Login: {login}   Password: {password}   Age: {age}  Comment: {comment}  Is married: {isMarried}  color: {color}  phones: {result}";
            return Content(authData);
        }

        public IActionResult GetRouteData()
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();

            return Content($"controller: {controller} | action: {action}");
        }

        [Route("homepage")]
        public IActionResult AttributeRouteMethod()
        {
            return Content("Hello ASP.NET MVC 6");
        }

        [Route("{id:int}/{name:maxlength(10)}")]
        public IActionResult AttributeRouteMethod2(int id, string name)
        {
            return Content($" id={id} | name={name}");
        }

        public IActionResult About2()
        {
            string contentUrl = Url.Content("~/lib/jquery/dist/jquery.js");
            string actionUrl = Url.Action("Index", "Home");
            return Content(contentUrl);
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
