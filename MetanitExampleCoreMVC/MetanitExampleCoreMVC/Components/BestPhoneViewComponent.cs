using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetanitExampleCoreMVC.Components
{
    [ViewComponent]
    public class BestPhoneViewComponent : ViewComponent
    {
        IEnumerable<Phone2> phones;

        public BestPhoneViewComponent()
        {
            phones = new List<Phone2>()
            {
                new Phone2 {Title="iPhone 7", Price=56000},
                new Phone2 {Title="Idol S4", Price=26000 },
                new Phone2 {Title="Elite x3", Price=55000 },
                new Phone2 {Title="Honor 8", Price=23000 }
            };
        }

        public string Invoke()
        {
            var item = phones.OrderByDescending(x => x.Price).Take(1).FirstOrDefault();

            return $"Самый дорогой телефон: {item.Title}  Цена: {item.Price}";
        }
    }
}
