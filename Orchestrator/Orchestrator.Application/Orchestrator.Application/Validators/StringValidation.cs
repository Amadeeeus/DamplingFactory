using System.Data;
using FluentValidation;

namespace Orchestrator.Application.Validators;

public class StringValidation: AbstractValidator<string>
{
    public StringValidation()
    {
        RuleFor(x=>x).NotEmpty().NotNull().MaximumLength(3).MaximumLength(255).WithMessage("Maximum or minimum length exceeded");
    }
}