using FluentValidation;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Validators;

public class CookValidator:AbstractValidator<Cook>
{
    public CookValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name cannot be empty");
    }
}