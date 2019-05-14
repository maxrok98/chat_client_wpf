using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

namespace chat_client_wpf.User
{
    public class SocketIOSystem
    {
        public event Action ReceiveMessageFromSocket;
        public Socket socket = IO.Socket(ConfigurationManager.AppSettings["Socket1"]);
        public ForReceive forReceive;

        public void RunSocket()
        {
            socket.On(Socket.EVENT_CONNECT, () =>
            {

                //Console.WriteLine("Connected");

            });

            socket.On("return message", (data) =>
            {
                string d = data.ToString();
                forReceive = JsonConvert.DeserializeObject<ForReceive>(d);

                ReceiveMessageFromSocket();

            });
        }
        public void send(string json)
        {
            RunSocket();
            socket.Emit("send message", json);
        }
    }
}
