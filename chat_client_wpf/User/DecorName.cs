using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    public class DecorName : Decorator
    {
        public static void DecorN(Component component, string name)
        {
            component.Name = name;
        }
    }
}
