using AutoMapper;
using MyPiggyBank.Data.Model;
using System;

namespace MyPiggyBank.Core.Protocol.Query {
    public class OperationsQuery : QueryStringParams
    {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public Guid Resource { get; set; }
        public Guid OperationCategory { get; set; }
        public bool? IsIncome { get; set; }
        public DateTime? MinOccuredAt { get; set; }
        public DateTime? MaxOccuredAt { get; set; }
    }
}
