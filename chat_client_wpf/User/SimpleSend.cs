using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

namespace chat_client_wpf.User
{
    public class SimpleSend : ISend
    {
        void ISend.send(Socket socket, ForSend message)
        {
            string json = JsonConvert.SerializeObject(message);
            socket.Emit("send message", json);
        }
    }
}
