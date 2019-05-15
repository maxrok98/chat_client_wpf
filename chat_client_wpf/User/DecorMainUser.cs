using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    class DecorMainUser : Decorator
    {
        public static void DecorMainU(Component component)
        {
            component.MainUser = true;
        }
    }
}
