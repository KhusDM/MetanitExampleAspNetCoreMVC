using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.Models
{
    public class PhoneRepository : IRepository
    {
        public List<Phone2> GetPhones()
        {
            return new List<Phone2>
            {
                new Phone2 {Title="iPhone 7", Price=56000},
                new Phone2 {Title="Idol S4", Price=26000 },
                new Phone2 {Title="Elite x3", Price=55000 },
                new Phone2 {Title="Honor 8", Price=23000 },
                new Phone2 {Title="Pixel XL", Price= 40000 }
            };
        }
    }
}
