using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chat_client_wpf.User
{
    interface IUser
    {
        event Action ChatSelected;
        event Action UpdateU;
        event Action CreateNewChat;
        event Action SendMessage;
        event Action DeleteChat;
        


        List<Chat> Chats { get; }

        Chat SelectedChat { get; }

        void LoadMessages(ObservableCollection<Message> list);
        void ChangeLable(string name);
        void LoadChats(List<Chat> mychats);
        void OpenChat(Chat chat);
        string[] LoadNewChat();
        string Message();
        void AddNewMessage(Message message);
        void Close();
    }
}
