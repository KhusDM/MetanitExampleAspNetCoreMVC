using MetanitExampleCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.CustomAttributes
{
    public class NamePasswordEqualAttribute : ValidationAttribute
    {
        public NamePasswordEqualAttribute()
        {
            ErrorMessage = "Имя и пароль не должны совпадать!";
        }

        public override bool IsValid(object value)
        {
            Person p = value as Person;

            if (p.Name == p.Password)
            {
                return false;
            }

            return true;
        }
    }
}
