using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System.Windows;
using System.Configuration;

namespace chat_client_wpf.User
{
    public class Model
    {
        public event Action ReceiveMessage;
        Socket socket = IO.Socket(ConfigurationManager.AppSettings["Socket1"]);
        public MainUser user;
        public Chat chat;
        public Message message;
        public SqlMachine sql = SqlMachine.getInstance();
        int n = 0;
        ISend typesend;
        public Model(int n, ISend send)
        {
            this.typesend = send;
            this.n = n;
            DataSet ds = sql.SqlQuerySelect($"select username from users where id = '{n}'");
            string name = "";
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    name = Convert.ToString(dr[0]);
                }
            }
            user = new MainUser(n, name);
            UpdateMyChat();
            SocketIOManager();
        }
        public void SocketIOManager()
        {
            socket.On(Socket.EVENT_CONNECT, () =>
            {

                //Console.WriteLine("Connected");

            });

            socket.On("return message", (data) =>
            {
                string d = data.ToString();
                ForReceive f = JsonConvert.DeserializeObject<ForReceive>(d);
                if(f.chat_id == chat.Id)
                {
                    message = new Message(f.user, f.text, false);

                    if(f.user_id == user.Id)
                    {
                        message.MainUser = true;
                    }
                    ReceiveMessage(); 
                }
            });
        }
        public void UpdateMyChat()
        {
            user.mychat = new List<Chat>();
            DataSet ds = sql.SqlQuerySelect($"select id, name, creatorid, addedid from chats where creatorid = '{n}'");
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Chat chat = new Chat(Convert.ToInt32(row[0]), Convert.ToString(row[1]), Convert.ToInt32(row[2]), Convert.ToInt32(row[3]));
                        user.AddMyChat(chat);
                    }
                }
            }
            ds = sql.SqlQuerySelect($"select id, name, creatorid, addedid from chats where addedid = '{n}'");
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Chat chat = new Chat(Convert.ToInt32(row[0]), Convert.ToString(row[1]), Convert.ToInt32(row[2]), Convert.ToInt32(row[3]));
                        user.AddMyChat(chat);
                    }
                }
            }
        }
        public void CreateNewChat(string[] chat)
        {
            if (sql.SqlQuerySelect($"select * from users where username = '{chat[1]}'") == null)
            {
                MessageBox.Show("This user does not exist");
            }
            else
            {
                DataSet ds = sql.SqlQuerySelect($"select * from users where username = '{chat[1]}'");

                int added = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                sql.SqlQueryInsert1("INSERT INTO chats (name, creatorid, addedid) VALUES (@p, @p1, @p2)", chat[0], user.Id, added);
                MessageBox.Show("New chat was created");
            }
        }
        public void LoadMessages(Chat chat)
        {
            //Check name of other user
            string OtherUserName = string.Empty;
            if (user.Id == chat.CreatorId)
            {
                DataSet ds = sql.SqlQuerySelect($"select username from users where id = '{chat.AddedId}'");
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        OtherUserName = Convert.ToString(dr[0]);
                    }
                }
            }
            else
            {
                DataSet ds = sql.SqlQuerySelect($"select username from users where id = '{chat.CreatorId}'");
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        OtherUserName = Convert.ToString(dr[0]);
                    }
                }
            }

            //Check for all messages with chat.id and write it to the user.nessages
            user.Messages = new System.Collections.ObjectModel.ObservableCollection<Message>();
            DataSet dst = sql.SqlQuerySelect($"select chat_id, sender_id, text from masseges where chat_id = '{chat.Id}'");
            if (dst != null)
            {
                foreach (DataTable dt in dst.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Message message;
                        if (Convert.ToInt32(row[1]) == user.Id)
                            message = new Message(user.Username, Convert.ToString(row[2]), true);
                        else
                            message = new Message(OtherUserName, Convert.ToString(row[2]), false);
                        user.Messages.Add(message);
                    }
                }
            }
        }

        public void DeleteChat(Chat chat)
        {
            sql.Delete($"delete from \"chats\" where \"id\" = {chat.Id}");
            UpdateMyChat();
        }
        public void SendMessage(Chat chat, string text)
        {
            
            ForSend f = new ForSend();
            f.user_id = user.Id;
            f.user = user.Username;
            f.chat_id = chat.Id;
            f.text = text;
            typesend.send(socket, f);
        }
        

    }
    public class ForSend
    {
        public int user_id;
        public string user;
        public int chat_id;
        public string text;

    }
    public class ForReceive
    {
        public int user_id;
        public string user;
        public string text;
        public int chat_id;
    }
}
