using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;

namespace chat_client_wpf.User
{
    public interface ISend
    {
        void send(Socket socket, ForSend message);
    }
}
