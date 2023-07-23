using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_Data
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public bool KeepLoggedIn { get; set; }
        public List<int> Actions { get; set; }
        public String[] Roles { get; set; }
    }
}