using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.Models
{
    public interface IRepository
    {
        List<Phone2> GetPhones();
    }
}
