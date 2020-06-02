using AutoMapper;
using FluentValidation;
using MyPiggyBank.Data.Model;
using System;
using System.Text;

namespace MyPiggyBank.Core.Protocol {

    public class ResourcesQuery : QueryStringParams {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Currency { get; set; }
    }

    public class ResourcesQueryValidator : AbstractValidator<ResourcesQuery> {
        public ResourcesQueryValidator() {
            // TODO what about rules for base class (QueryStringParams)?
            // TODO validation rules
        }
    }
}
