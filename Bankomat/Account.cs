using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat
{
    public class Account
    {
        private string _username;
        private string _password;
        private ContoCorrente _contoCorrente;
        public bool bloccoAccount = false;
        public int tentativi = 3;

        public Account(string username, string password)
        {
            _username = username;
            _password = password;
            _contoCorrente = new ContoCorrente();
        }
        public ContoCorrente contoCorrente { get { return _contoCorrente; } set { _contoCorrente = value; } }
        public string username { get { return _username; } set { _username = value; } }
        public string password { get { return _password; } set { _password = value; } }

    }
}
