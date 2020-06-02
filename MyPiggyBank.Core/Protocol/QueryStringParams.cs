using FluentValidation;
using System;

namespace MyPiggyBank.Core.Protocol {
    public abstract class QueryStringParams {
        public int Limit { get; set; } = 50;
		public int Page { get; set; } = 1;
		public Guid User { get; set; }
	}

    public class QueryStringParamsValidator : AbstractValidator<QueryStringParams> {
        public QueryStringParamsValidator() {
            RuleFor(q => q.Limit)
                .NotEmpty().InclusiveBetween(1, 100).WithMessage("Collection size limit should be set between 1 and 100.");
        }
    }
}
