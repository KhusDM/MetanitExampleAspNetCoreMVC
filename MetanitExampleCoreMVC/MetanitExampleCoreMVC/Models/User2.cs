using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.Models
{
    //[Bind("Name")]
    public class User2
    {
        public int Id { get; set; }
        [BindingBehavior(BindingBehavior.Required)]
        public string Name { get; set; }
        public int Age { get; set; }
        [BindingBehavior(BindingBehavior.Never)]    
        public bool HasRight { get; set; }
    }
}
