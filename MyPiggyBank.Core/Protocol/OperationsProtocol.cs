using AutoMapper;
using FluentValidation;
using MyPiggyBank.Data.Model;
using System;

namespace MyPiggyBank.Core.Protocol {
    public class OperationsQuery : QueryStringParams {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public Guid Resource { get; set; }
        public Guid OperationCategory { get; set; }
        public bool? IsIncome { get; set; }
        public DateTime? MinOccuredAt { get; set; }
        public DateTime? MaxOccuredAt { get; set; }
    }

    public class OperationsQueryValidator : AbstractValidator<OperationsQuery> {
        public OperationsQueryValidator() {
            // TODO what about rules for base class (QueryStringParams)?
            // TODO validation rules
        }
    }
}
