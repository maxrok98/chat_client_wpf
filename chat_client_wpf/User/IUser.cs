using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.User
{
    interface IUser
    {
        event Action ChatSelected;
        event Action UpdateU;
        event Action CreateNewChat;


        List<Chat> Chats { get; }

        Chat SelectedChat { get; }

        void ChangeLable(string name);
        void LoadChats(List<Chat> mychats);
        void OpenChat(Chat chat);
        string[] LoadNewChat();
        void Close();
    }
}
