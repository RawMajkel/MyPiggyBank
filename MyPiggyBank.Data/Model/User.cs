using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    public class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<OperationCategory> OperationCategories { get; set; }
    }
}
