using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using System.Configuration;

namespace chat_client_wpf.User
{
    public class SimpleSend : ISend
    {
        public SocketIOSystem system;
        public SimpleSend(SocketIOSystem s)
        {
            system = s;
        }
        void ISend.send(ForSend message)
        {
            string json = JsonConvert.SerializeObject(message);
            system.send(json);
        }

    }
}
