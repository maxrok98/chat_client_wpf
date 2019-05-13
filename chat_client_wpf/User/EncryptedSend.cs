using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;

namespace chat_client_wpf.User
{
    class EncryptedSend : ISend
    {
        public void send(Socket socket, ForSend message)
        {
            //Encrypted sending
        }
    }
}
