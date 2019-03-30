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
            UpdateNotMyChat();

        }
        public void UpdateMyChat()
        {
            user.mychat = new List<Chat>();
            DataSet ds = sql.SqlQuerySelect($"select id, name from chats where creatorid = '{n}'");
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Chat chat = new Chat(Convert.ToInt32(row[0]), Convert.ToString(row[1]));
                        user.AddMyChat(chat);
                    }
                }
            }
        }
        public void UpdateNotMyChat()
        {
            user.notmychat = new List<Chat>();
            DataSet ds = sql.SqlQuerySelect($"select id, name from chats where addedid = '{n}'");
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Chat chat = new Chat(Convert.ToInt32(row[0]), Convert.ToString(row[1]));
                        user.AddNotMyChat(chat);
                    }
                }
            }
        }
    }
}
