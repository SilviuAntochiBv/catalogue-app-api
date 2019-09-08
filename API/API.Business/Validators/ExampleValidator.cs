using FluentValidation;
using API.Domain.Entities;

namespace API.Business.Validators
{
    public class ExampleValidator : AbstractValidator<BaseEntity<long>>
    {
        public ExampleValidator()
        {
            // Here you should add rules by entering new lambdas
            // 
            // Example:
            // RuleFor(entity => entity.Id)
            //     .GreaterThanOrEqualTo(0)
            //     .WithMessage("Id must be bigger than 0");
        }
    }
}
