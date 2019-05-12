using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public event Action SendMessage;


        public ObservableCollection<Message> list { get; set; }

        public Message message
        {
            get { return (Message)GetValue(ChatProperty); }
            set { SetValue(ChatProperty, value); }
        }

        public static readonly DependencyProperty ChatProperty = DependencyProperty.Register("message", typeof(Message), typeof(ItemsControl));

        public User()
        {
            
            InitializeComponent();
            list = new ObservableCollection<Message>();
            Messages.ItemsSource = list;
            
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

        public void LoadMessages(ObservableCollection<Message> list)
        {
            this.list = list;
            Messages.ItemsSource = this.list;
            
        }

        public void AddNewMessage(Message message)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.list.Add(message);
                
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage.Invoke();
        }
        public string Message()
        {
            string m = MessageField.Text;
            MessageField.Text = string.Empty;
            return m;
        }
    }
}
