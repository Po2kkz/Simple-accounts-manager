using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Accounts_Manager
{
    class User
    {
        public bool registered { get; set; }
        public String password { get; set; }
        public String Decryptedpassword { get; set; }
        public User(bool registered,String password)
        {
            this.registered = registered;
            this.password = password;
        }
    }
}
