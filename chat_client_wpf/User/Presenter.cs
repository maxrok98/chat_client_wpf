using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace chat_client_wpf.User
{
    class Presenter
    {
        public IUser view;
        public Model model;
        public Presenter(IUser view, Model model)
        {
            this.view = view;
            this.model = model;
            var mychats = model.user.MyChats();

            view.ChatSelected += OnMyChatSelected;
            view.UpdateU += OnUpdate;
            view.CreateNewChat += OnCreateNewChat;
            if (mychats != null)
            {
                view.LoadChats(mychats);
            }
            this.view.ChangeLable(this.model.user.Username);
        }

        public void OnMyChatSelected()
        {
            if (this.view.SelectedChat != null)
            {
                var id = this.view.SelectedChat.Id;
                var chat = this.model.user.GetMyById(id);
                this.view.OpenChat(chat);
            }
        }
        public void OnUpdate()
        {
            model.UpdateMyChat();

            var mychats = model.user.MyChats();
            if (mychats != null)
            {
                view.LoadChats(mychats);
            }
        }
        public void OnCreateNewChat()
        {
            string[] chat = view.LoadNewChat();
            if (model.sql.SqlQuerySelect($"select * from users where username = '{chat[1]}'") == null)
            {
                MessageBox.Show("This user does not exist");
            }
            else
            {
                DataSet ds = model.sql.SqlQuerySelect($"select * from users where username = '{chat[1]}'");

                int added = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                model.sql.SqlQueryInsert1("INSERT INTO chats (name, creatorid, addedid) VALUES (@p, @p1, @p2)", chat[0], model.user.Id, added);
                MessageBox.Show("New chat was created");
            }
        }
    }
}
