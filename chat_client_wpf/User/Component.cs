using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    public abstract class Component
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MainUser { get; set; }
        
        
    }
}
