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

namespace chat_client_wpf.Registration
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window, IRegistration
    {
        public event Action OnRegistration;
        public Registration()
        {
            InitializeComponent();
        }
        public string[] LoadUser()
        {
            string[] mas = new string[2];
            mas[0] = this.textBox1.Text;
            mas[1] = this.textBox2.Text;
            return mas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnRegistration != null)
            {
                OnRegistration();
            }
        }
    }
}
