using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Services;

namespace MetanitExampleCoreMVC.Services
{
    public class SimpleTimeService : ITimeService
    {
        public string Time { get; }

        public SimpleTimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
