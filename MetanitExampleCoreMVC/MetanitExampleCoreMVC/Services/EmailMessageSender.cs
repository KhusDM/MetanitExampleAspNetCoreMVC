using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.Services
{
    public class EmailMessageSender:IMessageSender
    {
        public string SendMessage()
        {
            return "сообщение отправлено на email";
        }
    }
}
