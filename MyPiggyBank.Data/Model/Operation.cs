using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    public class Operation
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public decimal Value { get; set; }
        public DateTime OccuredAt { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
