using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MetanitExampleCoreMVC.CustomAttributes
{
    public class PersonNameAttribute : ValidationAttribute
    {
        string[] names;

        public PersonNameAttribute(string[] names)
        {
            this.names = names;
        }

        public override bool IsValid(object value)
        {
            if (names.Contains(value.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
