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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace chat_client_wpf.Login
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILoginForm
    {
        public event Action OnLogin;
        Login.Model model1;
        Login.Presenter presenter1;
        Registration.Registration registration;
        Registration.Model model;
        Registration.Presenter presenter;
        User.Model usermodel;
        User.Presenter userpresenter;
        User.User user;
        public MainWindow()
        {
            InitializeComponent();
            model1 = new Model();
            presenter1 = new Presenter(this, model1);
        }

        

        public void CreateUserForm(int n)
        {
            user = new User.User();
            usermodel = new User.Model(n);
            userpresenter = new User.Presenter(user, usermodel);
            user.Show();
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
            if (OnLogin != null)
            {
                OnLogin();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            registration = new Registration.Registration();
            model = new Registration.Model();
            presenter = new Registration.Presenter(registration, model);
            registration.Show();
        }
    }
}
