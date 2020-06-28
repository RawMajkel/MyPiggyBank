using MyPiggyBank.Core.Protocol.Base;
using System;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Requests
{
    public class CyclicOperationGetRequest : QueryStringParams
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MinEstimatedValue { get; set; }
        public decimal? MaxEstimatedValue { get; set; }
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public bool? IsIncome { get; set; }
        public int? MinPeriod { get; set; }
        public int? MaxPeriod { get; set; }
        public DateTime? MinNextOccurence { get; set; }
        public DateTime? MaxNextOccurence { get; set; }
    }
}
