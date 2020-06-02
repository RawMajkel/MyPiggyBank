using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class ResourcesQueryValidator : AbstractValidator<ResourcesQuery>
    {
        public ResourcesQueryValidator()
        {
            // TODO what about rules for base class (QueryStringParams)?
            // TODO validation rules
        }
    }
}
