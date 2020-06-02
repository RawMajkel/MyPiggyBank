using System;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Model {
    public class CyclicOperation : BaseEntity
    {
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public decimal? EstimatedValue { get; set; }
        public int Period { get; set; }
        public DateTime NextOccurence { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual OperationCategory OperationCategory { get; set; }
    }
}
