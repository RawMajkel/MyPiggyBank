using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
