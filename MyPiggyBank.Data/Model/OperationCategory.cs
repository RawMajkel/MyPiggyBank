using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    public class OperationCategory
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<CyclicOperation> CyclicOperations  { get; set; }
    }
}
