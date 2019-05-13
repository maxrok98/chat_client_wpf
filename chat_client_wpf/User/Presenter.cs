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
            view.SendMessage += OnSendMessage;
            model.ReceiveMessage += OnReceiveMessage;
            view.DeleteChat += OnDeleteChat;
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
                //this.view.OpenChat(chat);
                model.LoadMessages(chat);
                model.chat = chat;
                view.LoadMessages(model.user.Messages);
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
            view.LoadMessages(null);
        }
        public void OnCreateNewChat()
        {
            model.CreateNewChat(view.LoadNewChat());
            OnUpdate();
        }
        public void OnReceiveMessage()
        {
            view.AddNewMessage(model.message);
        }
        public void OnSendMessage()
        {
            model.SendMessage(view.SelectedChat, view.Message());
        }
        public void OnDeleteChat()
        {
            model.DeleteChat(view.SelectedChat);
            view.LoadChats(model.user.MyChats());
            view.LoadMessages(null);
        }
        
    }
}
