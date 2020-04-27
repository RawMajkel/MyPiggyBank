using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<CyclicOperation> CyclicOperations { get; set; }
    }
}
