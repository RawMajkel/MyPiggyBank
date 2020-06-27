using MyPiggyBank.Core.Protocol.Base;
using System;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Responses
{
    public class CyclicOperationResponse : QueryStringParams
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? EstimatedValue { get; set; }
        public decimal? MinEstimatedValue { get; set; }
        public decimal? MaxEstimatedValue { get; set; }
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public bool? IsIncome { get; set; }
        public int? Period { get; set; }
        public int? MinPeriod { get; set; }
        public int? MaxPeriod { get; set; }
        public DateTime NextOccurence { get; set; }
        public DateTime? MinNextOccurence { get; set; }
        public DateTime? MaxNextOccurence { get; set; }
    }
}
