using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace chat_client_wpf.Login
{
    class Presenter
    {
        ILoginForm view;
        Model model;

        public Presenter(ILoginForm view, Model model)
        {
            this.view = view;
            this.model = model;
            view.OnLogin += Log;
        }

        public void Log()
        {
            string username = view.LoadUser()[0];
            string password = view.LoadUser()[1];
            if (model.sql.SqlQuerySelect($"select * from users where username = '{username}' and password = '{password}'") == null)
            {
                MessageBox.Show("Password or username are wrong!",
                    "Message",
                    MessageBoxButton.OK);

            }
            else
            {
                DataSet ds = model.sql.SqlQuerySelect($"select * from users where username = '{username}' and password = '{password}'");
                int n = 0;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var cells = row[0];
                        n = Convert.ToInt32(cells);
                    }
                }
                view.CreateUserForm(n);
            }
        }
    }
}
