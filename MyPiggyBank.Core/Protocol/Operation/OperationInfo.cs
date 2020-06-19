using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Operation
{
    public class OperationInfo
    {
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public decimal Value { get; set; }
        public DateTime OccuredAt { get; set; }
    }
}
