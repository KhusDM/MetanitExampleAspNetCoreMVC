using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;

namespace MetanitExampleCoreMVC.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Phone3> Phones { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
    }
}
