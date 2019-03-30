using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_client_wpf.Registration
{
    interface IRegistration
    {
        event Action OnRegistration;

        string[] LoadUser();
        void Close();
    }
}
