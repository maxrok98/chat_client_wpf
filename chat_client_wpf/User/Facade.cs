using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    class Facade
    {
        public SocketIOSystem system1;
        public Facade(SocketIOSystem SSystem)
        {
            system1 = SSystem;
        }
        public void RunSystem1()
        {
            system1.RunSocket();
        }
    }
}
