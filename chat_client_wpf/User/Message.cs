using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    public class Message
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MainUser { get; set; }
        public Message(string n, string c, bool t)
        {
            Name = n;
            Description = c;
            MainUser = t;
        }
    }
}
