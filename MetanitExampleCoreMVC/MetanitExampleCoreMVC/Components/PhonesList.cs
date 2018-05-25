using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;

namespace MetanitExampleCoreMVC.Components
{
    public class PhonesList : ViewComponent
    {
        IRepository repository;

        public PhonesList(IRepository repository)
        {
            this.repository = repository;
        }

        public string Invoke(int maxPrice, int minPrice = 0)
        {
            int count = repository.GetPhones().Count(x => x.Price > minPrice && x.Price < maxPrice);

            return $"В диапазоне от {minPrice} до {maxPrice} найдено {count} модели(ей)";
        }
    }
}
