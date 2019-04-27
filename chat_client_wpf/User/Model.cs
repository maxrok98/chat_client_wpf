using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    class Model
    {
        public MainUser user;
        public SqlMachine sql = new SqlMachine();
        int n = 0;
        public Model(int n)
        {
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

    }
}
