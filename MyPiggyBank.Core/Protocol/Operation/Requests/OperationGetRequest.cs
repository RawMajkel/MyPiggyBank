using MyPiggyBank.Core.Protocol.Base;
using System;

namespace MyPiggyBank.Core.Protocol.Operation.Requests
{
    public class OperationGetRequest : QueryStringParams
    {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? OperationCategoryId { get; set; }
        public bool? IsIncome { get; set; }
        public DateTime? MinOccuredAt { get; set; }
        public DateTime? MaxOccuredAt { get; set; }
    }
}
