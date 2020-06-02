using System.Collections.Generic;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Model {
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<OperationCategory> OperationCategories { get; set; }
    }
}
