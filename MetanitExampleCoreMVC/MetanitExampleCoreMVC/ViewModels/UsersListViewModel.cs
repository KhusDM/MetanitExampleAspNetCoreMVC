using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MetanitExampleCoreMVC.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public SelectList Companies { get; set; }
        public string Name { get; set; }
    }
}
