using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Accounts_Manager
{
    public class Account
    {
        public String username { get; set; }
        public String password { get; set; }
        public String location { get; set; }
        public Account(String username,String password, String location)
        {
            this.username = username;
            this.password = password;
            this.location = location;
        }
        public Account()
        {

        }
    }
}
