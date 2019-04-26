using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chat_client_wpf.User
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window, IUser
    {
        public event Action ChatSelected;
        public event Action UpdateU;
        public event Action CreateNewChat;
        public User()
        {
            InitializeComponent();
        }

        public List<Chat> Chats
        {
            get { return null; }
        }
        

        public Chat SelectedChat
        {
            get { return ChatsList.SelectedItem as Chat; }
        }



        public void ChangeLable(string name)
        {
            this.Label.Text = name + ", you are welcome!";
        }

        public void LoadChats(List<Chat> mychats)
        {
            this.ChatsList.ItemsSource = mychats;
        }

        public string[] LoadNewChat()
        {
            string[] chat = new string[2];
            chat[0] = NewChat.Text;
            chat[1] = UserForNewChat.Text;
            NewChat.Text = "";
            UserForNewChat.Text = "";
            return chat;
        }

        public void OpenChat(Chat chat)
        {
            MessageBox.Show($"Open chat {chat.Name}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CreateNewChat != null)
                CreateNewChat();
        }

        private void ChatsList_Selected(object sender, RoutedEventArgs e)
        {
            if (ChatSelected != null)
                ChatSelected();
        }

        private void ChatsList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChatSelected.Invoke();
        }

        private void ChatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatSelected.Invoke();
        }
    }
}
