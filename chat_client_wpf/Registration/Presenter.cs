using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace chat_client_wpf.Registration
{
    class Presenter
    {
        public IRegistration view;
        public Model model;

        public Presenter(IRegistration view, Model model)
        {
            this.view = view;
            this.model = model;
            view.OnRegistration += Reg;
        }

        public void Reg()
        {
            string username = view.LoadUser()[0];
            if (model.sql.SqlQuerySelect($"select username from users where username = '{username}'") == null)
            {
                model.sql.SqlQueryInsert("insert into users (username, password) values (@p, @p1)", view.LoadUser());
                view.Close();
            }
            else
            {
                MessageBox.Show("You have to change your username",
                    "Message",
                    MessageBoxButton.OK);
            }
        }
    }
}
