using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    class DecorDescription : Decorator
    {
        public static void DecorDesc(Component component, string desc)
        {
            component.Description = desc;
        }
    }
}
