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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Experiments
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Chat> list { get; set; }

        public Chat chat
        {
            get { return (Chat)GetValue(ChatProperty); }
            set { SetValue(ChatProperty, value); }
        }

        public static readonly DependencyProperty ChatProperty = DependencyProperty.Register("chat", typeof(Chat), typeof(ItemsControl));

        public MainWindow()
        {
            InitializeComponent();
            list = new ObservableCollection<Chat>();
            
            Items.ItemsSource = list;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new Chat() { Name = "Taras", Description = "How you doing?" });
        }
    }
}
